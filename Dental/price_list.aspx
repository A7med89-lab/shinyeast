<%@ Page Title="" Language="C#" MasterPageFile="~/dental-master.master" AutoEventWireup="true" CodeFile="price_list.aspx.cs" Inherits="price_list" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" runat="server" contentplaceholderid="PLACE_MASTER">
    <asp:Panel ID="PANEL_MASTER" runat="server">
        <div>

                            <asp:Label ID="Label1" runat="server" Text="المنتج"></asp:Label>
                            &nbsp;
                            <asp:DropDownList ID="DRP_PRODUCT" runat="server" Height="30px" Width="143px">
                            </asp:DropDownList>
                            <br />
                            <br />
                            <asp:Label ID="Label2" runat="server" Text="سعر البيع"></asp:Label>
                            &nbsp; &nbsp; &nbsp;
                            <asp:TextBox ID="TXT_PRICE" runat="server"></asp:TextBox>
                            <br />
                            <br />
                            <asp:Button ID="BTN" runat="server" Text="حفظ" OnClick="BTN_Click" Height="26px" Width="76px" />
                            <br />
                            <br />
                            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" OnRowDeleting="GridView1_RowDeleting" OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating">
                                <AlternatingRowStyle BackColor="White" />
                                <Columns>
                                    <asp:TemplateField HeaderText="ID">
                                        <EditItemTemplate>
                                            <asp:Label ID="LBL_PRODUCT_ID_GRD" runat="server" Text='<%# Bind("product_id") %>'></asp:Label>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="LBL_PRODUCT_ID_GRD_" runat="server" Text='<%# Bind("product_id") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="المنتج">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="TXT_PRODUCT_NAME_GRID" runat="server" Text='<%# Bind("name") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="LBL_PRODUCT_NAME_GRD" runat="server" Text='<%# Bind("name") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="سعر البيع">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="TXT_PRICE_GRD" runat="server" Text='<%# Bind("price") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="Label5" runat="server" Text='<%# Bind("price") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
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


