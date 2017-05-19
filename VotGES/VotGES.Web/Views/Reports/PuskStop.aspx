<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<VotGES.OgranGA.OgranGAReport>" %>

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
    <%if (Model.IsKOTMI) { %>
        <h2>(Данные из КОТМИ)</h2>
    <%} %>
    <table class='cifr'>
		<tr>
			<th>Параметр</th>
			<th>Генератор №1</th>
			<th>Генератор №2</th>
			<th>Генератор №3</th>
			<th>Генератор №4</th>
			<th>Генератор №5</th>
			<th>Генератор №6</th>
			<th>Генератор №7</th>
			<th>Генератор №8</th>
			<th>Генератор №9</th>
			<th>Генератор №10</th>
			<th>Сумма</th>
		</tr>
		<tr>
			<th>Пусков</th>
			<%for (int ga=1;ga<=10;ga++){ %>
				<td>
					<%=Model.sumData[ga].cntPusk.ToString("0") %>
				</td>
			<%} %>
			<td>
				<%=Model.sumRecord.cntPusk.ToString("0") %> 
			</td>
		</tr>
		<tr>
			<th>Остановов</th>
			<%for (int ga=1;ga<=10;ga++){ %>
				<td>
					<%=Model.sumData[ga].cntStop.ToString("0") %>
				</td>
			<%} %>
			<td>
				<%=Model.sumRecord.cntStop.ToString("0") %>
			</td>
		</tr>

        <tr>
			<th>Переходов НОВЗР</th>
			<%for (int ga=1;ga<=10;ga++){ %>
				<td>
					<%=Model.sumData[ga].cntOgran.ToString("0") %>
				</td>
			<%} %>
			<td>
				<%=Model.sumRecord.cntOgran.ToString("0") %>
			</td>
		</tr>

         <tr>
			<th>Переходов ЗРР</th>
			<%for (int ga=1;ga<=10;ga++){ %>
				<td>
					<%=Model.sumData[ga].cntZapr.ToString("0") %>
				</td>
			<%} %>
			<td>
				<%=Model.sumRecord.cntZapr.ToString("0") %>
			</td>
		</tr>
			
        <%if (!Model.IsKOTMI) { %>
		<tr>
			<th>Время работы</th>
			<%for (int ga = 1; ga <= 10; ga++) { %>
				<td>
					<%=Model.sumData[ga].TimeRunStr %>
				</td>
			<%} %>
			<td>
				<%=Model.sumRecord.TimeRunStr %>
			</td>
		</tr>	
        <%} %>


        <tr>
			<th>Время генерации</th>
			<%for (int ga=1;ga<=10;ga++){ %>
				<td>
					<%=Model.sumData[ga].TimeGenStr %>
				</td>
			<%} %>
			<td>
				<%=Model.sumRecord.TimeGenStr %>
			</td>
		</tr>	
        
        <%if (!Model.IsKOTMI) { %>
        <tr>
			<th>Время СК</th>
			<%for (int ga=1;ga<=10;ga++){ %>
				<td>
					<%=Model.sumData[ga].TimeSKStr %>
				</td>
			<%} %>
			<td>
				<%=Model.sumRecord.TimeSKStr %>
			</td>
		</tr>
        <%} %>

        <%if (!Model.IsKOTMI) { %>
        <tr>
			<th>Время ХХТ</th>
			<%for (int ga=1;ga<=10;ga++){ %>
				<td>
					<%=Model.sumData[ga].TimeHHTStr %>
				</td>
			<%} %>
			<td>
				<%=Model.sumRecord.TimeHHTStr %>
			</td>
		</tr>	
        <%} %>

        <%if (!Model.IsKOTMI) { %>
        <tr>
			<th>Время ХХГ</th>
			<%for (int ga=1;ga<=10;ga++){ %>
				<td>
					<%=Model.sumData[ga].TimeHHGStr %>
				</td>
			<%} %>
			<td>
				<%=Model.sumRecord.TimeHHGStr %>
			</td>
		</tr>
        <%} %>

        <tr>
			<th>Время НОВЗР</th>
			<%for (int ga=1;ga<=10;ga++){ %>
				<td>
					<%=Model.sumData[ga].TimeOgranStr %>
				</td>
			<%} %>
			<td>
				<%=Model.sumRecord.TimeOgranStr %>
			</td>
		</tr>

        <tr>
			<th>Время ЗРР</th>
			<%for (int ga=1;ga<=10;ga++){ %>
				<td>
					<%=Model.sumData[ga].TimeZaprStr %>
				</td>
			<%} %>
			<td>
				<%=Model.sumRecord.TimeZaprStr %>
			</td>
		</tr>

        <%if (!Model.IsKOTMI) { %>
        <tr>
			<th>Время НПРЧ</th>
			<%for (int ga=1;ga<=10;ga++){ %>
				<td>
					<%=Model.sumData[ga].TimeNPRCHStr %>
				</td>
			<%} %>
			<td>
				<%=Model.sumRecord.TimeNPRCHStr %>
			</td>
		</tr>
        <%} %>

        <%if (!Model.IsKOTMI) { %>
         <tr>
			<th>Переходов в НПРЧ</th>
			<%for (int ga=1;ga<=10;ga++){ %>
				<td>
					<%=Model.sumData[ga].cntNPRCH.ToString() %>
				</td>
			<%} %>
			<td>
				<%=Model.sumRecord.cntNPRCH.ToString() %>
			</td>
		</tr>
        <%} %>

        <%if (!Model.IsKOTMI) { %>
        <tr>
			<th>Время ОПРЧ</th>
			<%for (int ga=1;ga<=10;ga++){ %>
				<td>
					<%=Model.sumData[ga].TimeOPRCHStr %>
				</td>
			<%} %>
			<td>
				<%=Model.sumRecord.TimeOPRCHStr %>
			</td>
		</tr>
        <%} %>

        <%if (!Model.IsKOTMI) { %>
        <tr>
			<th>Переходов в ОПРЧ</th>
			<%for (int ga=1;ga<=10;ga++){ %>
				<td>
					<%=Model.sumData[ga].cntOPRCH.ToString() %>
				</td>
			<%} %>
			<td>
				<%=Model.sumRecord.cntOPRCH.ToString() %>
			</td>
		</tr>
        <%} %>
        
        <%if (Model.IsKOTMI) { %>
        <tr>
			<th>Время АВРЧМ</th>
			<%for (int ga=1;ga<=10;ga++){ %>
				<td>
					<%=Model.sumData[ga].TimeAVRCHMStr %>
				</td>
			<%} %>
			<td>
				<%=Model.sumRecord.TimeAVRCHMStr %>
			</td>
		</tr>
        <%} %>

        <%if (Model.IsKOTMI) { %>
        <tr>
			<th>Переходов в АВРЧМ</th>
			<%for (int ga=1;ga<=10;ga++){ %>
				<td>
					<%=Model.sumData[ga].cntAVRCHM.ToString() %>
				</td>
			<%} %>
			<td>
				<%=Model.sumRecord.cntAVRCHM.ToString() %>
			</td>
		</tr>
        <tr>
			<th>АВРЧМ+</th>
			<%for (int ga=1;ga<=10;ga++){ %>
				<td>
					<%=Model.sumData[ga].posAVRCHM.ToString("0.00") %>
				</td>
			<%} %>
			<td>
				<%=Model.sumRecord.posAVRCHM.ToString("0.00") %>
			</td>
		</tr>
        <tr>
			<th>АВРЧМ-</th>
			<%for (int ga=1;ga<=10;ga++){ %>
				<td>
					<%=Model.sumData[ga].negAVRCHM.ToString("0.00") %>
				</td>
			<%} %>
			<td>
				<%=Model.sumRecord.negAVRCHM.ToString("0.00") %>
			</td>
		</tr>
        <%} %>


	</table>
</body>
</html>
