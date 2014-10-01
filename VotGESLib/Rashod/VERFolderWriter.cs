using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace VotGES.Rashod {
	public static class VERFolderWriter {
		public static string Folder = "e:/VERData";

		public static void writeData(int year, int month, int day, string Data) {
			Logger.Info(String.Format("Write VER data {0}.{1}.{2}",year,month,day));
			string dir = String.Format("{0}/{1}/{2}", Folder, year, month);
			try {
				Directory.CreateDirectory(dir);
				string fileName = String.Format("{0}/day_{1}.xls", dir, day);
				TextWriter writer = new StreamWriter(fileName, false);
				writer.Write(Data);
				writer.Close();
				Logger.Info("===finish");
			}
			catch (Exception e) {
				Logger.Error(e.ToString());
			}
		}
	}
}
