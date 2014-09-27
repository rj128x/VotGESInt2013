using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using VotGES.Piramida;

namespace VotGES.OgranGA {
	public class OgranGAReport {
		public Dictionary<int, List<OgranGARecord>> data { get; protected set; }
		public Dictionary<int, OgranGARecord> sumData { get; protected set; }

		public OgranGARecord sumRecord { get; protected set; }

		public DateTime DateStart { get; protected set; }
		public DateTime DateEnd { get; protected set; }

		public Dictionary<int, List<OgranGARecord>> Data {
			get { return data; }
		}

		public Dictionary<int, OgranGARecord> SumData {
			get { return sumData; }
		}


		public OgranGAReport(DateTime dateStart, DateTime dateEnd) {
			DateStart = dateStart;
			DateEnd = dateEnd;
			data = new Dictionary<int, List<OgranGARecord>>();
			sumData = new Dictionary<int, OgranGARecord>();
			for (int ga = 1; ga <= 10; ga++) {
				data.Add(ga, new List<OgranGARecord>());
				sumData.Add(ga, new OgranGARecord());
			}
		}

		public void readData() {
			SqlDataReader reader = null;
			SqlCommand command = null;
			SqlConnection connection = PiramidaAccess.getConnection("PSV");

			command = connection.CreateCommand();
			command.Parameters.AddWithValue("@dateStart", DateStart);
			command.Parameters.AddWithValue("@dateEnd", DateEnd);
			connection.Open();
			command.CommandText = "Select gaNumber,dateStart,dateEnd,cntPusk,cntStop,cntAfterMax,cntLessMin,timeSK,timeGen,timeAfterMax,timeLessMin,timeRun,timeHHT,timeHHG from PuskStopTable Where dateStart>=@dateStart and dateEnd<=@dateStart";
			try {
				reader = command.ExecuteReader();
				while (reader.Read()) {
					OgranGARecord rec = new OgranGARecord();
					rec.GA = reader.GetInt32(0);
					rec.dateStart = reader.GetDateTime(1);
					rec.dateEnd = reader.GetDateTime(2);
					rec.cntPusk = reader.GetInt32(4);
					rec.cntStop = reader.GetInt32(5);
					rec.cntAfterMax = reader.GetInt32(6);
					rec.cntLessMin = reader.GetInt32(7);
					rec.timeSK = reader.GetInt32(8);
					rec.timeGen = reader.GetInt32(9);
					rec.timeAfterMax = reader.GetInt32(10);
					rec.timeLessMin = reader.GetInt32(11);
					rec.timeRun = reader.GetInt32(12);
					rec.timeHHT = reader.GetInt32(13);
					rec.timeHHG = reader.GetInt32(14);

					data[rec.GA].Add(rec);
				}
			} finally {
				try { reader.Close(); } catch { }
				try { command.Dispose(); } catch { }
				try { connection.Close(); } catch { }
			}
		}

		public void readSumData() {
			SqlDataReader reader = null;
			SqlCommand command = null;
			SqlConnection connection = PiramidaAccess.getConnection("PSV");
			command = connection.CreateCommand();
			command.Parameters.AddWithValue("@dateStart", DateStart);
			command.Parameters.AddWithValue("@dateEnd", DateEnd);
			connection.Open();
			command.CommandText = "Select gaNumber,sum(cntPusk),sum(cntStop),sum(cntAfterMax),sum(cntLessMin),sum(timeSK),sum(timeGen),sum(timeAfterMax),sum(timeLessMin),sum(timeRun),sum(timeHHT),sum(timeHHG) from PuskStopTable Where dateStart>=@dateStart and dateEnd<=@dateStart group by gaNumber";
			sumRecord = new OgranGARecord();
			try {
				reader = command.ExecuteReader();
				while (reader.Read()) {
					OgranGARecord rec = new OgranGARecord();
					rec.GA = reader.GetInt32(0);
					rec.dateStart = DateStart;
					rec.dateEnd = DateEnd;
					rec.cntPusk = reader.GetInt32(1);
					rec.cntStop = reader.GetInt32(2);
					rec.cntAfterMax = reader.GetInt32(3);
					rec.cntLessMin = reader.GetInt32(4);
					rec.timeSK = reader.GetInt32(5);
					rec.timeGen = reader.GetInt32(6);
					rec.timeAfterMax = reader.GetInt32(7);
					rec.timeLessMin = reader.GetInt32(8);
					rec.timeRun = reader.GetInt32(9);
					rec.timeHHT = reader.GetInt32(10);
					rec.timeHHG = reader.GetInt32(11);

					sumRecord.cntAfterMax += rec.cntAfterMax;
					sumRecord.cntLessMin += rec.cntLessMin;
					sumRecord.cntPusk += rec.cntLessMin;
					sumRecord.cntStop += rec.cntStop;
					sumRecord.timeAfterMax += rec.timeAfterMax;
					sumRecord.timeGen += rec.timeGen;
					sumRecord.timeHHG += rec.timeHHG;
					sumRecord.timeHHT += rec.timeHHT;
					sumRecord.timeLessMin += rec.timeLessMin;
					sumRecord.timeRun += rec.timeRun;
					rec.timeSK += rec.timeSK;
					sumData.Add(rec.GA, rec);
				}
				sumRecord.processStr();
			} finally {
				try { reader.Close(); } catch { }
				try { command.Dispose(); } catch { }
				try { connection.Close(); } catch { }
			}
		}

	}
}
