﻿using Modes;
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

namespace VotGES.ModesCentre {
	public class MCServerReader {
		public Dictionary<int, MCPBRData> Data{get;set;}
		public DateTime Date { get; set; }
		public List<string> LogInfo { get; set; }
		public List<string> AutooperData { get; set; }
		public int NPBR { get; set; }
		public MCServerReader(DateTime date) {
			Logger.Info("Чтение ПБР за " + date.ToString());
			Date = date;
			LogInfo = new List<string>();
			AutooperData = new List<string>();
			Logger.Info("Connect MC");
			try {
				ModesApiFactory.Initialize(MCSettings.Single.MCServer, MCSettings.Single.MCUser, MCSettings.Single.MCPassword);
				IApiExternal api = ModesApiFactory.GetModesApi();
				SyncZone zone = SyncZone.First;

				IModesTimeSlice ts = null;
				ts = api.GetModesTimeSlice(date.Date.LocalHqToSystemEx(), zone
						  , TreeContent.PGObjects /*только оборудование, по которому СО публикует ПГ(включая родителей)*/
						  , false);

				bool ok = true;				
				foreach (IGenObject obj in ts.GenTree) {
					ok=ok&&getPlan(api, obj);
				}
				if (ok) {
					try {
						Logger.Info("Запись номера ПБР: " + NPBR.ToString());
						SqlConnection con = PiramidaAccess.getConnection("P2000");
						con.Open();
						SqlCommand com = con.CreateCommand();
						com.CommandText=String.Format("insert into eventlist ([EVIDPROTECT],[EVTYPE],[EVCODE],[OBJCODE],[OBJTYPE],[OBJITEM],[EVGENESISDT],[EVALTDT],[EVWRITETODBDT],[SEASON],[APPID]) Select {0}, {1}, {2}, {3}, {4}, {5}, '{6}', '{7}', '{8}', {9}, {10}",
							0,2,5000+NPBR,0,99,1,DateTime.Now.ToString(MCPBRData.DateFormat),DateTime.Now.ToString(MCPBRData.DateFormat),DateTime.Now.ToString(MCPBRData.DateFormat),0,0);
						com.ExecuteNonQuery();
						LogInfo.Add("Номер ПБР: " + NPBR.ToString());
						con.Close();
					}
					catch (Exception e){
						Logger.Info(e.ToString());
						Logger.Info("Ошибка при записи номера ПБР в базу");
					}
				}
				sendAutooperData();
				ModesApiFactory.CloseConnection();
			} catch (Exception e) {
				Logger.Info("Ошибка при получении ПБР с сервера MC " + e);
			}
		}

		public bool getPlan(IApiExternal api, IGenObject obj) {
			DateTime dt1 = Date.Date.LocalHqToSystemEx();
			DateTime dt0 = Date.Date.AddDays(1).LocalHqToSystemEx();
			IList<PlanValueItem> data = api.GetPlanValuesActual(dt1, dt0, obj);
			bool ok = true;
			if (data.Count > 0) {
				Logger.Info(String.Format("Обработка ПБР для {0}({1})",obj.Description,obj.Id));
				MCPBRData pbr = new MCPBRData(obj.Id);
				int index = 0;
				foreach (PlanValueItem item in data) {
					if (item.ObjFactor == 0) {
						pbr.AddValue(item.DT.SystemToLocalHqEx(), item.Value);						
							string pt = item.Type.ToString().Replace("ПБР", "");
							int num = 0;
							try {
								num = Int32.Parse(pt);
							}
							catch { }
							NPBR = num;						
					}
				}
				LogInfo.Add(String.Format("Получено {0} записей с {1} по {2} по объекту {3}", pbr.Data.Count, dt1.SystemToLocalHqEx(), dt0.SystemToLocalHqEx(),obj.Description));
				if (data.Count > 1) {
					ok = ok && pbr.ProcessData();
					LogInfo.Add("===Данные записаны в базу: " + (ok ? "Успешно" : "Ошибка"));
					pbr.addAutooperData(AutooperData);
				}
				else {
					LogInfo.Add("Недостаточно данных");
					ok = false;
				}				
			}
			foreach (IGenObject ch in obj.Children) {
				ok=ok && getPlan(api, ch);
			}
			return ok;
		}

		public void sendAutooperData() {
			try {
				string fn = "pbr-000000-"+Date.ToString("yyyyMMdd") + ".csv";

				string body = String.Join("\r\n", AutooperData);
				/*TextWriter writer = new StreamWriter(fn, false, Encoding.ASCII);				
				foreach (string str in AutooperData) {
					writer.WriteLine(str);					
				}
				writer.Close();*/

				FileInfo file = new FileInfo(fn);
				System.Net.Mail.MailMessage mess = new System.Net.Mail.MailMessage();

				mess.From = new MailAddress(MCSettings.Single.SMTPFrom);
				mess.Subject = "pbr-000000-" + Date.ToString("yyyyMMdd"); 
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
				}catch{};
			} catch (Exception e) {
				Logger.Error(String.Format("Ошибка при отправке почты: {0}", e.ToString()), Logger.LoggerSource.server);
				LogInfo.Add("Данные в автооператор не отправлены");
			}
		}
	}
}
