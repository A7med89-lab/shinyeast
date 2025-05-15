<%@ Page Title="" Language="C#" MasterPageFile="~/dental-master.master" AutoEventWireup="true" CodeFile="stock_purchases.aspx.cs" Inherits="stock_purchases" %>

<script runat="server">

    protected void TXT_Date_TextChanged(object sender, EventArgs e)
    {

    }
</script>


<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" runat="server" contentplaceholderid="PLACE_MASTER">
    <asp:Panel ID="PANEL_MASTER" runat="server">
        <dev>
            <br />
            <asp:Label ID="Label1" runat="server" Text="التاريخ" ></asp:Label>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="TXT_Date" runat="server" TextMode="Date" AutoPostBack="true" OnTextChanged="TXT_Date_TextChanged"></asp:TextBox>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;

            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <br />
            <br />
            <asp:Label ID="Label2" runat="server" Text="ID"></asp:Label>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;
            <asp:TextBox ID="TXT_ID" runat="server" ></asp:TextBox>
            <br />
            <br />
            <asp:GridView ID="GRD_STOCK_IN" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" AutoGenerateColumns="False" OnRowCommand="GRD_STOCK_IN_RowCommand" OnRowDeleted="GRD_STOCK_IN_RowDeleted" OnRowDeleting="GRD_STOCK_IN_RowDeleting" OnSelectedIndexChanged="GRD_STOCK_IN_SelectedIndexChanged">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:TemplateField HeaderText="رقم امر الشراء">
                        <ItemTemplate>
                            <asp:Label ID="LBL_PURCHASE_ID_GRD" runat="server" Text='<%# Bind("id") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="التاريخ">
                        <ItemTemplate>
                            <asp:Label ID="LBL_DATE_GRD" runat="server" Text='<%# Bind("date") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="المورد">
                        <ItemTemplate>
                            <asp:Label ID="LBL_SUPP_NAME" runat="server" Text='<%# Bind("supplier_name") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="رقم المورد" Visible="False">
                         <ItemTemplate>
                             <asp:Label ID="LBL_SUPP_ID" runat="server" Text='<%# Bind("supplier_id") %>'></asp:Label>
                         </ItemTemplate>
                     </asp:TemplateField>
                    <asp:TemplateField HeaderText="اجمالى الكمية">
                        <ItemTemplate>
                            <asp:Label ID="LBL_SUM_QTY_GRD" runat="server" Text='<%# Bind("sum_qty") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="المخزن">
                        <ItemTemplate>
                            <asp:DropDownList ID="DRP_INV_GRD" runat="server" DataSourceID="inv" DataTextField="name" DataValueField="id">
                            </asp:DropDownList>
                            <asp:SqlDataSource ID="inv" runat="server" ConnectionString="<%$ ConnectionStrings:DentalConnectionString2 %>" SelectCommand="SELECT [id], [name] FROM [inventories]"></asp:SqlDataSource>
                            <asp:SqlDataSource ID="SqlDataSource1" runat="server"></asp:SqlDataSource>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="اسم المستخدم">
                        <ItemTemplate>
                            <asp:Label ID="LBL_USER_NAME_GRD" runat="server" Text='<%# Bind("user_name") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="رقم المستخدم" Visible="False">
                         <ItemTemplate>
                             <asp:Label ID="LBL_USER_ID" runat="server" Text='<%# Bind("user_id") %>'></asp:Label>
                         </ItemTemplate>
                     </asp:TemplateField>
                    <asp:CommandField ShowSelectButton="True" EditText="تعديل" SelectText="تفاصيل" />
                    <asp:CommandField EditText="تعديل" NewText="تأكيد" ShowInsertButton="True" />
                    <asp:CommandField CancelText="رفض" DeleteText="رفض" ShowDeleteButton="True" />
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
            <br />
            <asp:GridView ID="GRD_STOCK_DETAILS" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" AutoGenerateColumns="False">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:TemplateField HeaderText="رقم الصنف">
                        <ItemTemplate>
                            <asp:Label ID="Label8" runat="server" Text='<%# Bind("prod_id") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="الصنف">
                        <ItemTemplate>
                            <asp:Label ID="Label11" runat="server" Text='<%# Bind("name") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="الكمية">
                        <ItemTemplate>
                            <asp:Label ID="Label12" runat="server" Text='<%# Bind("qty") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                <SortedAscendingCellStyle BackColor="#FDF5AC" />
                <SortedAscendingHeaderStyle BackColor="#4D0000" />
                <SortedDescendingCellStyle BackColor="#FCF6C0" />
                <SortedDescendingHeaderStyle BackColor="#820000" />
            </asp:GridView>
            <br />
        </dev>      
    </asp:Panel>
</asp:Content>


