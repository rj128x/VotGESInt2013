﻿using System;
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

	public class MCSettings {
		public string MCServer { get; set; }
		public string MCUser { get; set; }
		public string MCPassword { get; set; }
		public List<MCSettingsRecord> MCData { get; set; }
		public static MCSettings Single { get; protected set; }

		public string SMTPServer { get; set; }
		public string SMTPDomain { get; set; }
		public int SMTPPort { get; set; }
		public string SMTPUser { get; set; }
		public string SMTPPassword { get; set; }
		public string SMTPFrom { get; set; }
		public string AutooperMail { get; set; }
		
		public static void init() {
			//чтение настроек из xml
			MCSettings settings = XMLSer<MCSettings>.fromXML("Data\\Settings.xml");

			Single = settings;
		}
	}
}