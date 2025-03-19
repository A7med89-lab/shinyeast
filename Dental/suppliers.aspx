<%@ Page Title="" Language="C#" MasterPageFile="~/dental-master.master" AutoEventWireup="true" CodeFile="suppliers.aspx.cs" Inherits="suppliers" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" runat="server" contentplaceholderid="PLACE_MASTER">
    <asp:Panel ID="PANEL_MASTER" runat="server">
           <div>
            <asp:Label ID="LBL_ID" runat="server" Text="ID"></asp:Label>
            &nbsp;&nbsp &nbsp&nbsp&nbsp &nbsp &nbsp &nbsp&nbsp; &nbsp <asp:TextBox ID="TXT_ID" runat="server" Height="21px"></asp:TextBox>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Label ID="LBL_Region" runat="server" Text="Regoin"></asp:Label>
            &nbsp;&nbsp&nbsp; &nbsp;&nbsp;&nbsp;&nbsp; 
            <asp:DropDownList ID="DRP_REGON" runat="server" Height="29px" Width="126px">
            </asp:DropDownList>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <br />
            <br />
            <asp:Label ID="Label2" runat="server" Text="Name"></asp:Label>
            &nbsp;&nbsp &nbsp&nbsp&nbsp &nbsp&nbsp&nbsp; <asp:TextBox ID="TXT_NAME" runat="server"></asp:TextBox>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Label ID="Label3" runat="server" Text="Street"></asp:Label>
            &nbsp;&nbsp; &nbsp; &nbsp;&nbsp; &nbsp;&nbsp;
            <asp:TextBox ID="TXT_STRT" runat="server"></asp:TextBox>
            <br />
            <br />
            <asp:Label ID="Label8" runat="server" Text="Phone"></asp:Label>
            &nbsp;&nbsp &nbsp&nbsp&nbsp &nbsp&nbsp&nbsp; <asp:TextBox ID="TXT_PHONE" runat="server"></asp:TextBox>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="Label5" runat="server" Text="Buid NO"></asp:Label>
            &nbsp; &nbsp; &nbsp; <asp:TextBox ID="TXT_BULD" runat="server"></asp:TextBox>
            <br />
            <br />
            <asp:Label ID="Label1" runat="server" Text="Email"></asp:Label>
            &nbsp;&nbsp &nbsp&nbsp&nbsp &nbsp&nbsp&nbsp; <asp:TextBox ID="TXT_MAIL" runat="server"></asp:TextBox>
            <br />
            <br />
            <asp:Label ID="Label22" runat="server" Text="Type"></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;
            <asp:CheckBox ID="CHK_PARTNER" runat="server" Text="partner"/>
&nbsp;&nbsp;&nbsp;
            <asp:CheckBox ID="CHK_DIST" runat="server" Text="distributor" />
            <br />
            <br />
            <asp:Label ID="Label9" runat="server" Text="Working Hours"></asp:Label>      
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
            <table class="auto-style1">
                <tr>
                    <td class="auto-style3">
                        <asp:Button ID="Button1" runat="server" Text="Submit" OnClick="Button1_Click" />
                    </td>
                    
                    <td class="auto-style5">
                         <asp:Button ID="Button4" runat="server" Text="Dispaly" OnClick="Button4_Click"/>
                    </td>
                </tr>
            </table>
            <br />
            <br />
            <asp:GridView ID="GridView1" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" AutoGenerateColumns="False" OnRowCancelingEdit="GridView1_RowCancelingEdit" OnRowDeleting="GridView1_RowDeleting" OnRowEditing="GridView1_RowEditing" OnRowUpdating="GridView1_RowUpdating">
                <AlternatingRowStyle BackColor="White" />
                <Columns>
                    <asp:TemplateField HeaderText="ID">
                        <EditItemTemplate>
                            <asp:Label ID="Label13" runat="server" Text='<%# Bind("id") %>'></asp:Label>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label12" runat="server" Text='<%# Bind("id") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Name">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("name") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label15" runat="server" Text='<%# Bind("name") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Region_Name">
                        <EditItemTemplate>
                            <asp:DropDownList ID="DRP_REGION_GRID" runat="server" DataSourceID="SqlDataSource1" DataTextField="name" DataValueField="id"></asp:DropDownList>
                            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:DentalConnectionString2 %>" SelectCommand="SELECT [id], [name] FROM [regions]"></asp:SqlDataSource>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label16" runat="server" Text='<%# Bind("region") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Street">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("street") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label17" runat="server" Text='<%# Bind("street") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Buid_Number">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox4" runat="server" Text='<%# Bind("buid_number") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label18" runat="server" Text='<%# Bind("buid_number") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Phone">
                        <EditItemTemplate>
                            <asp:TextBox ID="TextBox5" runat="server" Text='<%# Bind("phone") %>'></asp:TextBox>
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label19" runat="server" Text='<%# Bind("phone") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="From">
                        <EditItemTemplate>
                            <asp:DropDownList ID="DropDownList1" runat="server">
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
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label20" runat="server" Text='<%# Bind("wh_from") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="To">
                        <EditItemTemplate>
                            <asp:DropDownList ID="DropDownList2" runat="server">
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
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label ID="Label21" runat="server" Text='<%# Bind("wh_to") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:CommandField ShowEditButton="True" />
                    <asp:CommandField ShowDeleteButton="True" />
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
         </div>
</asp:Panel>
                    </asp:Content>


