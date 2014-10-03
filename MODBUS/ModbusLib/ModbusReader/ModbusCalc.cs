using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using VotGES;
using VotGES.Rashod;

namespace ModbusLib {
	public class ModbusCalc {
		#region InitClass

		public SortedList<string, double> Data { get; set; }
		public SortedList<string, double> ResultData { get; set; }
		public ModbusInitDataArray InitCalc { get; set; }

		public ModbusCalc() {
			ResultData = new SortedList<string, double>();
		}

		public void call(string name, ModbusInitData data) {
			double val = Double.NaN;
			try {
				MethodInfo mi = typeof(ModbusCalc).GetMethod(name, System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
				val = (double)mi.Invoke(this, new object[] { });
			}
			catch (Exception e) {
				if (!e.ToString().Contains("FlagError")) {
					Logger.Error("Ошибка при расчете метода " + name);
					Logger.Error(e.ToString());
				}
				val = Double.NaN;
			}
			Data[InitCalc.ID + "_" + data.ID] = val;
			ResultData.Add(data.ID, val);
		}

		public void Init(SortedList<string, double> Data) {
			this.Data = Data;
			ResultData.Clear();
		}

		#endregion

		public double this[string key] {
			get {
				if (Double.IsNaN(Data[key])) {
					throw new Exception("FlagError");
				}

				return Data[key];

			}
		}

		#region P
		public double P_GTP1() {
			return this["MB_GA1_P"] + this["MB_GA2_P"];
		}

		public double P_GTP2() {
			return this["MB_GA3_P"] + this["MB_GA4_P"] + this["MB_GA5_P"] + this["MB_GA6_P"] + this["MB_GA7_P"] + this["MB_GA8_P"] + this["MB_GA9_P"] + this["MB_GA10_P"];
		}

		public double P_GES() {
			return this["MB_GA1_P"] + this["MB_GA2_P"] + this["MB_GA3_P"] + this["MB_GA4_P"] + this["MB_GA5_P"] + this["MB_GA6_P"] + this["MB_GA7_P"] + this["MB_GA8_P"] + this["MB_GA9_P"] + this["MB_GA10_P"];
		}

		public double P_RGE2() {
			return this["MB_GA3_P"] + this["MB_GA4_P"];
		}

		public double P_RGE3() {
			return this["MB_GA5_P"] + this["MB_GA6_P"];
		}

		public double P_RGE4() {
			return this["MB_GA7_P"] + this["MB_GA8_P"] + this["MB_GA9_P"] + this["MB_GA10_P"];
		}


		#endregion

		#region Rashod
		public double Rashod_GES() {
			return this["MB_GA1_Rash"] + this["MB_GA2_Rash"] + this["MB_GA3_Rash"] + this["MB_GA4_Rash"] + this["MB_GA5_Rash"] +
					this["MB_GA6_Rash"] + this["MB_GA7_Rash"] + this["MB_GA8_Rash"] + this["MB_GA9_Rash"] + this["MB_GA10_Rash"];
		}

		public double Rashod_GTP1() {
			return this["MB_GA1_Rash"] + this["MB_GA2_Rash"];
		}

		public double Rashod_GTP2() {
			return this["MB_GA3_Rash"] + this["MB_GA4_Rash"] + this["MB_GA5_Rash"] +
					this["MB_GA6_Rash"] + this["MB_GA7_Rash"] + this["MB_GA8_Rash"] + this["MB_GA9_Rash"] + this["MB_GA10_Rash"];
		}

		public static List<int> gtp1 = new List<int>(new int[] { 1, 2 });
		public static List<int> gtp2 = new List<int>(new int[] { 3, 4, 5, 6, 7, 8, 9, 10 });
		public static List<int> ges = new List<int>(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 });

		public double OptRashod_GTP1() {
			return RUSA.getOptimRashod(this["Calc_P_GTP1"], this["MB_Napor"], true, null, gtp1);
		}

		public double OptRashod_GTP2() {
			return RUSA.getOptimRashod(this["Calc_P_GTP2"], this["MB_Napor"], true, null, gtp2);
		}

		public double OptRashod_GES() {
			return RUSA.getOptimRashod(this["Calc_P_GES"], this["MB_Napor"], true, null, ges);
		}
		#endregion

		#region Ogran

		public double GA1_AFTER_MAX() { return isGAAfterMax(this["MB_GA1_STATE"], this["MB_GA1_P"], this["MB_GA1_MAXP_TEC"]); }
		public double GA2_AFTER_MAX() { return isGAAfterMax(this["MB_GA2_STATE"], this["MB_GA2_P"], this["MB_GA2_MAXP_TEC"]); }
		public double GA3_AFTER_MAX() { return isGAAfterMax(this["MB_GA3_STATE"], this["MB_GA3_P"], this["MB_GA3_MAXP_TEC"]); }
		public double GA4_AFTER_MAX() { return isGAAfterMax(this["MB_GA4_STATE"], this["MB_GA4_P"], this["MB_GA4_MAXP_TEC"]); }
		public double GA5_AFTER_MAX() { return isGAAfterMax(this["MB_GA5_STATE"], this["MB_GA5_P"], this["MB_GA5_MAXP_TEC"]); }
		public double GA6_AFTER_MAX() { return isGAAfterMax(this["MB_GA6_STATE"], this["MB_GA6_P"], this["MB_GA6_MAXP_TEC"]); }
		public double GA7_AFTER_MAX() { return isGAAfterMax(this["MB_GA7_STATE"], this["MB_GA7_P"], this["MB_GA7_MAXP_TEC"]); }
		public double GA8_AFTER_MAX() { return isGAAfterMax(this["MB_GA8_STATE"], this["MB_GA8_P"], this["MB_GA8_MAXP_TEC"]); }
		public double GA9_AFTER_MAX() { return isGAAfterMax(this["MB_GA9_STATE"], this["MB_GA9_P"], this["MB_GA9_MAXP_TEC"]); }
		public double GA10_AFTER_MAX() { return isGAAfterMax(this["MB_GA10_STATE"], this["MB_GA10_P"], this["MB_GA10_MAXP_TEC"]); }

		public double GA1_LESS_MIN() { return isGALessMin(this["MB_GA1_STATE"], this["MB_GA1_P"], 35); }
		public double GA2_LESS_MIN() { return isGALessMin(this["MB_GA2_STATE"], this["MB_GA2_P"], 35); }
		public double GA3_LESS_MIN() { return isGALessMin(this["MB_GA3_STATE"], this["MB_GA3_P"], 35); }
		public double GA4_LESS_MIN() { return isGALessMin(this["MB_GA4_STATE"], this["MB_GA4_P"], 35); }
		public double GA5_LESS_MIN() { return isGALessMin(this["MB_GA5_STATE"], this["MB_GA5_P"], 35); }
		public double GA6_LESS_MIN() { return isGALessMin(this["MB_GA6_STATE"], this["MB_GA6_P"], 35); }
		public double GA7_LESS_MIN() { return isGALessMin(this["MB_GA7_STATE"], this["MB_GA7_P"], 35); }
		public double GA8_LESS_MIN() { return isGALessMin(this["MB_GA8_STATE"], this["MB_GA8_P"], 35); }
		public double GA9_LESS_MIN() { return isGALessMin(this["MB_GA9_STATE"], this["MB_GA9_P"], 35); }
		public double GA10_LESS_MIN() { return isGALessMin(this["MB_GA10_STATE"], this["MB_GA10_P"], 35); }

		public double GA1_SK() { return isGASK(this["MB_GA1_STATE"]); }
		public double GA2_SK() { return isGASK(this["MB_GA2_STATE"]); }
		public double GA9_SK() { return isGASK(this["MB_GA9_STATE"]); }
		public double GA10_SK() { return isGASK(this["MB_GA10_STATE"]); }

		public double GA1_GEN() { return isGAGen(this["MB_GA1_STATE"]); }
		public double GA2_GEN() { return isGAGen(this["MB_GA2_STATE"]); }
		public double GA3_GEN() { return isGAGen(this["MB_GA3_STATE"]); }
		public double GA4_GEN() { return isGAGen(this["MB_GA4_STATE"]); }
		public double GA5_GEN() { return isGAGen(this["MB_GA5_STATE"]); }
		public double GA6_GEN() { return isGAGen(this["MB_GA6_STATE"]); }
		public double GA7_GEN() { return isGAGen(this["MB_GA7_STATE"]); }
		public double GA8_GEN() { return isGAGen(this["MB_GA8_STATE"]); }
		public double GA9_GEN() { return isGAGen(this["MB_GA9_STATE"]); }
		public double GA10_GEN() { return isGAGen(this["MB_GA10_STATE"]); }

		public double GA1_HHG() { return isGAHHG(this["MB_GA1_STATE"]); }
		public double GA2_HHG() { return isGAHHG(this["MB_GA2_STATE"]); }
		public double GA3_HHG() { return isGAHHG(this["MB_GA3_STATE"]); }
		public double GA4_HHG() { return isGAHHG(this["MB_GA4_STATE"]); }
		public double GA5_HHG() { return isGAHHG(this["MB_GA5_STATE"]); }
		public double GA6_HHG() { return isGAHHG(this["MB_GA6_STATE"]); }
		public double GA7_HHG() { return isGAHHG(this["MB_GA7_STATE"]); }
		public double GA8_HHG() { return isGAHHG(this["MB_GA8_STATE"]); }
		public double GA9_HHG() { return isGAHHG(this["MB_GA9_STATE"]); }
		public double GA10_HHG() { return isGAHHG(this["MB_GA10_STATE"]); }

		public double GA1_HHT() { return isGAHHT(this["MB_GA1_STATE"]); }
		public double GA2_HHT() { return isGAHHT(this["MB_GA2_STATE"]); }
		public double GA3_HHT() { return isGAHHT(this["MB_GA3_STATE"]); }
		public double GA4_HHT() { return isGAHHT(this["MB_GA4_STATE"]); }
		public double GA5_HHT() { return isGAHHT(this["MB_GA5_STATE"]); }
		public double GA6_HHT() { return isGAHHT(this["MB_GA6_STATE"]); }
		public double GA7_HHT() { return isGAHHT(this["MB_GA7_STATE"]); }
		public double GA8_HHT() { return isGAHHT(this["MB_GA8_STATE"]); }
		public double GA9_HHT() { return isGAHHT(this["MB_GA9_STATE"]); }
		public double GA10_HHT() { return isGAHHT(this["MB_GA10_STATE"]); }

		public double GA1_RUN() { return isGARun(this["MB_GA1_STATE"]); }
		public double GA2_RUN() { return isGARun(this["MB_GA2_STATE"]); }
		public double GA3_RUN() { return isGARun(this["MB_GA3_STATE"]); }
		public double GA4_RUN() { return isGARun(this["MB_GA4_STATE"]); }
		public double GA5_RUN() { return isGARun(this["MB_GA5_STATE"]); }
		public double GA6_RUN() { return isGARun(this["MB_GA6_STATE"]); }
		public double GA7_RUN() { return isGARun(this["MB_GA7_STATE"]); }
		public double GA8_RUN() { return isGARun(this["MB_GA8_STATE"]); }
		public double GA9_RUN() { return isGARun(this["MB_GA9_STATE"]); }
		public double GA10_RUN() { return isGARun(this["MB_GA10_STATE"]); }

		#endregion


		protected int isGARun(double gaState) {
			return 1-GlobalVotGES.getBIT((UInt16)gaState, 15);
		}

		protected int isGAGen(double gaState) {
			return GlobalVotGES.getBIT((UInt16)gaState, 4);
		}

		protected int isGASK(double gaState) {
			return GlobalVotGES.getBIT((UInt16)gaState, 5);
		}

		protected int isGAHHG(double gaState) {
			return GlobalVotGES.getBIT((UInt16)gaState, 7);
		}

		protected int isGAHHT(double gaState) {
			return GlobalVotGES.getBIT((UInt16)gaState, 6);
		}

		protected int isGALessMin(double gaState, double gaP, double Min) {
			return isGAGen(gaState) == 1 && gaP < Min ? 1 : 0;
		}

		protected int isGAAfterMax(double gaState, double gaP, double Max) {
			return isGAGen(gaState) == 1 && gaP > Max ? 1 : 0;
		}
	}




}
