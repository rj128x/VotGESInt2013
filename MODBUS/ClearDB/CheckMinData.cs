using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VotGES;
using VotGES.Piramida;
using VotGES.Piramida.Report;

namespace ClearDB
{
	public class CheckMinData
	{
		public DateTime DateStart;
		public DateTime DateEnd;
		public bool UseP2000;
		public Dictionary<DateTime, Dictionary<string, double>> newData;
		public Dictionary<DateTime, double> GTP1;
		public Dictionary<DateTime, double> GTP2;
		public Dictionary<DateTime, double> GES;
		List<string> log;

		public CheckMinData(DateTime dateStart, DateTime dateEnd,bool useP2000) {
			DateStart = dateStart;
			DateEnd = dateEnd;
			UseP2000 = useP2000;
			newData = new Dictionary<DateTime, Dictionary<string, double>>();
			GTP1 = new Dictionary<DateTime, double>();
			GTP2 = new Dictionary<DateTime, double>();
			GES = new Dictionary<DateTime, double>();
			log = new List<string>();
		}

		protected bool IsMissed(DateTime date, FullReport r2000, PiramidaRecord v2000) {
			bool isMissed = !r2000.Dates.Contains(date);
			if (r2000.EmptyData.ContainsKey(date)) {
				foreach (RecordTypeDB rdb in r2000.EmptyData[date]) {
					isMissed = rdb.ID == v2000.Key;
				}
			}
			return isMissed;
		}

		protected void addNewVal(DateTime date, string key, double val, string oldVal, string title,string source) {
			string info = String.Format("=={0}: {1} Old={2} {3:0.00} [{4}]", date.ToString("dd.MM.yyyy HH:mm"), title, oldVal, val,source);
			log.Add(info);
			Logger.Info(info);
			if (!newData.ContainsKey(date) && val >= 0) {
				newData.Add(date, new Dictionary<string, double>());
			}
			newData[date].Add(key, val);
		}

		protected double checkRecord(DateTime date, FullReport r2000, FullReport rOV, FullReport rOVMax, PiramidaRecord v2000, PiramidaRecord vOV) {
			bool isMissed2000 = IsMissed(date, r2000, v2000);
			bool isMissedOV = IsMissed(date, rOV, vOV);
			if (!isMissedOV) {
				double avgOV = rOV[date, vOV.Key] * 1000;
				double maxOV = rOVMax[date, vOV.Key] * 1000;
				if (avgOV == maxOV)
					isMissedOV = true;
			}
			//Logger.Info(string.Format("{0}: {1}   {2}", date, v2000.Key,String.Join(";",r2000.Dates)));
			double result = !isMissed2000 ? r2000[date, v2000.Key] : double.MinValue;
			if (isMissed2000 && !isMissedOV) {
				addNewVal(date, v2000.Key, rOV[date, vOV.Key] * 1000, "---", v2000.Title,"Ovation");
				result = rOV[date, vOV.Key] * 1000;
			} else if (!isMissed2000 && !isMissedOV) {
				double valOV = rOV[date, vOV.Key] * 1000;
				double val2000 = r2000[date, v2000.Key];
				if (valOV - val2000 > 10000) {
					addNewVal(date, v2000.Key, valOV, String.Format("{0:0.00}", val2000), v2000.Title,"Ovation");
					result = valOV;
				}
			} else if (isMissed2000 && isMissedOV) {
				try {
					double prevVal = double.MinValue;
					if (newData.ContainsKey(date.AddMinutes(-1)) && newData[date.AddMinutes(-1)].ContainsKey(v2000.Key)) {
						prevVal = newData[date.AddMinutes(-1)][v2000.Key];
					} else {
						prevVal = r2000[date.AddMinutes(-1), v2000.Key];
					}

					addNewVal(date, v2000.Key, prevVal, "---", v2000.Title,"P2000 Prev Val");
					result = prevVal;
				} catch {				}
			}
			return result;
		}

		protected double checkRecord(DateTime date, FullReport r2000, PiramidaRecord v2000, double val) {
			bool isMissed2000 = IsMissed(date, r2000, v2000);

			double result = r2000[date, v2000.Key];
			if (isMissed2000) {
				addNewVal(date, v2000.Key, val, "---", v2000.Title,"SUM GG");
				result = val;
			}
			if (!isMissed2000) {
				double val2000 = r2000[date, v2000.Key];
				if (val - val2000 > 10000) {
					addNewVal(date, v2000.Key, val, String.Format("{0:0.00}", val2000), v2000.Title, "SUM GG");
					result = val;
				}
			}
			return result;
		}

		public void checkData() {
			FullReport fullReportP2000 = new FullReport(DateStart, DateEnd, IntervalReportEnum.minute);
			fullReportP2000.RecordTypes[PiramidaRecords.P_GA1_Otd.Key].Visible = true;
			fullReportP2000.RecordTypes[PiramidaRecords.P_GA2_Otd.Key].Visible = true;
			fullReportP2000.RecordTypes[PiramidaRecords.P_GA3_Otd.Key].Visible = true;
			fullReportP2000.RecordTypes[PiramidaRecords.P_GA4_Otd.Key].Visible = true;
			fullReportP2000.RecordTypes[PiramidaRecords.P_GA5_Otd.Key].Visible = true;
			fullReportP2000.RecordTypes[PiramidaRecords.P_GA6_Otd.Key].Visible = true;
			fullReportP2000.RecordTypes[PiramidaRecords.P_GA7_Otd.Key].Visible = true;
			fullReportP2000.RecordTypes[PiramidaRecords.P_GA8_Otd.Key].Visible = true;
			fullReportP2000.RecordTypes[PiramidaRecords.P_GA9_Otd.Key].Visible = true;
			fullReportP2000.RecordTypes[PiramidaRecords.P_GA10_Otd.Key].Visible = true;

			fullReportP2000.RecordTypes[PiramidaRecords.P_GTP1.Key].Visible = true;
			fullReportP2000.RecordTypes[PiramidaRecords.P_GTP2.Key].Visible = true;
			fullReportP2000.RecordTypes[PiramidaRecords.P_GES.Key].Visible = true;

			FullReport fullReportOV = new FullReport(DateStart, DateEnd, IntervalReportEnum.minute);
			fullReportOV.RecordTypes[PiramidaRecords.MBW_GA1_P.Key].Visible = true;
			fullReportOV.RecordTypes[PiramidaRecords.MBW_GA2_P.Key].Visible = true;
			fullReportOV.RecordTypes[PiramidaRecords.MBW_GA3_P.Key].Visible = true;
			fullReportOV.RecordTypes[PiramidaRecords.MBW_GA4_P.Key].Visible = true;
			fullReportOV.RecordTypes[PiramidaRecords.MBW_GA5_P.Key].Visible = true;
			fullReportOV.RecordTypes[PiramidaRecords.MBW_GA6_P.Key].Visible = true;
			fullReportOV.RecordTypes[PiramidaRecords.MBW_GA7_P.Key].Visible = true;
			fullReportOV.RecordTypes[PiramidaRecords.MBW_GA8_P.Key].Visible = true;
			fullReportOV.RecordTypes[PiramidaRecords.MBW_GA9_P.Key].Visible = true;
			fullReportOV.RecordTypes[PiramidaRecords.MBW_GA10_P.Key].Visible = true;
			

			FullReport fullReportOVMax = new FullReport(DateStart, DateEnd, IntervalReportEnum.minute, FullReportMembersType.max);
			fullReportOVMax.RecordTypes[PiramidaRecords.MBW_GA1_P.Key].Visible = true;
			fullReportOVMax.RecordTypes[PiramidaRecords.MBW_GA2_P.Key].Visible = true;
			fullReportOVMax.RecordTypes[PiramidaRecords.MBW_GA3_P.Key].Visible = true;
			fullReportOVMax.RecordTypes[PiramidaRecords.MBW_GA4_P.Key].Visible = true;
			fullReportOVMax.RecordTypes[PiramidaRecords.MBW_GA5_P.Key].Visible = true;
			fullReportOVMax.RecordTypes[PiramidaRecords.MBW_GA6_P.Key].Visible = true;
			fullReportOVMax.RecordTypes[PiramidaRecords.MBW_GA7_P.Key].Visible = true;
			fullReportOVMax.RecordTypes[PiramidaRecords.MBW_GA8_P.Key].Visible = true;
			fullReportOVMax.RecordTypes[PiramidaRecords.MBW_GA9_P.Key].Visible = true;
			fullReportOVMax.RecordTypes[PiramidaRecords.MBW_GA10_P.Key].Visible = true;

			fullReportP2000.UsePiramida2000 = UseP2000;
			fullReportP2000.ReadData();
			fullReportOV.ReadData();
			fullReportOVMax.ReadData();

			DateTime date = DateStart.AddMinutes(1);
			if (fullReportP2000.Dates.First() > date)
				date = fullReportP2000.Dates.First().AddMinutes(0);
			DateTime dateEnd = DateEnd.AddMinutes(0);
			if (fullReportP2000.Dates.Last() < dateEnd)
				dateEnd = fullReportP2000.Dates.Last().AddMinutes(0);
			if (fullReportOV.Dates.Last() < dateEnd)
				dateEnd = fullReportOV.Dates.Last().AddMinutes(0);
			while (date <= dateEnd) {
				try {
					double gg1 = checkRecord(date, fullReportP2000, fullReportOV, fullReportOVMax, PiramidaRecords.P_GA1_Otd, PiramidaRecords.MBW_GA1_P);
					double gg2 = checkRecord(date, fullReportP2000, fullReportOV, fullReportOVMax, PiramidaRecords.P_GA2_Otd, PiramidaRecords.MBW_GA2_P);
					double gg3 = checkRecord(date, fullReportP2000, fullReportOV, fullReportOVMax, PiramidaRecords.P_GA3_Otd, PiramidaRecords.MBW_GA3_P);
					//double gg4=checkRecord(date, fullReportP2000, fullReportOV, fullReportOVMax, PiramidaRecords.P_GA4_Otd, PiramidaRecords.MBW_GA4_P);
					double gg5 = checkRecord(date, fullReportP2000, fullReportOV, fullReportOVMax, PiramidaRecords.P_GA5_Otd, PiramidaRecords.MBW_GA5_P);
					double gg6 = checkRecord(date, fullReportP2000, fullReportOV, fullReportOVMax, PiramidaRecords.P_GA6_Otd, PiramidaRecords.MBW_GA6_P);
					double gg7 = checkRecord(date, fullReportP2000, fullReportOV, fullReportOVMax, PiramidaRecords.P_GA7_Otd, PiramidaRecords.MBW_GA7_P);
					double gg8 = checkRecord(date, fullReportP2000, fullReportOV, fullReportOVMax, PiramidaRecords.P_GA8_Otd, PiramidaRecords.MBW_GA8_P);
					double gg9 = checkRecord(date, fullReportP2000, fullReportOV, fullReportOVMax, PiramidaRecords.P_GA9_Otd, PiramidaRecords.MBW_GA9_P);
					double gg10 = checkRecord(date, fullReportP2000, fullReportOV, fullReportOVMax, PiramidaRecords.P_GA10_Otd, PiramidaRecords.MBW_GA10_P);

					if (gg1 >= 0 && gg2 >= 0 && gg3 >= 0 && gg5 >= 0 && gg6 >= 0 && gg7 >= 0 && gg8 >= 0 && gg9 >= 0 && gg10 >= 0) {
						checkRecord(date, fullReportP2000, PiramidaRecords.P_GTP1, gg1 + gg2);
						checkRecord(date, fullReportP2000, PiramidaRecords.P_GTP2, gg3 + gg5 + gg6 + gg7 + gg8 + gg9 + gg10);
						checkRecord(date, fullReportP2000, PiramidaRecords.P_GES, gg1 + gg2 + gg3 + gg5 + gg6 + gg7 + gg8 + gg9 + gg10);
					}
				} catch (Exception e) {

				}
				date = date.AddMinutes(1);
			}

			SqlConnection con = PiramidaAccess.getConnection(UseP2000?"P2000":"PMin");
			con.Open();
			foreach (KeyValuePair<DateTime, Dictionary<string, double>> de in newData) {
				try {
					date = de.Key;
					Dictionary<string, double> vals = de.Value;
					List<string> Dels = new List<string>();
					List<string> Inss = new List<string>();
					foreach (string key in vals.Keys) {
						RecordTypeDB rdb = fullReportP2000.RecordTypes[key] as RecordTypeDB;
						string del = String.Format("(OBJECT={0} AND OBJTYPE={1} and ITEM={2})", rdb.DBRecord.Obj, rdb.DBRecord.ObjType, rdb.DBRecord.Item);
						Dels.Add(del);
						string ins = String.Format("SELECT {0}, {1}, {2}, {3}, {4}, '{5}', '{6}', {7}", 4, rdb.DBRecord.Obj, rdb.DBRecord.Item, vals[key].ToString().Replace(",", "."),
							rdb.DBRecord.ObjType, date.ToString(DBInfo.DateFormat), date.ToString(DBInfo.DateFormat), DBSettings.getSeason(date));
						Inss.Add(ins);
					}
					String DelStr = String.Format("DELETE FROM DATA where data_date='{0}' and parnumber=4 and ({1}) ", date.ToString(DBInfo.DateFormat), String.Join("OR", Dels));
					String InsStr = String.Format("INSERT INTO Data (parnumber,object,item,value0,objtype,data_date,rcvstamp,season) {0}", String.Join("\nUNION ALL\n", Inss));

					SqlTransaction trans = con.BeginTransaction();
					SqlCommand comDel = con.CreateCommand();
					comDel.CommandText = DelStr;
					comDel.Transaction = trans;
					comDel.ExecuteNonQuery();

					SqlCommand comIns = con.CreateCommand();
					comIns.CommandText = InsStr;
					comIns.Transaction = trans;
					comIns.ExecuteNonQuery();
					trans.Commit();
				} catch (Exception e) {
					Logger.Info("Ошибка при обновлении данных " + e.ToString());
				}
			}
			try {
				con.Close();
			} catch { }

			try {
				if (log.Count>0)
					MailClass.sendMail(String.Format("Корректировка минутных значений в БД {0}", UseP2000 ? "P2001" : "PMin"), String.Join("<br/>", log), Settings.single.ErrorMailTo);
			} catch { }
		}
	}
}
