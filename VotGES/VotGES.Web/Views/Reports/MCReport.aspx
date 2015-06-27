<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage<VotGES.ModesCentre.MCServerReader>" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Выгрузка данных</title>
</head>
<body>
	<h1>Выгрузка данных из ModesCentre за <%=Model.Date %></h1>
    <%foreach (string str in Model.LogInfo){ %>
    <h2><%= str%></h2>
    <%} %>
</body>
</html>
