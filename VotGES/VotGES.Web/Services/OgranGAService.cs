
namespace VotGES.Web.Services
{
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
	public class OgranGAService : DomainService
	{
        public OgranGAAnswer getOgranGAAnswer(int ga)
        {
            WebLogger.Info("OgranGATable process", VotGES.Logger.LoggerSource.service);
            OgranGAAnswer answer = new OgranGAAnswer();
            answer.createAnswer(ga);
            return answer;
        }
	}
}


