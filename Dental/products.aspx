<%@ Page Title="" Language="C#" MasterPageFile="~/dental-master.master" AutoEventWireup="true" CodeFile="products.aspx.cs" Inherits="products" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
    .auto-style10 {
        width: 58px;
    }
    .auto-style11 {
        width: 10%;
    }
</style>
</asp:Content>
<asp:Content ID="Content2" runat="server" contentplaceholderid="PLACE_MASTER">
    <asp:Panel ID="PANEL_MASTER" runat="server">
<div>
            <asp:Label ID="LBL_ID" runat="server" Text="ID"></asp:Label>
            &nbsp;&nbsp &nbsp&nbsp&nbsp &nbsp &nbsp &nbsp&nbsp; &nbsp <asp:TextBox ID="TXT_ID" runat="server"></asp:TextBox>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Label ID="Label3" runat="server" Text="Made In"></asp:Label>
            &nbsp;&nbsp; &nbsp; &nbsp;
            <asp:TextBox ID="TXT_MADE_IN" runat="server"></asp:TextBox>
            
            <br />
            <br />
            <asp:Label ID="Label2" runat="server" Text="Name"></asp:Label>
            &nbsp;&nbsp &nbsp&nbsp&nbsp &nbsp&nbsp&nbsp; <asp:TextBox ID="TXT_NAME" runat="server"></asp:TextBox>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <br />
            <br />
            <asp:Label ID="Label8" runat="server" Text="Category"></asp:Label>
            &nbsp;&nbsp; &nbsp;&nbsp;<asp:DropDownList ID="DRP_CATEGORY" runat="server" Height="19px" Width="93px" AutoPostBack="True" OnSelectedIndexChanged="DRP_CATEGORY_SelectedIndexChanged" OnLoad="DRP_TYPE_Load" OnTextChanged="DRP_TYPE_TextChanged">
                
            </asp:DropDownList>
&nbsp;&nbsp;&nbsp;
           <asp:LinkButton ID="LINK_CATEGORY_NEW" runat="server" OnClick="LINK_CATEGORY_NEW_Click">New</asp:LinkButton>
&nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="TXT_CATEGORY_ID" runat="server" Visible="False" OnTextChanged="TXT_CATEGORY_ID_TextChanged"></asp:TextBox>
            &nbsp;&nbsp;&nbsp;<asp:TextBox ID="TXT_CATEGORY_Name" runat="server" Visible="False"></asp:TextBox>
            
            &nbsp;&nbsp;&nbsp;
            <asp:LinkButton ID="LINK_CATEGORY_SUBMIT" runat="server" Visible="False" OnClick="LINK_CATEGORY_SUBMIT_Click">Submit</asp:LinkButton>
&nbsp;&nbsp;&nbsp;
            <asp:LinkButton ID="LINK_CATEGORY_CANCEL" runat="server" Visible="False" OnClick="LINK_CATEGORY_CANCEL_Click">Cancel</asp:LinkButton>
            &nbsp;&nbsp;&nbsp;<asp:LinkButton ID="LINK_CATEGORY_EDITE_DELETE" runat="server" Visible="true" OnClick="LINK_CATEGORY_EDITE_DELETE_Click">Edite/Delete</asp:LinkButton>
            <br />
            <br />
            <asp:Label ID="Label4" runat="server" Text="Type"></asp:Label>
            &nbsp;&nbsp &nbsp&nbsp&nbsp&nbsp; &nbsp&nbsp&nbsp;
            <asp:DropDownList ID="DRP_TYPE" runat="server" Height="19px" Width="93px" AutoPostBack="True" OnSelectedIndexChanged="DRP_TYPE_SelectedIndexChanged" OnLoad="DRP_TYPE_Load" OnTextChanged="DRP_TYPE_TextChanged">
            </asp:DropDownList>
&nbsp;&nbsp;&nbsp;
           <asp:LinkButton ID="LinkButton1" runat="server" OnClick="LINK_TYPE_NEW_Click">New</asp:LinkButton>
&nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="TXT_TYPE_ID" runat="server" Visible="False" OnTextChanged="TXT_TYPE_ID_TextChanged"></asp:TextBox>
            &nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="TXT_TYPE_Name" runat="server" Visible="False"></asp:TextBox>
            &nbsp;&nbsp;&nbsp;
            <asp:LinkButton ID="LINK_TYPE_SUBMIT" runat="server" Visible="False" OnClick="LINK_TYPE_SUBMIT_Click">Submit</asp:LinkButton>
&nbsp;&nbsp;&nbsp;
            <asp:LinkButton ID="LINK_TYPE_CANCEL" runat="server" Visible="False" OnClick="LINK_TYPE_CANCEL_Click">Cancel</asp:LinkButton>
            &nbsp;&nbsp;&nbsp;<asp:LinkButton ID="LINK_TYPE_EDITE_DELETE" runat="server" Visible="true" OnClick="LINK_TYPE_EDITE_DELETE_Click">Edite/Delete</asp:LinkButton>
            <br />
            <br />

            <asp:Label ID="Label12" runat="server" Text="Size"></asp:Label>
            &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp;&nbsp;&nbsp; 
            <asp:DropDownList ID="DRP_SIZE" runat="server" Height="19px" Width="93px" AutoPostBack="True" OnSelectedIndexChanged="DRP_SIZE_SelectedIndexChanged" OnLoad="DRP_SIZE_Load" OnTextChanged="DRP_SIZE_TextChanged" OnInit="DRP_SIZE_Init">
            </asp:DropDownList>
            &nbsp;&nbsp;&nbsp;&nbsp;<asp:LinkButton ID="LINK_SIZE_NEW" runat="server" OnClick="LINK_SIZE_NEW_Click">New</asp:LinkButton>
&nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="TXT_SIZE_ID" runat="server" OnTextChanged="TXT_SIZE_ID_TextChanged" Visible="false"></asp:TextBox>
            &nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="TXT_SIZE_NAME" runat="server" OnTextChanged="TXT_SIZE_NAME_TextChanged" Visible="false"></asp:TextBox>
            &nbsp;&nbsp;&nbsp;
            <asp:LinkButton ID="LINK_SIZE_SUBMIT" runat="server" Visible="false" OnClick="LINK_SIZE_SUBMIT_Click">Submit</asp:LinkButton>
&nbsp;&nbsp;&nbsp;
            <asp:LinkButton ID="LINK_SIZE_CANCEL" runat="server" Visible="false" OnClick="LINK_SIZE_CANCEL_Click">Cancel</asp:LinkButton>
            &nbsp;&nbsp;&nbsp;<asp:LinkButton ID="LINK_SIZE_EDITE_DELETE" runat="server" Visible="true" OnClick="LINK_SIZE_EDITE_DELETE_Click">Edite/Delete</asp:LinkButton>
            <br />
            <br />
            <asp:Label ID="Label1" runat="server" Text="Colors"></asp:Label>
            &nbsp; &nbsp;&nbsp;&nbsp; &nbsp; &nbsp;<asp:DropDownList ID="DRP_COLOR" runat="server" Height="19px" Width="93px">
            </asp:DropDownList>
            &nbsp;&nbsp;&nbsp;&nbsp;<asp:LinkButton ID="LINK_COLOR_NEW" runat="server" OnClick="LINK_COLOR_NEW_Click">New</asp:LinkButton>
&nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="TXT_COLOR_ID" runat="server" OnTextChanged="TXT_COLOR_ID_TextChanged" Visible="false"></asp:TextBox>
            &nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="TXT_COLOR_NAME" runat="server" OnTextChanged="TXT_COLOR_NAME_TextChanged" Visible="false"></asp:TextBox>
            &nbsp;&nbsp;&nbsp;
            <asp:LinkButton ID="LINK_COLOR_SUBMIT" runat="server" Visible="false" OnClick="LINK_COLOR_SUBMIT_Click">Submit</asp:LinkButton>
&nbsp;&nbsp;&nbsp;
            <asp:LinkButton ID="LINK_COLOR_CANCEL" runat="server" Visible="false" OnClick="LINK_COLOR_CANCEL_Click">Cancel</asp:LinkButton>
            &nbsp;&nbsp;&nbsp;<asp:LinkButton ID="LINK_COLOR_EDITE_DETELTE" runat="server" Visible="true" OnClick="LINK_COLOR_EDITE_DETELTE_Click">Edite/Delete</asp:LinkButton>
            <br />
            <br />
            &nbsp;<asp:CheckBox ID="CHK_FXD" runat="server" Text="Fixed" />
            &nbsp;<asp:CheckBox ID="CHK_DYNMIC" runat="server" Text="Removable" OnCheckedChanged="CHK_DYNMIC_CheckedChanged" />

            <br />
            <br />
            <table class="auto-style11">
                <tr>
                    <td class="auto-style10">
                        <asp:Button ID="Button1" runat="server" Text="Submit" OnClick="Button1_Click" style="height: 26px" />
                    </td>
                    
                    <td class="auto-style5">
                         <asp:Button ID="Button4" runat="server" Text="Dispaly" OnClick="Button4_Click"/>
                    </td>
                </tr>
            </table>
            <br />

            <asp:GridView ID="GridView1" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" OnRowDeleting="GridView1_RowDeleting">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:CommandField ShowDeleteButton="True" />
                    <asp:CommandField ShowEditButton="True" />
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


