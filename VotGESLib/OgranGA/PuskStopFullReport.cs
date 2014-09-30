using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VotGES.Chart;
using VotGES.Piramida;

namespace VotGES.OgranGA {
	public class PuskStopFullRecord {
		public DateTime DateStart { get; set; }
		public DateTime DateEnd { get; set; }
		public OgranGARecord.ITEM_ENUM ItemType { get; set; }
		public int GANumber { get; set; }

	}

	public class PuskStopFullReport {
		public DateTime DateStart { get; set; }
		public DateTime DateEnd { get; set; }

		public Dictionary<int,List<PuskStopFullRecord>> FullData { get; set; }

		

		protected void processData(int ga, List<PiramidaEnrty> prevData, List<PiramidaEnrty> data) {
			Dictionary<OgranGARecord.ITEM_ENUM, PuskStopFullRecord> tempData=new Dictionary<OgranGARecord.ITEM_ENUM,PuskStopFullRecord>();

			List<PiramidaEnrty> allData=new List<PiramidaEnrty>();

			foreach (PiramidaEnrty rec in prevData) {
				allData.Add(rec);
			}
			foreach (PiramidaEnrty rec in data) {
				allData.Add(rec);
			}


			foreach (PiramidaEnrty rec in allData) {
				OgranGARecord.ITEM_ENUM item = OgranGARecord.getItemType(rec.Item);
				if (!tempData.ContainsKey(item))
					tempData.Add(item, null);

				if (rec.Value0 > 0) {
					PuskStopFullRecord newRecord = new PuskStopFullRecord();
					newRecord.DateStart = rec.Date<DateStart?DateStart:rec.Date;
					newRecord.GANumber = ga;
					newRecord.DateEnd = DateEnd;
					newRecord.ItemType = item;
					tempData[item] = newRecord;
					FullData[ga].Add(newRecord);
				}
				else {
					if (tempData[item] != null) {
						tempData[item].DateEnd = rec.Date;
					}
					tempData[item] = null; 
				}				
			}
		}

		public void ReadData() {

			Dictionary<int, List<PiramidaEnrty>> PrevData = new Dictionary<int, List<PiramidaEnrty>>();
			Dictionary<int, List<PiramidaEnrty>> Data = new Dictionary<int, List<PiramidaEnrty>>();
			FullData = new Dictionary<int, List<PuskStopFullRecord>>();
			for (int ga = 1; ga <= 10; ga++) {
				PrevData[ga] = OgranGA.GetPrevData(DateStart, ga);
				List<int> items = new List<int>();
				for (int h = 1; h <= 7; h++) {
					items.Add(h * 100 + ga);
				}
				Data[ga] = PiramidaAccess.GetDataFromDB(DateStart, DateEnd, 30, 2, 13, items, true, true, "PSV");
				FullData.Add(ga, new List<PuskStopFullRecord>());
				processData(ga, PrevData[ga], Data[ga]);
			}
		}

		public ChartAnswer createChart() {			
			ChartAnswer answer = new ChartAnswer();
			answer.Data = new ChartData();

			ChartProperties props = new ChartProperties();
			props.XAxisType = XAxisTypeEnum.datetime;
			props.XValueFormatString = "dd.MM HH";
			ChartAxisProperties yAx = new ChartAxisProperties();
			yAx.Auto = false;
			yAx.Index = 0;
			yAx.Min = 0;
			yAx.Max = 11;
			yAx.Interval = 1;
			props.addAxis(yAx);			

			for (int ga = 1; ga <= 10; ga++) {
				ChartSerieProperties serie = new ChartSerieProperties();
				serie.Color = ChartColor.GetColorStr(System.Drawing.Color.Green);
				serie.SerieType = ChartSerieType.stepLine;
				serie.TagName = "ga" + ga;
				serie.Title = "ГА-" + ga;
				serie.LineWidth = 2;
				serie.HideInLegend = true;
				serie.AllowHigh = false;
				props.Series.Add(serie);
				

				ChartSerieProperties serieThin = new ChartSerieProperties();
				serieThin.Color = ChartColor.GetColorStr(System.Drawing.Color.Black);
				serieThin.LineWidth = 0;
				serieThin.SerieType = ChartSerieType.line;
				serieThin.TagName = "ga_temp" + ga;
				serieThin.HideInLegend = true;
				serieThin.AllowHigh = false;
				props.Series.Add(serieThin);

				ChartSerieProperties serieGen = new ChartSerieProperties();
				serieGen.Color = ChartColor.GetColorStr(System.Drawing.Color.Blue);
				serieGen.LineWidth = 1;
				serieGen.SerieType = ChartSerieType.stepLine;
				serieGen.TagName = "ga_gen" + ga;
				serieGen.HideInLegend = true;
				serieGen.AllowHigh = false;
				props.Series.Add(serieGen);

				if (ga <= 2 || ga >= 9) {
					ChartSerieProperties serieSK = new ChartSerieProperties();
					serieSK.Color = ChartColor.GetColorStr(System.Drawing.Color.Orange);
					serieSK.LineWidth = 1;
					serieSK.SerieType = ChartSerieType.stepLine;
					serieSK.TagName = "ga_sk" + ga;
					serieSK.HideInLegend = true;
					serieSK.AllowHigh = false;
					props.Series.Add(serieSK);
				}

				ChartSerieProperties serieHHG = new ChartSerieProperties();
				serieHHG.Color = ChartColor.GetColorStr(System.Drawing.Color.Yellow);
				serieHHG.LineWidth = 1;
				serieHHG.SerieType = ChartSerieType.stepLine;
				serieHHG.TagName = "ga_hhg" + ga;
				serieHHG.HideInLegend = true;
				serieHHG.AllowHigh = false;
				props.Series.Add(serieHHG);

				ChartSerieProperties serieHHT = new ChartSerieProperties();
				serieHHT.Color = ChartColor.GetColorStr(System.Drawing.Color.YellowGreen);
				serieHHT.LineWidth = 1;
				serieHHT.SerieType = ChartSerieType.stepLine;
				serieHHT.TagName = "ga_hht" + ga;
				serieHHT.HideInLegend = true;
				serieHHT.AllowHigh = false;
				props.Series.Add(serieHHT);

				ChartSerieProperties serieAfterMax = new ChartSerieProperties();
				serieAfterMax.Color = ChartColor.GetColorStr(System.Drawing.Color.Red);
				serieAfterMax.LineWidth = 1;
				serieAfterMax.SerieType = ChartSerieType.stepLine;
				serieAfterMax.TagName = "ga_afterMax" + ga;
				serieAfterMax.HideInLegend = true;
				serieAfterMax.AllowHigh = false;
				props.Series.Add(serieAfterMax);

				ChartSerieProperties serieLessMin = new ChartSerieProperties();
				serieLessMin.Color = ChartColor.GetColorStr(System.Drawing.Color.Red);
				serieLessMin.LineWidth = 1;
				serieLessMin.SerieType = ChartSerieType.stepLine;
				serieLessMin.TagName = "ga_lessMin" + ga;
				serieLessMin.HideInLegend = true;
				serieLessMin.AllowHigh = false;				
				props.Series.Add(serieLessMin);

				

				ChartDataSerie data = new ChartDataSerie();
				data.Name = "ga" + ga;

				ChartDataSerie dataTemp = new ChartDataSerie();
				dataTemp.Name = "ga_temp" + ga;

				ChartDataSerie dataGen = new ChartDataSerie();
				dataGen.Name = "ga_gen" + ga;

				ChartDataSerie dataSK = new ChartDataSerie();
				dataSK.Name = "ga_sk" + ga;

				ChartDataSerie dataHHG = new ChartDataSerie();
				dataHHG.Name = "ga_hhg" + ga;

				ChartDataSerie dataHHT = new ChartDataSerie();
				dataHHT.Name = "ga_hht" + ga;

				ChartDataSerie dataAfterMax = new ChartDataSerie();
				dataAfterMax.Name = "ga_afterMax" + ga;

				ChartDataSerie dataLessMin = new ChartDataSerie();
				dataLessMin.Name = "ga_lessMin" + ga;

				dataTemp.Points.Add(new ChartDataPoint(DateStart, ga));
				dataTemp.Points.Add(new ChartDataPoint(DateEnd, ga));

				DateTime prevDateRun = DateTime.MinValue;
				DateTime prevDateGen = DateTime.MinValue;
				DateTime prevDateSK = DateTime.MinValue;
				DateTime prevDateHHT = DateTime.MinValue;
				DateTime prevDateHHG = DateTime.MinValue;
				DateTime prevDateAfterMax = DateTime.MinValue;
				DateTime prevDateLessMin = DateTime.MinValue;

				foreach (PuskStopFullRecord rec in FullData[ga]) {
					if (rec.ItemType == OgranGARecord.ITEM_ENUM.rezRun) {
						if (prevDateRun != DateTime.MinValue) {
							data.Points.Add(new ChartDataPoint(prevDateRun, ga));
						}
						data.Points.Add(new ChartDataPoint(rec.DateStart, ga+0.7));
						data.Points.Add(new ChartDataPoint(rec.DateEnd, ga+0.7));
						prevDateRun = rec.DateEnd;
					}

					if (rec.ItemType == OgranGARecord.ITEM_ENUM.rezGen) {
						if (prevDateGen != DateTime.MinValue) {
							dataGen.Points.Add(new ChartDataPoint(prevDateGen, ga));
						}
						dataGen.Points.Add(new ChartDataPoint(rec.DateStart, ga + 0.4));
						dataGen.Points.Add(new ChartDataPoint(rec.DateEnd, ga + 0.4));
						prevDateGen = rec.DateEnd;
					}

					if (rec.ItemType == OgranGARecord.ITEM_ENUM.rezSK) {
						if (prevDateSK != DateTime.MinValue) {
							dataSK.Points.Add(new ChartDataPoint(prevDateSK, ga));
						}
						dataSK.Points.Add(new ChartDataPoint(rec.DateStart, ga + 0.4));
						dataSK.Points.Add(new ChartDataPoint(rec.DateEnd, ga + 0.4));
						prevDateSK = rec.DateEnd;
					}

					if (rec.ItemType == OgranGARecord.ITEM_ENUM.rezHHG) {
						if (prevDateHHG != DateTime.MinValue) {
							dataHHG.Points.Add(new ChartDataPoint(prevDateHHG, ga));
						}
						dataHHG.Points.Add(new ChartDataPoint(rec.DateStart, ga + 0.4));
						dataHHG.Points.Add(new ChartDataPoint(rec.DateEnd, ga + 0.4));
						prevDateHHG = rec.DateEnd;
					}

					if (rec.ItemType == OgranGARecord.ITEM_ENUM.rezHHT) {
						if (prevDateHHT != DateTime.MinValue) {
							dataHHT.Points.Add(new ChartDataPoint(prevDateHHT, ga));
						}
						dataHHT.Points.Add(new ChartDataPoint(rec.DateStart, ga + 0.4));
						dataHHT.Points.Add(new ChartDataPoint(rec.DateEnd, ga + 0.4));
						prevDateHHT = rec.DateEnd;
					}

					if (rec.ItemType == OgranGARecord.ITEM_ENUM.pAfterMax) {
						if (prevDateAfterMax != DateTime.MinValue) {
							dataAfterMax.Points.Add(new ChartDataPoint(prevDateAfterMax, ga));
						}
						dataAfterMax.Points.Add(new ChartDataPoint(rec.DateStart, ga + 0.9));
						dataAfterMax.Points.Add(new ChartDataPoint(rec.DateEnd, ga + 0.9));
						prevDateAfterMax = rec.DateEnd;
					}

					if (rec.ItemType == OgranGARecord.ITEM_ENUM.pLessMin) {
						if (prevDateLessMin != DateTime.MinValue) {
							dataLessMin.Points.Add(new ChartDataPoint(prevDateLessMin, ga));
						}
						dataLessMin.Points.Add(new ChartDataPoint(rec.DateStart, ga + 0.9));
						dataLessMin.Points.Add(new ChartDataPoint(rec.DateEnd, ga + 0.9));
						prevDateLessMin = rec.DateEnd;
					}


				}
				answer.Data.addSerie(data);
				answer.Data.addSerie(dataTemp);
				answer.Data.addSerie(dataGen);
				answer.Data.addSerie(dataHHG);
				answer.Data.addSerie(dataHHT);
				answer.Data.addSerie(dataAfterMax);
				answer.Data.addSerie(dataLessMin);
				if (ga <= 2 || ga >= 9) {
					answer.Data.addSerie(dataSK);
				}
			}

			answer.Properties = props;
			return answer;
		}
	}
}
