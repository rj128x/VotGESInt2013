using BytesRoad.Net.Ftp;
using Ionic.Zip;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using VotGES.Piramida;
using VotGES.Piramida.Report;

namespace VotGES.NPRCH {
	public class MonitorNPRCH {
		public DateTime DateStart { get; set; }
		public DateTime DateEnd { get; set; }
		public string TempDir { get; set; }
		public string FTPUser{get;set;}
		public string FTPServer{get;set;}
		public int FTPPort{get;set;}
		public string FTPPassword{get;set;}

		public MonitorNPRCH(DateTime dateStart, DateTime dateEnd, string tempDir,string ftpServer, int ftpPort, string ftpUser, string ftpPassword) {
			DateStart = dateStart;
			DateEnd = dateEnd;
			TempDir = tempDir;
			FTPServer=ftpServer;
			FTPUser=ftpUser;
			FTPPassword=ftpPassword;
			FTPPort=ftpPort;
			readData();
		}

		public void readData() {		
			for (int ga=1;ga<=10;ga++){
				Logger.Info("Чтение данных НПРЧ ГА-"+ga.ToString());
				int[] items = { 100 + ga, 200 + ga, 300 + ga };
				List<PiramidaEnrty> data=PiramidaAccess.GetDataFromDB(DateStart, DateEnd, 5, 2, 44, items.ToList(), true, true, "PSec");
				Dictionary<DateTime, Dictionary<int, double>> ProcessedData = new Dictionary<DateTime, Dictionary<int, double>>();
				foreach (PiramidaEnrty entry in data) {
					DateTime date = entry.Date;
					if (!ProcessedData.ContainsKey(date)) {
						ProcessedData.Add(date, new Dictionary<int, double>());						
					}
					int item = entry.Item/100;
					ProcessedData[date].Add(item, entry.Value0);					
				}

				string fileName = getFileName(DateStart, ga, "txt");
				Logger.Info("Создание файла отчета " + fileName);
				FileInfo fi = new FileInfo(fileName);
				if (!Directory.Exists(fi.Directory.FullName)) {
					Directory.CreateDirectory(fi.Directory.FullName);
				}

				TextWriter writer = new StreamWriter(fileName, false);
				foreach (DateTime date in ProcessedData.Keys) {
					int sec = date.Minute * 60 + date.Second + 1;

					String str = String.Format("{0}:{1};{2};{3};2;", sec, ProcessedData[date][1], ProcessedData[date][2], ProcessedData[date][3]);
					writer.WriteLine(str);
				}
				writer.Close();
				ZipFile zip = new ZipFile(getFileName(DateStart, ga, "zip"));
				zip.AddFile(fileName, "");
				zip.Save();
				fi.Delete();
				Logger.Info("Создание файла отчета завершено");
				SendFile(getFileName(DateStart, ga, "zip"));
			}									
		}

		public string getFileName(DateTime DateStart, int ga, string ext) {
			string dir = TempDir + "/" + ga.ToString("00") + "/" + DateStart.ToString("yyyy") + "/" + DateStart.ToString("MM") + "/" + DateStart.ToString("dd");
			string fileName = dir + "/" + String.Format("{0:00}{1}{2:00}.{3}", ga, DateStart.ToString("yyyyMMdd"), DateStart.Hour + 1, ext);
			return fileName;
		}

		 public bool SendFile(string fileName)
        {
            bool ok = true;
            try
            {
                Logger.Info("Отправка файла на ftp: " + fileName);
                FtpClient client = new FtpClient();
                int timeout = 3000;

                client.Connect(timeout, FTPServer, FTPPort);
                client.Login(timeout, FTPUser, FTPPassword);
                client.PassiveMode = false;


                FileInfo fi = new FileInfo(fileName);
                List<string> dirs = new List<string>();
                DirectoryInfo dir = fi.Directory;
                DirectoryInfo InitDI = new DirectoryInfo(TempDir);

                dirs.Add(dir.Name);
                while (dir.Parent.FullName != InitDI.FullName)
                {
                    dirs.Add(dir.Parent.Name);
                    dir = dir.Parent;
                }

                for (int i = dirs.Count - 1; i >= 0; i--)
                {
                    try
                    {
                        client.ChangeDirectory(timeout, dirs[i]);
                    }
                    catch
                    {
                        try
                        {
                            Logger.Info(String.Format("Создание директории {0}", dirs[i]));
                            client.CreateDirectory(timeout, dirs[i]);
                            client.ChangeDirectory(timeout, dirs[i]);
                        }
                        catch (Exception e)
                        {
                            Logger.Info("Ошибка при создаинии директории ");
                            Logger.Info(e.ToString());
                        }
                    }
                }

                try
                {
                    client.PassiveMode = true;
                    client.PutFile(timeout, fi.Name, fileName);
                }
                catch (Exception e){
                   
                    ok = false;
                }
                client.Disconnect(timeout);                
            }
            catch (Exception e)
            { 
                Logger.Info("Ошибка при отправке файла");
                Logger.Info(e.ToString());
                ok = false;
            }
            Logger.Info("Отправка завершена: " + ok.ToString());
            return ok;
        }
               
    }	
}
