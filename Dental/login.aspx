<%@ Page Language="C#" AutoEventWireup="true" CodeFile="login.aspx.cs" Inherits="login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 22%;
        }
        .auto-style2 {
            width: 12px;
        }
        .auto-style3 {
            width: 31px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>

            <br />

            <asp:Label ID="Label1" runat="server" Text="Username"></asp:Label>
&nbsp;
            <asp:TextBox ID="TXT_USERNAME" runat="server"></asp:TextBox>
&nbsp;&nbsp;&nbsp;
            <asp:Label ID="Label2" runat="server" Text="Password"></asp:Label>
            &nbsp;
            <asp:TextBox ID="TXT_PASSWORD" runat="server" TextMode="Password"></asp:TextBox>
            <br />
            <br />
            <table class="auto-style1">
                <tr>
                    <td class="auto-style2">
                        <asp:Button ID="Button1" runat="server" Text="Login" OnClick="Button1_Click" />
                    </td>
                    <td class="auto-style3">
                        <asp:Button ID="Button2" runat="server" Text="Change Password" />
                    </td>
                    <td>
                        <asp:Button ID="Button3" runat="server" Text="Sign Up" OnClick="Button3_Click" />
                    </td>
                </tr>
            </table>
            <br />
            
        </div>
    </form>
</body>
</html>
