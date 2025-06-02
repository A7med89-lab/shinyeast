<%@ Page Title="" Language="C#" MasterPageFile="~/dental-master.master" AutoEventWireup="true" CodeFile="purchases_edit_delete.aspx.cs" Inherits="purchases_edit_delete" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" runat="server" contentplaceholderid="PLACE_MASTER">
    <asp:Panel ID="PANEL_MASTER" runat="server">
        <dev>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <br />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <table class="auto-style10">
                <tr>        
                    
                    
        
                    <td>
                        <asp:DropDownList ID="DRP_SUPP_FILTER" runat="server" AutoPostBack="true"></asp:DropDownList>
                    </td>                    
                    <td>
                        <asp:TextBox ID="TXT_DATE_FILTER" runat="server" AutoPostBack="True" TextMode="Date"></asp:TextBox>
                    </td>                    
                    <td>
                        <asp:DropDownList ID="DRP_INV_FILTER" runat="server" AutoPostBack="true"> </asp:DropDownList>
                    </td>
                    <td>
                         
                    </td>
                    <td>
                        &nbsp;</td>
                </tr>
            </table>
            <asp:GridView ID="GRD_STOCK_IN" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" AutoGenerateColumns="False" OnRowCancelingEdit="GRD_STOCK_IN_RowCancelingEdit" OnRowCommand="GRD_STOCK_IN_RowCommand1" OnRowEditing="GRD_STOCK_IN_RowEditing">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:TemplateField HeaderText="رقم امر الشراء">
                        <EditItemTemplate>
                            <asp:Label ID="LBL_PURCHASE_ID_EDIT_GRD" runat="server" Text='<%# Bind("id") %>'></asp:Label>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="LBL_PURCHASE_ID_GRD" runat="server" Text='<%# Bind("id") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="التاريخ">
                        <EditItemTemplate>
                            <asp:Label ID="LBL_DATE_EDIT_GRD" runat="server" Text='<%# Bind("date") %>'></asp:Label>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="LBL_DATE_GRD" runat="server" Text='<%# Bind("date") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="اسم المورد">
                        <EditItemTemplate>
                            <asp:DropDownList ID="DRP_SUPP_EDIT_GRD" runat="server" DataSourceID="supp" DataTextField="name" DataValueField="id">
                            </asp:DropDownList>
                            <asp:SqlDataSource ID="supp" runat="server" ConnectionString="<%$ ConnectionStrings:dental %>" SelectCommand="SELECT [id], [name] FROM [suppliers]"></asp:SqlDataSource>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="LBL_SUPP_NAME" runat="server" Text='<%# Bind("supplier_name") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="اجمالى الكمية">
                        <ItemTemplate>
                            <asp:Label ID="LBL_SUM_QTY_GRD" runat="server" Text='<%# Bind("sum_qty") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>                    
                    <asp:TemplateField HeaderText="اسم المستخدم">
                        <ItemTemplate>
                            <asp:Label ID="LBL_USER_NAME_GRD" runat="server" Text='<%# Bind("user_name") %>'></asp:Label>
                        </ItemTemplate>
                         <EditItemTemplate>
                             <asp:Label ID="LBL_USER_NAME_EDIT_GRD" runat="server" Text='<%# Bind("user_name") %>'></asp:Label>
                         </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:CommandField ShowSelectButton="True" EditText="تعديل" SelectText="تفاصيل" />
                    <asp:CommandField EditText="تعديل" NewText="تأكيد" ShowEditButton="True" />
                    <asp:CommandField CancelText="رفض" DeleteText="مسح" ShowDeleteButton="True" />
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
            <asp:GridView ID="GRD_STOCK_DETAILS" runat="server" CellPadding="3" AutoGenerateColumns="False" BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" CellSpacing="2">
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
                    <asp:TemplateField HeaderText="سعر الشراء">
                        <ItemTemplate>
                            <asp:Label ID="LBL_PURCHASE_PRICE_GRD" runat="server" Text='<%# Bind("price") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="الكمية">
                        <ItemTemplate>
                            <asp:Label ID="LBL_QTY_GRD" runat="server" Text='<%# Bind("qty") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="الاجمالى">
                        <ItemTemplate>
                            <asp:Label ID="LBL_TOTAL_PRICE_GRD" runat="server" Text='<%# Bind("total_price") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="الضريبة %">
                        <ItemTemplate>
                            <asp:Label ID="LBL_TAX_GRD" runat="server" Text='<%# Bind("tax") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="الاجمالى بعد الضريبة">
                    <ItemTemplate>
                        <asp:Label ID="LBL_TOTAL_AFTER_TAX_GRD" runat="server" Text='<%# Bind("total_after_tax") %>'></asp:Label>
                    </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="الخصم %">
                        <ItemTemplate>
                            <asp:Label ID="LBL_DISC_GRD" runat="server" Text='<%# Bind("discount") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="الاجمالى بعد الخصم">
                        <ItemTemplate>
                            <asp:Label ID="LBL_TOAL_AFTER_DISC_GRD" runat="server" Text='<%# Bind("total_after_discount") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="الربح">
                        <ItemTemplate>
                            <asp:Label ID="LBL_PROFIT_GRD" runat="server" Text='<%# Bind("profit") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:CommandField EditText="تعديل" ShowEditButton="True" />
                    <asp:CommandField DeleteText="مسح" ShowDeleteButton="True" />
                </Columns>
                <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
                <HeaderStyle BackColor="#A55129" Font-Bold="True" ForeColor="White" />
                <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
                <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
                <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
                <SortedAscendingCellStyle BackColor="#FFF1D4" />
                <SortedAscendingHeaderStyle BackColor="#B95C30" />
                <SortedDescendingCellStyle BackColor="#F1E5CE" />
                <SortedDescendingHeaderStyle BackColor="#93451F" />
            </asp:GridView>
            <br />
        </dev>      
    </asp:Panel>
</asp:Content>


