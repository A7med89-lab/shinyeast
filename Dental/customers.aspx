<%@ Page Title="" Language="C#" MasterPageFile="~/dental-master.master" AutoEventWireup="true" CodeFile="customers.aspx.cs" Inherits="customers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .auto-style10 {
            width: 55px;
        }
        .auto-style11 {
            width: 14%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" runat="server" contentplaceholderid="PLACE_MASTER">
    <asp:Panel ID="PANEL_MASTER" runat="server">
    <div>
            <asp:Label ID="LBL_ID" runat="server" Text="ID"></asp:Label>
            &nbsp;&nbsp &nbsp&nbsp&nbsp &nbsp &nbsp &nbsp&nbsp; &nbsp <asp:TextBox ID="TXT_ID" runat="server" Height="21px"></asp:TextBox>

            &nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<br /><br /><asp:Label ID="Label2" runat="server" Text="Name"></asp:Label>
            &nbsp;&nbsp &nbsp&nbsp&nbsp &nbsp&nbsp&nbsp; <asp:TextBox ID="TXT_NAME" runat="server"></asp:TextBox>
            <br />
            <br />
            <asp:Label ID="Label12" runat="server" Text="Lab Name"></asp:Label>
            &nbsp;&nbsp;&nbsp; <asp:TextBox ID="TXT_LAB_NAME" runat="server"></asp:TextBox>
            <br />
            <br />
            <asp:Label ID="Label8" runat="server" Text="Phone"></asp:Label>
            &nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp; <asp:TextBox ID="TXT_PHONE" runat="server" TextMode="MultiLine" Height="16px" Width="175px"></asp:TextBox>
            <br />
            <br />
            <asp:Label ID="LBL_Region" runat="server" Text="Regoin"></asp:Label>
            &nbsp;&nbsp&nbsp; &nbsp;&nbsp;&nbsp;&nbsp; 
            <asp:DropDownList ID="DRP_REGON" runat="server" Height="19px" Width="171px">
            </asp:DropDownList>
            <br />
            <br />
            <asp:Label ID="Label3" runat="server" Text="Street"></asp:Label>
            &nbsp;&nbsp; &nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:TextBox ID="TXT_STRT" runat="server"></asp:TextBox>
            <br />
            <br />
            <asp:Label ID="Label5" runat="server" Text="Buid NO"></asp:Label>
            &nbsp; &nbsp; &nbsp; <asp:TextBox ID="TXT_BULD" runat="server"></asp:TextBox>
            <br />
            <br />
            <asp:Label ID="Label7" runat="server" Text="Dental Type"></asp:Label>
            &nbsp;<asp:CheckBox ID="CHK_FXD" runat="server" Text="Fixed" />
            &nbsp;<asp:CheckBox ID="CHK_DYNMIC" runat="server" Text="Dynamic" />

            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="Label9" runat="server" Text="Working Hours"></asp:Label>      
            &nbsp;&nbsp;&nbsp;      
            <asp:Label ID="Label10" runat="server" Text="From"></asp:Label>
            &nbsp; &nbsp; &nbsp; <asp:DropDownList ID="DRP_FROM" runat="server">
                <asp:ListItem Value="1">1 AM</asp:ListItem>
                <asp:ListItem Value="2">2 AM</asp:ListItem>
                <asp:ListItem Value="3">3 AM</asp:ListItem>
                <asp:ListItem Value="4">4 AM</asp:ListItem>
                <asp:ListItem Value="5">5 AM</asp:ListItem>
                <asp:ListItem Value="6">6 AM</asp:ListItem>
                <asp:ListItem Value="7">7 AM</asp:ListItem>
                <asp:ListItem Value="8">8 AM</asp:ListItem>
                <asp:ListItem Value="9">9 AM</asp:ListItem>
                <asp:ListItem Value="10">10 AM</asp:ListItem>
                <asp:ListItem Value="11">11 AM</asp:ListItem>
                <asp:ListItem Value="12">12 AM</asp:ListItem>
            </asp:DropDownList>
            &nbsp; &nbsp; &nbsp; <asp:Label ID="Label11" runat="server" Text="TO"></asp:Label>
            &nbsp; &nbsp; &nbsp; <asp:DropDownList ID="DRP_TO" runat="server">
                <asp:ListItem Value="1">1 PM</asp:ListItem>
                <asp:ListItem Value="2">2 PM</asp:ListItem>
                <asp:ListItem Value="3">3 PM</asp:ListItem>
                <asp:ListItem Value="4">4 PM</asp:ListItem>
                <asp:ListItem Value="5">5 PM</asp:ListItem>
                <asp:ListItem Value="6">6 PM</asp:ListItem>
                <asp:ListItem Value="7">7 PM</asp:ListItem>
                <asp:ListItem Value="8">8 PM</asp:ListItem>
                <asp:ListItem Value="9">9 PM</asp:ListItem>
                <asp:ListItem Value="10">10 PM</asp:ListItem>
                <asp:ListItem Value="11">11 PM</asp:ListItem>
                <asp:ListItem Value="12">12 PM</asp:ListItem>
                </asp:DropDownList>
            <br />
            <br />
            <table class="auto-style11">
                <tr>
                    <td class="auto-style10">
                        <asp:Button ID="Button1" runat="server" Text="Submit" OnClick="Button1_Click" style="height: 26px" />
                    </td>
                    
                    
                    <td class="auto-style5">
                         <asp:Button ID="Button2" runat="server" Text="Appointment" OnClick="Button2_Click" />
                    </td>

                </tr>
            </table>
            <br />
            <asp:GridView ID="GridView1" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowDeleting="GridView1_RowDeleting" OnRowEditing="GridView1_RowEditing" AutoGenerateColumns="False" OnRowUpdating="GridView1_RowUpdating">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:TemplateField HeaderText="ID">
                        <EditItemTemplate>
                            <asp:Label ID="Label16" runat="server" Text='<%# Bind("id") %>'></asp:Label>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label13" runat="server" Text='<%# Bind("id") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Name">
                        <EditItemTemplate>
                            <asp:TextBox ID="cst_name_edite" runat="server" Text='<%# Bind("name") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="cst_name_desgin" runat="server" Text='<%# Bind("name") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Lab">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox4" runat="server" Text='<%# Bind("lab") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label15" runat="server" Text='<%# Bind("lab") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Region">
                        <EditItemTemplate>
                            <asp:DropDownList ID="DropDownList1" runat="server" DataSourceID="SqlDataSource2" DataTextField="name" DataValueField="id" SelectedValue='<%# Bind("region_id") %>'>
                            </asp:DropDownList>
                            <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:DentalConnectionString %>" SelectCommand="SELECT [id], [name] FROM [regions]"></asp:SqlDataSource>
                            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DentalConnectionString %>" SelectCommand="SELECT [name] FROM [regions]"></asp:SqlDataSource>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label26" runat="server" Text='<%# Bind("region") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Street">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox5" runat="server" Text='<%# Bind("street") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label18" runat="server" Text='<%# Bind("street") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Build_No">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox6" runat="server" Text='<%# Bind("buid_number") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label19" runat="server" Text='<%# Bind("buid_number") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Phone">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox7" runat="server" Text='<%# Bind("phone") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label20" runat="server" Text='<%# Bind("phone") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Fixed">
                        <ItemTemplate>
                            <asp:Label ID="Label21" runat="server" Text='<%# Bind("fixed") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Dynamic">
                        <ItemTemplate>
                            <asp:Label ID="Label22" runat="server" Text='<%# Bind("dynamic") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="From">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox8" runat="server" Text='<%# Bind("wh_from") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label25" runat="server" Text='<%# Bind("wh_from") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="TO">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox9" runat="server" Text='<%# Bind("wh_to") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label24" runat="server" Text='<%# Bind("wh_to") %>'></asp:Label>
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
            <asp:SqlDataSource ID="SqlDataSource1" runat="server"></asp:SqlDataSource>
        </div>
</asp:Panel>
</asp:Content>


