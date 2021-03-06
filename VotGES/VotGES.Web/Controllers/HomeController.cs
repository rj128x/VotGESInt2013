﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VotGES.Piramida.Report;
using VotGES.Rashod;


namespace VotGES.Web.Controllers
{
	[HandleError]
	public class HomeController : Controller
	{
		public ActionResult Index() {
			ViewData["Message"] = "Добро пожаловать в ASP.NET MVC!";

			return View("MainSL");
		}

		public ActionResult TestReport() {
			RezhimSKReport report=new RezhimSKReport(new DateTime(2010,3,14),new DateTime(2010,03,15),IntervalReportEnum.halfHour);
			report.ReadData();
			return View("Test");
		}

		public ActionResult RUSA() {
			RusaUtils.createRusaOptim();
			return View();
		}

		public ActionResult About() {
			return View();
		}
	}
}
