﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VotGES.Chart
{
	public class ChartAnswer
	{
		public ChartData Data { get; set; }
		public ChartProperties Properties { get; set; }
		public bool AllowZoom { get; set; }
		public bool AllowTrack { get; set; }
		public string Title { get; set; }

		public ChartAnswer() {
			AllowZoom = true;
			AllowTrack = true;
		}
		public void processAxes() {
			foreach (ChartAxisProperties ax in Properties.Axes) {
				if (ax.Auto && ax.ProcessAuto) {
					double min=double.NaN;
					double max=double.NaN;
					foreach (ChartSerieProperties serie in Properties.Series) {
						if (serie.YAxisIndex == ax.Index) {
							ChartDataSerie serieData=Data.getSerie(serie.TagName);
							foreach (ChartDataPoint point in serieData.Points) {
								max = (point.YVal > max || double.IsNaN(max)) ? point.YVal : max;
								min = (point.YVal < min || double.IsNaN(min)) ? point.YVal : min;
							}
						}
					}
					if (!double.IsNaN(max) && !double.IsNaN(min)) {
						double height=Math.Abs(max - min);
						if (ax.MinHeight > height) {
							ax.Auto = false;
							ax.Min = min - ax.Interval;
							ax.Max = ax.Min + ax.MinHeight + ax.Interval;
						}
					}
				}
			}
		}

		public static ChartAnswer getEmptyAnswer() {
			ChartAnswer answer = new ChartAnswer();
			answer.Data = new ChartData();
			ChartProperties props = new ChartProperties();
			props.XAxisType = XAxisTypeEnum.numeric;
			props.XValueFormatString = "0.##";
			ChartAxisProperties yAx = new ChartAxisProperties();
			yAx.Auto = true;
			answer.Properties = props;
			props.addAxis(yAx);
			return answer;
		}
	}
}
