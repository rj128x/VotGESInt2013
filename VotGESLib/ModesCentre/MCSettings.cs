using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VotGES.XMLSer;

namespace VotGES.ModesCentre {
	public class MCSettingsRecord {
		public int MCCode { get; set; }
		public int PiramidaCode { get; set; }
		public bool WriteIntegratedData { get; set; }
		public bool Autooper { get; set; }
	}

	public class MCMaketVar {
		public int PiramidaCode { get; set; }
		public int MCCode { get; set; }
	}

	public class MCMaketRecord {
		public int MCcode { get; set; }
		public int GA { get; set; }
		public List<MCMaketVar> Vars { get; set; }
	}

	public class MCSettings {
		public string MCServer { get; set; }
		public string MCUser { get; set; }
		public string MCPassword { get; set; }
		public List<MCSettingsRecord> MCData { get; set; }
		public List<MCMaketRecord> MaketData { get; set; }
		public static MCSettings Single { get; protected set; }

		public string SMTPServer { get; set; }
		public string SMTPDomain { get; set; }
		public int SMTPPort { get; set; }
		public string SMTPUser { get; set; }
		public string SMTPPassword { get; set; }
		public string SMTPFrom { get; set; }
		public string AutooperMail { get; set; }
		public string ErrorMail { get; set; }
		public string InfoMail { get; set; }

		public static void init(string filename = null) {
			if (filename == null) {
				filename = "Data\\DBSettings.xml";
			}
			MCSettings settings = XMLSer<MCSettings>.fromXML(filename);

			Single = settings;
		}
	}
}
