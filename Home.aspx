<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Home.aspx.cs" Inherits="Home" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Park Tudor Chemical Database</title>
    <link rel="stylesheet" href="styles/PrimaryStyleSheet.css" type="text/css" />
    <script type="text/javascript" src="scripts/jquery.js"></script>
    <script type="text/javascript" src="scripts/functions.js"></script>
    <script type="text/javascript" src="scripts/jquery.dataTables.js"></script>
    <style type="text/css" title="currentStyle">
        @import "styles/SecondaryStyleSheet.css";
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div id="wrapper">
        <div id="header">
            <span id="header-text">PARK TUDOR CHEMICAL DATABASE</span>
            <a href="About.aspx" id="left-item" class="navi-item"><</a>
            <a href="Database.aspx" id="right-item" class="navi-item">></a>
        </div>
        <div id="inner-wrapper">
            <div id="right-wrapper">
              <div id="intro">
                  Welcome!  You are currently on the home page.  To navigate to other pages, please use the navigation bar above or the quick-links below.  The tables on your left list recent changes to the database and chemical entries that require attention.
              </div>
              <div id="navi-button-bar">
                <table id="button-bar-table">
                    <tr>
                        <td><asp:ImageButton ID="ImageButton1" ToolTip="Database" CssClass="navi-button-item" runat="server" ImageUrl="~/images/database.PNG" PostBackUrl="~/Database.aspx" /></td>
                      <td><asp:ImageButton ID="ImageButton2" ToolTip="Labeling" CssClass="navi-button-item" runat="server" ImageUrl="~/images/grid.PNG" PostBackUrl="~/Labeling.aspx"/></td>
                    </tr>
                    <tr>
                       <td><asp:ImageButton ID="ImageButton3" ToolTip="Settings" CssClass="navi-button-item" runat="server" ImageUrl="~/images/settings.PNG" PostBackUrl="~/Settings.aspx"/></td>
                        <td><asp:ImageButton ID="ImageButton4" ToolTip="About" CssClass="navi-button-item" runat="server" ImageUrl="~/images/question_mark.PNG" PostBackUrl="~/About.aspx"/></td>
                    </tr>
                </table>
              <!--
                <div style="width: 100%;">
                    <asp:ImageButton ID="btnImage1" CssClass="navi-button-ite" runat="server" style="float:right; width: 49%; background-color: rgba(0, 0, 0, 0.6);" ImageUrl="images/database.PNG" PostBackUrl="Database.aspx"/>
                    <asp:ImageButton ID="btnImage2" CssClass="navi-button-ite" runat="server" style="float:left; width: 49%; background-color: rgba(0, 0, 0, 0.6);" ImageUrl="images/grid.PNG" PostBackUrl="Labeling.aspx"/>
                </div>
                <div style="padding-top: 10px; width: 100%;">
                    <asp:ImageButton ID="btnImage3" CssClass="navi-button-ite" runat="server" style="float:right; width: 49%; background-color: rgba(0, 0, 0, 0.6);" ImageUrl="images/settings.PNG" PostBackUrl="Settings.aspx"/>
                    <asp:ImageButton ID="btnImage4" CssClass="navi-button-ite" runat="server" style="float:left; width: 49%; background-color: rgba(0, 0, 0, 0.6);" ImageUrl="images/question_mark.PNG" PostBackUrl="About.aspx"/>
                </div>-->
              </div>
            </div>
            <div id="data-view1">
                <div class="data-view-title">Priority Chemicals</div>
                <asp:Table ID="dataTable1" runat="server" cellpadding="0" cellspacing="0" border="0" class="display">
                </asp:Table>
            </div>
            <div id="data-view2">
                <div class="data-view-title">Recent Changes</div>
                <asp:Table ID="dataTable2" runat="server" cellpadding="0" cellspacing="0" border="0" class="display">
                </asp:Table>
            </div>
        </div>
    </div>
    </form>
</body>
</html>
