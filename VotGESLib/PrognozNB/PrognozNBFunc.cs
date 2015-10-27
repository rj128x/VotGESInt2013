using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VotGES.Piramida;
using VotGES.Chart;
using System.Runtime.InteropServices;

namespace VotGES.PrognozNB
{
	public class PrognozNBFunc
	{
		[DllImport("C:/dc_votges/dc_votges.dll",EntryPoint = "predict_ub")]
		public static extern double predict_ub(String iv);

		[DllImport("C:/dc_votges/dc_votges.dll", EntryPoint = "predict_nb")]
		public static extern double predict_nb(String iv);

		[DllImport("C:/dc_votges/dc_votges.dll", EntryPoint = "p_max")]
		public static extern double p_max(double h, int n);

		[DllImport("C:/dc_votges/dc_votges.dll", EntryPoint = "nu_max")]
		public static extern double nu_max(double h);
				
		
		protected DateTime dateStart;
		public DateTime DateStart {get;set;}

		public DateTime DateEnd {get;set;}
				
		public DateTime DatePrognozStart {get;set;}

		
		public int DaysCount {get;set;}
				
		public SortedList<DateTime, double> PBR {get;set;} 

		public SortedList<DateTime, double> NBFakt{get;set;}
				
		public SortedList<DateTime, double> NaporFakt{get;set;}
				
		public SortedList<DateTime, double> VBFakt{get;set;}
				
		public SortedList<DateTime, double> QFakt{get;set;}
				
		public SortedList<DateTime, double> TFakt{get;set;}
				
		public SortedList<DateTime, double> PFakt{get;set;}
				
		public double T {get;set;}
				
		public PrognozNB Prognoz{get;set;}

		
		public PrognozNBFunc(DateTime dateStart, int daysCount) {
			DateStart = dateStart.Date;
			DaysCount = daysCount;
			DateEnd = DateStart.AddDays(daysCount);
			PBR = new SortedList<DateTime, double>();
			NBFakt = new SortedList<DateTime, double>();
			VBFakt = new SortedList<DateTime, double>();
			QFakt = new SortedList<DateTime, double>();
			PFakt = new SortedList<DateTime, double>();
			TFakt = new SortedList<DateTime, double>();
			NaporFakt = new SortedList<DateTime, double>();
		}



		public void readPBR() {
			List<PiramidaEnrty> dataArr=PiramidaAccess.GetDataFromDB(DateStart, DateEnd, 0, 2, 212, (new int[] { 1 }).ToList<int>(), false, true);
			foreach (PiramidaEnrty data in dataArr) {
				if (!PBR.Keys.Contains(data.Date)) {
					if (data.Date.Minute==0)
						PBR.Add(data.Date, data.Value0 / 1000);
				}
			}
		}

		public void readP() {
			List<PiramidaEnrty> dataArr=PiramidaAccess.GetDataFromDB(DateStart.AddMinutes(-30), DateEnd, 0, 2, 12, (new int[] { 1 }).ToList<int>(), false, true);
			foreach (PiramidaEnrty data in dataArr) {
				DateTime dt = data.Date.Minute > 0 ? data.Date.AddMinutes(30) : data.Date;
				if (!PFakt.Keys.Contains(dt)) {
					PFakt.Add(dt, 0);
				}
				PFakt[dt] += data.Value0 / 2 / 1000;
			}
		}

		public void readWater() {
			int[] items=new int[] { 354, 276,373,275,274 };
			List<int> il=items.ToList();			
			List<PiramidaEnrty> dataArr=PiramidaAccess.GetDataFromDB(DateStart.AddMinutes(-30), DateEnd, 1, 2, 12, il, true, true);

			foreach (PiramidaEnrty data in dataArr) {
				DateTime dt = data.Date.Minute > 0 ? data.Date.AddMinutes(30) : data.Date;
				switch (data.Item) {
					case 354:
						if (!QFakt.Keys.Contains(dt)) {
							QFakt.Add(dt, 0);
						}
						QFakt[dt] += data.Value0 / 2;
						break;
					case 276:
						if (!NaporFakt.Keys.Contains(dt)) {
							NaporFakt.Add(dt, 0);
						}
						NaporFakt[dt] += data.Value0 / 2;
						break;
					case 275:
						if (!NBFakt.Keys.Contains(dt)) {
							NBFakt.Add(dt, 0);
						}
						NBFakt[dt] += data.Value0 / 2;
						break;
					case 274:
						if (!VBFakt.Keys.Contains(dt)) {
							VBFakt.Add(dt, 0);
						}
						VBFakt[dt] += data.Value0 / 2;
						break;
					case 373:
						if (!TFakt.Keys.Contains(dt)) {
							TFakt.Add(dt, 0);
						}
						TFakt[dt] += data.Value0 / 2;
						break;
				}
			}
		}

		public void checkData(DateTime dateStart, DateTime dateEnd) {
			DateTime date=dateStart.AddMinutes(60);
			while (date <= dateEnd) {
				if (!NBFakt.Keys.Contains(date) || NBFakt[date] ==0) {
					if (NBFakt.Keys.Contains(date))
						NBFakt.Remove(date);
					if (NBFakt.Keys.Contains(date.AddMinutes(-60)))
						NBFakt.Add(date, NBFakt[date.AddMinutes(-60)]);
					else NBFakt.Add(date, 66);
				}
				if (NBFakt[date] < 60)
					NBFakt[date] = NBFakt[date]*2;
								
				if (!VBFakt.Keys.Contains(date) || VBFakt[date] == 0) {
					if (VBFakt.Keys.Contains(date))
						VBFakt.Remove(date);
					if (VBFakt.Keys.Contains(date.AddMinutes(-60)))
						VBFakt.Add(date, VBFakt[date.AddMinutes(-60)]);
					else VBFakt.Add(date, 87);
				}
				if (VBFakt[date] < 80)
					VBFakt[date] *= 2;

				
				if (!NaporFakt.Keys.Contains(date) || NaporFakt[date] == 0) {
					if (NaporFakt.Keys.Contains(date))
						NaporFakt.Remove(date);
					NaporFakt.Add(date, VBFakt[date] - NBFakt[date]);
				}
				if (NaporFakt[date] < 15)
					NaporFakt[date] *= 2;


				if (!PFakt.Keys.Contains(date)) {
					if (PFakt.Keys.Contains(date.AddMinutes(-60)))
						PFakt.Add(date, PFakt[date.AddMinutes(-60)]);
					else PFakt.Add(date, 100);
				}
				if (!QFakt.Keys.Contains(date) || (QFakt[date] == 0 && PFakt[date] > 0)) {
					if (QFakt.Keys.Contains(date))
						QFakt.Remove(date);
					QFakt.Add(date, RashodTable.getStationRashod(PFakt[date], NaporFakt[date], RashodCalcMode.avg));
				}
				date = date.AddMinutes(60);
			}
		}

		public virtual  SortedList<DateTime,PrognozNBFirstData> readFirstData(DateTime date) {
			
			int[] items=new int[] { 354, 276, 373, 275, 274 };
			List<int> il=items.ToList();
			DateTime ds=date.AddHours(-12);
			DateTime de=date.AddHours(0);
			List<PiramidaEnrty> dataArrW = PiramidaAccess.GetDataFromDB(ds.AddMinutes(-30), de, 1, 2, 12, il, true, true);
			List<PiramidaEnrty> dataArrP = PiramidaAccess.GetDataFromDB(ds.AddMinutes(-30), de, 0, 2, 12, (new int[] { 1 }).ToList<int>(), true, true);
			List<PiramidaEnrty> dataArr = new List<PiramidaEnrty>();
			foreach (PiramidaEnrty entry in dataArrW) {
				dataArr.Add(entry);
			}
			foreach (PiramidaEnrty entry in dataArrP) {
				dataArr.Add(entry);
			}

			return processFirstData(dataArr);
		}

		public virtual SortedList<DateTime, PrognozNBFirstData> readFirstDataSut(DateTime date) {

			int[] items = new int[] {373, };
			int[] itemsSut = new int[] { 24 };
			List<int> il = items.ToList();
			DateTime ds = date.Date.AddDays(-8);
			DateTime de = date.Date.AddHours(0);
			List<PiramidaEnrty> dataArrW = PiramidaAccess.GetDataFromDB(ds, de, 1, 2, 26, il, true, true);
			List<PiramidaEnrty> dataArrSut = PiramidaAccess.GetDataFromDB(ds,de, 7, 2, 26, itemsSut.ToList(), true, true);
			List<PiramidaEnrty> dataArr = new List<PiramidaEnrty>();
			
			foreach (PiramidaEnrty entry in dataArrW) {
				dataArr.Add(entry);
			}

			foreach (PiramidaEnrty entry in dataArrSut) {
				dataArr.Add(entry);
			}
			return processFirstData(dataArr);
		}


		protected SortedList<DateTime, PrognozNBFirstData> processFirstData(List<PiramidaEnrty> dataArr) {
			SortedList<DateTime,PrognozNBFirstData> firstData=new SortedList<DateTime, PrognozNBFirstData>();
			foreach (PiramidaEnrty data in dataArr) {
				DateTime date = data.Date.Minute == 0 ? data.Date : data.Date.AddMinutes(30);
				if (!firstData.Keys.Contains(date)) {
					PrognozNBFirstData newData=new PrognozNBFirstData();
					newData.Date = date;
					firstData.Add(date, newData);
				}
				

				switch (data.Item) {
					case 1:						
						firstData[date].P += data.Value0/2/1000;
						break;
					case 354:
						firstData[date].Q += data.Value0/2;
						break;
					case 275:
						firstData[date].NB += data.Value0/2;
						break;
					case 274:
						firstData[date].VB += data.Value0/2;
						break;
					case 373:
						firstData[date].T = data.Value0;
						break;
					case 24:
						firstData[date].Pritok = data.Value0;
						break;
				}
			}

			foreach (DateTime date in firstData.Keys) {
				if (firstData[date].Q == 0 && firstData[date].P > 0) {
					firstData[date].Q = RashodTable.getStationRashod(firstData[date].P, firstData[date].VB-firstData[date].NB, RashodCalcMode.avg);
				}
			}
			return firstData;
		}

		

		public virtual void writeFaktData(ChartData data) {
			ChartDataSerie nbFaktSerie=new ChartDataSerie();
			nbFaktSerie.Name = "NBFakt";
			foreach (KeyValuePair<DateTime,double> de in NBFakt){
				nbFaktSerie.Points.Add(new ChartDataPoint(de.Key, de.Value));
			}
			data.addSerie(nbFaktSerie);

			ChartDataSerie pFaktSerie=new ChartDataSerie();
			pFaktSerie.Name = "PFakt";
			foreach (KeyValuePair<DateTime,double> de in PFakt) {
				pFaktSerie.Points.Add(new ChartDataPoint(de.Key, de.Value));
			}
			data.addSerie(pFaktSerie);

			ChartDataSerie pbrSerie=new ChartDataSerie();
			pbrSerie.Name = "PBR";
			foreach (KeyValuePair<DateTime,double> de in PBR) {
				pbrSerie.Points.Add(new ChartDataPoint(de.Key, de.Value));
			}
			data.addSerie(pbrSerie);

			ChartDataSerie qFaktSerie=new ChartDataSerie();
			qFaktSerie.Name = "QFakt";
			foreach (KeyValuePair<DateTime,double> de in QFakt) {
				qFaktSerie.Points.Add(new ChartDataPoint(de.Key, de.Value));
			}
			data.addSerie(qFaktSerie);

			ChartDataSerie naporFaktSerie=new ChartDataSerie();
			naporFaktSerie.Name = "Napor";
			foreach (KeyValuePair<DateTime,double> de in NaporFakt) {
				naporFaktSerie.Points.Add(new ChartDataPoint(de.Key, de.Value));
			}
			data.addSerie(naporFaktSerie);

			ChartDataSerie vbFaktSerie=new ChartDataSerie();
			vbFaktSerie.Name = "VB";
			foreach (KeyValuePair<DateTime,double> de in VBFakt) {
				vbFaktSerie.Points.Add(new ChartDataPoint(de.Key, de.Value));
			}
			data.addSerie(vbFaktSerie);

			ChartDataSerie tFaktSerie=new ChartDataSerie();
			tFaktSerie.Name = "T";
			foreach (KeyValuePair<DateTime,double> de in TFakt) {
				tFaktSerie.Points.Add(new ChartDataPoint(de.Key, de.Value));
			}
			data.addSerie(tFaktSerie);

			

			
		}

		public virtual ChartAnswer getChart() {
			ChartAnswer answer=new ChartAnswer();
			answer.Data = new ChartData();
			writeFaktData(answer.Data);
			Prognoz.AddChartData(answer.Data);
			answer.Properties = Prognoz.createChartProperties();
			return answer;
		}
	}
}
