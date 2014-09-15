﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Data.SqlClient;

namespace VotGES.Piramida
{
	public class DBInfo
	{
		private string id;
		public string ID {
			get { return id; }
			set { id = value; }
		}

		private string address;
		public string Address {
			get { return address; }
			set { address = value; }
		}

		private string dbName;
		public string DBName {
			get { return dbName; }
			set { dbName = value; }
		}

		private string user;
		public string User {
			get { return user; }
			set { user = value; }
		}

		private string password;
		public string Password {
			get { return password; }
			set { password = value; }
		}
	}

	public class PiramidaEnrty
	{
		public DateTime Date { get; set; }
		public int ParNumber { get; set; }
		public int Object { get; set; }
		public int ObjType { get; set; }
		public double Value0 { get; set; }
		public int Item { get; set; }
	}

	public class PiramidaAccess
	{
		public static string getConnectionString(DBInfo dbInfo) {
			return String.Format(@"Data Source={0};Initial Catalog={1};Persist Security Info=True;User ID={2};Password={3};Connection Timeout={4}",
				dbInfo.Address, dbInfo.DBName, dbInfo.User, dbInfo.Password, 3);
		}


		static PiramidaAccess() {

		}

		public static List<PiramidaEnrty> GetDataFromDB(DateTime dateStart, DateTime dateEnd, int obj, int objType, int parNumber, List<int> items, bool includeFirst = false, bool includeLast = true, string dbName="P3000") {

			List<PiramidaEnrty> result=new List<PiramidaEnrty>();
			SqlConnection connection=getConnection(dbName);
			SqlDataReader reader=null;
			SqlCommand command=null;
			connection.Open();
			try {
				command = connection.CreateCommand();
				command.Parameters.AddWithValue("@dateStart", dateStart);
				command.Parameters.AddWithValue("@dateEnd", dateEnd);

				string itemsStr=String.Join(",", items);

				string dateStartCond=includeFirst ? ">=" : ">";
				string dateEndCond=includeLast ? "<=" : "<";

				string valueParams=String.Format(" ( d.[DATA_DATE]{0}@dateStart and d.[DATA_DATE]{1}@dateEnd and d.[PARNUMBER]={2} and d.[OBJTYPE]={3} and d.[OBJECT]={4} and d.[ITEM] in ({5}) ) ",
					dateStartCond, dateEndCond, parNumber, objType, obj, itemsStr);


				command.CommandText = String.Format("SELECT d.[DATA_DATE], d.[OBJECT], d.[OBJTYPE], d.[ITEM], d.[PARNUMBER], d.[VALUE0] from DATA as d  WHERE {0}", valueParams);



				reader = command.ExecuteReader();

				while (reader.Read()) {
					PiramidaEnrty entry=new PiramidaEnrty();
					entry.Date = reader.GetDateTime(0);
					entry.Object = reader.GetInt32(1);
					entry.ObjType = reader.GetInt16(2);
					entry.Item = reader.GetInt32(3);
					entry.ParNumber = reader.GetInt32(4);
					entry.Value0 = reader.GetDouble(5);

					result.Add(entry);
				}

			} finally {
				try { reader.Close(); } catch { }
				try { command.Dispose(); } catch { }
				try { connection.Close(); } catch { }
			}

			return result;
		}


		public static SqlConnection getConnection(string dbName) {
			SqlConnection con=new SqlConnection(getConnectionString(DBSettings.single.DBInfoList[dbName]));			
			return con;
		}



		
	}
}
