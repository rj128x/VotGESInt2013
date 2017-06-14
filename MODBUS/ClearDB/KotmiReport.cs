using KotmiLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using VotGES;

namespace ClearDB
{
	public class KotmiReport
	{
		[STAThread]
		public static void ReadData(DateTime dateStart, DateTime dateEnd,bool min=true) {
			KOTMISettings.init(Directory.GetCurrentDirectory().ToString() + "\\Data\\KOTMISettings.xml");
			DateTime date = dateStart;
			while (date < dateEnd) {
				try {
					DateTime de = date.AddHours(min?2:6);
					de = de < dateEnd ? de : dateEnd;
					OUReport report = new OUReport(date, de, 1);
					report.processData(min);
				} catch (Exception e) {
					Logger.Info(e.ToString());
				}
				date = date.AddHours(min ? 2 : 6);
			}
		}
	}
}
