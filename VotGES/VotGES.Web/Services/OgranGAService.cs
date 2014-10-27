
namespace VotGES.Web.Services {
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.ComponentModel.DataAnnotations;
	using System.Linq;
	using System.ServiceModel.DomainServices.Hosting;
	using System.ServiceModel.DomainServices.Server;
	using VotGES.Web.Models;
	using VotGES.Web.Logging;
	using VotGES.Piramida.Report;
	using VotGES.Chart;
	using VotGES.Rashod;
	using VotGES.OgranGA;



	// TODO: создайте методы, содержащие собственную логику приложения.
	[EnableClientAccess()]
	public class OgranGAService : DomainService {
		public OgranGAAnswer getOgranGAAnswer(int ga) {
			WebLogger.Info("OgranGATable process", VotGES.Logger.LoggerSource.service);
			OgranGAAnswer answer = new OgranGAAnswer();
			answer.createAnswer(ga,true,true);

			ChartSerieProperties serie = new ChartSerieProperties();
			serie.Color = ChartColor.GetColorStr(System.Drawing.Color.Black);
			serie.LineWidth = 0;
			serie.SerieType = ChartSerieType.line;
			serie.Marker = true;
			serie.LineWidth = 2;
			serie.Title = "Тек ";
			serie.TagName = "dataWork";
			serie.Enabled = true;
			serie.YAxisIndex = 0;

			//answer.ChartAnswer = KPDLine.createKPDTable(ga);
			answer.ChartAnswer = ChartAnswer.getEmptyAnswer();		
			answer.ChartAnswer.AllowZoom = false;
			answer.ChartAnswer.AllowTrack = false;
			answer.ChartAnswer.Properties.Series.Add(serie);
			answer.ChartAnswer.Data.addSerie(answer.CurrentData);
						
			return answer;
		}

		public OgranGAAnswer getOgranGAData(int ga) {
			WebLogger.Info("OgranGAData process", VotGES.Logger.LoggerSource.service);
			OgranGAAnswer answer = new OgranGAAnswer();
			answer.createAnswer(ga, false, true);
			//answer.ChartAnswer.Data.addSerie(answer.CurrentData);
			
			return answer;
		}

		public ChartAnswer getPuskStopFull(DateTime dateStart, DateTime dateEnd) {
			WebLogger.Info(String.Format("Получение пусков-остановов график {0} - {1}",dateStart,dateEnd), VotGES.Logger.LoggerSource.service);
			PuskStopFullReport report = new PuskStopFullReport();
			report.DateStart = dateStart;
			report.DateEnd = dateEnd>DateTime.Now.AddHours(-2)?GlobalVotGES.getMoscowTime(DateTime.Now):dateEnd;
			report.ReadData();
			return report.createChart();

		}
	}
}


