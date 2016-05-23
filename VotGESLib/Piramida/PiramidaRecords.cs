using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VotGES.Piramida {
	public static class PiramidaRecords {
		public static PiramidaRecord P_GES = new PiramidaRecord(2, 0, 1, "P ГЭС");
		public static PiramidaRecord Q_GES = new PiramidaRecord(2, 0, 4, "Q ГЭС");
		public static PiramidaRecord P_GTP1 = new PiramidaRecord(2, 0, 2, "P ГТП1");
		public static PiramidaRecord P_GTP2 = new PiramidaRecord(2, 0, 3, "P ГТП2");
		public static PiramidaRecord P_RGE1 = new PiramidaRecord(2, 0, 1045, "P РГЕ1");
		public static PiramidaRecord P_RGE2 = new PiramidaRecord(2, 0, 1703, "P РГЕ2");
		public static PiramidaRecord P_RGE3 = new PiramidaRecord(2, 0, 1704, "P РГЕ3");
		public static PiramidaRecord P_RGE4 = new PiramidaRecord(2, 0, 1046, "P РГЕ4");
		

		public static PiramidaRecord P_IKM_SN = new PiramidaRecord(2, 0, 14, "Расход на собственные нужды ГЭС");
		public static PiramidaRecord P_IKM_Nebalans_GES = new PiramidaRecord(2, 0, 24, "Небаланс по ГЭС");
		public static PiramidaRecord P_IKM_SP = new PiramidaRecord(2, 0, 52, "Собственное потребление");


		public static PiramidaRecord P_IKM_SK = new PiramidaRecord(2, 0, 5, "Режим СК [A+]");
		public static PiramidaRecord P_IKM_Saldo500Emelino = new PiramidaRecord(2, 0, 15, "Сальдо ВЛ 500кВ Емелино");
		public static PiramidaRecord P_IKM_Saldo500Karmanovo = new PiramidaRecord(2, 0, 16, "Сальдо ВЛ 500кВ Карманово");
		public static PiramidaRecord P_IKM_Saldo500Vyatka = new PiramidaRecord(2, 0, 17, "Сальдо ВЛ 500кВ Вятка");
		public static PiramidaRecord P_IKM_Nebalans_T = new PiramidaRecord(2, 0, 25, "Потери в сетях и трансформаторах");
		public static PiramidaRecord P_IKM_Nebalans500 = new PiramidaRecord(2, 0, 26, "Баланс СШ 500кВ");
		public static PiramidaRecord P_IKM_Nebalans220 = new PiramidaRecord(2, 0, 27, "Баланс СШ 220кВ");
		public static PiramidaRecord P_IKM_Nebalans110 = new PiramidaRecord(2, 0, 28, "Баланс СШ 110кВ");
		public static PiramidaRecord P_IKM_Nebalans1T = new PiramidaRecord(2, 0, 29, "Баланс 1Т");
		public static PiramidaRecord P_IKM_Nebalans2AT = new PiramidaRecord(2, 0, 30, "Баланс 2АТ");
		public static PiramidaRecord P_IKM_Nebalans3AT = new PiramidaRecord(2, 0, 31, "Баланс 3АТ");
		public static PiramidaRecord P_IKM_Nebalans4T = new PiramidaRecord(2, 0, 32, "Баланс 4Т");
		public static PiramidaRecord P_IKM_Nebalans56AT = new PiramidaRecord(2, 0, 33, "Баланс 5,6 АТ");
		public static PiramidaRecord P_IKM_NebalansT = new PiramidaRecord(2, 0, 34, "Баланс в трансформаторах");
		public static PiramidaRecord P_IKM_NebalansKRU1 = new PiramidaRecord(2, 0, 83, "Небаланс КРУ-1");
		public static PiramidaRecord P_IKM_NebalansKRU3 = new PiramidaRecord(2, 0, 99, "Небаланс КРУ-3");
		public static PiramidaRecord P_IKM_NebalansKRU2 = new PiramidaRecord(2, 0, 1705, "Небаланс КРУ-2");

		public static PiramidaRecord P_IKM_1N = new PiramidaRecord(2, 0, 71, "СН 1Н");
		public static PiramidaRecord P_IKM_2N = new PiramidaRecord(2, 0, 72, "СН 2Н");
		public static PiramidaRecord P_IKM_7N = new PiramidaRecord(2, 0, 73, "СН 7Н");
		public static PiramidaRecord P_IKM_8N = new PiramidaRecord(2, 0, 74, "СН 8Н");
		public static PiramidaRecord P_IKM_10N = new PiramidaRecord(2, 0, 76, "СН 10Н");
		public static PiramidaRecord P_IKM_3N = new PiramidaRecord(2, 0, 77, "СН 3Н");
		public static PiramidaRecord P_IKM_9N = new PiramidaRecord(2, 0, 78, "СН 9Н");
		public static PiramidaRecord P_IKM_3536N = new PiramidaRecord(2, 0, 79, "СН 35-36Н");
		public static PiramidaRecord P_IKM_SN_GA = new PiramidaRecord(2, 0, 35, "СН ГА");
		public static PiramidaRecord P_IKM_Vozb = new PiramidaRecord(2, 0, 42, "СН Возбуждение");


		public static PiramidaRecord P_3AT_500_Priem = new PiramidaRecord(0, 8739, 1, "3АТ 500 кВ Прием (P)");
		public static PiramidaRecord P_3AT_500_Otd = new PiramidaRecord(0, 8739, 2, "3АТ 500 кВ Отдача (P)");
		public static PiramidaRecord Q_3AT_500_Priem = new PiramidaRecord(0, 8739, 3, "3АТ 500 кВ Прием (Q)");
		public static PiramidaRecord Q_3AT_500_Otd = new PiramidaRecord(0, 8739, 4, "3АТ 500 кВ Отдача (Q)");

		public static PiramidaRecord P_2AT_500_Priem = new PiramidaRecord(0, 8739, 5, "2АТ 500 кВ Прием (P)");
		public static PiramidaRecord P_2AT_500_Otd = new PiramidaRecord(0, 8739, 6, "2АТ 500 кВ Отдача (P)");
		public static PiramidaRecord Q_2AT_500_Priem = new PiramidaRecord(0, 8739, 7, "2АТ 500 кВ Прием (Q)");
		public static PiramidaRecord Q_2AT_500_Otd = new PiramidaRecord(0, 8739, 8, "2АТ 500 кВ Отдача (Q)");

		public static PiramidaRecord P_1VS_N1 = new PiramidaRecord(0, 8739, 9, "1-1 ВС (P)");
		public static PiramidaRecord P_KRU2_24T = new PiramidaRecord(0, 8739, 10, "КРУ-2 24Т (P)");
		public static PiramidaRecord P_KRU2_TVI = new PiramidaRecord(0, 8739, 11, "КРУ-2 ТВИ (P)");
		public static PiramidaRecord P_KRU2_TP2 = new PiramidaRecord(0, 8739, 12, "КРУ-2 ТП2 (P)");
		public static PiramidaRecord P_KRU1_31T = new PiramidaRecord(0, 8739, 13, "КРУ-1 31Т (P)");
		public static PiramidaRecord P_KRU2_RUSN = new PiramidaRecord(0, 8739, 14, "КРУ-2 РУСН (P)");


		public static PiramidaRecord P_KRU2_36T = new PiramidaRecord(0, 8739, 16, "КРУ-2 36Т (P)");
		public static PiramidaRecord P_KRU1_27T = new PiramidaRecord(0, 8739, 17, "КРУ-1 27Т (P)");
		public static PiramidaRecord P_KRU1_37T = new PiramidaRecord(0, 8739, 18, "КРУ-1 37Т (P)");
		public static PiramidaRecord P_KRU2_30T = new PiramidaRecord(0, 8739, 19, "КРУ-2 30Т (P)");
		public static PiramidaRecord P_KRU2_28T = new PiramidaRecord(0, 8739, 20, "КРУ-2 28Т (P)");
		public static PiramidaRecord P_KRU1_33T = new PiramidaRecord(0, 8739, 21, "КРУ-1 33Т (P)");
		public static PiramidaRecord P_KRU1_21T = new PiramidaRecord(0, 8739, 22, "КРУ-1 21Т (P)");
		public static PiramidaRecord P_KRU1_25T = new PiramidaRecord(0, 8739, 23, "КРУ-1 25Т (P)");
		public static PiramidaRecord P_KRU2_38T = new PiramidaRecord(0, 8739, 24, "КРУ-2 38Т (P)");
		public static PiramidaRecord P_KRU1_Rez1 = new PiramidaRecord(0, 8739, 25, "Резерв #1@КРУ-1:11");
		public static PiramidaRecord P_KRU1_Rez2 = new PiramidaRecord(0, 8739, 26, "Резерв #2@КРУ-1:12");
		public static PiramidaRecord P_KRU1_35T = new PiramidaRecord(0, 8739, 27, "КРУ-1 35Т (P)");
		public static PiramidaRecord P_KRU1_Rez3 = new PiramidaRecord(0, 8739, 28, "Резерв #3@КРУ-1:13");

		public static PiramidaRecord P_R500_Emelino_priem = new PiramidaRecord(0, 8739, 29, "R 500 Емелино прием (P)");
		public static PiramidaRecord Q_R500_Emelino_priem = new PiramidaRecord(0, 8739, 31, "R 500 Емелино прием (Q)");
		public static PiramidaRecord P_R500_Vyatka_priem = new PiramidaRecord(0, 8739, 33, "R 500 Вятка прием (P)");
		public static PiramidaRecord Q_R500_Vyatka_priem = new PiramidaRecord(0, 8739, 35, "R 500 Вятка прием (Q)");


		public static PiramidaRecord P_Vozb_GA9_Priem = new PiramidaRecord(0, 8740, 1, "Возбуждение Г/А 9 прием (P)");
		public static PiramidaRecord P_Vozb_GA10_Priem = new PiramidaRecord(0, 8740, 2, "Возбуждение Г/А 10 прием (P)");
		public static PiramidaRecord P_SN_19T_Priem = new PiramidaRecord(0, 8740, 3, "СН 19Т прием (P)");
		public static PiramidaRecord P_SN_20T_Priem = new PiramidaRecord(0, 8740, 4, "СН 20Т прием (P)");
		public static PiramidaRecord P_Vozb_GA7_Priem = new PiramidaRecord(0, 8740, 5, "Возбуждение Г/А 7 прием (P)");
		public static PiramidaRecord P_Vozb_GA8_Priem = new PiramidaRecord(0, 8740, 6, "Возбуждение Г/А 8 прием (P)");
		public static PiramidaRecord P_SN_17T_Priem = new PiramidaRecord(0, 8740, 7, "СН 17Т прием (P)");
		public static PiramidaRecord P_SN_18T_Priem = new PiramidaRecord(0, 8740, 8, "СН 28Т прием (P)");

		public static PiramidaRecord P_1T_110_Priem = new PiramidaRecord(0, 8740, 9, "1Т 110 кВ Прием (P)");
		public static PiramidaRecord P_1T_110_Otd = new PiramidaRecord(0, 8740, 10, "1Т 110 кВ Отдача (P)");
		public static PiramidaRecord Q_1T_110_Priem = new PiramidaRecord(0, 8740, 11, "1Т 110 кВ Прием (Q)");
		public static PiramidaRecord Q_1T_110_Otd = new PiramidaRecord(0, 8740, 12, "1Т 110 кВ Отдача (Q)");

		public static PiramidaRecord P_2AT_220_Priem = new PiramidaRecord(0, 8740, 13, "2АТ 220 кВ Прием (P)");
		public static PiramidaRecord P_2AT_220_Otd = new PiramidaRecord(0, 8740, 14, "2АТ 220 кВ Отдача (P)");
		public static PiramidaRecord Q_2AT_220_Priem = new PiramidaRecord(0, 8740, 15, "2АТ 220 кВ Прием (Q)");
		public static PiramidaRecord Q_2AT_220_Otd = new PiramidaRecord(0, 8740, 16, "2АТ 220 кВ Отдача (Q)");

		public static PiramidaRecord P_3AT_220_Priem = new PiramidaRecord(0, 8740, 17, "3АТ 220 кВ Прием (P)");
		public static PiramidaRecord P_3AT_220_Otd = new PiramidaRecord(0, 8740, 18, "3АТ 220 кВ Отдача (P)");
		public static PiramidaRecord Q_3AT_220_Priem = new PiramidaRecord(0, 8740, 19, "3АТ 220 кВ Прием (Q)");
		public static PiramidaRecord Q_3AT_220_Otd = new PiramidaRecord(0, 8740, 20, "3АТ 220 кВ Отдача (Q)");

		public static PiramidaRecord P_56AT_220_Priem = new PiramidaRecord(0, 8740, 21, "5-6АТ 220 кВ Прием (P)");
		public static PiramidaRecord P_56AT_220_Otd = new PiramidaRecord(0, 8740, 22, "5-6АТ 220 кВ Отдача (P)");
		public static PiramidaRecord Q_56AT_220_Priem = new PiramidaRecord(0, 8740, 23, "5-6АТ 220 кВ Прием (Q)");
		public static PiramidaRecord Q_56AT_220_Otd = new PiramidaRecord(0, 8740, 24, "5-6АТ 220 кВ Отдача (Q)");

		public static PiramidaRecord P_4T_220_Priem = new PiramidaRecord(0, 8740, 25, "4Т 220 кВ Прием (P)");
		public static PiramidaRecord P_4T_220_Otd = new PiramidaRecord(0, 8740, 26, "4Т 220 кВ Отдача (P)");
		public static PiramidaRecord Q_4T_220_Priem = new PiramidaRecord(0, 8740, 27, "4Т 220 кВ Прием (Q)");
		public static PiramidaRecord Q_4T_220_Otd = new PiramidaRecord(0, 8740, 28, "4Т 220 кВ Отдача (Q)");

		public static PiramidaRecord P_Vozb_GA5_Priem = new PiramidaRecord(0, 8740, 29, "Возбуждение Г/А 5 прием (P)");
		public static PiramidaRecord P_Vozb_GA6_Priem = new PiramidaRecord(0, 8740, 30, "Возбуждение Г/А 6 прием (P)");
		public static PiramidaRecord P_SN_15T_Priem = new PiramidaRecord(0, 8740, 31, "СН 15Т прием (P)");
		public static PiramidaRecord P_SN_16T_Priem = new PiramidaRecord(0, 8740, 32, "СН 26Т прием (P)");
		public static PiramidaRecord P_SN_8T_Priem = new PiramidaRecord(0, 8740, 33, "СН 8Т прием (P)");

		public static PiramidaRecord P_Vozb_GA1_Priem = new PiramidaRecord(0, 8740, 34, "Возбуждение Г/А 1 прием (P)");
		public static PiramidaRecord P_Vozb_GA2_Priem = new PiramidaRecord(0, 8740, 35, "Возбуждение Г/А 2 прием (P)");
		public static PiramidaRecord P_SN_11T_Priem = new PiramidaRecord(0, 8740, 36, "СН 11Т прием (P)");
		public static PiramidaRecord P_SN_12T_Priem = new PiramidaRecord(0, 8740, 37, "СН 12Т прием (P)");
		public static PiramidaRecord P_SN_7T_Priem = new PiramidaRecord(0, 8739, 37, "СН 7Т прием (P)");

		public static PiramidaRecord P_Vozb_GA3_Priem = new PiramidaRecord(0, 8740, 39, "Возбуждение Г/А 3 прием (P)");
		public static PiramidaRecord P_Vozb_GA4_Priem = new PiramidaRecord(0, 8740, 40, "Возбуждение Г/А 4 прием (P)");
		public static PiramidaRecord P_SN_13T_Priem = new PiramidaRecord(0, 8740, 41, "СН 13Т прием (P)");
		public static PiramidaRecord P_SN_14T_Priem = new PiramidaRecord(0, 8740, 42, "СН 14Т прием (P)");

		public static PiramidaRecord P_56AT_110_Priem = new PiramidaRecord(0, 8740, 43, "5-6АТ 110 кВ Прием (P)");
		public static PiramidaRecord P_56AT_110_Otd = new PiramidaRecord(0, 8740, 44, "5-6АТ 110 кВ Отдача (P)");
		public static PiramidaRecord Q_56AT_110_Priem = new PiramidaRecord(0, 8740, 45, "5-6АТ 110 кВ Прием (Q)");
		public static PiramidaRecord Q_56AT_110_Otd = new PiramidaRecord(0, 8740, 46, "5-6АТ 110 кВ Отдача (Q)");


		public static PiramidaRecord P_KRU3_29T = new PiramidaRecord(0, 8740, 47, "КРУ-3 29Т (P)");
		public static PiramidaRecord P_KRU3_22T = new PiramidaRecord(0, 8740, 48, "КРУ-3 22Т (P)");
		public static PiramidaRecord P_2VS_N1 = new PiramidaRecord(0, 8740, 49, "1-2 ВС (P)");
		public static PiramidaRecord P_KRU3_32T = new PiramidaRecord(0, 8740, 50, "КРУ-3 32Т (P)");
		public static PiramidaRecord P_KRU3_23T = new PiramidaRecord(0, 8740, 51, "КРУ-3 23Т (P)");
		public static PiramidaRecord P_KRU3_TP1 = new PiramidaRecord(0, 8740, 52, "КРУ-3 ТП1 (P)");
		public static PiramidaRecord P_KRU3_34T = new PiramidaRecord(0, 8740, 53, "КРУ-3 34Т (P)");
		public static PiramidaRecord P_KRU3_26T = new PiramidaRecord(0, 8740, 55, "КРУ-3 26Т (P)");

		public static PiramidaRecord P_VL110_Svetlaya_Priem = new PiramidaRecord(0, 8737, 1, "ВЛ 110 Светлая прием (P)");
		public static PiramidaRecord P_VL110_Svetlaya_Otd = new PiramidaRecord(0, 8737, 2, "ВЛ 110 Светлая отдача (P)");
		public static PiramidaRecord Q_VL110_Svetlaya_Priem = new PiramidaRecord(0, 8737, 3, "ВЛ 110 Светлая прием (Q)");
		public static PiramidaRecord Q_VL110_Svetlaya_Otd = new PiramidaRecord(0, 8737, 4, "ВЛ 110 Светлая отдача (Q)");

		public static PiramidaRecord P_VL110_Ivanovka_Priem = new PiramidaRecord(0, 8737, 5, "ВЛ 110 Ивановка прием (P)");
		public static PiramidaRecord P_VL110_Ivanovka_Otd = new PiramidaRecord(0, 8737, 6, "ВЛ 110 Ивановка отдача (P)");
		public static PiramidaRecord Q_VL110_Ivanovka_Priem = new PiramidaRecord(0, 8737, 7, "ВЛ 110 Ивановка прием (Q)");
		public static PiramidaRecord Q_VL110_Ivanovka_Otd = new PiramidaRecord(0, 8737, 8, "ВЛ 110 Ивановка отдача (Q)");

		public static PiramidaRecord P_VL110_Kauchuk_Priem = new PiramidaRecord(0, 8737, 9, "ВЛ 110 Каучук прием (P)");
		public static PiramidaRecord P_VL110_Kauchuk_Otd = new PiramidaRecord(0, 8737, 10, "ВЛ 110 Каучук отдача (P)");
		public static PiramidaRecord Q_VL110_Kauchuk_Priem = new PiramidaRecord(0, 8737, 11, "ВЛ 110 Каучук прием (Q)");
		public static PiramidaRecord Q_VL110_Kauchuk_Otd = new PiramidaRecord(0, 8737, 12, "ВЛ 110 Каучук отдача (Q)");

		public static PiramidaRecord P_VL110_TEC_Priem = new PiramidaRecord(0, 8737, 13, "ВЛ 110 ЧаТЭЦ прием (P)");
		public static PiramidaRecord P_VL110_TEC_Otd = new PiramidaRecord(0, 8737, 14, "ВЛ 110 ЧаТЭЦ отдача (P)");
		public static PiramidaRecord Q_VL110_TEC_Priem = new PiramidaRecord(0, 8737, 15, "ВЛ 110 ЧаТЭЦ прием (Q)");
		public static PiramidaRecord Q_VL110_TEC_Otd = new PiramidaRecord(0, 8737, 16, "ВЛ 110 ЧаТЭЦ отдача (Q)");

		public static PiramidaRecord P_VL110_Berezovka_Priem = new PiramidaRecord(0, 8737, 17, "ВЛ 110 Березовка прием (P)");
		public static PiramidaRecord P_VL110_Berezovka_Otd = new PiramidaRecord(0, 8737, 18, "ВЛ 110 Березовка отдача (P)");
		public static PiramidaRecord Q_VL110_Berezovka_Priem = new PiramidaRecord(0, 8737, 19, "ВЛ 110 Березовка прием (Q)");
		public static PiramidaRecord Q_VL110_Berezovka_Otd = new PiramidaRecord(0, 8737, 20, "ВЛ 110 Березовка отдача (Q)");

		public static PiramidaRecord P_VL220_Svetlaya_Priem = new PiramidaRecord(0, 8737, 21, "ВЛ 220 Светлая прием (P)");
		public static PiramidaRecord P_VL220_Svetlaya_Otd = new PiramidaRecord(0, 8737, 22, "ВЛ 220 Светлая отдача (P)");
		public static PiramidaRecord Q_VL220_Svetlaya_Priem = new PiramidaRecord(0, 8737, 23, "ВЛ 220 Светлая прием (Q)");
		public static PiramidaRecord Q_VL220_Svetlaya_Otd = new PiramidaRecord(0, 8737, 24, "ВЛ 220 Светлая отдача (Q)");

		public static PiramidaRecord P_VL220_Kauchuk1_Priem = new PiramidaRecord(0, 8737, 25, "ВЛ 220 Каучук-1 прием (P)");
		public static PiramidaRecord P_VL220_Kauchuk1_Otd = new PiramidaRecord(0, 8737, 26, "ВЛ 220 Каучук-1 отдача (P)");
		public static PiramidaRecord Q_VL220_Kauchuk1_Priem = new PiramidaRecord(0, 8737, 27, "ВЛ 220 Каучук-1 прием (Q)");
		public static PiramidaRecord Q_VL220_Kauchuk1_Otd = new PiramidaRecord(0, 8737, 28, "ВЛ 220 Каучук-1 отдача (Q)");

		public static PiramidaRecord P_VL220_Kauchuk2_Priem = new PiramidaRecord(0, 8737, 29, "ВЛ 220 Каучук-2 прием (P)");
		public static PiramidaRecord P_VL220_Kauchuk2_Otd = new PiramidaRecord(0, 8737, 30, "ВЛ 220 Каучук-2 отдача (P)");
		public static PiramidaRecord Q_VL220_Kauchuk2_Priem = new PiramidaRecord(0, 8737, 31, "ВЛ 220 Каучук-2 прием (Q)");
		public static PiramidaRecord Q_VL220_Kauchuk2_Otd = new PiramidaRecord(0, 8737, 32, "ВЛ 220 Каучук-2 отдача (Q)");

		public static PiramidaRecord P_VL220_Izhevsk1_Priem = new PiramidaRecord(0, 8737, 33, "ВЛ 220 Ижевск-1 прием (P)");
		public static PiramidaRecord P_VL220_Izhevsk1_Otd = new PiramidaRecord(0, 8737, 34, "ВЛ 220 Ижевск-1 отдача (P)");
		public static PiramidaRecord Q_VL220_Izhevsk1_Priem = new PiramidaRecord(0, 8737, 35, "ВЛ 220 Ижевск-1 прием (Q)");
		public static PiramidaRecord Q_VL220_Izhevsk1_Otd = new PiramidaRecord(0, 8737, 36, "ВЛ 220 Ижевск-1 отдача (Q)");

		public static PiramidaRecord P_VL220_Izhevsk2_Priem = new PiramidaRecord(0, 8737, 37, "ВЛ 220 Ижевск-2 прием (P)");
		public static PiramidaRecord P_VL220_Izhevsk2_Otd = new PiramidaRecord(0, 8737, 38, "ВЛ 220 Ижевск-2 отдача (P)");
		public static PiramidaRecord Q_VL220_Izhevsk2_Priem = new PiramidaRecord(0, 8737, 39, "ВЛ 220 Ижевск-2 прием (Q)");
		public static PiramidaRecord Q_VL220_Izhevsk2_Otd = new PiramidaRecord(0, 8737, 40, "ВЛ 220 Ижевск-2 отдача (Q)");

		public static PiramidaRecord P_VL110_KSHT1_Priem = new PiramidaRecord(0, 8737, 41, "ВЛ 110 КШТ-1 прием (P)");
		public static PiramidaRecord P_VL110_KSHT1_Otd = new PiramidaRecord(0, 8737, 42, "ВЛ 110 КШТ-1 отдача (P)");
		public static PiramidaRecord Q_VL110_KSHT1_Priem = new PiramidaRecord(0, 8737, 43, "ВЛ 110 КШТ-1 прием (Q)");
		public static PiramidaRecord Q_VL110_KSHT1_Otd = new PiramidaRecord(0, 8737, 44, "ВЛ 110 КШТ-1 отдача (Q)");

		public static PiramidaRecord P_VL110_KSHT2_Priem = new PiramidaRecord(0, 8737, 45, "ВЛ 110 КШТ-2 прием (P)");
		public static PiramidaRecord P_VL110_KSHT2_Otd = new PiramidaRecord(0, 8737, 46, "ВЛ 110 КШТ-2 отдача (P)");
		public static PiramidaRecord Q_VL110_KSHT2_Priem = new PiramidaRecord(0, 8737, 47, "ВЛ 110 КШТ-2 прием (Q)");
		public static PiramidaRecord Q_VL110_KSHT2_Otd = new PiramidaRecord(0, 8737, 48, "ВЛ 110 КШТ-2 отдача (Q)");

		public static PiramidaRecord P_VL110_Dubovaya_Priem = new PiramidaRecord(0, 8737, 49, "ВЛ 110 Дубовая прием (P)");
		public static PiramidaRecord P_VL110_Dubovaya_Otd = new PiramidaRecord(0, 8737, 50, "ВЛ 110 Дубовая отдача (P)");
		public static PiramidaRecord Q_VL110_Dubovaya_Priem = new PiramidaRecord(0, 8737, 51, "ВЛ 110 Дубовая прием (Q)");
		public static PiramidaRecord Q_VL110_Dubovaya_Otd = new PiramidaRecord(0, 8737, 52, "ВЛ 110 Дубовая отдача (Q)");

		public static PiramidaRecord P_VL110_Vodozabor2_Priem = new PiramidaRecord(0, 8737, 53, "ВЛ 110 Водозабор-2 прием (P)");
		public static PiramidaRecord P_VL110_Vodozabor2_Otd = new PiramidaRecord(0, 8737, 54, "ВЛ 110 Водозабор-2 отдача (P)");
		public static PiramidaRecord Q_VL110_Vodozabor2_Priem = new PiramidaRecord(0, 8737, 55, "ВЛ 110 Водозабор-2 прием (Q)");
		public static PiramidaRecord Q_VL110_Vodozabor2_Otd = new PiramidaRecord(0, 8737, 56, "ВЛ 110 Водозабор-2 отдача (Q)");

		public static PiramidaRecord P_VL110_Vodozabor1_Priem = new PiramidaRecord(0, 8737, 57, "ВЛ 110 Водозабор-1 прием (P)");
		public static PiramidaRecord P_VL110_Vodozabor1_Otd = new PiramidaRecord(0, 8737, 58, "ВЛ 110 Водозабор-1 отдача (P)");
		public static PiramidaRecord Q_VL110_Vodozabor1_Priem = new PiramidaRecord(0, 8737, 59, "ВЛ 110 Водозабор-1 прием (Q)");
		public static PiramidaRecord Q_VL110_Vodozabor1_Otd = new PiramidaRecord(0, 8737, 60, "ВЛ 110 Водозабор-1 отдача (Q)");

		public static PiramidaRecord P_VL500_Emelino_Priem = new PiramidaRecord(0, 8737, 61, "ВЛ 500 Емелино прием (P)");
		public static PiramidaRecord P_VL500_Emelino_Otd = new PiramidaRecord(0, 8737, 62, "ВЛ 500 Емелино отдача (P)");
		public static PiramidaRecord Q_VL500_Emelino_Priem = new PiramidaRecord(0, 8737, 63, "ВЛ 500 Емелино прием (Q)");
		public static PiramidaRecord Q_VL500_Emelino_Otd = new PiramidaRecord(0, 8737, 64, "ВЛ 500 Емелино отдача (Q)");

		public static PiramidaRecord P_VL500_Karmanovo_Priem = new PiramidaRecord(0, 8737, 65, "ВЛ 500 Карманово прием (P)");
		public static PiramidaRecord P_VL500_Karmanovo_Otd = new PiramidaRecord(0, 8737, 66, "ВЛ 500 Карманово отдача (P)");
		public static PiramidaRecord Q_VL500_Karmanovo_Priem = new PiramidaRecord(0, 8737, 67, "ВЛ 500 Карманово прием (Q)");
		public static PiramidaRecord Q_VL500_Karmanovo_Otd = new PiramidaRecord(0, 8737, 68, "ВЛ 500 Карманово отдача (Q)");

		public static PiramidaRecord P_VL500_Vyatka_Priem = new PiramidaRecord(0, 8737, 69, "ВЛ 500 Вятка прием (P)");
		public static PiramidaRecord P_VL500_Vyatka_Otd = new PiramidaRecord(0, 8737, 70, "ВЛ 500 Вятка отдача (P)");
		public static PiramidaRecord Q_VL500_Vyatka_Priem = new PiramidaRecord(0, 8737, 71, "ВЛ 500 Вятка прием (Q)");
		public static PiramidaRecord Q_VL500_Vyatka_Otd = new PiramidaRecord(0, 8737, 72, "ВЛ 500 Вятка отдача (Q)");

		public static PiramidaRecord P_GA1_Priem = new PiramidaRecord(0, 8738, 1, "Генератор-1 прием (P)");
		public static PiramidaRecord P_GA1_Otd = new PiramidaRecord(0, 8738, 2, "Генератор-1 отдача (P)");
		public static PiramidaRecord Q_GA1_Priem = new PiramidaRecord(0, 8738, 3, "Генератор-1 прием (Q)");
		public static PiramidaRecord Q_GA1_Otd = new PiramidaRecord(0, 8738, 4, "Генератор-1 отдача (Q)");

		public static PiramidaRecord P_GA2_Priem = new PiramidaRecord(0, 8738, 5, "Генератор-2 прием (P)");
		public static PiramidaRecord P_GA2_Otd = new PiramidaRecord(0, 8738, 6, "Генератор-2 отдача (P)");
		public static PiramidaRecord Q_GA2_Priem = new PiramidaRecord(0, 8738, 7, "Генератор-2 прием (Q)");
		public static PiramidaRecord Q_GA2_Otd = new PiramidaRecord(0, 8738, 8, "Генератор-2 отдача (Q)");

		public static PiramidaRecord P_KL6_Shluz1_Priem = new PiramidaRecord(0, 8738, 9, "КЛ 6 Шлюз-1 прием (P)");
		public static PiramidaRecord P_KL6_Shluz1_Otd = new PiramidaRecord(0, 8738, 10, "КЛ 6 Шлюз-1 отдача (P)");
		public static PiramidaRecord Q_KL6_Shluz1_Priem = new PiramidaRecord(0, 8738, 11, "КЛ 6 Шлюз-1 прием (Q)");
		public static PiramidaRecord Q_KL6_Shluz1_Otd = new PiramidaRecord(0, 8738, 12, "КЛ 6 Шлюз-1 отдача (Q)");
		public static PiramidaRecord P_KL6_Shluz2_Priem = new PiramidaRecord(0, 8738, 13, "КЛ 6 Шлюз-2 прием (P)");
		public static PiramidaRecord P_KL6_Shluz2_Otd = new PiramidaRecord(0, 8738, 14, "КЛ 6 Шлюз-2 отдача (P)");
		public static PiramidaRecord Q_KL6_Shluz2_Priem = new PiramidaRecord(0, 8738, 15, "КЛ 6 Шлюз-2 прием (Q)");
		public static PiramidaRecord Q_KL6_Shluz2_Otd = new PiramidaRecord(0, 8738, 16, "КЛ 6 Шлюз-2 отдача (Q)");

		public static PiramidaRecord P_GA3_Priem = new PiramidaRecord(0, 8738, 17, "Генератор-3 прием (P)");
		public static PiramidaRecord P_GA3_Otd = new PiramidaRecord(0, 8738, 18, "Генератор-3 отдача (P)");
		public static PiramidaRecord Q_GA3_Priem = new PiramidaRecord(0, 8738, 19, "Генератор-3 прием (Q)");
		public static PiramidaRecord Q_GA3_Otd = new PiramidaRecord(0, 8738, 20, "Генератор-3 отдача (Q)");

		public static PiramidaRecord P_GA4_Priem = new PiramidaRecord(0, 8738, 21, "Генератор-4 прием (P)");
		public static PiramidaRecord P_GA4_Otd = new PiramidaRecord(0, 8738, 22, "Генератор-4 отдача (P)");
		public static PiramidaRecord Q_GA4_Priem = new PiramidaRecord(0, 8738, 23, "Генератор-4 прием (Q)");
		public static PiramidaRecord Q_GA4_Otd = new PiramidaRecord(0, 8738, 24, "Генератор-4 отдача (Q)");

		public static PiramidaRecord P_GA5_Priem = new PiramidaRecord(0, 8738, 25, "Генератор-5 прием (P)");
		public static PiramidaRecord P_GA5_Otd = new PiramidaRecord(0, 8738, 26, "Генератор-5 отдача (P)");
		public static PiramidaRecord Q_GA5_Priem = new PiramidaRecord(0, 8738, 27, "Генератор-5 прием (Q)");
		public static PiramidaRecord Q_GA5_Otd = new PiramidaRecord(0, 8738, 28, "Генератор-5 отдача (Q)");

		public static PiramidaRecord P_GA6_Priem = new PiramidaRecord(0, 8738, 29, "Генератор-6 прием (P)");
		public static PiramidaRecord P_GA6_Otd = new PiramidaRecord(0, 8738, 30, "Генератор-6 отдача (P)");
		public static PiramidaRecord Q_GA6_Priem = new PiramidaRecord(0, 8738, 31, "Генератор-6 прием (Q)");
		public static PiramidaRecord Q_GA6_Otd = new PiramidaRecord(0, 8738, 32, "Генератор-6 отдача (Q)");

		public static PiramidaRecord P_KL6_Filtr1_Priem = new PiramidaRecord(0, 8738, 33, "КЛ 6 Фильтр-1 прием (P)");
		public static PiramidaRecord P_KL6_Filtr1_Otd = new PiramidaRecord(0, 8738, 34, "КЛ 6 Фильтр-1 отдача (P)");
		public static PiramidaRecord Q_KL6_Filtr1_Priem = new PiramidaRecord(0, 8738, 35, "КЛ 6 Фильтр-1 прием (Q)");
		public static PiramidaRecord Q_KL6_Filtr1_Otd = new PiramidaRecord(0, 8738, 36, "КЛ 6 Фильтр-1 отдача (Q)");
		public static PiramidaRecord P_KL6_Filtr2_Priem = new PiramidaRecord(0, 8738, 37, "КЛ 6 Фильтр-2 прием (P)");
		public static PiramidaRecord P_KL6_Filtr2_Otd = new PiramidaRecord(0, 8738, 38, "КЛ 6 Фильтр-2 отдача (P)");
		public static PiramidaRecord Q_KL6_Filtr2_Priem = new PiramidaRecord(0, 8738, 39, "КЛ 6 Фильтр-2 прием (Q)");
		public static PiramidaRecord Q_KL6_Filtr2_Otd = new PiramidaRecord(0, 8738, 40, "КЛ 6 Фильтр-2 отдача (Q)");

		public static PiramidaRecord P_GA7_Priem = new PiramidaRecord(0, 8738, 41, "Генератор-7 прием (P)");
		public static PiramidaRecord P_GA7_Otd = new PiramidaRecord(0, 8738, 42, "Генератор-7 отдача (P)");
		public static PiramidaRecord Q_GA7_Priem = new PiramidaRecord(0, 8738, 43, "Генератор-7 прием (Q)");
		public static PiramidaRecord Q_GA7_Otd = new PiramidaRecord(0, 8738, 44, "Генератор-7 отдача (Q)");

		public static PiramidaRecord P_GA8_Priem = new PiramidaRecord(0, 8738, 45, "Генератор-8 прием (P)");
		public static PiramidaRecord P_GA8_Otd = new PiramidaRecord(0, 8738, 46, "Генератор-8 отдача (P)");
		public static PiramidaRecord Q_GA8_Priem = new PiramidaRecord(0, 8738, 47, "Генератор-8 прием (Q)");
		public static PiramidaRecord Q_GA8_Otd = new PiramidaRecord(0, 8738, 48, "Генератор-8 отдача (Q)");

		public static PiramidaRecord P_GA9_Priem = new PiramidaRecord(0, 8738, 49, "Генератор-9 прием (P)");
		public static PiramidaRecord P_GA9_Otd = new PiramidaRecord(0, 8738, 50, "Генератор-9 отдача (P)");
		public static PiramidaRecord Q_GA9_Priem = new PiramidaRecord(0, 8738, 51, "Генератор-9 прием (Q)");
		public static PiramidaRecord Q_GA9_Otd = new PiramidaRecord(0, 8738, 52, "Генератор-9 отдача (Q)");

		public static PiramidaRecord P_GA10_Priem = new PiramidaRecord(0, 8738, 53, "Генератор-10 прием (P)");
		public static PiramidaRecord P_GA10_Otd = new PiramidaRecord(0, 8738, 54, "Генератор-10 отдача (P)");
		public static PiramidaRecord Q_GA10_Priem = new PiramidaRecord(0, 8738, 55, "Генератор-10 прием (Q)");
		public static PiramidaRecord Q_GA10_Otd = new PiramidaRecord(0, 8738, 56, "Генератор-10 отдача (Q)");

		public static PiramidaRecord P_SN_9T_Priem = new PiramidaRecord(0, 8740, 57, "СН 9Т прием (P)");

		public static PiramidaRecord Water_NB = new PiramidaRecord(2, 1, 275, "НБ");
		public static PiramidaRecord Water_VB = new PiramidaRecord(2, 1, 274, "ВБ");
		public static PiramidaRecord Water_Napor = new PiramidaRecord(2, 1, 276, "Напор");
		public static PiramidaRecord Water_Temp = new PiramidaRecord(2, 1, 373, "Температура");
		public static PiramidaRecord Water_QGES = new PiramidaRecord(2, 1, 354, "Расход ГЭС");
		public static PiramidaRecord Water_QGG = new PiramidaRecord(2, 1, 355, "Расход ГГ");
		public static PiramidaRecord Water_QVP = new PiramidaRecord(2, 1, 356, "Расход ВП");
		public static PiramidaRecord Water_QOptGES = new PiramidaRecord(2, 10, 1, "Опт. расход ГЭС");
		public static PiramidaRecord Water_QOptGTP1 = new PiramidaRecord(2, 10, 2, "Опт. расход ГТП-1");
		public static PiramidaRecord Water_QOptGTP2 = new PiramidaRecord(2, 10, 3, "Опт. расход ГТП-2");

		public static PiramidaRecord Water_Q_GA1 = new PiramidaRecord(2, 1, 104, "Расход ГА-1");
		public static PiramidaRecord Water_Q_GA2 = new PiramidaRecord(2, 1, 129, "Расход ГА-2");
		public static PiramidaRecord Water_Q_GA3 = new PiramidaRecord(2, 1, 154, "Расход ГА-3");
		public static PiramidaRecord Water_Q_GA4 = new PiramidaRecord(2, 1, 179, "Расход ГА-4");
		public static PiramidaRecord Water_Q_GA5 = new PiramidaRecord(2, 1, 204, "Расход ГА-5");
		public static PiramidaRecord Water_Q_GA6 = new PiramidaRecord(2, 1, 229, "Расход ГА-6");
		public static PiramidaRecord Water_Q_GA7 = new PiramidaRecord(2, 1, 254, "Расход ГА-7");
		public static PiramidaRecord Water_Q_GA8 = new PiramidaRecord(2, 1, 279, "Расход ГА-8");
		public static PiramidaRecord Water_Q_GA9 = new PiramidaRecord(2, 1, 304, "Расход ГА-9");
		public static PiramidaRecord Water_Q_GA10 = new PiramidaRecord(2, 1, 329, "Расход ГА-10");

		public static PiramidaRecord GSV2 = new PiramidaRecord(2, 7, 2, "Верхний бьеф на 8 утра");
		public static PiramidaRecord GSV3 = new PiramidaRecord(2, 7, 3, "Нижний бьеф на 8 утра");
		public static PiramidaRecord GSV4 = new PiramidaRecord(2, 7, 4, "Нижний бьеф (средний за сутки)");
		public static PiramidaRecord GSV5 = new PiramidaRecord(2, 7, 5, "Нижний бьеф (макс. за сутки)");
		public static PiramidaRecord GSV6 = new PiramidaRecord(2, 7, 6, "Нижний бьеф (мин за сутки)");
		public static PiramidaRecord GSV7 = new PiramidaRecord(2, 7, 7, "Среднесуточный напор (брутто)");
		public static PiramidaRecord GSV8 = new PiramidaRecord(2, 7, 8, "Среднесуточный напор (нетто)");
		public static PiramidaRecord GSV9 = new PiramidaRecord(2, 7, 9, "Среднесуточный напор (нетто с учетом потери на сут.рег.)");
		public static PiramidaRecord GSV10 = new PiramidaRecord(2, 7, 10, "Перепад на решетках");
		public static PiramidaRecord GSV11 = new PiramidaRecord(2, 7, 11, "Суточная выработка эл.энергии");
		public static PiramidaRecord GSV12 = new PiramidaRecord(2, 7, 12, "Выработка эл.энергии с начала месяца");
		public static PiramidaRecord GSV13 = new PiramidaRecord(2, 7, 13, "Нагрузка ГЭС (средняя)");
		public static PiramidaRecord GSV14 = new PiramidaRecord(2, 7, 14, "Нагрузка ГЭС (макс)");
		public static PiramidaRecord GSV15 = new PiramidaRecord(2, 7, 15, "Нагрузка ГЭС (мин)");
		public static PiramidaRecord GSV16 = new PiramidaRecord(2, 7, 16, "Средний расход воды (турбины)");
		public static PiramidaRecord GSV17 = new PiramidaRecord(2, 7, 17, "Средний расход воды (водослив)");
		public static PiramidaRecord GSV18 = new PiramidaRecord(2, 7, 18, "Средний расход воды (фильтр)");
		public static PiramidaRecord GSV19 = new PiramidaRecord(2, 7, 19, "Средний расход воды (шлюзов.)");
		public static PiramidaRecord GSV20 = new PiramidaRecord(2, 7, 20, "Средний расход воды (общий)");
		public static PiramidaRecord GSV21 = new PiramidaRecord(2, 7, 21, "Удельный расход");
		public static PiramidaRecord GSV22 = new PiramidaRecord(2, 7, 22, "Расход в НБ КамГЭС");
		public static PiramidaRecord GSV23 = new PiramidaRecord(2, 7, 23, "Боковой приток");
		public static PiramidaRecord GSV24 = new PiramidaRecord(2, 7, 24, "Наш приток");
		public static PiramidaRecord GSV25 = new PiramidaRecord(2, 7, 25, "Верхний бьеф КамГЭС");
		public static PiramidaRecord GSV26 = new PiramidaRecord(2, 7, 26, "Нижний бьеф КамГЭС");
		public static PiramidaRecord GSV27 = new PiramidaRecord(2, 7, 27, "Приток КамГЭС");
		public static PiramidaRecord GSV28 = new PiramidaRecord(2, 1, 373, "Температура", "sut");

		public static PiramidaRecord MBW_GES_Rash = new PiramidaRecord(2, 3, 1, "ГЭС Расход");
		public static PiramidaRecord MBW_VB = new PiramidaRecord(2, 3, 2, "ВБ");
		public static PiramidaRecord MBW_NB = new PiramidaRecord(2, 3, 3, "НБ");
		public static PiramidaRecord MBW_Napor = new PiramidaRecord(2, 3, 4, "Напор");
		public static PiramidaRecord MBW_Temp = new PiramidaRecord(2, 3, 5, "Температура");
		public static PiramidaRecord MBW_TempShit = new PiramidaRecord(2, 3, 6, "Температура щитовых сооружений");
		public static PiramidaRecord MBW_GG_Rash = new PiramidaRecord(2, 3, 7, "ГГ Расход");
		public static PiramidaRecord MBW_VP_Rash = new PiramidaRecord(2, 3, 8, "ВП Расход");
		public static PiramidaRecord MBW_U110 = new PiramidaRecord(2, 30, 9, "U 110");
		public static PiramidaRecord MBW_U220 = new PiramidaRecord(2, 30, 10, "U 220");
		public static PiramidaRecord MBW_U500 = new PiramidaRecord(2, 30, 11, "U 500");
		public static PiramidaRecord MBW_U110_1SSH = new PiramidaRecord(2, 3, 12, "U 110 1СШ");
		public static PiramidaRecord MBW_U110_2SSH = new PiramidaRecord(2, 3, 13, "U 110 2СШ");
		public static PiramidaRecord MBW_U220_1SSH = new PiramidaRecord(2, 3, 14, "U 220 1СШ");
		public static PiramidaRecord MBW_U220_2SSH = new PiramidaRecord(2, 3, 15, "U 220 2СШ");
		public static PiramidaRecord MBW_U500_EML = new PiramidaRecord(2, 3, 16, "U 500 Емелино");
		public static PiramidaRecord MBW_U500_KARM = new PiramidaRecord(2, 3, 17, "U 500 Карманово");
		public static PiramidaRecord MBW_U500_VYAT = new PiramidaRecord(2, 3, 18, "U 500 Вятка");


		public static PiramidaRecord MBW_GA1_Rash = new PiramidaRecord(2, 3, 101, "ГА-1 Расход");
		public static PiramidaRecord MBW_GA2_Rash = new PiramidaRecord(2, 3, 102, "ГА-2 Расход");
		public static PiramidaRecord MBW_GA3_Rash = new PiramidaRecord(2, 3, 103, "ГА-3 Расход");
		public static PiramidaRecord MBW_GA4_Rash = new PiramidaRecord(2, 3, 104, "ГА-4 Расход");
		public static PiramidaRecord MBW_GA5_Rash = new PiramidaRecord(2, 3, 105, "ГА-5 Расход");
		public static PiramidaRecord MBW_GA6_Rash = new PiramidaRecord(2, 3, 106, "ГА-6 Расход");
		public static PiramidaRecord MBW_GA7_Rash = new PiramidaRecord(2, 3, 107, "ГА-7 Расход");
		public static PiramidaRecord MBW_GA8_Rash = new PiramidaRecord(2, 3, 108, "ГА-8 Расход");
		public static PiramidaRecord MBW_GA9_Rash = new PiramidaRecord(2, 3, 109, "ГА-9 Расход");
		public static PiramidaRecord MBW_GA10_Rash = new PiramidaRecord(2, 3, 110, "ГА-10 Расход");

		public static PiramidaRecord MBW_GA1_P = new PiramidaRecord(2, 3, 201, "ГА-1 P");
		public static PiramidaRecord MBW_GA2_P = new PiramidaRecord(2, 3, 202, "ГА-2 P");
		public static PiramidaRecord MBW_GA3_P = new PiramidaRecord(2, 3, 203, "ГА-3 P");
		public static PiramidaRecord MBW_GA4_P = new PiramidaRecord(2, 3, 204, "ГА-4 P");
		public static PiramidaRecord MBW_GA5_P = new PiramidaRecord(2, 3, 205, "ГА-5 P");
		public static PiramidaRecord MBW_GA6_P = new PiramidaRecord(2, 3, 206, "ГА-6 P");
		public static PiramidaRecord MBW_GA7_P = new PiramidaRecord(2, 3, 207, "ГА-7 P");
		public static PiramidaRecord MBW_GA8_P = new PiramidaRecord(2, 3, 208, "ГА-8 P");
		public static PiramidaRecord MBW_GA9_P = new PiramidaRecord(2, 3, 209, "ГА-9 P");
		public static PiramidaRecord MBW_GA10_P = new PiramidaRecord(2, 3, 210, "ГА-10 P");

		public static PiramidaRecord MBW_GA1_PF = new PiramidaRecord(2, 3, 301, "ГА-1 коррекция P по F");
		public static PiramidaRecord MBW_GA2_PF = new PiramidaRecord(2, 3, 302, "ГА-2 коррекция P по F");
		public static PiramidaRecord MBW_GA3_PF = new PiramidaRecord(2, 3, 303, "ГА-3 коррекция P по F");
		public static PiramidaRecord MBW_GA4_PF = new PiramidaRecord(2, 3, 304, "ГА-4 коррекция P по F");
		public static PiramidaRecord MBW_GA5_PF = new PiramidaRecord(2, 3, 305, "ГА-5 коррекция P по F");
		public static PiramidaRecord MBW_GA6_PF = new PiramidaRecord(2, 3, 306, "ГА-6 коррекция P по F");
		public static PiramidaRecord MBW_GA7_PF = new PiramidaRecord(2, 3, 307, "ГА-7 коррекция P по F");
		public static PiramidaRecord MBW_GA8_PF = new PiramidaRecord(2, 3, 308, "ГА-8 коррекция P по F");
		public static PiramidaRecord MBW_GA9_PF = new PiramidaRecord(2, 3, 309, "ГА-9 коррекция P по F");
		public static PiramidaRecord MBW_GA10_PF = new PiramidaRecord(2, 3, 310, "ГА-10 коррекция P по F");

		public static PiramidaRecord MBW_GA1_OtkrNA = new PiramidaRecord(2, 3, 401, "ГА-1 Открытие НА");
		public static PiramidaRecord MBW_GA2_OtkrNA = new PiramidaRecord(2, 3, 402, "ГА-2 Открытие НА");
		public static PiramidaRecord MBW_GA3_OtkrNA = new PiramidaRecord(2, 3, 403, "ГА-3 Открытие НА");
		public static PiramidaRecord MBW_GA4_OtkrNA = new PiramidaRecord(2, 3, 404, "ГА-4 Открытие НА");
		public static PiramidaRecord MBW_GA5_OtkrNA = new PiramidaRecord(2, 3, 405, "ГА-5 Открытие НА");
		public static PiramidaRecord MBW_GA6_OtkrNA = new PiramidaRecord(2, 3, 406, "ГА-6 Открытие НА");
		public static PiramidaRecord MBW_GA7_OtkrNA = new PiramidaRecord(2, 3, 407, "ГА-7 Открытие НА");
		public static PiramidaRecord MBW_GA8_OtkrNA = new PiramidaRecord(2, 3, 408, "ГА-8 Открытие НА");
		public static PiramidaRecord MBW_GA9_OtkrNA = new PiramidaRecord(2, 3, 409, "ГА-9 Открытие НА");
		public static PiramidaRecord MBW_GA10_OtkrNA = new PiramidaRecord(2, 3, 410, "ГА-10 Открытие НА");

		public static PiramidaRecord MBW_GA1_UgolRK = new PiramidaRecord(2, 3, 501, "ГА-1 Угол открытия  РК");
		public static PiramidaRecord MBW_GA2_UgolRK = new PiramidaRecord(2, 3, 502, "ГА-2 Угол открытия  РК");
		public static PiramidaRecord MBW_GA3_UgolRK = new PiramidaRecord(2, 3, 503, "ГА-3 Угол открытия  РК");
		public static PiramidaRecord MBW_GA4_UgolRK = new PiramidaRecord(2, 3, 504, "ГА-4 Угол открытия  РК");
		public static PiramidaRecord MBW_GA5_UgolRK = new PiramidaRecord(2, 3, 505, "ГА-5 Угол открытия  РК");
		public static PiramidaRecord MBW_GA6_UgolRK = new PiramidaRecord(2, 3, 506, "ГА-6 Угол открытия  РК");
		public static PiramidaRecord MBW_GA7_UgolRK = new PiramidaRecord(2, 3, 507, "ГА-7 Угол открытия  РК");
		public static PiramidaRecord MBW_GA8_UgolRK = new PiramidaRecord(2, 3, 508, "ГА-8 Угол открытия  РК");
		public static PiramidaRecord MBW_GA9_UgolRK = new PiramidaRecord(2, 3, 509, "ГА-9 Угол открытия  РК");
		public static PiramidaRecord MBW_GA10_UgolRK = new PiramidaRecord(2, 3, 510, "ГА-10 Угол открытия  РК");

		public static PiramidaRecord MBW_GA1_Napor = new PiramidaRecord(2, 3, 601, "ГА-1 Напор");
		public static PiramidaRecord MBW_GA2_Napor = new PiramidaRecord(2, 3, 602, "ГА-2 Напор");
		public static PiramidaRecord MBW_GA3_Napor = new PiramidaRecord(2, 3, 603, "ГА-3 Напор");
		public static PiramidaRecord MBW_GA4_Napor = new PiramidaRecord(2, 3, 604, "ГА-4 Напор");
		public static PiramidaRecord MBW_GA5_Napor = new PiramidaRecord(2, 3, 605, "ГА-5 Напор");
		public static PiramidaRecord MBW_GA6_Napor = new PiramidaRecord(2, 3, 606, "ГА-6 Напор");
		public static PiramidaRecord MBW_GA7_Napor = new PiramidaRecord(2, 3, 607, "ГА-7 Напор");
		public static PiramidaRecord MBW_GA8_Napor = new PiramidaRecord(2, 3, 608, "ГА-8 Напор");
		public static PiramidaRecord MBW_GA9_Napor = new PiramidaRecord(2, 3, 609, "ГА-9 Напор");
		public static PiramidaRecord MBW_GA10_Napor = new PiramidaRecord(2, 3, 610, "ГА-10 Напор");


		public static PiramidaRecord NPRCH_GA1_P = new PiramidaRecord(2, 5, 101, "ГА-1 P");
		public static PiramidaRecord NPRCH_GA2_P = new PiramidaRecord(2, 5, 102, "ГА-2 P");
		public static PiramidaRecord NPRCH_GA3_P = new PiramidaRecord(2, 5, 103, "ГА-3 P");
		public static PiramidaRecord NPRCH_GA4_P = new PiramidaRecord(2, 5, 104, "ГА-4 P");
		public static PiramidaRecord NPRCH_GA5_P = new PiramidaRecord(2, 5, 105, "ГА-5 P");
		public static PiramidaRecord NPRCH_GA6_P = new PiramidaRecord(2, 5, 106, "ГА-6 P");
		public static PiramidaRecord NPRCH_GA7_P = new PiramidaRecord(2, 5, 107, "ГА-7 P");
		public static PiramidaRecord NPRCH_GA8_P = new PiramidaRecord(2, 5, 108, "ГА-8 P");
		public static PiramidaRecord NPRCH_GA9_P = new PiramidaRecord(2, 5, 109, "ГА-9 P");
		public static PiramidaRecord NPRCH_GA10_P = new PiramidaRecord(2, 5, 110, "ГА-10 P");

		public static PiramidaRecord NPRCH_GA1_PZad = new PiramidaRecord(2, 5, 201, "ГА-1 задание P");
		public static PiramidaRecord NPRCH_GA2_PZad = new PiramidaRecord(2, 5, 202, "ГА-2 задание P");
		public static PiramidaRecord NPRCH_GA3_PZad = new PiramidaRecord(2, 5, 203, "ГА-3 задание P");
		public static PiramidaRecord NPRCH_GA4_PZad = new PiramidaRecord(2, 5, 204, "ГА-4 задание P");
		public static PiramidaRecord NPRCH_GA5_PZad = new PiramidaRecord(2, 5, 205, "ГА-5 задание P");
		public static PiramidaRecord NPRCH_GA6_PZad = new PiramidaRecord(2, 5, 206, "ГА-6 задание P");
		public static PiramidaRecord NPRCH_GA7_PZad = new PiramidaRecord(2, 5, 207, "ГА-7 задание P");
		public static PiramidaRecord NPRCH_GA8_PZad = new PiramidaRecord(2, 5, 208, "ГА-8 задание P");
		public static PiramidaRecord NPRCH_GA9_PZad = new PiramidaRecord(2, 5, 209, "ГА-9 задание P");
		public static PiramidaRecord NPRCH_GA10_PZad = new PiramidaRecord(2, 5, 210, "ГА-10 задание P");

		public static PiramidaRecord NPRCH_GA1_PF = new PiramidaRecord(2, 5, 301, "ГА-1 P коррекция");
		public static PiramidaRecord NPRCH_GA2_PF = new PiramidaRecord(2, 5, 302, "ГА-2 P коррекция");
		public static PiramidaRecord NPRCH_GA3_PF = new PiramidaRecord(2, 5, 303, "ГА-3 P коррекция");
		public static PiramidaRecord NPRCH_GA4_PF = new PiramidaRecord(2, 5, 304, "ГА-4 P коррекция");
		public static PiramidaRecord NPRCH_GA5_PF = new PiramidaRecord(2, 5, 305, "ГА-5 P коррекция");
		public static PiramidaRecord NPRCH_GA6_PF = new PiramidaRecord(2, 5, 306, "ГА-6 P коррекция");
		public static PiramidaRecord NPRCH_GA7_PF = new PiramidaRecord(2, 5, 307, "ГА-7 P коррекция");
		public static PiramidaRecord NPRCH_GA8_PF = new PiramidaRecord(2, 5, 308, "ГА-8 P коррекция");
		public static PiramidaRecord NPRCH_GA9_PF = new PiramidaRecord(2, 5, 309, "ГА-9 P коррекция");
		public static PiramidaRecord NPRCH_GA10_PF = new PiramidaRecord(2, 5, 310, "ГА-10 P коррекция");
				
		/*
		=СЦЕПИТЬ("public static PiramidaRecord MB";[@Addr];"=new PiramidaRecord(2, 3,"; [@Addr];", """;[@Name];""");")
		 * */


		static void PiramidaRecord() {

		}

		public static void addRecord(string key, int objType, int obj, int item, string title) {
		}

	}
}

