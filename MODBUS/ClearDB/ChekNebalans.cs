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
		public static void checkData(DateTime dateStart,DateTime dateEnd,bool isFull=false,bool showTU=true) {
			Logger.Info(String.Format("Получение небаланса с {0} по {1} ", dateStart.ToString("dd.MM.yyyy HH:mm"), dateEnd.ToString("dd.MM.yyyy HH:mm")));
			ReportNebalans report = new ReportNebalans(dateStart, dateEnd);
			report.IsFull = isFull;
			report.ShowTU = showTU;

			bool hasNB=false;
			bool hasEmpty=false;
			bool hasEmptyCalc = false;
			bool hasDiffCalc = false;
			
			string dates = String.Format("{2} выборка данных за период  с {0} по {1} <br/><br/>", dateStart.ToString("dd.MM.yyyy HH:mm"), dateEnd.ToString("dd.MM.yyyy HH:mm"),!isFull?"Краткая":"Подробная");
			string result = dates+report.checkData(Settings.single.Limits, Settings.single.AvailEmptyNBData, ref hasNB, ref hasEmpty,ref hasEmptyCalc,ref hasDiffCalc);			
			string header = string.Format("{3}{4}АСКУЭ{0}{1}{2}", hasNB ? ".Небаланс" : "", hasEmpty ? ".Счетчик" : "", hasEmptyCalc||hasDiffCalc ? ".РасчетБД":"",isFull?"Full_":"",showTU?"TU_":"");			
			if (hasEmpty||hasNB||hasEmptyCalc||hasDiffCalc) {
				MailClass.sendMail(header,result,isFull||showTU?Settings.single.NebalansFullMailTo:Settings.single.NebalansMailTo);
			}
		}


	}
}
