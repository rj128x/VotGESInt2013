using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using VotGES;
using VotGES.XMLSer;

namespace ModbusLib.MBLostData {
	public class MBLostFileReader {
		public string FileName;
		protected TextReader Reader { get; set; }
		public SortedList<string, ModbusInitDataArray> InitArrays { get; protected set; }
		public ModbusInitDataArray InitCalc { get; protected set; }
		public SortedList<string, ModbusDataReader> Readers { get; protected set; }
		public SortedList<string, ModbusDataWriter> WritersHH { get; protected set; }
		public SortedList<string, ModbusDataWriter> WritersMin { get; protected set; }
		public List<string> ResultKeys { get; protected set; }
		public SortedList<string, double> FullResultData { get; protected set; }
		public ModbusCalc Calc { get; protected set; }

		public void init() {
			Reader = new StreamReader(System.IO.File.OpenRead(FileName));
			InitArrays = new SortedList<string, ModbusInitDataArray>();
			Readers = new SortedList<string, ModbusDataReader>();
			WritersHH = new SortedList<string, ModbusDataWriter>();
			WritersMin = new SortedList<string, ModbusDataWriter>();
			FullResultData = new SortedList<string, double>();
			ResultKeys = new List<string>();
			foreach (string fileName in Settings.single.InitFiles) {
				try {
					Logger.Info(String.Format("Чтение настроек modbus из файла '{0}'", fileName));
					ModbusInitDataArray arr = XMLSer<ModbusInitDataArray>.fromXML(fileName);
					arr.processData();
					InitArrays.Add(arr.ID, arr);
					String.Format("===Считано {0} записей", arr.FullData.Count);


					if (arr.WriteMin) {
						Logger.Info(String.Format("Создание объекта записи данных в файл (минуты)"));
						ModbusDataWriter writer = new ModbusDataWriter(arr, RWModeEnum.min);
						WritersMin.Add(arr.ID, writer);
						String.Format("===Объект создан");
					}

					if (arr.WriteHH) {
						Logger.Info(String.Format("Создание объекта записи данных в файл (получасовки)"));
						ModbusDataWriter writer = new ModbusDataWriter(arr, RWModeEnum.hh);
						WritersHH.Add(arr.ID, writer);
						String.Format("===Объект создан");
					}

					foreach (KeyValuePair<string, ModbusInitData> de in arr.FullData) {
						FullResultData.Add(arr.ID + "_" + de.Value.ID, 0);
						ResultKeys.Add(arr.ID + "_" + de.Value.ID);
					}

				} catch (Exception e) {
					String.Format("===Ошибка при чтении настроек");
					Logger.Error(e.ToString());
				}
			}

			try {
				Logger.Info(String.Format("Чтение настроек modbus из файла '{0}'", Settings.single.InitCalcFile));
				InitCalc = XMLSer<ModbusInitDataArray>.fromXML(Settings.single.InitCalcFile);
				InitCalc.processData();
				String.Format("===Считано {0} записей", InitCalc.FullData.Count);

				if (InitCalc.WriteMin) {
					Logger.Info(String.Format("Создание объекта записи данных в файл (минуты)"));
					ModbusDataWriter writer = new ModbusDataWriter(InitCalc, RWModeEnum.min);
					WritersMin.Add(InitCalc.ID, writer);
					String.Format("===Объект создан");
				}

				if (InitCalc.WriteHH) {
					Logger.Info(String.Format("Создание объекта записи данных в файл (получасовки)"));
					ModbusDataWriter writer = new ModbusDataWriter(InitCalc, RWModeEnum.hh);
					WritersHH.Add(InitCalc.ID, writer);
					String.Format("===Объект создан");
				}

				foreach (KeyValuePair<string, ModbusInitData> de in InitCalc.FullData) {
					FullResultData.Add(InitCalc.ID + "_" + de.Value.ID, 0);
					ResultKeys.Add(InitCalc.ID + "_" + de.Value.ID);
				}

			} catch (Exception e) {
				String.Format("===Ошибка при чтении настроек");
				Logger.Error(e.ToString());
			}
			Calc = new ModbusCalc();
			Calc.InitCalc = InitCalc;

		}

		public void readData() {
			Reader = new StreamReader(FileName);
			string valsStr = null;
			string headerStr = Reader.ReadLine();
			string[] headersArr = headerStr.Split(';');
			while ((valsStr = Reader.ReadLine()) != null) {
				try {
					string[] valsArr = valsStr.Split(';');
					DateTime date;
					bool isDate = DateTime.TryParse(valsArr[0], out date);
					if (!isDate) {
						Logger.Info("Не удалось прочитать строку даты " + valsArr[0]);
						continue;
					}
					Logger.Info("read " + date.ToString());
					foreach (ModbusInitDataArray arr in InitArrays.Values) {
						SortedList<string, double> ResultData = new SortedList<string, double>();
						foreach (ModbusInitData data in arr.FullData.Values) {
							FullResultData[arr.ID + "_" + data.ID] = Double.NaN;
							ResultData[data.ID] = Double.NaN;

							if (data.ID.Contains("_FLAG")) {
								ResultData[data.ID] = 0;
							} else {
								for (int i = 0; i < headersArr.Length; i++) {
									if (headersArr[i].ToLower() == data.ID.ToLower()) {
										string valStr = valsArr[i];
										double val = Double.NaN;
										try {
											val = Convert.ToDouble(valStr);
										} catch (Exception) {
											val = Convert.ToDouble(valStr.Replace(",", "."), Settings.NFIPoint);
										}
										FullResultData[arr.ID + "_" + data.ID] = val;
										ResultData[data.ID] = val;
									}
								}
							}
							if (arr.WriteMin && ResultData.Count > 0) {
								WritersMin[arr.ID].IsDateNow = false;
								WritersMin[arr.ID].DateForWrite = date;
								WritersMin[arr.ID].writeData(ResultData);
							}
							if (arr.WriteHH) {
								WritersHH[arr.ID].IsDateNow = false;
								WritersHH[arr.ID].DateForWrite = date;
								WritersHH[arr.ID].writeData(ResultData);
							}
						}
						ProcessFinish(date);
					}
				} catch (Exception e) {
					Logger.Error("Ошибка при чтенни строки файла");
				}

			}
		}

		public void ProcessFinish(DateTime date) {
			Logger.Info("===calc  ");
			Calc.Init(FullResultData);
			foreach (ModbusInitData initData in InitCalc.Data) {
				Calc.call(initData.FuncName, initData);
			}
			if (InitCalc.WriteHH) {
				WritersHH[InitCalc.ID].IsDateNow = false;
				WritersHH[InitCalc.ID].DateForWrite = date;
				WritersHH[InitCalc.ID].writeData(Calc.ResultData);
			}
			if (InitCalc.WriteMin) {
				WritersMin[InitCalc.ID].IsDateNow = false;
				WritersMin[InitCalc.ID].DateForWrite = date;
				WritersMin[InitCalc.ID].writeData(Calc.ResultData);
			}
			Logger.Info("====ok  ");

		}

	}
}
