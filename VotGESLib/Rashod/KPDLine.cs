using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VotGES.Chart;

namespace VotGES.Rashod
{
    public class KPDLine
    {
        protected static ChartProperties getChartPropertiesKPDS(int[] kpd,bool ogran)
        {
            ChartProperties props = new ChartProperties();
            props.XAxisType = XAxisTypeEnum.numeric;
            props.XValueFormatString = "0.##";
            ChartAxisProperties yAx = new ChartAxisProperties();
            yAx.Auto = true;
            yAx.Index = 0;

            for (int index = 0; index < kpd.Length; index++)
            {
                string post = kpd[index].ToString();
                ChartSerieProperties serie = new ChartSerieProperties();
                serie.Color = ChartColor.GetColorStr(index);
                serie.LineWidth = 0;
                serie.SerieType = ChartSerieType.kpdLine;
                serie.Marker = true;
                serie.LineWidth = -1;
                serie.Title = "КПД " + post;
                serie.TagName = "kpd_" + post;
                serie.Enabled = true;
                serie.YAxisIndex = 0;
                props.addSerie(serie);
            }

            if (ogran)
            {
                ChartSerieProperties ser = new ChartSerieProperties();
                ser.Color = ChartColor.GetColorStr(System.Drawing.Color.Red);
                ser.LineWidth = 3;
                ser.SerieType = ChartSerieType.line;
                ser.Title = "Ограничения макс";
                ser.TagName = "ogrMax";
                ser.Enabled = true;
                ser.YAxisIndex = 0;
                props.addSerie(ser);

                ser = new ChartSerieProperties();
                ser.Color = ChartColor.GetColorStr(System.Drawing.Color.Red);
                ser.LineWidth = 3;
                ser.SerieType = ChartSerieType.line;
                ser.Title = "Ограничения мин";
                ser.TagName = "ogrMin";
                ser.Enabled = true;
                ser.YAxisIndex = 0;
                props.addSerie(ser);
            }

            props.addAxis(yAx);
            return props;
        }

        public static ChartAnswer createKPDTable(int ga)
        {
            ChartAnswer answer = new ChartAnswer();
            int[] kpds = { 75, 80, 85, 90 };
            RashodTable table = RashodTable.getRashodTable(ga);

            answer.Properties = getChartPropertiesKPDS(kpds,(ga>=1&&ga<=10));
            answer.Data = new ChartData();

            Dictionary<int, ChartDataSerie> dataSeries = new Dictionary<int, ChartDataSerie>();
            foreach (int kpd in kpds)
            {
                ChartDataSerie data = new ChartDataSerie();
                data.Name = "kpd_" + kpd;
                dataSeries.Add(kpd, data);
            }

            for (double power = table.minPower; power <= table.maxPower; power += table.stepPower)
            {
                for (double napor = table.minNapor; napor < table.maxNapor; napor += table.stepNapor)
                {
                    double rashod = RashodTable.getRashod(ga, power, napor);
                    double kpd = RashodTable.KPD(power, napor, rashod);
                    int kpdInt = (int)(kpd * 100);
                    if (kpds.Contains(kpdInt))
                    {
                        dataSeries[kpdInt].Points.Add(new ChartDataPoint(power, napor));
                    }
                }
            }

            foreach (int kpd in kpds)
            {
                answer.Data.addSerie(dataSeries[kpd]);
            }

            if (ga >= 1 && ga <= 10)
            {
                ChartDataSerie ogranMin = new ChartDataSerie();
                ogranMin.Name = "ogrMin";

                ChartDataSerie ogranMax = new ChartDataSerie();
                ogranMax.Name = "ogrMax";

                foreach (KeyValuePair<double, double> de in OgranLineTable.OgranData[ga])
                {
                    ogranMax.Points.Add(new ChartDataPoint(de.Value, de.Key));
                    ogranMin.Points.Add(new ChartDataPoint(35, de.Key));
                }
                answer.Data.addSerie(ogranMin);
                answer.Data.addSerie(ogranMax);
            }



            return answer;
        }
    }
}
