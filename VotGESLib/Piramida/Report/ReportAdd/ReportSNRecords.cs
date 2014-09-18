using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VotGES.Piramida.Report
{
	public class ReportSNRecords
	{
		public static RecordTypeCalc P_SN_1N=new RecordTypeCalc("P_SN_1N", "СН 1Н", null);
		public static RecordTypeCalc P_SN_2N=new RecordTypeCalc("P_SN_2N", "СН 2Н", null);
		public static RecordTypeCalc P_SN_3N=new RecordTypeCalc("P_SN_3N", "СН 3Н", null);
        public static RecordTypeCalc P_SN_5N = new RecordTypeCalc("P_SN_5N", "СН 5Н", null);
		public static RecordTypeCalc P_SN_7N=new RecordTypeCalc("P_SN_7N", "СН 7Н", null);
		public static RecordTypeCalc P_SN_8N=new RecordTypeCalc("P_SN_8N", "СН 8Н", null);
		public static RecordTypeCalc P_SN_9N=new RecordTypeCalc("P_SN_9N", "СН 4Н 9Н", null);
		public static RecordTypeCalc P_SN_10N=new RecordTypeCalc("P_SN_10N", "СН 10Н", null);
		public static RecordTypeCalc P_SN_36N=new RecordTypeCalc("P_SN_36N", "СН 35Н 36Н", null);
		public static RecordTypeCalc P_SN_Nasos=new RecordTypeCalc("P_SN_Nasos", "СН Насосы", null);
		public static RecordTypeCalc P_SN=new RecordTypeCalc("P_SN", "Собственные нужды", null);

		static ReportSNRecords() {
			CreateSNP();
		}
		

		public static void CreateSNP() {
			P_SN_1N.CalcFunction=new RecordCalcDelegate((report, date) => {
				return report[date,PiramidaRecords.P_KRU1_21T.Key] + report[date,PiramidaRecords.P_KRU3_22T.Key];
			});

			P_SN_2N.CalcFunction=new RecordCalcDelegate((report, date) => {
				return report[date,PiramidaRecords.P_KRU3_23T.Key] + report[date,PiramidaRecords.P_KRU2_24T.Key];
			});

			P_SN_3N.CalcFunction=new RecordCalcDelegate((report, date) => {
				return report[date,PiramidaRecords.P_KRU1_27T.Key] + report[date,PiramidaRecords.P_KRU2_28T.Key];
			});

            P_SN_5N.CalcFunction = new RecordCalcDelegate((report, date) =>
            {
                return report[date, PiramidaRecords.P_KRU3_26T.Key];
            });

			P_SN_7N.CalcFunction=new RecordCalcDelegate((report, date) => {
				return report[date,PiramidaRecords.P_KRU1_31T.Key] + report[date,PiramidaRecords.P_KRU3_32T.Key];
			});

			P_SN_8N.CalcFunction=new RecordCalcDelegate((report, date) => {
				return report[date,PiramidaRecords.P_KRU1_37T.Key] + report[date,PiramidaRecords.P_KRU2_38T.Key];
			});

			P_SN_9N.CalcFunction=new RecordCalcDelegate((report, date) => {
				return report[date,PiramidaRecords.P_KRU3_29T.Key] + report[date,PiramidaRecords.P_KRU2_30T.Key];
			});

			P_SN_10N.CalcFunction= new RecordCalcDelegate((report, date) => {
				return report[date,PiramidaRecords.P_KRU1_33T.Key] + report[date,PiramidaRecords.P_KRU3_34T.Key];
			});

			P_SN_36N.CalcFunction=new RecordCalcDelegate((report, date) => {
				return report[date,PiramidaRecords.P_KRU1_35T.Key] + report[date,PiramidaRecords.P_KRU2_36T.Key];
			});

			P_SN_Nasos.CalcFunction=new RecordCalcDelegate((report, date) => {
				return report[date,PiramidaRecords.P_1VS_N1.Key] + report[date,PiramidaRecords.P_2VS_N1.Key];
			});

			P_SN.CalcFunction=new RecordCalcDelegate((report, date) => {
				return		
					report[date, ReportGARecords.P_SN_GA.ID] +
					report[date, PiramidaRecords.P_SN_7T_Priem.Key] +
					report[date, PiramidaRecords.P_SN_8T_Priem.Key] +
                    report[date,PiramidaRecords.P_SN_9T_Priem.Key] +
					report[date, ReportLinesRecords.P_KL6_Saldo.ID];
			});

			
		}

		public static void AddCalcRecords(Report report, bool visible, bool toChart, ResultTypeEnum oper) {
			report.AddRecordType(new RecordTypeCalc(P_SN_10N, toChart, visible,oper));
			report.AddRecordType(new RecordTypeCalc(P_SN_1N, toChart, visible, oper));
			report.AddRecordType(new RecordTypeCalc(P_SN_2N, toChart, visible, oper));
			report.AddRecordType(new RecordTypeCalc(P_SN_36N, toChart, visible, oper));
			report.AddRecordType(new RecordTypeCalc(P_SN_3N, toChart, visible, oper));
            report.AddRecordType(new RecordTypeCalc(P_SN_5N, toChart, visible, oper));
			report.AddRecordType(new RecordTypeCalc(P_SN_7N, toChart, visible, oper));
			report.AddRecordType(new RecordTypeCalc(P_SN_8N, toChart, visible, oper));
			report.AddRecordType(new RecordTypeCalc(P_SN_9N, toChart, visible, oper));
			report.AddRecordType(new RecordTypeCalc(P_SN_Nasos, toChart, visible, oper));

			report.AddRecordType(new RecordTypeCalc(P_SN, toChart, visible, oper));
		}
		

		public static void AddPRecordsSN(Report report, int parNumber, double scaleMult, double scaleDiv, bool visible, bool toChart, DBOperEnum oper, ResultTypeEnum result) {
            report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_KRU1_21T, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
            report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_KRU3_22T, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));

            report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_KRU3_23T, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
            report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_KRU2_24T, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));

			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_KRU3_26T, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType:result, dbOper:oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_KRU1_27T, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType:result, dbOper:oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_KRU2_28T, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType:result, dbOper:oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_KRU3_29T, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType:result, dbOper:oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_KRU2_30T, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType:result, dbOper:oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_KRU1_31T, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType:result, dbOper:oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_KRU3_32T, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType:result, dbOper:oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_KRU1_33T, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType:result, dbOper:oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_KRU3_34T, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType:result, dbOper:oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_KRU1_35T, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType:result, dbOper:oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_KRU2_36T, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType:result, dbOper:oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_KRU1_37T, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType:result, dbOper:oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_KRU2_38T, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType:result, dbOper:oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_1VS_N1, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType:result, dbOper:oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_2VS_N1, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType:result, dbOper:oper));
            report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_KRU2_TVI, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));			
		}


	}
}
