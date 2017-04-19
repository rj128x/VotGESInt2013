using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VotGES;
using VotGES.Piramida.Report;

namespace ClearDB
{
	public class CheckMaket
	{
		public static void checkData(DateTime dateStart, DateTime dateEnd, bool isIKM, bool isTU) {
			Logger.Info(String.Format("Проверка макетов с {0} по {1} ", dateStart.ToString("dd.MM.yyyy HH:mm"), dateEnd.ToString("dd.MM.yyyy HH:mm")));
			DateTime date = dateStart.Date.AddDays(0);
			bool send = false;
			string message = "";
			while (date <= dateEnd.Date) {
				ReportCheckMaket report = new ReportCheckMaket(date);
				List<string> result = report.CheckData(isIKM, isTU);
				if (result.Count > 0) {
					message += String.Format("<h1>{0}</h1>{1}", date.ToString("dd.MM.yyyy"), string.Join("<br/>", result));
					send = true;
				}
				date = date.AddDays(1);
			}
			string header = string.Format("Проверка макетов 80020");
			if (send)
				MailClass.sendMail(header, message, Settings.single.ErrorMailTo);

		}
	}
}
