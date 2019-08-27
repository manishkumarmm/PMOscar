<%@ Page Title="" Language="C#" MasterPageFile="~/PMOMaster.master" AutoEventWireup="true" CodeBehind="UserListing.aspx.cs" Inherits="PMOscar.UserListing" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cntBody" runat="server">
    
<script type="text/javascript">
       function Confirm(UserId) {
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
                var userid = document.createElement("INPUT");
                userid.type = "hidden";
                userid.name = "userid";
                userid.value = UserId;
                document.forms[0].appendChild(userid);
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
                var userid = document.createElement("INPUT");
                userid.type = "hidden";
                userid.name = "userid";
                userid.value = UserId;
                document.forms[0].appendChild(userid);
            }
            
        }
    </script>
    <div style="width:100%;">
         <style type="text/css" media="screen">
   
                BODY {  width:100%}
     </style>
             <table width = "60%">
            <tr>
              <td align ="left" style="font-size:15px; border-bottom:1px solid #ccc;">
                   <b>Users</b></td>                 
            </tr>
            
             <tr>
                 <td>
                     <table width="850px">
                         <tr>
                            <td style="width:273px;">&nbsp;&nbsp;
                                <asp:Label ID="lbMessage" runat="server" ForeColor="Green" Text="Label"></asp:Label>
                            </td>
                             <td style="width:273px;" align="right">
                                <asp:Button ID="btnAdd" runat="server" Text="Add" onclick="btnAdd_Click" />
                            </td>
                             
                            <td style="width:273px;" align="right">
                                <asp:Label ID="lblStatus" Text="Status"  runat="server">               
                                </asp:Label>
                                <asp:DropDownList ID="ddlStatus" runat="server" 
                                    AutoPostBack="True" onselectedindexchanged="ddlStatus_SelectedIndexChanged"
                                     TabIndex="2" Width="100px"  CssClass="dropedownbox">
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
                    <asp:GridView ID="dgusers" runat="server" Width="850px" 
                        AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" OnRowDataBound="dgusers_RowDataBound"  AllowSorting="true" OnSorting="dgusers_Sorting">
                    
                        <RowStyle BackColor="#EFF3FB" />
                    
                     <Columns>
                     <asp:BoundField HeaderText="#" DataField = "UserId" SortExpression="UserId" />                               
                                                                     
                                                                       
                                      <asp:TemplateField HeaderText="Name" SortExpression="FirstName">
                                           <ItemStyle Width="20%"></ItemStyle>
                                            <ItemTemplate>
                                         <asp:HyperLink id ="hplink" runat="server" NavigateUrl ='<%# "UserEdit.aspx?UserEditId=" + 
                                         DataBinder.Eval(Container.DataItem,"UserId")+ " &FirstName=" + DataBinder.Eval(Container.DataItem,"FirstName") %>'> <%#Eval("FirstName")%></asp:HyperLink>                                     
                                             </ItemTemplate>                                             
                                         </asp:TemplateField> 
                                     
                                     
                                     
                                     
                                     <asp:BoundField HeaderText="Name" DataField = "FirstName" SortExpression="FirstName"/>                                      
                                      <asp:BoundField HeaderText="Role" DataField = "UserRole" SortExpression="UserRole"/>   
                                      <asp:BoundField HeaderText="Status" DataField = "Status" SortExpression="Status"/>                                      
                                  
                                                                        
                                         <asp:TemplateField>
                                          <ItemStyle Width="5%"></ItemStyle>
                                            <ItemTemplate>
                                     <asp:LinkButton id="aResourceDelete" CssClass="deactivate" runat="server" OnClick="OnConfirm" OnClientClick='<%# DataBinder.Eval(Container.DataItem, "UserId", "Confirm({0});") %>'></asp:LinkButton>
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
           
        </table>
    </div>
</asp:Content>