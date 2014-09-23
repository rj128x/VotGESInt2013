using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VotGES.Chart;

namespace VotGES.Rashod
{
    public class KPDLine
    {
        protected static ChartProperties getChartPropertiesKPDS(int[] kpd)
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
                serie.Color = index == kpd.Length - 1 ? "0-0-0" : ChartColor.NextColor();
                serie.LineWidth = 0;
                serie.SerieType = ChartSerieType.area;
                serie.Marker = true;
                serie.LineWidth = -1;
                serie.Title = "КПД " + post;
                serie.TagName = "kpd_" + post;
                serie.Enabled = true;
                serie.YAxisIndex = 0;
                props.addSerie(serie);
            }
            props.addAxis(yAx);
            return props;
        }

        public static ChartAnswer createKPDTable(int ga)
        {
            ChartAnswer answer=new ChartAnswer();
            int[] kpds={83,84,85,86};
            RashodTable table = RashodTable.getRashodTable(ga);

            answer.Properties = getChartPropertiesKPDS(kpds);
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
                    double rashod=RashodTable.getRashod(ga,power,napor);
                    double kpd = RashodTable.KPD(power, napor, rashod);
                    int kpdInt = (int)Math.Round(kpd*100);
                    if (kpds.Contains(kpdInt))
                    {
                        dataSeries[kpdInt].Points.Add(new ChartDataPoint(power,napor));
                    }
                }
            }

            foreach (int kpd in kpds)
            {
                answer.Data.addSerie(dataSeries[kpd]);
            }
            return answer;
        }
    }
}
