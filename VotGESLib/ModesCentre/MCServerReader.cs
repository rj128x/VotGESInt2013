using Modes;
using Modes.BusinessLogic;
using ModesApiExternal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VotGES.ModesCentre {
	public class MCServerReader {
		public Dictionary<int, MCPBRData> Data{get;set;}
		public DateTime Date { get; set; }
		public MCServerReader(DateTime date) {
			Date = date;
			Logger.Info("Connect MC");
			try {
				ModesApiFactory.Initialize(MCSettings.Single.MCServer, MCSettings.Single.MCUser, MCSettings.Single.MCPassword);
				IApiExternal api = ModesApiFactory.GetModesApi();
				SyncZone zone = SyncZone.First;

				IModesTimeSlice ts = null;
				ts = api.GetModesTimeSlice(Date.LocalHqToSystemEx(), zone
						  , TreeContent.PGObjects /*только оборудование, по которому СО публикует ПГ(включая родителей)*/
						  , false);

				foreach (IGenObject obj in ts.GenTree) {
					getPlan(api, obj);
				}
				ModesApiFactory.CloseConnection();
			} catch (Exception e) {
				Logger.Info("Ошибка при получении ПБР с сервера MC " + e);
			}
		}

		public void getPlan(IApiExternal api, IGenObject obj) {
			DateTime dt1 = Date.Date.LocalHqToSystemEx();
			DateTime dt0 = Date.Date.AddDays(1).LocalHqToSystemEx();
			IList<PlanValueItem> data = api.GetPlanValuesActual(dt1, dt0, obj);
			if (data.Count > 0) {
				Logger.Info(String.Format("Обработка ПБР для {0}({1})",obj.Description,obj.Id));
				MCPBRData pbr = new MCPBRData(obj.Id);
				foreach (PlanValueItem item in data) {
					if (item.ObjFactor == 0) {
						pbr.AddValue(item.DT, item.Value);
					}
				}
				pbr.ProcessData();
			}
			foreach (IGenObject ch in obj.Children) {
				getPlan(api, ch);
			}
		}
	}
}
