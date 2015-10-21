using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using VotGES.Piramida;

namespace VotGES.PrognozNB {

	[Serializable]
	public class DataRecord {
		public DateTime Date { get; set; }
		public Double NB { get; set; }
		public Double Q { get; set; }
	}

	public class TestPrognozClass {
		public SortedList<DateTime, DataRecord> InitData;
		public TestPrognozClass() {
			
		}

		public void readData() {
			DateTime DateStart = new DateTime(2010, 1, 1);
			DateTime DateEnd = new DateTime(2015, 10, 1);
			List<int> items = new List<int> { 275, 354, 373 };
			InitData = new SortedList<DateTime, DataRecord>();

			DateTime dt = DateStart.AddDays(0);
			while (dt < DateEnd) {
				DateTime de = dt.AddDays(10);
				Logger.Info(String.Format("Read {0} - {1} ", dt, de));
				List<PiramidaEnrty> data = PiramidaAccess.GetDataFromDB(dt, de, 1, 2, 12, items);
				dt = de.AddDays(0);


				foreach (PiramidaEnrty rec in data) {
					DataRecord record = null;
					if (!InitData.ContainsKey(rec.Date)) {
						record = new DataRecord();
						record.Date = rec.Date;
						InitData.Add(rec.Date, record);
					}
					record = InitData[rec.Date];
					switch (rec.Item) {
						case 275:
							record.NB = rec.Value0;
							break;
						case 354:
							record.Q = rec.Value0;
							break;
					}
				}
			}
		}

		public SortedList<double, SortedList<double, List<double>>> Data;
		public SortedList<double, SortedList<double, double>> ResultData;

		public void ProcessData() {
			Data = new SortedList<double, SortedList<double, List<double>>>();
			ResultData = new SortedList<double, SortedList<double, double>>();
			double prevNB = double.NaN;
			double prevNBReal = double.NaN;
			foreach (DataRecord record in InitData.Values) {
				double nb = Math.Round(record.NB * 20) / 20.0;
				if (!Data.ContainsKey(nb)) {
					Data.Add(nb, new SortedList<double, List<double>>());
				}

				if (!Double.IsNaN(prevNB)) {
					double q = Math.Round(record.Q / 50.00) * 50;
					if (!Data[prevNB].ContainsKey(q)) {
						Data[prevNB].Add(q, new List<double>());
					}
					Data[prevNB][q].Add(record.NB - prevNBReal);
				}

				prevNBReal = record.NB;
				prevNB = nb;
			}

			foreach (double nb in Data.Keys) {
				ResultData.Add(nb, new SortedList<double, double>());
				foreach (double q in Data[nb].Keys) {
					double sum = 0;
					double count = 0;			
					foreach (double diff in Data[nb][q]) {
						sum += diff;
						count++;
					}
					double avg = 0;
					try {
						avg = sum / count;
					}
					catch { }
					ResultData[nb].Add(q, avg);
				}
			}
		}

		public void SaveInitData(string FileName) {
			FileStream fs = new FileStream(FileName, FileMode.Create);

			// Construct a BinaryFormatter and use it to serialize the data to the stream.
			BinaryFormatter formatter = new BinaryFormatter();
			try {
				formatter.Serialize(fs, InitData);
			}
			catch (SerializationException e) {
				Logger.Info("Failed to serialize. Reason: " + e.Message);
				throw;
			}
			finally {
				fs.Close();
			}

		}

		public void ReadInitData(string FileName) {
			FileStream fs = new FileStream(FileName, FileMode.Open);
			try {
				BinaryFormatter formatter = new BinaryFormatter();

				// Deserialize the hashtable from the file and 
				// assign the reference to the local variable.
				InitData = (SortedList<DateTime, DataRecord>)formatter.Deserialize(fs);
			}
			catch (SerializationException e) {
				Console.WriteLine("Failed to deserialize. Reason: " + e.Message);
				throw;
			}
			finally {
				fs.Close();
			}
		}



		public void SaveResultData(string FileName) {
			FileStream fs = new FileStream(FileName, FileMode.Create);

			// Construct a BinaryFormatter and use it to serialize the data to the stream.
			BinaryFormatter formatter = new BinaryFormatter();
			try {
				formatter.Serialize(fs, ResultData);
			}
			catch (SerializationException e) {
				Logger.Info("Failed to serialize. Reason: " + e.Message);
				throw;
			}
			finally {
				fs.Close();
			}

		}

		public void ReadResultData(string FileName) {
			FileStream fs = new FileStream(FileName, FileMode.Open);
			try {
				BinaryFormatter formatter = new BinaryFormatter();

				// Deserialize the hashtable from the file and 
				// assign the reference to the local variable.
				ResultData = (SortedList<double, SortedList<double, double>>)formatter.Deserialize(fs);
			}
			catch (SerializationException e) {
				Console.WriteLine("Failed to deserialize. Reason: " + e.Message);
				throw;
			}
			finally {
				fs.Close();
			}
		}


		public double getPrognoz(double nbPrev, double q) {
			double nb1 = Double.NaN;
			double nb2 = Double.NaN;
			nb1 = ResultData.Keys.First((double n) => { return n >= nbPrev; });
			nb2 = ResultData.Keys.Last((double n) => { return n <= nbPrev; });

			double diff1 = double.NaN;
			try {
				double q11 = double.NaN;
				double q12 = double.NaN;
				q11 = ResultData[nb1].Keys.First((double x) => { return x >= q; });
				q12 = ResultData[nb1].Keys.Last((double x) => { return x <= q; });

				double k = (q - q11) / (q12 - q11);
				k = double.IsNaN(k) ? 0 : k;
								
				diff1 = q11+k * (ResultData[nb1][q12] - ResultData[nb2][q11]);
			}
			catch { }
			double diff2 = double.NaN;
			try {
				double q21 = double.NaN;
				double q22 = double.NaN;
				q21 = ResultData[nb2].Keys.First((double x) => { return x >= q; });
				q22 = ResultData[nb2].Keys.Last((double x) => { return x <= q; });

				double k = (q - q21) / (q22 - q21);
				k = double.IsNaN(k) ? 0 : k;

				diff2 =q21+k * (ResultData[nb2][q22] - ResultData[nb2][q21]);
			}
			catch { }

			double kn = (nbPrev - nb1) / (nb2 - nb1);
			kn = double.IsNaN(kn) ? 0 : kn;

			double nb =nb1+kn * (diff2 - diff1);
			return nb;
		}

		public void TestData() {
			DataRecord prevRecord = null;
			DateTime DateStart = new DateTime(2015, 1, 1);
			DateTime DateEnd = new DateTime(2015, 10, 1);

			foreach (DataRecord record in InitData.Values) {
				if (prevRecord != null) {
					double calcNB = getPrognoz(prevRecord.NB, record.Q);

					OutputData.writeToOutput("test_02", String.Format("{0};{1}",record.NB, calcNB));
				}

				prevRecord = record;
			}
		}
	}
}
