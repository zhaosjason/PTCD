<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Authentication.aspx.cs" Inherits="Authentication" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Park Tudor Chemical Database</title>
    <link rel="stylesheet" href="styles/PrimaryStyleSheet.css" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div id="input-wrapper">
        <div id="input-header">Park Tudor Chemical Database</div>
        <div id="input">
            <div id="input-subtitle">Authentication</div>
            <div id="input-description">You are currently trying to access the PTCD secure webpage.  Please enter the site password to proceed:</div>
            <div id="input-fields">
                <asp:TextBox ID="authenticateText" runat="server" TextMode="Password" required></asp:TextBox>&nbsp;
                <asp:Button ID="authenticateButton" runat="server" Text="Submit" Width="68px" Height="22px"
                    onclick="authenticateButton_Click" />
            </div>
        </div>
    </div>
    </form>
</body>
</html>
