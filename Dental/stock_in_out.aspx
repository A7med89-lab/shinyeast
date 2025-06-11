<%@ Page Title="" Language="C#" MasterPageFile="~/dental-master.master" AutoEventWireup="true" CodeFile="stock_in_out.aspx.cs" Inherits="stock_in_out" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" runat="server" contentplaceholderid="PLACE_MASTER">
    <asp:Panel ID="PANEL_MASTER" runat="server">
    <br />
    <asp:Label ID="Label1" runat="server" Text="اوامر التوريد"></asp:Label>
    <dev>
        <br />
        <br />
        <table class="auto-style10">
                <tr>        
                    <td>
                        <asp:DropDownList ID="DRP_SUPP_FILTER" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DRP_SUPP_FILTER_SelectedIndexChanged"></asp:DropDownList>
                    </td>                    
                    <td>
                        <asp:TextBox ID="TXT_DATE_FILTER" runat="server" AutoPostBack="True" TextMode="Date" OnTextChanged="TXT_DATE_FILTER_TextChanged"></asp:TextBox>
                    </td>                    
                    <td>
                        <asp:DropDownList ID="DRP_INV_FILTER" runat="server" AutoPostBack="true" OnSelectedIndexChanged="DRP_INV_FILTER_SelectedIndexChanged"> </asp:DropDownList>
                    </td>
                    <td>     
                        <asp:TextBox ID="TXT_ORDER_FILTER" runat="server" AutoPostBack="true"></asp:TextBox>
                    </td>
                    <td>
                        <asp:DropDownList ID="DRP_STATUS_FILTER" runat="server" AutoPostBack="True" OnSelectedIndexChanged="DRP_STATUS_FILTER_SelectedIndexChanged"></asp:DropDownList>
                    </td>
                    <td>
                        &nbsp;</td>
                </tr>
            </table>
    </dev>
    <asp:GridView ID="PURCHASE_GRD" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" OnSelectedIndexChanged="sales_grid_SelectedIndexChanged" AutoGenerateColumns="False">
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:TemplateField HeaderText="امر التوريد">
                <ItemTemplate>
                    <asp:Label ID="Label3" runat="server" Text='<%# Bind("id") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="التاريخ">
                 <ItemTemplate>
                    <asp:Label ID="Label4" runat="server" Text='<%# Bind("date") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="الوقت">
                <ItemTemplate>
                    <asp:Label ID="Label5" runat="server" Text='<%# Bind("time") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="امر البيع">
                <ItemTemplate>
                    <asp:Label ID="Label6" runat="server" Text='<%# Bind("purchase_id") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="المخزن">
                <ItemTemplate>
                    <asp:Label ID="Label7" runat="server" Text='<%# Bind("inv_name") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="اجمالى الكمية">
                <ItemTemplate>
                    <asp:Label ID="Label8" runat="server" Text='<%# Bind("quantity") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="المورد">
                <ItemTemplate>
                    <asp:Label ID="Labe19" runat="server" Text='<%# Bind("supplier_name") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="الحالة (قبول\رفض)">
                <ItemTemplate>
                    <asp:Label ID="Labe20" runat="server" Text='<%# Bind("status") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
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
    <br />
    <br />
    <asp:Label ID="Label2" runat="server" Text="اوامر الصرف"></asp:Label>
    <br />
    <br />
    <asp:GridView ID="SALES_GRD" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None">
        <AlternatingRowStyle BackColor="White" />
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
</asp:Panel>
</asp:Content>


