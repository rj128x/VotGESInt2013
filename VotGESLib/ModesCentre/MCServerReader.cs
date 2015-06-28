using Modes;
using Modes.BusinessLogic;
using ModesApiExternal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;

namespace VotGES.ModesCentre {
	public class MCServerReader {
		public Dictionary<int, MCPBRData> Data{get;set;}
		public DateTime Date { get; set; }
		public List<string> LogInfo { get; set; }
		public List<string> AutooperData { get; set; }
		public MCServerReader(DateTime date) {
			Date = date;
			LogInfo = new List<string>();
			AutooperData = new List<string>();
			Logger.Info("Connect MC");
			try {
				ModesApiFactory.Initialize(MCSettings.Single.MCServer, MCSettings.Single.MCUser, MCSettings.Single.MCPassword);
				IApiExternal api = ModesApiFactory.GetModesApi();
				SyncZone zone = SyncZone.First;

				IModesTimeSlice ts = null;
				ts = api.GetModesTimeSlice(Date.LocalHqToSystemEx(), zone
						  , TreeContent.PGObjects /*только оборудование, по которому СО публикует ПГ(включая родителей)*/
						  , false);

				foreach (IGenObject obj in ts.GenTree) {
					getPlan(api, obj);
				}
				sendAutooperData();
				ModesApiFactory.CloseConnection();
			} catch (Exception e) {
				Logger.Info("Ошибка при получении ПБР с сервера MC " + e);
			}
		}

		public void getPlan(IApiExternal api, IGenObject obj) {
			DateTime dt1 = Date.Date.LocalHqToSystemEx();
			DateTime dt0 = Date.Date.AddDays(1).LocalHqToSystemEx();
			IList<PlanValueItem> data = api.GetPlanValuesActual(dt1, dt0, obj);
			if (data.Count > 0) {
				Logger.Info(String.Format("Обработка ПБР для {0}({1})",obj.Description,obj.Id));
				MCPBRData pbr = new MCPBRData(obj.Id);
				foreach (PlanValueItem item in data) {
					if (item.ObjFactor == 0) {
						pbr.AddValue(item.DT.SystemToLocalHqEx(), item.Value);
					}
				}
				LogInfo.Add(String.Format("Получено {0} записей с {1} по {2} по объекту {3}", pbr.Data.Count, dt1.SystemToLocalHqEx(), dt0.SystemToLocalHqEx()));
				bool ok=pbr.ProcessData();
				LogInfo.Add("===Данные записаны в базу: " + (ok ? "Успеншно" : "Ошибка"));
				pbr.addAutooperData(AutooperData);
			}
			foreach (IGenObject ch in obj.Children) {
				getPlan(api, ch);
			}
		}

		public void sendAutooperData() {
			try {
				string fn = Date.ToString("yyyyMMddTHHmm") + ".csv";
				TextWriter writer = new StreamWriter(Date.ToString("yyyyMMddTHHmm"));
				foreach (string str in AutooperData) {
					writer.WriteLine(str);
				}
				writer.Close();

				FileInfo file = new FileInfo(fn);
				System.Net.Mail.MailMessage mess = new System.Net.Mail.MailMessage();

				mess.From = new MailAddress(MCSettings.Single.SMTPFrom);
				mess.Subject = Date.ToString("yyyyMMddTHHmm"); mess.Body = "";
				mess.To.Add(MCSettings.Single.AutooperMail);
				mess.Attachments.Add(new Attachment(fn));

				mess.SubjectEncoding = System.Text.Encoding.UTF8;
				mess.BodyEncoding = System.Text.Encoding.UTF8;
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
