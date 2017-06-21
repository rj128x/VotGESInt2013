using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VotGES;
using VotGES.Rashod;

namespace Rashods
{
	class Program
	{
		static void Main(string[] args) {
			Logger.InitFileLogger("C:/logs/", "rashods.txt");
			int ga = Int32.Parse(args[0]);
			bool ish = args[1] == "ish";
			double napor = 13;
			double minPower = ga <= 10 ? 0 : 35;
			double maxPower = ga <= 10 ? 120 : 1100;
			

			double power = minPower;
			string text = "";
			string firstString = "X";
			
			while (power <= maxPower) {
				napor = 13;
				text += String.Format("{0:0}", power);
				while (napor <= 23.05) {
					if (power == minPower) {
						firstString += string.Format(";{0:0.0}", napor);
					}
					double q = ga <= 10 ? InerpolationRashod.getRashodGA(ga, power, napor) : RUSA.getOptimRashod(power, napor);
					if (ga == 12)
						q *= 1.1;
					text += string.Format(";{0:0.00}", q);
					napor += ish?1:0.1;
				}
				text += "\r\n";
				power += ish?10:1;
			}
			Console.WriteLine(firstString+"\r\n"+text);
		}
	}
}
