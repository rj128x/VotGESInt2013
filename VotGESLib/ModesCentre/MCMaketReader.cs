using Modes;
using Modes.BusinessLogic;
using ModesApiExternal;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using VotGES.Piramida;

namespace VotGES.ModesCentre {
	public class MCMaketReader {		
		protected IApiExternal api;
		public DateTime Date;
		public MCMaketData MaketData;
		public List<string> LogInfo;

		public void modesConnect() {
			int index = 0;
			while (!ModesApiFactory.IsInitilized && index<=10) {
				try {
					Logger.Info(String.Format("Подключение к MC. Попытка {0}", index));
					ModesApiFactory.Initialize(MCSettings.Single.MCServer, MCSettings.Single.MCUser, MCSettings.Single.MCPassword);
					api = ModesApiFactory.GetModesApi();
				}
				catch (Exception e) {
					Logger.Info(e.ToString());
				}
				index++;
			}
		}

		public MCMaketReader(DateTime date) {
			Logger.Info("Чтение ПБР за " + date.ToString());
			Date = date;			
			Logger.Info("Connect MC");
			LogInfo = new List<string>();
			
			MaketData = new MCMaketData();
			try {
				modesConnect();
				SyncZone zone = SyncZone.First;

				IModesTimeSlice ts = null;
				ts = api.GetModesTimeSlice(date.Date.LocalHqToSystemEx(), zone
						  , TreeContent.PGObjects /*только оборудование, по которому СО публикует ПГ(включая родителей)*/
						  , false);

				getMaket(date, date.AddDays(7));

				Logger.Info("Зaпись Макета в Базу");
					try {
						bool okWrite = false;
						int i = 0;
						while (!okWrite && i < 3) {
							Logger.Info(String.Format("Запись Макетов. Попытка {0}", i));
							okWrite = MaketData.writeToDB("P3000");
							i++;
						}
					}
					catch {
						Logger.Info("Ошибка при записи ПБР в базу");
					}

				Logger.Info("finish");
				return;
			
			} catch (Exception e) {
				Logger.Info("Ошибка при получении ПБР с сервера MC " + e);
			}
		}


		public void getMaket(DateTime DateStart, DateTime DateEnd) {
			DateTime dt1 = DateStart.Date.LocalHqToSystemEx();
			DateTime dt0 = DateEnd.Date.AddDays(1).LocalHqToSystemEx();

			modesConnect();
			IList<IMaketHeader> headers= api.GetMaketHeaders53500(dt1,dt0);
			foreach (IMaketHeader header in headers) {
				//Logger.Info("process header " + header.Mrid);
				if (header.DtTarget >= DateStart && header.DtTarget <= DateEnd) {
					DateTime ds = header.DtTarget.SystemToLocalHqEx().Date;
					if (header.StProceed != StateProceed.Accept)
						continue;
					Guid[] ids=new Guid[]{header.Mrid};
					IList<IMaketEquipment> maketData=api.GetMaket53500Equipment(ids);
					foreach (IMaketEquipment maket in maketData) {									
						Logger.Info("process maket " + maket.Mrid);
						foreach (IGenObject obj in maket.GenTree) {							
							processGenTreeObject(obj,ds);
						}
					}
				}
			}			
		}

		protected void processGenTreeObject(IGenObject obj,DateTime ds) {
			Logger.Info("=================================================");
			Logger.Info("Process " + obj.Name + " " + obj.Id + " " + obj.Description);

			int id = -1;
			try {
				id = Int32.Parse(obj.Name);
			}
			catch { };

			if (MaketData.processObjects.Contains(id)) {
				foreach (IVarParam param in obj.VarParams) {
					//Logger.Info(param.Id.ToString()+param.Description);
					if (!MaketData.processVars.Contains(param.Id))
						continue;

					for (int i = 0; i < param.PointCount; i++) {
						string valStr = "";
						try {
							valStr = param.GetValue(i).ToString();
							double val = Double.Parse(valStr.Replace(",", "."));
							MaketData.processPiramidaCode(id, param.Id, ds.AddHours(i + 1), val);
						}
						catch (Exception e) {
							Logger.Info("parse " + valStr);
						}
					}
				}
			}
			
			if (obj.Children != null && obj.Children.Count>0) {
				Logger.Info("Process children");
				foreach (IGenObject ch in obj.Children) {
					processGenTreeObject(ch,ds);
				}
			}
		}

	}
}
