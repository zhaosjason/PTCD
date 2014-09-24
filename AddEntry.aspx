<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddEntry.aspx.cs" Inherits="AddEntry" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Add Entry</title>
    <link rel="stylesheet" href="styles/PrimaryStyleSheet.css" type="text/css" />
    <style type="text/css">
        .style1
        {
            width: 242px;
        }
        .style2
        {
            width: 118px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div id="add-wrapper">
        <div id="header">
            <span id="header-text">PARK TUDOR CHEMICAL DATABASE</span>
        </div>
        <div id="form-header">Add a New Chemical</div>
        <div id="form-wrapper">
            <table align="center" id="form-add-table" cellpadding="2">
                <tr>
                    <td align="right" class="style2">
                        <span class="field">Chemical Name:&nbsp;&nbsp; </span>
                    </td>
                    <td class="style1">
                        <span class="field">
                            <asp:TextBox ID="TextBox1" runat="server" Width="186px"></asp:TextBox></span>
                    </td>
                </tr>
                <tr>
                    <td align="right" class="style2">
                        <span class="field">Amount:&nbsp;</span>&nbsp;</td>
                    <td class="style1">
                        <span class="field">
                            <asp:TextBox ID="TextBox2" runat="server" Width="70px"></asp:TextBox>
                        &nbsp;
                        <asp:DropDownList ID="DropDownList1" runat="server" Width="58px">
                        </asp:DropDownList>
                        </span>
                    </td>
                </tr>
                <tr>
                    <td align="right" class="style2">
                        <span class="field">Concentration:</span>&nbsp;&nbsp;
                    </td>
                    <td class="style1">
                        <span class="field">
                            <asp:TextBox ID="TextBox4" runat="server" Width="70px"></asp:TextBox>&nbsp;M</span></td>
                </tr>
                <tr>
                    <td align="right" class="style2">
                        &nbsp;</td>
                    <td class="style1">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td align="right" class="style2">
                        Location:&nbsp;&nbsp; </td>
                    <td class="style1">
                        <asp:DropDownList ID="DropDownList2" runat="server" Height="20px" Width="200px">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="right" class="style2">
                        <span class="field">Class: </span>
                    &nbsp;</td>
                    <td class="style1">
                        <span class="field">
                            <asp:TextBox ID="TextBox5" runat="server" Width="130px"></asp:TextBox></span>
                    </td>
                </tr>
                <tr>
                    <td align="right" class="style2">
                        <span class="field">Form:&nbsp;&nbsp;</span></td>
                    <td class="style1">
                        <span class="field">
                            <asp:TextBox ID="TextBox6" runat="server" Width="130px"></asp:TextBox></span>
                    </td>
                </tr>
                <tr>
                    <td align="right" class="style2">
                        <span class="field">Family:</span>&nbsp;&nbsp;
                    </td>
                    <td class="style1">
                        <span class="field">
                            <asp:TextBox ID="TextBox7" runat="server" Width="130px"></asp:TextBox></span>
                    </td>
                </tr>
                <tr>
                    <td align="right" class="style2">
                        <span class="field">Hazard Code:</span>&nbsp;&nbsp;
                    </td>
                    <td class="style1">
                        <span class="field">
                            <asp:TextBox ID="TextBox8" runat="server" Width="130px"></asp:TextBox></span>
                    </td>
                </tr>
                <tr>
                    <td align="right" class="style2">
                        <span class="field">Disposal Code:</span>&nbsp;&nbsp;
                    </td>
                    <td class="style1">
                        <span class="field">
                            <asp:TextBox ID="TextBox9" runat="server" Width="130px"></asp:TextBox></span>
                    </td>
                </tr>
                <tr>
                    <td align="right" class="style2">
                        Date (optional):&nbsp;&nbsp;
                    </td>
                    <td class="style1">
                        <asp:TextBox ID="TextBox11" runat="server" Width="130px"></asp:TextBox>
&nbsp;(MM/DD/YYYY)</td>
                </tr>
                <tr>
                    <td align="right" class="style2">
                        &nbsp;</td>
                    <td class="style1">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td align="right" class="style2">
                        <span class="field">Checksum: </span>&nbsp;</td>
                    <td class="style1">
                        <span class="field">
                            <asp:TextBox ID="TextBox10" runat="server" Width="60px"></asp:TextBox>
                        &nbsp;(optional)</span></td>
                </tr>
            </table>
            <div id="label-add-bar">
            <asp:Label ID="Label1" runat="server" Text=" "></asp:Label>
            </div>
            <div id="button-add-bar">
                <asp:Button ID="btnSubmit" runat="server" Text="Submit" onclick="btnSubmit_Click" Height="25px" Width="95px" /> &nbsp&nbsp&nbsp
                <asp:Button ID="btnCancel" runat="server" Text="Cancel" onclick="btnCancel_Click" Height="25px" Width="95px" />
            </div>
        </div>
    </div>
    </form>
</body>
</html>
