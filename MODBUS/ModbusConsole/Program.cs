using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ModbusLib;
using System.Net;
using System.IO;
using System.Threading;
using VotGES;
using VotGES.Piramida;
using ModbusLib.DBWriter;
using ModbusLib.ModbusReader;
using ModbusLib.MBLostData;

namespace ModbusConsole
{
	class Program
	{

		static void Main(string[] args) {

			if (args.Length > 0) {
				string fileName = args[0];
				Settings.init();
				DBSettings.init();
				Logger.InitFileLogger(Settings.single.LogPath, "logL");
				MBLostFileReader reader = new MBLostFileReader();
				reader.FileName = fileName;
				reader.init();
				reader.readData();
			} else {
				(new ModbusReaderRunner()).Run();
			}

			Console.ReadLine();
		}

	}
}

