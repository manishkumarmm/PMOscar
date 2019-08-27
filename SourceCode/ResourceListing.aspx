<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/PMOMaster.master" CodeBehind="ResourceListing.aspx.cs" Inherits="PMOscar.ResourceListing" %>

<asp:Content ContentPlaceHolderID="cntBody" runat="server">

    <script type="text/javascript">
        function Confirm(ResourceId) {
            var val = $('.deactivate').html();            
            if (val == 'Deactivate')
            {
                var confirm_value = document.createElement("INPUT");
                confirm_value.type = "hidden";
                confirm_value.name = "confirm_value";
                if (confirm("Are you sure you want to deactivate?")) {
                    confirm_value.value = "Yes";
                }
                else {
                    confirm_value.value = "No";
                }
                document.forms[0].appendChild(confirm_value);
                var resourceId = document.createElement("INPUT");
                resourceId.type = "hidden";
                resourceId.name = "resourceId";
                resourceId.value = ResourceId;
                document.forms[0].appendChild(resourceId);
            }
            else {
                var confirm_value = document.createElement("INPUT");
                confirm_value.type = "hidden";
                confirm_value.name = "confirm_value";
                if (confirm("Are you sure you want to activate?")) {
                    confirm_value.value = "Yes";
                }
                else {
                    confirm_value.value = "No";
                }
                document.forms[0].appendChild(confirm_value);
                var resourceId = document.createElement("INPUT");
                resourceId.type = "hidden";
                resourceId.name = "resourceId";
                resourceId.value = ResourceId;
                document.forms[0].appendChild(resourceId);
            }
            
        }
    </script>

    <div style="width: 100%;">
        <style type="text/css" media="screen">
   
                BODY {  width:100%}
     </style>
        <table width="50%">
            <tr>
                <td align="left" style="font-size: 15px; border-bottom: 1px solid #ccc;">
                    <b>Resources</b></td>

            </tr>

            <tr>
                <td>
                    <table style="width: 600px;">
                        <tr>
                            <td style="width: 260px;">&nbsp;&nbsp;
                                    <asp:Label ID="lbMessage" runat="server" ForeColor="Green" Text="Label"></asp:Label>
                            </td>
                            <td style="width: 200px;" align="right">
                                <asp:Button ID="btnAdd" runat="server" Text="Add" OnClick="btnAdd_Click" TabIndex="1" />
                            </td>

                            <td style="width: 200px;" align="right">
                                <asp:Label ID="lblStatus" Text="Status" runat="server">               
                                </asp:Label>
                                <asp:DropDownList ID="ddlStatus" runat="server"
                                    AutoPostBack="True" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged"
                                    TabIndex="2" Width="100px" CssClass="dropedownbox">
                                    <asp:ListItem Selected="True" Value="0">Active</asp:ListItem>
                                    <asp:ListItem Value="1">Inactive</asp:ListItem>
                                    <asp:ListItem Value="2">All</asp:ListItem>
                                </asp:DropDownList>

                            </td>
                        </tr>
                    </table>
                </td>
            </tr>

            <tr>
                <td>
                    <asp:GridView ID="dgResourceDetails" runat="server"
                        AutoGenerateColumns="False" Width="600px" CellPadding="4"
                        ForeColor="#333333" TabIndex="2" AllowSorting="true" OnRowDataBound="dgResourceDetails_RowDataBound" OnSorting="dgResourceDetails_Sorting">

                        <RowStyle BackColor="#EFF3FB" />

                        <Columns>
                           <asp:BoundField HeaderText="#" />
                           <asp:BoundField HeaderText="ResourceId" DataField = "ResourceId"/>
                            <asp:TemplateField HeaderText="Resource Name" SortExpression="ResourceName">
                                <ItemStyle Width="20%"></ItemStyle>
                                <ItemTemplate>
                                    <asp:HyperLink ID="hplink" runat="server" NavigateUrl='<%# "Resource.aspx?ResEditId=" + 
                                         DataBinder.Eval(Container.DataItem,"ResourceId")+ " &ResourceName=" + DataBinder.Eval(Container.DataItem,"ResourceName") %>'><%#Eval("ResourceName")%></asp:HyperLink>
                                </ItemTemplate>
                            </asp:TemplateField>



                            <asp:BoundField HeaderText="Role" DataField="Role" SortExpression="Role" />
                            <asp:BoundField HeaderText="Team" DataField="Team" SortExpression="Team" />
                            <asp:BoundField HeaderText="Status" DataField="Status1" SortExpression="Status1" />




                            <asp:TemplateField>
                                <ItemStyle Width="5%"></ItemStyle>
                                <ItemTemplate>
                                    <asp:LinkButton Id="aResourceDelete" CssClass="deactivate" runat="server" OnClick="OnConfirm" OnClientClick='<%# DataBinder.Eval(Container.DataItem, "ResourceId", "Confirm({0});") %>'></asp:LinkButton>                                  
                                </ItemTemplate>
                            </asp:TemplateField>

                        </Columns>

                        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <HeaderStyle BackColor="#368ac8" Font-Bold="True" ForeColor="White" />
                        <EditRowStyle BackColor="#2461BF" />
                        <AlternatingRowStyle BackColor="White" />

                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td align="center">&nbsp;&nbsp;
                </td>
            </tr>
        </table>
    </div>

</asp:Content>
