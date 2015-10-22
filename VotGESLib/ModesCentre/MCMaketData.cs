using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using VotGES.Piramida;

namespace VotGES.ModesCentre {
	public class MCMaketData {
		public static string InsertIntoHeader = "INSERT INTO Data (parnumber,object,item,value0,objtype,data_date,rcvstamp,season)";
		public static string InsertInfoFormat = "SELECT {0}, {1}, {2}, {3}, {4}, '{5}', '{6}', {7}";
		public static string DateFormat = "yyyy-MM-dd HH:mm:ss";

		public DateTime DateStart { get; set; }
		public DateTime DateEnd { get; set; }
		public SortedList<DateTime, SortedList<int, double>> Data;
		public List<int> processObjects;
		public List<int> processVars;
		public List<int> processItems;
		public MCMaketData(DateTime dateStart, DateTime dateEnd) {
			DateStart = dateStart;
			DateEnd = dateEnd;
			Data = new SortedList<DateTime, SortedList<int, double>>();
			DateTime dt = dateStart.AddHours(0);
			while (dt <= DateEnd) {
				Data.Add(dt, new SortedList<int, double>());
				DateTime de = dt.AddHours(1);
				dt = de.AddHours(0);
			}
			processSettings();
		}

		public MCMaketData() {
			Data = new SortedList<DateTime, SortedList<int, double>>();
			processSettings();
		}

		protected void processSettings() {
			processObjects = new List<int>();
			processVars = new List<int>();
			processItems = new List<int>();
			foreach (MCMaketRecord record in MCSettings.Single.MaketData) {
				if (!processObjects.Contains(record.MCcode))
					processObjects.Add(record.MCcode);

				foreach (MCMaketVar var in record.Vars) {
					if (!processVars.Contains(var.MCCode))
						processVars.Add(var.MCCode);
					if (!processItems.Contains(var.PiramidaCode))
						processItems.Add(var.PiramidaCode);
				}
			}
		}

		public void processPiramidaCode(int MCObjCode, int MCVarCode, DateTime date, double val) {
			foreach (MCMaketRecord record in MCSettings.Single.MaketData) {
				if (record.MCcode == MCObjCode) {
					foreach (MCMaketVar var in record.Vars) {
						if (var.MCCode == MCVarCode) {
							try {
								if (!Data.ContainsKey(date))
									Data.Add(date, new SortedList<int, double>());
								Data[date].Add(var.PiramidaCode, val);
							}
							catch {  }
						}
					}
				}
			}
		}

		public bool writeToDB(string DBName) {
			SqlConnection con = null;
			bool ok = false;
			try {
				con = PiramidaAccess.getConnection(DBName);
				con.Open();
				SqlTransaction transact = con.BeginTransaction();
				List<int> items = new List<int>();
				string delStr = String.Format("DELETE FROM DATA WHERE object=53500 and objtype=2 and item in ({0}) and parnumber={1} and data_date>='{2}' and data_date<='{3}'",
				String.Join(",", processItems), 212, Data.First().Key.ToString(DateFormat), Data.Last().Key.ToString(DateFormat));

				SqlCommand commandDel = transact.Connection.CreateCommand();
				commandDel.CommandText = delStr;
				commandDel.Transaction = transact;
				Logger.Info("Удаление Макета");
				commandDel.ExecuteNonQuery();

				List<string> inserts = new List<string>();
				foreach (KeyValuePair<DateTime,SortedList<int,double>> de in Data) {
					foreach (KeyValuePair<int, double> var in de.Value) {
						string ins = String.Format(InsertInfoFormat, 212, 53500, var.Key, var.Value.ToString().Replace(',', '.'), 2, de.Key.ToString(DateFormat), DateTime.Now.ToString(DateFormat), DBSettings.getSeason(de.Key));
						inserts.Add(ins);
						if (inserts.Count % 100 == 0 || de.Key == Data.Last().Key) {
							string insertsSQL = String.Join("\nUNION ALL\n", inserts);
							string insertSQL = String.Format("{0}\n{1}", InsertIntoHeader, insertsSQL);
							SqlCommand commandIns = transact.Connection.CreateCommand();
							commandIns.CommandText = insertSQL;
							commandIns.Transaction = transact;
							Logger.Info("Запись данных");
							commandIns.ExecuteNonQuery();
							inserts.Clear();
						}
					}
				}

				transact.Commit();
				ok = true;
			}
			catch (Exception e) {
				Logger.Info("ошибка при записи Макета в базу " + e);
				ok = false;

			}
			finally {
				try {
					con.Close();
				}
				catch { }
			}
			return ok;
		}
	}

}
