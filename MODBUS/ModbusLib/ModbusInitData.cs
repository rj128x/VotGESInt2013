using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using VotGES;

namespace ModbusLib {
	public enum RWModeEnum { hh, min }

	public class ServerInfo {
		public string IP { get; set; }
		public ushort Port { get; set; }
	}

	public class ModbusInitData {
		[System.Xml.Serialization.XmlAttribute]
		public string ID { get; set; }

		[System.Xml.Serialization.XmlAttribute]
		public string Name { get; set; }

		[System.Xml.Serialization.XmlAttribute]
		public int Addr { get; set; }

		[System.Xml.Serialization.XmlAttribute]
		public double Scale { get; set; }

		[System.Xml.Serialization.XmlAttribute]
		public bool WriteToDBMin { get; set; }

		[System.Xml.Serialization.XmlAttribute]
		public bool WriteToDBSec { get; set; }

		[System.Xml.Serialization.XmlAttribute]
		public bool WriteToDBHH { get; set; }

		[System.Xml.Serialization.XmlAttribute]
		public bool WriteToDBDiff { get; set; }

		[System.Xml.Serialization.XmlAttribute]
		public int ParNumberMin { get; set; }

		[System.Xml.Serialization.XmlAttribute]
		public int ParNumberHH { get; set; }

		[System.Xml.Serialization.XmlAttribute]
		public int ParNumberDiff { get; set; }

		[System.Xml.Serialization.XmlAttribute]
		public int ParNumberSec { get; set; }

		[System.Xml.Serialization.XmlAttribute]
		public int Obj { get; set; }

		[System.Xml.Serialization.XmlAttribute]
		public int ObjType { get; set; }

		[System.Xml.Serialization.XmlAttribute]
		public string DBNameMin { get; set; }

		[System.Xml.Serialization.XmlAttribute]
		public string DBNameHH { get; set; }

		[System.Xml.Serialization.XmlAttribute]
		public string DBNameDiff { get; set; }

		[System.Xml.Serialization.XmlAttribute]
		public string DBNameSec { get; set; }

		[System.Xml.Serialization.XmlAttribute]
		public int Item { get; set; }

		[System.Xml.Serialization.XmlAttribute]
		public string FuncName { get; set; }

		[System.Xml.Serialization.XmlAttribute]
		public double Diff { get; set; }

		[System.Xml.Serialization.XmlAttribute]
		public double Add { get; set; }

		[System.Xml.Serialization.XmlAttribute]
		public int FlagBit { get; set; }

		[System.Xml.Serialization.XmlAttribute]
		public int ValBit { get; set; }

		[System.Xml.Serialization.XmlAttribute]
		public double MinValue { get; set; }

		[System.Xml.Serialization.XmlAttribute]
		public double MaxValue { get; set; }

		[System.Xml.Serialization.XmlAttribute]
		public bool SignVal { get; set; }

		[System.Xml.Serialization.XmlAttribute]
		public int MinTimeDiff { get; set; }

		
		public ModbusInitData() {
			WriteToDBMin = false;
			WriteToDBHH = false;
			WriteToDBDiff = false;
			WriteToDBSec = false;
			ParNumberMin = -1;
			ParNumberHH = -1;
			ParNumberDiff = -1;
			ParNumberSec = -1;
			Obj = -1;
			ObjType = -1;
			DBNameMin = null;
			DBNameHH = null;
			DBNameDiff = null;
			Item = -1;
			FuncName = null;
			Name = "";
			Diff = 0.1;
			Scale = 1;
			FlagBit = -1;
			ValBit = -1;
			MinValue = Double.MinValue;
			MaxValue = Double.MaxValue;
			SignVal = false;
			MinTimeDiff = 20;
		}
	}

	public class ModbusInitDataArray {
		public string ID { get; set; }
		public List<ServerInfo> ServerInfoArray { get; set; }
		public List<ModbusInitData> Data { get; set; }
		public bool WriteMin { get; set; }
		public bool WriteHH { get; set; }
		public bool IsDiscrete { get; set; }

		[System.Xml.Serialization.XmlIgnore]
		public SortedList<string, ModbusInitData> FullData { get; set; }

		[System.Xml.Serialization.XmlIgnore]
		public int MaxAddr { get; set; }

		public int ParNumberMin { get; set; }
		public int ParNumberHH { get; set; }
		public int ParNumberDiff { get; set; }
		public int ParNumberSec { get; set; }

		public int Obj { get; set; }
		public int ObjType { get; set; }
		public string DBNameMin { get; set; }
		public string DBNameHH { get; set; }
		public string DBNameDiff { get; set; }
		public string DBNameSec { get; set; }


		public void processData() {
			FullData = new SortedList<string, ModbusInitData>();
			MaxAddr = 0;
			foreach (ModbusInitData init in Data) {
				try {
					init.ParNumberHH = init.ParNumberHH == -1 ? ParNumberHH : init.ParNumberHH;
					init.ParNumberMin = init.ParNumberMin == -1 ? ParNumberMin : init.ParNumberMin;
					init.ParNumberDiff = init.ParNumberDiff == -1 ? ParNumberDiff : init.ParNumberDiff;
					init.ParNumberSec = init.ParNumberSec == -1 ? ParNumberSec : init.ParNumberSec;

					init.Obj = init.Obj == -1 ? Obj : init.Obj;
					init.ObjType = init.ObjType == -1 ? ObjType : init.ObjType;
					init.Item = init.Item == -1 ? init.Addr : init.Item;
					init.DBNameHH = init.DBNameHH == null ? DBNameHH : init.DBNameHH;
					init.DBNameMin = init.DBNameMin == null ? DBNameMin : init.DBNameMin;
					init.DBNameDiff = init.DBNameDiff == null ? DBNameDiff : init.DBNameDiff;
					init.DBNameSec = init.DBNameSec == null ? DBNameSec : init.DBNameSec;

					if (init.Name.Contains("_FLAG") && !init.ID.Contains("_FLAG")) {
						init.ID = (init.Addr - 1) + "_FLAG";
					}

					FullData.Add(init.ID, init);
					if (MaxAddr < init.Addr) {
						MaxAddr = init.Addr;
					}
				}
				catch {
					Logger.Error(String.Format("Ошибка при добавлении записи Addr={0} Name={1}", init.Addr, init.Name));
				}
			}

		}

		public static DateTime prevMailDate = DateTime.MinValue;

		public void WriteVal(int addr, double val, SortedList<string, double> DataArray) {			
			foreach (ModbusInitData data in Data) {
				if (data.Addr == addr) {
					if (data.ValBit == -1) {
						if (data.SignVal) {
							try {
								//Logger.Info(data.ID + "  " + val);
								val = Convert.ToInt16(val);
								//Logger.Info("===" + val);
							}
							catch { }
						}
						val = val * data.Scale + data.Add;
					}
					else {
						val=GlobalVotGES.getBIT((short)val,data.ValBit);
					}

					if (data.ID.Contains("PF") && (!data.ID.Contains("_FLAG")) && Math.Abs(val) > 0) {
						Logger.Info("Send Mail");
						if (prevMailDate.AddSeconds(30) < DateTime.Now) {
							SendMail("Работа в ОПРЧ " + data.ID, data.ID);
							prevMailDate = DateTime.Now;
						}
					}

					if (val > data.MaxValue || val < data.MinValue) {
						Logger.Info(String.Format("Выход за границы диапазона {0} val={1}", data.ID, val));
						val = double.NaN;
					}



					if (DataArray.ContainsKey(data.ID)) {
						DataArray[data.ID] = val;
					} else {
						DataArray.Add(data.ID, val);
					}
				}
			}
		}

		public static bool SendMail(string subject, string message) {
			try {
				List<string> mailToList = new List<string>();
				mailToList.Add("ChekunovaMV@votges.rushydro.ru");
				mailToList.Add("FedorkoAV@votges.rushydro.ru");
				System.Net.Mail.MailMessage mess = new System.Net.Mail.MailMessage();

				mess.From = new MailAddress("ChekunovaMV@votges.rushydro.ru");
				mess.Subject = subject; mess.Body = message;
				foreach (string mail in mailToList) {
					mess.To.Add(mail);
				}

				mess.SubjectEncoding = System.Text.Encoding.UTF8;
				mess.BodyEncoding = System.Text.Encoding.UTF8;
				mess.IsBodyHtml = false;
			
				System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient("mx-votges-021", 25);
				client.Host= "mx-votges-021";				
				client.UseDefaultCredentials = false;
				client.Credentials = new System.Net.NetworkCredential("ChekunovaMV", "rJ320204", "CORP");				
				// Отправляем письмо
				client.Send(mess);
				return true;
			}
			catch (Exception e) {
				Logger.Info("Ошибка при отправке почты");
				Logger.Info(e.ToString());
			}
			return false;
		}


	}

}
