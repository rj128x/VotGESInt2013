
namespace VotGES.Web.Services {
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.ComponentModel.DataAnnotations;
	using System.Linq;
	using System.ServiceModel.DomainServices.Hosting;
	using System.ServiceModel.DomainServices.Server;
	using VotGES.PBR;
	using VotGES.Piramida.Report;
	using VotGES.Piramida;
	using VotGES.Chart;
	using VotGES.Rashod;

	public class FullGraphVyrab {
		public GraphVyrabAnswer GTP { get; set; }
		public GraphVyrabRGEAnswer RGE { get; set; }
		public Dictionary<int, string> TimeStopGA { get; set; }
		public double Napor { get; set; }
		public double QOpt { get; set; }
		public double QFakt { get; set; }
		public double QVER { get; set; }
		public FullGraphVyrab() {
			Napor = 21;
			QOpt = 0;
			QFakt = 0;
			QVER = 0;
		}
	}

	// TODO: создайте методы, содержащие собственную логику приложения.
	[EnableClientAccess()]
	public class GraphVyrabDomainService : DomainService {
		public GraphVyrabAnswer getGraphVyrab(bool steppedPBR = true) {
			try {
				DateTime date = GlobalVotGES.getMoscowTime(DateTime.Now); 
				return GraphVyrab.getAnswer(date, true, steppedPBR);
			}
			catch (Exception e) {
				Logger.Error("Ошибка при получении графика нагрузки " + e);
				return null;
			}
		}

		public GraphVyrabAnswer getGraphVyrabMin(DateTime date, bool steppedPBR = true) {
			try {
				Logger.Info("Получение факта нагрузки по минутам " + date.ToString());
				date = date.Date;
				return GraphVyrab.getAnswer(date, false, steppedPBR);
			}
			catch (Exception e) {
				Logger.Error("Ошибка при получении факта нагрузки " + e);
				return null;
			}
		}

		public CheckGraphVyrabAnswer getGraphVyrabHH(DateTime date) {
			try {
				Logger.Info("Получение факта нагрузки по получасовкам " + date.ToString());
				date = date.Date;
				return GraphVyrab.getAnswerHH(date);
			}
			catch (Exception e) {
				Logger.Error("Ошибка при получении факта нагрузки " + e);
				return null;
			}
		}

		public GraphVyrabRGEAnswer getGraphVyrabRGE(bool steppedPBR = true) {
			try {
				DateTime date = GlobalVotGES.getMoscowTime(DateTime.Now);
				return GraphVyrabRGE.getAnswer(date, true, steppedPBR);
			}
			catch (Exception e) {
				Logger.Error("Ошибка при получении графика нагрузки РГЕ" + e);
				return null;
			}
		}

		public GraphVyrabRGEAnswer getGraphVyrabRGEMin(DateTime date, bool steppedPBR = true) {
			try {
				Logger.Info("Получение факта нагрузки по минутам РГЕ " + date.ToString());
				date = date.Date;
				return GraphVyrabRGE.getAnswer(date, false, steppedPBR);
			}
			catch (Exception e) {
				Logger.Error("Ошибка при получении факта нагрузки РГЕ" + e);
				return null;
			}
		}

		public CheckGraphVyrabRGEAnswer getGraphVyrabRGEHH(DateTime date) {
			try {
				Logger.Info("Получение факта нагрузки по получасовкам РГЕ " + date.ToString());
				date = date.Date;
				return GraphVyrabRGE.getAnswerHH(date);
			}
			catch (Exception e) {
				Logger.Error("Ошибка при получении факта нагрузки РГЕ" + e);
				return null;
			}
		}

		public FullGraphVyrab getFullGraphVyrab(bool steppedPBR = true) {
			Logger.Info("Получение графика нагрузки stairs:" + steppedPBR);
			FullGraphVyrab answer = new FullGraphVyrab();
			answer.GTP = getGraphVyrab(steppedPBR);
			answer.RGE = getGraphVyrabRGE(steppedPBR);

			answer.TimeStopGA = new Dictionary<int, string>();
			for (int ga = 1; ga <= 10; ga++) {
				answer.TimeStopGA = OgranGA.OgranGA.GetTimeStopGA(DateTime.Now.AddHours(-2));
			}

			try {
				List<PiramidaEnrty> list = PiramidaAccess.GetDataFromDB(DateTime.Now.AddMinutes(-130), DateTime.Now.AddHours(-2), 3, 2, 4, (new int[] { 1,4 }).ToList(), true, true, "PMin");
				foreach (PiramidaEnrty entry in list) {
					switch (entry.Item) {
						case 1:
							answer.QFakt = entry.Value0;
							break;
						case 4:
							answer.Napor = entry.Value0;
							break;
					}					
				}
				double p1 = answer.GTP.TableCurrent[1].GTP1;
				double p2 = answer.GTP.TableCurrent[1].GTP2;
				List<int> gtp1 = new List<int>(new int[] { 1, 2 });
				List<int> gtp2 = new List<int>(new int[] { 3, 4, 5, 6, 7, 8, 9, 10 });
				List<int> ges = new List<int>(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 });
				answer.QOpt = RUSA.getOptimRashod(p1, answer.Napor, true, null, gtp1) + RUSA.getOptimRashod(p2, answer.Napor, true, null, gtp2);
				answer.QVER = answer.QFakt > 0 ? answer.QOpt / answer.QFakt * 100 : answer.QOpt == 0 ? 100 : 0;
				
			}
			catch (Exception e) {
				Logger.Error("Ошибка при получении напора" + e);
			}

			

			return answer;
		}
	}
}
