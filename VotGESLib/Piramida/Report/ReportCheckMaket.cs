using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VotGES.Piramida.Report
{
	public class ReportCheckMaket
	{
		public DateTime Date;
		public FullReport Report2000;
		public FullReport Report0000;

		public ReportCheckMaket(DateTime date) {
			Logger.Info(String.Format("Maket {0}", date.ToString("dd.MM.yyyy HH:mm")));
			Date = date.Date;
			Report0000 = GetReport(date);
			Report2000 = GetReport(date);
			Report2000.UsePiramida2000 = true;
			Report0000.UsePiramida0000 = true;
			Report0000.ReadData();
			Report2000.ReadData();
		}

		public FullReport GetReport(DateTime date) {
			FullReport report = new FullReport(date.Date, date.AddDays(1), IntervalReportEnum.halfHour);
			report.RecordTypes[ReportMainRecords.P_SP.ID].Visible = true;
			report.RecordTypes[ReportMainRecords.P_Poter.ID].Visible = true;
			report.RecordTypes[ReportLinesRecords.P_KL6_Saldo.ID].Visible = true;
			report.RecordTypes[ReportLinesRecords.P_VL110_Saldo.ID].Visible = true;
			report.RecordTypes[ReportLinesRecords.P_VL220_Saldo.ID].Visible = true;
			report.RecordTypes[ReportLinesRecords.P_VL500_Saldo.ID].Visible = true;
			report.RecordTypes[ReportSNRecords.P_SN.ID].Visible = true;
			report.RecordTypes[ReportGARecords.P_Vozb.ID].Visible = true;

			report.RecordTypes[PiramidaRecords.P_IKM_SP.Key].Visible = true;			
			report.RecordTypes[PiramidaRecords.P_IKM_Nebalans_T.Key].Visible = true;
			report.RecordTypes[PiramidaRecords.P_IKM_SN.Key].Visible = true;
			report.RecordTypes[PiramidaRecords.P_IKM_SN_GA.Key].Visible = true;
			report.RecordTypes[PiramidaRecords.P_IKM_SP.Key].Visible = true;
			report.RecordTypes[PiramidaRecords.P_IKM_Vozb.Key].Visible = true;
			//report.RecordTypes[PiramidaRecords.P_IKM_Nebalans_GES.Key].Visible = true;
			return report;
		}

		public List<string> CheckData(bool ikm,bool TU) {
			Logger.Info("check");
			List<string> result = new List<string>();

			DateTime date = Date.Date.AddMinutes(30);
			string changes = "";
			while (date <= Date.Date.AddDays(1)) {
				//Logger.Info(date.ToString("dd.MM.yyyy HH:mm"));
				bool ok = true;
				try {
					if (!Report2000.Dates.Contains(date)) {
						result.Add(String.Format("Не заполнена дата {0} в Базе 2000"));
						ok = false;
					}
					if (!Report0000.Dates.Contains(date)) {
						result.Add(String.Format("Не заполнена дата {0} в Базе 2000"));
						ok = false;
					}
					if (!ok) {
						date = date.AddMinutes(30);
						continue;
					}

					foreach (RecordTypeBase rec in Report2000.RecordTypes.Values) {
						if (rec.Visible || (rec.IsNeed && rec is RecordTypeDB)) {
							bool isIKM = false;
							bool isTU = false;
							if (rec is RecordTypeDB) {
								RecordTypeDB rdb = rec as RecordTypeDB;
								isIKM = rdb.DBRecord.Obj == 0 && rdb.DBRecord.ObjType == 2;
								isTU = (rdb.DBRecord.Obj == 8739 && rdb.DBRecord.Item >= 9 && rdb.DBRecord.Item <= 36) || (rdb.DBRecord.Obj == 8740 && rdb.DBRecord.Item >= 47 && rdb.DBRecord.Item <= 55);
							}

							if (!ikm && isIKM)
								continue;
							if (!TU && isTU)
								continue;

							double v2000 = Math.Abs(Report2000[date, rec.ID]);
							double v0000 = Math.Abs(Report0000[date, rec.ID]);

							if (Math.Abs(v2000 - v0000) > 10) {
								changes += String.Format("<tr><td width='100'>{0}</td><td width='200'>{1}</td><td width='100'>{2:0.00}</td><td width='100'>{3:0.00}</td><td width='100'>{4}</td></tr>",
									date.ToString("dd.MM.yyyy HH:mm"), rec.Title, v2000, v0000, rec is RecordTypeCalc ? "CALC" : "DB");
							}
						}
					}
				}catch (Exception e) {
					Logger.Info("Ошибка при получении данных за " + date + e.ToString());
				}
				
				date = date.AddMinutes(30);
			}
			if (!string.IsNullOrEmpty(changes)) {
				changes = String.Format("<table><tr><td width='100'>Date</td><td width='200'>Title</td><td width='100'>V2000</td><td width='100'>V0000</td><td width='100'>Source</td></tr>{0}</table>", changes);
			}
			if (!string.IsNullOrEmpty(changes))
				result.Add(changes);
			return result;
		}
		
	}
}
