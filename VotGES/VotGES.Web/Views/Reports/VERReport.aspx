<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<VotGES.Piramida.Report.VERReport>" ContentType="application/vnd.ms-excel; charset=utf-8"%>
<%@ Import Namespace="VotGES.Piramida.Report" %>
<%@ Import Namespace="VotGES.Piramida" %>
<%@ Import Namespace="VotGES" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">    
	 <style>
	 	table,tr,td,p {
			font-family: 'Arial';
			font-size: 8pt;
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
			width:80px;
		}
		
		
		table.cifr
		{
			margin-top:5px;
		}

		table td.right,table th.right{
			text-align: right;
		}	
		
		table td.fill, table th.fill{
			background-color:Gray;
		}	
	 </style>
</head>
<body>
	<table class='cifr'>
		<tr>
			<th rowspan='2'>Время</th>
			<th rowspan='2'>ВБ</th>
            <th rowspan='2'>НБ</th>
            <th colspan="5">ГА-1</th>
            <th colspan="5">ГА-2</th>
            <th colspan="5">ГА-3</th>
            <th colspan="5">ГА-4</th>
            <th colspan="5">ГА-5</th>
            <th colspan="5">ГА-6</th>
            <th colspan="5">ГА-7</th>
            <th colspan="5">ГА-8</th>
            <th colspan="5">ГА-9</th>
            <th colspan="5">ГА-10</th>
		</tr>
		<tr>
			<%for (int ga=1;ga<=10;ga++){ %>		
                <th>Открытие НА</th>
                <th>Угол РК</th>
                <th>Мощность</th>
                <th>Напор</th>
                <th>Расход</th>
            <%} %>
		</tr>
		
		<%foreach (DateTime date in Model.Dates) { %>
			<tr>
				<th><%=date.ToString("HH:mm")%></th>
                <td><%=Model[date,PiramidaRecords.MBW_VB.Key].ToString("0.00")%></td>
                <td><%=Model[date,PiramidaRecords.MBW_NB.Key].ToString("0.00")%></td>

                <td><%=Model[date,PiramidaRecords.MBW_GA1_OtkrNA.Key].ToString("0.00")%></td>
                <td><%=Model[date,PiramidaRecords.MBW_GA1_UgolRK.Key].ToString("0.00")%></td>
                <td><%=Model[date,PiramidaRecords.MBW_GA1_P.Key].ToString("0.00")%></td>
                <td><%=Model[date,PiramidaRecords.MBW_GA1_Napor.Key].ToString("0.00")%></td>
                <td><%=Model[date,PiramidaRecords.MBW_GA1_Rash.Key].ToString("0.00")%></td>

                <td><%=Model[date,PiramidaRecords.MBW_GA2_OtkrNA.Key].ToString("0.00")%></td>
                <td><%=Model[date,PiramidaRecords.MBW_GA2_UgolRK.Key].ToString("0.00")%></td>
                <td><%=Model[date,PiramidaRecords.MBW_GA2_P.Key].ToString("0.00")%></td>
                <td><%=Model[date,PiramidaRecords.MBW_GA2_Napor.Key].ToString("0.00")%></td>
                <td><%=Model[date,PiramidaRecords.MBW_GA2_Rash.Key].ToString("0.00")%></td>

                <td><%=Model[date,PiramidaRecords.MBW_GA3_OtkrNA.Key].ToString("0.00")%></td>
                <td><%=Model[date,PiramidaRecords.MBW_GA3_UgolRK.Key].ToString("0.00")%></td>
                <td><%=Model[date,PiramidaRecords.MBW_GA3_P.Key].ToString("0.00")%></td>
                <td><%=Model[date,PiramidaRecords.MBW_GA3_Napor.Key].ToString("0.00")%></td>
                <td><%=Model[date,PiramidaRecords.MBW_GA3_Rash.Key].ToString("0.00")%></td>

                <td><%=Model[date,PiramidaRecords.MBW_GA4_OtkrNA.Key].ToString("0.00")%></td>
                <td><%=Model[date,PiramidaRecords.MBW_GA4_UgolRK.Key].ToString("0.00")%></td>
                <td><%=Model[date,PiramidaRecords.MBW_GA4_P.Key].ToString("0.00")%></td>
                <td><%=Model[date,PiramidaRecords.MBW_GA4_Napor.Key].ToString("0.00")%></td>
                <td><%=Model[date,PiramidaRecords.MBW_GA4_Rash.Key].ToString("0.00")%></td>

                <td><%=Model[date,PiramidaRecords.MBW_GA5_OtkrNA.Key].ToString("0.00")%></td>
                <td><%=Model[date,PiramidaRecords.MBW_GA5_UgolRK.Key].ToString("0.00")%></td>
                <td><%=Model[date,PiramidaRecords.MBW_GA5_P.Key].ToString("0.00")%></td>
                <td><%=Model[date,PiramidaRecords.MBW_GA5_Napor.Key].ToString("0.00")%></td>
                <td><%=Model[date,PiramidaRecords.MBW_GA5_Rash.Key].ToString("0.00")%></td>

                <td><%=Model[date,PiramidaRecords.MBW_GA6_OtkrNA.Key].ToString("0.00")%></td>
                <td><%=Model[date,PiramidaRecords.MBW_GA6_UgolRK.Key].ToString("0.00")%></td>
                <td><%=Model[date,PiramidaRecords.MBW_GA6_P.Key].ToString("0.00")%></td>
                <td><%=Model[date,PiramidaRecords.MBW_GA6_Napor.Key].ToString("0.00")%></td>
                <td><%=Model[date,PiramidaRecords.MBW_GA6_Rash.Key].ToString("0.00")%></td>

                <td><%=Model[date,PiramidaRecords.MBW_GA7_OtkrNA.Key].ToString("0.00")%></td>
                <td><%=Model[date,PiramidaRecords.MBW_GA7_UgolRK.Key].ToString("0.00")%></td>
                <td><%=Model[date,PiramidaRecords.MBW_GA7_P.Key].ToString("0.00")%></td>
                <td><%=Model[date,PiramidaRecords.MBW_GA7_Napor.Key].ToString("0.00")%></td>
                <td><%=Model[date,PiramidaRecords.MBW_GA7_Rash.Key].ToString("0.00")%></td>

                <td><%=Model[date,PiramidaRecords.MBW_GA8_OtkrNA.Key].ToString("0.00")%></td>
                <td><%=Model[date,PiramidaRecords.MBW_GA8_UgolRK.Key].ToString("0.00")%></td>
                <td><%=Model[date,PiramidaRecords.MBW_GA8_P.Key].ToString("0.00")%></td>
                <td><%=Model[date,PiramidaRecords.MBW_GA8_Napor.Key].ToString("0.00")%></td>
                <td><%=Model[date,PiramidaRecords.MBW_GA8_Rash.Key].ToString("0.00")%></td>

                <td><%=Model[date,PiramidaRecords.MBW_GA9_OtkrNA.Key].ToString("0.00")%></td>
                <td><%=Model[date,PiramidaRecords.MBW_GA9_UgolRK.Key].ToString("0.00")%></td>
                <td><%=Model[date,PiramidaRecords.MBW_GA9_P.Key].ToString("0.00")%></td>
                <td><%=Model[date,PiramidaRecords.MBW_GA9_Napor.Key].ToString("0.00")%></td>
                <td><%=Model[date,PiramidaRecords.MBW_GA9_Rash.Key].ToString("0.00")%></td>

                <td><%=Model[date,PiramidaRecords.MBW_GA10_OtkrNA.Key].ToString("0.00")%></td>
                <td><%=Model[date,PiramidaRecords.MBW_GA10_UgolRK.Key].ToString("0.00")%></td>
                <td><%=Model[date,PiramidaRecords.MBW_GA10_P.Key].ToString("0.00")%></td>
                <td><%=Model[date,PiramidaRecords.MBW_GA10_Napor.Key].ToString("0.00")%></td>
                <td><%=Model[date,PiramidaRecords.MBW_GA10_Rash.Key].ToString("0.00")%></td>

			</tr>
		<%} %>
	</table>
</body>
</html>
