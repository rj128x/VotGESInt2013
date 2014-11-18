using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VotGES.OgranGA;


namespace VotGES.Web.Models {
	public class TimeStopGAAnswer {		
		public Dictionary<int,OgranGARecord> YearRecords { get; set; }
		public Dictionary<int,OgranGARecord> MonthRecords { get; set; }
		public Dictionary<int, string> TimeStopGA { get; set; }

		public void readData() {
			DateTime date = DateTime.Now.AddHours(-2);
			DateTime startYear = new DateTime(date.Year, 1, 1);
			DateTime startMonth = new DateTime(date.Year, date.Month, 1);

			OgranGAReport reportYear = new OgranGAReport(startYear, date);
			OgranGAReport reportMonth = new OgranGAReport(startMonth, date);
			
			reportYear.readSumData();
			reportMonth.readSumData();

			YearRecords = new Dictionary<int, OgranGARecord>();
			MonthRecords = new Dictionary<int, OgranGARecord>();
			TimeStopGA = OgranGA.OgranGA.GetTimeStopGA(DateTime.Now.AddHours(-2));

			for (int ga = 1; ga <= 10; ga++) {
				YearRecords.Add(ga,reportYear.SumData[ga]);
				MonthRecords.Add(ga, reportMonth.SumData[ga]);
			}

		}
	}
}