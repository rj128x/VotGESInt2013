using Modes;
using Modes.BusinessLogic;
using ModesApiExternal;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using VotGES.Piramida;
using Tools;

namespace VotGES.ModesCentre
{
	public class MCServerReader
	{
		//public Dictionary<int, MCPBRData> Data { get; set; }
		public DateTime Date { get; set; }
		public List<string> LogInfo { get; set; }
		public List<string> AutooperData { get; set; }
		public int NPBR { get; set; }
		protected IApiExternal api;
		public Dictionary<int,MCPBRData> ProcessedPBRS;

		public void modesConnect() {
			int index = 0;
			while (!ModesApiFactory.IsInitilized && index <= 10) {
				try {
					using (Impersonator imp = new Impersonator(MCSettings.Single.MCUser, "corp", MCSettings.Single.MCPassword)) {
						Logger.Info(String.Format("Подключение к MC. Попытка {0}", index));
						//ModesApiFactory.Initialize(MCSettings.Single.MCServer, MCSettings.Single.MCUser, MCSettings.Single.MCPassword);
						ModesApiFactory.Initialize(MCSettings.Single.MCServer);
						api = ModesApiFactory.GetModesApi();
					}
					/*Logger.Info(String.Format("Подключение к MC. Попытка {0}", index));
					ModesApiFactory.Initialize(MCSettings.Single.MCServer, "mc_votges", "mcV0tges");					
					api = ModesApiFactory.GetModesApi();*/
				} catch (Exception e) {
					Logger.Info(e.ToString());
				}
				index++;
			}
		}

		public MCServerReader(DateTime date, bool writeNPBR = true) {
			Logger.Info("Чтение ПБР за " + date.ToString());
			Date = date;
			LogInfo = new List<string>();
			AutooperData = new List<string>();
			Logger.Info("Connect MC");
			ProcessedPBRS = new Dictionary<int, MCPBRData>();
			bool sendError = false;

			try {
				modesConnect();
				SyncZone zone = SyncZone.First;

				IModesTimeSlice ts = null;
				ts = api.GetModesTimeSlice(date.Date.LocalHqToSystemEx(), zone
							, TreeContent.PGObjects /*только оборудование, по которому СО публикует ПГ(включая родителей)*/
							, false);


				bool ok = true;
				foreach (IGenObject obj in ts.GenTree) {
					ok = ok && getPlan(obj);
				}

				if (ok) {
					if (ProcessedPBRS.Count != 7) {
						LogInfo.Add("!!!!! Количество ПБР !=7. Проверьте корректность макета");
						Logger.Info("Количество ПБР !=7");
						sendError = true;
					} else {
						foreach (MCPBRData pbr in ProcessedPBRS.Values) {
							pbr.CreateData();
							pbr.addAutooperData(AutooperData);
						}
						Logger.Info("АО кол.строк " + AutooperData.Count);
						if (AutooperData.Count == 100) {
							Logger.Info("Отправка в АО кол.строк ");
							try {
								sendAutooperData();
								Logger.Info("Данный отправлены  в АО");
							} catch (Exception e) {
								Logger.Info("Ошибка при отправке данных в АО");
								Logger.Info(e.ToString());
							}
						} else {
							Logger.Info("Неверное количество данных в АО");
							LogInfo.Add("Неверное количество данных в АО");
							sendError = true;
						}

					}
				}

				if (ok) {
					Logger.Info("Зaпись ПБР в Базу");
					foreach (MCPBRData pbr in ProcessedPBRS.Values) {
						try {
							pbr.CreateData();
							bool okWrite = false;
							int i = 0;
							while (!okWrite && i < 3) {
								Logger.Info(String.Format("Запись {0} ПБР. Попытка {1}", pbr.Item, i));
								LogInfo.Add(String.Format("Запись {0} ПБР. Попытка {1}", pbr.Item, i));
								okWrite = pbr.ProcessData();
								i++;
							}
						} catch {
							Logger.Info("Ошибка при записи ПБР в базу");
							LogInfo.Add("Ошибка при записи ПБР в базу");
							sendError = true;
						}

					}
					if (writeNPBR) {
						try {
							Logger.Info("Запись номера ПБР: " + NPBR.ToString());
							SqlConnection con = PiramidaAccess.getConnection("P2000");
							con.Open();
							SqlCommand com = con.CreateCommand();
							com.CommandText = String.Format("insert into eventlist ([EVIDPROTECT],[EVTYPE],[EVCODE],[OBJCODE],[OBJTYPE],[OBJITEM],[EVGENESISDT],[EVALTDT],[EVWRITETODBDT],[SEASON],[APPID]) Select {0}, {1}, {2}, {3}, {4}, {5}, '{6}', '{7}', '{8}', {9}, {10}",
								0, 2, 5000 + NPBR, 0, 99, 1, DateTime.Now.ToString(MCPBRData.DateFormat), DateTime.Now.ToString(MCPBRData.DateFormat), DateTime.Now.ToString(MCPBRData.DateFormat), 0, 0);
							com.ExecuteNonQuery();
							LogInfo.Add("Номер ПБР: " + NPBR.ToString());
							con.Close();
						} catch (Exception e) {
							Logger.Info(e.ToString());
							Logger.Info("Ошибка при записи номера ПБР в базу");
						}
					}
				}
			} catch (Exception e) {
				Logger.Info("Ошибка при получении ПБР с сервера MC " + e);
			} finally {
				ModesApiFactory.CloseConnection();
			}
			//if (sendError)
			sendErrorPBRData(!sendError);


		}

		public bool getPlan(IGenObject obj) {
			DateTime dt1 = Date.Date.LocalHqToSystemEx();
			DateTime dt0 = Date.Date.AddDays(1).LocalHqToSystemEx();
			modesConnect();
			IList<PlanValueItem> data = api.GetPlanValuesActual(dt1, dt0, obj);
			bool ok = true;
			if (data.Count > 0) {
				Logger.Info(String.Format("Обработка ПБР для {0}({1}) [{2}]", obj.Description, obj.Id, obj.Name));

				LogInfo.Add(String.Format("Обработка ПБР для {0}({1})  [{2}]", obj.Description, obj.Id, obj.Name));
				List<MCPBRData> pbrs = MCPBRData.getPBRS(obj.Id, obj.Name);

				foreach (MCPBRData pbr in pbrs) { 
					if (ProcessedPBRS.ContainsKey(pbr.Item)) {
						string log = string.Format("Данные по коду {0} уже были считаны ",pbr.Item);
						Logger.Info(log);
						LogInfo.Add(log);
						continue;
					}
					if (pbr.DataSettings != null) {
						foreach (PlanValueItem item in data) {
							if (item.ObjFactor == 0) {
								pbr.AddValue(item.DT.SystemToLocalHqEx(), item.Value);
								string pt = item.Type.ToString().Replace("ПБР", "");
								int num = 0;
								try {
									num = Int32.Parse(pt);
								} catch { }
								NPBR = num;
							}
						}
						LogInfo.Add(String.Format("Получено {0} записей с {1} по {2} по объекту {3}", pbr.Data.Count, dt1.SystemToLocalHqEx(), dt0.SystemToLocalHqEx(), obj.Description));
						if (pbr.Data.Count > 10) {
							if (!ProcessedPBRS.ContainsKey(pbr.Item))
								ProcessedPBRS.Add(pbr.Item, pbr);
						} else {
							LogInfo.Add("Недостаточно данных");
							ok = false;
						}
						LogInfo.Add("===Данные считаны: " + (ok ? "Успешно" : "Ошибка"));
						Logger.Info("===Данные считаны: " + (ok ? "Успешно" : "Ошибка"));
					} else {
						LogInfo.Add("===Ошибка при разборе полученного макета. Возможно изменение кодировки MC");
					}

				}
				
			}
			foreach (IGenObject ch in obj.Children) {
				bool ok2 = getPlan(ch);
				ok = ok && ok2;
			}
			return ok;
		}

		public void sendAutooperData() {
			try {
				string fn = "pbr-0000" + (NPBR < 10 ? "0" : "") + NPBR.ToString() + "-" + Date.ToString("yyyyMMdd") + ".csv";

				string body = String.Join("\r\n", AutooperData);
				/*TextWriter writer = new StreamWriter(fn, false, Encoding.ASCII);				
				foreach (string str in AutooperData) {
					writer.WriteLine(str);					
				}
				writer.Close();*/

				FileInfo file = new FileInfo(fn);
				System.Net.Mail.MailMessage mess = new System.Net.Mail.MailMessage();

				mess.From = new MailAddress(MCSettings.Single.SMTPFrom);

				mess.Subject = "pbr-0000" + (NPBR < 10 ? "0" : "") + NPBR.ToString() + "-" + Date.ToString("yyyyMMdd");
				mess.Body = body;
				mess.To.Add(MCSettings.Single.AutooperMail);
				//mess.Attachments.Add(new Attachment(fn));

				mess.SubjectEncoding = System.Text.Encoding.Default;
				mess.BodyEncoding = System.Text.Encoding.Default;
				mess.IsBodyHtml = false;
				System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient(MCSettings.Single.SMTPServer, MCSettings.Single.SMTPPort);
				client.EnableSsl = true;
				if (string.IsNullOrEmpty(MCSettings.Single.SMTPUser)) {
					client.UseDefaultCredentials = true;
				} else {
					client.Credentials = new System.Net.NetworkCredential(MCSettings.Single.SMTPUser, MCSettings.Single.SMTPPassword, MCSettings.Single.SMTPDomain);
				}
				// Отправляем письмо
				client.Send(mess);
				LogInfo.Add("Данные в автооператор отправлены успешно");
				try {
					file.Delete();
				} catch { };
			} catch (Exception e) {
				Logger.Error(String.Format("Ошибка при отправке почты: {0}", e.ToString()), Logger.LoggerSource.server);
				LogInfo.Add("Данные в автооператор не отправлены");
			}
		}

		public void sendErrorPBRData(bool info) {
			try {

				string body = String.Join("<br/>", LogInfo);

				System.Net.Mail.MailMessage mess = new System.Net.Mail.MailMessage();

				mess.From = new MailAddress(MCSettings.Single.SMTPFrom);
				mess.Subject = info ? "Разбор ПБР из MC" : "Ошибка при получении ПБР из MC ";
				mess.Body = body;
				string mailTo = info ? MCSettings.Single.InfoMail : MCSettings.Single.ErrorMail;
				string[] mails = mailTo.Split(new char[] { ';' });
				foreach (string mail in mails) {
					if (!string.IsNullOrEmpty(mail)) {
						mess.To.Add(mail);
					}
				}

				mess.SubjectEncoding = System.Text.Encoding.UTF8;
				mess.BodyEncoding = System.Text.Encoding.UTF8;
				mess.IsBodyHtml = true;
				System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient(MCSettings.Single.SMTPServer, MCSettings.Single.SMTPPort);
				client.EnableSsl = true;
				if (string.IsNullOrEmpty(MCSettings.Single.SMTPUser)) {
					client.UseDefaultCredentials = true;
				} else {
					client.Credentials = new System.Net.NetworkCredential(MCSettings.Single.SMTPUser, MCSettings.Single.SMTPPassword, MCSettings.Single.SMTPDomain);
				}
				// Отправляем письмо
				client.Send(mess);
			} catch (Exception e) {
				Logger.Error(String.Format("Ошибка при отправке почты: {0}", e.ToString()), Logger.LoggerSource.server);
			}
		}
	}
}
