using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VotGES.Chart;
using VotGES.OgranGA;
using VotGES.Piramida;
using VotGES.Rashod;


namespace VotGES.Web.Models {	
	public class OgranGAAnswer {
		public OgranGARecord ExpStartRecord { get; protected set; }
		public OgranGARecord YearStartRecord { get; protected set; }
		public OgranGARecord MonthStartRecord { get; protected set; }
		public OgranGARecord DayStartRecord { get; protected set; }
		public OgranGARecord KRRecord { get; protected set; }
		public ChartAnswer ChartAnswer { get;  set; }
		public ChartDataSerie CurrentData  {get;set;}

		public double CurrentP { get; protected set; }
		public double CurrentOtkrNA { get; protected set; }
		public double CurrentUgolRK { get; protected set; }
		public double CurrentNapor { get; protected set; }
		public double CurrentKPD { get; protected set; }
		public double CurrentRashod { get; protected set; }

		public void createAnswer(int ga, bool createNarab,bool createCurrent) {
			if (createNarab) {
				DateTime now = DateTime.Now;
				DateTime expStart = new DateTime(2000, 1, 1);
				DateTime yearStart = new DateTime(now.Year, 1, 1);
				DateTime monthStart = new DateTime(now.Year, now.Month, 1);
				DateTime dayStart = new DateTime(now.Year, now.Month, now.Day);
				DateTime krDate = KapRemontsData.Single.Data[ga - 1].Date;

				OgranGAReport reportEkspStart = new OgranGAReport(expStart, now);
				reportEkspStart.readSumData();
				ExpStartRecord = reportEkspStart.SumData[ga];

				OgranGAReport reportYearStart = new OgranGAReport(yearStart, now);
				reportYearStart.readSumData();
				YearStartRecord = reportYearStart.SumData[ga];

				OgranGAReport reportMonthStart = new OgranGAReport(monthStart, now);
				reportMonthStart.readSumData();
				MonthStartRecord = reportMonthStart.SumData[ga];

				OgranGAReport reportDayStart = new OgranGAReport(dayStart, now);
				reportDayStart.readSumData();
				DayStartRecord = reportDayStart.SumData[ga];

				OgranGAReport reportKR = new OgranGAReport(krDate, now);
				reportKR.readSumData();
				KRRecord = reportKR.SumData[ga];
			}


			if (createCurrent) {
				int itemP = 200 + ga;
				List<int> items = new List<int>() { 200 + ga, 100 + ga, 400 + ga, 500 + ga, 600 + ga };
				ChartDataSerie data = new ChartDataSerie();
				List<PiramidaEnrty> currentData = PiramidaAccess.GetDataFromDB(DateTime.Now.AddHours(-2), DateTime.Now.AddHours(-2).AddMinutes(-50), 3, 2, 4, items, false, true, "PMin");
				foreach (PiramidaEnrty rec in currentData) {
					if (rec.Item == 200 + ga)
						CurrentP = rec.Value0;
					if (rec.Item == 100 + ga)
						CurrentRashod = rec.Value0;
					if (rec.Item == 400 + ga)
						CurrentOtkrNA = rec.Value0;
					if (rec.Item == 500 + ga)
						CurrentUgolRK = rec.Value0;
					if (rec.Item == 600 + ga)
						CurrentNapor = rec.Value0;
					data.Points.Add(new ChartDataPoint(CurrentP, CurrentNapor));
				}
				CurrentKPD = RashodTable.KPD(CurrentP, CurrentNapor, CurrentRashod);
				data.Name = "dataWork";
				CurrentData = data;


				/*ChartDataSerie data1 = new ChartDataSerie();
				Random rand = new Random();
				data1.Points.Add(new ChartDataPoint(30,16));
				data1.Points.Add(new ChartDataPoint(40, 17));
				data1.Points.Add(new ChartDataPoint(50, 18));
				data1.Points.Add(new ChartDataPoint(60, 19));
				data1.Points.Add(new ChartDataPoint(70, 20));
				data1.Points.Add(new ChartDataPoint(80, 21));
				data1.Points.Add(new ChartDataPoint(90, 22));
				data1.Name = "dataWork";
				CurrentData = data1;*/
			}
		}
	}
}