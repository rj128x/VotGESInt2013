using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using VotGES;
using VotGES.Rashod;

namespace ModbusLib
{
	public class ModbusCalc
	{
		#region InitClass

		public SortedList<string, double> Data { get; set; }
		public SortedList<string, double> ResultData { get; set; }
		public ModbusInitDataArray InitCalc { get; set; }

		public ModbusCalc() {
			ResultData = new SortedList<string, double>();
		}

		public void call(string name, ModbusInitData data) {
			double val=Double.NaN;
			try {
				MethodInfo mi = typeof(ModbusCalc).GetMethod(name, System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);
				val = (double)mi.Invoke(this, new object[] { });
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
				if (Double.IsNaN(Data[key])) {
					throw new Exception("FlagError");
				}
				return Data[key];
			}
		}

		#region P
		public double P_GTP1() {
			return this["MB_216"] + this["MB_266"];
		}

		public double P_GTP2() {
			return this["MB_316"] + this["MB_366"] + this["MB_416"] + this["MB_466"] + this["MB_516"] + this["MB_566"] + this["MB_616"] + this["MB_666"];
		}

		public double P_GES() {
			return this["MB_216"] + this["MB_266"] + this["MB_316"] + this["MB_366"] + this["MB_416"] + this["MB_466"] + this["MB_516"] + this["MB_566"] + this["MB_616"] + this["MB_666"];
		}

		public double P_RGE2() {
			return this["MB_316"] + this["MB_366"];
		}

		public double P_RGE3() {
			return this["MB_416"] + this["MB_466"];
		}

		public double P_RGE4() {
			return this["MB_516"] + this["MB_566"] + this["MB_616"] + this["MB_666"];
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

		public static List<int>gtp1=new List<int>(new int[] { 1, 2 });
		public static List<int>gtp2=new List<int>(new int[] { 3, 4, 5, 6, 7, 8, 9, 10 });
		public static List<int>ges=new List<int>(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 });

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

    

	}
}
