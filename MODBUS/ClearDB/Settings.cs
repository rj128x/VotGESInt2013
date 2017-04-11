using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using VotGES.Piramida;
using VotGES.XMLSer;
using VotGES.Piramida.Report;

namespace ClearDB
{
	public class Settings
	{
		protected static Settings settings;
		public string LogPath { get; set; }
		public string SutVedPath { get; set; }
		public string SutVedPathTo { get; set; }
		public string DBDateFormat { get; set; }

		public string SMTPServer { get; set; }
		public string SMTPDomain { get; set; }
		public int SMTPPort { get; set; }
		public string SMTPUser { get; set; }
		public string SMTPPassword { get; set; }
		public string SMTPFrom { get; set; }
		public string ErrorMailTo { get; set; }
		public string NebalansMailTo { get; set; }

		public NebalansLimits Limits { get; set; }
		public string AvailEmptyNBData { get; set; }
		
		public static Settings single {
			get {
				return settings;
			}
		}


		static Settings() {			
			NFIPoint = new CultureInfo("ru-RU").NumberFormat;
			NFIPoint.NumberDecimalSeparator = ".";
		}
		public static NumberFormatInfo NFIPoint;

		public static void init() {
			System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo("en-GB");
			ci.NumberFormat.NumberDecimalSeparator = ".";
			ci.NumberFormat.NumberGroupSeparator = "";

			System.Threading.Thread.CurrentThread.CurrentCulture = ci;
			System.Threading.Thread.CurrentThread.CurrentUICulture = ci;	
						

			Settings settings=XMLSer<Settings>.fromXML("Data\\Settings.xml");
			Settings.settings = settings;
						
		}
	}
}
