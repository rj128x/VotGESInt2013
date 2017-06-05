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
		public static void ReadData(DateTime dateStart, DateTime dateEnd) {
			KOTMISettings.init(Directory.GetCurrentDirectory().ToString() + "\\Data\\KOTMISettings.xml");
			DateTime date = dateStart;
			while (date < dateEnd) {
				try {
					DateTime de = date.AddHours(2);
					de = de < dateEnd ? de : dateEnd;
					OUReport report = new OUReport(date, de, 1);
					report.processData();
				} catch (Exception e) {
					Logger.Info(e.ToString());
				}
				date = date.AddHours(2);
			}
		}
	}
}
