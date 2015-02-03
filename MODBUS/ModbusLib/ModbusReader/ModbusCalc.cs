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
				if (val < data.MinValue || val > data.MaxValue) {
					Logger.Info(String.Format("Выход за границы диапазона {0} val={1}", data.ID, val));
					val = double.NaN;
				}
			} catch (Exception e) {
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
				if (double.IsNaN(Data[key])) {
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

		public double GA1_AFTER_MAX() { return isGAAfterMax(1); }
		public double GA2_AFTER_MAX() { return isGAAfterMax(2); }
		public double GA3_AFTER_MAX() { return isGAAfterMax(3); }
		public double GA4_AFTER_MAX() { return isGAAfterMax(4); }
		public double GA5_AFTER_MAX() { return isGAAfterMax(5); }
		public double GA6_AFTER_MAX() { return isGAAfterMax(6); }
		public double GA7_AFTER_MAX() { return isGAAfterMax(7); }
		public double GA8_AFTER_MAX() { return isGAAfterMax(8); }
		public double GA9_AFTER_MAX() { return isGAAfterMax(9); }
		public double GA10_AFTER_MAX() { return isGAAfterMax(10); }

		public double GA1_LESS_MIN() { return isGALessMin(1); }
		public double GA2_LESS_MIN() { return isGALessMin(2); }
		public double GA3_LESS_MIN() { return isGALessMin(3); }
		public double GA4_LESS_MIN() { return isGALessMin(4); }
		public double GA5_LESS_MIN() { return isGALessMin(5); }
		public double GA6_LESS_MIN() { return isGALessMin(6); }
		public double GA7_LESS_MIN() { return isGALessMin(7); }
		public double GA8_LESS_MIN() { return isGALessMin(8); }
		public double GA9_LESS_MIN() { return isGALessMin(9); }
		public double GA10_LESS_MIN() { return isGALessMin(10); }

		public double GA1_SK() { return isGASK(1); }
		public double GA2_SK() { return isGASK(2); }
		public double GA9_SK() { return isGASK(9); }
		public double GA10_SK() { return isGASK(10); }

		public double GA1_GEN() { return isGAGen(1); }
		public double GA2_GEN() { return isGAGen(2); }
		public double GA3_GEN() { return isGAGen(3); }
		public double GA4_GEN() { return isGAGen(4); }
		public double GA5_GEN() { return isGAGen(5); }
		public double GA6_GEN() { return isGAGen(6); }
		public double GA7_GEN() { return isGAGen(7); }
		public double GA8_GEN() { return isGAGen(8); }
		public double GA9_GEN() { return isGAGen(9); }
		public double GA10_GEN() { return isGAGen(10); }

		public double GA1_HHG() { return isGAHHG(1); }
		public double GA2_HHG() { return isGAHHG(2); }
		public double GA3_HHG() { return isGAHHG(3); }
		public double GA4_HHG() { return isGAHHG(4); }
		public double GA5_HHG() { return isGAHHG(5); }
		public double GA6_HHG() { return isGAHHG(6); }
		public double GA7_HHG() { return isGAHHG(7); }
		public double GA8_HHG() { return isGAHHG(8); }
		public double GA9_HHG() { return isGAHHG(9); }
		public double GA10_HHG() { return isGAHHG(10); }

		public double GA1_HHT() { return isGAHHT(1); }
		public double GA2_HHT() { return isGAHHT(2); }
		public double GA3_HHT() { return isGAHHT(3); }
		public double GA4_HHT() { return isGAHHT(4); }
		public double GA5_HHT() { return isGAHHT(5); }
		public double GA6_HHT() { return isGAHHT(6); }
		public double GA7_HHT() { return isGAHHT(7); }
		public double GA8_HHT() { return isGAHHT(8); }
		public double GA9_HHT() { return isGAHHT(9); }
		public double GA10_HHT() { return isGAHHT(10); }

		public double GA1_RUN() { return isGARun(1); }
		public double GA2_RUN() { return isGARun(2); }
		public double GA3_RUN() { return isGARun(3); }
		public double GA4_RUN() { return isGARun(4); }
		public double GA5_RUN() { return isGARun(5); }
		public double GA6_RUN() { return isGARun(6); }
		public double GA7_RUN() { return isGARun(7); }
		public double GA8_RUN() { return isGARun(8); }
		public double GA9_RUN() { return isGARun(9); }
		public double GA10_RUN() { return isGARun(10); }

		public double GA1_NPRCH() { return isGANPRCH(1); }
		public double GA2_NPRCH() { return isGANPRCH(2); }
		public double GA3_NPRCH() { return isGANPRCH(3); }
		public double GA4_NPRCH() { return isGANPRCH(4); }
		public double GA5_NPRCH() { return isGANPRCH(5); }
		public double GA6_NPRCH() { return isGANPRCH(6); }
		public double GA7_NPRCH() { return isGANPRCH(7); }
		public double GA8_NPRCH() { return isGANPRCH(8); }
		public double GA9_NPRCH() { return isGANPRCH(9); }
		public double GA10_NPRCH() { return isGANPRCH(10); }

		public double GA1_OPRCH() { return isGAOPRCH(1); }
		public double GA2_OPRCH() { return isGAOPRCH(2); }
		public double GA3_OPRCH() { return isGAOPRCH(3); }
		public double GA4_OPRCH() { return isGAOPRCH(4); }
		public double GA5_OPRCH() { return isGAOPRCH(5); }
		public double GA6_OPRCH() { return isGAOPRCH(6); }
		public double GA7_OPRCH() { return isGAOPRCH(7); }
		public double GA8_OPRCH() { return isGAOPRCH(8); }
		public double GA9_OPRCH() { return isGAOPRCH(9); }
		public double GA10_OPRCH() { return isGAOPRCH(10); }

		public double GA1_AVRCHM() { return isGAAVRCHM(1); }
		public double GA2_AVRCHM() { return isGAAVRCHM(1); }
		public double GA3_AVRCHM() { return isGAAVRCHM(1); }
		public double GA4_AVRCHM() { return isGAAVRCHM(1); }
		public double GA5_AVRCHM() { return isGAAVRCHM(1); }
		public double GA6_AVRCHM() { return isGAAVRCHM(1); }
		public double GA7_AVRCHM() { return isGAAVRCHM(1); }
		public double GA8_AVRCHM() { return isGAAVRCHM(1); }
		public double GA9_AVRCHM() { return isGAAVRCHM(1); }
		public double GA10_AVRCHM() { return isGAAVRCHM(1); }

		#endregion

		protected double isGARun(int gaNumber) {
			try {
				return 1 - GlobalVotGES.getBIT((UInt16)this[String.Format("MB_GA{0}_STATE", gaNumber)], 15);
			} catch {
				return this[String.Format("MB_GA{0}_P", gaNumber)] != 0 ? 1 : 0;
			}
		}

		protected double isGAGen(int gaNumber) {
			try {
				UInt16 state=(UInt16)this[String.Format("MB_GA{0}_STATE", gaNumber)];
				return  (GlobalVotGES.getBIT(state, 0) == 1 && GlobalVotGES.getBIT(state, 1) == 1 &&  GlobalVotGES.getBIT(state, 5) == 0) ? 1 : 0;
			} catch {
				return this[String.Format("MB_GA{0}_P", gaNumber)] > 10 ? 1 : 0;
			}
		}

		protected double isGASK(int gaNumber) {
			try {
				return GlobalVotGES.getBIT((UInt16)this[String.Format("MB_GA{0}_STATE", gaNumber)], 5);
			} catch {
				return this[String.Format("MB_GA{0}_P", gaNumber)] < 0 ? 1 : 0;
			}
		}


		protected double isGAHHG(int gaNumber) {
			return GlobalVotGES.getBIT((UInt16)this[String.Format("MB_GA{0}_STATE", gaNumber)], 7);
		}

		protected double isGAHHT(int gaNumber) {
			return GlobalVotGES.getBIT((UInt16)this[String.Format("MB_GA{0}_STATE", gaNumber)], 6);
		}


		protected double isGALessMin(int gaNumber) {
			return isGAGen(gaNumber) > 0 && this[String.Format("MB_GA{0}_P", gaNumber)] < 34 ? 1 : 0;
		}

		protected double isGANPRCH(int gaNumber) {
			try {
				UInt16 state = (UInt16)this[String.Format("MB_GA{0}_STATE", gaNumber)];
				return (GlobalVotGES.getBIT(state, 3) == 1 && isGAGen(gaNumber) > 0 )? 1 : 0;
			}
			catch {
				return 0;
			}
		}

		protected double isGAOPRCH(int gaNumber) {
			return 0;
			try {
				double correct = this[String.Format("MB_GA{0}_PF", gaNumber)];
				return correct != 0?0:1;
			}
			catch {
				return 0;
			}
		}

		protected double isGAAVRCHM(int gaNumber) {
			return 0;
			try {
				double correct = this[String.Format("MB_GA{0}_PF", gaNumber)];
				return correct != 0 ? 0 : 1;
			}
			catch {
				return 0;
			}
		}


		protected double isGAAfterMax(int gaNumber) {
			try {
				return isGAGen(gaNumber) > 0 && this[String.Format("MB_GA{0}_P", gaNumber)] > this[String.Format("MB_GA{0}_MAXP_TEC", gaNumber)] ? 1 : 0;
			} catch {
				return isGAGen(gaNumber) > 0 && this[String.Format("MB_GA{0}_P", gaNumber)] > 105 ? 1 : 0;
			}
		}

	}




}
