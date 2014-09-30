using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace VotGES.Piramida.Report {
	public class FullReportRoot {
		public FullReportRecord RootMain { get; set; }
		public FullReportRecord RootLines { get; set; }
		public FullReportRecord RootSN { get; set; }
	}

	public class FullReportRecord : INotifyPropertyChanged {
		public event PropertyChangedEventHandler PropertyChanged;
		public void NotifyChanged(string propName) {
			if (PropertyChanged != null)
				PropertyChanged(this, new PropertyChangedEventArgs(propName));
		}
		public List<FullReportRecord> Children { get; set; }

		public string Title { get; set; }
		public string Key { get; set; }
		public bool IsGroup { get; set; }
		public bool Selectable { get; set; }
		protected bool selected;
		public bool Selected { get { return selected; } set { selected = value; NotifyChanged("Selected"); } }
		public bool Axis1 { get; set; }
		public bool Axis2 { get; set; }
		public bool Axis3 { get; set; }
		public bool Axis4 { get; set; }
		public bool Axis5 { get; set; }

		public FullReportRecord() { }

		public FullReportRecord addChild(FullReportRecord child) {
			Children.Add(child);
			IsGroup = true;
			return child;
		}
	}


	public class FullReportInitData {
		public FullReportRecord RootMain { get; set; }
		public FullReportRecord RootLines { get; set; }
		public FullReportRecord RootSN { get; set; }
		public FullReportInitData() {
			CreateMain();
			CreateLines();
			CreateSN();
		}


		protected void CreateMain() {
			FullReportRecord record, childRecord;
			RootMain = GetFullReportRecord("Воткинская ГЭС", "votGES");
			record = RootMain.addChild(GetFullReportRecord("Основные параметры", "mainParams"));
			record.addChild(GetFullReportRecord(PiramidaRecords.P_GES));
			record.addChild(GetFullReportRecord(PiramidaRecords.P_GTP1));
			record.addChild(GetFullReportRecord(PiramidaRecords.P_GTP2));
			record.addChild(GetFullReportRecord(PiramidaRecords.P_IKM_SN));
			record.addChild(GetFullReportRecord(PiramidaRecords.P_IKM_Nebalans_GES));
			record.addChild(GetFullReportRecord(PiramidaRecords.P_IKM_SP));

			record.addChild(GetFullReportRecord(PiramidaRecords.P_IKM_SK));
			record.addChild(GetFullReportRecord(PiramidaRecords.P_IKM_Saldo500Emelino));
			record.addChild(GetFullReportRecord(PiramidaRecords.P_IKM_Saldo500Karmanovo));
			record.addChild(GetFullReportRecord(PiramidaRecords.P_IKM_Saldo500Vyatka));
			record.addChild(GetFullReportRecord(PiramidaRecords.P_IKM_Nebalans_T));
			record.addChild(GetFullReportRecord(PiramidaRecords.P_IKM_Nebalans500));
			record.addChild(GetFullReportRecord(PiramidaRecords.P_IKM_Nebalans220));
			record.addChild(GetFullReportRecord(PiramidaRecords.P_IKM_Nebalans110));
			record.addChild(GetFullReportRecord(PiramidaRecords.P_IKM_Nebalans1T));
			record.addChild(GetFullReportRecord(PiramidaRecords.P_IKM_Nebalans2AT));
			record.addChild(GetFullReportRecord(PiramidaRecords.P_IKM_Nebalans3AT));
			record.addChild(GetFullReportRecord(PiramidaRecords.P_IKM_Nebalans4T));
			record.addChild(GetFullReportRecord(PiramidaRecords.P_IKM_Nebalans56AT));
			record.addChild(GetFullReportRecord(PiramidaRecords.P_IKM_NebalansT));
			record.addChild(GetFullReportRecord(PiramidaRecords.P_IKM_NebalansKRU1));
			record.addChild(GetFullReportRecord(PiramidaRecords.P_IKM_NebalansKRU2));
			record.addChild(GetFullReportRecord(PiramidaRecords.P_IKM_NebalansKRU3));

			record = RootMain.addChild(GetFullReportRecord("Вода", "water"));
			record.addChild(GetFullReportRecord(PiramidaRecords.Water_Napor));
			record.addChild(GetFullReportRecord(PiramidaRecords.Water_NB));
			record.addChild(GetFullReportRecord(PiramidaRecords.Water_QGES));
			record.addChild(GetFullReportRecord(ReportWaterRecords.Water_QGTP1));
			record.addChild(GetFullReportRecord(ReportWaterRecords.Water_QGTP2));
			record.addChild(GetFullReportRecord(PiramidaRecords.Water_Temp));
			record.addChild(GetFullReportRecord(PiramidaRecords.Water_VB));
			record.addChild(GetFullReportRecord(PiramidaRecords.Water_QOptGES));
			record.addChild(GetFullReportRecord(PiramidaRecords.Water_QOptGTP1));
			record.addChild(GetFullReportRecord(PiramidaRecords.Water_QOptGTP2));
			record.addChild(GetFullReportRecord(ReportWaterRecords.Water_OVER_GES));
			record.addChild(GetFullReportRecord(ReportWaterRecords.Water_OVER_GTP1));
			record.addChild(GetFullReportRecord(ReportWaterRecords.Water_OVER_GTP2));

			childRecord = record.addChild(GetFullReportRecord("Генераторы", "qga"));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.Water_Q_GA1));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.Water_Q_GA2));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.Water_Q_GA3));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.Water_Q_GA4));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.Water_Q_GA5));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.Water_Q_GA6));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.Water_Q_GA7));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.Water_Q_GA8));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.Water_Q_GA9));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.Water_Q_GA10));

			record = RootMain.addChild(GetFullReportRecord("Суточная ведомость", "gsv"));
			record.addChild(GetFullReportRecord(PiramidaRecords.GSV2));
			record.addChild(GetFullReportRecord(PiramidaRecords.GSV3));
			record.addChild(GetFullReportRecord(PiramidaRecords.GSV4));
			record.addChild(GetFullReportRecord(PiramidaRecords.GSV5));
			record.addChild(GetFullReportRecord(PiramidaRecords.GSV6));
			record.addChild(GetFullReportRecord(PiramidaRecords.GSV7));
			record.addChild(GetFullReportRecord(PiramidaRecords.GSV8));
			record.addChild(GetFullReportRecord(PiramidaRecords.GSV9));
			record.addChild(GetFullReportRecord(PiramidaRecords.GSV10));
			record.addChild(GetFullReportRecord(PiramidaRecords.GSV11));
			record.addChild(GetFullReportRecord(PiramidaRecords.GSV12));
			record.addChild(GetFullReportRecord(PiramidaRecords.GSV13));
			record.addChild(GetFullReportRecord(PiramidaRecords.GSV14));
			record.addChild(GetFullReportRecord(PiramidaRecords.GSV15));
			record.addChild(GetFullReportRecord(PiramidaRecords.GSV16));
			record.addChild(GetFullReportRecord(PiramidaRecords.GSV17));
			record.addChild(GetFullReportRecord(PiramidaRecords.GSV18));
			record.addChild(GetFullReportRecord(PiramidaRecords.GSV19));
			record.addChild(GetFullReportRecord(PiramidaRecords.GSV20));
			record.addChild(GetFullReportRecord(PiramidaRecords.GSV21));
			record.addChild(GetFullReportRecord(PiramidaRecords.GSV22));
			record.addChild(GetFullReportRecord(PiramidaRecords.GSV23));
			record.addChild(GetFullReportRecord(PiramidaRecords.GSV24));
			record.addChild(GetFullReportRecord(PiramidaRecords.GSV25));
			record.addChild(GetFullReportRecord(PiramidaRecords.GSV26));
			record.addChild(GetFullReportRecord(PiramidaRecords.GSV27));
			record.addChild(GetFullReportRecord(PiramidaRecords.GSV28));

			record = RootMain.addChild(GetFullReportRecord("Генераторы", "generators"));
			childRecord = record.addChild(GetFullReportRecord(ReportGARecords.P_GA1));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_GA1_Priem));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_GA1_Otd));
			childRecord = record.addChild(GetFullReportRecord(ReportGARecords.Q_GA1));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.Q_GA1_Priem));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.Q_GA1_Otd));

			childRecord = record.addChild(GetFullReportRecord(ReportGARecords.P_GA2));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_GA2_Priem));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_GA2_Otd));
			childRecord = record.addChild(GetFullReportRecord(ReportGARecords.Q_GA2));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.Q_GA2_Priem));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.Q_GA2_Otd));

			childRecord = record.addChild(GetFullReportRecord(ReportGARecords.P_GA3));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_GA3_Priem));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_GA3_Otd));
			childRecord = record.addChild(GetFullReportRecord(ReportGARecords.Q_GA3));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.Q_GA3_Priem));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.Q_GA3_Otd));

			childRecord = record.addChild(GetFullReportRecord(ReportGARecords.P_GA4));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_GA4_Priem));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_GA4_Otd));
			childRecord = record.addChild(GetFullReportRecord(ReportGARecords.Q_GA4));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.Q_GA4_Priem));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.Q_GA4_Otd));

			childRecord = record.addChild(GetFullReportRecord(ReportGARecords.P_GA5));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_GA5_Priem));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_GA5_Otd));
			childRecord = record.addChild(GetFullReportRecord(ReportGARecords.Q_GA5));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.Q_GA5_Priem));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.Q_GA5_Otd));

			childRecord = record.addChild(GetFullReportRecord(ReportGARecords.P_GA6));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_GA1_Priem));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_GA1_Otd));
			childRecord = record.addChild(GetFullReportRecord(ReportGARecords.Q_GA6));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.Q_GA6_Priem));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.Q_GA6_Otd));

			childRecord = record.addChild(GetFullReportRecord(ReportGARecords.P_GA7));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_GA7_Priem));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_GA7_Otd));
			childRecord = record.addChild(GetFullReportRecord(ReportGARecords.Q_GA7));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.Q_GA7_Priem));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.Q_GA7_Otd));

			childRecord = record.addChild(GetFullReportRecord(ReportGARecords.P_GA8));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_GA8_Priem));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_GA8_Otd));
			childRecord = record.addChild(GetFullReportRecord(ReportGARecords.Q_GA8));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.Q_GA8_Priem));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.Q_GA8_Otd));

			childRecord = record.addChild(GetFullReportRecord(ReportGARecords.P_GA9));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_GA9_Priem));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_GA9_Otd));
			childRecord = record.addChild(GetFullReportRecord(ReportGARecords.Q_GA9));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.Q_GA9_Priem));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.Q_GA9_Otd));

			childRecord = record.addChild(GetFullReportRecord(ReportGARecords.P_GA10));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_GA10_Priem));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_GA10_Otd));
			childRecord = record.addChild(GetFullReportRecord(ReportGARecords.Q_GA10));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.Q_GA10_Priem));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.Q_GA10_Otd));
		}

		protected void CreateLines() {
			FullReportRecord record, childRecord, child2, rec0;
			RootLines = GetFullReportRecord("ВЛ", "VL");

			rec0 = RootLines.addChild(GetFullReportRecord("P ВЛ", "pvl"));

			record = rec0.addChild(GetFullReportRecord(ReportLinesRecords.P_VL110_Saldo));
			childRecord = record.addChild(GetFullReportRecord(ReportLinesRecords.P_VL110_Priem));
			childRecord = record.addChild(GetFullReportRecord(ReportLinesRecords.P_VL110_Otd));

			childRecord = record.addChild(GetFullReportRecord(ReportLinesRecords.P_VL110_Berezovka));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_VL110_Berezovka_Otd));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_VL110_Berezovka_Priem));

			childRecord = record.addChild(GetFullReportRecord(ReportLinesRecords.P_VL110_Dubovaya));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_VL110_Dubovaya_Otd));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_VL110_Dubovaya_Priem));

			childRecord = record.addChild(GetFullReportRecord(ReportLinesRecords.P_VL110_Ivanovka));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_VL110_Ivanovka_Otd));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_VL110_Ivanovka_Priem));

			childRecord = record.addChild(GetFullReportRecord(ReportLinesRecords.P_VL110_Kauchuk));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_VL110_Kauchuk_Otd));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_VL110_Kauchuk_Priem));

			childRecord = record.addChild(GetFullReportRecord(ReportLinesRecords.P_VL110_KSHT1));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_VL110_KSHT1_Otd));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_VL110_KSHT1_Priem));

			childRecord = record.addChild(GetFullReportRecord(ReportLinesRecords.P_VL110_KSHT2));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_VL110_KSHT2_Otd));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_VL110_KSHT2_Priem));

			childRecord = record.addChild(GetFullReportRecord(ReportLinesRecords.P_VL110_Svetlaya));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_VL110_Svetlaya_Otd));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_VL110_Svetlaya_Priem));

			childRecord = record.addChild(GetFullReportRecord(ReportLinesRecords.P_VL110_TEC));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_VL110_TEC_Otd));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_VL110_TEC_Priem));

			childRecord = record.addChild(GetFullReportRecord(ReportLinesRecords.P_VL110_Vodozabor1));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_VL110_Vodozabor1_Otd));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_VL110_Vodozabor1_Priem));

			childRecord = record.addChild(GetFullReportRecord(ReportLinesRecords.P_VL110_Vodozabor2));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_VL110_Vodozabor2_Otd));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_VL110_Vodozabor2_Priem));

			record = rec0.addChild(GetFullReportRecord(ReportLinesRecords.P_VL220_Saldo));
			childRecord = record.addChild(GetFullReportRecord(ReportLinesRecords.P_VL220_Priem));
			childRecord = record.addChild(GetFullReportRecord(ReportLinesRecords.P_VL220_Otd));

			childRecord = record.addChild(GetFullReportRecord(ReportLinesRecords.P_VL220_Izhevsk1));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_VL220_Izhevsk1_Otd));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_VL220_Izhevsk1_Priem));

			childRecord = record.addChild(GetFullReportRecord(ReportLinesRecords.P_VL220_Izhevsk2));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_VL220_Izhevsk2_Otd));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_VL220_Izhevsk2_Priem));

			childRecord = record.addChild(GetFullReportRecord(ReportLinesRecords.P_VL220_Kauchuk1));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_VL220_Kauchuk1_Otd));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_VL220_Kauchuk1_Priem));

			childRecord = record.addChild(GetFullReportRecord(ReportLinesRecords.P_VL220_Kauchuk2));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_VL220_Kauchuk2_Otd));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_VL220_Kauchuk2_Priem));

			childRecord = record.addChild(GetFullReportRecord(ReportLinesRecords.P_VL220_Svetlaya));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_VL220_Svetlaya_Otd));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_VL220_Svetlaya_Priem));

			record = rec0.addChild(GetFullReportRecord(ReportLinesRecords.P_VL500_Saldo));
			childRecord = record.addChild(GetFullReportRecord(ReportLinesRecords.P_VL500_Priem));
			childRecord = record.addChild(GetFullReportRecord(ReportLinesRecords.P_VL500_Otd));

			childRecord = record.addChild(GetFullReportRecord(ReportLinesRecords.P_VL500_Karmanovo));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_VL500_Karmanovo_Otd));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_VL500_Karmanovo_Priem));

			childRecord = record.addChild(GetFullReportRecord(ReportLinesRecords.P_VL500_Emelino));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_VL500_Emelino_Otd));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_VL500_Emelino_Priem));

			childRecord = record.addChild(GetFullReportRecord(ReportLinesRecords.P_VL500_Vyatka));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_VL500_Vyatka_Otd));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_VL500_Vyatka_Priem));

			record = rec0.addChild(GetFullReportRecord(ReportLinesRecords.P_KL6_Saldo));
			childRecord = record.addChild(GetFullReportRecord(ReportLinesRecords.P_KL6_Otd));
			childRecord = record.addChild(GetFullReportRecord(ReportLinesRecords.P_KL6_Priem));

			childRecord = record.addChild(GetFullReportRecord(ReportLinesRecords.P_KL_Shluz1));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_KL6_Shluz1_Otd));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_KL6_Shluz1_Priem));

			childRecord = record.addChild(GetFullReportRecord(ReportLinesRecords.P_KL_Shluz2));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_KL6_Shluz2_Otd));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_KL6_Shluz2_Priem));

			childRecord = record.addChild(GetFullReportRecord(ReportLinesRecords.P_KL_Filtr1));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_KL6_Filtr1_Otd));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_KL6_Filtr1_Priem));

			childRecord = record.addChild(GetFullReportRecord(ReportLinesRecords.P_KL_Filtr2));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_KL6_Filtr2_Otd));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_KL6_Filtr2_Priem));

			rec0 = RootLines.addChild(GetFullReportRecord("Q ВЛ", "qvl"));

			record = rec0.addChild(GetFullReportRecord(ReportLinesRecords.Q_VL110_Saldo));
			childRecord = record.addChild(GetFullReportRecord(ReportLinesRecords.Q_VL110_Priem));
			childRecord = record.addChild(GetFullReportRecord(ReportLinesRecords.Q_VL110_Otd));

			childRecord = record.addChild(GetFullReportRecord(ReportLinesRecords.Q_VL110_Berezovka));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.Q_VL110_Berezovka_Otd));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.Q_VL110_Berezovka_Priem));

			childRecord = record.addChild(GetFullReportRecord(ReportLinesRecords.Q_VL110_Dubovaya));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.Q_VL110_Dubovaya_Otd));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.Q_VL110_Dubovaya_Priem));

			childRecord = record.addChild(GetFullReportRecord(ReportLinesRecords.Q_VL110_Ivanovka));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.Q_VL110_Ivanovka_Otd));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.Q_VL110_Ivanovka_Priem));

			childRecord = record.addChild(GetFullReportRecord(ReportLinesRecords.Q_VL110_Kauchuk));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.Q_VL110_Kauchuk_Otd));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.Q_VL110_Kauchuk_Priem));

			childRecord = record.addChild(GetFullReportRecord(ReportLinesRecords.Q_VL110_KSHT1));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.Q_VL110_KSHT1_Otd));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.Q_VL110_KSHT1_Priem));

			childRecord = record.addChild(GetFullReportRecord(ReportLinesRecords.Q_VL110_KSHT2));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.Q_VL110_KSHT2_Otd));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.Q_VL110_KSHT2_Priem));

			childRecord = record.addChild(GetFullReportRecord(ReportLinesRecords.Q_VL110_Svetlaya));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.Q_VL110_Svetlaya_Otd));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.Q_VL110_Svetlaya_Priem));

			childRecord = record.addChild(GetFullReportRecord(ReportLinesRecords.Q_VL110_TEC));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.Q_VL110_TEC_Otd));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.Q_VL110_TEC_Priem));

			childRecord = record.addChild(GetFullReportRecord(ReportLinesRecords.Q_VL110_Vodozabor1));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.Q_VL110_Vodozabor1_Otd));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.Q_VL110_Vodozabor1_Priem));

			childRecord = record.addChild(GetFullReportRecord(ReportLinesRecords.Q_VL110_Vodozabor2));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.Q_VL110_Vodozabor2_Otd));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.Q_VL110_Vodozabor2_Priem));

			record = rec0.addChild(GetFullReportRecord(ReportLinesRecords.Q_VL220_Saldo));
			childRecord = record.addChild(GetFullReportRecord(ReportLinesRecords.Q_VL220_Priem));
			childRecord = record.addChild(GetFullReportRecord(ReportLinesRecords.Q_VL220_Otd));

			childRecord = record.addChild(GetFullReportRecord(ReportLinesRecords.Q_VL220_Izhevsk1));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.Q_VL220_Izhevsk1_Otd));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.Q_VL220_Izhevsk1_Priem));

			childRecord = record.addChild(GetFullReportRecord(ReportLinesRecords.Q_VL220_Izhevsk2));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.Q_VL220_Izhevsk2_Otd));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.Q_VL220_Izhevsk2_Priem));

			childRecord = record.addChild(GetFullReportRecord(ReportLinesRecords.Q_VL220_Kauchuk1));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.Q_VL220_Kauchuk1_Otd));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.Q_VL220_Kauchuk1_Priem));

			childRecord = record.addChild(GetFullReportRecord(ReportLinesRecords.Q_VL220_Kauchuk2));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.Q_VL220_Kauchuk2_Otd));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.Q_VL220_Kauchuk2_Priem));

			childRecord = record.addChild(GetFullReportRecord(ReportLinesRecords.Q_VL220_Svetlaya));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.Q_VL220_Svetlaya_Otd));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.Q_VL220_Svetlaya_Priem));

			record = rec0.addChild(GetFullReportRecord(ReportLinesRecords.Q_VL500_Saldo));
			childRecord = record.addChild(GetFullReportRecord(ReportLinesRecords.Q_VL500_Priem));
			childRecord = record.addChild(GetFullReportRecord(ReportLinesRecords.Q_VL500_Otd));

			childRecord = record.addChild(GetFullReportRecord(ReportLinesRecords.Q_VL500_Karmanovo));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.Q_VL500_Karmanovo_Otd));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.Q_VL500_Karmanovo_Priem));

			childRecord = record.addChild(GetFullReportRecord(ReportLinesRecords.Q_VL500_Emelino));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.Q_VL500_Emelino_Otd));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.Q_VL500_Emelino_Priem));

			childRecord = record.addChild(GetFullReportRecord(ReportLinesRecords.Q_VL500_Vyatka));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.Q_VL500_Vyatka_Otd));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.Q_VL500_Vyatka_Priem));

			record = rec0.addChild(GetFullReportRecord(ReportLinesRecords.Q_KL6_Saldo));
			childRecord = record.addChild(GetFullReportRecord(ReportLinesRecords.Q_KL6_Otd));
			childRecord = record.addChild(GetFullReportRecord(ReportLinesRecords.Q_KL6_Priem));

			childRecord = record.addChild(GetFullReportRecord(ReportLinesRecords.Q_KL_Shluz1));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.Q_KL6_Shluz1_Otd));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.Q_KL6_Shluz1_Priem));

			childRecord = record.addChild(GetFullReportRecord(ReportLinesRecords.Q_KL_Shluz2));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.Q_KL6_Shluz2_Otd));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.Q_KL6_Shluz2_Priem));

			childRecord = record.addChild(GetFullReportRecord(ReportLinesRecords.Q_KL_Filtr1));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.Q_KL6_Filtr1_Otd));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.Q_KL6_Filtr1_Priem));

			childRecord = record.addChild(GetFullReportRecord(ReportLinesRecords.Q_KL_Filtr2));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.Q_KL6_Filtr2_Otd));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.Q_KL6_Filtr2_Priem));


			record = RootLines.addChild(GetFullReportRecord("Главные трансформаторы", "mainTrans"));
			childRecord = record.addChild(GetFullReportRecord("1Т", "1T"));
			child2 = childRecord.addChild(GetFullReportRecord(ReportGlTransformRecords.P_1T_110));
			child2.addChild(GetFullReportRecord(PiramidaRecords.P_1T_110_Otd));
			child2.addChild(GetFullReportRecord(PiramidaRecords.P_1T_110_Priem));
			child2 = childRecord.addChild(GetFullReportRecord(ReportGlTransformRecords.Q_1T_110));
			child2.addChild(GetFullReportRecord(PiramidaRecords.Q_1T_110_Otd));
			child2.addChild(GetFullReportRecord(PiramidaRecords.Q_1T_110_Priem));

			childRecord = record.addChild(GetFullReportRecord("2АТ", "2AT"));
			child2 = childRecord.addChild(GetFullReportRecord(ReportGlTransformRecords.P_2AT_220));
			child2.addChild(GetFullReportRecord(PiramidaRecords.P_2AT_220_Otd));
			child2.addChild(GetFullReportRecord(PiramidaRecords.P_2AT_220_Priem));
			child2 = childRecord.addChild(GetFullReportRecord(ReportGlTransformRecords.P_2AT_500));
			child2.addChild(GetFullReportRecord(PiramidaRecords.P_2AT_500_Otd));
			child2.addChild(GetFullReportRecord(PiramidaRecords.P_2AT_500_Priem));
			child2 = childRecord.addChild(GetFullReportRecord(ReportGlTransformRecords.Q_2AT_220));
			child2.addChild(GetFullReportRecord(PiramidaRecords.Q_2AT_220_Otd));
			child2.addChild(GetFullReportRecord(PiramidaRecords.Q_2AT_220_Priem));
			child2 = childRecord.addChild(GetFullReportRecord(ReportGlTransformRecords.Q_2AT_500));
			child2.addChild(GetFullReportRecord(PiramidaRecords.Q_2AT_500_Otd));
			child2.addChild(GetFullReportRecord(PiramidaRecords.Q_2AT_500_Priem));

			childRecord = record.addChild(GetFullReportRecord("3АТ", "3AT"));
			child2 = childRecord.addChild(GetFullReportRecord(ReportGlTransformRecords.P_3AT_220));
			child2.addChild(GetFullReportRecord(PiramidaRecords.P_3AT_220_Otd));
			child2.addChild(GetFullReportRecord(PiramidaRecords.P_3AT_220_Priem));
			child2 = childRecord.addChild(GetFullReportRecord(ReportGlTransformRecords.P_3AT_500));
			child2.addChild(GetFullReportRecord(PiramidaRecords.P_3AT_500_Otd));
			child2.addChild(GetFullReportRecord(PiramidaRecords.P_3AT_500_Priem));
			child2 = childRecord.addChild(GetFullReportRecord(ReportGlTransformRecords.Q_3AT_220));
			child2.addChild(GetFullReportRecord(PiramidaRecords.Q_3AT_220_Otd));
			child2.addChild(GetFullReportRecord(PiramidaRecords.Q_3AT_220_Priem));
			child2 = childRecord.addChild(GetFullReportRecord(ReportGlTransformRecords.Q_3AT_500));
			child2.addChild(GetFullReportRecord(PiramidaRecords.Q_3AT_500_Otd));
			child2.addChild(GetFullReportRecord(PiramidaRecords.Q_3AT_500_Priem));

			childRecord = record.addChild(GetFullReportRecord("4Т", "4T"));
			child2 = childRecord.addChild(GetFullReportRecord(ReportGlTransformRecords.P_4T_220));
			child2.addChild(GetFullReportRecord(PiramidaRecords.P_4T_220_Otd));
			child2.addChild(GetFullReportRecord(PiramidaRecords.P_4T_220_Priem));
			child2 = childRecord.addChild(GetFullReportRecord(ReportGlTransformRecords.Q_4T_220));
			child2.addChild(GetFullReportRecord(PiramidaRecords.Q_4T_220_Otd));
			child2.addChild(GetFullReportRecord(PiramidaRecords.Q_4T_220_Priem));

			childRecord = record.addChild(GetFullReportRecord("5-6АТ", "56AT"));
			child2 = childRecord.addChild(GetFullReportRecord(ReportGlTransformRecords.P_56AT_220));
			child2.addChild(GetFullReportRecord(PiramidaRecords.P_56AT_220_Otd));
			child2.addChild(GetFullReportRecord(PiramidaRecords.P_56AT_220_Priem));
			child2 = childRecord.addChild(GetFullReportRecord(ReportGlTransformRecords.P_56AT_110));
			child2.addChild(GetFullReportRecord(PiramidaRecords.P_56AT_110_Otd));
			child2.addChild(GetFullReportRecord(PiramidaRecords.P_56AT_110_Priem));
			child2 = childRecord.addChild(GetFullReportRecord(ReportGlTransformRecords.Q_56AT_220));
			child2.addChild(GetFullReportRecord(PiramidaRecords.Q_56AT_220_Otd));
			child2.addChild(GetFullReportRecord(PiramidaRecords.Q_56AT_220_Priem));
			child2 = childRecord.addChild(GetFullReportRecord(ReportGlTransformRecords.Q_56AT_110));
			child2.addChild(GetFullReportRecord(PiramidaRecords.Q_56AT_110_Otd));
			child2.addChild(GetFullReportRecord(PiramidaRecords.Q_56AT_110_Priem));
		}

		protected void CreateSN() {
			FullReportRecord record, childRecord, child2;
			RootSN = GetFullReportRecord("СН", "SN");


			record = RootSN.addChild(GetFullReportRecord(ReportMainRecords.P_SP));
			childRecord = record.addChild(GetFullReportRecord(ReportGARecords.P_Vozb));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_Vozb_GA1_Priem));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_Vozb_GA2_Priem));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_Vozb_GA3_Priem));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_Vozb_GA4_Priem));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_Vozb_GA5_Priem));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_Vozb_GA6_Priem));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_Vozb_GA7_Priem));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_Vozb_GA8_Priem));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_Vozb_GA9_Priem));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_Vozb_GA10_Priem));

			childRecord = record.addChild(GetFullReportRecord(ReportGARecords.P_SN_GA));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_SN_11T_Priem));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_SN_12T_Priem));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_SN_13T_Priem));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_SN_14T_Priem));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_SN_15T_Priem));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_SN_16T_Priem));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_SN_17T_Priem));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_SN_18T_Priem));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_SN_19T_Priem));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_SN_20T_Priem));
			record.addChild(GetFullReportRecord(ReportGARecords.P_SK));
			childRecord = record.addChild(GetFullReportRecord(ReportMainRecords.P_Nebalans));

			child2 = childRecord.addChild(GetFullReportRecord(ReportGlTransformRecords.P_T_Nebalans));
			child2.addChild(GetFullReportRecord(ReportGlTransformRecords.P_1T_Nebalans));
			child2.addChild(GetFullReportRecord(ReportGlTransformRecords.P_2AT_Nebalans));
			child2.addChild(GetFullReportRecord(ReportGlTransformRecords.P_3AT_Nebalans));
			child2.addChild(GetFullReportRecord(ReportGlTransformRecords.P_4T_Nebalans));
			child2.addChild(GetFullReportRecord(ReportGlTransformRecords.P_56AT_Nebalans));

			child2 = childRecord.addChild(GetFullReportRecord(ReportLinesRecords.P_VL_Nebalans));
			child2.addChild(GetFullReportRecord(ReportLinesRecords.P_VL110_Nebalans));
			child2.addChild(GetFullReportRecord(ReportLinesRecords.P_VL220_Nebalans));
			child2.addChild(GetFullReportRecord(ReportLinesRecords.P_VL500_Nebalans));

			childRecord = record.addChild(GetFullReportRecord(ReportSNRecords.P_SN));

			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_SN_7T_Priem));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_SN_8T_Priem));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_SN_9T_Priem));

			child2 = childRecord.addChild(GetFullReportRecord(ReportSNRecords.P_SN_1N));
			child2.addChild(GetFullReportRecord(PiramidaRecords.P_KRU1_21T));
			child2.addChild(GetFullReportRecord(PiramidaRecords.P_KRU3_22T));

			child2 = childRecord.addChild(GetFullReportRecord(ReportSNRecords.P_SN_2N));
			child2.addChild(GetFullReportRecord(PiramidaRecords.P_KRU3_23T));
			child2.addChild(GetFullReportRecord(PiramidaRecords.P_KRU2_24T));

			child2 = childRecord.addChild(GetFullReportRecord(ReportSNRecords.P_SN_3N));
			child2.addChild(GetFullReportRecord(PiramidaRecords.P_KRU1_27T));
			child2.addChild(GetFullReportRecord(PiramidaRecords.P_KRU2_28T));

			child2 = childRecord.addChild(GetFullReportRecord(ReportSNRecords.P_SN_5N));
			child2.addChild(GetFullReportRecord(PiramidaRecords.P_KRU1_25T));
			child2.addChild(GetFullReportRecord(PiramidaRecords.P_KRU3_26T));

			child2 = childRecord.addChild(GetFullReportRecord(ReportSNRecords.P_SN_7N));
			child2.addChild(GetFullReportRecord(PiramidaRecords.P_KRU1_31T));
			child2.addChild(GetFullReportRecord(PiramidaRecords.P_KRU3_32T));

			child2 = childRecord.addChild(GetFullReportRecord(ReportSNRecords.P_SN_8N));
			child2.addChild(GetFullReportRecord(PiramidaRecords.P_KRU1_37T));
			child2.addChild(GetFullReportRecord(PiramidaRecords.P_KRU2_38T));

			child2 = childRecord.addChild(GetFullReportRecord(ReportSNRecords.P_SN_9N));
			child2.addChild(GetFullReportRecord(PiramidaRecords.P_KRU3_29T));
			child2.addChild(GetFullReportRecord(PiramidaRecords.P_KRU2_30T));

			child2 = childRecord.addChild(GetFullReportRecord(ReportSNRecords.P_SN_10N));
			child2.addChild(GetFullReportRecord(PiramidaRecords.P_KRU1_33T));
			child2.addChild(GetFullReportRecord(PiramidaRecords.P_KRU3_34T));

			child2 = childRecord.addChild(GetFullReportRecord(ReportSNRecords.P_SN_36N));
			child2.addChild(GetFullReportRecord(PiramidaRecords.P_KRU1_35T));
			child2.addChild(GetFullReportRecord(PiramidaRecords.P_KRU2_36T));

			child2 = childRecord.addChild(GetFullReportRecord(ReportSNRecords.P_SN_Nasos));
			child2.addChild(GetFullReportRecord(PiramidaRecords.P_1VS_N1));
			child2.addChild(GetFullReportRecord(PiramidaRecords.P_2VS_N1));

			child2 = childRecord.addChild(GetFullReportRecord("Р Емелино", "rEmelino"));
			child2.addChild(GetFullReportRecord(PiramidaRecords.P_R500_Emelino_priem));
			child2.addChild(GetFullReportRecord(PiramidaRecords.Q_R500_Emelino_priem));

			child2 = childRecord.addChild(GetFullReportRecord("Р Вятка", "rVyatka"));
			child2.addChild(GetFullReportRecord(PiramidaRecords.P_R500_Vyatka_priem));
			child2.addChild(GetFullReportRecord(PiramidaRecords.Q_R500_Vyatka_priem));


			child2 = childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_KRU2_TVI));
			child2 = childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_KRU3_TP1));
			child2 = childRecord.addChild(GetFullReportRecord(PiramidaRecords.P_KRU2_TP2));

			record = RootSN.addChild(GetFullReportRecord("МБ Вода", "mbw"));
			record.addChild(GetFullReportRecord(PiramidaRecords.MBW_GES_Rash));
			record.addChild(GetFullReportRecord(PiramidaRecords.MBW_VB));
			record.addChild(GetFullReportRecord(PiramidaRecords.MBW_NB));
			record.addChild(GetFullReportRecord(PiramidaRecords.MBW_Napor));
			record.addChild(GetFullReportRecord(PiramidaRecords.MBW_Temp));
			record.addChild(GetFullReportRecord(PiramidaRecords.MBW_TempShit));

			childRecord = record.addChild(GetFullReportRecord("Расходы ГА", "gaRash"));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.MBW_GA1_Rash));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.MBW_GA2_Rash));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.MBW_GA3_Rash));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.MBW_GA4_Rash));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.MBW_GA5_Rash));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.MBW_GA6_Rash));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.MBW_GA7_Rash));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.MBW_GA8_Rash));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.MBW_GA9_Rash));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.MBW_GA10_Rash));

			childRecord = record.addChild(GetFullReportRecord("P ГА", "gaP"));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.MBW_GA1_P));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.MBW_GA2_P));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.MBW_GA3_P));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.MBW_GA4_P));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.MBW_GA5_P));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.MBW_GA6_P));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.MBW_GA7_P));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.MBW_GA8_P));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.MBW_GA9_P));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.MBW_GA10_P));

			childRecord = record.addChild(GetFullReportRecord("Q ГА", "gaQ"));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.MBW_GA1_Q));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.MBW_GA2_Q));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.MBW_GA3_Q));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.MBW_GA4_Q));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.MBW_GA5_Q));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.MBW_GA6_Q));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.MBW_GA7_Q));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.MBW_GA8_Q));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.MBW_GA9_Q));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.MBW_GA10_Q));

			childRecord = record.addChild(GetFullReportRecord("Открытие НА", "gaOtkrNA"));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.MBW_GA1_OtkrNA));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.MBW_GA2_OtkrNA));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.MBW_GA3_OtkrNA));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.MBW_GA4_OtkrNA));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.MBW_GA5_OtkrNA));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.MBW_GA6_OtkrNA));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.MBW_GA7_OtkrNA));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.MBW_GA8_OtkrNA));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.MBW_GA9_OtkrNA));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.MBW_GA10_OtkrNA));

			childRecord = record.addChild(GetFullReportRecord("Угол РК", "gaUgolRK"));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.MBW_GA1_UgolRK));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.MBW_GA2_UgolRK));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.MBW_GA3_UgolRK));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.MBW_GA4_UgolRK));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.MBW_GA5_UgolRK));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.MBW_GA6_UgolRK));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.MBW_GA7_UgolRK));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.MBW_GA8_UgolRK));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.MBW_GA9_UgolRK));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.MBW_GA10_UgolRK));

			childRecord = record.addChild(GetFullReportRecord("Напор", "gaNapor"));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.MBW_GA1_Napor));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.MBW_GA2_Napor));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.MBW_GA3_Napor));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.MBW_GA4_Napor));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.MBW_GA5_Napor));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.MBW_GA6_Napor));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.MBW_GA7_Napor));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.MBW_GA8_Napor));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.MBW_GA9_Napor));
			childRecord.addChild(GetFullReportRecord(PiramidaRecords.MBW_GA10_Napor));

			
		}


		public static FullReportRecord GetFullReportRecord(string title, string id) {
			FullReportRecord record = new FullReportRecord();
			record.Title = title;
			record.Key = id;
			record.Selectable = false;
			record.Children = new List<FullReportRecord>();
			return record;
		}

		public static FullReportRecord GetFullReportRecord(RecordTypeCalc record, string title = null) {
			FullReportRecord rec = new FullReportRecord();
			rec.Title = String.IsNullOrEmpty(title) ? record.Title : title;
			rec.Key = record.ID;
			rec.Selectable = true;
			rec.Axis1 = true;
			rec.Children = new List<FullReportRecord>();
			return rec;
		}

		public static FullReportRecord GetFullReportRecord(PiramidaRecord record, string title = null) {
			FullReportRecord rec = new FullReportRecord();
			rec.Title = String.IsNullOrEmpty(title) ? record.Title : title;
			rec.Key = record.Key;
			rec.Selectable = true;
			rec.Children = new List<FullReportRecord>();
			return rec;
		}


	}
}
