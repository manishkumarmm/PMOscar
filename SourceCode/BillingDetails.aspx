<%@ Page Language="C#" MasterPageFile="~/PMOMaster.master" AutoEventWireup="true" CodeBehind="BillingDetails.aspx.cs" Inherits="PMOscar.BillingDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cntBody" runat="server">
    <script type="text/javascript" charset="utf-8">
        $(document).ready(function () {
            $('#divLoading').hide();

            function FreezeProject() {
                if (confirm('Are you sure you want to Finalize the Project?')) {
                    $('#divLoading').show();
                    $.ajax({
                        type: "POST",
                        url: "BillingDetails.aspx/FreezeProject",
                        data: "{year:" + <%= ddlYear.SelectedValue %> + ", month:" + <%= ddlMonth.SelectedValue %> +"}",
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        async: true,
                        cache: false,
                        success: function (result) {
                            $('#projIdDiv' + projectId).css('display', 'none');
                            $('#projIdSpan' + projectId).css('display', 'block');

                            $('#divLoading').hide();
                        },
                        error: function (result) {
                            $('#divLoading').hide();
                        }

                    });
                }
            }
        });
    </script>
    <div style="width: 100%;">
         <style type="text/css" media="screen">
   
                BODY {  width:100%}
     </style>
        <table width="50%">
            <tr>
                <td colspan="2">
                    <table width="100%">
                        <tr>
                            <td align="left" colspan="11" style="font-size: 15px; border-bottom: 1px solid #ccc;">
                                <b>Project Billing</b>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="9" align="right" style="padding-top:5px;">
                                <asp:LinkButton ID="lnkInvoiceSummary" Text="Invoice Summary" runat="server" OnClick="lnkInvoiceSummary_Click"></asp:LinkButton>
                            </td>
                        </tr>
                        <tr>

                            <td align="left" style="height: 35px; width: 50px">
                                <asp:Label ID="lblMonth" runat="server" Text="Month:"></asp:Label>
                            </td>
                            <td align="left" style="height: 5px; width: 80px">

                                <asp:DropDownList ID="ddlMonth" runat="server" Height="20px">
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

                            <td align="center" style="height: 5px; width: 60px">
                                <asp:Label ID="lblYear" runat="server" Text="Year:"></asp:Label>
                            </td>
                            <td align="left" style="height: 5px; width: 67px">
                                <asp:DropDownList ID="ddlYear" runat="server" Height="20px">
                                </asp:DropDownList>
                            </td>
                            <td align="left">
                                <asp:Button ID="btnGo" Text="Go" runat="server" OnClick="btn_Go" align="right" Width="30px" />
                            </td>
                            <td colspan="4"></td>
                        </tr>
                        <tr>
                            <td colspan="5" style="width: 174px">
                                <asp:Label ID="lblMsg" runat="server" ForeColor="#009900"></asp:Label>

                            </td>


                            <td style="width: 81px">
                                <asp:Button Text="Billing Details" runat="server" ID="Button1" OnClick="btnAdd_Click" align="right" />
                            </td>
                            <td style="width: 43px">
                                <asp:Button Text="Export" runat="server" ID="btnExport" align="left" OnClick="btnExport_Click" />
                            </td>
                            <td align="left" style="width: 120px">
                                <asp:Button ID="btnFinalize" Text="Finalize" runat="server" OnClientClick="return confirm('Do you really want to finalize project billing for the selected month? ');" OnClick="btn_Finalize" align="left" Width="50px" />
                            </td>
                            <td align="left"></td>
                            <td>
                                <asp:Label ID="lblerrors" runat="server" ForeColor="Red" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="6" align="right"></td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="gdBillingDetails" runat="server" AutoGenerateColumns="false" EmptyDataText="No Records Found" ShowFooter="true"
                        Width="800px" CellPadding="5" OnRowCreated="Grview_RowCreated" OnRowDataBound="Grview_RowDataBound" AllowSorting="true" OnSorting="gdBillingDetails_Sorting">
                        <Columns>
                            <asp:BoundField HeaderText="#" />
                            <asp:TemplateField HeaderText="Project Name" SortExpression="ProjectName">
                                <HeaderStyle ForeColor="#FCFF00" />
                                <ItemTemplate>
                                    <asp:HyperLink ID="lnkProject" runat="server"
                                        NavigateUrl='<%# "AddBillingDetails.aspx?projectId="+ DataBinder.Eval(Container.DataItem,"ProjectID") +"&month="+ ddlMonth.SelectedValue + "&year="+ ddlYear.SelectedValue %>'><%#Eval("ProjectName")%> </asp:HyperLink>
                                    </div>
                            <asp:Label ID="lblWeeklyComments" runat="server"> </asp:Label>
                                </ItemTemplate>
                                <ItemStyle Width="200px" HorizontalAlign="left" />
                            </asp:TemplateField>

                            <asp:BoundField HeaderText="Month" DataField="month" SortExpression="month">
                                <HeaderStyle ForeColor="#FCFF00" Width="100px" />
                                <ItemStyle HorizontalAlign="left" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Project billable hours" DataField="totalBillableHrs" SortExpression="totalBillableHrs">
                                <HeaderStyle ForeColor="#FCFF00" Width="150px" />
                                <ItemStyle HorizontalAlign="right" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Actual billing till last month" DataField="totalBilledTillLastMonth" SortExpression="totalBilledTillLastMonth">
                                <HeaderStyle ForeColor="#FCFF00" Width="150px" />
                                <ItemStyle HorizontalAlign="right" />
                            </asp:BoundField>                            
                            <asp:BoundField HeaderText="Planned billing for current month" DataField="plannedTotal" SortExpression="plannedTotal">
                                <HeaderStyle ForeColor="#FCFF00" Width="150px" />
                                <ItemStyle HorizontalAlign="right" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText=" Actual spent this month" DataField="ActualSpentThisMonth" SortExpression="ActualSpentThisMonth">
                                <HeaderStyle ForeColor="#FCFF00" Width="150px" />
                                <ItemStyle HorizontalAlign="right" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Actual billing for current month" DataField="actualTotal" SortExpression="actualTotal">
                                <HeaderStyle ForeColor="#FCFF00"  Width="150px"/>
                                <ItemStyle HorizontalAlign="right" />
                            </asp:BoundField>
                            <asp:BoundField HeaderText="Total Billed" DataField="totalBilled" SortExpression="totalBilled">
                                <HeaderStyle ForeColor="#FCFF00" Width="150px" />
                                <ItemStyle HorizontalAlign="right" />
                            </asp:BoundField>
                        </Columns>
                        <FooterStyle BackColor="#368ac8" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                        <HeaderStyle BackColor="#368ac8" Font-Bold="True" ForeColor="White" />
                        <EditRowStyle BackColor="#2461BF" />
                        <AlternatingRowStyle BackColor="White" />
                    </asp:GridView>

                    &nbsp;</td>

            </tr>

            <tr>
                <td align="center">&nbsp;&nbsp;
                </td>
            </tr>

        </table>

    </div>
    <div id="divLoading" class="ajax-loader">
        <img src="Images/loader.gif" alt="" />
    </div>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="cntHead" runat="server">
</asp:Content>
