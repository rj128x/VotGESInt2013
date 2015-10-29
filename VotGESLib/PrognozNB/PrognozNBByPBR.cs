using System;
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
		public string Avail { get; set; }
		public double KPD { get; set; }
		public double Q { get; set; }
		public double P { get; set; }
		public double Napor { get; set; }
		public double UdRash { get; set; }
		public double VB { get; set; }
		public double NB { get; set; }
		public double PMax { get; set; }
		public double NUMax { get; set; }

	}

	public class PrognozNBInitData {
		public DateTime Date { get; set; }
		public double Pritok0 { get; set; }
		public double Pritok1 { get; set; }

		public double T1 { get; set; }
		public double T2 { get; set; }
		public double T3 { get; set; }
		public double T4 { get; set; }
		public double T5 { get; set; }
		public double T6 { get; set; }
		public double T7 { get; set; }
		public double T8 { get; set; }

		public double Davl1 { get; set; }
		public double Davl2 { get; set; }
		public double Davl3 { get; set; }

		public double Vl1 { get; set; }
		public double Vl2 { get; set; }
		public double Vl3 { get; set; }

		public double Koeff { get; set; }

		public bool UseInitData { get; set; }

		public double Podpor1 { get; set; }
		public double Podpor2 { get; set; }
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
		public PrognozNBInitData InitData { get; set; }
	}

	public class PrognozNBByPBR : PrognozNBFunc
	{
		public bool QMax { get; set; }

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

			DatePrognozStart = DatePrognozStart.Date.AddHours(hour);
			//DatePrognozStart = DateStart.Date;
			UserPBR = userPBR;


		}

		public void readPBRPrevSutki() {
			DateTime ds=DatePrognozStart;
			DateTime de=DatePrognozStart.AddDays(-1);
			List<PiramidaEnrty> dataArr=PiramidaAccess.GetDataFromDB(DateStart, DateEnd, 0, 2, 212, (new int[] { 1 }).ToList<int>(), true, true);
			foreach (PiramidaEnrty data in dataArr) {
				if (data.Date.Minute == 0) {
					if (!pbrPrevSutki.Keys.Contains(data.Date)) {
						pbrPrevSutki.Add(data.Date, data.Value0 / 1000);
					}
				}
			}
		}

		public void preparePArr() {
			pbrFull = new SortedList<DateTime, double>();
			DateTime date=DateStart.AddMinutes(0);
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
					answer.VyrabFakt += PFakt[date];
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
				values[dt].Vyrab += Prognoz.PArr[currentDate];
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
					}
					if (!Avail[rec.Date].Contains(rec.Item) && rec.Value0 > 0) {
						Avail[rec.Date].Add(rec.Item);
					}
				}
			}
			catch {}

			List<int>prevAvail=null;
			foreach (KeyValuePair<DateTime, double> ke in Prognoz.PArr) {
				try {					
					PrognozRusaData dat = new PrognozRusaData();
					double p = ke.Value;
					double napor = Prognoz.Napors[ke.Key];
					List<int> sostav = new List<int>();
					List<int> avail = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
					if (!Avail.ContainsKey(ke.Key)) {
						if (prevAvail != null)
							avail = prevAvail;
					}
					else {
						avail = Avail[ke.Key];
					}
					prevAvail = avail;
					
					//Logger.Info(String.Format("{0}: {1}", ke.Key, String.Join(",", avail)));
					double q = RUSA.getOptimRashod(p, napor, !QMax, sostav, avail);
					sostav.Sort();
					string str = String.Format("{0}: {1} (Расход: {2} КПД: {3})", ke.Key, String.Join(",", sostav), q, 0);
					dat.Date = ke.Key;
					dat.KPD = RashodTable.KPD(p, napor, q)*100;
					dat.Sostav = String.Join(",", sostav);
					dat.Avail = String.Join(",", avail);
					dat.VB = Prognoz.PrognozVB[ke.Key];
					dat.NB = Prognoz.Prognoz[ke.Key];
					dat.UdRash = q/p;
					dat.Q = q;
					dat.P = p;
					dat.Napor = napor;
					double state=0;
					for (int ga=1;ga<=10;ga++){
						if (avail.Contains(ga)) {
							state += Math.Pow(2, ga - 1);
						}
					}
					dat.PMax = PrognozNBFunc.p_max(dat.Napor, (int)state);
					dat.NUMax = PrognozNBFunc.nu_max(dat.Napor);
					//dat.PMax=PrognozNBFunc.p_max(dat.Napor)
					answer.RUSA.Add(dat);
				}
				catch (Exception e) {
					//Logger.Info(e.ToString());
				}
			}
		}

		public void startPrognoz(PrognozNBInitData initData) {
			Prognoz = new PrognozNB();

			Prognoz.FirstData = readFirstData(DateStart);
			Prognoz.FirstDataSut = readFirstDataSut(DateStart);
			readP();
			readPBR();
			readWater();
			preparePArr();
			checkData(DateStart, DatePrognozStart);

			Prognoz.DatePrognozStart = DateStart;
			Prognoz.DatePrognozEnd = DateEnd;
			Prognoz.PArr = new SortedList<DateTime, double>();
			Prognoz.IsQFakt = false;
			Prognoz.QMax = QMax;
			Prognoz.InitData = initData==null?new PrognozNBInitData():initData;
			bool isFirst=true;
			double prev=0;
			foreach (KeyValuePair<DateTime,double> de in PBRFull) {
				if (!isFirst) {
					Prognoz.PArr.Add(de.Key, (de.Value + prev) / 2);
				}
				prev = de.Value;
				isFirst = false;
			}
			foreach (KeyValuePair<DateTime, double> de in PFakt) {
				if (Prognoz.PArr.ContainsKey(de.Key))
					Prognoz.PArr[de.Key] = PFakt[de.Key];
			}
			//prognoz.calcPrognoz(correct);
			Prognoz.calcPrognozNeW();
			processAnswer();
			PrognozAnswer.InitData = Prognoz.InitData;
			
			

			/*Prognoz.Prognoz.Add(DatePrognozStart, Prognoz.FirstData.Last().Value.NB);
			Prognoz.Rashods.Add(DatePrognozStart, Prognoz.FirstData.Last().Value.Q);
			Prognoz.Napors.Add(DatePrognozStart, Prognoz.FirstData.Last().Value.VB - Prognoz.FirstData.Last().Value.NB);*/
			PrognozAnswer.Chart = getChart();
			PrognozAnswer.Chart.processAxes();
		}
	}
}
