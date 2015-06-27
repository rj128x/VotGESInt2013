using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using VotGES.Piramida;

namespace VotGES.ModesCentre {
	public class MCPBRData {
		public static string InsertIntoHeader = "INSERT INTO Data (parnumber,object,item,value0,objtype,data_date,rcvstamp,season)";
		public static string InsertInfoFormat = "SELECT {0}, {1}, {2}, {3}, {4}, '{5}', '{6}', {7}";
		public static string DateFormat = "yyyy-MM-dd HH:mm:ss";

		public SortedList<DateTime, double> Data { get; set; }
		public int Item { get; set; }
		public MCSettingsRecord DataSettings;

		public MCPBRData(int mcCode) {
			foreach (MCSettingsRecord rec in MCSettings.Single.MCData) {
				if (rec.MCCode == mcCode) {
					Item = rec.PiramidaCode;
					DataSettings = rec;
				}
			}
			Data = new SortedList<DateTime, double>();
		}

		public void AddValue(DateTime date, double val) {
			try {
				Data.Add(date, val);
			} catch (Exception e) {
				Logger.Info("ошибка при разборе пбр. дубль в исходных днных " + e);
			}
		}

		protected SortedList<DateTime, double> createInegratedData() {
			SortedList<DateTime, double>  integr = new SortedList<DateTime, double>();
			double sum = 0;
			foreach (KeyValuePair<DateTime, double> de in Data) {
				KeyValuePair<DateTime, double> next =  Data.Last();
				if (de.Key == next.Key)
					break;
				DateTime dt = de.Key.AddMinutes(0);
				double step = (de.Value + next.Value) / 60;
				while (dt < next.Key) {
					integr.Add(dt, sum);
					sum += step;
					dt = dt.AddMinutes(1);
				}
			}
			return integr;
		}

		protected bool writeToDB(string DBName,SortedList<DateTime,double>data,int parnumber) {

			SqlConnection con = null;
			bool ok = false;
			try {
				con = PiramidaAccess.getConnection(DBName);
				con.Open();
				SqlTransaction transact = con.BeginTransaction();
				string delStr = String.Format("DELETE FROM DATA WHERE object=0 and objtype=2 and item={0} and parnumber={1} and data_date>='{2}' and data_date<='{3}'",
				Item,parnumber, Data.First().Key.ToString(DateFormat), Data.Last().Key.ToString(DateFormat));

				SqlCommand commandDel = transact.Connection.CreateCommand();
				commandDel.CommandText = delStr;
				commandDel.Transaction = transact;
				//Logger.Info(delStr);
				commandDel.ExecuteNonQuery();

				List<string> inserts = new List<string>();
				foreach (KeyValuePair<DateTime, double> de in data) {
					string ins = String.Format(InsertInfoFormat, parnumber, 0, Item, de.Value, 2, de.Key.ToString(DateFormat), DateTime.Now.ToString(DateFormat), DBSettings.getSeason(de.Key));
					inserts.Add(ins);
					if (inserts.Count % 30 == 0 || de.Key == data.Last().Key) {
						string insertsSQL = String.Join("\nUNION ALL\n", ins);
						string insertSQL = String.Format("{0}\n{1}", InsertIntoHeader, insertsSQL);
						SqlCommand commandIns = transact.Connection.CreateCommand();
						commandIns.CommandText = insertSQL;
						commandIns.Transaction = transact;
						commandIns.ExecuteNonQuery();
					}
				}

				transact.Commit();
				ok = true;
			} catch (Exception e) {
				Logger.Info("ошибка при записи ПБР в базу " + e);
				ok = false;

			} finally {
				try {
					con.Close();
				} catch { }
			}
			return ok;
		}

		public void addAutooperData(List<string> data) {
			if (DataSettings.Autooper) {
				foreach (KeyValuePair<DateTime, double> de in Data) {
					data.Add(String.Format("{0};{1};{2}", Item, de.Key.ToString("yyyyMMddTHHmm"), de.Value));
				}
			}
		}

		public bool ProcessData() {
			bool ok = false;
			ok=ok&&writeToDB("P3000", Data, 212);
			if (DataSettings.WriteIntegratedData) {
				SortedList<DateTime, double> integr = createInegratedData();
				ok=ok&&writeToDB("P3000", integr, 204);
			}
			return ok;
		}
	}

}
