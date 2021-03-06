﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;
using VotGES.Piramida;
using VotGES.XMLSer;

namespace ModbusLib
{
	public class Settings
	{
		protected static Settings settings;
		public string LogPath { get; set; }
		public string DataPath { get; set; }
		public List<string> AddDataPath { get; set; }
		
		public static Settings single {
			get {
				return settings;
			}
		}
		public int HoursDiff { get; set; }
		public List<String> InitFiles{ get; set; }
		public string DBDateFormat { get; set; }
		public string InitCalcFile { get; set; }

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
