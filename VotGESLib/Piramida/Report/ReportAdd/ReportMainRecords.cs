using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VotGES.Piramida.Report
{
	public class ReportMainRecords
	{
		public static RecordTypeCalc P_Nebalans=new RecordTypeCalc("P_Nebalans", "Небаланс P", null);
		public static RecordTypeCalc P_SP=new RecordTypeCalc("P_SP", "Собственное потребление", null);
		public static RecordTypeCalc P_FullP = new RecordTypeCalc("P_FullP", "Полный переток", null);

		static ReportMainRecords() {
			Create();
		}

		public static void Create() {
			P_SP.CalcFunction= new RecordCalcDelegate((report, date) => {
				/*return
					report[date, ReportSNRecords.P_SN.ID] +
					report[date, ReportGARecords.P_Vozb.ID] +
					report[date, ReportGlTransformRecords.P_T_Nebalans.ID] +
					report[date, ReportLinesRecords.P_VL_Nebalans.ID]+
					//report[date, ReportGARecords.P_SN_GA.ID] +
					report[date, ReportGARecords.P_SK_FULL.ID];*/
					return 
						report[date,ReportGARecords.P_GA1.ID]+
						report[date,ReportGARecords.P_GA2.ID]+
						report[date,ReportGARecords.P_GA3.ID]+
						report[date,ReportGARecords.P_GA4.ID]+
						report[date,ReportGARecords.P_GA5.ID]+
						report[date,ReportGARecords.P_GA6.ID]+
						report[date,ReportGARecords.P_GA7.ID]+
						report[date,ReportGARecords.P_GA8.ID]+
						report[date,ReportGARecords.P_GA9.ID]+
						report[date,ReportGARecords.P_GA10.ID]+
						(report[date,ReportLinesRecords.P_VL110_Saldo.ID]+
						report[date,ReportLinesRecords.P_VL220_Saldo.ID]+
						report[date,ReportLinesRecords.P_VL500_Saldo.ID]+
						report[date,ReportLinesRecords.P_KL6_Saldo.ID]);						
			});

			P_Nebalans.CalcFunction=new RecordCalcDelegate((report, date) => {
				return
					report[date, ReportGlTransformRecords.P_T_Nebalans.ID] +
					report[date, ReportLinesRecords.P_VL_Nebalans.ID];
			});

			P_FullP.CalcFunction = new RecordCalcDelegate((report, date) => {
				return
					report[date, ReportGlTransformRecords.P_T_FullP.ID] +
					report[date, ReportLinesRecords.P_VL_FullP.ID] +
					report[date, PiramidaRecords.P_GES.Key];
			});

		}

		

		public static void AddCalcRecords(Report report, bool visible, bool toChart, ResultTypeEnum oper) {
			report.AddRecordType(new RecordTypeCalc(P_SP, toChart, visible, oper));
			report.AddRecordType(new RecordTypeCalc(P_Nebalans, toChart, visible, oper));
			report.AddRecordType(new RecordTypeCalc(P_FullP, toChart, visible, oper));
		}

		public static void AddPRecords(Report report, int parNumber, double scaleMult, double scaleDiv, bool visible, bool toChart, DBOperEnum oper, ResultTypeEnum result) {

			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_GES, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType:result, dbOper:oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.Q_GES, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_GTP1, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType:result, dbOper:oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_GTP2, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType:result, dbOper:oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_RGE1, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_RGE2, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_RGE3, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_RGE4, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));

			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_IKM_SN, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_IKM_Nebalans_GES, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_IKM_SP, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));

			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_IKM_SK, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_IKM_Saldo500Emelino, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_IKM_Saldo500Karmanovo, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_IKM_Saldo500Vyatka, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_IKM_Nebalans_T, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_IKM_Nebalans500, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_IKM_Nebalans220, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_IKM_Nebalans110, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_IKM_Nebalans1T, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_IKM_Nebalans2AT, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_IKM_Nebalans3AT, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_IKM_Nebalans4T, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_IKM_Nebalans56AT, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_IKM_NebalansT, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_IKM_NebalansKRU1, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_IKM_NebalansKRU2, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_IKM_NebalansKRU3, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));

			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_IKM_1N, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_IKM_2N, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_IKM_7N, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_IKM_8N, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));			
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_IKM_9N, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));			
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_IKM_3N, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));			
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_IKM_10N, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));			
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_IKM_3536N, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));

			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_IKM_SN_GA, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_IKM_Vozb, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));			

		}

	}
}
