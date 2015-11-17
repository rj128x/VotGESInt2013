using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Mail;
using System.Text;
using VotGES;
using VotGES.Piramida;

namespace ClearDB {
	public class CheckModbusWater {
		public static void CheckData() {
			DateTime date = DateTime.Now.AddHours(-2).AddMinutes(-10);
			SqlConnection con = PiramidaAccess.getConnection("PMin");
			String sql = String.Format("select count(*) from data where parnumber=4 and objtype=2 and object=3  and item in(1,2,3) and data_date>'{0}'", date.ToString(DBClass.DateFormat));
			try {
				con.Open();
				SqlCommand com = con.CreateCommand();
				com.CommandText=sql;
				object cnt=com.ExecuteScalar();
				int count = Int32.Parse(cnt.ToString());
				if (count == 0) {
					sendMail("Нет данных modbus за последние 10 минут");
				}
			}
			catch (Exception e) {
				Logger.Info("Не удалось обработать данные modbus");
			}
		}

		public static void sendMail(string message) {
			try {
				string body = message;


				System.Net.Mail.MailMessage mess = new System.Net.Mail.MailMessage();

				mess.From = new MailAddress(Settings.single.SMTPFrom);
				mess.Subject = "ошибка";
				mess.Body = body;

				string[] addr = Settings.single.ErrorMailTo.Split(new char[] { ';' });
				foreach (string add in addr) {
					try {
						mess.To.Add(add);
					}
					catch { }
				}				

				mess.SubjectEncoding = System.Text.Encoding.UTF8;
				mess.BodyEncoding = System.Text.Encoding.UTF8;
				mess.IsBodyHtml = true;
				System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient(Settings.single.SMTPServer, Settings.single.SMTPPort);
				client.EnableSsl = true;
				if (string.IsNullOrEmpty(Settings.single.SMTPUser)) {
					client.UseDefaultCredentials = true;
				}
				else {
					client.Credentials = new System.Net.NetworkCredential(Settings.single.SMTPUser, Settings.single.SMTPPassword, Settings.single.SMTPDomain);
				}
				// Отправляем письмо
				client.Send(mess);
				
			}
			catch (Exception e) {
				Logger.Error(String.Format("Ошибка при отправке почты: {0}", e.ToString()), Logger.LoggerSource.server);
			}
		}

	}
}
