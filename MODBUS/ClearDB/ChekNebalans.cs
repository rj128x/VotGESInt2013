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
			bool hasEmptyCalc = false;
			bool hasDiffCalc = false;
			string result = String.Format("Выборка данных за период  с {0} по {1} \r\n\r\n", dateStart.ToString("dd.MM.yyyy HH:mm"), dateEnd.ToString("dd.MM.yyyy HH:mm"))
				+report.checkData(Settings.single.Limits, Settings.single.AvailEmptyNBData, ref hasNB, ref hasEmpty,ref hasEmptyCalc,ref hasDiffCalc);
			string header = string.Format("АСКУЭ{0}{1}{2}", hasNB ? ".Небаланс" : "", hasEmpty ? ".Счетчик" : "", hasEmptyCalc||hasDiffCalc ? ".РасчетБД":"");
			if (hasEmpty||hasNB||hasEmptyCalc||hasDiffCalc) {
				MailClass.sendMail(header,result);
			}		

		}
	}
}
