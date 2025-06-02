<%@ Page Title="" Language="C#" MasterPageFile="~/dental-master.master" AutoEventWireup="true" CodeFile="inventories.aspx.cs" Inherits="inventories" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" runat="server" contentplaceholderid="PLACE_MASTER">
    <asp:Panel ID="PANEL_MASTER" runat="server">
    <div>

    <asp:Label ID="LBL_ID" runat="server" Text="ID"></asp:Label>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;
    <asp:TextBox ID="TXT_ID" runat="server"></asp:TextBox>
    <br />
    <br />
    <asp:Label ID="Label2" runat="server" Text="الاسم"></asp:Label>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;
    <asp:TextBox ID="TXT_NAME" runat="server"></asp:TextBox>
    <br />
    <br />
        <asp:Label ID="Label3" runat="server" Text="المحافظة"></asp:Label>
            &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;
        <asp:DropDownList ID="DRP_CITY" runat="server" Height="32px" Width="129px">
        </asp:DropDownList>
        <br />
        <br />
        <asp:Label ID="Label1" runat="server" Text="المنطقة"></asp:Label>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;
        <asp:DropDownList ID="DRP_REGON" runat="server" Height="32px" Width="128px">
        </asp:DropDownList>
        <br />
        <br />
        <asp:Label ID="Label6" runat="server" Text="الشارع"></asp:Label>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;
        <asp:TextBox ID="TXT_STREET" runat="server"></asp:TextBox>
        <br />
        <br />
         <asp:Label ID="Label7" runat="server" Text="رقم المبنى"></asp:Label>
         &nbsp;&nbsp;&nbsp;&nbsp;<asp:TextBox ID="TXT_BUILD_NO" runat="server"></asp:TextBox>
         <br />
         <br />        
        <asp:Label ID="Label4" runat="server" Text="ملحوظات"></asp:Label>
        &nbsp; &nbsp;
    <asp:TextBox ID="TXT_NOTES" runat="server" TextMode="MultiLine" Width="128px"></asp:TextBox>
        <br />
        <br />
        <asp:Button ID="BTN_SAVE" runat="server" Text="حفظ" Width="79px" OnClick="BTN_SAVE_Click" />
    <br />
    <br />
    <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" OnRowDeleting="GridView1_RowDeleting">
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:TemplateField HeaderText="ID">
                <ItemTemplate>
                    <asp:Label ID="Label8" runat="server" Text='<%# Bind("id") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="الاسم">
                <ItemTemplate>
                    <asp:Label ID="Label9" runat="server" Text='<%# Bind("name") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="المحافظة">
                <ItemTemplate>
                    <asp:Label ID="Label10" runat="server" Text='<%# Bind("city_name") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="المنطقة">
                <ItemTemplate>
                    <asp:Label ID="Label11" runat="server" Text='<%# Bind("region_name") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="الشارع">
                <ItemTemplate>
                    <asp:Label ID="Label12" runat="server" Text='<%# Bind("street") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="رقم المبنى">
                <ItemTemplate>
                    <asp:Label ID="Label13" runat="server" Text='<%# Bind("buid_number") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="ملحوظات">
                <ItemTemplate>
                    <asp:Label ID="Label14" runat="server" Text='<%# Bind("desc") %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:CommandField ShowEditButton="True" />
            <asp:CommandField ShowDeleteButton="True" />
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


