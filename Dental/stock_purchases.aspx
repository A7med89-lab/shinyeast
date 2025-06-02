<%@ Page Title="" Language="C#" MaintainScrollPositionOnPostBack="true" MasterPageFile="~/dental-master.master" AutoEventWireup="true" CodeFile="stock_purchases.aspx.cs" Inherits="stock_purchases" %>

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
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Label ID="LBL_PURCHASE_MASSEGE" runat="server" Text="لا توجد اوامر شراء" Visible="False"></asp:Label>
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
                            <asp:SqlDataSource ID="inv" runat="server" ConnectionString="<%$ ConnectionStrings:dental %>" SelectCommand="SELECT [id], [name] FROM [inventories]"></asp:SqlDataSource>                          
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
            <asp:GridView ID="GRD_STOCK_DETAILS" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" AutoGenerateColumns="False" OnRowCommand="GRD_STOCK_DETAILS_RowCommand">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:TemplateField HeaderText="رقم الصنف">
                        <ItemTemplate>
                            <asp:Label ID="LBL_PROD_ID_GRD" runat="server" Text='<%# Bind("prod_id") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="الصنف">
                        <ItemTemplate>
                            <asp:Label ID="LBL_PROD_NAME_GRD" runat="server" Text='<%# Bind("name") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="الكمية">
                        <ItemTemplate>
                            <asp:Label ID="LBL_QTY_GRD" runat="server" Text='<%# Bind("qty") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:CommandField ShowSelectButton="True" SelectText="التكلفة" />
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
            <asp:Label ID="Label13" runat="server" Text="تكلفة المنتج"></asp:Label>
            <br />
            <br />
            <asp:GridView ID="GRD_PROD_COST" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" OnRowCommand="GRD_PROD_COST_RowCommand" OnRowDeleting="GRD_PROD_COST_RowDeleting">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:TemplateField HeaderText="رقم الصنف">
                        <ItemTemplate>
                            <asp:Label ID="LBL_COST_PROD_ID_GRD" runat="server" Text='<%# Bind("prod_id") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="الصنف">
                        <ItemTemplate>
                            <asp:Label ID="LBL_COST_PROD_NAME_GRD" runat="server" Text='<%# Bind("prod_name") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="اسم التكلفة">
                        <ItemTemplate>
                            <asp:DropDownList ID="DRP_COST_NAME_GRD"  runat="server" OnSelectedIndexChanged="DRP_COST_NAME_GRD_SelectedIndexChanged" AutoPostBack="True">
                            </asp:DropDownList>
                            <asp:Label ID="LBL_COST_NAME_GRD" runat="server" Text='<%# Bind("cost_name") %>'></asp:Label>
                            <asp:Label ID="LBL_COST_ID_GRD" runat="server" Text='<%# Bind("cost_id") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="قيمه التكلفة">
                        <ItemTemplate>
                            <asp:TextBox ID="TXT_COST_AMOUNT_GRD" runat="server" Text='<%# Bind("amount") %>' Height="16px"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ملحوظات">
                        <ItemTemplate>
                            <asp:TextBox ID="TXT_COST_DESC_GRD" runat="server" Text='<%# Bind("desc") %>'></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="index">
                        <ItemTemplate>
                            <asp:Label ID="LBL_ID_GRD" runat="server" Text='<%# Bind("id") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:CommandField SelectText="حفظ" ShowSelectButton="True" />
                    <asp:CommandField DeleteText="مسح" ShowDeleteButton="True" />
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
            <br />
        </dev>      
    </asp:Panel>
</asp:Content>


