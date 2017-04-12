using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VotGES.Piramida.Report
{
	public class NebalansLimits
	{
		public int NB500Min {	get; set;	}
		public int NB500Max{ get; set; }
		public int NB110Min{ get; set; }
		public int NB110Max{ get; set; }
		public int NB220Min{ get; set; }
		public int NB220Max{ get; set; }
		public int NBVLMin{ get; set; }
		public int NBVLMax{ get; set; }
		public int NB1TMin{ get; set; }
		public int NB1TMax{ get; set; }
		public int NB2ATMin{ get; set; }
		public int NB2ATMax{ get; set; }
		public int NB3ATMin{ get; set; }
		public int NB3ATMax{ get; set; }
		public int NB4TMin{ get; set; }
		public int NB4TMax{ get; set; }
		public int NB56ATMin{ get; set; }
		public int NB56ATMax{ get; set; }
		public int NBTransMin{ get; set; }
		public int NBTransMax{ get; set; }
		public int NBGESMin{ get; set; }
		public int NBGESMax{ get; set; }
		public int SPMin { get; set; }
		public int SPMax { get; set; }
		public int SNMin { get; set; }
		public int SNMax { get; set; }
	}
	public class ReportNebalans
	{
		public bool FoundNebalans = false;
		public FullReport report;
		public ReportNebalans(DateTime dateStart, DateTime dateEnd) {
			Logger.Info(String.Format("Получение небаланса с {0} по {1} ", dateStart.ToString("dd.MM.yyyy HH:mm"), dateEnd.ToString("dd.MM.yyyy HH:mm")));
			report = new FullReport(dateStart, dateEnd, IntervalReportEnum.halfHour, FullReportMembersType.def);
			report.UsePiramida2000 = true;
			report.RecordTypes[ReportLinesRecords.P_VL500_Nebalans.ID].Visible = true;
			report.RecordTypes[ReportLinesRecords.P_VL110_Nebalans.ID].Visible = true;
			report.RecordTypes[ReportLinesRecords.P_VL220_Nebalans.ID].Visible = true;
			report.RecordTypes[ReportLinesRecords.P_VL_Nebalans.ID].Visible = true;
			report.RecordTypes[ReportGlTransformRecords.P_1T_Nebalans.ID].Visible = true;
			report.RecordTypes[ReportGlTransformRecords.P_4T_Nebalans.ID].Visible = true;
			report.RecordTypes[ReportGlTransformRecords.P_2AT_Nebalans.ID].Visible = true;
			report.RecordTypes[ReportGlTransformRecords.P_3AT_Nebalans.ID].Visible = true;
			report.RecordTypes[ReportGlTransformRecords.P_56AT_Nebalans.ID].Visible = true;
			report.RecordTypes[ReportGlTransformRecords.P_T_Nebalans.ID].Visible = true;
			report.RecordTypes[ReportMainRecords.P_Nebalans.ID].Visible = true;
			report.RecordTypes[ReportMainRecords.P_SP.ID].Visible = true;
			report.RecordTypes[ReportSNRecords.P_SN.ID].Visible = true;

			report.RecordTypes[PiramidaRecords.P_IKM_Nebalans110.Key].Visible = true;
			report.RecordTypes[PiramidaRecords.P_IKM_Nebalans220.Key].Visible = true;
			report.RecordTypes[PiramidaRecords.P_IKM_Nebalans500.Key].Visible = true;
			report.RecordTypes[PiramidaRecords.P_IKM_Nebalans1T.Key].Visible = true;
			report.RecordTypes[PiramidaRecords.P_IKM_Nebalans4T.Key].Visible = true;
			report.RecordTypes[PiramidaRecords.P_IKM_Nebalans2AT.Key].Visible = true;
			report.RecordTypes[PiramidaRecords.P_IKM_Nebalans3AT.Key].Visible = true;
			report.RecordTypes[PiramidaRecords.P_IKM_Nebalans56AT.Key].Visible = true;
			report.RecordTypes[PiramidaRecords.P_IKM_NebalansT.Key].Visible = true;
			report.RecordTypes[PiramidaRecords.P_IKM_Nebalans_GES.Key].Visible = true;
			report.RecordTypes[PiramidaRecords.P_IKM_SN.Key].Visible = true;
			report.RecordTypes[PiramidaRecords.P_IKM_SP.Key].Visible = true;


		}

		protected string checkLimit2( double val, double calc, double min, double max, string caption,  ref bool hasDiffCalc) {
			string result = "";
			if (val > max || val < min) {
				FoundNebalans = true;
				caption = caption.PadRight(50, ' ').Replace("<br/>", "");
				result = String.Format("<tr><td width='300'>&nbsp;&nbsp;&nbsp;&nbsp;<b>{0}</b></td><td width='200'><b><u>{1:0.##}</b></u></td> <td width='200'>[{2}]-[{3}]</tr>", caption, val,min,max);
			}
			if (Math.Abs(calc - val) > 10) {
				hasDiffCalc = true;				
				if (String.IsNullOrEmpty(result))
					result = String.Format("<tr><td width='300'>&nbsp;&nbsp;&nbsp;&nbsp;<b>{0}</b></td><td width='200'></td><td width='200'></td> </tr>", caption.Replace("<br/>", "")); 
				result += String.Format("<tr><td  width='300'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<i>===Несоответствие данных:</td><td width='200' align='right'>Расчет: <u>[{0:0.##}]</u></td> <td  width='200' align='right'>БД:[{1:0.##}] </td></tr>", val, calc);
			}
			return result;
		}

		public string checkData(NebalansLimits lim, string AvailEmpty, ref bool hasNb, ref bool hasEmpty,ref bool hasEmptyCalc,ref bool hasDiffCalc) {
			string DatesMSG = "";
			string NB500Cap = String.Format("Небаланс по СШ500кВ", lim.NB500Min, lim.NB500Max);
			string NB110Cap = String.Format("Небаланс по СШ110кВ", lim.NB110Min, lim.NB110Max);
			string NB220Cap = String.Format("Небаланс по СШ220кВ", lim.NB220Min, lim.NB220Max);
			string NBVLCap =  String.Format("Небаланс по ВЛ", lim.NBVLMin, lim.NBVLMax);
			string NB1TCap =  String.Format("Небаланс по 1T", lim.NB1TMin, lim.NB1TMax);
			string NB2ATCap = String.Format("Небаланс по 2АT", lim.NB2ATMin, lim.NB2ATMax);
			string NB3ATCap = String.Format("Небаланс по 3АT", lim.NB3ATMin, lim.NB3ATMax);
			string NB4TCap =    String.Format("Небаланс по 4T", lim.NB4TMin, lim.NB4TMax);
			string NB56ATCap =  String.Format("Небаланс по 5-6AT", lim.NB56ATMin, lim.NB56ATMax);
			string NBTransCap = String.Format("Небаланс по трансформаторам", lim.NBTransMin, lim.NBTransMax);
			string NBGESCap = String.Format("Небаланс по ГЭС", lim.NBGESMin, lim.NBGESMax);
			string SPCap = String.Format("Собственное потребление", lim.SPMin, lim.SPMax);
			string SNCap =       String.Format("Собственные нужды", lim.SNMin, lim.SNMax);

			string NB500 = NB500Cap + "";
			string NB110 = NB110Cap + "";
			string NB220 = NB220Cap + "";
			string NBVL = NBVLCap + "";
			string NB1T = NB1TCap + "";
			string NB4T = NB4TCap + "";
			string NB2AT = NB2ATCap + "";
			string NB3AT = NB3ATCap + "";
			string NB56AT = NB56ATCap + "";
			string NBTrans = NBTransCap + "";
			string NBGES = NBGESCap + "";
			string SP = SPCap + "";
			string SN = SNCap + "";


			List<string> EmptyIds = AvailEmpty.Split(';').ToList();

			hasEmpty=false;
			hasEmptyCalc = false;
			hasDiffCalc = false;

			try {
				report.ReadData();				
				foreach (DateTime date in report.Dates) {
					double v500 = report[date,ReportLinesRecords.P_VL500_Nebalans.ID]*2;					
					double v110 = report[date,ReportLinesRecords.P_VL110_Nebalans.ID]*2;
					double v220 = report[date,ReportLinesRecords.P_VL220_Nebalans.ID]*2;
					double vVL= report[date, ReportLinesRecords.P_VL_Nebalans.ID]*2;
					double v1T = -report[date, ReportGlTransformRecords.P_1T_Nebalans.ID]*2;
					double v4T = -report[date, ReportGlTransformRecords.P_4T_Nebalans.ID]*2;
					double v2AT = -report[date, ReportGlTransformRecords.P_2AT_Nebalans.ID]*2;
					double v3AT = -report[date, ReportGlTransformRecords.P_3AT_Nebalans.ID]*2;
					double v56AT = -report[date, ReportGlTransformRecords.P_56AT_Nebalans.ID]*2;
					double vTrans = -report[date, ReportGlTransformRecords.P_T_Nebalans.ID]*2;
					double vGES = report[date, ReportMainRecords.P_Nebalans.ID]*2;
					double vSP = report[date, ReportMainRecords.P_SP.ID] * 2;
					double vSN = report[date, ReportSNRecords.P_SN.ID] * 2;

					double ikm500= report[date, PiramidaRecords.P_IKM_Nebalans500.Key] * 2;
					double ikm110 = report[date, PiramidaRecords.P_IKM_Nebalans110.Key] * 2;
					double ikm220 = report[date, PiramidaRecords.P_IKM_Nebalans220.Key] * 2;
					double ikm1T = report[date, PiramidaRecords.P_IKM_Nebalans1T.Key] * 2;
					double ikm4T = report[date, PiramidaRecords.P_IKM_Nebalans4T.Key] * 2;
					double ikm2AT = report[date, PiramidaRecords.P_IKM_Nebalans2AT.Key] * 2;
					double ikm3AT = report[date, PiramidaRecords.P_IKM_Nebalans3AT.Key] * 2;
					double ikm56AT = report[date, PiramidaRecords.P_IKM_Nebalans56AT.Key] * 2;
					double ikmT = report[date, PiramidaRecords.P_IKM_NebalansT.Key] * 2;
					double ikmGES= report[date, PiramidaRecords.P_IKM_Nebalans_GES.Key] * 2;
					double ikmSP= report[date, PiramidaRecords.P_IKM_SP.Key] * 2;
					double ikmSN = report[date, PiramidaRecords.P_IKM_SN.Key] * 2;

					string str =
						checkLimit2(v500, ikm500, lim.NB500Min, lim.NB500Max, NB500Cap, ref hasDiffCalc) +
						checkLimit2(v220, ikm220, lim.NB220Min, lim.NB220Max, NB220Cap,  ref hasDiffCalc) +
						checkLimit2(v110, ikm110, lim.NB110Min, lim.NB110Max, NB110Cap,  ref hasDiffCalc) +
						checkLimit2(v1T, ikm1T, lim.NB1TMin, lim.NB1TMax, NB1TCap,  ref hasDiffCalc) +
						checkLimit2(v4T, ikm4T, lim.NB4TMin, lim.NB4TMax, NB4TCap,  ref hasDiffCalc) +
						checkLimit2(v2AT, ikm2AT, lim.NB2ATMin, lim.NB2ATMax, NB2ATCap,  ref hasDiffCalc) +
						checkLimit2(v3AT, ikm3AT, lim.NB3ATMin, lim.NB3ATMax, NB3ATCap,  ref hasDiffCalc) +
						checkLimit2(v56AT, ikm56AT, lim.NB56ATMin, lim.NB56ATMax, NB56ATCap,  ref hasDiffCalc) +
						checkLimit2(vTrans, ikmT, lim.NBTransMin, lim.NBTransMax, NBTransCap, ref hasDiffCalc) +
						checkLimit2(vGES, ikmGES, lim.NBGESMin, lim.NBGESMax, NBGESCap,  ref hasDiffCalc) +
						checkLimit2(vSP, ikmSP, lim.SPMin, lim.SPMax, SPCap,  ref hasDiffCalc) +
						checkLimit2(vSN, ikmSN, lim.SNMin, lim.SNMax, SNCap,  ref hasDiffCalc);

					if (report.EmptyData.ContainsKey(date) || !String.IsNullOrEmpty(str)){
						string msg = "";
						if (!String.IsNullOrEmpty(str))
							msg += str;
						if (report.EmptyData.ContainsKey(date)&&report.EmptyData[date].Count>0) {
							string strE = "";
							foreach (RecordTypeDB rdb in report.EmptyData[date]) {
								if (!EmptyIds.Contains(rdb.ID)) {
									strE += String.Format("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;==={0} [{1}] <br/>", rdb.Title, rdb.ID);
									if (rdb.DBRecord.ObjType == 0)
										hasEmpty = true;
									else 
										hasEmptyCalc = true;
								}
							}
							if (!String.IsNullOrEmpty(strE))
								msg += "<tr><td colspan='2'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<b>Нет данных:</b> <br/>" + strE + "</td>";
						}
						if (!String.IsNullOrEmpty(msg))
							DatesMSG += String.Format("<b><u>{0}</u></b>:<br/><table>{1}</table>", date,msg);
					}
				}
				
			} catch (Exception e) {
				Logger.Info("Ошибка при чтении данных из отчета небаланса");
			}
			hasNb = FoundNebalans;
			//Logger.Info(Result);
			return DatesMSG;
		}

	}
}
