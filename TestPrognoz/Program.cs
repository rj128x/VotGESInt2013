using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VotGES;
using VotGES.Piramida;
using VotGES.PrognozNB;

namespace TestPrognoz
{
    public class Program
    {
			static void Main(string[] args) {
				Logger.InitFileLogger("C:/prognoz", "logL");
				DBSettings.init();
				TestPrognozClass TP = new TestPrognozClass();
				//TP.readData();
				//TP.SaveInitData("C:/Prognoz/InitFull.ser");
				TP.ReadInitData("C:/Prognoz/InitFull.ser");
				TP.ProcessData();
				TP.SaveResultData("C:/prognoz/Data.ser");
				//TP.ReadResultData("C:/prognoz/Data.ser");
				TP.TestData();
			}
    }
}
