<%@ Page Title="" Language="C#" MasterPageFile="~/dental-master.master" AutoEventWireup="true" CodeFile="products.aspx.cs" Inherits="products" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
    .auto-style10 {
        width: 58px;
    }
    .auto-style11 {
        width: 7%;
    }
</style>
</asp:Content>
<asp:Content ID="Content2" runat="server" contentplaceholderid="PLACE_MASTER">
    <asp:Panel ID="PANEL_MASTER" runat="server">
<div>
            &nbsp;<asp:Label ID="LBL_ID" runat="server" Text="ID"></asp:Label>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;<asp:TextBox ID="TXT_ID" runat="server"></asp:TextBox>
            <br />
            <br />
            &nbsp;<asp:Label ID="Label2" runat="server" Text="الاسم"></asp:Label>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="TXT_NAME" runat="server"></asp:TextBox>
            <br />
            <br />
            <asp:Label ID="Label3" runat="server" Text="صنع فى"></asp:Label>
            &nbsp;&nbsp;
            <asp:TextBox ID="TXT_MADE_IN" runat="server"></asp:TextBox>
            &nbsp;<br /> &nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <br />
            <br />
            <asp:Label ID="Label8" runat="server" Text="التصنيف"></asp:Label>
            &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp;<asp:DropDownList ID="DRP_CATEGORY" runat="server" Height="24px" Width="113px" AutoPostBack="True" OnSelectedIndexChanged="DRP_CATEGORY_SelectedIndexChanged" OnLoad="DRP_TYPE_Load" OnTextChanged="DRP_TYPE_TextChanged">
            </asp:DropDownList>
&nbsp;&nbsp;&nbsp;
            <asp:LinkButton ID="LINK_CATEGORY_NEW" runat="server" OnClick="LINK_CATEGORY_NEW_Click">جديد</asp:LinkButton>
            &nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="TXT_CATEGORY_ID" runat="server" OnTextChanged="TXT_CATEGORY_ID_TextChanged" Visible="False"></asp:TextBox>
            &nbsp;&nbsp;&nbsp;<asp:TextBox ID="TXT_CATEGORY_Name" runat="server" Visible="False"></asp:TextBox>
            &nbsp;&nbsp;&nbsp;
            <asp:LinkButton ID="LINK_CATEGORY_SUBMIT" runat="server" OnClick="LINK_CATEGORY_SUBMIT_Click" Visible="False">حفظ</asp:LinkButton>
            &nbsp;&nbsp;&nbsp;
            <asp:LinkButton ID="LINK_CATEGORY_CANCEL" runat="server" OnClick="LINK_CATEGORY_CANCEL_Click" Visible="False">الغاء</asp:LinkButton>
            &nbsp;&nbsp;&nbsp;<asp:LinkButton ID="LINK_CATEGORY_EDITE_DELETE" runat="server" OnClick="LINK_CATEGORY_EDITE_DELETE_Click" Visible="true">تعديل/مسح</asp:LinkButton>
            <br />
            <br />
            <asp:Label ID="Label4" runat="server" Text="النوع"></asp:Label>
            &nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp;&nbsp;
            <asp:DropDownList ID="DRP_TYPE" runat="server" AutoPostBack="True" Height="24px" Width="113px" OnLoad="DRP_TYPE_Load" OnSelectedIndexChanged="DRP_TYPE_SelectedIndexChanged" OnTextChanged="DRP_TYPE_TextChanged">
            </asp:DropDownList>
            &nbsp;&nbsp;&nbsp;
           <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LINK_TYPE_NEW_Click">جديد</asp:LinkButton>
&nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="TXT_TYPE_ID" runat="server" Visible="False" OnTextChanged="TXT_TYPE_ID_TextChanged"></asp:TextBox>
            &nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="TXT_TYPE_Name" runat="server" Visible="False"></asp:TextBox>
            &nbsp;&nbsp;&nbsp;
            <asp:LinkButton ID="LINK_TYPE_SUBMIT" runat="server" Visible="False" OnClick="LINK_TYPE_SUBMIT_Click">حفظ</asp:LinkButton>
&nbsp;&nbsp;&nbsp;
            <asp:LinkButton ID="LINK_TYPE_CANCEL" runat="server" Visible="False" OnClick="LINK_TYPE_CANCEL_Click">الغاء</asp:LinkButton>
            &nbsp;&nbsp;&nbsp;<asp:LinkButton ID="LINK_TYPE_EDITE_DELETE" runat="server" Visible="true" OnClick="LINK_TYPE_EDITE_DELETE_Click">تعديل/مسح</asp:LinkButton>
            <br />
            <br />

            <asp:Label ID="Label12" runat="server" Text="الحجم"></asp:Label>
            &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp;&nbsp;&nbsp; 
            <asp:DropDownList ID="DRP_SIZE" runat="server" Height="24px" Width="113px" AutoPostBack="True" OnSelectedIndexChanged="DRP_SIZE_SelectedIndexChanged" OnLoad="DRP_SIZE_Load" OnTextChanged="DRP_SIZE_TextChanged" OnInit="DRP_SIZE_Init">
            </asp:DropDownList>
            &nbsp;&nbsp;&nbsp;&nbsp;<asp:LinkButton ID="LINK_SIZE_NEW" runat="server" OnClick="LINK_SIZE_NEW_Click">جديد</asp:LinkButton>
&nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="TXT_SIZE_ID" runat="server" OnTextChanged="TXT_SIZE_ID_TextChanged" Visible="false"></asp:TextBox>
            &nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="TXT_SIZE_NAME" runat="server" OnTextChanged="TXT_SIZE_NAME_TextChanged" Visible="false"></asp:TextBox>
            &nbsp;&nbsp;&nbsp;
            <asp:LinkButton ID="LINK_SIZE_SUBMIT" runat="server" Visible="false" OnClick="LINK_SIZE_SUBMIT_Click">حفظ</asp:LinkButton>
&nbsp;&nbsp;&nbsp;
            <asp:LinkButton ID="LINK_SIZE_CANCEL" runat="server" Visible="false" OnClick="LINK_SIZE_CANCEL_Click">الغاء</asp:LinkButton>
            &nbsp;&nbsp;&nbsp;<asp:LinkButton ID="LINK_SIZE_EDITE_DELETE" runat="server" Visible="true" OnClick="LINK_SIZE_EDITE_DELETE_Click">تعديل/مسح</asp:LinkButton>
            <br />
            <br />
            <asp:Label ID="Label1" runat="server" Text="اللون"></asp:Label>
            &nbsp; &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp; &nbsp; &nbsp;<asp:DropDownList ID="DRP_COLOR" runat="server" Height="24px" Width="113px">
            </asp:DropDownList>
            &nbsp;&nbsp;&nbsp;&nbsp;<asp:LinkButton ID="LINK_COLOR_NEW" runat="server" OnClick="LINK_COLOR_NEW_Click">جديد</asp:LinkButton>
&nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="TXT_COLOR_ID" runat="server" OnTextChanged="TXT_COLOR_ID_TextChanged" Visible="false"></asp:TextBox>
            &nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="TXT_COLOR_NAME" runat="server" OnTextChanged="TXT_COLOR_NAME_TextChanged" Visible="false"></asp:TextBox>
            &nbsp;&nbsp;&nbsp;
            <asp:LinkButton ID="LINK_COLOR_SUBMIT" runat="server" Visible="false" OnClick="LINK_COLOR_SUBMIT_Click">حفظ</asp:LinkButton>
&nbsp;&nbsp;&nbsp;
            <asp:LinkButton ID="LINK_COLOR_CANCEL" runat="server" Visible="false" OnClick="LINK_COLOR_CANCEL_Click">الغاء</asp:LinkButton>
            &nbsp;&nbsp;&nbsp;<asp:LinkButton ID="LINK_COLOR_EDITE_DETELTE" runat="server" Visible="true" OnClick="LINK_COLOR_EDITE_DETELTE_Click">تعديل/مسح</asp:LinkButton>
            <br />
            <br />
            &nbsp;<asp:CheckBox ID="CHK_FXD" runat="server" Text="ثابت" />
            &nbsp;<asp:CheckBox ID="CHK_DYNMIC" runat="server" Text="متحرك" OnCheckedChanged="CHK_DYNMIC_CheckedChanged" />

            <br />
            <br />
            <table class="auto-style11">
                <tr>
                    <td class="auto-style10">
                        <asp:Button ID="Button1" runat="server" Text="حفظ" OnClick="Button1_Click" Width="89px" />
                    </td>
                </tr>
            </table>
            <br />

            <asp:GridView ID="GridView1" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" OnRowDeleting="GridView1_RowDeleting">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:CommandField ShowDeleteButton="True" CancelText="الغاء" DeleteText="مسح" EditText="تعديل" UpdateText="تعديل" />
                    <asp:CommandField ShowEditButton="True" CancelText="الغاء" DeleteText="مسح" EditText="تعديل" UpdateText="تعديل" />
                </Columns>
                <EditRowStyle BackColor="#2461BF" />
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                <RowStyle BackColor="#EFF3FB" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <SortedAscendingCellStyle BackColor="#F5F7FB" />
                <SortedAscendingHeaderStyle BackColor="#6D95E1" />
                <SortedDescendingCellStyle BackColor="#E9EBEF" />
                <SortedDescendingHeaderStyle BackColor="#4870BE" />
            </asp:GridView>
        </div>
                        </asp:Panel>
                    </asp:Content>


