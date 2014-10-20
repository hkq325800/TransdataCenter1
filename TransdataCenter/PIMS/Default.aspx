<%@ Page Language="C#" MasterPageFile="~/PIMSSite.master" AutoEventWireup="true" CodeFile="Default.aspx.cs"
    Inherits="_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table style="width: 100%">
        <tr>
            <td valign="top" style="margin-right: 5px">
                <div>
                    <asp:Panel ID="ContentPanel" runat="server">
                    </asp:Panel>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
