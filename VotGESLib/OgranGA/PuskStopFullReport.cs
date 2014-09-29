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


			foreach (PiramidaEnrty rec in data) {
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
			props.addAxis(yAx);			

			for (int ga = 1; ga <= 10; ga++) {
				ChartSerieProperties serie = new ChartSerieProperties();
				serie.Color = ChartColor.GetColorStr(System.Drawing.Color.Green);
				serie.SerieType = ChartSerieType.stepLine;
				serie.TagName = "ga" + ga;
				serie.Title = "ГА-" + ga;
				props.Series.Add(serie);


				ChartDataSerie data = new ChartDataSerie();
				data.Name = "ga" + ga;
				foreach (PuskStopFullRecord rec in FullData[ga]) {
					if (rec.ItemType == OgranGARecord.ITEM_ENUM.rezRun) {
						data.Points.Add(new ChartDataPoint(rec.DateStart, 0.5));
						data.Points.Add(new ChartDataPoint(rec.DateEnd, 0));
					}
				}
				answer.Data.addSerie(data);
			}

			answer.Properties = props;
			return answer;
		}
	}
}
