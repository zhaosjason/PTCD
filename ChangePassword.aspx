<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ChangePassword.aspx.cs" Inherits="ChangePassword" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>PTCD</title>
    <link href='http://fonts.googleapis.com/css?family=Marvel' rel='stylesheet' type='text/css' />
    <link href="styles/Reset.css" rel="stylesheet" type="text/css" />
    <link href="styles/PasswordTheme.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div id="wrapper">
        <span id="title">Change Administrator Password</span>

        <table id="changePasswordTable">
            <tr>
                <td>
                    Current Password:
                </td>

                <td>
                    <asp:TextBox ID="tbCurrPass" runat="server" TextMode="Password"></asp:TextBox>
        
                    <asp:RequiredFieldValidator ID="rfvcurrPass" runat="server" 
                        ControlToValidate="tbCurrPass" ErrorMessage="Current password is required." 
                        ForeColor="White">*</asp:RequiredFieldValidator>
        
                    <asp:CompareValidator ID="cvCurrpass" runat="server" 
                        ControlToValidate="tbCurrPass" ErrorMessage="Current password is invalid." 
                        ForeColor="White" EnableClientScript="False">*</asp:CompareValidator>
                </td>
            </tr>
            
            <tr>
                <td>
                    New Password:
                </td>

                <td>
                    <asp:TextBox ID="tbNewPass" runat="server" TextMode="Password"></asp:TextBox>
                    
                    <asp:RequiredFieldValidator ID="rfvNewPass" runat="server" 
                        ControlToValidate="tbNewPass" ErrorMessage="New password is required." 
                        ForeColor="White">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            
            <tr>
                <td>
                    Confirm New Password:
                </td>

                <td>
                    <asp:TextBox ID="tbConfirmPass" runat="server" 
                        TextMode="Password"></asp:TextBox>

                    <asp:RequiredFieldValidator ID="rfvConfirm" runat="server" 
                        ControlToValidate="tbConfirmPass" 
                        ErrorMessage="Confirm new password is required." ForeColor="White">*</asp:RequiredFieldValidator>
                    
                    <asp:CompareValidator ID="cvNewPass" runat="server" 
                        ControlToCompare="tbNewPass" ControlToValidate="tbConfirmPass" 
                        ErrorMessage="Confirm password must match new password." ForeColor="White">*</asp:CompareValidator>
                </td>
            </tr>
            
            <tr>
                <td colspan="2">
                    <asp:ValidationSummary ID="vs1" runat="server" ForeColor="Red" />
                    
                    <asp:Label ID="lblConfirm" runat="server" ForeColor="Green"></asp:Label>
                </td>
            </tr>

            <tr>
                <td colspan="2">
                    <asp:Button ID="btnSubmit" runat="server" onclick="btnSubmit_Click" 
                        Text="Submit" />
                    
                    <asp:Button ID="btnClear" runat="server" CausesValidation="False" 
                        onclick="btnClear_Click" Text="Clear" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
