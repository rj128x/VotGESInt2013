using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VotGES.Chart;

namespace VotGES.Rashod
{
	public class KPDLine
	{
		protected static ChartProperties getChartPropertiesKPDS(int[] kpd, bool ogran) {
			ChartProperties props = new ChartProperties();
			props.XAxisType = XAxisTypeEnum.numeric;
			props.XValueFormatString = "0.##";
			ChartAxisProperties yAx = new ChartAxisProperties();
			yAx.Auto = false;
			yAx.Index = 0;
			yAx.Min = 15;
			yAx.Max = 23;

			for (int index = 0; index < kpd.Length; index++) {
				string post = kpd[index].ToString();
				ChartSerieProperties serie = new ChartSerieProperties();
				serie.Color = ChartColor.GetColorStr(index);
				serie.LineWidth = 0;
				serie.SerieType = ChartSerieType.kpdLine;
				serie.Marker = true;
				serie.LineWidth = -1;
				serie.Title = "КПД " + post;
				serie.TagName = "kpd_" + post;
				serie.Enabled = true;
				serie.YAxisIndex = 0;
				props.addSerie(serie);
			}

			if (ogran) {
				ChartSerieProperties ser = new ChartSerieProperties();
				ser.Color = ChartColor.GetColorStr(System.Drawing.Color.Red);
				ser.LineWidth = 3;
				ser.SerieType = ChartSerieType.line;
				ser.Title = "Ограничения макс";
				ser.TagName = "ogrMax";
				ser.Enabled = true;
				ser.YAxisIndex = 0;
				ser.AllowHigh = false;
				props.addSerie(ser);


				ser = new ChartSerieProperties();
				ser.Color = ChartColor.GetColorStr(System.Drawing.Color.Red);
				ser.LineWidth = 3;
				ser.SerieType = ChartSerieType.line;
				ser.Title = "Ограничения мин";
				ser.TagName = "ogrMin";
				ser.Enabled = true;
				ser.YAxisIndex = 0;
				ser.AllowHigh = false;
				props.addSerie(ser);
			}

			props.addAxis(yAx);
			return props;
		}

		public static ChartAnswer createKPDTable(int ga) {
			ChartAnswer answer = new ChartAnswer();

			int[] kpds = { 80, 83, 85, 86, 87, 88, 89, 90, 95 };
			/*switch (ga) {
				case 1:
					int[] ks1={78,79,80,81,82,83,84,85,86,87};
					kpds = ks1;
					break;
				case 2:
					int[] ks2 = { 80,81, 82, 83, 84, 85, 86, 87, 88, 89};
					kpds = ks2;
					break;
				case 3:
					int[] ks3 = { 81, 82, 83, 84, 85, 86, 87, 88, 89,90 };
					kpds = ks3;
					break;
				case 4:
					int[] ks4 = { 80, 81, 82, 83, 84, 85, 86, 87, 88,89,90 };
					kpds = ks4;
					break;
				case 5:
					int[] ks5 = { 80, 81, 82, 83, 84, 85, 86, 87, 88, 89 };
					kpds = ks5;
					break;
				case 6:
					int[] ks6 = {  81, 82, 83, 84, 85, 86, 87, 88, 89,90 };
					kpds = ks6;
					break;
				case 7:
					int[] ks7 = {  81, 82, 83, 84, 85, 86, 87, 88, 89,90 };
					kpds = ks7;
					break;
				case 8:
					int[] ks8 = { 80, 81, 82, 83, 84, 85, 86, 87, 88, 89 };
					kpds = ks8;
					break;
				case 9:
					int[] ks9 = { 75, 80, 81, 82, 83, 84, 85, 86, 87, 88, 89 };
					kpds = ks9;
					break;
				case 10:
					int[] ks10 = {79, 80, 81, 82, 83, 84, 85, 86, 87, 88};
					kpds = ks10;
					break;

			}*/
			if (ga == 4) {
				int[] ks4 = { 90,93,94,95,96,97,98 };
				kpds = ks4;
			}
			RashodTable table = RashodTable.getRashodTable(ga);

			answer.Properties = getChartPropertiesKPDS(kpds, (ga >= 1 && ga <= 10));
			answer.Data = new ChartData();

			Dictionary<int, ChartDataSerie> dataSeries = new Dictionary<int, ChartDataSerie>();
			foreach (int kpd in kpds) {
				ChartDataSerie data = new ChartDataSerie();
				data.Name = "kpd_" + kpd;
				dataSeries.Add(kpd, data);
			}

			for (double napor = 15; napor < 23; napor += table.stepNapor) {
				double minP = 35;
				double nap = 15;
				double maxP = 1200;
				double step = 10;
				if (ga >= 1 && ga <= 10) {
					nap = OgranLineTable.OgranData[ga].Keys.First(n => n >= napor);
					maxP = OgranLineTable.OgranData[ga][nap];
					step = table.stepPower;
				}
				for (double power = minP; power <= maxP; power += step) {
					double rashod = RashodTable.getRashod(ga, power, napor);
					double kpd = RashodTable.KPD(power, napor, rashod);
					int kpdInt = (int)Math.Round(kpd * 100);
					kpdInt = kpdInt < kpds.Min() ? kpds.Min() : kpdInt;
					kpdInt = kpdInt > kpds.Max() ? kpds.Max() : kpdInt;
					int kpdRes = kpds.First(k => k >= kpdInt);
					if (kpds.Contains(kpdRes)) {
						dataSeries[kpdRes].Points.Add(new ChartDataPoint(power, napor));
					}
				}
			}

			foreach (int kpd in kpds) {
				answer.Data.addSerie(dataSeries[kpd]);
			}

			if (ga >= 1 && ga <= 10) {
				ChartDataSerie ogranMin = new ChartDataSerie();
				ogranMin.Name = "ogrMin";

				ChartDataSerie ogranMax = new ChartDataSerie();
				ogranMax.Name = "ogrMax";

				foreach (KeyValuePair<double, double> de in OgranLineTable.OgranData[ga]) {
					if (de.Key >= 15) {
						ogranMax.Points.Add(new ChartDataPoint(de.Value, de.Key));
						ogranMin.Points.Add(new ChartDataPoint(35, de.Key));
					}
				}
				answer.Data.addSerie(ogranMin);
				answer.Data.addSerie(ogranMax);
			}



			return answer;
		}
	}
}
