using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VotGES.OgranGA;


namespace VotGES.Web.Models {
	public class TimeStopGAAnswer {		
		public Dictionary<int,OgranGARecord> YearRecords { get; set; }
		public Dictionary<int,OgranGARecord> MonthRecords { get; set; }
		public Dictionary<int, OgranGARecord> WeekRecords { get; set; }
		public Dictionary<int, string> TimeStopGA { get; set; }

		public void readData() {
			DateTime date = DateTime.Now.AddHours(-2);
			DateTime startYear = new DateTime(date.Year, 1, 1);
			DateTime startMonth = new DateTime(date.Year, date.Month, 1);
			DateTime startWeek = date.AddDays(-7);


			OgranGAReport reportYear = new OgranGAReport(startYear, date);
			OgranGAReport reportMonth = new OgranGAReport(startMonth, date);
			OgranGAReport reportWeek = new OgranGAReport(startWeek, date);
			
			reportYear.readSumData();
			reportMonth.readSumData();
			reportWeek.readSumData();

			YearRecords = new Dictionary<int, OgranGARecord>();
			MonthRecords = new Dictionary<int, OgranGARecord>();
			WeekRecords = new Dictionary<int, OgranGARecord>();

			TimeStopGA = OgranGA.OgranGA.GetTimeStopGA(DateTime.Now.AddHours(-2));

			for (int ga = 1; ga <= 10; ga++) {
				YearRecords.Add(ga,reportYear.SumData[ga]);
				MonthRecords.Add(ga, reportMonth.SumData[ga]);
				WeekRecords.Add(ga, reportWeek.SumData[ga]);
			}

		}
	}

	public class TimeStopGAWeek {
		public Dictionary<int, OgranGARecord> WeekRecords { get; set; }

		public void readData() {
			DateTime date = DateTime.Now.AddHours(-2);
			DateTime startWeek = date.AddDays(-7);

			OgranGAReport reportWeek = new OgranGAReport(startWeek, date);

			reportWeek.readSumData();

			WeekRecords = new Dictionary<int, OgranGARecord>();


			for (int ga = 1; ga <= 10; ga++) {
				WeekRecords.Add(ga, reportWeek.SumData[ga]);
			}

		}
	}
}