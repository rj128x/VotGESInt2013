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
		public string PiramidaCode { get; set; }
		public bool Sel { get; set; }

		public ArcField(string name, string pCode = "") {
			string[] arr = name.Split('_');
			ID = Int32.Parse(arr[1]);
			PTI = arr[0] == "PTI";
			Code = name;
			PiramidaCode = pCode;

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
		public List<DateTime> MinDates { get; set; }
		public Dictionary<ArcField, SortedList<DateTime, double>> Values { get; set; }
		public Dictionary<ArcField, SortedList<DateTime, double>> NegValues { get; set; }
		public Dictionary<ArcField, SortedList<DateTime, double>> PosValues { get; set; }
		public Dictionary<ArcField, SortedList<DateTime, double>> MinValues { get; set; }
		public bool NegPos { get; set; }
		public bool Minutes { get; set; }

		public int StepSeconds { get; set; }

		public List<ArcField> Fields { get; set; }

		public KotmiResult() {

		}

		public KotmiResult(DateTime dateStart, DateTime dateEnd, List<ArcField> fields, int stepSeconds, string mode, bool negPos, bool minutes = false) {
			Mode = mode;
			DateStart = dateStart;
			DateEnd = dateEnd;
			StepSeconds = stepSeconds;
			Fields = fields;
			NegPos = negPos;
			Minutes = minutes;
			if (mode != "HH")
				NegPos = false;
			Values = new Dictionary<ArcField, SortedList<DateTime, double>>();
			MinValues = new Dictionary<ArcField, SortedList<DateTime, double>>();
			if (NegPos) {
				NegValues = new Dictionary<ArcField, SortedList<DateTime, double>>();
				PosValues = new Dictionary<ArcField, SortedList<DateTime, double>>();
			}
			Dates = new List<DateTime>();
			if (Minutes) {
				MinDates = new List<DateTime>();
			}
			foreach (ArcField field in Fields) {
				Values.Add(field, new SortedList<DateTime, double>());
				if (NegPos) {
					NegValues.Add(field, new SortedList<DateTime, double>());
					PosValues.Add(field, new SortedList<DateTime, double>());
				}
				DateTime date = DateStart.AddMinutes(mode == "HH" ? 30 : 0);
				while (date <= DateEnd) {
					if (!Dates.Contains(date))
						Dates.Add(date);
					Values[field].Add(date, 0);
					if (NegPos) {
						NegValues[field].Add(date, 0);
						PosValues[field].Add(date, 0);
					}
					if (mode == "HH")
						date = date.AddMinutes(30);
					else
						date = date.AddSeconds(StepSeconds);
				}
			}

			if (Minutes) {
				foreach (ArcField field in Fields) {
					MinValues.Add(field, new SortedList<DateTime, double>());
					DateTime date = DateStart.AddMinutes(1);
					while (date <= DateEnd) {
						if (!MinDates.Contains(date))
							MinDates.Add(date);
						MinValues[field].Add(date, 0);
						date = date.AddMinutes(1);
					}
				}
			}
		}

		public void ReadData() {
			KotmiClass Kotmi = new KotmiClass();
			foreach (ArcField field in Fields) {
				Logger.Info(String.Format("{0} - {1}", DateStart, DateEnd));
				Kotmi.ReadVals(DateStart, DateEnd, field, StepSeconds);
				SortedList<DateTime, double> Data = Kotmi.FullData;
				Dictionary<DateTime, double> CNTS = new Dictionary<DateTime, double>();
				Dictionary<DateTime, double> CNTSMin = new Dictionary<DateTime, double>();
				foreach (KeyValuePair<DateTime, double> de in Data) {
					DateTime date = Dates.First(d => d >= de.Key);

					if (Mode == "HH") {
						Values[field][date] += de.Value;
						if (!CNTS.ContainsKey(date))
							CNTS.Add(date, 0);
						CNTS[date]++;

						if (Minutes) {
							DateTime minDate = MinDates.First(d => d >= de.Key);
							MinValues[field][minDate] += de.Value;
							if (!CNTSMin.ContainsKey(minDate))
								CNTSMin.Add(minDate, 0);
							CNTSMin[minDate]++;
						}

						if (NegPos) {
							if (de.Value > 0) {
								PosValues[field][date] += de.Value;

							}
							if (de.Value < 0) {
								NegValues[field][date] += de.Value;

							}
						}
					} else {
						Values[field][date] = de.Value;
					}
				}

				if (Mode == "HH") {
					foreach (KeyValuePair<DateTime, double> de in CNTS) {
						Values[field][de.Key] /= de.Value;
					}
					if (NegPos) {
						foreach (KeyValuePair<DateTime, double> de in PosValues[field]) {
							PosValues[field][de.Key] = PosValues[field][de.Key] / CNTS[de.Key];
						}
						foreach (KeyValuePair<DateTime, double> de in NegValues[field]) {
							NegValues[field][de.Key] = NegValues[field][de.Key] / CNTS[de.Key];
						}
					}
				}
				if (Minutes) {
					foreach (KeyValuePair<DateTime, double> de in CNTSMin) {
						MinValues[field][de.Key] /= de.Value;
					}
				}

			}
			Kotmi.Close();
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
			_Connect();
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
			Client.ReconnectAuto = false;
			Client.Open();
			return Client.CliActive;
		}

		public bool Connect() {
			//Logger.Info("Check connect");
			if (!Client.CliActive) {
				Logger.Info("reconnect");
				Client.Open();

			}
			return Client.CliActive; ;
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
