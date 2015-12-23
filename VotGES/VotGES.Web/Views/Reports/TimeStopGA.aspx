<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<VotGES.Web.Models.TimeStopGAAnswer>" %>
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
	<h1>Простой генераторов  <%=DateTime.Now.ToString("dd.MM.yyyy HH:mm") %> </h1>

    <h2>С начала года</h2>
    <table class='cifr'>
		<tr>
			<th >Параметр</th>
			<th >Генератор №1</th>
			<th >Генератор №2</th>
			<th >Генератор №3</th>
			<th >Генератор №4</th>
			<th >Генератор №5</th>
			<th >Генератор №6</th>
			<th >Генератор №7</th>
			<th >Генератор №8</th>
			<th >Генератор №9</th>
			<th >Генератор №10</th>
		</tr>
        <tr>
            <th>Простой</th>
            <%for (int ga=1;ga<=10;ga++){ %>
                <td>
                    <%=OgranGARecord.getDaysStr(Model.YearRecords[ga].timeStop) %>
                </td>
            <%} %>
        </tr>

        <tr>
            <th>Работа</th>
            <%for (int ga=1;ga<=10;ga++){ %>
                <td>
                    <%=OgranGARecord.getDaysStr(Model.YearRecords[ga].timeRun) %>
                </td>
            <%} %>
        </tr>

        <tr>
            <th>Пусков/Остановов</th>
            <%for (int ga=1;ga<=10;ga++){ %>
                <td>
                    <%=Model.YearRecords[ga].cntPusk %>/<%=Model.YearRecords[ga].cntStop %>
                </td>
            <%} %>
        </tr>
      
	</table>

    <h2>С начала месяца</h2>
     <table class='cifr'>
		<tr>
			<th >Параметр</th>
			<th >Генератор №1</th>
			<th >Генератор №2</th>
			<th >Генератор №3</th>
			<th >Генератор №4</th>
			<th >Генератор №5</th>
			<th >Генератор №6</th>
			<th >Генератор №7</th>
			<th >Генератор №8</th>
			<th >Генератор №9</th>
			<th >Генератор №10</th>
		</tr>
        <tr>
            <th>Простой</th>
            <%for (int ga=1;ga<=10;ga++){ %>
                <td>
                    <%=OgranGARecord.getDaysStr(Model.MonthRecords[ga].timeStop) %>
                </td>
            <%} %>
        </tr>

        <tr>
            <th>Работа</th>
            <%for (int ga=1;ga<=10;ga++){ %>
                <td>
                    <%=OgranGARecord.getDaysStr(Model.MonthRecords[ga].timeRun) %>
                </td>
            <%} %>
        </tr>

        <tr>
            <th>Пусков/Остановов</th>
            <%for (int ga=1;ga<=10;ga++){ %>
                <td>
                   <%=Model.MonthRecords[ga].cntPusk %>/<%=Model.MonthRecords[ga].cntStop %>
                </td>
            <%} %>
        </tr>      
	</table>

    <h2>Последние 7 дней</h2>
     <table class='cifr'>
		<tr>
			<th >Параметр</th>
			<th >Генератор №1</th>
			<th >Генератор №2</th>
			<th >Генератор №3</th>
			<th >Генератор №4</th>
			<th >Генератор №5</th>
			<th >Генератор №6</th>
			<th >Генератор №7</th>
			<th >Генератор №8</th>
			<th >Генератор №9</th>
			<th >Генератор №10</th>
		</tr>
        <tr>
            <th>Простой</th>
            <%for (int ga=1;ga<=10;ga++){ %>
                <td>
                    <%=OgranGARecord.getDaysStr(Model.WeekRecords[ga].timeStop) %>
                </td>
            <%} %>
        </tr>

        <tr>
            <th>Работа</th>
            <%for (int ga=1;ga<=10;ga++){ %>
                <td>
                    <%=OgranGARecord.getDaysStr(Model.WeekRecords[ga].timeRun) %>
                </td>
            <%} %>
        </tr>

        <tr>
            <th>Пусков/Остановов</th>
            <%for (int ga=1;ga<=10;ga++){ %>
                <td>
                   <%=Model.WeekRecords[ga].cntPusk %>/<%=Model.WeekRecords[ga].cntStop %>
                </td>
            <%} %>
        </tr>      
	</table>

</body>
</html>
