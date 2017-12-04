using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using VotGES.Piramida;
using VotGES.EDSService;

namespace VotGES.ModesCentre
{
	public class MCPBRData
	{
		public static string InsertIntoHeader = "INSERT INTO Data (parnumber,object,item,value0,objtype,data_date,rcvstamp,season)";
		public static string InsertInfoFormat = "SELECT {0}, {1}, {2}, {3}, {4}, '{5}', '{6}', {7}";
		public static string DateFormat = "yyyy-MM-dd HH:mm:ss";

		public SortedList<DateTime, double> Data { get; set; }
		public SortedList<DateTime, double> DataHH { get; set; }
		public int Item { get; set; }
		public string PointName { get; set; }
		public MCSettingsRecord DataSettings;

		public MCPBRData(MCSettingsRecord rec) {
			Item = rec.PiramidaCode;
			PointName = rec.EDSPoint;
			Logger.Info(rec.EDSPoint);
			DataSettings = rec;

			Data = new SortedList<DateTime, double>();
			DataHH = new SortedList<DateTime, double>();
		}

		public static List<MCPBRData> getPBRS(int mcCode, string mcName) {
			List<MCPBRData> result = new List<MCPBRData>();
			foreach (MCSettingsRecord rec in MCSettings.Single.MCData) {
				if (rec.MCCode == mcCode) {
					MCPBRData pbr = new MCPBRData(rec);
					result.Add(pbr);
				}
			}
			if (result.Count == 0) {
				foreach (MCSettingsRecord rec in MCSettings.Single.MCData) {
					if (rec.MCName.ToLower() == mcName.ToLower()) {
						MCPBRData pbr = new MCPBRData(rec);
						result.Add(pbr);
					}
				}
			}
			if (result.Count == 0) {
				Logger.Info("Ошибка при разборе полученного макета. Возможно изменение кодировки MC");
			}
			return result;
		}

		public void AddValue(DateTime date, double val) {
			try {
				Data.Add(date, val);
			} catch (Exception e) {
				Logger.Info("ошибка при разборе пбр. дубль в исходных днных " + e);
			}
		}

		protected SortedList<DateTime, double> createInegratedData() {
			SortedList<DateTime, double> integr = new SortedList<DateTime, double>();
			int index = 0;

			foreach (KeyValuePair<DateTime, double> de in Data) {
				DateTime nextDate = index < Data.Count - 1 ? Data.Keys[index + 1] : Data.Last().Key;
				if (de.Key == nextDate)
					break;
				DateTime dt = de.Key.AddMinutes(0);
				int i = 0;
				while (dt < nextDate) {
					double sum = de.Value + (Data[nextDate] - de.Value) / 60 * i;
					integr.Add(dt, sum);
					dt = dt.AddMinutes(1);
					i++;
				}
				index++;
			}
			return integr;
		}

		protected SortedList<DateTime, double> createHHData() {
			SortedList<DateTime, double> hh = new SortedList<DateTime, double>();
			int index = 0;
			foreach (KeyValuePair<DateTime, double> de in Data) {
				hh.Add(de.Key, de.Value);
				DateTime nextDate = index < Data.Count - 1 ? Data.Keys[index + 1] : Data.Last().Key;
				if (de.Key == nextDate)
					break;
				DateTime dt = de.Key.AddMinutes(30);
				double val = (de.Value + Data[nextDate]) / 2;
				hh.Add(dt, val);
				index++;
			}
			return hh;
		}

		protected SortedList<DateTime, double> createHH15Data() {
			SortedList<DateTime, double> hh = new SortedList<DateTime, double>();
			int index = 0;
			foreach (KeyValuePair<DateTime, double> de in Data) {
				hh.Add(de.Key.AddMinutes(-15), de.Value);
				DateTime nextDate = index < Data.Count - 1 ? Data.Keys[index + 1] : Data.Last().Key;
				if (de.Key == nextDate)
					break;
				DateTime dt = de.Key.AddMinutes(30);
				double val = (de.Value + Data[nextDate]) / 2;
				hh.Add(dt.AddMinutes(-15), val);
				index++;
			}
			return hh;
		}


		protected bool writeToDB(string DBName, SortedList<DateTime, double> data, int parnumber) {
			Logger.Info("Запись данных по объекту " + parnumber);
			SqlConnection con = null;
			bool ok = false;
			try {
				con = PiramidaAccess.getConnection(DBName);
				con.Open();
				SqlTransaction transact = con.BeginTransaction();
				string delStr = String.Format("DELETE FROM DATA WHERE object=0 and objtype=2 and item={0} and parnumber={1} and data_date>='{2}' and data_date<='{3}'",
				Item, parnumber, data.First().Key.ToString(DateFormat), data.Last().Key.ToString(DateFormat));

				SqlCommand commandDel = transact.Connection.CreateCommand();
				commandDel.CommandText = delStr;
				commandDel.Transaction = transact;
				Logger.Info("Удаление ПБР");
				commandDel.ExecuteNonQuery();

				List<string> inserts = new List<string>();
				foreach (KeyValuePair<DateTime, double> de in data) {
					string ins = String.Format(InsertInfoFormat, parnumber, 0, Item, (de.Value * 1000).ToString().Replace(',', '.'), 2, de.Key.ToString(DateFormat), DateTime.Now.ToString(DateFormat), DBSettings.getSeason(de.Key));
					inserts.Add(ins);
					if (inserts.Count % 100 == 0 || de.Key == data.Last().Key) {
						string insertsSQL = String.Join("\nUNION ALL\n", inserts);
						string insertSQL = String.Format("{0}\n{1}", InsertIntoHeader, insertsSQL);
						SqlCommand commandIns = transact.Connection.CreateCommand();
						commandIns.CommandText = insertSQL;
						commandIns.Transaction = transact;
						Logger.Info("Запись данных");
						commandIns.ExecuteNonQuery();
						inserts.Clear();
					}
				}

				transact.Commit();
				ok = true;
			} catch (Exception e) {
				Logger.Info("ошибка при записи ПБР в базу " + e);
				ok = false;

			} finally {
				try {
					con.Close();
				} catch { }
			}
			return ok;
		}

		protected static bool process(edsPortTypeClient client, string authStr, uint id) {
			bool finished = false;
			RequestStatus status;
			float progress;
			string msg;
			int i = 0;
			bool ok = false;
			do {
				i++;
				client.getRequestStatus(authStr, id, out status, out progress, out msg);
				Logger.Info(String.Format("{3} {0}: {1} ({2})", status, progress * 100, msg, i));
				ok = status == RequestStatus.REQUESTSUCCESS;
				finished = ok || i >= 5;
				System.Threading.Thread.Sleep(2000);
			} while (!finished);
			//Console.ReadLine();
			return ok;
		}

		protected static long toTS(DateTime date) {
			DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
			TimeSpan diff = date.ToUniversalTime() - origin;
			return (long)(diff.TotalSeconds);
		}


		public bool writeToEDS(string pointName, SortedList<DateTime, double> data, bool data15) {
			bool ok = false;
			try {
				//SortedList<DateTime, double> data15 = createHH15Data();
				edsPortTypeClient client = new edsPortTypeClient();
				string authStr = client.login("admin", "Ovation123", ClientType.CLIENTTYPEDEFAULT);
				List<ShadeSelector> sel = new List<ShadeSelector>();
				sel.Add(new ShadeSelector() {
					period = new TimePeriod() {
						from = new Timestamp() { second = toTS(data.Keys.First().AddHours(2)) },
						till = new Timestamp() { second = toTS(data.Keys.Last().AddHours(2)) }
					},
					pointId = new PointId() {
						iess = pointName
					}
				});

				uint id = client.requestShadesClear(authStr, sel.ToArray());
				ok = process(client, authStr, id);

				if (ok) {
					List<Shade> shades = new List<Shade>();
					List<ShadeValue> vals = new List<ShadeValue>();

					if (data15) {
						foreach (KeyValuePair<DateTime, double> de in data) {
							DateTime d = de.Key.AddMinutes(0);
							DateTime dEnd = de.Key.AddMinutes(30);
							dEnd = dEnd > data.Last().Key ? data.Last().Key : dEnd;
							while (d < dEnd) {
								vals.Add(new ShadeValue() {
									period = new TimePeriod() {
										from = new Timestamp() { second = toTS(d.AddHours(2)) },
										till = new Timestamp() { second = toTS(d.AddHours(2).AddMinutes(1)) }
									},
									quality = Quality.QUALITYGOOD,
									value = new PointValue() { av = (float)de.Value, avSpecified = true }
								});
								d = d.AddMinutes(1);
							}
						};
					} else {
						double sum = 0;
						foreach (KeyValuePair<DateTime, double> de in data) {
							DateTime d = de.Key.AddMinutes(0);
							sum += de.Value/60;
							vals.Add(new ShadeValue() {
								period = new TimePeriod() {
									from = new Timestamp() { second = toTS(d.AddHours(2)) },
									till = new Timestamp() { second = toTS(d.AddHours(2).AddMinutes(1))-1 }
								},
								quality = Quality.QUALITYGOOD,
								value = new PointValue() { av = (float)sum, avSpecified = true }
							});
							d = d.AddMinutes(1);
						};
					}
					shades.Add(new Shade() {
						pointId = new PointId() { iess = pointName },
						values = vals.ToArray()
					});


					id = client.requestShadesWrite(authStr, shades.ToArray());
					ok = process(client, authStr, id);
					client.logout(authStr);
				}
			} catch (Exception e) {
				Logger.Info("Ошибка при записи ПБР в EDS " + e.ToString());
				ok = false;
			}
			return ok;
		}

		public void addAutooperData(List<string> data) {
			try {
				if (DataSettings.Autooper) {
					foreach (KeyValuePair<DateTime, double> de in Data) {
						data.Add(String.Format("{0};{1};{2};", Item, de.Key.ToString("yyyyMMddTHHmm"), de.Value));
					}
				}
			} catch (Exception e) {
				Logger.Info(e.ToString());
			}
		}

		public void CreateData() {
			DataHH = createHHData();
		}

		public bool ProcessData() {
			try {
				bool ok = true;
				SortedList<DateTime, double> data15 = createHH15Data();
				ok = ok && writeToDB("P3000", DataHH, 212);
				ok = ok && writeToDB("P2000", DataHH, 212);
				ok = ok && writeToDB("P2000", data15, 213);
				if (!String.IsNullOrEmpty(this.PointName)) {
					Logger.Info("Запись в ЕДС");
					this.writeToEDS(this.PointName,createHH15Data(),true);
				}
				if (DataSettings.WriteIntegratedData) {
					SortedList<DateTime, double> integr = createInegratedData();
					Logger.Info("Запись в ЕДС");
					this.writeToEDS("PBR_GES_VYR_PLAN.EDS@CALC", integr, false);
					ok = ok && writeToDB("P3000", integr, 204);
					ok = ok && writeToDB("P2000", integr, 204);
				}
				return ok;
			} catch (Exception e) {
				Logger.Info("Ошибка при записи ПБР в базу ");
				Logger.Info(e.ToString());
				return false;
			}
		}
	}

}
