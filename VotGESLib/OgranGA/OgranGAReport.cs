using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using VotGES.Piramida;

namespace VotGES.OgranGA
{
    public class OgranGAReport
    {
        protected Dictionary<int, List<OgranGARecord>> data;
        protected Dictionary<int, OgranGARecord> sumData;
        protected DateTime DateStart { get; set; }
        protected DateTime DateEnd { get; set; }

        public Dictionary<int, List<OgranGARecord>> Data
        {
            get { return data; }
        }

        public Dictionary<int, OgranGARecord> SumData
        {
            get { return sumData; }
        }
        

        public OgranGAReport(DateTime dateStart, DateTime dateEnd)
        {
            DateStart = dateStart;
            DateEnd = dateEnd;
            data = new Dictionary<int, List<OgranGARecord>>();
            sumData = new Dictionary<int, OgranGARecord>();
            for (int ga = 1; ga <= 10; ga++)
            {
                data.Add(ga, new List<OgranGARecord>());
                sumData.Add(ga, new OgranGARecord());
            }
        }

        public void readData()
        {
            SqlDataReader reader = null;
            SqlCommand command = null;
            SqlConnection connection = PiramidaAccess.getConnection("PSV");

            command = connection.CreateCommand();
            command.Parameters.AddWithValue("@dateStart", DateStart);
            command.Parameters.AddWithValue("@dateEnd", DateEnd);
            connection.Open();
            command.CommandText = "Select gaNumber,dateStart,dateEnd,cntPusk,cntStop,cntAfterMax,cntLessMin,timeSK,timeGen,timeAfterMax,timeLessMin,timeRun,timeHHT,timeHHG from PuskStopTable Where dateStart>=@dateStart and dateEnd<=@dateStart";
            try
            {
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    OgranGARecord rec = new OgranGARecord();
                    rec.GA = reader.GetInt32(0);
                    rec.dateStart = reader.GetDateTime(1);
                    rec.dateEnd = reader.GetDateTime(2);
                    rec.cntPusk = reader.GetInt32(4);
                    rec.cntStop = reader.GetInt32(5);
                    rec.cntAfterMax = reader.GetInt32(6);
                    rec.cntLessMin = reader.GetInt32(7);
                    rec.timeSK = reader.GetInt32(8);
                    rec.timeGen = reader.GetInt32(9);
                    rec.timeAfterMax = reader.GetInt32(10);
                    rec.timeLessMin = reader.GetInt32(11);
                    rec.timeRun = reader.GetInt32(12);
                    rec.timeHHT = reader.GetInt32(13);
                    rec.timeHHG = reader.GetInt32(14);

                    data[rec.GA].Add(rec);
                }
            }
            finally
            {
                try { reader.Close(); }
                catch { }
                try { command.Dispose(); }
                catch { }
                try { connection.Close(); }
                catch { }
            }
        }

        public void readSumData()
        {
            SqlDataReader reader = null;
            SqlCommand command = null;
            SqlConnection connection = PiramidaAccess.getConnection("PSV");
            command = connection.CreateCommand();
            command.Parameters.AddWithValue("@dateStart", DateStart);
            command.Parameters.AddWithValue("@dateEnd", DateEnd);
            connection.Open();
            command.CommandText = "Select gaNumber,sum(cntPusk),sum(cntStop),sum(cntAfterMax),sum(cntLessMin),sum(timeSK),sum(timeGen),sum(timeAfterMax),sum(timeLessMin),sum(timeRun),sum(timeHHT),sum(timeHHG) from PuskStopTable Where dateStart>=@dateStart and dateEnd<=@dateStart group by gaNumber";
            try
            {
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    OgranGARecord rec = new OgranGARecord();
                    rec.GA = reader.GetInt32(0);
                    rec.dateStart = DateStart;
                    rec.dateEnd = DateEnd;
                    rec.cntPusk = reader.GetInt32(1);
                    rec.cntStop = reader.GetInt32(2);
                    rec.cntAfterMax = reader.GetInt32(3);
                    rec.cntLessMin = reader.GetInt32(4);
                    rec.timeSK = reader.GetInt32(5);
                    rec.timeGen = reader.GetInt32(6);
                    rec.timeAfterMax = reader.GetInt32(7);
                    rec.timeLessMin = reader.GetInt32(8);
                    rec.timeRun = reader.GetInt32(9);
                    rec.timeHHT = reader.GetInt32(10);
                    rec.timeHHG = reader.GetInt32(11);

                    sumData.Add(rec.GA, rec);
                }
            }
            finally
            {
                try { reader.Close(); }
                catch { }
                try { command.Dispose(); }
                catch { }
                try { connection.Close(); }
                catch { }
            }
        }

    }
}
