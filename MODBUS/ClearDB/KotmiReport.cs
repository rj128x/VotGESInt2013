using KotmiLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace ClearDB
{
	public class KotmiReport
	{
		public static void ReadData(DateTime dateStart, DateTime dateEnd, int stepSeconds, string mode, string fields,string fileName) {
			KOTMISettings.init(Directory.GetCurrentDirectory().ToString() + "\\Data\\KOTMISettings.xml");
			string[] fieldsArr = fields.Split(new char[] { '~' });
			List<ArcField> Fields = new List<ArcField>();
			foreach (string fieldStr in fieldsArr) {
				Fields.Add(KOTMISettings.Single.KotmiDict[fieldStr]);
			}
			KotmiResult res = new KotmiResult(dateStart, dateEnd, Fields, stepSeconds, mode);
			res.ReadData();
			BinaryFormatter binFormat = new BinaryFormatter();
			using (Stream fStream = new FileStream(fileName,
			FileMode.Create, FileAccess.Write, FileShare.None)) {
				binFormat.Serialize(fStream, res);
			}

		}
	}
}
