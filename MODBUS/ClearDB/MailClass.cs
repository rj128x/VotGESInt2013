using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using VotGES;

namespace ClearDB
{
	public class MailClass
	{
		public static void sendMail(string message, string body = "", string mailTo = "") {
			try {
				body = String.IsNullOrEmpty(body)?message:body;


				System.Net.Mail.MailMessage mess = new System.Net.Mail.MailMessage();

				mess.From = new MailAddress(Settings.single.SMTPFrom);
				mess.Subject = message;
				mess.Body = body;

				mailTo = string.IsNullOrEmpty(mailTo) ? Settings.single.ErrorMailTo : mailTo;
				string[] addr = mailTo.Split(new char[] { ';' });
				foreach (string add in addr) {
					try {
						mess.To.Add(add);
					} catch { }
				}

				mess.SubjectEncoding = System.Text.Encoding.UTF8;
				mess.BodyEncoding = System.Text.Encoding.UTF8;
				mess.IsBodyHtml = false;
				System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient(Settings.single.SMTPServer, Settings.single.SMTPPort);
				client.EnableSsl = true;
				if (string.IsNullOrEmpty(Settings.single.SMTPUser)) {
					client.UseDefaultCredentials = true;
				} else {
					client.Credentials = new System.Net.NetworkCredential(Settings.single.SMTPUser, Settings.single.SMTPPassword, Settings.single.SMTPDomain);
				}
				// Отправляем письмо
				client.Send(mess);

			} catch (Exception e) {
				Logger.Error(String.Format("Ошибка при отправке почты: {0}", e.ToString()), Logger.LoggerSource.server);
			}
		}
	}
}
