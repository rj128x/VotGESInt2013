﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VotGES.Piramida.Report
{
	public class ReportLinesRecords
	{
		public static RecordTypeCalc P_VL110_Otd=new RecordTypeCalc("P_VL110_Otd", "ВЛ 110кВ отдача P", null);
		public static RecordTypeCalc P_VL110_Priem=new RecordTypeCalc("P_VL110_Priem", "ВЛ 110кВ прием P", null);
		public static RecordTypeCalc P_VL220_Otd=new RecordTypeCalc("P_VL220_Otd", "ВЛ 220кВ отдача P", null);
		public static RecordTypeCalc P_VL220_Priem=new RecordTypeCalc("P_VL220_Priem", "ВЛ 220кВ прием P", null);
		public static RecordTypeCalc P_VL500_Otd=new RecordTypeCalc("P_VL500_Otd", "ВЛ 500кВ отдача P", null);
		public static RecordTypeCalc P_VL500_Priem=new RecordTypeCalc("P_VL500_Priem", "ВЛ 500кВ прием P", null);
		public static RecordTypeCalc P_KL6_Otd=new RecordTypeCalc("P_KL6_Otd", "КЛ 6кВ отдача P", null);
		public static RecordTypeCalc P_KL6_Priem=new RecordTypeCalc("P_KL6_Priem", "КЛ 6кВ прием P", null);

		public static RecordTypeCalc P_VL110_Saldo=new RecordTypeCalc("P_VL110_Saldo", "ВЛ 110кВ сальдо P", null);
		public static RecordTypeCalc P_VL220_Saldo=new RecordTypeCalc("P_VL220_Saldo", "ВЛ 220кВ сальдо P", null);
		public static RecordTypeCalc P_VL500_Saldo=new RecordTypeCalc("P_VL500_Saldo", "ВЛ 500кВ сальдо P", null);
		public static RecordTypeCalc P_KL6_Saldo=new RecordTypeCalc("P_KL6_Saldo", "КЛ 6кВ сальдо P", null);

		public static RecordTypeCalc P_VL110_Nebalans=new RecordTypeCalc("P_VL110_Nebalans", "ВЛ 110кВ небаланс P", null);
		public static RecordTypeCalc P_VL220_Nebalans=new RecordTypeCalc("P_VL220_Nebalans", "ВЛ 220кВ небаланс P", null);
		public static RecordTypeCalc P_VL500_Nebalans=new RecordTypeCalc("P_VL500_Nebalans", "ВЛ 500кВ небаланс P", null);
		public static RecordTypeCalc P_VL_Nebalans=new RecordTypeCalc("P_VL_Nebalans", "ВЛ небаланс P", null);

		public static RecordTypeCalc P_VL110_FullP = new RecordTypeCalc("P_VL110_FullP", "ВЛ 110кВ полный переток", null);
		public static RecordTypeCalc P_VL220_FullP = new RecordTypeCalc("P_VL220_FullP", "ВЛ 220кВ полный переток", null);
		public static RecordTypeCalc P_VL500_FullP = new RecordTypeCalc("P_VL500_FullP", "ВЛ 500кВ полный переток", null);
		public static RecordTypeCalc P_VL_FullP = new RecordTypeCalc("P_VL_FullP", "ВЛ полный переток", null);

		public static RecordTypeCalc P_VL110_Berezovka=new RecordTypeCalc("P_VL110_Berezovka", "ВЛ 110кВ Березовка P", null);
		public static RecordTypeCalc P_VL110_Dubovaya=new RecordTypeCalc("P_VL110_Dubovaya", "ВЛ 110кВ Дубовая P", null);
		public static RecordTypeCalc P_VL110_Ivanovka=new RecordTypeCalc("P_VL110_Ivanovka", "ВЛ 110кВ Ивановка P", null);
		public static RecordTypeCalc P_VL110_Kauchuk=new RecordTypeCalc("P_VL110_Kauchuk", "ВЛ 110кВ Каучук P", null);
		public static RecordTypeCalc P_VL110_KSHT1=new RecordTypeCalc("P_VL110_KSHT1", "ВЛ 110кВ КШТ-1 P", null);
		public static RecordTypeCalc P_VL110_KSHT2=new RecordTypeCalc("P_VL110_KSHT2", "ВЛ 110кВ КШТ-2 P", null);
		public static RecordTypeCalc P_VL110_Svetlaya=new RecordTypeCalc("P_VL110_Svetlaya", "ВЛ 110кВ Светлая P", null);
		public static RecordTypeCalc P_VL110_TEC=new RecordTypeCalc("P_VL110_TEC", "ВЛ 110кВ ТЭЦ P", null);
		public static RecordTypeCalc P_VL110_Vodozabor1=new RecordTypeCalc("P_VL110_Vodozabor1", "ВЛ 110кВ Водозабор1 P", null);
		public static RecordTypeCalc P_VL110_Vodozabor2=new RecordTypeCalc("P_VL110_Vodozabor2", "ВЛ 110кВ Водозабор2 P", null);

		public static RecordTypeCalc P_VL220_Izhevsk1=new RecordTypeCalc("P_VL220_Izhevsk1", "ВЛ 220кВ Ижевск-1 P", null);
		public static RecordTypeCalc P_VL220_Izhevsk2=new RecordTypeCalc("P_VL220_Izhevsk2", "ВЛ 220кВ Ижевск-2 P", null);
		public static RecordTypeCalc P_VL220_Kauchuk1=new RecordTypeCalc("P_VL220_Kauchuk1", "ВЛ 220кВ Каучук-1 P", null);
		public static RecordTypeCalc P_VL220_Kauchuk2=new RecordTypeCalc("P_VL220_Kauchuk2", "ВЛ 220кВ Каучук-2 P", null);
		public static RecordTypeCalc P_VL220_Svetlaya=new RecordTypeCalc("P_VL220_Svetlaya", "ВЛ 220кВ Светлая P", null);

		public static RecordTypeCalc P_KL_Shluz1=new RecordTypeCalc("P_KL_Shluz1", "КЛ 6кВ Шлюз-1 P", null);
		public static RecordTypeCalc P_KL_Shluz2=new RecordTypeCalc("P_KL_Shluz2", "КЛ 6кВ Шлюз-2 P", null);
		public static RecordTypeCalc P_KL_Filtr1=new RecordTypeCalc("P_KL_Filtr1", "КЛ 6кВ Фильтровальная-1 P", null);
		public static RecordTypeCalc P_KL_Filtr2=new RecordTypeCalc("P_KL_Filtr2", "КЛ 6кВ Фильтровальная-2 P", null);


		public static RecordTypeCalc P_VL500_Emelino=new RecordTypeCalc("P_VL500_Emelino", "ВЛ 500кВ Емелино P", null);
		public static RecordTypeCalc P_VL500_Karmanovo=new RecordTypeCalc("P_VL500_Karmanovo", "ВЛ 500кВ Карманово P", null);
		public static RecordTypeCalc P_VL500_Vyatka=new RecordTypeCalc("P_VL500_Vyatka", "ВЛ 500кВ Вятка P", null);

		public static RecordTypeCalc Q_VL110_Otd=new RecordTypeCalc("Q_VL110_Otd", "ВЛ 110кВ отдача Q", null);
		public static RecordTypeCalc Q_VL110_Priem=new RecordTypeCalc("Q_VL110_Priem", "ВЛ 110кВ прием Q", null);
		public static RecordTypeCalc Q_VL220_Otd=new RecordTypeCalc("Q_VL220_Otd", "ВЛ 220кВ отдача Q", null);
		public static RecordTypeCalc Q_VL220_Priem=new RecordTypeCalc("Q_VL220_Priem", "ВЛ 220кВ прием Q", null);
		public static RecordTypeCalc Q_VL500_Otd=new RecordTypeCalc("Q_VL500_Otd", "ВЛ 500кВ отдача Q", null);
		public static RecordTypeCalc Q_VL500_Priem=new RecordTypeCalc("Q_VL500_Priem", "ВЛ 500кВ прием Q", null);
		public static RecordTypeCalc Q_KL6_Otd=new RecordTypeCalc("Q_KL6_Otd", "КЛ 6кВ отдача Q", null);
		public static RecordTypeCalc Q_KL6_Priem=new RecordTypeCalc("Q_KL6_Priem", "КЛ 6кВ прием Q", null);

		public static RecordTypeCalc Q_VL110_Saldo=new RecordTypeCalc("Q_VL110_Saldo", "ВЛ 110кВ сальдо Q", null);
		public static RecordTypeCalc Q_VL220_Saldo=new RecordTypeCalc("Q_VL220_Saldo", "ВЛ 220кВ сальдо Q", null);
		public static RecordTypeCalc Q_VL500_Saldo=new RecordTypeCalc("Q_VL500_Saldo", "ВЛ 500кВ сальдо Q", null);
		public static RecordTypeCalc Q_KL6_Saldo=new RecordTypeCalc("Q_KL6_Saldo", "КЛ 6кВ сальдо Q", null);

		public static RecordTypeCalc Q_VL110_Nebalans=new RecordTypeCalc("Q_VL110_Nebalans", "ВЛ 110кВ небаланс Q", null);
		public static RecordTypeCalc Q_VL220_Nebalans=new RecordTypeCalc("Q_VL220_Nebalans", "ВЛ 220кВ небаланс Q", null);
		public static RecordTypeCalc Q_VL500_Nebalans=new RecordTypeCalc("Q_VL500_Nebalans", "ВЛ 5000кВ небаланс Q", null);
		public static RecordTypeCalc Q_VL_Nebalans=new RecordTypeCalc("Q_VL_Nebalans", "ВЛ небаланс Q", null);

		public static RecordTypeCalc Q_VL110_Berezovka=new RecordTypeCalc("Q_VL110_Berezovka", "ВЛ 110кВ Березовка Q", null);
		public static RecordTypeCalc Q_VL110_Dubovaya=new RecordTypeCalc("Q_VL110_Dubovaya", "ВЛ 110кВ Дубовая Q", null);
		public static RecordTypeCalc Q_VL110_Ivanovka=new RecordTypeCalc("Q_VL110_Ivanovka", "ВЛ 110кВ Ивановка Q", null);
		public static RecordTypeCalc Q_VL110_Kauchuk=new RecordTypeCalc("Q_VL110_Kauchuk", "ВЛ 110кВ Каучук Q", null);
		public static RecordTypeCalc Q_VL110_KSHT1=new RecordTypeCalc("Q_VL110_KSHT1", "ВЛ 110кВ КШТ-1 Q", null);
		public static RecordTypeCalc Q_VL110_KSHT2=new RecordTypeCalc("Q_VL110_KSHT2", "ВЛ 110кВ КШТ-2 Q", null);
		public static RecordTypeCalc Q_VL110_Svetlaya=new RecordTypeCalc("Q_VL110_Svetlaya", "ВЛ 110кВ Светлая Q", null);
		public static RecordTypeCalc Q_VL110_TEC=new RecordTypeCalc("Q_VL110_TEC", "ВЛ 110кВ ТЭЦ Q", null);
		public static RecordTypeCalc Q_VL110_Vodozabor1=new RecordTypeCalc("Q_VL110_Vodozabor1", "ВЛ 110кВ Водозабор1 Q", null);
		public static RecordTypeCalc Q_VL110_Vodozabor2=new RecordTypeCalc("Q_VL110_Vodozabor2", "ВЛ 110кВ Водозабор2 Q", null);

		public static RecordTypeCalc Q_VL220_Izhevsk1=new RecordTypeCalc("Q_VL220_Izhevsk1", "ВЛ 220кВ Ижевск-1 Q", null);
		public static RecordTypeCalc Q_VL220_Izhevsk2=new RecordTypeCalc("Q_VL220_Izhevsk2", "ВЛ 220кВ Ижевск-2 Q", null);
		public static RecordTypeCalc Q_VL220_Kauchuk1=new RecordTypeCalc("Q_VL220_Kauchuk1", "ВЛ 220кВ Каучук-1 Q", null);
		public static RecordTypeCalc Q_VL220_Kauchuk2=new RecordTypeCalc("Q_VL220_Kauchuk2", "ВЛ 220кВ Каучук-2 Q", null);
		public static RecordTypeCalc Q_VL220_Svetlaya=new RecordTypeCalc("Q_VL220_Svetlaya", "ВЛ 220кВ Светлая Q", null);

		public static RecordTypeCalc Q_KL_Shluz1=new RecordTypeCalc("Q_KL_Shluz1", "КЛ 6кВ Шлюз-1 Q", null);
		public static RecordTypeCalc Q_KL_Shluz2=new RecordTypeCalc("Q_KL_Shluz2", "КЛ 6кВ Шлюз-2 Q", null);
		public static RecordTypeCalc Q_KL_Filtr1=new RecordTypeCalc("Q_KL_Filtr1", "КЛ 6кВ Фильтровальная-1 Q", null);
		public static RecordTypeCalc Q_KL_Filtr2=new RecordTypeCalc("Q_KL_Filtr2", "КЛ 6кВ Фильтровальная-2 Q", null);


		public static RecordTypeCalc Q_VL500_Emelino=new RecordTypeCalc("Q_VL500_Emelino", "ВЛ 500кВ Емелино Q", null);
		public static RecordTypeCalc Q_VL500_Karmanovo=new RecordTypeCalc("Q_VL500_Karmanovo", "ВЛ 500кВ Карманово Q", null);
		public static RecordTypeCalc Q_VL500_Vyatka=new RecordTypeCalc("Q_VL500_Vyatka", "ВЛ 500кВ Вятка Q", null);
		
		static ReportLinesRecords() {
			CreateLinesP();
		}

		public static void CreateLinesP() {
			P_VL110_Berezovka.CalcFunction = new RecordCalcDelegate((report, date) => {
				return report[date, PiramidaRecords.P_VL110_Berezovka_Priem.Key] - report[date, PiramidaRecords.P_VL110_Berezovka_Otd.Key];
			});

			P_VL110_Dubovaya.CalcFunction = new RecordCalcDelegate((report, date) => {
				return report[date, PiramidaRecords.P_VL110_Dubovaya_Priem.Key] - report[date, PiramidaRecords.P_VL110_Dubovaya_Otd.Key];
			});

			P_VL110_Ivanovka.CalcFunction = new RecordCalcDelegate((report, date) => {
				return report[date, PiramidaRecords.P_VL110_Ivanovka_Priem.Key] - report[date, PiramidaRecords.P_VL110_Ivanovka_Otd.Key];
			});

			P_VL110_Kauchuk.CalcFunction = new RecordCalcDelegate((report, date) => {
				return report[date, PiramidaRecords.P_VL110_Kauchuk_Priem.Key] - report[date, PiramidaRecords.P_VL110_Kauchuk_Otd.Key];
			});

			P_VL110_KSHT1.CalcFunction = new RecordCalcDelegate((report, date) => {
				return report[date, PiramidaRecords.P_VL110_KSHT1_Priem.Key] - report[date, PiramidaRecords.P_VL110_KSHT1_Otd.Key];
			});

			P_VL110_KSHT2.CalcFunction = new RecordCalcDelegate((report, date) => {
				return report[date, PiramidaRecords.P_VL110_KSHT2_Priem.Key] - report[date, PiramidaRecords.P_VL110_KSHT2_Otd.Key];
			});

			P_VL110_Svetlaya.CalcFunction = new RecordCalcDelegate((report, date) => {
				return report[date, PiramidaRecords.P_VL110_Svetlaya_Priem.Key] - report[date, PiramidaRecords.P_VL110_Svetlaya_Otd.Key];
			});

			P_VL110_TEC.CalcFunction = new RecordCalcDelegate((report, date) => {
				return report[date, PiramidaRecords.P_VL110_TEC_Priem.Key] - report[date, PiramidaRecords.P_VL110_TEC_Otd.Key];
			});

			P_VL110_Vodozabor1.CalcFunction = new RecordCalcDelegate((report, date) => {
				return report[date, PiramidaRecords.P_VL110_Vodozabor1_Priem.Key] - report[date, PiramidaRecords.P_VL110_Vodozabor1_Otd.Key];
			});

			P_VL110_Vodozabor2.CalcFunction = new RecordCalcDelegate((report, date) => {
				return report[date, PiramidaRecords.P_VL110_Vodozabor2_Priem.Key] - report[date, PiramidaRecords.P_VL110_Vodozabor2_Otd.Key];
			});

			P_VL220_Izhevsk1.CalcFunction = new RecordCalcDelegate((report, date) => {
				return report[date, PiramidaRecords.P_VL220_Izhevsk1_Priem.Key] - report[date, PiramidaRecords.P_VL220_Izhevsk1_Otd.Key];
			});

			P_VL220_Izhevsk2.CalcFunction = new RecordCalcDelegate((report, date) => {
				return report[date, PiramidaRecords.P_VL220_Izhevsk2_Priem.Key] - report[date, PiramidaRecords.P_VL220_Izhevsk2_Otd.Key];
			});

			P_VL220_Kauchuk1.CalcFunction = new RecordCalcDelegate((report, date) => {
				return report[date, PiramidaRecords.P_VL220_Kauchuk1_Priem.Key] - report[date, PiramidaRecords.P_VL220_Kauchuk1_Otd.Key];
			});

			P_VL220_Kauchuk2.CalcFunction = new RecordCalcDelegate((report, date) => {
				return report[date, PiramidaRecords.P_VL220_Kauchuk2_Priem.Key] - report[date, PiramidaRecords.P_VL220_Kauchuk2_Otd.Key];
			});

			P_VL220_Svetlaya.CalcFunction = new RecordCalcDelegate((report, date) => {
				return report[date, PiramidaRecords.P_VL220_Svetlaya_Priem.Key] - report[date, PiramidaRecords.P_VL220_Svetlaya_Otd.Key];
			});

			P_VL500_Emelino.CalcFunction = new RecordCalcDelegate((report, date) => {
				return report[date, PiramidaRecords.P_VL500_Emelino_Priem.Key] - report[date, PiramidaRecords.P_VL500_Emelino_Otd.Key];
			});

			P_VL500_Karmanovo.CalcFunction = new RecordCalcDelegate((report, date) => {
				return report[date, PiramidaRecords.P_VL500_Karmanovo_Priem.Key] - report[date, PiramidaRecords.P_VL500_Karmanovo_Otd.Key];
			});

			P_VL500_Vyatka.CalcFunction = new RecordCalcDelegate((report, date) => {
				return report[date, PiramidaRecords.P_VL500_Vyatka_Priem.Key] - report[date, PiramidaRecords.P_VL500_Vyatka_Otd.Key];
			});

			P_KL_Shluz1.CalcFunction = new RecordCalcDelegate((report, date) => {
				return report[date, PiramidaRecords.P_KL6_Shluz1_Priem.Key] - report[date, PiramidaRecords.P_KL6_Shluz1_Otd.Key];
			});

			P_KL_Shluz2.CalcFunction = new RecordCalcDelegate((report, date) => {
				return report[date, PiramidaRecords.P_KL6_Shluz2_Priem.Key] - report[date, PiramidaRecords.P_KL6_Shluz2_Otd.Key];
			});

			P_KL_Filtr1.CalcFunction = new RecordCalcDelegate((report, date) => {
				return report[date, PiramidaRecords.P_KL6_Filtr1_Priem.Key] - report[date, PiramidaRecords.P_KL6_Filtr1_Otd.Key];
			});

			P_KL_Filtr2.CalcFunction = new RecordCalcDelegate((report, date) => {
				return report[date, PiramidaRecords.P_KL6_Filtr2_Priem.Key] - report[date, PiramidaRecords.P_KL6_Filtr2_Otd.Key];
			});

			P_KL6_Otd.CalcFunction = new RecordCalcDelegate((report, date) => {
				return
					report[date, PiramidaRecords.P_KL6_Filtr1_Otd.Key] +
					report[date, PiramidaRecords.P_KL6_Filtr2_Otd.Key] +
					report[date, PiramidaRecords.P_KL6_Shluz1_Otd.Key] +
					report[date, PiramidaRecords.P_KL6_Shluz2_Otd.Key];
			});

			P_KL6_Priem.CalcFunction = new RecordCalcDelegate((report, date) => {
				return
					report[date, PiramidaRecords.P_KL6_Filtr1_Priem.Key] +
					report[date, PiramidaRecords.P_KL6_Filtr2_Priem.Key] +
					report[date, PiramidaRecords.P_KL6_Shluz1_Priem.Key] +
					report[date, PiramidaRecords.P_KL6_Shluz2_Priem.Key];
			});

			P_VL110_Otd.CalcFunction=new RecordCalcDelegate((report, date) => {
				return
					report[date,PiramidaRecords.P_VL110_Berezovka_Otd.Key] +
					report[date,PiramidaRecords.P_VL110_Dubovaya_Otd.Key] +
					report[date,PiramidaRecords.P_VL110_Ivanovka_Otd.Key] +
					report[date,PiramidaRecords.P_VL110_Kauchuk_Otd.Key] +
					report[date,PiramidaRecords.P_VL110_KSHT1_Otd.Key] +
					report[date,PiramidaRecords.P_VL110_KSHT2_Otd.Key] +
					report[date,PiramidaRecords.P_VL110_Svetlaya_Otd.Key] +
					report[date,PiramidaRecords.P_VL110_TEC_Otd.Key] +
					report[date,PiramidaRecords.P_VL110_Vodozabor1_Otd.Key] +
					report[date,PiramidaRecords.P_VL110_Vodozabor2_Otd.Key];
			});

			P_VL110_Priem.CalcFunction=new RecordCalcDelegate((report, date) => {
				return
					report[date,PiramidaRecords.P_VL110_Berezovka_Priem.Key] +
					report[date,PiramidaRecords.P_VL110_Dubovaya_Priem.Key] +
					report[date,PiramidaRecords.P_VL110_Ivanovka_Priem.Key] +
					report[date,PiramidaRecords.P_VL110_Kauchuk_Priem.Key] +
					report[date,PiramidaRecords.P_VL110_KSHT1_Priem.Key] +
					report[date,PiramidaRecords.P_VL110_KSHT2_Priem.Key] +
					report[date,PiramidaRecords.P_VL110_Svetlaya_Priem.Key] +
					report[date,PiramidaRecords.P_VL110_TEC_Priem.Key] +
					report[date,PiramidaRecords.P_VL110_Vodozabor1_Priem.Key] +
					report[date,PiramidaRecords.P_VL110_Vodozabor2_Priem.Key];
			});

			P_VL220_Otd.CalcFunction=new RecordCalcDelegate((report, date) => {
				return
					report[date,PiramidaRecords.P_VL220_Izhevsk1_Otd.Key] +
					report[date,PiramidaRecords.P_VL220_Izhevsk2_Otd.Key] +
					report[date,PiramidaRecords.P_VL220_Kauchuk1_Otd.Key] +
					report[date,PiramidaRecords.P_VL220_Kauchuk2_Otd.Key] +
					report[date,PiramidaRecords.P_VL220_Svetlaya_Otd.Key];
			});

			P_VL220_Priem.CalcFunction=new RecordCalcDelegate((report, date) => {
				return
					report[date,PiramidaRecords.P_VL220_Izhevsk1_Priem.Key] +
					report[date,PiramidaRecords.P_VL220_Izhevsk2_Priem.Key] +
					report[date,PiramidaRecords.P_VL220_Kauchuk1_Priem.Key] +
					report[date,PiramidaRecords.P_VL220_Kauchuk2_Priem.Key] +
					report[date,PiramidaRecords.P_VL220_Svetlaya_Priem.Key];
			});

			P_VL500_Otd.CalcFunction=new RecordCalcDelegate((report, date) => {
				return
					report[date,PiramidaRecords.P_VL500_Emelino_Otd.Key] +
					report[date,PiramidaRecords.P_VL500_Karmanovo_Otd.Key] +
					report[date,PiramidaRecords.P_VL500_Vyatka_Otd.Key];
			});

			P_VL500_Priem.CalcFunction= new RecordCalcDelegate((report, date) => {
				return
					report[date,PiramidaRecords.P_VL500_Emelino_Priem.Key] +
					report[date,PiramidaRecords.P_VL500_Karmanovo_Priem.Key] +
					report[date,PiramidaRecords.P_VL500_Vyatka_Priem.Key];
			});

			P_VL110_Saldo.CalcFunction=new RecordCalcDelegate((report, date) => {
				return
					-report[date,ReportLinesRecords.P_VL110_Otd.ID] + report[date,ReportLinesRecords.P_VL110_Priem.ID];
			});

			P_VL220_Saldo.CalcFunction=new RecordCalcDelegate((report, date) => {
				return
					-report[date,ReportLinesRecords.P_VL220_Otd.ID] + report[date,ReportLinesRecords.P_VL220_Priem.ID];
			});

			P_VL500_Saldo.CalcFunction= new RecordCalcDelegate((report, date) => {
				return
					-report[date,ReportLinesRecords.P_VL500_Otd.ID] + report[date,ReportLinesRecords.P_VL500_Priem.ID];
			});

			P_KL6_Saldo.CalcFunction = new RecordCalcDelegate((report, date) => {
				return
					-report[date, ReportLinesRecords.P_KL6_Otd.ID] + report[date, ReportLinesRecords.P_KL6_Priem.ID];
			});

			P_VL110_Nebalans.CalcFunction=new RecordCalcDelegate((report, date) => {
				return
					report[date,ReportLinesRecords.P_VL110_Saldo.ID] + 
					report[date,ReportGlTransformRecords.P_56AT_110.ID]+
					report[date,ReportGlTransformRecords.P_1T_110.ID];
			});

			P_VL220_Nebalans.CalcFunction=new RecordCalcDelegate((report, date) => {
				return
					report[date,ReportLinesRecords.P_VL220_Saldo.ID] +
					report[date,ReportGlTransformRecords.P_56AT_220.ID] +
					report[date,ReportGlTransformRecords.P_4T_220.ID] +
					report[date,ReportGlTransformRecords.P_2AT_220.ID] +
					report[date,ReportGlTransformRecords.P_3AT_220.ID];
			});

			P_VL500_Nebalans.CalcFunction=new RecordCalcDelegate((report, date) => {
				return
					report[date,ReportLinesRecords.P_VL500_Saldo.ID] +
					report[date,ReportGlTransformRecords.P_2AT_500.ID] +
					report[date,ReportGlTransformRecords.P_3AT_500.ID]-
					report[date,PiramidaRecords.P_R500_Emelino_priem.Key]- report[date, PiramidaRecords.P_R500_Vyatka_priem.Key];
			});

			P_VL_Nebalans.CalcFunction=
				new RecordCalcDelegate((report, date) => {
					return
						report[date, ReportLinesRecords.P_VL110_Nebalans.ID] +
						report[date, ReportLinesRecords.P_VL220_Nebalans.ID] +
						report[date, ReportLinesRecords.P_VL500_Nebalans.ID];
				});


			P_VL110_FullP.CalcFunction = new RecordCalcDelegate((report, date) => {
				return
					report[date, ReportLinesRecords.P_VL110_Priem.ID] +
					report[date, ReportLinesRecords.P_VL110_Otd.ID] +
					report[date, PiramidaRecords.P_56AT_110_Otd.Key] +
					report[date, PiramidaRecords.P_56AT_110_Priem.Key] +
					report[date, PiramidaRecords.P_1T_110_Otd.Key] +
					report[date, PiramidaRecords.P_1T_110_Priem.Key];
			});

			P_VL220_FullP.CalcFunction = new RecordCalcDelegate((report, date) => {
				return
					report[date, ReportLinesRecords.P_VL220_Priem.ID] +
					report[date, ReportLinesRecords.P_VL220_Otd.ID] +
					report[date, PiramidaRecords.P_56AT_220_Otd.Key] +
					report[date, PiramidaRecords.P_56AT_220_Priem.Key] +
					report[date, PiramidaRecords.P_4T_220_Otd.Key] +
					report[date, PiramidaRecords.P_4T_220_Priem.Key] + 
					report[date, PiramidaRecords.P_2AT_220_Otd.Key] +
					report[date, PiramidaRecords.P_2AT_220_Priem.Key] + 
					report[date, PiramidaRecords.P_3AT_220_Otd.Key] +
					report[date, PiramidaRecords.P_3AT_220_Priem.Key];
			});

			P_VL500_FullP.CalcFunction = new RecordCalcDelegate((report, date) => {
				return
					report[date, ReportLinesRecords.P_VL500_Priem.ID] +
					report[date, ReportLinesRecords.P_VL500_Otd.ID] +
					report[date, PiramidaRecords.P_2AT_500_Otd.Key] +
					report[date, PiramidaRecords.P_2AT_500_Priem.Key]+
					report[date, PiramidaRecords.P_3AT_500_Otd.Key] +
					report[date, PiramidaRecords.P_3AT_500_Priem.Key];
			});

			P_VL_FullP.CalcFunction =
				new RecordCalcDelegate((report, date) => {
					return
						report[date, ReportLinesRecords.P_VL110_FullP.ID] +
						report[date, ReportLinesRecords.P_VL220_FullP.ID] +
						report[date, ReportLinesRecords.P_VL500_FullP.ID];
				});



			Q_VL110_Berezovka.CalcFunction = new RecordCalcDelegate((report, date) => {
				return report[date, PiramidaRecords.Q_VL110_Berezovka_Priem.Key] - report[date, PiramidaRecords.Q_VL110_Berezovka_Otd.Key];
			});

			Q_VL110_Dubovaya.CalcFunction = new RecordCalcDelegate((report, date) => {
				return report[date, PiramidaRecords.Q_VL110_Dubovaya_Priem.Key] - report[date, PiramidaRecords.Q_VL110_Dubovaya_Otd.Key];
			});

			Q_VL110_Ivanovka.CalcFunction = new RecordCalcDelegate((report, date) => {
				return report[date, PiramidaRecords.Q_VL110_Ivanovka_Priem.Key] - report[date, PiramidaRecords.Q_VL110_Ivanovka_Otd.Key];
			});

			Q_VL110_Kauchuk.CalcFunction = new RecordCalcDelegate((report, date) => {
				return report[date, PiramidaRecords.Q_VL110_Kauchuk_Priem.Key] - report[date, PiramidaRecords.Q_VL110_Kauchuk_Otd.Key];
			});

			Q_VL110_KSHT1.CalcFunction = new RecordCalcDelegate((report, date) => {
				return report[date, PiramidaRecords.Q_VL110_KSHT1_Priem.Key] - report[date, PiramidaRecords.Q_VL110_KSHT1_Otd.Key];
			});

			Q_VL110_KSHT2.CalcFunction = new RecordCalcDelegate((report, date) => {
				return report[date, PiramidaRecords.Q_VL110_KSHT2_Priem.Key] - report[date, PiramidaRecords.Q_VL110_KSHT2_Otd.Key];
			});

			Q_VL110_Svetlaya.CalcFunction = new RecordCalcDelegate((report, date) => {
				return report[date, PiramidaRecords.Q_VL110_Svetlaya_Priem.Key] - report[date, PiramidaRecords.Q_VL110_Svetlaya_Otd.Key];
			});

			Q_VL110_TEC.CalcFunction = new RecordCalcDelegate((report, date) => {
				return report[date, PiramidaRecords.Q_VL110_TEC_Priem.Key] - report[date, PiramidaRecords.Q_VL110_TEC_Otd.Key];
			});

			Q_VL110_Vodozabor1.CalcFunction = new RecordCalcDelegate((report, date) => {
				return report[date, PiramidaRecords.Q_VL110_Vodozabor1_Priem.Key] - report[date, PiramidaRecords.Q_VL110_Vodozabor1_Otd.Key];
			});

			Q_VL110_Vodozabor2.CalcFunction = new RecordCalcDelegate((report, date) => {
				return report[date, PiramidaRecords.Q_VL110_Vodozabor2_Priem.Key] - report[date, PiramidaRecords.Q_VL110_Vodozabor2_Otd.Key];
			});

			Q_VL220_Izhevsk1.CalcFunction = new RecordCalcDelegate((report, date) => {
				return report[date, PiramidaRecords.Q_VL220_Izhevsk1_Priem.Key] - report[date, PiramidaRecords.Q_VL220_Izhevsk1_Otd.Key];
			});

			Q_VL220_Izhevsk2.CalcFunction = new RecordCalcDelegate((report, date) => {
				return report[date, PiramidaRecords.Q_VL220_Izhevsk2_Priem.Key] - report[date, PiramidaRecords.Q_VL220_Izhevsk2_Otd.Key];
			});

			Q_VL220_Kauchuk1.CalcFunction = new RecordCalcDelegate((report, date) => {
				return report[date, PiramidaRecords.Q_VL220_Kauchuk1_Priem.Key] - report[date, PiramidaRecords.Q_VL220_Kauchuk1_Otd.Key];
			});

			Q_VL220_Kauchuk2.CalcFunction = new RecordCalcDelegate((report, date) => {
				return report[date, PiramidaRecords.Q_VL220_Kauchuk2_Priem.Key] - report[date, PiramidaRecords.Q_VL220_Kauchuk2_Otd.Key];
			});

			Q_VL220_Svetlaya.CalcFunction = new RecordCalcDelegate((report, date) => {
				return report[date, PiramidaRecords.Q_VL220_Svetlaya_Priem.Key] - report[date, PiramidaRecords.Q_VL220_Svetlaya_Otd.Key];
			});

			Q_VL500_Emelino.CalcFunction = new RecordCalcDelegate((report, date) => {
				return report[date, PiramidaRecords.Q_VL500_Emelino_Priem.Key] - report[date, PiramidaRecords.Q_VL500_Emelino_Otd.Key];
			});

			Q_VL500_Karmanovo.CalcFunction = new RecordCalcDelegate((report, date) => {
				return report[date, PiramidaRecords.Q_VL500_Karmanovo_Priem.Key] - report[date, PiramidaRecords.Q_VL500_Karmanovo_Otd.Key];
			});

			Q_VL500_Vyatka.CalcFunction = new RecordCalcDelegate((report, date) => {
				return report[date, PiramidaRecords.Q_VL500_Vyatka_Priem.Key] - report[date, PiramidaRecords.Q_VL500_Vyatka_Otd.Key];
			});

			Q_KL_Shluz1.CalcFunction = new RecordCalcDelegate((report, date) => {
				return report[date, PiramidaRecords.Q_KL6_Shluz1_Priem.Key] - report[date, PiramidaRecords.Q_KL6_Shluz1_Otd.Key];
			});

			Q_KL_Shluz2.CalcFunction = new RecordCalcDelegate((report, date) => {
				return report[date, PiramidaRecords.Q_KL6_Shluz2_Priem.Key] - report[date, PiramidaRecords.Q_KL6_Shluz2_Otd.Key];
			});

			Q_KL_Filtr1.CalcFunction = new RecordCalcDelegate((report, date) => {
				return report[date, PiramidaRecords.Q_KL6_Filtr1_Priem.Key] - report[date, PiramidaRecords.Q_KL6_Filtr1_Otd.Key];
			});

			Q_KL_Filtr2.CalcFunction = new RecordCalcDelegate((report, date) => {
				return report[date, PiramidaRecords.Q_KL6_Filtr2_Priem.Key] - report[date, PiramidaRecords.Q_KL6_Filtr2_Otd.Key];
			});

			Q_KL6_Otd.CalcFunction = new RecordCalcDelegate((report, date) => {
				return
					report[date, PiramidaRecords.Q_KL6_Filtr1_Otd.Key] +
					report[date, PiramidaRecords.Q_KL6_Filtr2_Otd.Key] +
					report[date, PiramidaRecords.Q_KL6_Shluz1_Otd.Key] +
					report[date, PiramidaRecords.Q_KL6_Shluz2_Otd.Key];
			});

			Q_KL6_Priem.CalcFunction = new RecordCalcDelegate((report, date) => {
				return
					report[date, PiramidaRecords.Q_KL6_Filtr1_Priem.Key] +
					report[date, PiramidaRecords.Q_KL6_Filtr2_Priem.Key] +
					report[date, PiramidaRecords.Q_KL6_Shluz1_Priem.Key] +
					report[date, PiramidaRecords.Q_KL6_Shluz2_Priem.Key];
			});

			Q_VL110_Otd.CalcFunction = new RecordCalcDelegate((report, date) => {
				return
					report[date, PiramidaRecords.Q_VL110_Berezovka_Otd.Key] +
					report[date, PiramidaRecords.Q_VL110_Dubovaya_Otd.Key] +
					report[date, PiramidaRecords.Q_VL110_Ivanovka_Otd.Key] +
					report[date, PiramidaRecords.Q_VL110_Kauchuk_Otd.Key] +
					report[date, PiramidaRecords.Q_VL110_KSHT1_Otd.Key] +
					report[date, PiramidaRecords.Q_VL110_KSHT2_Otd.Key] +
					report[date, PiramidaRecords.Q_VL110_Svetlaya_Otd.Key] +
					report[date, PiramidaRecords.Q_VL110_TEC_Otd.Key] +
					report[date, PiramidaRecords.Q_VL110_Vodozabor1_Otd.Key] +
					report[date, PiramidaRecords.Q_VL110_Vodozabor2_Otd.Key];
			});

			Q_VL110_Priem.CalcFunction = new RecordCalcDelegate((report, date) => {
				return
					report[date, PiramidaRecords.Q_VL110_Berezovka_Priem.Key] +
					report[date, PiramidaRecords.Q_VL110_Dubovaya_Priem.Key] +
					report[date, PiramidaRecords.Q_VL110_Ivanovka_Priem.Key] +
					report[date, PiramidaRecords.Q_VL110_Kauchuk_Priem.Key] +
					report[date, PiramidaRecords.Q_VL110_KSHT1_Priem.Key] +
					report[date, PiramidaRecords.Q_VL110_KSHT2_Priem.Key] +
					report[date, PiramidaRecords.Q_VL110_Svetlaya_Priem.Key] +
					report[date, PiramidaRecords.Q_VL110_TEC_Priem.Key] +
					report[date, PiramidaRecords.Q_VL110_Vodozabor1_Priem.Key] +
					report[date, PiramidaRecords.Q_VL110_Vodozabor2_Priem.Key];
			});

			Q_VL220_Otd.CalcFunction = new RecordCalcDelegate((report, date) => {
				return
					report[date, PiramidaRecords.Q_VL220_Izhevsk1_Otd.Key] +
					report[date, PiramidaRecords.Q_VL220_Izhevsk2_Otd.Key] +
					report[date, PiramidaRecords.Q_VL220_Kauchuk1_Otd.Key] +
					report[date, PiramidaRecords.Q_VL220_Kauchuk2_Otd.Key] +
					report[date, PiramidaRecords.Q_VL220_Svetlaya_Otd.Key];
			});

			Q_VL220_Priem.CalcFunction = new RecordCalcDelegate((report, date) => {
				return
					report[date, PiramidaRecords.Q_VL220_Izhevsk1_Priem.Key] +
					report[date, PiramidaRecords.Q_VL220_Izhevsk2_Priem.Key] +
					report[date, PiramidaRecords.Q_VL220_Kauchuk1_Priem.Key] +
					report[date, PiramidaRecords.Q_VL220_Kauchuk2_Priem.Key] +
					report[date, PiramidaRecords.Q_VL220_Svetlaya_Priem.Key];
			});

			Q_VL500_Otd.CalcFunction = new RecordCalcDelegate((report, date) => {
				return
					report[date, PiramidaRecords.Q_VL500_Emelino_Otd.Key] +
					report[date, PiramidaRecords.Q_VL500_Karmanovo_Otd.Key] +
					report[date, PiramidaRecords.Q_VL500_Vyatka_Otd.Key];
			});

			Q_VL500_Priem.CalcFunction = new RecordCalcDelegate((report, date) => {
				return
					report[date, PiramidaRecords.Q_VL500_Emelino_Priem.Key] +
					report[date, PiramidaRecords.Q_VL500_Karmanovo_Priem.Key] +
					report[date, PiramidaRecords.Q_VL500_Vyatka_Priem.Key];
			});

			Q_VL110_Saldo.CalcFunction = new RecordCalcDelegate((report, date) => {
				return
					-report[date, ReportLinesRecords.Q_VL110_Otd.ID] + report[date, ReportLinesRecords.Q_VL110_Priem.ID];
			});

			Q_VL220_Saldo.CalcFunction = new RecordCalcDelegate((report, date) => {
				return
					-report[date, ReportLinesRecords.Q_VL220_Otd.ID] + report[date, ReportLinesRecords.Q_VL220_Priem.ID];
			});

			Q_VL500_Saldo.CalcFunction = new RecordCalcDelegate((report, date) => {
				return
					-report[date, ReportLinesRecords.Q_VL500_Otd.ID] + report[date, ReportLinesRecords.Q_VL500_Priem.ID];
			});

			Q_KL6_Saldo.CalcFunction = new RecordCalcDelegate((report, date) => {
				return
					-report[date, ReportLinesRecords.Q_KL6_Otd.ID] + report[date, ReportLinesRecords.Q_KL6_Priem.ID];
			});

			Q_VL110_Nebalans.CalcFunction = new RecordCalcDelegate((report, date) => {
				return
					report[date, ReportLinesRecords.Q_VL110_Saldo.ID] +
					report[date, ReportGlTransformRecords.Q_56AT_110.ID] +
					report[date, ReportGlTransformRecords.Q_1T_110.ID];
			});

			Q_VL220_Nebalans.CalcFunction = new RecordCalcDelegate((report, date) => {
				return
					report[date, ReportLinesRecords.Q_VL220_Saldo.ID] +
					report[date, ReportGlTransformRecords.Q_56AT_220.ID] +
					report[date, ReportGlTransformRecords.Q_4T_220.ID] +
					report[date, ReportGlTransformRecords.Q_2AT_220.ID] +
					report[date, ReportGlTransformRecords.Q_3AT_220.ID];
			});

			Q_VL500_Nebalans.CalcFunction = new RecordCalcDelegate((report, date) => {
				return
					report[date, ReportLinesRecords.Q_VL500_Saldo.ID] +
					report[date, ReportGlTransformRecords.Q_2AT_500.ID] +
					report[date, ReportGlTransformRecords.Q_3AT_500.ID];
			});

			Q_VL_Nebalans.CalcFunction =
				new RecordCalcDelegate((report, date) => {
					return
						report[date, ReportLinesRecords.Q_VL110_Nebalans.ID] +
						report[date, ReportLinesRecords.Q_VL220_Nebalans.ID] +
						report[date, ReportLinesRecords.Q_VL500_Nebalans.ID];
				});


		}

		public static void AddCalcRecords(Report report, bool visible, bool toChart, ResultTypeEnum oper) {
			report.AddRecordType(new RecordTypeCalc(P_VL110_Nebalans, toChart, visible, oper));
			report.AddRecordType(new RecordTypeCalc(P_VL110_Otd, toChart, visible, oper));
			report.AddRecordType(new RecordTypeCalc(P_VL110_Priem, toChart, visible, oper));
			report.AddRecordType(new RecordTypeCalc(P_VL110_Saldo, toChart, visible, oper));
			report.AddRecordType(new RecordTypeCalc(P_VL220_Nebalans, toChart, visible, oper));
			report.AddRecordType(new RecordTypeCalc(P_VL220_Otd, toChart, visible, oper));
			report.AddRecordType(new RecordTypeCalc(P_VL220_Priem, toChart, visible, oper));
			report.AddRecordType(new RecordTypeCalc(P_VL220_Saldo, toChart, visible, oper));
			report.AddRecordType(new RecordTypeCalc(P_VL500_Nebalans, toChart, visible, oper));
			report.AddRecordType(new RecordTypeCalc(P_VL500_Otd, toChart, visible, oper));
			report.AddRecordType(new RecordTypeCalc(P_VL500_Priem, toChart, visible, oper));
			report.AddRecordType(new RecordTypeCalc(P_VL500_Saldo, toChart, visible, oper));

			report.AddRecordType(new RecordTypeCalc(P_VL110_Berezovka, toChart, visible, oper));
			report.AddRecordType(new RecordTypeCalc(P_VL110_Dubovaya, toChart, visible, oper));
			report.AddRecordType(new RecordTypeCalc(P_VL110_Ivanovka, toChart, visible, oper));
			report.AddRecordType(new RecordTypeCalc(P_VL110_Kauchuk, toChart, visible, oper));
			report.AddRecordType(new RecordTypeCalc(P_VL110_KSHT1, toChart, visible, oper));
			report.AddRecordType(new RecordTypeCalc(P_VL110_KSHT2, toChart, visible, oper));
			report.AddRecordType(new RecordTypeCalc(P_VL110_Svetlaya, toChart, visible, oper));
			report.AddRecordType(new RecordTypeCalc(P_VL110_TEC, toChart, visible, oper));
			report.AddRecordType(new RecordTypeCalc(P_VL110_Vodozabor1, toChart, visible, oper));
			report.AddRecordType(new RecordTypeCalc(P_VL110_Vodozabor2, toChart, visible, oper));

			report.AddRecordType(new RecordTypeCalc(P_VL220_Izhevsk1, toChart, visible, oper));
			report.AddRecordType(new RecordTypeCalc(P_VL220_Izhevsk2, toChart, visible, oper));
			report.AddRecordType(new RecordTypeCalc(P_VL220_Kauchuk1, toChart, visible, oper));
			report.AddRecordType(new RecordTypeCalc(P_VL220_Kauchuk2, toChart, visible, oper));
			report.AddRecordType(new RecordTypeCalc(P_VL220_Svetlaya, toChart, visible, oper));

			report.AddRecordType(new RecordTypeCalc(P_VL500_Emelino, toChart, visible, oper));
			report.AddRecordType(new RecordTypeCalc(P_VL500_Karmanovo, toChart, visible, oper));
			report.AddRecordType(new RecordTypeCalc(P_VL500_Vyatka, toChart, visible, oper));

			report.AddRecordType(new RecordTypeCalc(P_KL_Shluz1, toChart, visible, oper));
			report.AddRecordType(new RecordTypeCalc(P_KL_Shluz2, toChart, visible, oper));
			report.AddRecordType(new RecordTypeCalc(P_KL_Filtr1, toChart, visible, oper));
			report.AddRecordType(new RecordTypeCalc(P_KL_Filtr2, toChart, visible, oper));
			report.AddRecordType(new RecordTypeCalc(P_KL6_Otd, toChart, visible, oper));
			report.AddRecordType(new RecordTypeCalc(P_KL6_Priem, toChart, visible, oper));
			report.AddRecordType(new RecordTypeCalc(P_KL6_Saldo, toChart, visible, oper));

			report.AddRecordType(new RecordTypeCalc(P_VL_Nebalans, toChart, visible, oper));


			report.AddRecordType(new RecordTypeCalc(Q_VL110_Nebalans, toChart, visible, oper));
			report.AddRecordType(new RecordTypeCalc(Q_VL110_Otd, toChart, visible, oper));
			report.AddRecordType(new RecordTypeCalc(Q_VL110_Priem, toChart, visible, oper));
			report.AddRecordType(new RecordTypeCalc(Q_VL110_Saldo, toChart, visible, oper));
			report.AddRecordType(new RecordTypeCalc(Q_VL220_Nebalans, toChart, visible, oper));
			report.AddRecordType(new RecordTypeCalc(Q_VL220_Otd, toChart, visible, oper));
			report.AddRecordType(new RecordTypeCalc(Q_VL220_Priem, toChart, visible, oper));
			report.AddRecordType(new RecordTypeCalc(Q_VL220_Saldo, toChart, visible, oper));
			report.AddRecordType(new RecordTypeCalc(Q_VL500_Nebalans, toChart, visible, oper));
			report.AddRecordType(new RecordTypeCalc(Q_VL500_Otd, toChart, visible, oper));
			report.AddRecordType(new RecordTypeCalc(Q_VL500_Priem, toChart, visible, oper));
			report.AddRecordType(new RecordTypeCalc(Q_VL500_Saldo, toChart, visible, oper));

			report.AddRecordType(new RecordTypeCalc(Q_VL110_Berezovka, toChart, visible, oper));
			report.AddRecordType(new RecordTypeCalc(Q_VL110_Dubovaya, toChart, visible, oper));
			report.AddRecordType(new RecordTypeCalc(Q_VL110_Ivanovka, toChart, visible, oper));
			report.AddRecordType(new RecordTypeCalc(Q_VL110_Kauchuk, toChart, visible, oper));
			report.AddRecordType(new RecordTypeCalc(Q_VL110_KSHT1, toChart, visible, oper));
			report.AddRecordType(new RecordTypeCalc(Q_VL110_KSHT2, toChart, visible, oper));
			report.AddRecordType(new RecordTypeCalc(Q_VL110_Svetlaya, toChart, visible, oper));
			report.AddRecordType(new RecordTypeCalc(Q_VL110_TEC, toChart, visible, oper));
			report.AddRecordType(new RecordTypeCalc(Q_VL110_Vodozabor1, toChart, visible, oper));
			report.AddRecordType(new RecordTypeCalc(Q_VL110_Vodozabor2, toChart, visible, oper));

			report.AddRecordType(new RecordTypeCalc(Q_VL220_Izhevsk1, toChart, visible, oper));
			report.AddRecordType(new RecordTypeCalc(Q_VL220_Izhevsk2, toChart, visible, oper));
			report.AddRecordType(new RecordTypeCalc(Q_VL220_Kauchuk1, toChart, visible, oper));
			report.AddRecordType(new RecordTypeCalc(Q_VL220_Kauchuk2, toChart, visible, oper));
			report.AddRecordType(new RecordTypeCalc(Q_VL220_Svetlaya, toChart, visible, oper));

			report.AddRecordType(new RecordTypeCalc(Q_VL500_Emelino, toChart, visible, oper));
			report.AddRecordType(new RecordTypeCalc(Q_VL500_Karmanovo, toChart, visible, oper));
			report.AddRecordType(new RecordTypeCalc(Q_VL500_Vyatka, toChart, visible, oper));

			report.AddRecordType(new RecordTypeCalc(Q_KL_Shluz1, toChart, visible, oper));
			report.AddRecordType(new RecordTypeCalc(Q_KL_Shluz2, toChart, visible, oper));
			report.AddRecordType(new RecordTypeCalc(Q_KL_Filtr1, toChart, visible, oper));
			report.AddRecordType(new RecordTypeCalc(Q_KL_Filtr2, toChart, visible, oper));
			report.AddRecordType(new RecordTypeCalc(Q_KL6_Otd, toChart, visible, oper));
			report.AddRecordType(new RecordTypeCalc(Q_KL6_Priem, toChart, visible, oper));
			report.AddRecordType(new RecordTypeCalc(Q_KL6_Saldo, toChart, visible, oper));

			report.AddRecordType(new RecordTypeCalc(Q_VL_Nebalans, toChart, visible, oper));


			report.AddRecordType(new RecordTypeCalc(P_VL_FullP, toChart, visible, oper));
			report.AddRecordType(new RecordTypeCalc(P_VL220_FullP, toChart, visible, oper));
			report.AddRecordType(new RecordTypeCalc(P_VL500_FullP, toChart, visible, oper));
			report.AddRecordType(new RecordTypeCalc(P_VL110_FullP, toChart, visible, oper));
		}

		public static void AddLineRecordsP(Report report, int parNumber, double scaleMult, double scaleDiv, bool visible, bool toChart, DBOperEnum oper, ResultTypeEnum result) {
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_VL110_Berezovka_Otd, parNumber, visible: visible, toChart:toChart, divParam:scaleDiv, multParam:scaleMult, resultType:result, dbOper:oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_VL110_Berezovka_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType:result, dbOper:oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_VL110_Dubovaya_Otd, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType:result, dbOper:oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_VL110_Dubovaya_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType:result, dbOper:oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_VL110_Ivanovka_Otd, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType:result, dbOper:oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_VL110_Ivanovka_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType:result, dbOper:oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_VL110_Kauchuk_Otd, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType:result, dbOper:oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_VL110_Kauchuk_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType:result, dbOper:oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_VL110_KSHT1_Otd, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType:result, dbOper:oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_VL110_KSHT1_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType:result, dbOper:oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_VL110_KSHT2_Otd, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType:result, dbOper:oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_VL110_KSHT2_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType:result, dbOper:oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_VL110_Svetlaya_Otd, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType:result, dbOper:oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_VL110_Svetlaya_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType:result, dbOper:oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_VL110_TEC_Otd, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType:result, dbOper:oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_VL110_TEC_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType:result, dbOper:oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_VL110_Vodozabor1_Otd, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType:result, dbOper:oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_VL110_Vodozabor1_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType:result, dbOper:oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_VL110_Vodozabor2_Otd, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType:result, dbOper:oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_VL110_Vodozabor2_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType:result, dbOper:oper));

			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_VL220_Izhevsk1_Otd, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType:result, dbOper:oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_VL220_Izhevsk1_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType:result, dbOper:oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_VL220_Izhevsk2_Otd, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType:result, dbOper:oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_VL220_Izhevsk2_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType:result, dbOper:oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_VL220_Kauchuk1_Otd, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType:result, dbOper:oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_VL220_Kauchuk1_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType:result, dbOper:oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_VL220_Kauchuk2_Otd, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType:result, dbOper:oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_VL220_Kauchuk2_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType:result, dbOper:oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_VL220_Svetlaya_Otd, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType:result, dbOper:oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_VL220_Svetlaya_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType:result, dbOper:oper));

			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_VL500_Emelino_Otd, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType:result, dbOper:oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_VL500_Emelino_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType:result, dbOper:oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_VL500_Karmanovo_Otd, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType:result, dbOper:oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_VL500_Karmanovo_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType:result, dbOper:oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_VL500_Vyatka_Otd, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType:result, dbOper:oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_VL500_Vyatka_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType:result, dbOper:oper));

			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_KL6_Filtr1_Otd, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_KL6_Filtr2_Otd, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_KL6_Filtr1_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_KL6_Filtr2_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));

			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_KL6_Shluz1_Otd, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_KL6_Shluz2_Otd, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_KL6_Shluz1_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.P_KL6_Shluz2_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
		}

		public static void AddLineRecordsQ(Report report, int parNumber, double scaleMult, double scaleDiv, bool visible, bool toChart, DBOperEnum oper, ResultTypeEnum result) {
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.Q_VL110_Berezovka_Otd, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.Q_VL110_Berezovka_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.Q_VL110_Dubovaya_Otd, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.Q_VL110_Dubovaya_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.Q_VL110_Ivanovka_Otd, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.Q_VL110_Ivanovka_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.Q_VL110_Kauchuk_Otd, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.Q_VL110_Kauchuk_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.Q_VL110_KSHT1_Otd, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.Q_VL110_KSHT1_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.Q_VL110_KSHT2_Otd, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.Q_VL110_KSHT2_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.Q_VL110_Svetlaya_Otd, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.Q_VL110_Svetlaya_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.Q_VL110_TEC_Otd, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.Q_VL110_TEC_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.Q_VL110_Vodozabor1_Otd, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.Q_VL110_Vodozabor1_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.Q_VL110_Vodozabor2_Otd, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.Q_VL110_Vodozabor2_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));

			report.AddRecordType(new RecordTypeDB(PiramidaRecords.Q_VL220_Izhevsk1_Otd, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.Q_VL220_Izhevsk1_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.Q_VL220_Izhevsk2_Otd, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.Q_VL220_Izhevsk2_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.Q_VL220_Kauchuk1_Otd, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.Q_VL220_Kauchuk1_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.Q_VL220_Kauchuk2_Otd, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.Q_VL220_Kauchuk2_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.Q_VL220_Svetlaya_Otd, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.Q_VL220_Svetlaya_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));

			report.AddRecordType(new RecordTypeDB(PiramidaRecords.Q_VL500_Emelino_Otd, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.Q_VL500_Emelino_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.Q_VL500_Karmanovo_Otd, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.Q_VL500_Karmanovo_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.Q_VL500_Vyatka_Otd, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.Q_VL500_Vyatka_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));

			report.AddRecordType(new RecordTypeDB(PiramidaRecords.Q_KL6_Filtr1_Otd, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.Q_KL6_Filtr2_Otd, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.Q_KL6_Filtr1_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.Q_KL6_Filtr2_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));

			report.AddRecordType(new RecordTypeDB(PiramidaRecords.Q_KL6_Shluz1_Otd, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.Q_KL6_Shluz2_Otd, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.Q_KL6_Shluz1_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
			report.AddRecordType(new RecordTypeDB(PiramidaRecords.Q_KL6_Shluz2_Priem, parNumber, visible: visible, toChart: toChart, divParam: scaleDiv, multParam: scaleMult, resultType: result, dbOper: oper));
		}	


	}
}
