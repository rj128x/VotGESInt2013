using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VotGES.Piramida.Report;
using VotGES.PBR;
using VotGES.Piramida;
using System.IO;
using System.Web.UI;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System.Text;
using System.Threading;
using VotGES.OgranGA;
using VotGES.Rashod;
using VotGES.Web.Models;
using VotGES.NPRCH;
using VotGES.ModesCentre;

namespace VotGES.Web.Controllers
{	

	public class ReportsController : Controller
	{
		//
		// GET: /Reports/

		public ActionResult Index() {
			return View();
		}


		[AcceptVerbs(HttpVerbs.Get)]
		public ActionResult SutVed(int year, int month, int day, bool isUral=true) {
			Logger.Info(String.Format("Суточная ведомость за {0}.{1}.{2}", day, month, year));
			DateTime date=new DateTime(year, month, day);
			int h=isUral ? 2 : 0;
			SutVedReport report=new SutVedReport(date.Date.AddHours(-h), date.Date.AddDays(1).AddHours(-h), IntervalReportEnum.halfHour);
			report.ReadData();
			report.AddHours = h;

			StringWriter writer=new StringWriter();

			ViewResult view=View("SutVed", report);
			return view;
		}
		
			

		[AcceptVerbs(HttpVerbs.Get)]
		public ActionResult PBR(int year, int month, int day) {
			Logger.Info(String.Format("ПБР за {0}.{1}.{2}", day, month, year));
			DateTime date=new DateTime(year, month, day);
			PBRFull pbr=new PBRFull(date);
			return View("PBR", pbr);
		}

		[AcceptVerbs(HttpVerbs.Get)]
		public ActionResult Prikaz20(int year, int month, int day) {
			Logger.Info(String.Format("Приказ 20 за {0}.{1}.{2}", day, month, year));
			DateTime date=new DateTime(year, month, day);
			Prikaz20Report report=new Prikaz20Report(date);

			return View("Prikaz20", report);
		}

		
		[AcceptVerbs(HttpVerbs.Get)]
		public ActionResult FullReport(string guid) {
			ReportAnswer report=null;
			Guid ID=Guid.Parse(guid);
			while (report==null) {
				if (Reports.CreatedReports.ContainsKey(ID)) {					
					report = Reports.CreatedReports[ID];
					Reports.removeReport(ID);
				} else {
					Thread.Sleep(TimeSpan.FromSeconds(2));
				}
			}
			return View("FullReport", report);
		}

		[AcceptVerbs(HttpVerbs.Get)]
		public ActionResult PuskStop(int year1, int month1, int day1, int year2, int month2, int day2) {
			DateTime dateStart = new DateTime(year1, month1, day1);
			DateTime dateEnd = new DateTime(year2, month2, day2);
			dateEnd = dateEnd > DateTime.Now.AddHours(-2) ? DateTime.Now.AddHours(-2) : dateEnd;
			Logger.Info(String.Format("Пуски-остановы с {0} по {1}", dateStart, dateEnd));
			OgranGAReport report = new OgranGAReport(dateStart, dateEnd);
			report.readSumData();

			return View("PuskStop", report);
		}


		[AcceptVerbs(HttpVerbs.Get)]
		public ActionResult PuskStopByDays(int year1, int month1, int day1, int year2, int month2, int day2) {
			DateTime dateStart = new DateTime(year1, month1, day1);
			DateTime dateEnd = new DateTime(year2, month2, day2);
			dateEnd = dateEnd > DateTime.Now.AddHours(-2) ? DateTime.Now.AddHours(-2) : dateEnd;
			Logger.Info(String.Format("Пуски-остановы по дням с {0} по {1}", dateStart, dateEnd));
			OgranGAReport report = new OgranGAReport(dateStart, dateEnd);
			report.readSumDataByDays();
			
			return View("PuskStopByDays", report);
		}

		[AcceptVerbs(HttpVerbs.Get)]
		public ActionResult TimeStopGA() {
			Logger.Info("Получение информации о простое гидроагрегатов");
			TimeStopGAAnswer answer = new TimeStopGAAnswer();
			answer.readData();
			
			return View("TimeStopGA", answer);
		}


		protected VERReport getVERReport(int year, int month, int day) {
			DateTime dateStart = new DateTime(year, month, day);
			DateTime dateEnd = dateStart.AddDays(1);
			Logger.Info(String.Format("ВЭР с {0} по {1}", dateStart, dateEnd));
			VERReport report = new VERReport(dateStart, dateEnd, IntervalReportEnum.minute);
			report.ReadData();
			return report;
		}

		protected VERReportMonth getVERReportMonth(int year, int month) {
			DateTime dateStart = new DateTime(year, month, 1);
			DateTime dateEnd = dateStart.AddMonths(1);
			Logger.Info(String.Format("ВЭР с {0} по {1}", dateStart, dateEnd));
			VERReportMonth report = new VERReportMonth(dateStart, dateEnd, IntervalReportEnum.hour);
			report.ReadData();
			return report;
		}

		[AcceptVerbs(HttpVerbs.Get)]
		public ActionResult VERReport(int year, int month, int day) {
			ViewResult view = View("VERReport", getVERReport(year,month,day));			
			return view;
		}

		[AcceptVerbs(HttpVerbs.Get)]
		public ActionResult VERReportMonth(int year, int month) {
			ViewResult view = View("VERReportMonth", getVERReportMonth(year, month));
			return view;
		}

		[AcceptVerbs(HttpVerbs.Get)]		
		public ActionResult VERReportToFile(int year, int month, int day) {
			VERReport report = getVERReport(year, month, day);
			StringWriter sw = new StringWriter();
			ViewData.Model = report;
			ViewEngineResult result = ViewEngines.Engines.FindView(ControllerContext, "VERReport","");
			ViewContext context = new ViewContext(ControllerContext, result.View, ViewData, TempData, sw);
			result.View.Render(context, sw);
			result.ViewEngine.ReleaseView(ControllerContext, result.View);

			string viewString = sw.ToString();

			VERFolderWriter.writeData(year, month, day, viewString);
			return Content("finish");
		}

		[AcceptVerbs(HttpVerbs.Get)]
		public ActionResult VERReportToFilePrevDate() {
			DateTime date=DateTime.Now.Date.AddDays(-1);
			return VERReportToFile(date.Year, date.Month, date.Day);
		}

		[AcceptVerbs(HttpVerbs.Get)]
		public ActionResult VERReportMonthToFile(int year, int month) {
			VERReportMonth report = getVERReportMonth(year, month);
			StringWriter sw = new StringWriter();
			ViewData.Model = report;
			ViewEngineResult result = ViewEngines.Engines.FindView(ControllerContext, "VERReportMonth", "");
			ViewContext context = new ViewContext(ControllerContext, result.View, ViewData, TempData, sw);
			result.View.Render(context, sw);
			result.ViewEngine.ReleaseView(ControllerContext, result.View);

			string viewString = sw.ToString();

			VERFolderWriter.writeDataMonth(year, month, viewString);
			return Content("finish");
		}

		[AcceptVerbs(HttpVerbs.Get)]
		public ActionResult VERReportMonthToFilePrevDate() {
			DateTime date = DateTime.Now.Date.AddDays(-1);
			return VERReportMonthToFile(date.Year, date.Month);
		}

		[AcceptVerbs(HttpVerbs.Get)]
		public ActionResult CreateNPRCHDataPrevDate() {
			DateTime dateStart = DateTime.Now.Date.AddDays(-1);
			DateTime dateEnd = dateStart.AddDays(1);
			DateTime ds = dateStart.AddDays(0);
			while (ds < dateEnd) {
				DateTime de = ds.AddHours(1);
				MonitorNPRCH monitor = new MonitorNPRCH(ds, de, "E:/MBData/TEMP", "sr-votges-int", 21, "nprch", "Cnceflvby951");
				ds = de.AddDays(0);
			}
			return Content("finish");
		}

		[AcceptVerbs(HttpVerbs.Get)]
		public ActionResult PBRFromMC(int year, int month, int day) {
			Logger.Info(String.Format("Запрос ПБР из MC за {0}-{1}-{2}", year, month, day));
			MCServerReader reader = new MCServerReader(new DateTime(year, month, day));
			ViewResult view = View("MCReport", reader);
			return view;
		}

		[AcceptVerbs(HttpVerbs.Get)]
		public ActionResult PBRFromMCToday() {
			Logger.Info(String.Format("Запрос ПБР из MC за {0}", DateTime.Now.Date));
			MCServerReader reader = new MCServerReader(DateTime.Now.Date);
			return Content("finish");
		}

		[AcceptVerbs(HttpVerbs.Get)]
		public ActionResult MaketFromMCToday() {
			Logger.Info(String.Format("Запрос Макета 53500 из MC за {0}", DateTime.Now.Date));
			MCMaketReader reader = new MCMaketReader(DateTime.Now.Date);
			return Content("finish");
		}

		[AcceptVerbs(HttpVerbs.Get)]
		public ActionResult MaketFromMC(int year, int month, int day) {
			Logger.Info(String.Format("Запрос Макета из MC за {0}-{1}-{2}", year, month, day));
			MCMaketReader reader = new MCMaketReader(new DateTime(year, month, day));
			ViewResult view = View("MCMaketReport", reader);
			return view;
		}
	}
}
