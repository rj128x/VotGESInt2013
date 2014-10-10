using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using VotGES;
using VotGES.Piramida;

namespace ClearDB {
	public class RecalcPuskStop {

		public static void RecalcData(DateTime dateStart, DateTime dateEnd) {
			Logger.Info(String.Format("{0} - {1}", dateStart, dateEnd));

			string sel = String.Format("SELECT * FROM DATA where parnumber=13 and data_date>='{0}' and data_date<='{1}'",
				dateStart.ToString(DBClass.DateFormat), dateEnd.ToString(DBClass.DateFormat));
			SqlConnection con = null;
			SqlConnection con2 = null;
			try {
				con = PiramidaAccess.getConnection("PSV");
				con.Open();

				con2 = PiramidaAccess.getConnection("PSV");
				con2.Open();

				SqlCommand command = con.CreateCommand();
				command.CommandText = sel;
				SqlDataReader reader = command.ExecuteReader();

				while (reader.Read()) {
					DateTime date = DateTime.Parse(reader["data_date"].ToString());
					Logger.Info("====" + date.ToString(DBClass.DateFormat));
					
					sel = String.Format("SELECT top 1 data_date FROM DATA where parnumber=13 and data_date<'{0}' and item={1} and object={2} and objtype={3} order by data_date desc",
						date.ToString(DBClass.DateFormat), reader["item"], reader["object"], reader["objtype"]);

					SqlCommand cmd = con2.CreateCommand();
					cmd.CommandText = sel;					

					Object ld=cmd.ExecuteScalar();
					DateTime lastDate = DateTime.Parse(ld.ToString());
					
					Logger.Info("====lastDate: " + lastDate.ToString(DBClass.DateFormat));

					double timeChange = (date.Ticks- lastDate.Ticks) / (10000000.0 * 60.0);
										
					string upd = String.Format("UPDATE data set value1={0} where data_date='{1}' and parnumber=13 and item={2} and object={3} and objtype={4}",
						timeChange, date.ToString(DBClass.DateFormat), reader["item"], reader["object"], reader["objtype"]);
					cmd = con2.CreateCommand();
					cmd.CommandText = upd;
					cmd.ExecuteNonQuery();
					Logger.Info("=====ok");
				}
				reader.Close();
			}
			catch (Exception e) {
				Logger.Info(e.ToString());
			}
			finally {
				try { 					
					con.Close();
					con2.Close();
				}

				catch { };
			}
		}
	}
}
