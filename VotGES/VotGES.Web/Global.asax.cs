﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using VotGES.Web.Logging;
using VotGES.Piramida;
using VotGES.OgranGA;
using VotGES.ModesCentre;
using KotmiLib;

namespace VotGES.Web
{
	// Примечание: Инструкции по включению классического режима IIS6 или IIS7 
	// см. по ссылке http://go.microsoft.com/?LinkId=9394801

	public class MvcApplication : System.Web.HttpApplication
	{
		public static void RegisterRoutes(RouteCollection routes) {
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

			routes.MapRoute(
				 "Default", // Имя маршрута
				 "{controller}/{action}/{id}", // URL-адрес с параметрами
				 new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Параметры по умолчанию
			);

		}
			

		protected void Application_Start() {
			DBSettings.init(Server.MapPath("/bin/Data/DBSettings.xml"));
			MCSettings.init(Server.MapPath("/bin/Data/MCSettings.xml"));
			try {
				KapRemontsData.init(Server.MapPath("/bin/Data/KapRemontsData.xml"));
				Logger.Info(KapRemontsData.Single.Data.Count.ToString());
			}
			catch { }
			
			VotGES.GlobalVotGES.setCulture();

			Logger logger=new WebLogger();
			Logger.InitFileLogger(Server.MapPath("/logs/"), "orders", new Web.Logging.WebLogger());
			Logger.Info("Старт приложения "+DateTime.Now);

			try {
				KOTMISettings.init(Server.MapPath("/bin/Data/KOTMISettings.xml"));
			} catch { }

			AreaRegistration.RegisterAllAreas();

			RegisterRoutes(RouteTable.Routes);
		}

		
	}
}