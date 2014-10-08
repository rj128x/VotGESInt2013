using ConnectUNCWithCredentials;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;

namespace VotGES.Rashod {
	public static class VERFolderWriter {
		public static string Folder = "\\\\sr-votges-013.corp.gidroogk.com/Рабочие_документы$/Предприятие/Оперативная служба/Группа режимов/ВЭР/AutoArchive";
		//public static string Folder = "Z:";

		public static void writeData(int year, int month, int day, string Data) {
			Logger.Info(String.Format("Write VER data {0}.{1}.{2}", year, month, day));
			string dir = String.Format("{0}/{1}/{2}", Folder, year, month);
			try {
				using (UNCAccessWithCredentials unc = new UNCAccessWithCredentials()) {
					if (unc.NetUseWithCredentials(Folder, "chekunovamv", "corp", "rJ320204")) {
						Logger.Info(dir);
						Directory.CreateDirectory(dir);
						string fileName = String.Format("{0}/day_{1}.xls", dir, day);
						TextWriter writer = new StreamWriter(fileName, false);
						writer.Write(Data);
						writer.Close();
						Logger.Info("===finish");
					}
				}
			}
			catch (Exception e) {
				Logger.Error(e.ToString());
			}
		}

		public static void writeDataMonth(int year, int month, string Data) {
			Logger.Info(String.Format("Write VER data month {0}.{1}", year, month));
			string dir = String.Format("{0}/{1}/Table1", Folder, year, month);
			try {
				using (UNCAccessWithCredentials unc = new UNCAccessWithCredentials()) {
					if (unc.NetUseWithCredentials(Folder, "chekunovamv", "corp", "rJ320204")) {
						Logger.Info(dir);
						Directory.CreateDirectory(dir);
						string fileName = String.Format("{0}/month_{1}.xls", dir, month);
						TextWriter writer = new StreamWriter(fileName, false);
						writer.Write(Data);
						writer.Close();
						Logger.Info("===finish");
					}
				}
			}
			catch (Exception e) {
				Logger.Error(e.ToString());
			}
		}
	}

	
}
