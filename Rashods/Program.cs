using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VotGES.Rashod;

namespace Rashods
{
	class Program
	{
		static void Main(string[] args) {
			int ga = Int32.Parse(args[0]);
			double napor = 13;
			double power = 0;
			string text = "";
			string firstString = "X";
			while (power <= 120) {
				napor = 13;
				text += String.Format("{0:0}", power);
				while (napor <= 23.05) {
					if (power == 0) {
						firstString += string.Format(";{0:0.0}", napor);
					}
					text += string.Format(";{0:0.00}", InerpolationRashod.getRashodGA(ga,power, napor));
					napor += 0.1;
				}
				text += "\r\n";
				power += 1;
			}
			Console.WriteLine(firstString+"\r\n"+text);
		}
	}
}
