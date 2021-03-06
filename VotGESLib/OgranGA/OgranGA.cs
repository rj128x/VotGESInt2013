﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using VotGES.Piramida;

namespace VotGES.OgranGA
{

	public class OgranGARecord
	{
		public enum ITEM_ENUM { rezNone, pAfterMax, pLessMin, rezSK, rezGen, rezHHT, rezHHG, rezRun, rezNPRCH, rezOPRCH, rezAVRCHM }
		public int GA { get; set; }
		public DateTime dateStart { get; set; }
		public DateTime dateEnd { get; set; }
		public int cntPusk { get; set; }
		public int cntStop { get; set; }
		public int cntAfterMax { get; set; }
		public int cntLessMin { get; set; }
		public int cntOgran { get; set; }
		public int cntZapr { get; set; }
		public int cntNPRCH { get; set; }
		public int cntOPRCH { get; set; }
		public int cntAVRCHM { get; set; }

		public double timeSK { get; set; }
		public double timeGen { get; set; }
		public double timeRun { get; set; }
		public double timeHHT { get; set; }
		public double timeHHG { get; set; }
		public double timeLessMin { get; set; }
		public double timeAfterMax { get; set; }
		public double timeOgran { get; set; }
		public double timeZapr { get; set; }
		public double timeStop { get; set; }
		public double timeNPRCH { get; set; }
		public double timeOPRCH { get; set; }
		public double timeAVRCHM { get; set; }

		public double posAVRCHM { get; set; }
		public double negAVRCHM { get; set; }

		public string TimeSKStr { get; protected set; }
		public string TimeRunStr { get; protected set; }
		public string TimeStopStr { get; protected set; }
		public string TimeGenStr { get; protected set; }
		public string TimeHHTStr { get; protected set; }
		public string TimeHHGStr { get; protected set; }
		public string TimeLessMinStr { get; protected set; }
		public string TimeAfterMaxStr { get; protected set; }
		public string TimeNPRCHStr { get; protected set; }
		public string TimeOPRCHStr { get; protected set; }
		public string TimeAVRCHMStr { get; protected set; }

		public string TimeOgranStr { get; protected set; }
		public string TimeZaprStr { get; protected set; }


		protected string getTimeSTR(double time) {
			int hours = (int)(time / 60.0);
			int min = (int)(time - hours * 60);
			int sec = (int)(time * 60 - hours * 60 * 60 - min * 60);
			if (hours > 10000)
				return ((int)(hours / 1000)).ToString() + "т.ч";
			if (hours > 100)
				return hours.ToString() + "ч";

			if (hours == 0 && min < 10) {
				return String.Format("{0}м{1:0}с", min, sec);
			}
			return String.Format("{0}ч{1:0}м", hours, min);
		}

		public static string getFullTimeStr(double time) {
			int hours = (int)(time / 60.0);
			int min = (int)(time - hours * 60);
			int sec = (int)(time * 60 - hours * 60 * 60 - min * 60);

			return String.Format("{0:0}ч{1:0}м{2:0}с", hours, min, sec);
		}

		public static string getDaysStr(double time) {
			int days = (int)(time / 60 / 24);
			int hours = (int)(time) / 60;
			int minutes = (int)(time - days * 60 * 24 - hours * 60);
			string res = String.Format(" {1:0}", days, hours);
			return res;
		}

		public void processStr() {
			timeZapr = 0;
			timeOgran = timeLessMin + timeAfterMax;
			cntZapr = 0;
			cntOgran = cntLessMin + cntAfterMax;
			TimeSKStr = getTimeSTR(timeSK);
			TimeRunStr = getTimeSTR(timeRun);
			TimeGenStr = getTimeSTR(timeGen);
			TimeHHTStr = getTimeSTR(timeHHT);
			TimeHHGStr = getTimeSTR(timeHHG);
			TimeLessMinStr = getTimeSTR(timeLessMin);
			TimeAfterMaxStr = getTimeSTR(timeAfterMax);
			TimeOgranStr = getTimeSTR(timeOgran);
			TimeZaprStr = getTimeSTR(timeZapr);
			TimeStopStr = getTimeSTR(timeStop);
			TimeNPRCHStr = getTimeSTR(timeNPRCH);
			TimeOPRCHStr = getTimeSTR(timeOPRCH);
			TimeAVRCHMStr = getTimeSTR(timeAVRCHM);
		}


		public static double dateDiff(DateTime dateStart, DateTime dateEnd) {
			return (dateEnd.Ticks - dateStart.Ticks) / (10000000.0 * 60.0);
		}

		public static ITEM_ENUM getItemType(int item) {
			if (item >= 101 && item <= 110)
				return ITEM_ENUM.pAfterMax;
			if (item >= 201 && item <= 210)
				return ITEM_ENUM.pLessMin;
			if (item >= 301 && item <= 310)
				return ITEM_ENUM.rezSK;
			if (item >= 401 && item <= 410)
				return ITEM_ENUM.rezGen;
			if (item >= 501 && item <= 510)
				return ITEM_ENUM.rezHHG;
			if (item >= 601 && item <= 610)
				return ITEM_ENUM.rezHHT;
			if (item >= 701 && item <= 710)
				return ITEM_ENUM.rezRun;
			if (item >= 801 && item <= 810)
				return ITEM_ENUM.rezNPRCH;
			if (item >= 901 && item <= 910)
				return ITEM_ENUM.rezOPRCH;
			if (item >= 1001 && item <= 1010)
				return ITEM_ENUM.rezAVRCHM;
			return ITEM_ENUM.rezNone;
		}

	}

	public class OgranGA
	{

		public static List<PiramidaEnrty> GetPrevData(DateTime dateStart, int gaNumber, bool onlyRun = false) {
			List<PiramidaEnrty> prevData = new List<PiramidaEnrty>();
			List<int> items = new List<int>();
			for (int h = 1; h <= 10; h++) {
				items.Add(h * 100 + gaNumber);
			}

			SqlConnection connection = PiramidaAccess.getConnection("PSV");
			SqlCommand command = connection.CreateCommand();
			command.Parameters.AddWithValue("@date", dateStart);
			string cmdFormat = "SELECT top 1 data_date,item,value0 from data WHERE objtype=2 and object=30 and parnumber=13 and data_date<@date and item={0} order by data_date desc ";
			List<string> cmdParts = new List<string>();
			try {
				connection.Open();

				foreach (int item in items) {
					command.CommandText = String.Format(cmdFormat, item);
					SqlDataReader reader = command.ExecuteReader();

					while (reader.Read()) {
						PiramidaEnrty rec = new PiramidaEnrty();
						rec.Date = reader.GetDateTime(0);
						rec.Item = reader.GetInt32(1);
						rec.Value0 = reader.GetDouble(2);
						prevData.Add(rec);
					}
					reader.Close();
				}

			} finally {
				try { command.Dispose(); } catch { }
				try { connection.Close(); } catch { }
			}

			return prevData;
		}

		public static Dictionary<int, string> GetTimeStopGA(DateTime date) {
			Dictionary<int, string> timeStopGA = new Dictionary<int, string>();
			List<int> items = new List<int>();
			for (int ga = 1; ga <= 10; ga++) {
				items.Add(200 + ga);
			}

			SqlConnection connection = PiramidaAccess.getConnection("PSV");
			SqlCommand command = connection.CreateCommand();
			command.Parameters.AddWithValue("@date", date);
			command.Parameters.AddWithValue("@dateStart", date.AddDays(-30));
			string cmdFormat = "SELECT top 1 data_date,item,value0 from data WHERE objtype=2 and object=3 and parnumber=12 and data_date<@date and data_date>=@dateStart and item={0} and value0>0 order by data_date desc ";
			List<string> cmdParts = new List<string>();
			try {
				connection.Open();

				foreach (int item in items) {
					command.CommandText = String.Format(cmdFormat, item);
					int ga = item % 100;
					string res = String.Format(" --- ");
					timeStopGA.Add(ga, res);
					SqlDataReader reader = command.ExecuteReader();
					
					while (reader.Read()) {
						PiramidaEnrty rec = new PiramidaEnrty();
						DateTime lastDate = reader.GetDateTime(0);
						int val = (int)reader.GetDouble(2);

						if (lastDate.AddHours(2) > date) {
							timeStopGA[ga] = "0";
						} else {
							double diff = OgranGARecord.dateDiff(lastDate, date);
							int days = (int)(diff / 60 / 24);
							int hours = (int)(diff % (60 * 24)) / 60;
							int minutes = (int)(diff - days * 60 * 24 - hours * 60);
							res = String.Format(" {0}:{1:00}:{2:00} ", days, hours, minutes);
							timeStopGA[ga] = res;
						}

					}
					reader.Close();
				}

			} finally {
				try { command.Dispose(); } catch { }
				try { connection.Close(); } catch { }
			}

			return timeStopGA;

		}

		protected static OgranGARecord calcOgran(DateTime dateStart, DateTime dateEnd, int gaNumber, List<PiramidaEnrty> data) {
			OgranGARecord result = new OgranGARecord();
			result.GA = gaNumber;
			result.dateStart = dateStart;
			result.dateEnd = dateEnd;

			double timeSKToEnd = 0;
			double timeGenToEnd = 0;
			double timeRunToEnd = 0;
			double timeHHTToEnd = 0;
			double timeHHGToEnd = 0;
			double timeAfterMaxToEnd = 0;
			double timeLessMinToEnd = 0;
			double timeNPRCHToEnd = 0;
			double timeOPRCHToEnd = 0;
			double timeAVRCHMToEnd = 0;

			bool processedPuskStop = false;
			bool processedSK = false;
			bool processedHHT = false;
			bool processedHHG = false;
			bool processedAfterMax = false;
			bool processedLessMin = false;
			bool processedGen = false;
			bool processedNPRCH = false;
			bool processedOPRCH = false;
			bool processedAVRCHM = false;

			foreach (PiramidaEnrty record in data) {
				double diffFromStart = OgranGARecord.dateDiff(dateStart, record.Date);
				double addTime = 0;
				int addCnt = 0;
				double timeToEnd = 0;

				if (record.Value0 == 0) {
					if (record.Value1 > diffFromStart)
						addTime += diffFromStart;
					else
						addTime += record.Value1;
				} else {
					addCnt++;
					timeToEnd = OgranGARecord.dateDiff(record.Date, dateEnd);
					addTime = 0;
				}

				OgranGARecord.ITEM_ENUM itemType = OgranGARecord.getItemType(record.Item);
				switch (itemType) {
					case (OgranGARecord.ITEM_ENUM.pAfterMax):
						result.timeAfterMax += addTime;
						result.cntAfterMax += addCnt;
						timeAfterMaxToEnd = timeToEnd;
						processedAfterMax = true;
						break;
					case (OgranGARecord.ITEM_ENUM.pLessMin):
						result.timeLessMin += addTime;
						result.cntLessMin += addCnt;
						timeLessMinToEnd = timeToEnd;
						processedLessMin = true;
						break;
					case (OgranGARecord.ITEM_ENUM.rezGen):
						result.timeGen += addTime;
						timeGenToEnd = timeToEnd;
						processedGen = true;
						break;
					case (OgranGARecord.ITEM_ENUM.rezHHG):
						result.timeHHG += addTime;
						timeHHGToEnd = timeToEnd;
						processedHHG = true;
						break;
					case (OgranGARecord.ITEM_ENUM.rezHHT):
						result.timeHHT += addTime;
						timeHHTToEnd = timeToEnd;
						processedHHT = true;
						break;
					case (OgranGARecord.ITEM_ENUM.rezRun):
						result.cntStop += (1 - addCnt);
						result.cntPusk += addCnt;
						timeRunToEnd = timeToEnd;
						result.timeRun += addTime;
						processedPuskStop = true;
						break;
					case (OgranGARecord.ITEM_ENUM.rezNPRCH):
						result.cntNPRCH += addCnt;
						result.timeNPRCH += addTime;
						timeNPRCHToEnd = timeToEnd;
						processedNPRCH = true;
						break;
					case (OgranGARecord.ITEM_ENUM.rezOPRCH):
						result.cntOPRCH += addCnt;
						result.timeOPRCH += addTime;
						timeOPRCHToEnd = timeToEnd;
						processedOPRCH = true;
						break;
					case (OgranGARecord.ITEM_ENUM.rezAVRCHM):
						result.cntAVRCHM += addCnt;
						result.timeAVRCHM += addTime;
						timeAVRCHMToEnd = timeToEnd;
						processedAVRCHM = true;
						break;
				}
			}


			List<PiramidaEnrty> prevData = GetPrevData(dateStart, gaNumber);


			//Logger.Info(cmdText);



			double timefull = OgranGARecord.dateDiff(dateStart, dateEnd);
			foreach (PiramidaEnrty rec in prevData) {
				if (rec.Item % 100 == gaNumber) {
					OgranGARecord.ITEM_ENUM itemType = OgranGARecord.getItemType(rec.Item);
					switch (itemType) {
						case OgranGARecord.ITEM_ENUM.pAfterMax:
							if (!processedAfterMax) {
								if (rec.Value0 > 0) {
									result.timeAfterMax = timefull;
								}
							}
							break;
						case OgranGARecord.ITEM_ENUM.pLessMin:
							if (!processedLessMin) {
								if (rec.Value0 > 0) {
									result.timeLessMin = timefull;
								}
							}
							break;
						case OgranGARecord.ITEM_ENUM.rezGen:
							if (!processedGen) {
								if (rec.Value0 > 0) {
									result.timeGen = timefull;
								}
							}
							break;
						case OgranGARecord.ITEM_ENUM.rezHHG:
							if (!processedHHG) {
								if (rec.Value0 > 0) {
									result.timeHHG = timefull;
								}
							}
							break;
						case OgranGARecord.ITEM_ENUM.rezHHT:
							if (!processedHHT) {
								if (rec.Value0 > 0) {
									result.timeHHT = timefull;
								}
							}
							break;
						case OgranGARecord.ITEM_ENUM.rezRun:
							if (!processedPuskStop) {
								if (rec.Value0 > 0) {
									result.timeRun = timefull;
								}
							}
							break;
						case OgranGARecord.ITEM_ENUM.rezSK:
							if (!processedSK) {
								if (rec.Value0 > 0) {
									result.timeSK = timefull;
								}
							}
							break;
						case OgranGARecord.ITEM_ENUM.rezNPRCH:
							if (!processedNPRCH) {
								if (rec.Value0 > 0) {
									result.timeNPRCH = timefull;
								}
							}
							break;
						case OgranGARecord.ITEM_ENUM.rezOPRCH:
							if (!processedOPRCH) {
								if (rec.Value0 > 0) {
									result.timeOPRCH = timefull;
								}
							}
							break;
						case OgranGARecord.ITEM_ENUM.rezAVRCHM:
							if (!processedAVRCHM) {
								if (rec.Value0 > 0) {
									result.timeAVRCHM = timefull;
								}
							}
							break;
					}
				}
			}



			result.timeAfterMax += timeAfterMaxToEnd;
			result.timeLessMin += timeLessMinToEnd;
			result.timeHHG += timeHHGToEnd;
			result.timeHHT += timeHHTToEnd;
			result.timeRun += timeRunToEnd;
			result.timeSK += timeSKToEnd;
			result.timeGen += timeGenToEnd;
			result.timeNPRCH += timeNPRCHToEnd;
			result.timeOPRCH += timeOPRCHToEnd;
			result.timeAVRCHM += timeAVRCHMToEnd;

			result.processStr();
			return result;
		}

		protected static void procesPuskStopData(DateTime dateStart, DateTime dateEnd, int minutes, int gaNumber) {
			List<int> items = new List<int>();
			for (int h = 1; h <= 10; h++) {
				items.Add(h * 100 + gaNumber);
			}

			Logger.Info("====Чтение исходных данных", Logger.LoggerSource.service);
			List<PiramidaEnrty> data = PiramidaAccess.GetDataFromDB(dateStart, dateEnd, 30, 2, 13, items, true, true, "PSV");
			List<OgranGARecord> result = new List<OgranGARecord>();

			DateTime date = dateStart.AddHours(0);
			List<PiramidaEnrty> smallData = new List<PiramidaEnrty>();



			while (date < dateEnd) {
				DateTime de = date.AddMinutes(minutes);
				foreach (PiramidaEnrty rec in data) {
					if (rec.Date > date && rec.Date <= de)
						smallData.Add(rec);
				}
				OgranGARecord resultRecord = calcOgran(date, de, gaNumber, smallData);

				smallData.Clear();
				result.Add(resultRecord);

				date = de.AddMinutes(0);
			}
			writeToDB(result);
		}

		public static void processData(DateTime dateStart, DateTime dateEnd, int minutes) {
			Logger.Info(String.Format("Обработка пусков-остановов {0}-{1} ", dateStart, dateEnd), Logger.LoggerSource.service);
			for (int ga = 1; ga <= 10; ga++) {
				Logger.Info("===GA" + ga, Logger.LoggerSource.service);
				procesPuskStopData(dateStart, dateEnd, minutes, ga);
			}
		}

		public static void processPiramidaPuskStop(DateTime dateStart, DateTime dateEnd) {
			string insertIntoHeader = "INSERT INTO Data (parnumber,object,item,value0,value1,valueMin,valueMax,valueEq,objtype,data_date,rcvstamp,season)";
			string frmt = "SELECT {0}, {1}, {2}, {3}, {4}, {5}, {6}, {7}, {8}, '{9}', '{10}', {11}";
			List<string> allData = new List<string>();
			int[] items = { 2, 6, 18, 22, 26, 30, 42, 46, 50, 54 };
			Dictionary<int, int> prevVal = new Dictionary<int, int>();
			Dictionary<int, DateTime> prevDate = new Dictionary<int, DateTime>();
			for (int ga = 1; ga <= 10; ga++) {
				prevVal[ga] = -1;
				prevDate[ga] = DateTime.MinValue;
			}
			List<PiramidaEnrty> data = PiramidaAccess.GetDataFromDB(dateStart, dateEnd, 8738, 0, 4, items.ToList(), true, true, "PMin");
			foreach (PiramidaEnrty rec in data) {
				int ga = 0;
				foreach (int item in items) {
					ga++;
					if (rec.Item == item)
						break;
				}
				/*if (ga != 3)
					continue;*/
				int val = rec.Value0 > 0 ? 1 : 0;
				if (prevVal[ga] != val && OgranGARecord.dateDiff(prevDate[ga], rec.Date) > 5) {
					String insertStr = String.Format(frmt, 13, 30, ga + 700, val, 0, 0, 0, 0, 2, rec.Date.ToString("yyyy-MM-dd HH:mm:ss"), rec.Date.ToString("yyyy-MM-dd HH:mm:ss"), DBSettings.getSeason(rec.Date));
					allData.Add(insertStr);
					insertStr = String.Format(frmt, 13, 30, ga + 400, val, 0, 0, 0, 0, 2, rec.Date.ToString("yyyy-MM-dd HH:mm:ss"), rec.Date.ToString("yyyy-MM-dd HH:mm:ss"), DBSettings.getSeason(rec.Date));
					allData.Add(insertStr);
					prevVal[ga] = val;
					prevDate[ga] = rec.Date;
				}

			}
			Logger.Info(insertIntoHeader + " \n " + String.Join("\n UNION ALL \n", allData));

		}



		protected static void writeToDB(List<OgranGARecord> data) {
			SqlCommand command = null;
			Logger.Info("====запись пусков-остановов", Logger.LoggerSource.service);
			SqlConnection connection = PiramidaAccess.getConnection("PSV");
			command = connection.CreateCommand();
			connection.Open();
			try {
				string query = "DELETE FROM PuskStopTable WHERE";
				string qOR = "(dateStart='{0}' and dateEnd='{1}' and gaNumber={2})";
				List<string> qList = new List<string>();

				int index = 0;
				foreach (OgranGARecord record in data) {
					string q = String.Format(qOR, record.dateStart.ToString(DBInfo.DateFormat), record.dateEnd.ToString(DBInfo.DateFormat), record.GA);
					qList.Add(q);
					index++;

					if (index == 20 || record == data.Last()) {
						command.CommandText = query + String.Join(" OR ", qList);
						int count = command.ExecuteNonQuery();
						Logger.Info(String.Format("=====Удаление пусков-остановов: {0}", count), Logger.LoggerSource.service);
						qList.Clear();
						index = 0;
					}
				}


				string insertStr = "INSERT INTO PuskStopTable (dateStart,dateEnd,gaNumber,cntPusk,cntStop,cntAfterMax,cntLessMin,timeSK,timeGen,timeAfterMax,timeLessMin,timeRun,timeHHT,timeHHG, timeNPRCH,timeOPRCH,timeAVRCHM,cntNPRCH,cntOPRCH,cntAVRCHM)";
				string dataFormat = "SELECT '{0}','{1}', {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}, {12}, {13}, {14},{15},{16},{17},{18},{19}";
				List<string> insertList = new List<string>();
				index = 0;
				foreach (OgranGARecord record in data) {
					string insert = string.Format(dataFormat, record.dateStart.ToString(DBInfo.DateFormat), record.dateEnd.ToString(DBInfo.DateFormat),
							record.GA, record.cntPusk, record.cntStop, record.cntAfterMax, record.cntLessMin,
							record.timeSK, record.timeGen, record.timeAfterMax, record.timeLessMin, record.timeRun, record.timeHHT, record.timeHHG,
							record.timeNPRCH, record.timeOPRCH, record.timeAVRCHM, record.cntNPRCH, record.cntOPRCH, record.cntAVRCHM);
					insertList.Add(insert);
					if (index == 20 || record == data.Last()) {
						command.CommandText = insertStr + " \n " + String.Join(" \nUNION ALL\n ", insertList);
						int count = command.ExecuteNonQuery();
						Logger.Info(String.Format("======запись пусков-остановов: {0}", count), Logger.LoggerSource.service);
						insertList.Clear();
						index = 0;
					}
				}
			} finally {
				try { command.Dispose(); } catch { }
				try { connection.Close(); } catch { }
			}
		}




	}
}
