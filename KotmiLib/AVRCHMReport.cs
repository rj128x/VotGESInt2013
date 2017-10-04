using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VotGES;

namespace KotmiLib
{
	public class AVRCHMResult
	{

		public Dictionary<int, double> TimeWork { get; set; }
		public Dictionary<int, double> TimeGen { get; set; }
		public Dictionary<int, double> TimeHHG { get; set; }
		public Dictionary<int, double> TimeHHT { get; set; }
		public Dictionary<int, double> TimeStop { get; set; }
		public Dictionary<int, double> TimeAVRCHM { get; set; }
		public Dictionary<int, double> TimeOPRCH { get; set; }
		public Dictionary<int, int> CntPusk { get; set; }
		public Dictionary<int, int> CntStop { get; set; }
		public Dictionary<int, int> CntAVRCHM { get; set; }
		public Dictionary<int, int> CntOPRCH { get; set; }

		public Dictionary<int, double> TimeLessMin { get; set; }
		public Dictionary<int, double> TimeAfterMax { get; set; }
		public Dictionary<int, int> CntLessMin { get; set; }
		public Dictionary<int, int> CntAfterMax { get; set; }
		public Dictionary<int, double> PosAVRCHM { get; set; }
		public Dictionary<int, double> NegAVRCHM { get; set; }


		public AVRCHMResult() {

			TimeWork = new Dictionary<int, double>();
			TimeAVRCHM = new Dictionary<int, double>();
			TimeOPRCH = new Dictionary<int, double>();
			TimeStop = new Dictionary<int, double>();
			TimeGen = new Dictionary<int, double>();
			TimeHHT = new Dictionary<int, double>();
			TimeHHG = new Dictionary<int, double>();
			CntPusk = new Dictionary<int, int>();
			CntStop = new Dictionary<int, int>();
			CntAVRCHM = new Dictionary<int, int>();
			CntOPRCH = new Dictionary<int, int>();
			TimeLessMin = new Dictionary<int, double>();
			TimeAfterMax = new Dictionary<int, double>();
			CntAfterMax = new Dictionary<int, int>();
			CntLessMin = new Dictionary<int, int>();
			PosAVRCHM = new Dictionary<int, double>();
			NegAVRCHM = new Dictionary<int, double>();


			for (int GA = 1; GA <= 10; GA++) {
				TimeWork.Add(GA, 0);
				TimeAVRCHM.Add(GA, 0);
				TimeStop.Add(GA, 0);
				CntPusk.Add(GA, 0);
				CntStop.Add(GA, 0);
				CntAVRCHM.Add(GA, 0);
				TimeAfterMax.Add(GA, 0);
				TimeLessMin.Add(GA, 0);
				CntAfterMax.Add(GA, 0);
				CntLessMin.Add(GA, 0);
				PosAVRCHM.Add(GA, 0);
				NegAVRCHM.Add(GA, 0);
				CntOPRCH.Add(GA, 0);
				TimeOPRCH.Add(GA, 0);
				TimeGen.Add(GA, 0);
				TimeHHG.Add(GA, 0);
				TimeHHT.Add(GA, 0);
			}
		}
	}


	public class AVRCHMReport
	{
		public static string DateFormat = "yyyy-MM-dd HH:mm:ss";

		public DateTime DateStart { get; set; }
		public DateTime DateEnd { get; set; }
		public Dictionary<DateTime, Dictionary<string, double>> InitData { get; set; }
		public static Dictionary<String, ArcField> DescArr { get; protected set; }
		public AVRCHMResult Result { get; set; }
		public int StepSec { get; set; }

		static AVRCHMReport() {
			DescArr = new Dictionary<string, ArcField>();

			for (int group = 0; group <= 8; group++) {
				DescArr.Add(String.Format("P_ZVN_GR{0}", group), new ArcField(String.Format("TI_{0}", 30300 + group)));
			}
			/*DescArr.Add("P_Plan_GTP1", new ArcField("TI_4111"));
			DescArr.Add("P_Plan_GTP2", new ArcField("TI_4115"));

			DescArr.Add("P_Zad_GTP1", new ArcField("TI_4101"));
			DescArr.Add("P_Zad_GTP2", new ArcField("TI_4105"));*/

			for (int ga = 1; ga <= 10; ga++) {
				DescArr.Add("P_GA_" + ga, new ArcField(String.Format("TI_{0}", 30015 + (ga - 1) * 30)));
				//DescArr.Add("F_GA_" + ga, new ArcField(String.Format("TI_{0}", 24+30*(ga - 1))));
				DescArr.Add("PF_GA_" + ga, new ArcField(String.Format("TI_{0}", 30024 + 30 * (ga - 1))));
				//DescArr.Add("MAXP_GA_" + ga, new ArcField(String.Format("TI_{0}", 30025 + 30 * (ga - 1))));
				//DescArr.Add("V_GA_" + ga, new ArcField(String.Format("TS_{0}", 10001 + (ga - 1) * 4)));
				//DescArr.Add("GA_NPRCH_" + ga, new ArcField(String.Format("TS_{0}", 30009 + 11 * (ga - 1))));
				DescArr.Add("GA_GR_" + ga, new ArcField(String.Format("TS_{0}", 30008 + 11 * (ga - 1))));
				DescArr.Add("GA_HHG_" + ga, new ArcField(String.Format("TS_{0}", 30007 + 11 * (ga - 1))));
				DescArr.Add("GA_HHT_" + ga, new ArcField(String.Format("TS_{0}", 30006 + 11 * (ga - 1))));
				DescArr.Add("GA_STOP_" + ga, new ArcField(String.Format("TS_{0}", 30005 + 11 * (ga - 1))));
				DescArr.Add("GA_GROUP_" + ga, new ArcField(String.Format("TI_{0}", 30023 + 30 * (ga - 1))));
			}

		}

		public AVRCHMReport(DateTime dateStart, DateTime dateEnd, int stepSec) {
			DateStart = dateStart;
			DateEnd = dateEnd;
			DateTime date = DateStart.AddSeconds(0);
			Result = new AVRCHMResult();

			StepSec = stepSec;

		}

		public void ReadData(int hhDiff) {
			//KotmiClass.Single.OnFinishRead = FinishRead;

			KotmiClass Kotmi = new KotmiClass();
			InitData = new Dictionary<DateTime, Dictionary<string, double>>();
			DateTime dt = DateStart.AddSeconds(-StepSec);
			while (dt <= DateEnd) {
				InitData.Add(dt, new Dictionary<string, double>());
				foreach (string key in DescArr.Keys) {
					InitData[dt].Add(key, 0);
				}
				dt = dt.AddSeconds(StepSec);
			}

			foreach (KeyValuePair<string, ArcField> de in DescArr) {
				Kotmi.ReadVals(DateStart.AddHours(hhDiff).AddSeconds(-StepSec), DateEnd.AddHours(hhDiff), de.Value, StepSec);
				SortedList<DateTime, double> data = Kotmi.FullData;
				foreach (DateTime dtt in data.Keys) {
					InitData[dtt.AddHours(-hhDiff)][de.Key] = data[dtt];
				}
			}
			Kotmi.Close();

			Random rand = new Random();
			foreach (DateTime dtt in InitData.Keys) {
				DateTime dtPrev = dtt.AddSeconds(-StepSec);
				Dictionary<string, double> data = InitData[dtt];

				if (dtt >= DateStart) {
					Dictionary<string, double> prevData = InitData[dtPrev];

					for (int group = 1; group <= 8; group++) {
						double ZVN = data[String.Format("P_ZVN_GR{0}", group)];
						double prevZVN = data[String.Format("P_ZVN_GR{0}", group)];
						if (Math.Abs(ZVN) > 0.1) {
							int cnt = 0;
							List<int> ggs = new List<int>();
							for (int ga = 1; ga <= 10; ga++) {
								if (Math.Abs(data["GA_GROUP_" + ga] - group) < 0.1) {
									cnt++;
									ggs.Add(ga);
								}
							}
							foreach (int ga in ggs) {
								if (data["GA_GR_" + ga] > 0) {
									Result.TimeAVRCHM[ga] += StepSec;
									if (Math.Abs(prevZVN) < 0.1)
										Result.CntAVRCHM[ga]++;
									if (cnt > 0) {
										if (ZVN > 0)
											Result.PosAVRCHM[ga] += (ZVN / cnt) * StepSec;
										else
											Result.NegAVRCHM[ga] += (ZVN / cnt) * StepSec;
									}
								}
							}
						}
					}


					for (int ga = 1; ga <= 10; ga++) {
						double pf = data["PF_GA_" + ga];
						double prevPF = prevData["PF_GA_" + ga];
						double p = data["P_GA_" + ga];
						double prevP = prevData["P_GA_" + ga];
						double maxP = ga <= 2 || ga == 4 ? 115 : 105;
						bool V = (Math.Abs(data["GA_STOP_" + ga]) < 0.01);
						bool prevV = (Math.Abs(prevData["GA_STOP_" + ga]) < 0.01);

						bool GR = (Math.Abs(data["GA_GR_" + ga] - 1) <= 0.01);
						bool prevGR = (Math.Abs(prevData["GA_GR_" + ga] - 1) <= 0.01);

						/*bool NPRCH = (Math.Abs(data["GA_NPRCH_" + ga] - 1) <= 0.01);
						bool prevNPRCH = (Math.Abs(prevData["GA_NPRCH_" + ga] - 1) <= 0.01);*/

						bool HHG = (Math.Abs(data["GA_HHG_" + ga] - 1) <= 0.01);
						bool prevHHG= (Math.Abs(prevData["GA_HHG_" + ga] - 1) <= 0.01);
						bool HHT = (Math.Abs(data["GA_HHT_" + ga] - 1) <= 0.01);
						bool prevHHT = (Math.Abs(prevData["GA_HHT_" + ga] - 1) <= 0.01);

						bool Run = V && (HHG || HHT || GR);
						bool prevRun= prevV && (prevHHG || prevHHT || prevGR);

						if (Run) {
							if (V && GR) {
								Result.TimeGen[ga] += StepSec;
								if (p < 33 && p > 10) {
									Result.TimeLessMin[ga] += StepSec;
									if (prevP == 0 || prevP >= 33)
										Result.CntLessMin[ga]++;
								}
								if (p > maxP) {
									Result.TimeAfterMax[ga] += StepSec;
									if (prevP <= maxP)
										Result.CntAfterMax[ga]++;
								}
							}

							if (V && GR && (Math.Abs(pf) > 0.1) && (Math.Abs(p) > 10) ) {
								Result.TimeOPRCH[ga] += StepSec;
								if (Math.Abs(prevPF) < 0.1) {
									Result.CntOPRCH[ga]++;
								}
							}
							if (HHG) {
								Result.TimeHHG[ga] += StepSec;
							}
							if (HHT) {
								Result.TimeHHT[ga] += StepSec;
							}

							Result.TimeWork[ga] += StepSec;
							if (!prevRun) {
								Result.CntPusk[ga]++;
							}
						} else {
							Result.TimeStop[ga] += StepSec;
							if (prevRun)
								Result.CntStop[ga]++;
						}
					}


				}
			}
		}

		public void WriteToDB() {
			SqlConnection con = new SqlConnection(String.Format("Data Source={0};Initial Catalog={1};Persist Security Info=True;User ID={2};Password={3};Trusted_Connection=False;",
				KOTMISettings.Single.DBServer, KOTMISettings.Single.DBName, KOTMISettings.Single.DBUser, KOTMISettings.Single.DBPassword));
			try {
				con.Open();
				SqlTransaction trans = con.BeginTransaction();
				SqlCommand command = new SqlCommand(String.Format("DELETE FROM GAKOTMI WHERE dateStart='{0}' and dateEnd='{1}' ",
						DateStart.ToString(DateFormat), DateEnd.ToString(DateFormat)));
				command.Connection = con;
				command.Transaction = trans;
				command.ExecuteNonQuery();

				string insertStr = "INSERT INTO GAKOTMI (dateStart,dateEnd,gaNumber,cntPusk,cntStop,cntAfterMax,cntLessMin,timeGen,timeAfterMax,timeLessMin, timeAVRCHM,cntAVRCHM,posAVRCHM,negAVRCHM,cntOPRCH,timeOPRCH,timeRun,timeHHG,timeHHT)";
				string dataFormat = "SELECT '{0}','{1}', {2}, {3}, {4}, {5}, {6}, {7:0.00}, {8:0.00}, {9:0.00}, {10:0.00}, {11}, {12:0.00}, {13:0.00},{14}, {15:0.00}, {16:0.00}, {17:0.00}, {18:0.00}";
				List<string> dataStrList = new List<string>();
				double seconds = (DateEnd - DateStart).TotalSeconds;

				for (int ga = 1; ga <= 10; ga++) {
					string dataStr = String.Format(dataFormat, DateStart.ToString(DateFormat), DateEnd.ToString(DateFormat),
						ga, Result.CntPusk[ga], Result.CntStop[ga], Result.CntAfterMax[ga], Result.CntLessMin[ga],
						(Result.TimeWork[ga] / 60).ToString().Replace(",", "."),
						(Result.TimeAfterMax[ga] / 60).ToString().Replace(",", "."),
						(Result.TimeLessMin[ga] / 60).ToString().Replace(",", "."),
						(Result.TimeAVRCHM[ga] / 60).ToString().Replace(",", "."),
						Result.CntAVRCHM[ga],
						(Result.PosAVRCHM[ga] / seconds).ToString().Replace(",", "."),
						(Result.NegAVRCHM[ga] / seconds).ToString().Replace(",", "."),
						Result.CntOPRCH[ga],
						(Result.TimeOPRCH[ga] / 60).ToString().Replace(",", "."),
						(Result.TimeWork[ga] / 60).ToString().Replace(",", "."),
						(Result.TimeHHG[ga] / 60).ToString().Replace(",", "."),
						(Result.TimeHHT[ga] / 60).ToString().Replace(",", "."));

					dataStrList.Add(dataStr);
				}
				string sql = insertStr + " \n " + String.Join(" \nUNION ALL\n ", dataStrList);
				//Logger.info(sql);
				command = new SqlCommand(sql);
				command.Connection = con;
				command.Transaction = trans;
				command.ExecuteNonQuery();
				trans.Commit();

			} catch (Exception e) {
				Logger.Info("Ошибка при записи данных в БД " + e.ToString());
			}
		}

	}
}
