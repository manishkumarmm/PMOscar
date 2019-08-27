<%@ Page Language="C#" MasterPageFile="~/PMOMaster.master" AutoEventWireup="true"
    CodeBehind="AddBillingDetails.aspx.cs" Inherits="PMOscar.AddBillingDetails" %>

<asp:Content ContentPlaceHolderID="cntBody" runat="server">
    <div style="width: 100%;">
         <style type="text/css" media="screen">
   
                BODY {  width:100%}
     </style>
        <table>
            <table width="63%">
                <tr>
                    <td align="left" colspan="16" style="font-size: 15px; border-bottom: 1px solid #ccc;">
                        <b>Billing Details</b>
                    </td>
                </tr>
                <tr>
                    <td colspan="1"></td>
                    <td colspan="1"></td>
                    <td colspan="4"></td>

                    <td align="right" colspan="3">
                        <asp:LinkButton ID="lbBack" Text="<< Back to Project Billing" runat="server"
                            OnClick="lbBack_Click" CausesValidation="false" TabIndex="6"></asp:LinkButton></td>
                </tr>

                <tr>
                    <td style="height: 10px"></td>
                </tr>
                <tr align="center" style="width: 200px">
                    <td style="width: 130px"></td>
                    <td align="left" style="width: 30px">
                        <asp:Label ID="Label5" runat="server" Text="Month:"></asp:Label>
                    </td>
                    <td align="left" style="width: 60px">
                        <asp:DropDownList ID="ddlMonth" runat="server" AutoPostBack="True"  OnSelectedIndexChanged="ddlmonth_SelectedIndexChanged" Height="20px" TabIndex="1">
                            <asp:ListItem Value="1">January</asp:ListItem>
                            <asp:ListItem Value="2">February</asp:ListItem>
                            <asp:ListItem Value="3">March</asp:ListItem>
                            <asp:ListItem Value="4">April</asp:ListItem>
                            <asp:ListItem Value="5">May</asp:ListItem>
                            <asp:ListItem Value="6">June</asp:ListItem>
                            <asp:ListItem Value="7">July</asp:ListItem>
                            <asp:ListItem Value="8">August</asp:ListItem>
                            <asp:ListItem Value="9">September</asp:ListItem>
                            <asp:ListItem Value="10">October</asp:ListItem>
                            <asp:ListItem Value="11">November</asp:ListItem>
                            <asp:ListItem Value="12">December</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td align="left" style="width: 10px">
                        <asp:Label ID="Label4" runat="server" Text="Year:"></asp:Label>
                    </td>
                    <td class="dropdown" width="40px;">
                        <asp:DropDownList ID="ddlYear" AutoPostBack="True"  OnSelectedIndexChanged="ddlyear_SelectedIndexChanged"   runat="server" Height="20px" TabIndex="2">
                        </asp:DropDownList>
                    </td>
                    <td align="left" style="width: 50px">
                        <asp:Label ID="lblProject" runat="server" Text="Project:"></asp:Label>
                    </td>

                    <td align="left">
                        <asp:DropDownList ID="ddlProjects" runat="server" TabIndex="3">
                        </asp:DropDownList>
                    </td>

                    <td align="left">
                        <asp:Button ID="btnGo" Text="Go" runat="server" TabIndex="4" OnClick="btnGo_Click" />
                    </td>
                    <td style="width: 200px"></td>

                </tr>
                <tr>
                    <td colspan="6"></td>
                    <td align="left">
                        <asp:RequiredFieldValidator ID="rfvProject" ControlToValidate="ddlProjects" InitialValue="0"
                            runat="server" ErrorMessage="Select Project"></asp:RequiredFieldValidator>
                    </td>
                    <td style="width: 20px"><%--<asp:Button ID="Button1" Text="Add/Edit Details" runat="server" TabIndex="5" OnClick="btn_addEdit" />--%></td>
                </tr>
                <tr>
                    <td colspan="7"></td>
                    <td style="width: 20px">
                        <asp:Button ID="btnAddEdit" Text="Add/ Edit Details" runat="server" TabIndex="5" OnClick="btn_addEdit" /></td>
                </tr>
            </table>

            <table width="60%">

                <tr>
                    <td style="width: 50px"></td>

                    <td colspan="20" align="left">
                        <asp:GridView ID="gdResources" runat="server" AutoGenerateColumns="false" Width="81%" EmptyDataText="No Records Found" ShowFooter="true"
                            CellPadding="5" OnRowCreated="Grview_RowCreated" TabIndex="-1" OnRowDataBound="Grview_RowDataBound" AllowSorting="true" OnSorting="gdResources_Sorting">
                            <Columns>
                                <asp:BoundField HeaderText="#">
                                    <ItemStyle HorizontalAlign="left" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Role" DataField="Role" SortExpression="Role">
                                    <HeaderStyle ForeColor="#FCFF00" />
                                    <ItemStyle HorizontalAlign="left" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Planned Billing" DataField="p" SortExpression="p">
                                    <HeaderStyle ForeColor="#FCFF00" />
                                    <ItemStyle HorizontalAlign="right" Width="15%"/>
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Actual Billing" DataField="a" SortExpression="a">
                                    <HeaderStyle ForeColor="#FCFF00" />
                                    <ItemStyle HorizontalAlign="right" Width="15%"/>
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Actual Spent" DataField="ActualSpentHours" SortExpression="ActualSpentHours">
                                    <HeaderStyle ForeColor="#FCFF00" />
                                    <ItemStyle HorizontalAlign="right" Width="15%" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="UBT" DataField="UBT" SortExpression="UBT">
                                    <HeaderStyle ForeColor="#FCFF00" />
                                    <ItemStyle HorizontalAlign="right" Width="15%" />
                                </asp:BoundField>
                            </Columns>
                            <FooterStyle BackColor="#368ac8" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#368ac8" Font-Bold="True" ForeColor="White" />
                            <EditRowStyle BackColor="#2461BF" />
                            <AlternatingRowStyle BackColor="White" />
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td colspan="8">&nbsp;
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;
                    </td>
                    <td>&nbsp;
                    </td>
                    <td>&nbsp;
                    </td>
                    <td>&nbsp;
                    </td>
                </tr>
            </table>
        </table>
    </div>

</asp:Content>
<asp:Content ContentPlaceHolderID="cntHead" runat="server">
</asp:Content>
