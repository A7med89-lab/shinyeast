<%@ Page Title="" Language="C#" MasterPageFile="~/dental-master.master" AutoEventWireup="true" CodeFile="sales_draft.aspx.cs" Inherits="sales_draft" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .auto-style10 {
            width: 10%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" runat="server" contentplaceholderid="PLACE_MASTER">
    <asp:Panel ID="PANEL_MASTER" runat="server">
        <dev>
            <asp:Label ID="Label1" runat="server" Text="تاريخ" ></asp:Label>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:TextBox ID="TXT_Date" runat="server" TextMode="Date" AutoPostBack="true"></asp:TextBox>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <br />
        <br />
        <asp:Label ID="Label2" runat="server" Text="ID"></asp:Label>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;
        <asp:TextBox ID="TXT_ID" runat="server" ></asp:TextBox>
        <br />
        <br />
        <asp:Label ID="Label3" runat="server" Text="العميل"></asp:Label>
        &nbsp;
        <asp:DropDownList ID="DRP_CUSTOMER" runat="server" Height="21px" Width="136px" AutoPostBack="True" OnSelectedIndexChanged="DRP_CUSTOMER_SelectedIndexChanged">
        </asp:DropDownList>
        <br />
        <br />
        <asp:Label ID="Label4" runat="server" Text="المنتج"></asp:Label>
        &nbsp;&nbsp;&nbsp;
        <asp:DropDownList ID="DRP_PRODUCT" runat="server" AutoPostBack="true" Height="17px" Width="139px" OnSelectedIndexChanged="DRP_PRODUCT_SelectedIndexChanged">
        </asp:DropDownList>
        <br />
        <br />
        <table class="auto-style10">
            <tr>
                
                <td>
                    <asp:Button ID="BTN_NEW" runat="server" Text="جديد" Width="117px" OnClick="BTN_NEW_Click" />
                </td>
                

            </tr>
        </table>

        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
        <table class="auto-style10">
            <tr>
                
                <td>
                    <asp:Label ID="LBL_FILTER" runat="server" Text="بحث"></asp:Label> 
                </td>
                <td>
                    <asp:CheckBox ID="CHK_Customer" runat="server" AutoPostBack="true" /> 
                </td>
                
                <td>
                    <asp:DropDownList ID="DRP_CUSTOMER_FILTER" runat="server" AutoPostBack="true"></asp:DropDownList>
                </td>
                <td>
                    <asp:CheckBox ID="CHK_Date" runat="server" AutoPostBack="true" /> 
                </td>
                <td>
                    <asp:TextBox ID="TXT_DATE_FILTER" runat="server" AutoPostBack="True" TextMode="Date"></asp:TextBox>
                </td>
                
            </tr>
        </table>
        
        <br />
       <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" OnRowUpdating="GridView1_RowUpdating" OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowDeleting="GridView1_RowDeleting">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:TemplateField HeaderText="ID">
                    <EditItemTemplate>
                        <asp:TextBox ID="TXT_ID_GRD" runat="server" Text='<%# Bind("id") %>' Height="16px" Width="95px"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="LBL_ID_GRD" runat="server" Text='<%# Bind("id") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="تاريخ">
                    <EditItemTemplate>
                        <asp:TextBox ID="TXT_DATE_GRD" runat="server" Text='<%# Bind("date") %>' TextMode="Date"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="LBL_DATE_GRD" runat="server" Text='<%# Bind("date") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="الاسم">
                    <EditItemTemplate>
                        <asp:TextBox ID="TXT_NAME_GRD" runat="server" Text='<%# Bind("product") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="LBL_NAME_GRD" runat="server" Text='<%# Bind("product") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="العميل">
                    <EditItemTemplate>
                        <asp:TextBox ID="TXT_CUST_NAME_GRD" runat="server" Text='<%# Bind("customer") %>' ></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="LBL_CUST_NAME_GRD" runat="server" Text='<%# Bind("customer") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="الكمية">
                    <EditItemTemplate>
                        <asp:TextBox ID="TXT_QTY_GRD" runat="server" Text='<%# Bind("quantity") %>' AutoPostBack="True" Height="16px" Width="95px" OnTextChanged="TXT_QTY_GRD_TextChanged"  ></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="LBL_QTY_GRD" runat="server" Text='<%# Bind("quantity") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="السعر">
                    <EditItemTemplate>
                        <asp:TextBox ID="TXT_PRICE_GRD" runat="server" Text='<%# Bind("price") %>' AutoPostBack="True" OnTextChanged="TXT_PRICE_GRD_TextChanged" Height="16px" Width="95px"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="LBL_PRICE_GRD" runat="server" Text='<%# Bind("price") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="اجمالى السعر">
                    <EditItemTemplate>
                        <asp:TextBox ID="TXT_TOTAL_PRICE_GRD" runat="server" Text='<%# Bind("total_price") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="LBL_TOTAL_PRICE_GRD" runat="server" Text='<%# Bind("total_price") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="الربح">
                    <EditItemTemplate>
                        <asp:TextBox ID="TXT_PROFIT_DRG" runat="server" Text='<%# Bind("profit") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="LBL_PROFIT_DRG" runat="server" Text='<%# Bind("profit") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="نسبة الربح %">
                    <EditItemTemplate>
                        <asp:TextBox ID="TXT_PERCENT_DRG" runat="server" AutoPostBack="True" Text='<%# Bind("profit_percent") %>' Width="89px"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="LBL_PERCENT_DRG" runat="server" Text='<%# Bind("profit_percent") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="المخزن">
                    <EditItemTemplate>
                        <asp:DropDownList ID="DRP_INV_GRD" runat="server" DataSourceID="inv" DataTextField="name" DataValueField="id">
                        </asp:DropDownList>
                        <asp:SqlDataSource ID="inv" runat="server" ConnectionString="<%$ ConnectionStrings:DentalConnectionString %>" SelectCommand="SELECT [id], [name] FROM [inventory]"></asp:SqlDataSource>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label ID="Label5" runat="server" Text='<%# Bind("inventory_name") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:CommandField ShowEditButton="True" CancelText="الغاء" DeleteText="مسح" EditText="تعديل" UpdateText="تعديل" />
                <asp:CommandField ShowDeleteButton="True" CancelText="الغاء" DeleteText="مسح" EditText="تعديل" UpdateText="تعديل" />
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
        </dev>
    </asp:Panel>
</asp:Content>




