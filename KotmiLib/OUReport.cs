using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VotGES.Piramida;
using VotGES;

namespace KotmiLib
{

	public class OUReport
	{
		public static string DateFormat = "yyyy-MM-dd HH:mm:ss";

		public DateTime DateStart { get; set; }
		public DateTime DateEnd { get; set; }
		public int StepSec { get; set; }
		public static Dictionary<String, ArcField> DescArr { get; protected set; }
		public Dictionary<DateTime, Dictionary<string, double>> Data { get; set; }
		public Dictionary<DateTime, Dictionary<string, double>> MinData { get; set; }

		public OUReport(DateTime dateStart, DateTime dateEnd, int stepSec) {
			DateStart = dateStart;
			DateEnd = dateEnd;
			StepSec = stepSec;
			DescArr = new Dictionary<string, ArcField>();
			DescArr.Add("QGES", new ArcField("PTI_210", "PSV~3~1;P3000~1~354"));
			DescArr.Add("VBGES", new ArcField("TI_4131", "PSV~3~2;P3000~1~274"));
			DescArr.Add("NBGES", new ArcField("TI_4132", "PSV~3~3;P3000~1~275"));
			DescArr.Add("TGES", new ArcField("TI_4133", "PSV~3~5;P3000~1~373"));
			DescArr.Add("TShit", new ArcField("TI_4134", "PSV~3~6"));
			DescArr.Add("QTurb", new ArcField("PTI_211", "PSV~3~7;P3000~1~355"));
			DescArr.Add("QVP", new ArcField("PTI_231", "PSV~3~8;P3000~1~356"));
			DescArr.Add("U1SH110", new ArcField("TI_2043", "PSV~3~12"));
			DescArr.Add("U2SH110", new ArcField("TI_2044", "PSV~3~13"));
			DescArr.Add("U1SH220", new ArcField("TI_2041", "PSV~3~14"));
			DescArr.Add("U2SH220", new ArcField("TI_2042", "PSV~3~15"));
			DescArr.Add("U500Karm", new ArcField("TI_337", "PSV~3~16"));
			DescArr.Add("U500Eml", new ArcField("TI_307", "PSV~3~17"));
			DescArr.Add("U500Vyat", new ArcField("TI_367", "PSV~3~18"));

			for (int ga = 1; ga <= 10; ga++) {
				DescArr.Add(String.Format("RashodGA_{0}", ga), new ArcField(String.Format("TI_{0}", 30028+(ga - 1)*30),
					String.Format("PSV~3~{0};P3000~1~{1}", 100 + ga, 104 + (ga - 1) * 25)));
				DescArr.Add(String.Format("PGA_{0}", ga), new ArcField(String.Format("TI_{0}", 30015+(ga - 1)*30),
					String.Format("PSV~3~{0}", 200 + ga)));
				DescArr.Add(String.Format("NAGA_{0}", ga), new ArcField(string.Format("TI_{0}", 30021+(ga-1)*30),
					string.Format("PSV~3~{0}", 400 + ga)));
				DescArr.Add(String.Format("RKGA_{0}", ga), new ArcField(string.Format("TI_{0}", 30022+(ga-1)*30),
					String.Format("PSV~3~{0}", 500 + ga)));
				DescArr.Add(String.Format("NaporGA_{0}", ga), new ArcField(String.Format("TI_{0}", 30027+(ga-1)*30),
					String.Format("PSV~3~{0}", 600 + ga)));
				DescArr.Add(String.Format("PSV~3~{0}",  ga), new ArcField(string.Format("TI_{0}", 30024+(ga-1)*30), 
					String.Format("PSV~3~{0}", 300 + ga)));
			}

			DescArr.Add("NaporGES", new ArcField("PTI_0", "PSV~3~4;P3000~1~276"));
			DescArr.Add("U110", new ArcField("PTI_0", "PSV~3~9"));
			DescArr.Add("U220", new ArcField("PTI_0", "PSV~3~10"));
			DescArr.Add("U500", new ArcField("PTI_0", "PSV~3~11"));
			DescArr.Add("PGTP1", new ArcField("PTI_0", "PSV~30~2"));
			DescArr.Add("PGTP2", new ArcField("PTI_0", "PSV~30~3"));
			DescArr.Add("PGES", new ArcField("PTI_0", "PSV~30~1"));
			DescArr.Add("PRGE2", new ArcField("PTI_0", "PSV~30~32"));
			DescArr.Add("PRGE3", new ArcField("PTI_0", "PSV~30~33"));
			DescArr.Add("PRGE4", new ArcField("PTI_0", "PSV~30~34"));
			DescArr.Add("QGTP1", new ArcField("PTI_0", "PSV~30~5"));
			DescArr.Add("QGTP2", new ArcField("PTI_0", "PSV~30~6"));
			DescArr.Add("QGESCalc", new ArcField("PTI_0", "PSV~30~4"));
		}

		public void processData(bool min=true) {
			//Logger.Info(String.Format("Чтение данных котми с {0} по {1}", DateStart, DateEnd));
			Data = new Dictionary<DateTime, Dictionary<string, double>>();
			MinData = new Dictionary<DateTime, Dictionary<string, double>>();
			foreach (KeyValuePair<string, ArcField> de in DescArr) {
				if (!de.Value.Code.Equals("PTI_0")) {
					Logger.Info(String.Format("==Чтение из котми по параметру {0}", de.Value.Code));
					List<ArcField> fields = new List<ArcField>();
					fields.Add(de.Value);
					KotmiResult result = new KotmiResult(DateStart.AddHours(2), DateEnd.AddHours(2), fields, min?10:60, "HH", false, true);
					result.ReadData();
					SortedList<DateTime, double> data = result.Values[de.Value];
					foreach (KeyValuePair<DateTime, double> kde in data) {
						DateTime dt = kde.Key.AddHours(-2);
						if (!Data.ContainsKey(dt))
							Data.Add(dt, new Dictionary<string, double>());
						Data[dt].Add(de.Key, kde.Value);
					}

					SortedList<DateTime, double> minData = result.MinValues[de.Value];
					foreach (KeyValuePair<DateTime, double> kde in minData) {
						DateTime dt = kde.Key.AddHours(-2);
						if (!MinData.ContainsKey(dt))
							MinData.Add(dt, new Dictionary<string, double>());
						MinData[dt].Add(de.Key, kde.Value);
					}
				}
			}

			Logger.Info("Подсчет рассчетных значений");
			foreach (DateTime dt in Data.Keys) {
				processArrayVals(Data[dt]);
			}
			foreach (DateTime dt in MinData.Keys) {
				processArrayVals(MinData[dt]);
			}


			string InsertFormat = "INSERT INTO Data (parnumber,object,item,value0,objtype,data_date,rcvstamp,season)";
			Dictionary<string, List<string>> SQLIns = new Dictionary<string, List<string>>();
			Dictionary<string, List<String>> SqlDel = new Dictionary<string, List<string>>();

			Logger.Info("Формирование запросов к БД");

			foreach (KeyValuePair<string, ArcField> de in DescArr) {
				//Logger.Info(de.Key);
				try {
					string[] pArr = de.Value.PiramidaCode.Split(';');
					foreach (string dbKey in pArr) {
						string[] dbArr = dbKey.Split('~');
						string db = dbArr[0];
						bool needMin = db == "PSV" && min;

						string obj = dbArr[1];
						string item = dbArr[2];
						if (!SqlDel.ContainsKey(db)) {
							SqlDel.Add(db, new List<string>());
							SQLIns.Add(db, new List<string>());

							if (needMin) {
								SqlDel.Add("PMin", new List<string>());
								SQLIns.Add("PMin", new List<string>());
							}
						}

						string delFormat = "DELETE from data where objtype=2 and object={0} and item={1} and parnumber={2} and data_date>='{3}' and data_date<='{4}'";

						string delHH = String.Format(delFormat,
							obj, item, 12, Data.First().Key.ToString(DateFormat), Data.Last().Key.ToString(DateFormat));
						string delMin = String.Format(delFormat,
							obj, item, 4, MinData.First().Key.ToString(DateFormat), MinData.Last().Key.ToString(DateFormat));
						SqlDel[db].Add(delHH);
						if (needMin) {
							SqlDel["PMin"].Add(delMin);
						}

						string format = "SELECT {0}, {1}, {2}, {3}, {4}, '{5}', '{6}', {7}";

						foreach (DateTime date in Data.Keys) {
							string insert = string.Format(format, 12, obj, item, Data[date][de.Key].ToString().Replace(",", "."), 2, date.ToString(DateFormat), date.ToString(DateFormat), 0);
							//Logger.Info(insert);
							SQLIns[db].Add(insert);
						}

						if (needMin) {
							foreach (DateTime date in MinData.Keys) {
								string insert = string.Format(format, 4, obj, item, MinData[date][de.Key].ToString().Replace(",", "."), 2, date.ToString(DateFormat), date.ToString(DateFormat), 0);
								//Logger.Info(insert);
								SQLIns["PMin"].Add(insert);
							}
						}
					}
				}catch (Exception e) {
					Logger.Info(e.ToString());
				}
			}

			Logger.Info("Запись данных в БД");
			foreach (string db in SQLIns.Keys) {
				try {
					Logger.Info("==" + db);
					SqlConnection con = PiramidaAccess.getConnection(db);
					con.Open();
					SqlTransaction trans = con.BeginTransaction();
					foreach (string del in SqlDel[db]) {
						SqlCommand command = con.CreateCommand();
						command.CommandText = del;
						command.Transaction = trans;
						command.ExecuteNonQuery();
					}
					List<string> inserts = new List<string>();
					foreach (string ins in SQLIns[db]) {
						inserts.Add(ins);
						if (inserts.Count == 10 || ins == SQLIns[db].Last()) {
							string insert = string.Format("{0} {1}", InsertFormat, string.Join("\n UNION ALL \n", inserts));
							SqlCommand command = con.CreateCommand();
							command.CommandText = insert;
							command.Transaction = trans;
							command.ExecuteNonQuery();

							inserts.Clear();
						}
					}

					trans.Commit();
					con.Close();
				} catch (Exception e) {
					Logger.Info(e.ToString());
				}
			}


		}

		protected void processArrayVals(Dictionary<string, double> arr) {
			arr.Add("NaporGES", arr["VBGES"] - arr["NBGES"]);
			arr.Add("U110", getAvg(arr["U1SH110"], arr["U2SH110"], 0, 100));
			arr.Add("U220", getAvg(arr["U1SH220"], arr["U2SH220"], 0, 200));
			arr.Add("U500", getAvg(arr["U500Karm"], arr["U500Eml"], arr["U500Vyat"], 500));
			arr.Add("PGTP1", arr["PGA_1"] + arr["PGA_2"]);
			arr.Add("PGTP2", arr["PGA_3"] + arr["PGA_4"] + arr["PGA_5"] + arr["PGA_6"] + arr["PGA_7"] + arr["PGA_8"] + arr["PGA_9"] + arr["PGA_10"]);
			arr.Add("PRGE2", arr["PGA_3"] + arr["PGA_4"]);
			arr.Add("PRGE3", arr["PGA_4"] + arr["PGA_5"]);
			arr.Add("PRGE4", arr["PGA_7"] + arr["PGA_8"] + arr["PGA_9"] + arr["PGA_10"]);
			arr.Add("PGES", arr["PGA_1"] + arr["PGA_2"] + arr["PGA_3"] + arr["PGA_4"] + arr["PGA_5"] + arr["PGA_6"] + arr["PGA_7"] + arr["PGA_8"] + arr["PGA_9"] + arr["PGA_10"]);
			arr.Add("QGTP1", arr["RashodGA_1"] + arr["RashodGA_2"]);
			arr.Add("QGTP2", arr["RashodGA_3"] + arr["RashodGA_4"] + arr["RashodGA_5"] + arr["RashodGA_6"] + arr["RashodGA_7"] + arr["RashodGA_8"] + arr["RashodGA_9"] + arr["RashodGA_10"]);
			arr.Add("QGESCalc", arr["RashodGA_1"] + arr["RashodGA_2"] + arr["RashodGA_3"] + arr["RashodGA_4"] + arr["RashodGA_5"] + arr["RashodGA_6"] + arr["RashodGA_7"] + arr["RashodGA_8"] + arr["RashodGA_9"] + arr["RashodGA_10"]);
		}

		protected double getAvg(double v1, double v2, double v3, double min) {
			v1 = v1 < min ? Math.Max(v2, v3) : v1;
			v2 = v2 < min ? Math.Max(v1, v3) : v2;
			v3 = v3 < min ? Math.Max(v2, v1) : v3;
			return (v1 + v2 + v3) / 3;
		}
	}




}
