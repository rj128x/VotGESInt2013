using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VotGES;
using VotGES.Piramida.Report;

namespace ClearDB
{
	public class ChekNebalans
	{
		public static void checkData(DateTime dateStart,DateTime dateEnd) {
			Logger.Info(String.Format("Получение небаланса с {0} по {1} ", dateStart.ToString("dd.MM.yyyy HH:mm"), dateEnd.ToString("dd.MM.yyyy HH:mm")));
			ReportNebalans report = new ReportNebalans(dateStart, dateEnd);
			bool hasNB=false;
			bool hasEmpty=false;
			string result = report.checkData(Settings.single.Limits, Settings.single.AvailEmptyNBData, ref hasNB, ref hasEmpty);
			if (hasEmpty) {
				MailClass.sendMail(String.Format("Нет данных {0} - {1}", dateStart.ToString("dd.MM HH:mm"), dateEnd.ToString("dd.MM HH:mm")),result);
			}
			if (!hasEmpty && hasNB) {
				MailClass.sendMail(String.Format("!!!Небаланс {0} - {1}", dateStart.ToString("dd.MM HH:mm"), dateEnd.ToString("dd.MM HH:mm")), result);
			}

		}
	}
}
