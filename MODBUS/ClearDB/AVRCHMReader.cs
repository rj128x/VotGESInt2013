﻿using KotmiLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VotGES;

namespace ClearDB
{
	public class AVRCHMReader
	{
		
		public static void readAVRCHM(DateTime dateStart, DateTime dateEnd) {
			Logger.Info(String.Format("Получение АВРЧМ: {0} - {1}", dateStart, dateEnd));
			KOTMISettings.init(Directory.GetCurrentDirectory().ToString() + "\\Data\\KOTMISettings.xml");
			Logger.Info(KOTMISettings.Single.Server);
			KotmiClass.init();

			DateTime date = dateStart.Date;
			while (date <= dateEnd) {
				try {
					Logger.Info(String.Format("Чтение блока {0} - {1}", date, date.AddHours(1)));
					AVRCHMReport report = new AVRCHMReport(date, date.AddHours(1), 1);
					report.ReadData(2);
					report.WriteToDB();
					AVRCHMResult result = report.Result;
					for (int ga = 1; ga <= 10; ga++) {
						Logger.Info(String.Format("GA {0}: WORK: {1:0.00} STOP: {2:0.00} AVRCHM: {3:0.00} ", ga, result.TimeWork[ga] / 60, result.TimeStop[ga] / 60, result.TimeAVRCHM[ga] / 60));
					}					
				}catch (Exception e) {
					Logger.Info(e.ToString());
				}
				date = date.AddHours(1);
			}
		}
	}
}
