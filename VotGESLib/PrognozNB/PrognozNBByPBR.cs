﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VotGES.Piramida;
using VotGES.Chart;
using VotGES.Rashod;

namespace VotGES.PrognozNB
{
	public class PrognozValue
	{
		public DateTime Date { get; set; }
		public double QAvg { get; set; }
		public double Vyrab { get; set; }
		public double NBAvg { get; set; }
		public double NBMin { get; set; }
		public double NBMax { get; set; }
	}

	public class PrognozRusaData {
		public DateTime Date { get; set; }
		public string Sostav { get; set; }
		public double KPD { get; set; }
		public double Q { get; set; }
		public double P { get; set; }
		public double Napor { get; set; }
		public double UdRash { get; set; }
	}


	public class PrognozNBByPBRAnswer
	{

		public ChartAnswer Chart { get; set; }
		public double VyrabFakt { get; set; }
		public double QFakt { get; set; }
		public double NBAvg { get; set; }
		public double NBMin { get; set; }
		public double NBMax { get; set; }
		public List<PrognozValue> PrognozValues{get;set;}
		public List<PrognozRusaData> RUSA{get;set;}
	}

	public class PrognozNBByPBR : PrognozNBFunc
	{
		

		protected SortedList<DateTime,double> userPBR;
		public SortedList<DateTime, double> UserPBR {
			get { return userPBR; }
			set { userPBR = value; }
		}


		protected SortedList<DateTime,double> pbrPrevSutki;
		public SortedList<DateTime, double> PBRPrevSutki {
			get {
				if (pbrPrevSutki == null) {
					pbrPrevSutki = new SortedList<DateTime, double>();
					readPBRPrevSutki();
				}
				return pbrPrevSutki;
			}
			set { pbrPrevSutki = value; }
		}

		protected SortedList<DateTime,double> pbrFull;
		public SortedList<DateTime, double> PBRFull {
			get { return pbrFull; }
			set { pbrFull = value; }
		}

		protected PrognozNBByPBRAnswer prognozAnswer;
		public PrognozNBByPBRAnswer PrognozAnswer {
			get { return prognozAnswer; }
			set { prognozAnswer = value; }
		}


		public PrognozNBByPBR(DateTime dateStart, int daysCount, DateTime datePrognozStart, SortedList<DateTime, double> userPBR)
			: base(dateStart, daysCount) {
			DateStart = dateStart.Date;
			DaysCount = daysCount;
			DateEnd = DateStart.AddDays(daysCount);

			DatePrognozStart = datePrognozStart;
			int hour=DatePrognozStart.Hour;
			int min=DatePrognozStart.Minute;
			min = min < 30 ? 0 : min;
			min = min > 30 ? 30 : min;
			//DatePrognozStart = DatePrognozStart.Date.AddHours(hour).AddMinutes(min);
			DatePrognozStart = DateStart.Date;
			UserPBR = userPBR;


		}

		public void readPBRPrevSutki() {
			DateTime ds=DatePrognozStart;
			DateTime de=DatePrognozStart.AddDays(-1);
			List<PiramidaEnrty> dataArr=PiramidaAccess.GetDataFromDB(DateStart, DateEnd, 0, 2, 212, (new int[] { 1 }).ToList<int>(), true, true);
			foreach (PiramidaEnrty data in dataArr) {
				if (!pbrPrevSutki.Keys.Contains(data.Date)) {
					pbrPrevSutki.Add(data.Date, data.Value0 / 1000);
				}
			}
		}

		public void preparePArr() {
			pbrFull = new SortedList<DateTime, double>();
			DateTime date=DatePrognozStart.AddMinutes(0);
			while (date <= DateEnd) {
				if (userPBR != null && userPBR.Keys.Contains(date)) {
					pbrFull.Add(date, userPBR[date]);
				} else if (PBR.Keys.Contains(date)) {
					pbrFull.Add(date, PBR[date]);
				} else if (pbrFull.Keys.Contains(date.AddHours(-24))) {
					pbrFull.Add(date, pbrFull[date.AddHours(-24)]);
				} else if (PBRPrevSutki.Keys.Contains(date.AddHours(-24))) {
					pbrFull.Add(date, PBRPrevSutki[date.AddHours(-24)]);
				} else if (pbrFull.Keys.Contains(date.AddMinutes(-60))) {
					pbrFull.Add(date, pbrFull[date.AddMinutes(-60)]);
				} else {
					pbrFull.Add(date, 0);
				}
				date = date.AddMinutes(60);
			}

		}

		/*public override SortedList<DateTime, PrognozNBFirstData> readFirstData(DateTime date) {
			date = DatePrognozStart.AddMinutes(0);
			int[] items=new int[] { 354, 276, 373, 275, 274 };
			List<PiramidaEnrty> dataArr=null;
			List<PiramidaEnrty> dataArrP,dataArrW=null;
			List<int> il=items.ToList();
			double cntW=0;
			double cntP=0;
			int index=0;
			while ((cntW < 25 || cntP<5) && index <= 10) {
				DateTime ds=date.AddHours(-2);
				DateTime de=date.AddHours(0);
				dataArrW=PiramidaAccess.GetDataFromDB(ds, de, 1, 2, 12, il, true, true);
				dataArrP = PiramidaAccess.GetDataFromDB(ds, de, 0, 2, 12, (new int[] { 1 }).ToList<int>(), true, true);
				dataArr = new List<PiramidaEnrty>();
				foreach (PiramidaEnrty entry in dataArrW) {
					dataArr.Add(entry);
					cntW++;
				}
				foreach (PiramidaEnrty entry in dataArrP) {
					dataArr.Add(entry);
					cntP++;
				}
				//cnt = dataArr.Count();
				date = date.AddMinutes(-30);
				index++;
			}
			DatePrognozStart = date.AddMinutes(30);
			return processFirstData(dataArr);
		}*/

		public override void writeFaktData(Chart.ChartData data) {
			base.writeFaktData(data);

			ChartDataSerie serie=data.Series[data.SeriesNames["PBR"]];
			serie.Points.Clear();
			foreach (KeyValuePair<DateTime,double> de in PBRFull) {
				serie.Points.Add(new ChartDataPoint(de.Key, de.Value));
			}
		}

		public void processAnswer() {
			PrognozNBByPBRAnswer answer=new PrognozNBByPBRAnswer();
			answer.RUSA = new List<PrognozRusaData>();
			foreach (DateTime date in PFakt.Keys) {
				if (date > DateStart && date <= DatePrognozStart) {
					answer.VyrabFakt += PFakt[date] / 2;
				}
			}

			int countQ=0;
			foreach (DateTime date in QFakt.Keys) {
				if (date > DateStart && date <= DatePrognozStart) {
					answer.QFakt += QFakt[date];
					countQ++;
				}
			}

			answer.NBMin = 100;
			answer.NBMax = 0;
			int countNB=0;
			foreach (DateTime date in NBFakt.Keys) {
				if (date > DateStart && date <= DatePrognozStart) {
					double nb=NBFakt[date];
					answer.NBAvg += nb;
					answer.NBMin = answer.NBMin < nb ? answer.NBMin : nb;
					answer.NBMax = answer.NBMax > nb ? answer.NBMax : nb;
					countNB++;
				}
			}
			
			SortedList<DateTime,PrognozValue> values=new SortedList<DateTime, PrognozValue>();
			DateTime currentDate=DatePrognozStart.AddMinutes(60);
			while (currentDate <= DateEnd) {
				DateTime dt=currentDate.AddMinutes(-60).Date;
				if (!values.Keys.Contains(dt)) {
					values.Add(dt, new PrognozValue());
					values[dt].NBMin = 100;
					values[dt].NBMax = 0;
					values[dt].Date = currentDate;
				}
				double nb= Prognoz.Prognoz[currentDate];
				values[dt].Vyrab += Prognoz.PArr[currentDate]/2;
				values[dt].QAvg += Prognoz.Rashods[currentDate];
				values[dt].NBAvg += nb;
				values[dt].NBMin = values[dt].NBMin < nb ? values[dt].NBMin : nb;
				values[dt].NBMax = values[dt].NBMax > nb ? values[dt].NBMax : nb;
								
				currentDate = currentDate.AddMinutes(60);
			}

			DateTime datePrognoz=DateStart.AddMinutes(60).Date;
			values[datePrognoz].QAvg += answer.QFakt;
			values[datePrognoz].NBAvg += answer.NBAvg;
			values[datePrognoz].Vyrab += answer.VyrabFakt;
			values[datePrognoz].NBMin = answer.NBMin < values[datePrognoz].NBMin ? answer.NBMin : values[datePrognoz].NBMin;
			values[datePrognoz].NBMax = answer.NBMax > values[datePrognoz].NBMax ? answer.NBMax : values[datePrognoz].NBMax;
			
			answer.QFakt /= countQ;
			answer.NBAvg /= countNB;

			foreach (DateTime date in values.Keys) {
				values[date].QAvg /= 24;
				values[date].NBAvg /= 24;
			}

			answer.PrognozValues = values.Values.ToList();
			PrognozAnswer = answer;


			SortedList<DateTime,List<int>>Avail=new SortedList<DateTime,List<int>>();
			try {
				List<int> allGa = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
				List<PiramidaEnrty> m53500 = PiramidaAccess.GetDataFromDB(Prognoz.PArr.Keys.First(), Prognoz.PArr.Keys.Last(), 53500, 2, 212, allGa, true, true, "P3000");
				foreach (PiramidaEnrty rec in m53500) {
					if (!Avail.ContainsKey(rec.Date)) {
						Avail.Add(rec.Date, new List<int>());
						Avail.Add(rec.Date.AddMinutes(-30), new List<int>());
					}
					if (!Avail[rec.Date].Contains(rec.Item) && rec.Value0 > 0) {
						Avail[rec.Date].Add(rec.Item);
						Avail[rec.Date.AddMinutes(-30)].Add(rec.Item);
					}
				}
			}
			catch {}

			foreach (KeyValuePair<DateTime, double> ke in Prognoz.PArr) {
				try {
					
					PrognozRusaData dat = new PrognozRusaData();
					double p = ke.Value;
					double napor = Prognoz.Napors[ke.Key];
					List<int> sostav = new List<int>();
					List<int> avail = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
					try{
						avail=Avail[ke.Key];						
					}catch{};
					//Logger.Info(String.Format("{0}: {1}", ke.Key, String.Join(",", avail)));
					double q = RUSA.getOptimRashod(p, napor, true, sostav, avail);
					sostav.Sort();
					string str = String.Format("{0}: {1} (Расход: {2} КПД: {3})", ke.Key, String.Join(",", sostav), q, 0);
					dat.Date = ke.Key;
					dat.KPD = RashodTable.KPD(p, napor, q)*100;
					dat.Sostav = String.Join(",", sostav);
					dat.UdRash = q/p;
					dat.Q = q;
					dat.P = p;
					dat.Napor = napor;
					answer.RUSA.Add(dat);
				}
				catch (Exception e) {
					//Logger.Info(e.ToString());
				}
			}
			
		}

		public void startPrognoz(bool correct) {
			Prognoz = new PrognozNB();

			Prognoz.FirstData = readFirstData(DatePrognozStart);
			Prognoz.FirstDataSut = readFirstDataSut(DatePrognozStart);
			readP();
			readPBR();
			readWater();
			preparePArr();
			checkData(DateStart, DateEnd);

			Prognoz.DatePrognozStart = DatePrognozStart;
			Prognoz.DatePrognozEnd = DateEnd;
			Prognoz.PArr = new SortedList<DateTime, double>();
			Prognoz.IsQFakt = false;
			bool isFirst=true;
			double prev=0;
			foreach (KeyValuePair<DateTime,double> de in PBRFull) {
				if (!isFirst) {
					Prognoz.PArr.Add(de.Key, (de.Value + prev) / 2);
				}
				prev = de.Value;
				isFirst = false;
			}
			//prognoz.calcPrognoz(correct);
			Prognoz.calcPrognozNeW();
			processAnswer();
			

			Prognoz.Prognoz.Add(DatePrognozStart, Prognoz.FirstData.Last().Value.NB);
			Prognoz.Rashods.Add(DatePrognozStart, Prognoz.FirstData.Last().Value.Q);
			Prognoz.Napors.Add(DatePrognozStart, Prognoz.FirstData.Last().Value.VB - Prognoz.FirstData.Last().Value.NB);
			PrognozAnswer.Chart = getChart();
			PrognozAnswer.Chart.processAxes();
		}
	}
}
