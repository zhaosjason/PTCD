<%@ Page Language="C#" MasterPageFile="~/MobileMaster.master" AutoEventWireup="true" CodeFile="Confirm.aspx.cs" Inherits="Confirm" Title="PTCD" %>

<asp:Content id="myContent" runat="server" ContentPlaceholderId="MainContentPlaceHolder">
    <div>
        <div id="subtitlediv">
            <h1 id="subtitle">Confirm Your Request</h1>
        </div>

        <div>
            <table id="datatable">
                <tr>
                    <td>
                        <asp:Label ID="lblTeacherName" runat="server" Text="[Teacher Name]"></asp:Label>
                        withdrawing
                    </td>
                </tr>

                <tr>
                    <td>
                        <asp:Label ID="lblAmount" runat="server" Text="[Amount]"></asp:Label>
                        <asp:Label ID="lblUnits" runat="server" Text="[Units]"></asp:Label>
                        of
                    </td>
                </tr>

                <tr>
                    <td>
                        <asp:Label ID="lblChemicalName" runat="server" Text="[Chemical Name]"></asp:Label>
                    </td>
                </tr>

                <tr>
                    <td>
                        <asp:Button class="fullCell bigButton" ID="btnGoBack" runat="server" Text="Go Back" 
                            onclick="btnGoBack_Click" />
                    </td>
                </tr>

                <tr>
                    <td>
                        <asp:Button class="fullCell bigButton" ID="btnSubmit" runat="server" Text="Confirm" 
                            onclick="btnSubmit_Click" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>