<%@ Page Language="C#" MasterPageFile="~/dental-master.master" AutoEventWireup="true" CodeFile="login.aspx.cs" Inherits="login" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .auto-style10 {
            width: 66px;
        }
        .auto-style11 {
            width: 167px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" runat="server" contentplaceholderid="PLACE_MASTER">
    <asp:Panel ID="PANEL_MASTER" runat="server">
        <dev>
            <br />
            <asp:Label ID="Label1" runat="server" Text="Username"></asp:Label>
            &nbsp;
            <asp:TextBox ID="TXT_USERNAME" runat="server"></asp:TextBox>
            <br />
 
            <br />
            <asp:Label ID="Label2" runat="server" Text="Password"></asp:Label>
            &nbsp;&nbsp;

            <asp:TextBox ID="TXT_PASSWORD" runat="server" TextMode="Password"></asp:TextBox>
            <br />
            
            &nbsp;&nbsp;&nbsp;
            
            <table class="auto-style1" style="width: 10%">
                <tr>
                    <td class="auto-style10">
                        <asp:Button ID="BTN_LOGIN" runat="server" Text="Login" OnClick="BTN_LOGIN_Click"  />
                    </td>
                    
                    <td>
                        <asp:Button ID="Button3" runat="server" Text="Sign Up" />
                    </td>

                </tr>
            </table>
            <br />
            
        </dev>
</asp:Panel>
</asp:Content>
