﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using VotGES.Piramida;

namespace VotGES.OgranGA
{

    public class OgranGARecord
    {
        public enum ITEM_ENUM { rezNone, pAfterMax, pLessMin, rezSK, rezGen, rezHHT, rezHHG, rezRun }
        public int GA { get; set; }
        public DateTime dateStart { get; set; }
        public DateTime dateEnd { get; set; }
        public int cntPusk { get; set; }
        public int cntStop { get; set; }
        public int cntAfterMax { get; set; }
        public int cntLessMin { get; set; }        
        public double timeSK { get; set; }
        public double timeGen { get; set; }
        public double timeRun { get; set; }
        public double timeHHT { get; set; }
        public double timeHHG { get; set; }
        public double timeLessMin { get; set; }
        public double timeAfterMax { get; set; }

        public static double dateDiff(DateTime dateStart, DateTime dateEnd)
        {
            return (dateEnd.Ticks - dateStart.Ticks) / (10000000.0 * 60.0);
        }

        public static ITEM_ENUM getItemType(int item)
        {
            if (item >= 101 && item <= 110)
                return ITEM_ENUM.pAfterMax;
            if (item >= 201 && item <= 210)
                return ITEM_ENUM.pLessMin;
            if (item >= 301 && item <= 310)
                return ITEM_ENUM.rezSK;
            if (item >= 401 && item <= 410)
                return ITEM_ENUM.rezGen;
            if (item >= 501 && item <= 510)
                return ITEM_ENUM.rezHHG;
            if (item >= 601 && item <= 610)
                return ITEM_ENUM.rezHHT;
            if (item >= 701 && item <= 710)
                return ITEM_ENUM.rezRun;
            return ITEM_ENUM.rezNone;
        }

    }

    public class OgranGA
    {
        protected static OgranGARecord calcOgran(DateTime dateStart, DateTime dateEnd, int gaNumber, List<PiramidaEnrty> data)
        {
            OgranGARecord result = new OgranGARecord();
            result.GA = gaNumber;

            double timeSKToEnd = 0;
            double timeGenToEnd = 0;
            double timeRunToEnd = 0;
            double timeHHTToEnd = 0;
            double timeHHGToEnd = 0;
            double timeAfterMaxToEnd = 0;
            double timeLessMinToEnd = 0;

            foreach (PiramidaEnrty record in data)
            {
                double diffFromStart = OgranGARecord.dateDiff(dateStart, record.Date);
                double addTime = 0;
                int addCnt = 0;
                double timeToEnd = 0;
                if (record.Value0 == 0)
                {
                    if (record.Value1 > diffFromStart)
                        addTime += diffFromStart;
                    else
                        addTime += record.Value1;
                }
                else
                {
                    addCnt++;
                    timeToEnd = OgranGARecord.dateDiff(record.Date, dateEnd);
                }

                OgranGARecord.ITEM_ENUM itemType = OgranGARecord.getItemType(record.Item);
                switch (itemType)
                {
                    case (OgranGARecord.ITEM_ENUM.pAfterMax):
                        result.timeAfterMax += addTime;
                        result.cntAfterMax += addCnt;
                        timeAfterMaxToEnd = timeToEnd;
                        break;
                    case (OgranGARecord.ITEM_ENUM.pLessMin):
                        result.timeLessMin += addTime;
                        result.cntLessMin += addCnt;
                        timeLessMinToEnd = timeToEnd;
                        break;
                    case (OgranGARecord.ITEM_ENUM.rezGen):
                        result.timeGen += addTime;
                        timeGenToEnd += timeToEnd;
                        break;
                    case (OgranGARecord.ITEM_ENUM.rezHHG):
                        result.timeHHG += addTime;
                        timeHHGToEnd = timeToEnd;
                        break;
                    case (OgranGARecord.ITEM_ENUM.rezHHT):
                        result.timeHHT += addTime;
                        timeHHTToEnd = timeToEnd;
                        break;
                    case (OgranGARecord.ITEM_ENUM.rezRun):
                        result.cntStop += addCnt;
                        result.cntPusk += (1 - addCnt);
                        timeRunToEnd = timeToEnd;
                        break;
                }
            }

            result.timeAfterMax += timeAfterMaxToEnd;
            result.timeLessMin += timeLessMinToEnd;
            result.timeHHG += timeHHGToEnd;
            result.timeHHT += timeHHTToEnd;
            result.timeRun += timeRunToEnd;
            result.timeSK += timeSKToEnd;
            return result;
        }

        protected static void procesPuskStopData(DateTime dateStart, DateTime dateEnd, int minutes, int gaNumber)
        {
            List<int> items = new List<int>();
            for (int h = 1; h <= 7; h++)
            {
                items.Add(h * 100 + gaNumber);
            }

            Logger.Info("====Чтение исходных данных", Logger.LoggerSource.service);
            List<PiramidaEnrty> data = PiramidaAccess.GetDataFromDB(dateStart, dateEnd, 30, 2, 13, items, true, true, "PSV");
            List<OgranGARecord> result = new List<OgranGARecord>();

            DateTime date = dateStart.AddHours(0);
            List<PiramidaEnrty> smallData = new List<PiramidaEnrty>();
            while (date < dateEnd)
            {
                DateTime de = date.AddMinutes(minutes);
                foreach (PiramidaEnrty rec in data)
                {
                    if (rec.Date > date && rec.Date <= de)
                        smallData.Add(rec);
                }
                OgranGARecord resultRecord = calcOgran(date, de, gaNumber, smallData);
                smallData.Clear();
                result.Add(resultRecord);

                date = de.AddMinutes(0);
            }
            writeToDB(result);
        }

        public static void processData(DateTime dateStart, DateTime dateEnd, int minutes)
        {
            Logger.Info("Обработка пусков-остановов", Logger.LoggerSource.service);
            for (int ga = 1; ga <= 10; ga++)
            {
                Logger.Info("===GA" + ga, Logger.LoggerSource.service);
                procesPuskStopData(dateStart, dateEnd, minutes, ga);
            }
        }



        protected static void writeToDB(List<OgranGARecord> data)
        {
            SqlCommand command = null;
            Logger.Info("====запись пусков-остановов", Logger.LoggerSource.service);
            SqlConnection connection = PiramidaAccess.getConnection("PSV");
            command = connection.CreateCommand();
            connection.Open();
            try
            {
                string query = "DELETE FROM PuskStopTable WHERE";
                string qOR = "(dateStart='{0}' and dateEnd='{1}' and gaNumber={2})";
                List<string> qList = new List<string>();

                int index = 0;
                foreach (OgranGARecord record in data)
                {
                    string q = String.Format(qOR, record.dateStart.ToString(DBInfo.DateFormat), record.dateEnd.ToString(DBInfo.DateFormat), record.GA);
                    qList.Add(q);
                    index++;

                    if (index == 20 || record == data.Last())
                    {
                        command.CommandText = query + String.Join(" OR ", qList);
                        int count = command.ExecuteNonQuery();
                        Logger.Info(String.Format("=====Удаление пусков-остановов: {0}", count), Logger.LoggerSource.service);
                        qList.Clear();
                        index = 0;
                    }
                }


                string insertStr = "INSERT INTO PuskStopTable (dateStart,dateEnd,gaNumber,cntPusk,cntStop,cntAfterMax,cntLessMin,timeSK,timeGen,timeAfterMax,timeLessMin,timeRun,timeHHT,timeHHG)";
                string dataFormat = "SELECT '{0}','{1}', {2}, {3}, {4}, {5}, {6}, {7}, {8}, {9}, {10}, {11}, {12}, {13}";
                List<string> insertList = new List<string>();
                index = 0;
                foreach (OgranGARecord record in data)
                {
                    string insert = string.Format(dataFormat, record.dateStart, record.dateEnd, record.GA, record.cntPusk, record.cntStop, record.cntAfterMax, record.cntLessMin,
                        record.timeSK, record.timeGen, record.timeAfterMax, record.timeLessMin, record.timeRun, record.timeHHT, record.timeHHG);
                    insertList.Add(insert);
                    if (index == 20 || record == data.Last())
                    {
                        command.CommandText = insertStr + " \n " + String.Join(" \nUNION ALL\n ", insertList);
                        int count = command.ExecuteNonQuery();
                        Logger.Info(String.Format("======запись пусков-остановов: {0}", count), Logger.LoggerSource.service);
                        insertList.Clear();
                        index = 0;
                    }
                }
            }
            finally
            {
                try { command.Dispose(); }
                catch { }
                try { connection.Close(); }
                catch { }
            }
        }




    }
}