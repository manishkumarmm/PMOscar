<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/PMOMaster.master" CodeBehind="InvoiceSummary.aspx.cs" Inherits="PMOscar.InvoiceSummary" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cntBody" runat="server">
    <div style="width: 100%;">
        <table width="60%">
            <tr>
                <td>
                    <table width="100%">
                        <tr>
                            <td align="left" colspan="2" style="font-size: 15px; border-bottom: 1px solid #ccc;">
                                <b>Project Billing</b>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" align="right" style="padding-top: 8px;"></td>
                        </tr>
                        <tr>
                            <td align="center" style="height: 35px;" colspan="2">
                                <table>
                                    <tr>
                                        <td>
                                            <asp:label id="lblStatus" text="Project: " runat="server" />
                                            <asp:dropdownlist id="ddlProject" runat="server"
                                                autopostback="True"
                                                tabindex="2" width="150px" cssclass="dropedownbox" onselectedindexchanged="ddlProject_SelectedIndexChanged" />
                                        </td>
                                        <td style="padding-left:5px;">
                                            <asp:label id="Label1" text="Year: " runat="server" />
                                            <asp:dropdownlist id="ddlYear" runat="server"
                                                autopostback="True"
                                                tabindex="2" width="100px" cssclass="dropedownbox" onselectedindexchanged="ddlYear_SelectedIndexChanged" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2"></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td></td>
            </tr>
            <tr>
                <td>
                    <asp:gridview id="dgInvoiceSummary" runat="server" showfooter="true"
                        autogeneratecolumns="False" width="100%" cellpadding="4" allowsorting="True"
                        forecolor="#333333" tabindex="2">
                        <RowStyle BackColor="#EFF3FB" />
                        <Columns>
                            <asp:BoundField HeaderText="Role" DataField="Role" />
                            <asp:BoundField HeaderText="Planned Billable" DataField="PlannedBillable" />
                            <asp:BoundField HeaderText="Actual Billable" DataField="ActualBillable" SortExpression="ActualBillable" />
                            <asp:BoundField HeaderText="Actual Spent" DataField="ActualSpent" SortExpression="ActualSpent" />
                            <asp:BoundField HeaderText="UBT" DataField="UBT" SortExpression="UBT" />
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
                    </asp:gridview>
                </td>
            </tr>
            <tr>
                <td></td>
            </tr>
        </table>
    </div>
</asp:Content>

