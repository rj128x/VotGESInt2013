<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<KotmiLib.KotmiResult>" %>

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
	<h1>Данные КОТМИ с <%=Model.DateStart.ToString("dd.MM.yyyy HH:mm") %> по <%=Model.DateEnd.ToString("dd.MM.yyyy HH:mm") %></h1>

    <table class='cifr'>
		<tr>
			<th>Параметр</th>
            <%foreach (KotmiLib.ArcField field in Model.Fields) {  %>
			<th><%=field.Name %></th>			
            <%} %>
		</tr>
        <%foreach (DateTime date in Model.Dates) { %>
        <tr>
            <th><%=date.ToString("dd.MM.yyyy HH:mm:ss") %></th>
            <%foreach (KotmiLib.ArcField field in Model.Fields) {  %>
                <th><%=Model.Values[field][date].ToString("0.00") %></th>
            <%} %>
        </tr>
        <%} %>
		

	</table>
</body>
</html>
