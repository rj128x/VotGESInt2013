<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<SortedList<int, OgranGAAnswer>>" %>

<%@ Import Namespace="VotGES.Web.Models" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>PuskStop</title>
    <style>
        table, tr, td, p {
            font-family: 'Arial';
            font-size: 8pt;
        }

        @media print {
            table, tr, td, p {
                font-family: 'Arial';
                font-size: 7pt;
            }
        }

        h1, h2, h3, h4, h5, h6, hr {
            padding: 0;
            margin: 0;
        }


        h1 {
            font-family: 'Arial';
            font-size: 10pt;
        }

        h2 {
            font-family: 'Arial';
            font-size: 8pt;
        }

        table {
            border-collapse: collapse;
        }

        td, th {
            border-width: 1px;
            border-style: solid;
            border-color: #BBBBFF;
            padding-left: 3px;
            padding-right: 3px;
        }

        table.cifr td {
            text-align: right;
            white-space: nowrap;
            padding-left: 1px;
            padding-right: 1px;
            width: 80px;
        }

        table.cifr th {
            text-align: center;
            padding-left: 1px;
            padding-right: 1px;
            white-space: nowrap;
        }


        table.cifr {
            margin-top: 2px;
        }

        table td.right, table th.right {
            text-align: right;
        }
    </style>
</head>
<body>
    <table class='cifr'>
        <tr>
            <th rowspan="2">Параметр</th>
            <th colspan="3">Работа в сети</th>
            <th colspan="3">ТТ турбины</th>
            <th colspan="3">ХХ генератора</th>
            <th colspan="3">НОВЗР</th>
            <th colspan="3">Пусков</th>
            <th colspan="3">Остановов</th>
        </tr>
        <tr>
            <th>С начала суток</th>
            <th>С начала года</th>
            <th>С кап ремонта</th>

            <th>С начала суток</th>
            <th>С начала года</th>
            <th>С кап ремонта</th>

            <th>С начала суток</th>
            <th>С начала года</th>
            <th>С кап ремонта</th>

            <th>С начала суток</th>
            <th>С начала года</th>
            <th>С кап ремонта</th>

            <th>С начала суток</th>
            <th>С начала года</th>
            <th>С кап ремонта</th>

            <th>С начала суток</th>
            <th>С начала года</th>
            <th>С кап ремонта</th>

        </tr>
        <%for (int ga = 1; ga <= 10; ga++) { %>
        <tr>
            <th>ГА-<%=ga %></th>

            <td><%=Model[ga].DayStartRecord.TimeRunStr %></td>
            <td><%=Model[ga].YearStartRecord.TimeRunStr %></td>
            <td><%=Model[ga].KRRecord.TimeRunStr %></td>

            <td><%=Model[ga].DayStartRecord.TimeHHTStr %></td>
            <td><%=Model[ga].YearStartRecord.TimeHHTStr %></td>
            <td><%=Model[ga].KRRecord.TimeHHTStr %></td>

            <td><%=Model[ga].DayStartRecord.TimeHHGStr %></td>
            <td><%=Model[ga].YearStartRecord.TimeHHGStr %></td>
            <td><%=Model[ga].KRRecord.TimeHHGStr %></td>

            <td><%=Model[ga].DayStartRecord.TimeOgranStr %></td>
            <td><%=Model[ga].YearStartRecord.TimeOgranStr %></td>
            <td><%=Model[ga].KRRecord.TimeOgranStr %></td>

            <td><%=Model[ga].DayStartRecord.cntPusk.ToString("0") %></td>
            <td><%=Model[ga].YearStartRecord.cntPusk.ToString("0") %></td>
            <td><%=Model[ga].KRRecord.cntPusk.ToString("0") %></td>

            <td><%=Model[ga].DayStartRecord.cntStop.ToString("0") %></td>
            <td><%=Model[ga].YearStartRecord.cntStop.ToString("0") %></td>
            <td><%=Model[ga].KRRecord.cntStop.ToString("0") %></td>


        </tr>
        <%} %>
    </table>
</body>
</html>
