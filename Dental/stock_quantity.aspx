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
          <asp:GridView ID="GRD_STOCK_TOTAL" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" AutoGenerateColumns="False">
              <AlternatingRowStyle BackColor="White" />
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
          <br />
          <br />
       </dev>
                            
</asp:Panel>
                    </asp:Content>


