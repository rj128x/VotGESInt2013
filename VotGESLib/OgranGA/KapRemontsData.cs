﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VotGES.XMLSer;

namespace VotGES.OgranGA {
	public class KapRemontsRecord {
		public int GA { get; set; }
		public DateTime Date { get; set; }
	}
	public class KapRemontsData {
		public List<KapRemontsRecord> Data { get; set; }
		public static KapRemontsData Single { get; protected set; }
		public static void init(string filename = null) {

			if (filename == null) {
				filename = "Data\\KapRemontsData.xml";
			}			
			KapRemontsData settings = XMLSer<KapRemontsData>.fromXML(filename);
			KapRemontsData.Single= settings;
			
		}
		public KapRemontsData() {
			Data = new List<KapRemontsRecord>();			
		}
		public void createXML() {
			Data = new List<KapRemontsRecord>();
			for (int ga = 1; ga <= 10; ga++) {
				KapRemontsRecord rec = new KapRemontsRecord();
				rec.GA = ga;
				rec.Date = DateTime.Now;
			}
			XMLSer<KapRemontsData>.toXML(this,"Data\\KapRemontsData.xml");
		}
	}
}