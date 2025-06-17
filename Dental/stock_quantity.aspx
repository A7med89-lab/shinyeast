<%@ Page Title="" Language="C#" MasterPageFile="~/dental-master.master" AutoEventWireup="true" CodeFile="stock_quantity.aspx.cs" Inherits="stock_quantity" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" runat="server" contentplaceholderid="PLACE_MASTER">
    <asp:Panel ID="PANEL_MASTER" runat="server">
    <dev>
          <br />
          <asp:Label ID="Label1" runat="server" Text="المخزن الاجمالى"></asp:Label>
          <br />
          <br />
          <asp:GridView ID="GRD_STOCK_TOTAL" runat="server" CellPadding="4" AutoGenerateColumns="False" BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px">
              <Columns>
                  <asp:TemplateField HeaderText="رقم المخزن">
                      <ItemTemplate>
                          <asp:Label ID="Label3" runat="server" Text='<%# Bind("inventory_id") %>'></asp:Label>
                      </ItemTemplate>
                  </asp:TemplateField>
                  <asp:TemplateField HeaderText="اسم المخزن">
                      <ItemTemplate>
                          <asp:Label ID="Label2" runat="server" Text='<%# Bind("inventory_name") %>'></asp:Label>
                      </ItemTemplate>
                  </asp:TemplateField>
                  <asp:TemplateField HeaderText="رقم الصنف">
                      <ItemTemplate>
                          <asp:Label ID="Label4" runat="server" Text='<%# Bind("product_id") %>'></asp:Label>
                      </ItemTemplate>
                  </asp:TemplateField>
                  <asp:TemplateField HeaderText="اسم الصنف">
                      <ItemTemplate>
                          <asp:Label ID="Label5" runat="server" Text='<%# Bind("product_name") %>'></asp:Label>
                      </ItemTemplate>
                  </asp:TemplateField>
                  <asp:TemplateField HeaderText="اجمالى  الواردات">
                       <ItemTemplate>
                          <asp:Label ID="Label6" runat="server" Text='<%# Bind("total_in") %>'></asp:Label>
                      </ItemTemplate>
                  </asp:TemplateField>
                  <asp:TemplateField HeaderText="اجمالى الصرف">
                       <ItemTemplate>
                          <asp:Label ID="Label9" runat="server" Text='<%# Bind("total_out") %>'></asp:Label>
                      </ItemTemplate>
                  </asp:TemplateField>
                  <asp:TemplateField HeaderText="الكمية الحالية">
                      <ItemTemplate>
                          <asp:Label ID="Label7" runat="server" Text='<%# Bind("net") %>'></asp:Label>
                      </ItemTemplate>
                  </asp:TemplateField>
                  <asp:TemplateField HeaderText="سعر الصنف">
                      <ItemTemplate>
                          <asp:Label ID="Label8" runat="server" Text='<%# Bind("product_price") %>'></asp:Label>
                      </ItemTemplate>
                  </asp:TemplateField>
                  
              </Columns>
              <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
              <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />
              <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
              <RowStyle BackColor="White" ForeColor="#003399" />
              <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
              <SortedAscendingCellStyle BackColor="#EDF6F6" />
              <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
              <SortedDescendingCellStyle BackColor="#D6DFDF" />
              <SortedDescendingHeaderStyle BackColor="#002876" />
          </asp:GridView>
          <br />
          <br />
          
          <br />
          <br />
          <br />
       </dev>
                            
</asp:Panel>
                    </asp:Content>


