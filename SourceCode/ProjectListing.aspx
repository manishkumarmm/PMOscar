<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/PMOMaster.master" CodeBehind="ProjectListing.aspx.cs" Inherits="PMOscar.ProjectListing" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cntBody" runat="server">
    <script type="text/javascript">
        function Confirm(ProjectId) {
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
                var projectId = document.createElement("INPUT");
                projectId.type = "hidden";
                projectId.name = "ProjectId";
                projectId.value = ProjectId;
                document.forms[0].appendChild(projectId);
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
                var projectId = document.createElement("INPUT");
                projectId.type = "hidden";
                projectId.name = "ProjectId";
                projectId.value = ProjectId;
                document.forms[0].appendChild(projectId);
            }
            
        }
    </script>
     
    <div style="width: 99%;">
        <style type="text/css" media="screen">
   
                BODY { margin: 0px; padding: 0; font: 1em "Trebuchet MS", verdana, arial, sans-serif; font-size: 100%; width:100%}
     </style>
        <table width="100%">
            <tr>
                <td align="left" colspan="4"
                    style="font-size: 15px; border-bottom: 1px solid #ccc; width: 1000px;">
                    <b>Projects</b></td>

            </tr>
            <tr>
                <td align="center" style="width: 854px">
                    <asp:Label ID="lbMessage" runat="server" ForeColor="Green" Text="Label"></asp:Label>
                </td>
                <td>

                    <asp:Button ID="btnAdd" runat="server" Text="Add" OnClick="btnAdd_Click"
                        TabIndex="1" />
                </td>
                <td align="right" style="height: 26px">
                    <table>
                        <tr>
                            <td>
                                <asp:Label ID="lblStatus" Text="Status" runat="server">               
                                </asp:Label>
                                <asp:DropDownList ID="ddlStatus" runat="server"
                                    AutoPostBack="True" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged"
                                    TabIndex="2" Width="100px" CssClass="dropedownbox">
                                    <asp:ListItem Selected="True" Value="0">My Projects</asp:ListItem>
                                    <asp:ListItem Value="1">Active</asp:ListItem>
                                    <asp:ListItem Value="2">Inactive</asp:ListItem>
                                    <asp:ListItem Value="3">All</asp:ListItem>

                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>


                </td>

            </tr>

            <tr>
                <td style="width: 100%" colspan="3">
                    <asp:GridView ID="dgProjectDetails" runat="server" ShowFooter="true"
                        AutoGenerateColumns="False" Width="100%" CellPadding="4" AllowSorting="True"
                        ForeColor="#333333" TabIndex="2" OnRowDataBound="Grview_RowDataBound"
                        OnSorting="Grview_Sorting" OnRowCreated="Grview_RowCreated">

                        <RowStyle BackColor="#EFF3FB" />

                        <Columns>
                            <asp:BoundField HeaderText="#" />
                            <asp:BoundField HeaderText="ProjectId" DataField="ProjectId" />
                            <asp:TemplateField HeaderText="Project Name" SortExpression="ProjectName">
                                <ItemStyle Width="17%"></ItemStyle>
                                <ItemTemplate>
                                    <asp:HyperLink ID="hplink" runat="server" NavigateUrl='<%# "Project.aspx?ProjectEditId=" + 
                                         DataBinder.Eval(Container.DataItem,"ProjectId")+ " &mode=Edit" %>'><%#Eval("ProjectName")%></asp:HyperLink>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField HeaderText="Category" DataField="Utilization" SortExpression="Utilization" HeaderStyle-Width="95px" />
                            <asp:BoundField HeaderText="Project Type" DataField="ProjectType" SortExpression="ProjectType" />
                            <asp:BoundField HeaderText="Current Phase" DataField="CurrentPhase" SortExpression="CurrentPhase" />
                            <asp:BoundField HeaderText="Client Name" DataField="Clientname" SortExpression="Clientname" />
                            <asp:BoundField HeaderText="Program Name" DataField="ProgName" SortExpression="ProgName" />
                            <asp:BoundField HeaderText="Project Owner" DataField="ProjOwner" SortExpression="ProjOwner" />
                            <asp:BoundField HeaderText="Project Manager" DataField="ProjManager" SortExpression="ProjManager" />
                            <asp:BoundField HeaderText="Billable Hrs"
                                DataField="BillableHours" SortExpression="BillableHours"
                                ControlStyle-Width="100px">
                                <ControlStyle Width="100px"></ControlStyle>
                                <ItemStyle Width="100px" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Budget Hrs" DataField="BudgetHours"
                                SortExpression="BudgetHours" ControlStyle-Width="100px">
                                <ControlStyle Width="100px"></ControlStyle>
                                <ItemStyle Width="100px" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Revised Budget Hrs"
                                DataField="RevisedBudgetHours" SortExpression="RevisedBudgetHours"
                                ControlStyle-Width="100px">
                                <ControlStyle Width="100px"></ControlStyle>
                                <ItemStyle Width="100px" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Actual Hrs" DataField="ActualHours"
                                SortExpression="ActualHours">
                                <ItemStyle Width="100px" />
                            </asp:BoundField>                          
                            <asp:BoundField HeaderText="Status" DataField="Status" SortExpression="Status" />
                            <asp:TemplateField>
                                <ItemStyle Width="5%"></ItemStyle>
                                <ItemTemplate>
                                    <asp:LinkButton id="aProjectDelete" CssClass="deactivate" runat="server" OnClick="OnConfirm" OnClientClick='<%# DataBinder.Eval(Container.DataItem, "ProjectId", "Confirm({0});") %>'></asp:LinkButton>                                    
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle BackColor="#368ac8" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <HeaderStyle BackColor="#539cd1" Font-Bold="True" ForeColor="White"
                            BorderColor="Black" />
                        <EditRowStyle BackColor="#2461BF" />
                        <AlternatingRowStyle BackColor="White" />
                        <EmptyDataTemplate>
                            <div style="text-align: center;">
                                No Records Found
                            </div>
                        </EmptyDataTemplate>
                    </asp:GridView>
                </td>
            </tr>

        </table>
    </div>

</asp:Content>

