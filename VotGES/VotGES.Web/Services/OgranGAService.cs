
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



	// TODO: создайте методы, содержащие собственную логику приложения.
	[EnableClientAccess()]
	public class OgranGAService : DomainService {
		public OgranGAAnswer getOgranGAAnswer(int ga) {
			WebLogger.Info("OgranGATable process", VotGES.Logger.LoggerSource.service);
			OgranGAAnswer answer = new OgranGAAnswer();
			answer.createAnswer(ga);

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


			answer.ChartAnswer.Properties.Series.Add(serie);
			answer.ChartAnswer.Data.addSerie(getCurrent(ga));
			return answer;
		}

		protected ChartDataSerie getCurrent(int ga) {
			ChartDataSerie data = new ChartDataSerie();
			Random rand=new Random();
			data.Points.Add(new ChartDataPoint(rand.Next(60,100), rand.Next(17,18)));
			data.Points.Add(new ChartDataPoint(rand.Next(60, 100), rand.Next(17, 18)));
			data.Points.Add(new ChartDataPoint(rand.Next(60, 100), rand.Next(17, 18)));
			data.Name = "dataWork";
			return data;
		}

		public ChartDataSerie getOgranGAData(int ga) {
			WebLogger.Info("OgranGAData process", VotGES.Logger.LoggerSource.service);
			return getCurrent(ga);
			
		}
	}
}


