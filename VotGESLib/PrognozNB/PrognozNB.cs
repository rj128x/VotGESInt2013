using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VotGES.Chart;
using VotGES.Rashod;

namespace VotGES.PrognozNB
{

	public class PrognozNBFirstData
	{
		public DateTime Date { get; set; }
		public double Q { get; set; }
		public double P { get; set; }
		public double T { get; set; }
		public double NB { get; set; }
		public double VB { get; set; }
		public double Pritok { get; set; }
	}
	
	public class PrognozNB
	{		
		public SortedList<DateTime, PrognozNBFirstData> FirstData { get; set; }
		public SortedList<DateTime, PrognozNBFirstData> FirstDataSut { get; set; }

		
		public double T  {get;set;}
		
		public DateTime DatePrognozStart {get;set;} 

		public DateTime DatePrognozEnd{get;set;} 

		public SortedList<DateTime, double> Rashods {get;set;}

		public SortedList<DateTime, double> Napors {get;set;}

		public SortedList<DateTime, double> PArr {get;set;}

		public SortedList<DateTime, double> Prognoz{get;set;}
		
		public SortedList<DateTime, double> PrognozVB {get;set;}

		public PrognozNBInitData InitData { get; set; }

		public bool IsQFakt {get;set;}
		public bool QMax { get; set; }

		protected static NNET.NNET nnet;

		static PrognozNB() {
			nnet = NNET.NNET.getNNET(NNET.NNETMODEL.vges_nb);			
		}

		public void calcPrognozNeW() {
			
			SortedList<int,double>InputVector=new SortedList<int,double>();
			SortedList<int, double> prevDataRashodArray = new SortedList<int, double>();
			SortedList<int, double> prevDataNBArray = new SortedList<int, double>();
			SortedList<int, double> prevDataVBArray = new SortedList<int, double>();
			SortedList<int, double> prevDataTArray = new SortedList<int, double>();
			SortedList<int, double> prevDataPritokArray = new SortedList<int, double>();
			
			Prognoz = new SortedList<DateTime, double>();

			int index = 0;
			double k = 0;
			foreach (DateTime date in FirstData.Keys) {
				prevDataRashodArray.Add(index, FirstData[date].Q);
				prevDataNBArray.Add(index, FirstData[date].NB);
				prevDataVBArray.Add(index, FirstData[date].VB);
				//k = FirstData[date].Q / RashodTable.getStationRashod(FirstData[date].P, FirstData[date].VB - FirstData[date].NB, RashodCalcMode.avg);
				index++;
			}

			index = 0;
			foreach (DateTime date in FirstDataSut.Keys) {
				prevDataTArray.Add(index, FirstDataSut[date].T);
				prevDataPritokArray.Add(index,FirstDataSut[date].Pritok);
				index++;
			}

			index=0;
			
			int cntSut=FirstDataSut.Keys.Count;
			int cntH=FirstData.Keys.Count;

			for (int i = -1; i <= 69; i++) {
				InputVector.Add(i, 0);
			}

			for (int i = 0; i < 8; i++) {
				InputVector[0+i]=prevDataTArray[cntSut - i - 1];
				switch (i) {
					case 0:
						InputVector[0 + i] = InitData.UseInitData ? InitData.T1 : InputVector[0 + i];
						InitData.T1 = InputVector[0 + i];
						break;
					case 1:
						InputVector[0 + i] = InitData.UseInitData ? InitData.T2 : InputVector[0 + i];
						InitData.T2 = InputVector[0 + i];
						break;
					case 2:
						InputVector[0 + i] = InitData.UseInitData ? InitData.T3 : InputVector[0 + i];
						InitData.T3 = InputVector[0 + i];
						break;
					case 3:
						InputVector[0 + i] = InitData.UseInitData ? InitData.T4 : InputVector[0 + i];
						InitData.T4 = InputVector[0 + i];
						break;
					case 4:
						InputVector[0 + i] = InitData.UseInitData ? InitData.T5 : InputVector[0 + i];
						InitData.T5 = InputVector[0 + i];
						break;
					case 5:
						InputVector[0 + i] = InitData.UseInitData ? InitData.T6 : InputVector[0 + i];
						InitData.T6 = InputVector[0 + i];
						break;
					case 6:
						InputVector[0 + i] = InitData.UseInitData ? InitData.T7 : InputVector[0 + i];
						InitData.T7 = InputVector[0 + i];
						break;
					case 7:
						InputVector[0 + i] = InitData.UseInitData ? InitData.T8 : InputVector[0 + i];
						InitData.T8 = InputVector[0 + i];
						break;
				}
			}

			InputVector[8]=InitData.UseInitData?InitData.Davl1:746;
			InputVector[9] = InitData.UseInitData ? InitData.Davl2 : 746;
			InputVector[10] = InitData.UseInitData ? InitData.Davl3 : 746;
			InitData.Davl1 = InputVector[8]; InitData.Davl2 = InputVector[9]; InitData.Davl3 = InputVector[10];


			InputVector[11] = InitData.UseInitData ? InitData.Vl1 : 83;
			InputVector[12] = InitData.UseInitData ? InitData.Vl2 : 83;
			InputVector[13] = InitData.UseInitData ? InitData.Vl3 : 83;
			InitData.Vl1 = InputVector[11]; InitData.Vl2 = InputVector[12]; InitData.Vl3 = InputVector[13];

			double Koeff = InitData.UseInitData ? InitData.Koeff : 1;
			InitData.Koeff = Koeff;

			InputVector[14]=0;
			InputVector[15]=0;
			InputVector[16]=0;
			InputVector[17]=0;
			InputVector[18]=0;
			InputVector[19]=0;

			InputVector[20]=prevDataPritokArray[cntSut - 1];
			InputVector[21]=prevDataPritokArray[cntSut - 2];

			if (InitData.UseInitData) {
				InputVector[20] = InitData.Pritok0;
				InputVector[21] = InitData.Pritok1;
			}
			else {
				InitData.Pritok0 = InputVector[20];
				InitData.Pritok1 = InputVector[21];
			}

			DateTime dateStart = PArr.First().Key.Date;
			DateTime dateEnd = PArr.Last().Key.Date;

			Napors = new SortedList<DateTime, double>();
			Rashods = new SortedList<DateTime, double>();
			PrognozVB=new SortedList<DateTime,double>();
			Prognoz=new SortedList<DateTime,double>();

			while (dateStart < dateEnd) {
				for (int hour=0;hour<24;hour++){
					DateTime dt=dateStart.AddHours(hour+1);
					if (!PrognozVB.ContainsKey(dt)){
						if (PrognozVB.Count==0)
							PrognozVB.Add(dt,prevDataVBArray.Last().Value);
						else
							PrognozVB.Add(dt, PrognozVB.Last().Value);
					}

					if (!Prognoz.ContainsKey(dt)){
						if (Prognoz.Count == 0)
							Prognoz.Add(dt, prevDataNBArray.Last().Value);
						else
							Prognoz.Add(dt, Prognoz.Last().Value);
					}


					if (!Rashods.ContainsKey(dt)){
						if (Rashods.Count == 0)
							Rashods.Add(dt, prevDataRashodArray.Last().Value);
						else
							Rashods.Add(dt, Rashods.Last().Value);
					}


				}


				for (int i = 0; i < 12; i++) {
					InputVector[22 + i]=prevDataRashodArray[i];
				}

				double napor = prevDataVBArray.Last().Value-prevDataNBArray.Last().Value;
				List<int> avail = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
				for (int i = 0; i < 24; i++) {					
					List<int> sostav = new List<int>();
					double q = IsQFakt ? PArr[dateStart.AddHours(i + 1)] : RUSA.getOptimRashod(PArr[dateStart.AddHours(i + 1)], napor, !QMax, sostav, avail)*Koeff;
					

					InputVector[34 + i]=q;
				}

				for (int i=0;i<12;i++){
					InputVector[58+i]=prevDataVBArray[i];
				}

				InputVector[-1]=(dateStart.Ticks - (new DateTime(1970, 1, 1)).Ticks) / 10000000;
					

				for (int step = 0; step <= 1; step++) {
					for (int i = 0; i < 12; i++) {
						InputVector[58 + i] = prevDataVBArray[i];
					}
					InputVector[20] = InitData.Pritok0;
					InputVector[21] = InitData.Pritok1;

					for (int hour = 0; hour < 24; hour++) {
						DateTime dt=dateStart.AddHours(hour+1);
						String str = hour.ToString() + ";" + String.Join(";", InputVector.Values).Replace(',', '.');
						//Logger.Info(str);
						double vb = PrognozNBFunc.predict_ub(str);

						PrognozVB[dt] = vb;
						double np = vb - Prognoz[dt];
						Napors[dt] = np;
						List<int> sostav = new List<int>();
						double q = IsQFakt ? PArr[dt] : RUSA.getOptimRashod(PArr[dt], np, !QMax, sostav, avail) * Koeff;
						Rashods[dt] = q;
						//InputVector[34 + hour] = q;

						if (hour > 11 && step==1) {
							prevDataVBArray[hour - 12] = vb;
						}
					}

					for (int h = 0; h < 24; h++) {
						InputVector[34 + h] = Rashods[dateStart.AddHours(h + 1)];
					}

						for (int i = 0; i < 12; i++) {
							InputVector[58 + i] = prevDataNBArray[i];
						}
						InputVector[20] = InitData.UseInitData ? InitData.Podpor1 : 63.27;
						InputVector[21] = InitData.UseInitData ? InitData.Podpor2 : 63.27;
						InitData.Podpor1 = InputVector[20];
						InitData.Podpor2 = InputVector[21];

					for (int hour = 0; hour < 24; hour++) {
						DateTime dt = dateStart.AddHours(hour + 1);
						String str = hour.ToString() + ";" + String.Join(";", InputVector.Values).Replace(',', '.');
						double nb = PrognozNBFunc.predict_nb(str);

						Prognoz[dt] = nb;
						double np = PrognozVB[dt] - nb;
						Napors[dt] = np;
						List<int> sostav = new List<int>();
						double q = IsQFakt ? PArr[dt] : RUSA.getOptimRashod(PArr[dt], np, !QMax, sostav, avail) * Koeff;
						Rashods[dt] = q;
						//InputVector[34 + hour] = q;

						if (hour > 11 && step==1) {
							prevDataNBArray[hour - 12] = nb;
							prevDataRashodArray[hour - 12] = q;
						}
						
					}
					for (int h = 0; h < 24; h++) {
						InputVector[34 + h] = Rashods[dateStart.AddHours(h + 1)];
					}


				}				
				dateStart = dateStart.AddHours(24);
			}
		}

				
		
		public void AddChartData(ChartData data) {
			ChartDataSerie prognozNBSerie=new ChartDataSerie();
			foreach (KeyValuePair<DateTime,double> de in Prognoz) {
				prognozNBSerie.Points.Add(new ChartDataPoint(de.Key,de.Value));
			}

			ChartDataSerie prognozVBSerie = new ChartDataSerie();
			foreach (KeyValuePair<DateTime, double> de in PrognozVB) {
				prognozVBSerie.Points.Add(new ChartDataPoint(de.Key, de.Value));
			}

			ChartDataSerie prognozQSerie=new ChartDataSerie();
			foreach (KeyValuePair<DateTime,double> de in Rashods) {
				prognozQSerie.Points.Add(new ChartDataPoint(de.Key, de.Value));
			}

			ChartDataSerie prognozNaporSerie=new ChartDataSerie();
			foreach (KeyValuePair<DateTime,double> de in Napors) {
				prognozNaporSerie.Points.Add(new ChartDataPoint(de.Key, de.Value));
			}

			prognozNBSerie.Name = "NBPrognoz";
			prognozVBSerie.Name = "VBPrognoz";
			prognozQSerie.Name = "QPrognoz";
			prognozNaporSerie.Name = "NaporPrognoz";

			data.addSerie(prognozNBSerie);
			data.addSerie(prognozVBSerie);
			data.addSerie(prognozQSerie);
			data.addSerie(prognozNaporSerie);
		}


		public ChartProperties createChartProperties() {
			ChartProperties props=new ChartProperties();
			props.XAxisType = XAxisTypeEnum.datetime;

			ChartAxisProperties pAx=new ChartAxisProperties();
			pAx.Min = 0;
			pAx.Max = 1020;
			pAx.Auto = false;
			pAx.Interval = 100;
			pAx.Index = 1;

			ChartAxisProperties nbAx=new ChartAxisProperties();
			nbAx.Auto = true;
			nbAx.Interval = 0.1;
			nbAx.Index = 0;
			nbAx.MinHeight = 0.2;
			nbAx.ProcessAuto = true;

			ChartAxisProperties qAx=new ChartAxisProperties();
			qAx.Auto = false;
			qAx.Min = 0;
			qAx.Max = 7200;
			qAx.Index = 2;

			ChartAxisProperties vbAx=new ChartAxisProperties();
			vbAx.Auto = true;
			vbAx.Index = 3;
			vbAx.MinHeight = 0.05;
			vbAx.ProcessAuto = true;

			ChartAxisProperties naporAx=new ChartAxisProperties();
			naporAx.Auto = true;
			naporAx.Index = 4;
			naporAx.MinHeight = 0.2;
			naporAx.ProcessAuto = true;

			ChartAxisProperties tAx=new ChartAxisProperties();
			tAx.Auto = true;
			tAx.Index = 5;
			tAx.MinHeight = 20;
			tAx.ProcessAuto = true;
			tAx.Interval = 1;


			ChartSerieProperties pSerie=new ChartSerieProperties();
			pSerie.Color = "0-0-255";
			pSerie.LineWidth = 2;
			pSerie.SerieType = ChartSerieType.line;
			pSerie.Title = "P факт";
			pSerie.TagName = "PFakt";
			pSerie.YAxisIndex = 1;

			ChartSerieProperties pbrSerie=new ChartSerieProperties();
			pbrSerie.Color = "0-0-255";
			pbrSerie.LineWidth = 1;
			pbrSerie.SerieType = ChartSerieType.line;
			pbrSerie.Title = "ПБР";
			pbrSerie.TagName = "PBR";
			pbrSerie.YAxisIndex = 1;

			ChartSerieProperties nbFaktSerie=new ChartSerieProperties();
			nbFaktSerie.Color = "255-0-0";
			nbFaktSerie.LineWidth = 2;
			nbFaktSerie.SerieType = ChartSerieType.line;
			nbFaktSerie.Title = "НБ факт";
			nbFaktSerie.TagName = "NBFakt";
			nbFaktSerie.YAxisIndex = 0;

			ChartSerieProperties nbPrognozSerie=new ChartSerieProperties();
			nbPrognozSerie.Color = "255-0-0";
			nbPrognozSerie.LineWidth = 1;
			nbPrognozSerie.SerieType = ChartSerieType.line;
			nbPrognozSerie.Title = "НБ прогноз";
			nbPrognozSerie.TagName = "NBPrognoz";
			nbPrognozSerie.YAxisIndex = 0;

			ChartSerieProperties qFaktSerie=new ChartSerieProperties();
			qFaktSerie.Color = "0-255-0";
			qFaktSerie.LineWidth = 2;
			qFaktSerie.SerieType = ChartSerieType.line;
			qFaktSerie.Title = "Q факт";
			qFaktSerie.TagName = "QFakt";
			qFaktSerie.YAxisIndex = 2;

			ChartSerieProperties qPrognozSerie=new ChartSerieProperties();
			qPrognozSerie.Color = "0-255-0";
			qPrognozSerie.LineWidth = 1;
			qPrognozSerie.SerieType = ChartSerieType.line;
			qPrognozSerie.Title = "Q прогноз";
			qPrognozSerie.TagName = "QPrognoz";
			qPrognozSerie.Enabled = false;
			qPrognozSerie.YAxisIndex = 2;

			ChartSerieProperties vbSerie=new ChartSerieProperties();
			vbSerie.Color = "0-255-255";
			vbSerie.LineWidth = 2;
			vbSerie.SerieType = ChartSerieType.line;
			vbSerie.Title = "ВБ факт";
			vbSerie.TagName = "VB";
			vbSerie.Enabled = false;
			vbSerie.YAxisIndex = 3;

			ChartSerieProperties vbPrognozSerie = new ChartSerieProperties();
			vbPrognozSerie.Color = "0-255-255";
			vbPrognozSerie.LineWidth = 1;
			vbPrognozSerie.SerieType = ChartSerieType.line;
			vbPrognozSerie.Title = "ВБ прогноз";
			vbPrognozSerie.TagName = "VBPrognoz";
			vbPrognozSerie.Enabled = false;
			vbPrognozSerie.YAxisIndex = 3;

			ChartSerieProperties naporSerie=new ChartSerieProperties();
			naporSerie.Color = "255-0-255";
			naporSerie.LineWidth = 2;
			naporSerie.SerieType = ChartSerieType.line;
			naporSerie.Title = "Напор";
			naporSerie.TagName = "Napor";
			naporSerie.Enabled = false;
			naporSerie.YAxisIndex = 4;

			ChartSerieProperties naporPrognozSerie=new ChartSerieProperties();
			naporPrognozSerie.Color = "255-0-255";
			naporPrognozSerie.LineWidth = 1;
			naporPrognozSerie.SerieType = ChartSerieType.line;
			naporPrognozSerie.Title = "Напор прогноз";
			naporPrognozSerie.TagName = "NaporPrognoz";
			naporPrognozSerie.Enabled = false;
			naporPrognozSerie.YAxisIndex = 4;

			ChartSerieProperties tSerie=new ChartSerieProperties();
			tSerie.Color = "120-0-255";
			tSerie.LineWidth = 2;
			tSerie.SerieType = ChartSerieType.line;
			tSerie.Title = "Температура";
			tSerie.TagName = "T";
			tSerie.Enabled = false;
			tSerie.YAxisIndex = 5;
			

			props.addAxis(pAx);
			props.addAxis(nbAx);
			props.addAxis(qAx);
			props.addAxis(vbAx);
			props.addAxis(naporAx);
			props.addAxis(tAx);

			props.addSerie(pSerie);
			props.addSerie(pbrSerie);
			props.addSerie(nbFaktSerie);
			props.addSerie(nbPrognozSerie);
			props.addSerie(qFaktSerie);
			props.addSerie(qPrognozSerie);
			props.addSerie(vbSerie);
			props.addSerie(vbPrognozSerie);
			props.addSerie(naporSerie);
			props.addSerie(naporPrognozSerie);
			props.addSerie(tSerie);

			props.XAxisType = XAxisTypeEnum.datetime;

			return props;
		}
	}
}
