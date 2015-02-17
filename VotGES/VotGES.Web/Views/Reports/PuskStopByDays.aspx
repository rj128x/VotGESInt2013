<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<VotGES.OgranGA.OgranGAReport>" %>
<%@ Import Namespace="VotGES.OgranGA" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>PuskStop</title>
	 <style>
	 	table,tr,td,p {
			font-family: 'Arial';
			font-size: 8pt;
		}
		
		@media print
		{
			table,tr,td,p {
				font-family: 'Arial';
				font-size: 7pt;
			}
		}
		
		h1,h2,h3,h4,h5,h6,hr
		{
			padding:0;
			margin:0;
		}
		
		
		h1
		{
			font-family: 'Arial';
			font-size: 10pt;
		}
		
		h2
		{
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
		
		table.cifr td{	
			text-align: right;
			white-space: nowrap;
			padding-left: 1px;
			padding-right: 1px;
			width:80px;
		}

		table.cifr th{
			text-align: center;			
			padding-left: 1px;			
			padding-right: 1px;
			white-space: nowrap;
		}
		
		
		table.cifr
		{
			margin-top:2px;
		}

		table td.right,table th.right{
			text-align: right;
		}	
	 </style>
</head>
<body>
	<h1>Работа генераторов с <%=Model.DateStart.ToString("dd.MM.yyyy HH:mm") %> по <%=Model.DateEnd.ToString("dd.MM.yyyy HH:mm") %></h1>
    <table class='cifr'>
		<tr>
			<th rowspan="2">Дата</th>
			<th colspan="3">Генератор №1</th>
			<th colspan="3">Генератор №2</th>
			<th colspan="3">Генератор №3</th>
			<th colspan="3">Генератор №4</th>
			<th colspan="3">Генератор №5</th>
			<th colspan="3">Генератор №6</th>
			<th colspan="3">Генератор №7</th>
			<th colspan="3">Генератор №8</th>
			<th colspan="3">Генератор №9</th>
			<th colspan="3">Генератор №10</th>
		</tr>
		<tr>
			<%for (int ga=1;ga<=10;ga++){ %>
				<th>
					Пуск/Стоп
				</th>

                <th>
					В работе 
				</th>

                 <th>
					Генерация
				</th>
			<%} %>
		</tr>
        <% foreach (DateTime date in Model.sumDataByDays.Keys){ %>
        <tr>
            <th><%=date.ToShortDateString() %></th>
            <%for (int ga=1;ga<=10;ga++){ %>
                <td>
                    <%= String.Format("{0}/{1}",
                        Model.sumDataByDays[date].sumData[ga].cntPusk,
                        Model.sumDataByDays[date].sumData[ga].cntStop)
                    %>
                </td>
                <td>
                    <%= String.Format("{0}",
                        OgranGARecord.getFullTimeStr(Model.sumDataByDays[date].sumData[ga].timeRun))
                    %>
                </td>
                <td>
                    <%= String.Format("{0}",
                         OgranGARecord.getFullTimeStr(Model.sumDataByDays[date].sumData[ga].timeGen))
                    %>
                </td>
            <%} %>
        </tr>
        <%} %>
	</table>
</body>
</html>
