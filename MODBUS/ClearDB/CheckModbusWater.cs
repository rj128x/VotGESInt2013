using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Text;
using VotGES;
using VotGES.Piramida;

namespace ClearDB {
	public class CheckModbusWater {
		public static void CheckData() {
			DateTime date = DateTime.Now.AddHours(-2).AddMinutes(-10);
			SqlConnection con = PiramidaAccess.getConnection("PMin");
			String sql = String.Format("select count(*) from data where parnumber=4 and objtype=2 and object=3  and item in(1,2,3) and data_date>'{0}'", date.ToString(DBClass.DateFormat));
			try {
				con.Open();
				SqlCommand com = con.CreateCommand();
				com.CommandText=sql;
				object cnt=com.ExecuteScalar();
				int count = Int32.Parse(cnt.ToString());
				if (count == 0) {
					MailClass.sendMail("Нет данных modbus за последние 10 минут");
				}
			}
			catch (Exception e) {
				Logger.Info("Не удалось обработать данные modbus");
			}

			date = DateTime.Now.AddHours(-2).AddMinutes(-10);
			con = PiramidaAccess.getConnection("PMin");
			sql = String.Format("select count(value0),item from data where object=3 and objtype=2 and parnumber=4 and item>=201 and item<=210  and value0=ValueMax and value0>0 and data_date>='{0}' group by item", date.ToString(DBClass.DateFormat));
			try {
				con.Open();
				SqlCommand com = con.CreateCommand();
				com.CommandText = sql;

				SqlDataReader reader = com.ExecuteReader();
				List<int> gaS = new List<int>();
				while (reader.Read()) {
					int item = reader.GetInt32(0);
					int cnt = reader.GetInt32(1);
					if (cnt > 5)
						gaS.Add(item - 200);
				}				
				if (gaS.Count()>0) {
					MailClass.sendMail(String.Format("Нет данных modbus по ГА №№ {0}",String.Join(",",gaS)));
				}
			} catch (Exception e) {
				Logger.Info("Не удалось обработать данные modbus");
			}
		}

		

	}
}
