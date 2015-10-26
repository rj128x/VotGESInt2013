using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VotGES.Piramida;
using VotGES.Chart;

namespace VotGES.PrognozNB
{
	public class CheckPrognozNB:PrognozNBFunc
	{
		public bool IsQFakt { get; protected set; }
		public int HourStart { get; protected set; }
		public int MinStart { get; protected set; }
		public CheckPrognozNB(DateTime dateStart, int daysCount, bool isQFakt,int hourStart, int minStart):base(dateStart,daysCount) {
			DateStart = dateStart.Date;
			DaysCount = daysCount;
			IsQFakt = isQFakt;
			MinStart = minStart;
			HourStart = hourStart;
			DatePrognozStart = DateStart.AddHours(HourStart).AddMinutes(MinStart);
			DateEnd = DateStart.AddDays(daysCount);
		}

		public void startPrognoz(bool correct) {
			Prognoz = new PrognozNB();

			Prognoz.FirstData = readFirstData(DatePrognozStart);
			Prognoz.FirstDataSut = readFirstDataSut(DatePrognozStart);
			readP();
			readPBR();
			readWater();
			checkData(DateStart,DateEnd);

			Prognoz.DatePrognozStart = DatePrognozStart;
			Prognoz.DatePrognozEnd = DateEnd;
			Prognoz.PArr = new SortedList<DateTime, double>();
			Prognoz.IsQFakt = IsQFakt;
			if (IsQFakt) {
				foreach (KeyValuePair<DateTime,double> de in QFakt) {
					if (de.Key > DatePrognozStart) {
						Prognoz.PArr.Add(de.Key, de.Value);
					}
				}
			} else {
				foreach (KeyValuePair<DateTime,double> de in PFakt) {
					DateTime dt = de.Key.Minute == 0 ? de.Key : de.Key.AddMinutes(60);
					if (!Prognoz.PArr.ContainsKey(dt))
						Prognoz.PArr.Add(dt, 0);
					if (de.Key > DatePrognozStart) {
						Prognoz.PArr[dt]+= de.Value;
					}
				}
			}
			//prognoz.calcPrognoz(correct);
			Prognoz.calcPrognozNeW();
		}

		public override ChartAnswer getChart() {
			ChartAnswer chart= base.getChart();
			chart.Properties.removeSerie("PBR");
			chart.Data.removeSerie("PBR");

			if (IsQFakt) {
				chart.Properties.removeSerie("QPrognoz");
				chart.Data.removeSerie("QPrognoz");
				chart.Properties.removeSerie("NaporPrognoz");
				chart.Data.removeSerie("NaporPrognoz");
			}

			chart.Properties.Series[chart.Properties.SeriesNames["PFakt"]].Enabled = false;
			chart.Properties.Series[chart.Properties.SeriesNames["QFakt"]].Enabled = false;

			chart.processAxes();

			return chart;
		}
	}
}
