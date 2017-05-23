using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AxScdSys;
using VotGES;
using System.Threading;

namespace KotmiLib
{
	[Serializable]
	public class ArcField
	{
		public int ID { get; set; }
		public bool PTI { get; set; }
		public string Code { get; set; }
		public string Name { get; set; }
		public bool Sel { get; set; }

		public ArcField(string name) {
			string[] arr = name.Split('_');
			ID = Int32.Parse(arr[1]);
			PTI = arr[0] == "PTI";
			Code = name;

		}

		public ArcField() {

		}
	}

	[Serializable]
	public class KotmiResult
	{
		public DateTime DateStart { get; set; }
		public DateTime DateEnd { get; set; }

		public string Mode { get; set; }

		public List<DateTime> Dates { get; set; }
		public Dictionary<ArcField,SortedList<DateTime,double>> Values { get; set; }

		public int StepSeconds { get; set; }

		public List<ArcField> Fields { get; set; }

		public KotmiResult() {

		}

		public KotmiResult(DateTime dateStart,DateTime dateEnd, List<ArcField> fields, int stepSeconds, string mode) {
			Mode = mode;
			DateStart = dateStart;
			DateEnd = dateEnd;
			StepSeconds = stepSeconds;
			Fields = fields;			
			Values = new Dictionary<ArcField, SortedList<DateTime, double>>();
			Dates = new List<DateTime>();
			foreach (ArcField field in Fields) {
				Values.Add(field, new SortedList<DateTime, double>());
				DateTime date = DateStart.AddHours(0);
				while (date <= DateEnd) {
					Dates.Add(date);
					Values[field].Add(date, 0);
					if (mode == "HH")
						date = date.AddMinutes(30);
					else
						date = date.AddSeconds(StepSeconds);
				}
				
			}
		}

		public void ReadData() {
			KotmiClass Kotmi = new KotmiClass();
			foreach (ArcField field in Fields) {
				Logger.Info(String.Format("{0} - {1}", DateStart, DateEnd));
				Kotmi.ReadVals(DateStart, DateEnd, field, StepSeconds);
				SortedList<DateTime, double> Data = Kotmi.FullData;
				DateTime prevDate = DateStart;
				double sum = 0;
				double cnt = 1;
				foreach (KeyValuePair<DateTime, double> de in Data) {
					DateTime date = Dates.First(d => d >= de.Key);
					if (Mode == "HH") {
						if (date > prevDate) {
							Values[field][prevDate] = sum / cnt;
							prevDate = date.AddHours(0) ;
							sum = 0;
							cnt = 1;
						} 
						sum += de.Value;
						cnt++;
					} else {						
						Values[field][date] = de.Value;
					}
				}
			}
		}


	}

	public delegate void OnFinishReadDelegate(SortedList<DateTime, double> Data);
	public class KotmiClass
	{
		public AxScadaCli Client { get; protected set; }
		public AxScadaAbo Abo { get; set; }
		protected SortedList<DateTime, double> Data;
		protected SortedList<DateTime, double> TempData;
		protected List<DateTime> SentData;
		public SortedList<DateTime, double> FullData;
		public OnFinishReadDelegate OnFinishRead;
		protected bool AllSent = false;
		protected bool Break = false;

		public KotmiClass() {
			CreateClients();
		}

		protected void CreateClients() {
			Form Form1 = new Form();

			//System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(KotmiClass));
			AxScadaCli cli = new AxScadaCli();
			AxScadaAbo abo = new AxScadaAbo();

			((System.ComponentModel.ISupportInitialize)(cli)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(abo)).BeginInit();
			//cli.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("cli.OcxState")));
			//abo.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("abo.OcxState")));
			Form1.Controls.Add(cli);
			Form1.Controls.Add(abo);
			((System.ComponentModel.ISupportInitialize)(cli)).EndInit();
			((System.ComponentModel.ISupportInitialize)(abo)).EndInit();


			this.Client = cli;
			this.Abo = abo;
			abo.CliHWnd = cli.hWnd;
			initEvents();
		}

		protected void initEvents() {
			Abo.OnRowValue += Abo_OnRowValue;
			Abo.OnBlockEnd += Abo_OnBlockEnd;
			Abo.OnBlockBegin += Abo_OnBlockBegin;
		}

		private void Abo_OnBlockBegin(object sender, EventArgs e) {

		}

		private void Abo_OnBlockEnd(object sender, IScadaAboEvents_OnBlockEndEvent e) {
			//Logger.Info("Завершение блока");
			processFullData();

			if ((Break || AllSent) && OnFinishRead != null) {
				//Logger.Info("Завершение запроса");
				OnFinishRead(FullData);
			}
		}

		private void Abo_OnRowValue(object sender, IScadaAboEvents_OnRowValueEvent e) {
			Object dt = e.recData.FieldValue["DT"];
			Object dt2 = Client.get_TimeSecToOle(Convert.ToInt32(dt));
			DateTime date = Convert.ToDateTime(dt2);
			double val = Convert.ToDouble(e.recData.FieldValue["VAL"]);
			if (!TempData.ContainsKey(date)) {
				TempData.Add(date, val);
			}
		}

		protected bool _Connect() {
			Client.SrvAddress = KOTMISettings.Single.Server;
			Client.UserName = KOTMISettings.Single.User;
			Client.UserPassword = KOTMISettings.Single.Password;
			Client.ReconnectAuto = true;
			Client.Open();
			return Client.CliActive;
		}

		public bool Connect() {
			if (!Client.CliActive)
				return _Connect();
			return ISConnected();
		}

		public bool ISConnected() {
			return Client.CliActive;
		}

		public void ReadVals(DateTime dateStart, DateTime dateEnd, ArcField field, int stepSeconds) {
			if (Connect()) {
				Break = false;
				Data = new SortedList<DateTime, double>();
				FullData = new SortedList<DateTime, double>();
				bool needStart = true;
				AllSent = false;

				int cnt = 0;
				DateTime date = dateStart.AddHours(0);
				DateTime dateS = date.AddHours(0);

				while (date <= dateEnd && !Break && !AllSent) {
					if (needStart) {
						dateS = date.AddHours(0);
						//Logger.Info(String.Format("Старт блока дата [{0}]", date));
						Abo.BlockBegin();
						TempData = new SortedList<DateTime, double>();
						SentData = new List<DateTime>();
						needStart = false;
					}
					cnt++;
					Abo.RequestPrmSet("PROC", "READ_ARCH");
					Abo.RequestPrmSet("TABLE_NAME", field.PTI ? "T_ARCH_PTI" : "T_ARCH_TI");
					Abo.Proc();

					Abo.FieldValue("ID", ScdSys.EFieldType.eftInt, field.ID);
					int time = Client.get_TimeOleToSec(date);
					Abo.FieldValue("DT", ScdSys.EFieldType.eftUnixDT, time);
					SentData.Add(date);
					date = date.AddSeconds(stepSeconds);
					Abo.Post();

					if (Break || cnt == 10000 || date > dateEnd) {
						AllSent = date >= dateEnd;
						cnt = 0;
						needStart = true;
						//Logger.Info(String.Format("Отправка запроса по измерению {0} [{1}]-[{2}]", field.Code, dateS, date));
						Abo.BlockEnd(true, true);
					}
				}
			}
		}

		protected void processFullData() {
			List<DateTime> missedDates = new List<DateTime>();
			foreach (DateTime date in SentData) {
				try {
					DateTime dt1 = TempData.Keys.Last(d => d <= date);
					FullData.Add(date, TempData[dt1]);
				} catch (Exception e) {
					missedDates.Add(date);
					FullData.Add(date, double.NaN);
				}
			}
			if (missedDates.Count > 0) {
				Logger.Info("Пропущены даты: " + string.Join(", ", missedDates));
			}
		}

		public void Close() {
			try {
				Client.Close();
			} catch {

			}
		}

		public void BreakRead() {
			Logger.Info("Прерывание запроса");
			Break = true;
		}

		

	}
}
