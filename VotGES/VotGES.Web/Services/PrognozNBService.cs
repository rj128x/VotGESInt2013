﻿
namespace VotGES.Web.Services
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.ComponentModel.DataAnnotations;
	using System.Data;
	using System.Linq;
	using System.ServiceModel.DomainServices.EntityFramework;
	using System.ServiceModel.DomainServices.Hosting;
	using System.ServiceModel.DomainServices.Server;
	using VotGES.Piramida;
	using VotGES.Chart;
	using VotGES.PrognozNB;
	using VotGES.Web.Logging;


	// Реализует логику приложения с использованием контекста Piramida3000Entities.
	// TODO: добавьте свою прикладную логику в эти или другие методы.
	// TODO: включите проверку подлинности (Windows/ASP.NET Forms) и раскомментируйте следующие строки, чтобы запретить анонимный доступ
	// Кроме того, рассмотрите возможность добавления ролей для соответствующего ограничения доступа.
	// [RequiresAuthentication]
	[EnableClientAccess()]
	public class PrognozNBService : DomainService
	{

		public CheckPrognozNBAnswer checkPrognozNB(DateTime date, int countDays, bool isQFakt, PrognozNBInitData initData) {
			WebLogger.Info(String.Format("Получение прогноза (факт) {0} - {1}", date, countDays));
			try {
				if (date.AddHours(-2).Date >= DateTime.Now.Date)
					date = DateTime.Now.AddHours(-2).Date.AddHours(-24);
							
				
				CheckPrognozNB prognoz=new CheckPrognozNB(date.Date,countDays,isQFakt);
				//PrognozNBByPBR prognoz=new PrognozNBByPBR(date.Date, 1, date.Date.AddHours(8).AddMinutes(15),null);
				prognoz.startPrognoz(initData);
				CheckPrognozNBAnswer answer = new CheckPrognozNBAnswer();
				answer.Chart = prognoz.getChart();
				answer.InitData = prognoz.Prognoz.InitData;
				return answer;
			} catch (Exception e) {
				WebLogger.Error(e.ToString());
				return null;
			}
		}

		public PrognozNBByPBRAnswer getPrognoz( int countDays,bool maxQ, SortedList<DateTime,double> pbr, PrognozNBInitData initData) {
			WebLogger.Info(String.Format("Получение прогноза (прогноз) {0} [{1}]", countDays, pbr == null ? "" : String.Join(" ", pbr)));
			try {
				WebLogger.Info(maxQ.ToString());
				DateTime date= GlobalVotGES.getMoscowTime(DateTime.Now);
				//DateTime date=new DateTime(2010, 03, 15);
				//date = date.AddHours(13).AddMinutes(35);
				PrognozNBByPBR prognoz=new PrognozNBByPBR(date.Date,countDays,date,pbr);
				prognoz.QMax = maxQ;
				prognoz.startPrognoz(initData);
				return prognoz.PrognozAnswer;
			} catch (Exception e) {
				WebLogger.Error(e.ToString());
				return null;
			}
		}
	}
}


