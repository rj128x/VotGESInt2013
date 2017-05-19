using log4net;
using log4net.Appender;
using log4net.Config;
using log4net.Layout;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using VotGES;

namespace KotmiLib
{
	public class XMLSer<T>
	{
		public static void toXML(T obj, string fileName) {
			XmlSerializer mySerializer = new XmlSerializer(typeof(T));
			// To write to a file, create a StreamWriter object.
			StreamWriter myWriter = new StreamWriter(fileName);
			mySerializer.Serialize(myWriter, obj);
			myWriter.Close();
		}

		public static T fromXML(string fileName) {
			try {
				XmlSerializer mySerializer = new XmlSerializer(typeof(T));
				// To read the file, create a FileStream.
				FileStream myFileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
				// Call the Deserialize method and cast to the object type.
				T data = (T)mySerializer.Deserialize(myFileStream);
				myFileStream.Close();
				return data;
			} catch (Exception e) {
				return default(T);
			}
		}
	}

	public class KOTMISettings
	{
		public String Server { get; set; }
		public String User { get; set; }
		public string Password { get; set; }
		public List<String> KotmiFields { get; set; }
		public String DBServer { get; set; }
		public String DBName { get; set; }
		public String DBUser { get; set; }
		public String DBPassword { get; set; }


		public static KOTMISettings Single { get; protected set; }
		public static void init(string filename) {
			try {
				KOTMISettings single = XMLSer<KOTMISettings>.fromXML( filename);
				Single = single;
				//single.KotmiFields = new List<string>();
				/*single.KotmiFields.Add("214235345");
				XMLSer<Settings>.toXML(single, "C:/test.xml");*/
			} catch (Exception e) {
				Logger.Error("Ошибка при чтении файла настроек " + e, Logger.LoggerSource.server);
			}
		}
	}

}
