<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<VotGES.Piramida.Report.VERReportMonth>" ContentType="application/vnd.ms-excel; charset=utf-8"%>
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
			<th>Время</th>
			<th>Т нар. воздуха</th>
            <th>Т щит. сооружений</th>

		</tr>
		
		<%foreach (DateTime date in Model.Dates) {
        if (date.Hour == 0 || date.Hour == 8 || date.Hour == 16) {%>
			<tr>
				<th><%=date.ToString("dd.MM.yyyy HH:mm")%></th>
                <td><%=Model[date, PiramidaRecords.MBW_Temp.Key].ToString("0.00")%></td>
                <td><%=Model[date, PiramidaRecords.MBW_TempShit.Key].ToString("0.00")%></td>    

			</tr>
		<%}
    } %>
	</table>
</body>
</html>
