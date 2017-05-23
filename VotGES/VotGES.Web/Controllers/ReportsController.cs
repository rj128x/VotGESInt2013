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
using KotmiLib;
using System.Diagnostics;
using System.Runtime.Serialization.Formatters.Binary;

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
		public ActionResult PuskStopKotmi(int year1, int month1, int day1, int year2, int month2, int day2) {
			DateTime dateStart = new DateTime(year1, month1, day1);
			DateTime dateEnd = new DateTime(year2, month2, day2);
			dateEnd = dateEnd > DateTime.Now.AddHours(-2) ? DateTime.Now.AddHours(-2) : dateEnd;
			Logger.Info(String.Format("Пуски-остановы с {0} по {1}", dateStart, dateEnd));
			OgranGAReport report = new OgranGAReport(dateStart, dateEnd,true);
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
		public ActionResult PuskStopToDate(int year1, int month1, int day1, int hour1, int minute1) {
			DateTime dateStart = new DateTime(year1, month1, day1,hour1,minute1,0);
			Logger.Info(String.Format("Пуски-остановы на дату {0}", dateStart));
			//OgranGAReport report = new OgranGAReport(dateStart, dateEnd);
			SortedList<int, OgranGAAnswer> Data = new SortedList<int, OgranGAAnswer>();
			for (int ga = 1; ga <= 10; ga++) {
				OgranGAAnswer answer = new OgranGAAnswer();
				answer.createAnswer(ga, true, false, dateStart);
				Data.Add(ga, answer);
			}

			return View("PuskStopToDate", Data);
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

		[AcceptVerbs(HttpVerbs.Get)]
		public ActionResult CheckMaket(int year, int month, int day,bool ikm) {
			Logger.Info(String.Format("Состояние макетов с начала месяца {0}-{1}-{2}", year, month, day));
			DateTime dateStart = new DateTime(year, month, 1);
			DateTime dateEnd = new DateTime(year, month, day);
			DateTime date = dateStart.Date.AddDays(0);
			string message = "";
			while (date <= dateEnd.Date) {
				ReportCheckMaket report = new ReportCheckMaket(date);
				List<string> result = report.CheckData(ikm, false);
				if (result.Count > 0)
					message += String.Format("<h1>{0}</h1>{1}", date.ToString("dd.MM.yyyy"), string.Join("<br/>", result));
				date = date.AddDays(1);
			}
			if (String.IsNullOrEmpty(message))
				message = "<h1>Отклонений не найдено</h1>";
			List<string> msgs = new List<string>();
			msgs.Add(message);
			ViewResult view = View("CheckMaketReport",msgs);
			return view;
		}

		protected void Read(Object res) {
			KotmiResult resK = res as KotmiResult;
			resK.ReadData();
		}


		[AcceptVerbs(HttpVerbs.Get)]
		public ActionResult GetKotmiData(int year1, int month1, int day1,int hh1,
			int year2, int month2, int day2, int hh2,int stepSeconds, string mode, bool negPos, string fields) {
			
			DateTime dateStart = new DateTime(year1, month1, day1,hh1,0,0);
			DateTime dateEnd = new DateTime(year2, month2, day2, hh2, 0, 0);
			Logger.Info(String.Format("Данные КОТМИ за {0}-{1}", dateStart, dateEnd));

			string[] fieldsArr = fields.Split(new char[] { '~' });
			List<ArcField> Fields = new List<ArcField>();
			foreach (string fieldStr in fieldsArr) {
				Fields.Add(KOTMISettings.Single.KotmiDict[fieldStr]);
			}

			KotmiResult res = new KotmiResult(dateStart,dateEnd, Fields, stepSeconds, mode,negPos);
			Thread th = new Thread(new ParameterizedThreadStart(Read));
			th.SetApartmentState(ApartmentState.STA);
			th.Start(res);
			th.Join();

			/*String path = "C:/int/Modbus/DB";
			string fileName = String.Format("{0}/{1}_{2}.dat", Server.MapPath("/temp"), mode, DateTime.Now.ToString("yyyyMMddHHmmss"));

			ProcessStartInfo pi = new ProcessStartInfo();
			pi.Arguments = String.Format("\"{0}\" \"{1}\" {2} {3} {4} {5} \"{6}\"", 				
				date, date.AddHours(24),"kotmiReport", stepSeconds, mode, fields,fileName);
			pi.WorkingDirectory= path;
			Logger.Info(pi.Arguments);
			pi.FileName = path+"/ClearDB.exe";
			pi.WindowStyle = ProcessWindowStyle.Hidden;

			Process process = new Process();
			process.StartInfo = pi;
			process.Start();
			process.WaitForExit();
			

			BinaryFormatter binFormat = new BinaryFormatter();
			KotmiResult res = null;
			using (Stream fStream = new FileStream(fileName,
			FileMode.Open, FileAccess.Read, FileShare.None)) {
				res=binFormat.Deserialize(fStream) as KotmiResult;
			}*/

			ViewResult view = View("GetKotmiData", res);
			return view;
		}

	}
}
