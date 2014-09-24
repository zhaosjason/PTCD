<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Settings.aspx.cs" Inherits="Settings" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Settings</title>
    <link rel="stylesheet" href="styles/PrimaryStyleSheet.css" type="text/css" />
    <script src="scripts/jquery-1.9.1.js"></script>
    <script src="scripts/jquery-ui-1.10.3.custom.js"></script>
    <style type="text/css">
        .style3
        {
            width: 81px;
            text-align: right;
        }
        .style6
        {
            width: 97px;
        }
        .style7
        {
            width: 200px;
        }
        .style8
        {
            width: 97px;
            height: 22px;
            text-align: right;
        }
        .style9
        {
            width: 200px;
            height: 22px;
        }
        .style10
        {
            width: 97px;
            font-size: medium;
        }
        .style11
        {
            width: 97px;
            text-align: right;
        }
        .style12
        {
            width: 215px;
        }
        .style13
        {
            width: 81px;
            text-align: right;
            height: 5px;
        }
        .style14
        {
            width: 215px;
            height: 5px;
        }
    </style>
    <script>
        $(function () {
            $("#accordion").accordion({
                heightStyle: "content",
                event: "click hoverintent"
            });
        });

        $.event.special.hoverintent = {
            setup: function () {
                $(this).bind("mouseover", jQuery.event.special.hoverintent.handler);
            },
            teardown: function () {
                $(this).unbind("mouseover", jQuery.event.special.hoverintent.handler);
            },
            handler: function (event) {
                var currentX, currentY, timeout,
        args = arguments,
        target = $(event.target),
        previousX = event.pageX,
        previousY = event.pageY;

                function track(event) {
                    currentX = event.pageX;
                    currentY = event.pageY;
                };

                function clear() {
                    target
          .unbind("mousemove", track)
          .unbind("mouseout", clear);
                    clearTimeout(timeout);
                }

                function handler() {
                    var prop,
          orig = event;

                    if ((Math.abs(previousX - currentX) +
            Math.abs(previousY - currentY)) < 7) {
                        clear();

                        event = $.Event("hoverintent");
                        for (prop in orig) {
                            if (!(prop in event)) {
                                event[prop] = orig[prop];
                            }
                        }
                        // Prevent accessing the original event since the new event
                        // is fired asynchronously and the old event is no longer
                        // usable (#6028)
                        delete event.originalEvent;

                        target.trigger(event);
                    } else {
                        previousX = currentX;
                        previousY = currentY;
                        timeout = setTimeout(handler, 100);
                    }
                }

                timeout = setTimeout(handler, 100);
                target.bind({
                    mousemove: track,
                    mouseout: clear
                });
            }
        };
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div id="add-wrapper">
        <div id="header">
            <span id="header-text">PARK TUDOR CHEMICAL DATABASE</span>
        </div>
        <div id="form-settings-header">
            Settings</div>
        <div id="form-settings-wrapper">
            <div id="accordion">
                <h3 class="accordion-header">
                    Database Settings</h3>
                <div class="accordion-body">
                    <span class="add-delete-label">Delete</span>
                    <div>
                        <table class="add-delete-table">
                            <tr>
                                <td class="style13">
                                </td>
                                <td class="style14">
                                </td>
                            </tr>
                            <tr>
                                <td class="style3">
                                    Teachers:&nbsp;&nbsp;
                                </td>
                                <td class="style12">
                                    <asp:DropDownList ID="DropDownList1" runat="server" Width="180px">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td class="style3">
                                    Location:&nbsp;&nbsp;
                                </td>
                                <td class="style12">
                                    <asp:DropDownList ID="DropDownList2" runat="server" Width="200px" Height="20px">
                                    </asp:DropDownList>
                            </tr>
                        </table>
                        <br />
                        &nbsp&nbsp&nbsp&nbsp&nbsp<asp:Button ID="btnDelete" 
                            runat="server" Text="Delete"
                            OnClick="btnDelete_Click" Height="25px" Width="95px" 
                            CausesValidation="False" />
                        <br />
                        <br />
                    </div>
                    <span class="add-delete-label">Add</span>
                    <div>
                        <table class="add-delete-table">
                            <tr>
                                <td class="style10">
                                    Teacher:
                                </td>
                                <td class="style7">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td class="style11">
                                    First Name:&nbsp;&nbsp;
                                </td>
                                <td class="style7">
                                    <asp:TextBox ID="TextBox1" runat="server" Width="140px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="style11">
                                    Last Name:&nbsp;&nbsp;
                                </td>
                                <td class="style7">
                                    <asp:TextBox ID="TextBox2" runat="server" Width="140px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="style11">
                                    Email Address:&nbsp;&nbsp;
                                </td>
                                <td class="style7">
                                    <asp:TextBox ID="TextBox4" runat="server" Width="170px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="style6">
                                    &nbsp;
                                </td>
                                <td class="style7">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td class="style10">
                                    Location:
                                </td>
                                <td class="style7">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td class="style8">
                                    Name:&nbsp;&nbsp;
                                </td>
                                <td class="style9">
                                    <asp:TextBox ID="TextBox3" runat="server" Width="180px"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                        <br />
                        &nbsp&nbsp&nbsp&nbsp&nbsp<asp:Button ID="btnAdd" 
                            runat="server" Text="Add" Height="25px"
                            Width="95px" OnClick="btnAdd_Click" CausesValidation="False" />
                        <br />
                        <div id="label-bar">
                            <asp:Label ID="Label1" runat="server" ForeColor="Red"></asp:Label>
                        </div>
                    </div>
                </div>
                <h3 class="accordion-header">
                    Administrative Settings</h3>
                <div class="accordion-body">
                    <span class="add-delete-label">Change Password</span>
                    <table class="add-delete-table">
                        <tr>
                            <td style="height: 5px;">
                            </td>
                            <td style="height: 5px;">
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right">
                                Current Password:&nbsp;&nbsp;
                            </td>
                            <td>
                                <asp:TextBox ID="tbCurrPass" runat="server" TextMode="Password"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvcurrPass" runat="server" ControlToValidate="tbCurrPass"
                                    ErrorMessage="Current password is required." ForeColor="White">*</asp:RequiredFieldValidator>
                                <asp:CompareValidator ID="cvCurrpass" runat="server" ControlToValidate="tbCurrPass"
                                    ErrorMessage="Current password is invalid." ForeColor="White" EnableClientScript="False">*</asp:CompareValidator>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right">
                                New Password:&nbsp;&nbsp;
                            </td>
                            <td>
                                <asp:TextBox ID="tbNewPass" runat="server" TextMode="Password"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvNewPass" runat="server" ControlToValidate="tbNewPass"
                                    ErrorMessage="New password is required." ForeColor="White">*</asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: right">
                                &nbsp; Confirm New Password:&nbsp;&nbsp;
                            </td>
                            <td>
                                <asp:TextBox ID="tbConfirmPass" runat="server" TextMode="Password"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvConfirm" runat="server" ControlToValidate="tbConfirmPass"
                                    ErrorMessage="Confirm new password is required." ForeColor="White">*</asp:RequiredFieldValidator>
                                <asp:CompareValidator ID="cvNewPass" runat="server" ControlToCompare="tbNewPass"
                                    ControlToValidate="tbConfirmPass" ErrorMessage="Confirm password must match new password."
                                    ForeColor="White">*</asp:CompareValidator>
                            </td>
                        </tr>
                    </table>
                        <br />&nbsp&nbsp&nbsp&nbsp&nbsp<asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click"
                            Text="Submit" Height="25px" Width="95px" />
                        <asp:ValidationSummary ID="vs1" style="margin: 3px 0px 0px 25px;" runat="server" ForeColor="Red" Width="225px" />
                        <asp:Label ID="lblConfirm" runat="server" ForeColor="Green"></asp:Label>
                </div>
            </div>
            <div id="button-bar">
                <asp:Button ID="btnDone" runat="server" Text="Return Home" OnClick="btnDone_Click"
                    Height="25px" Width="125px" CausesValidation="False" />
            </div>
        </div>
    </div>
    </form>
</body>
</html>
