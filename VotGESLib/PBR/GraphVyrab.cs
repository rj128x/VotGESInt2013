﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VotGES.Chart;

namespace VotGES.PBR
{
	public class GraphVyrabTableRow
	{
		public double GTP1 { get; set; }
		public double GTP2 { get; set; }
		public double GES { get; set; }
		public string Title { get; set; }
		public string Format { get; set; }

		public GraphVyrabTableRow(String title, double gtp1, double gtp2, double ges) {
			GTP1 = gtp1;
			GTP2 = gtp2;
			GES = ges;
			Title = title;
		}

		public GraphVyrabTableRow() {
		}

	}

	public class GraphVyrabAnswer
	{
		public ChartAnswer Chart { get; set; }
		public DateTime ActualDate { get; set; }
		public List<GraphVyrabTableRow> TableCurrent { get; set; }
		public List<GraphVyrabTableRow> TableHour { get; set; }

		public double VyrabPlan { get; set; }
		public double VyrabFakt { get; set; }
		public double VyrabDiff { get; set; }
		public double VyrabDiffProc { get; set; }

		public GraphVyrabAnswer() {
			TableCurrent = new List<GraphVyrabTableRow>();
			TableHour = new List<GraphVyrabTableRow>();
		}		
	}

	public class CheckGraphVyrabTableRow
	{
		public double GTP1Fakt { get; set; }
		public double GTP2Fakt { get; set; }
		public double GESFakt { get; set; }

		public double GTP1Plan { get; set; }
		public double GTP2Plan { get; set; }
		public double GESPlan { get; set; }

		public double GTP1Diff { get; set; }
		public double GTP2Diff { get; set; }
		public double GESDiff { get; set; }

		public double GTP1DiffProc { get; set; }
		public double GTP2DiffProc { get; set; }
		public double GESDiffProc { get; set; }

		public string Title { get; set; }
	}

	public class CheckGraphVyrabAnswer
	{
		public ChartAnswer Chart { get; set; }
		public List<CheckGraphVyrabTableRow> TableHH { get; set; }
		public List<CheckGraphVyrabTableRow> TableH { get; set; }

		public CheckGraphVyrabAnswer() {
			TableHH = new List<CheckGraphVyrabTableRow>();
			TableH = new List<CheckGraphVyrabTableRow>();
		}
	}


	public class GraphVyrab
	{
		public static GraphVyrabAnswer getAnswer(DateTime date, bool calcTables = true, bool steppedPBR=true) {
			DateTime dateStart=date.Date;
			DateTime dateEnd=date.Date.AddHours(24);
			date = calcTables ? date : dateEnd;

			GraphVyrabAnswer answer=new GraphVyrabAnswer();


			PBRData ges=new PBRData(dateStart, dateEnd, date, GTPEnum.ges);
			PBRData gtp1=new PBRData(dateStart, dateEnd, date, GTPEnum.gtp1);
			PBRData gtp2=new PBRData(dateStart, dateEnd, date, GTPEnum.gtp2);
			ges.IsSteppedPBR = steppedPBR;
			gtp1.IsSteppedPBR = steppedPBR;
			gtp2.IsSteppedPBR = steppedPBR;
		
			answer.Chart = new ChartAnswer();
			answer.Chart.Properties = getChartProperties(steppedPBR);
			answer.Chart.Data = new ChartData();

			gtp1.InitData();
			gtp2.InitData();
			ges.InitData();

			DateTime[] dates = new DateTime[] {gtp1.Date, gtp2.Date,ges.Date};
			answer.ActualDate = dates.ToList().Min();
			DateTime lastDate = answer.ActualDate;

			try {
				if (Math.Abs(gtp1.RealP[lastDate] + gtp2.RealP[lastDate] - ges.RealP[lastDate]) > 5) {
					answer.ActualDate = lastDate.AddMinutes(-1);
					lastDate = answer.ActualDate;
				}
			}
			catch { }

			answer.VyrabPlan = ges.IntegratedPBR[lastDate];
			answer.VyrabFakt = ges.IntegratedP[lastDate];
			answer.VyrabDiff = ges.IntegratedP[lastDate] - ges.IntegratedPBR[lastDate];
			answer.VyrabDiffProc = PBRData.getDiffProc(ges.IntegratedP[lastDate], ges.IntegratedPBR[lastDate]);


			if (calcTables) {
				answer.TableCurrent.Add(new GraphVyrabTableRow("P план", Math.Round(gtp1.MinutesPBR[lastDate]), Math.Round(gtp2.MinutesPBR[lastDate]), Math.Round(ges.MinutesPBR[lastDate])));
				answer.TableCurrent.Add(new GraphVyrabTableRow("P факт", Math.Round(gtp1.RealP[lastDate]), Math.Round(gtp2.RealP[lastDate]), Math.Round(ges.RealP[lastDate])));
				answer.TableCurrent.Add(new GraphVyrabTableRow("P откл", gtp1.getDiff(lastDate), gtp2.getDiff(lastDate), ges.getDiff(lastDate)));
				answer.TableCurrent.Add(new GraphVyrabTableRow("P откл %", gtp1.getDiffProc(lastDate), gtp2.getDiffProc(lastDate), ges.getDiffProc(lastDate)));


				SortedList<string,double> gtp1Hour=gtp1.getHourVals(lastDate);
				SortedList<string,double> gtp2Hour=gtp2.getHourVals(lastDate);
				SortedList<string,double> gesHour=ges.getHourVals(lastDate);

				answer.TableHour.Add(new GraphVyrabTableRow("P план", Math.Round(gtp1Hour["plan"]), Math.Round(gtp2Hour["plan"]), Math.Round(gesHour["plan"])));
				answer.TableHour.Add(new GraphVyrabTableRow("P факт", Math.Round(gtp1Hour["fakt"]), Math.Round(gtp2Hour["fakt"]), Math.Round(gesHour["fakt"])));
				answer.TableHour.Add(new GraphVyrabTableRow("P откл", gtp1Hour["diff"], gtp2Hour["diff"], gesHour["diff"]));
				answer.TableHour.Add(new GraphVyrabTableRow("P откл %", gtp1Hour["diffProc"], gtp2Hour["diffProc"], gesHour["diffProc"]));
				answer.TableHour.Add(new GraphVyrabTableRow("P рек", Math.Round(gtp1Hour["recP"]), Math.Round(gtp2Hour["recP"]), Math.Round(gesHour["recP"])));
			}


			answer.Chart.Data.addSerie(getDataSerie("gtp1Fakt", gtp1.RealP, -1));
			answer.Chart.Data.addSerie(getDataSerie("gtp2Fakt", gtp2.RealP, -1));
			answer.Chart.Data.addSerie(getDataSerie("gesFakt", ges.RealP, -1));

			
			answer.Chart.Data.addSerie(getDataSerie("gtp1Plan", steppedPBR?gtp1.SteppedPBR:gtp1.RealPBR, 0));
			answer.Chart.Data.addSerie(getDataSerie("gtp2Plan", steppedPBR?gtp2.SteppedPBR:gtp2.RealPBR, 0));
			answer.Chart.Data.addSerie(getDataSerie("gesPlan", steppedPBR?ges.SteppedPBR:ges.RealPBR, 0));

			answer.Chart.Data.addSerie(getDataSerie("vyrabPlan", ges.IntegratedPBR, -1));
			answer.Chart.Data.addSerie(getDataSerie("vyrabFakt", ges.IntegratedP, -1));

			return answer;
		}

		public static CheckGraphVyrabAnswer getAnswerHH(DateTime date) {
			DateTime dateStart=date.Date;
			DateTime dateEnd=date.Date.AddHours(24);

			CheckGraphVyrabAnswer answer=new CheckGraphVyrabAnswer();


			PBRDataHH ges=new PBRDataHH(dateStart, dateEnd, GTPEnum.ges);
			PBRDataHH gtp1=new PBRDataHH(dateStart, dateEnd, GTPEnum.gtp1);
			PBRDataHH gtp2=new PBRDataHH(dateStart, dateEnd, GTPEnum.gtp2);

			answer.Chart = new ChartAnswer();
			answer.Chart.Properties = getChartProperties(true);
			answer.Chart.Data = new ChartData();

			gtp1.InitData();
			gtp2.InitData();
			ges.InitData();


			CheckGraphVyrabTableRow rowFull=new CheckGraphVyrabTableRow();
			rowFull.Title = "Итог";
			CheckGraphVyrabTableRow rowMin=new CheckGraphVyrabTableRow();
			rowMin.Title = "Итог -";
			CheckGraphVyrabTableRow rowPl=new CheckGraphVyrabTableRow();
			rowPl.Title = "Итог +";

			answer.TableH.Add(rowFull);
			answer.TableH.Add(rowMin);
			answer.TableH.Add(rowPl);

			answer.TableHH.Add(rowFull);
			answer.TableHH.Add(rowMin);
			answer.TableHH.Add(rowPl);

			foreach (DateTime dt in ges.HalfHoursPBR.Keys) {
				CheckGraphVyrabTableRow row=new CheckGraphVyrabTableRow();

				row.Title = dt.ToString("dd.MM.yy HH:mm");
				row.GESFakt = ges.HalfHoursP[dt];
				row.GESPlan = ges.HalfHoursPBR[dt];
				row.GESDiff = ges.HalfHoursP[dt] - ges.HalfHoursPBR[dt];
				row.GESDiffProc = PBRData.getDiffProc(ges.HalfHoursP[dt], ges.HalfHoursPBR[dt]);
			

				row.GTP1Fakt = gtp1.HalfHoursP[dt];
				row.GTP1Plan = gtp1.HalfHoursPBR[dt];
				row.GTP1Diff = gtp1.HalfHoursP[dt] - gtp1.HalfHoursPBR[dt];
				row.GTP1DiffProc = PBRData.getDiffProc(gtp1.HalfHoursP[dt], gtp1.HalfHoursPBR[dt]);

				row.GTP2Fakt = gtp2.HalfHoursP[dt];
				row.GTP2Plan = gtp2.HalfHoursPBR[dt];
				row.GTP2Diff = gtp2.HalfHoursP[dt] - gtp2.HalfHoursPBR[dt];
				row.GTP2DiffProc = PBRData.getDiffProc(gtp2.HalfHoursP[dt], gtp2.HalfHoursPBR[dt]);

				rowMin.GESFakt += row.GESFakt / 2; rowPl.GESFakt += row.GESFakt / 2;	rowFull.GESFakt += row.GESFakt / 2;
				rowMin.GESPlan += row.GESPlan / 2; rowPl.GESPlan += row.GESPlan / 2; rowFull.GESPlan += row.GESPlan / 2;
				rowMin.GESDiff += row.GESDiff < 0 ? row.GESDiff/2 : 0;
				rowPl.GESDiff += row.GESDiff > 0 ? row.GESDiff/2 : 0;
				rowFull.GESDiff += row.GESDiff/2;

				rowMin.GTP1Fakt += row.GTP1Fakt / 2; rowPl.GTP1Fakt += row.GTP1Fakt / 2; rowFull.GTP1Fakt += row.GTP1Fakt / 2;
				rowMin.GTP1Plan += row.GTP1Plan / 2; rowPl.GTP1Plan += row.GTP1Plan / 2; rowFull.GTP1Plan += row.GTP1Plan / 2;
				rowMin.GTP1Diff += row.GTP1Diff < 0 ? row.GTP1Diff / 2 : 0;
				rowPl.GTP1Diff += row.GTP1Diff > 0 ? row.GTP1Diff / 2 : 0;
				rowFull.GTP1Diff += row.GTP1Diff / 2;

				rowMin.GTP2Fakt += row.GTP2Fakt / 2; rowPl.GTP2Fakt += row.GTP2Fakt / 2; rowFull.GTP2Fakt += row.GTP2Fakt / 2;
				rowMin.GTP2Plan += row.GTP2Plan / 2; rowPl.GTP2Plan += row.GTP2Plan / 2; rowFull.GTP2Plan += row.GTP2Plan / 2;
				rowMin.GTP2Diff += row.GTP2Diff < 0 ? row.GTP2Diff / 2 : 0;
				rowPl.GTP2Diff += row.GTP2Diff > 0 ? row.GTP2Diff / 2 : 0;
				rowFull.GTP2Diff += row.GTP2Diff / 2;

				answer.TableHH.Add(row);
			}

			rowMin.GESDiffProc = PBRData.getDiffProcDiff(rowMin.GESDiff, rowMin.GESPlan);
			rowPl.GESDiffProc = PBRData.getDiffProcDiff(rowPl.GESDiff, rowPl.GESPlan);
			rowFull.GESDiffProc = PBRData.getDiffProc(rowFull.GESFakt, rowFull.GESPlan);

			rowMin.GTP1DiffProc = PBRData.getDiffProcDiff(rowMin.GTP1Diff, rowMin.GTP1Plan);
			rowPl.GTP1DiffProc = PBRData.getDiffProcDiff( rowPl.GTP1Diff, rowPl.GTP1Plan);
			rowFull.GTP1DiffProc = PBRData.getDiffProc(rowFull.GTP1Fakt, rowFull.GTP1Plan);

			rowMin.GTP2DiffProc = PBRData.getDiffProcDiff(rowMin.GTP2Diff, rowMin.GTP2Plan);
			rowPl.GTP2DiffProc = PBRData.getDiffProcDiff(rowPl.GTP2Diff, rowPl.GTP2Plan);
			rowFull.GTP2DiffProc = PBRData.getDiffProc(rowFull.GTP2Fakt, rowFull.GTP2Plan);


			foreach (DateTime dt in ges.HoursPBR.Keys) {
				CheckGraphVyrabTableRow row=new CheckGraphVyrabTableRow();
				row.Title = dt.ToString("dd.MM.yy HH:mm");
				row.GESFakt = ges.HoursP[dt];
				row.GESPlan = ges.HoursPBR[dt];
				row.GESDiff = ges.HoursP[dt] - ges.HoursPBR[dt];
				row.GESDiffProc = PBRData.getDiffProc(ges.HoursP[dt], ges.HoursPBR[dt]);

				row.GTP1Fakt = gtp1.HoursP[dt];
				row.GTP1Plan = gtp1.HoursPBR[dt];
				row.GTP1Diff = gtp1.HoursP[dt] - gtp1.HoursPBR[dt];
				row.GTP1DiffProc = PBRData.getDiffProc(gtp1.HoursP[dt], gtp1.HoursPBR[dt]);

				row.GTP2Fakt = gtp2.HoursP[dt];
				row.GTP2Plan = gtp2.HoursPBR[dt];
				row.GTP2Diff = gtp2.HoursP[dt] - gtp2.HoursPBR[dt];
				row.GTP2DiffProc = PBRData.getDiffProc(gtp2.HoursP[dt], gtp2.HoursPBR[dt]);

				answer.TableH.Add(row);
			}


			answer.Chart.Data.addSerie(getDataSerie("gtp1Fakt", gtp1.HalfHoursP, -30));
			answer.Chart.Data.addSerie(getDataSerie("gtp2Fakt", gtp2.HalfHoursP, -30));
			answer.Chart.Data.addSerie(getDataSerie("gesFakt", ges.HalfHoursP, -30));
			answer.Chart.Data.addSerie(getDataSerie("gtp1Plan", gtp1.HalfHoursPBR, -30));
			answer.Chart.Data.addSerie(getDataSerie("gtp2Plan", gtp2.HalfHoursPBR, -30));
			answer.Chart.Data.addSerie(getDataSerie("gesPlan", ges.HalfHoursPBR, -30));

			answer.Chart.Properties.removeSerie("vyrabFakt");
			answer.Chart.Properties.removeSerie("vyrabPlan");
			return answer;
		}





		public static ChartDataSerie getDataSerie(string serieName, SortedList<DateTime, double> data, int correctTime) {
			ChartDataSerie serie=new ChartDataSerie();
			serie.Name = serieName;
			foreach (KeyValuePair<DateTime,double> de in data) {
				serie.Points.Add(new ChartDataPoint(de.Key.AddMinutes(correctTime), de.Value));
			}
			return serie;
		}

		public static ChartProperties getChartProperties(bool steppedPBR) {
			ChartProperties props=new ChartProperties();
			props.XAxisType = XAxisTypeEnum.datetime;
			props.XValueFormatString = "dd.MM HH:mm";

			ChartAxisProperties pAx=new ChartAxisProperties();
			pAx.Auto = true;
			pAx.Min = 0;
			pAx.Max = 1050;
			pAx.Interval = 50;
			pAx.Index = 0;

			ChartAxisProperties vAx=new ChartAxisProperties();
			vAx.Auto = true;
			vAx.Index = 1;

			props.addAxis(pAx);
			props.addAxis(vAx);

			ChartSerieProperties gtp1FaktSerie=new ChartSerieProperties();
			gtp1FaktSerie.Color = "0-255-0";
			gtp1FaktSerie.Title = "ГТП-1 факт";
			gtp1FaktSerie.TagName = "gtp1Fakt";
			gtp1FaktSerie.LineWidth = 2;
			gtp1FaktSerie.SerieType = ChartSerieType.stepLine;
			gtp1FaktSerie.YAxisIndex = 0;
			gtp1FaktSerie.Enabled = true;

			ChartSerieProperties gtp2FaktSerie=new ChartSerieProperties();
			gtp2FaktSerie.Color = "0-0-255";
			gtp2FaktSerie.Title = "ГТП-2 факт";
			gtp2FaktSerie.TagName = "gtp2Fakt";
			gtp2FaktSerie.LineWidth = 2;
			gtp2FaktSerie.SerieType = ChartSerieType.stepLine;
			gtp2FaktSerie.YAxisIndex = 0;
			gtp2FaktSerie.Enabled = true;

			ChartSerieProperties gesFaktSerie=new ChartSerieProperties();
			gesFaktSerie.Color = "0-0-0";
			gesFaktSerie.Title = "ГЭС факт";
			gesFaktSerie.TagName = "gesFakt";
			gesFaktSerie.LineWidth = 2;
			gesFaktSerie.SerieType = ChartSerieType.stepLine;
			gesFaktSerie.YAxisIndex = 0;
			gesFaktSerie.Enabled = false;

			ChartSerieProperties gtp1PlanSerie=new ChartSerieProperties();
			gtp1PlanSerie.Color = "0-255-0";
			gtp1PlanSerie.Title = "ГТП-1 план";
			gtp1PlanSerie.TagName = "gtp1Plan";
			gtp1PlanSerie.LineWidth = 1;			
			gtp1PlanSerie.SerieType = steppedPBR?ChartSerieType.stepLine:ChartSerieType.line;			
			gtp1PlanSerie.YAxisIndex = 0;
			gtp1PlanSerie.Enabled = true;

			ChartSerieProperties gtp2PlanSerie=new ChartSerieProperties();
			gtp2PlanSerie.Color = "0-0-255";
			gtp2PlanSerie.Title = "ГТП-2 план";
			gtp2PlanSerie.TagName = "gtp2Plan";
			gtp2PlanSerie.LineWidth = 1;
			gtp2PlanSerie.SerieType = steppedPBR ? ChartSerieType.stepLine : ChartSerieType.line;
			gtp2PlanSerie.YAxisIndex = 0;
			gtp2PlanSerie.Enabled = true;

			ChartSerieProperties gesPlanSerie=new ChartSerieProperties();
			gesPlanSerie.Color = "0-0-0";
			gesPlanSerie.Title = "ГЭС план";
			gesPlanSerie.TagName = "gesPlan";
			gesPlanSerie.LineWidth = 1;
			gesPlanSerie.SerieType = steppedPBR ? ChartSerieType.stepLine : ChartSerieType.line;
			gesPlanSerie.YAxisIndex = 0;
			gesPlanSerie.Enabled = false;

			ChartSerieProperties vyrabFaktSerie=new ChartSerieProperties();
			vyrabFaktSerie.Color = "255-0-0";
			vyrabFaktSerie.Title = "Выработка факт";
			vyrabFaktSerie.TagName = "vyrabFakt";
			vyrabFaktSerie.LineWidth = 2;
			vyrabFaktSerie.SerieType = ChartSerieType.stepLine;
			vyrabFaktSerie.YAxisIndex = 1;
			vyrabFaktSerie.Enabled = true;

			ChartSerieProperties vyrabPlanSerie=new ChartSerieProperties();
			vyrabPlanSerie.Color = "255-0-0";
			vyrabPlanSerie.Title = "Выработка план";
			vyrabPlanSerie.TagName = "vyrabPlan";
			vyrabPlanSerie.LineWidth = 1;
			vyrabPlanSerie.SerieType = ChartSerieType.stepLine;
			vyrabPlanSerie.YAxisIndex = 1;
			vyrabPlanSerie.Enabled = true;

			props.addSerie(gtp1FaktSerie);
			props.addSerie(gtp2FaktSerie);
			props.addSerie(gesFaktSerie);
			props.addSerie(gtp1PlanSerie);
			props.addSerie(gtp2PlanSerie);
			props.addSerie(gesPlanSerie);
			props.addSerie(vyrabPlanSerie);
			props.addSerie(vyrabFaktSerie);


			return props;
		}
	}
}
