<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Database.aspx.cs" Inherits="Database" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Full Database</title>
    <link rel="stylesheet" href="styles/PrimaryStyleSheet.css" type="text/css" />
    <script type="text/javascript" src="scripts/jquery.js"></script>
    <script type="text/javascript" src="scripts/functions.js"></script>
    <script type="text/javascript" src="scripts/jquery.dataTables.js"></script>
    <script type="text/javascript" src="scripts/jquery.jeditable.js"></script>
    <!--<script type="text/javascript" src="scripts/jquery-ui.js"></script>-->
    <script type="text/javascript" src="scripts/jquery.validate.js"></script>
    <script type="text/javascript" src="scripts/jquery.dataTables.editable.js"></script>
    <style type="text/css" title="currentStyle">
        @import "styles/SecondaryStyleSheet.css";
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div id="wrapper">
        <div id="header">
            <span id="header-text">PARK TUDOR CHEMICAL DATABASE</span> <a href="Home.aspx" id="left-item"
                class="navi-item"><</a>
        </div>
        <div id="database">
            <div class="data-view-title">Full Database</div>
            <div id="data-view-toolbar">
                <asp:Button ID="btnAddEntry" runat="server" Text="Add Entry" Height="23px" Width="110px" onclick="btnAddEntry_Click" />&nbsp&nbsp&nbsp
                <asp:Button ID="btnEditTable" runat="server" Text="Edit Table" Height="23px" Width="110px" onclick="btnEditTable_Click" />
            </div>
            <asp:Table ID="dataTable3" runat="server" CellPadding="0" CellSpacing="0" border="0" class="display">
            </asp:Table>
        </div>
    </div>
    </form>
</body>
</html>
