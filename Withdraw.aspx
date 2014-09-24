<%@ Page Language="C#" MasterPageFile="~/MobileMaster.master" AutoEventWireup="true" CodeFile="Withdraw.aspx.cs"
    Inherits="Withdraw" Title="PTCD" %>

<asp:Content ID="myContent" runat="server" ContentPlaceHolderID="MainContentPlaceHolder">
    <div>
        <div id="subtitlediv">
            <h1 id="subtitle">
                Withdraw a Chemical</h1>
        </div>
        <div>
            <table id="datatable">
                <tr>
                    <td>
                        Chemical
                    </td>
                    <td>
                        <asp:Label ID="lblChemicalName" runat="server">[Chemical Name]</asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        Name
                    </td>
                    <td>
                        <asp:DropDownList class="fullCell" ID="ddlTeacherName" runat="server" CausesValidation="False">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        Amount
                    </td>
                    <td>
                        <asp:TextBox class="halfCell" ID="txtAmount" runat="server"></asp:TextBox>
                        &nbsp;<asp:DropDownList class="halfCell" ID="ddlUnits" runat="server">
                        </asp:DropDownList>
                        <br />
                        from
                        <asp:Label ID="lblCurrentAmount" runat="server" Text="[Current Amount]"></asp:Label>
                        <asp:Label ID="lblCurrentUnits" runat="server" Text="[Current Units]"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        Checksum
                    </td>
                    <td colspan="2">
                        <asp:TextBox class="fullCell" ID="txtChecksum" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Button class="fullCell bigButton" ID="btnSubmit" runat="server" Text="Submit"
                            OnClick="btnSubmit_Click" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2" style="color: #f55;">
                        <asp:Label ID="lblErrors" runat="server" Text="* Errors with your request: " Visible="False"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>