﻿<%@ Page Title="" Language="C#"  MaintainScrollPositionOnPostBack="true" MasterPageFile="~/dental-master.master" AutoEventWireup="true" CodeFile="purchases.aspx.cs" Inherits="purchases" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .auto-style10 {
            width: 10%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" runat="server" contentplaceholderid="PLACE_MASTER">
    <asp:Panel ID="PANEL_MASTER" runat="server">
    <div>

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
        <asp:TextBox ID="TXT_ID" runat="server" OnTextChanged="TXT_ID_TextChanged" ></asp:TextBox>
        <br />
        <br />
        <asp:Label ID="LBL_SUPP" runat="server" Text="المورد"></asp:Label>
        &nbsp;
        <asp:DropDownList ID="DRP_SUPPLIER" runat="server" Height="21px" Width="136px" AutoPostBack="True" OnSelectedIndexChanged="DRP_SUPPLIER_SelectedIndexChanged" OnTextChanged="DRP_SUPPLIER_TextChanged">
        </asp:DropDownList>
        &nbsp;
        <br />
        <br />
&nbsp;
        <asp:Button ID="BTN_ADD_ORDER" runat="server" Text="اضافة امر اخر" Width="85px" OnClick="BTN_ADD_ORDER_Click" />
        <br />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;         
        
        <br />
        <script type="text/javascript">
            function allowOnlyNumbers(event) {
                var key = event.key;
                // Allow digits only
                if (!/^\d$/.test(key) && key !== 'Backspace' && key !== 'Tab') {
                    event.preventDefault();
                }
            }
        </script>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" OnRowUpdating="GridView1_RowUpdating" OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowDeleting="GridView1_RowDeleting" OnRowEditing="GridView1_RowEditing" OnDataBound="GridView1_DataBound" OnRowDataBound="GridView1_RowDataBound" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" ShowFooter="True" OnRowCommand="GridView1_RowCommand">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:TemplateField HeaderText="اسم المنتج">
                    <EditItemTemplate>
                        <asp:DropDownList ID="DRP_NAME_GRD_EDIT" runat="server" DataSourceID="prod" DataTextField="name" DataValueField="id">
                        </asp:DropDownList>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <br />

                        <asp:DropDownList ID="DRP_NAME_GRD" runat="server" DataTextField="name" DataValueField="id" AutoPostBack="True" OnSelectedIndexChanged="DRP_NAME_GRD_SelectedIndexChanged">
                        </asp:DropDownList>
                        <asp:Label ID="LBL_PROD_NAME_GRD" runat="server" Text='<%# Bind("product_name") %>'></asp:Label>
                        <br />
                        <asp:Label ID="LBL_PROD_ID_GRD" runat="server" Text='<%# Bind("product_id") %>'></asp:Label>
                        <br />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="السعر">
                    <EditItemTemplate>

                        <asp:TextBox ID="TXT_PRICE_GRD_EDIT" runat="server" AutoPostBack="True" Height="16px" OnTextChanged="TXT_PRICE_GRD_TextChanged" Text='<%# Bind("price") %>' Width="95px"></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:TextBox ID="TXT_PRICE_GRD" runat="server" onkeypress="allowOnlyNumbers(event)" AutoPostBack="True" Height="16px" OnTextChanged="TXT_PRICE_GRD_TextChanged" Text='<%# Bind("price") %>' Width="95px"></asp:TextBox>                       
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="العدد">
                    <EditItemTemplate>
                        <asp:TextBox ID="TXT_QTY_GRD_EDIT" runat="server" OnTextChanged="TXT_QTY_GRD_TextChanged" Text='<%# Bind("quantity") %>' AutoPostBack="True" Height="16px" Width="95px" ></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:TextBox ID="TXT_QTY_GRD" runat="server" onkeypress="allowOnlyNumbers(event)" OnTextChanged="TXT_QTY_GRD_TextChanged" Text='<%# Bind("quantity") %>' AutoPostBack="True" Height="16px" Width="95px" ></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="اجمالى السعر">
                    <EditItemTemplate>
                        <asp:TextBox ID="TXT_TOTAL_PRICE_GRD_EDIT" runat="server" Text='<%# Bind("total_price") %>' Enabled="False"></asp:TextBox>
                    </EditItemTemplate>                    
                    <ItemTemplate>
                        <asp:TextBox ID="TXT_TOTAL_PRICE_GRD" runat="server" Text='<%# Bind("total_price") %>' Enabled="False" ></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="الضريبة %">
                    <ItemTemplate>
                        <asp:TextBox ID="TXT_TAX_GRD" runat="server" onkeypress="allowOnlyNumbers(event)" Text='<%# Bind("tax") %>' AutoPostBack="True" OnTextChanged="TXT_TAX_GRD_TextChanged"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="الاجمالى بعد الضريبة">
                    <ItemTemplate>
                        <asp:TextBox ID="TXT_TOTAL_AFTER_TAX" runat="server" Enabled="False"  Text='<%# Bind("total_after_tax") %>'></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="الخصم %">
                    <ItemTemplate>
                        <asp:TextBox ID="TXT_DISC_GRD" runat="server" onkeypress="allowOnlyNumbers(event)" Text='<%# Bind("discount") %>' AutoPostBack="True"  Width="89px" OnTextChanged="TXT_DISC_GRD_TextChanged"></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="الاجمالى بعد الخصم">
                    <ItemTemplate>
                        <asp:TextBox ID="TXT_TOTAL_AFTER_DISC" runat="server" Enabled="False" Text='<%# Bind("total_after_discount") %>'></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="الربح">
                    <EditItemTemplate>
                        <asp:TextBox ID="TXT_PROFIT_DRG_EDIT" runat="server" Enabled="False" Text='<%# Bind("profit") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:TextBox ID="TXT_PROFIT_DRG" runat="server" Enabled="False" Text='<%# Bind("profit") %>'></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:CommandField CancelText="الغاء" DeleteText="مسح" EditText="تعديل" UpdateText="حفظ" NewText="حفظ" ShowCancelButton="False" ShowInsertButton="True" />
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
        <br />

    </div>
</asp:Panel>
</asp:Content>


