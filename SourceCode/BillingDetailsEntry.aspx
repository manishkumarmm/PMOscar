<%@ Page Language="C#" MasterPageFile="~/PMOMaster.master" AutoEventWireup="true"
    CodeBehind="BillingDetailsEntry.aspx.cs" Inherits="PMOscar.BillingDetailsEntry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="TimeTrackerAudit.ascx" TagName="TimeTrackerAudit" TagPrefix="uc1" %>
<asp:Content ContentPlaceHolderID="cntHead" runat="server">
    <link href="Style/jquery-ui-1.8.12.custom.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/jquery-1.5.1.min.js" type="text/javascript"></script>
    <script src="Scripts/jquery-ui-1.8.12.custom.min.js" type="text/javascript"></script>
    <style>
        /* This is the style for the trigger icon. The margin-bottom value causes the icon to shift down to center it. */
        .ui-datepicker-trigger
        {
            margin-left: 5px;
            margin-top: 0px;
            margin-bottom: -3px;
        }
        .auto-style1 {
            height: 25px;
        }
    </style>
    <div id="divNote" style="background-color: #fdfdd6; padding: 5px 10px 10px 10px;
        text-align: left; position: absolute; border: solid 1px black; visibility: hidden;
        white-space: normal; z-index: 1000; max-width: 700px; word-wrap: break-word;
        max-height: 700px; overflow-y: scroll;">
    </div>
    <script type="text/javascript" language="javascript">
        function showPopup(comment, e) {
            var posx = 0;
            var posy = 0;
            e = (window.event) ? window.event : e;
            posx = e.clientX + document.body.scrollLeft + document.documentElement.scrollLeft;
            posy = e.clientY + document.body.scrollTop + document.documentElement.scrollTop;

            var finalString = "<strong><span onclick=\"HideNotes()\" onmouseover=\"this.style.cursor='pointer';\" style=\"color: red;padding-left:0px;\">Close</span></strong><br /><br />"

            // Split a classnote into array by space
            var words = comment.split(' ')

            // Reset classnote = Empty
            comment = '';
            // Replace double quote(") to single quote (') through LOOP
            for (i = 0; i < words.length; i++) {
                comment = comment + words[i].replace('""', "'") + ' ';
            }

            finalString += comment

            document.getElementById("divNote").innerHTML = finalString;
            document.getElementById("divNote").style.visibility = "visible";
            document.getElementById("divNote").style.top = posy + "px";
            document.getElementById("divNote").style.left = (posx) + "px";
            document.getElementById("divNote").style.width = "auto";
            document.getElementById("divNote").style.height = "auto";
            document.getElementById("divNote").style.textAlign = "left";
        }
        function HideNotes() {

            document.getElementById("divNote").innerHTML = "";
            document.getElementById("divNote").style.visibility = "hidden";
        }
    </script>
</asp:Content>
<asp:Content ContentPlaceHolderID="cntBody" runat="server">
    <script type="text/javascript">

        Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(function () {

            var s = document.getElementById('<%=mnthLabel.ClientID %>').value;  // get month from label 

            $("#<%=txtToDate.ClientID %>").datepicker({

                dateFormat: 'dd/mm/yy',
                defaultDate: '1/' + s,   // set default date


                buttonImage: "Images/calendar.jpg",
                buttonImageOnly: true,
                showOn: 'both',

                onSelect: function (selected) {

                    $(".ui-datepicker-trigger").mouseover(function () {
                        $(this).css('cursor', 'pointer');
                    });
                    CalculateActualSpentHours();
                }


            });


            $("#<%=txtFromDate.ClientID %>").datepicker({
                dateFormat: 'dd/mm/yy',
                defaultDate: '1/' + s,
                showOn: 'both',
                buttonImage: "Images/calendar.jpg",
                buttonImageOnly: true,

                onSelect: function (selected) {

                    $(".ui-datepicker-trigger").mouseover(function () {
                        $(this).css('cursor', 'pointer');
                    });
                    CalculateActualSpentHours();
                }


            });

            //hand icon 
            $(".ui-datepicker-trigger").mouseover(function () {
                $(this).css('cursor', 'pointer');
            });

            function CalculateActualSpentHours() {
                var resourceID = parseInt($('[id$="_ddlResources"]').val());
                var projectID = parseInt($('[id$="_hiddenProjectID"]').val());
                var fromDate = $('[id$="_txtFromDate"]').val();
                var toDate = $('[id$="_txtToDate"]').val();

                var fromDateParts = fromDate.split('/');
                var fromDay = parseInt(fromDateParts[0]);
                var fromMonth = parseInt(fromDateParts[1]);
                var fromYear = parseInt(fromDateParts[2]);

                var toDateParts = toDate.split('/');
                var toDay = parseInt(toDateParts[0]);
                var toMonth = parseInt(toDateParts[1]);
                var toYear = parseInt(toDateParts[2]);

                $.ajax({
                    type: "POST",
                    url: "BillingDetailsEntry.aspx/GetSumOfActualSpentHours",
                    data: "{projectID:" + projectID + ",resourceID:" + resourceID + ", fromMonth:" + fromMonth + ", fromDay:" + fromDay + ", fromYear:" + fromYear + ", toMonth:" + toMonth + ", toDay:" + toDay + ", toYear:" + toYear + "}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    async: true,
                    cache: false,
                    success: function (result) {
                        $('[id$="_txtActualSpent"]').val(result.d);
                        console.log(result);
                    },
                    error: function (result) {
                        console.log("error" + result);
                    }
                });
            }

            $("#<%=txtFromDate.ClientID %>").click(function () {
                CalculateActualSpentHours();
            });

            $("#<%=txtToDate.ClientID %>").click(function () {
                CalculateActualSpentHours();

            });
        });

        function isNumberKey(evt) {
            var charCode = (evt.which) ? evt.which : event.keyCode

            if (charCode == 45)
                return false;
            else
                return true;
        }

<%--        function ToggleValidator(chk) {
        var valName = document.getElementById("<%=rfvResources.ClientID%>");
            ValidatorEnable(valName, !chk.checked);
        }--%>

        function setAvailableHour(evt) {

            var charCode = (evt.which) ? evt.which : event.keyCode

            if (charCode == 45)
                return false;
            else {
                $('#ctl00_cntBody_txtAvailableHour').val(Number($('#ctl00_cntBody_txtPlanned').val()) - Number($('#ctl00_cntBody_txtActualSpent').val()));
                return true;

            }
        }


    </script>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div>
                <div style="width: 100%; text-align: right">
                     <style type="text/css" media="screen">
   
                BODY {  width:100%}
     </style>
                </div>
                <table id="Table1" runat="server" width="55%">
                    <tr>
                        <td align="left" style="font-size: 15px; border-bottom: 1px solid #ccc;">
                            <b>Add/Edit-Billing Details</b>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:LinkButton ID="lbBack" Text="<< Back To Billing Details" runat="server" OnClick="lbBack_Click"
                                CausesValidation="false" Height="20px"></asp:LinkButton>
                            <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td align="left" style="width: 550px">
                                        <div id="pageTitle">
                                            Project :
                                            <asp:Label ID="lblProject" runat="server" CausesValidation="False"></asp:Label>
                                        </div>
                                    </td>
                                    <td colspan="2">
                                        &nbsp;
                                    </td>
                                    <td height="25" style="width: 350px;">
                                        <div id="pageTitle">
                                            Total Project (Planned Billing) : &nbsp;
                                            <asp:Label ID="lblTotalPlanned" runat="server" CausesValidation="true"></asp:Label>
                                            <asp:Label ID="lblHours" runat="server" Visible="true"></asp:Label>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" style="width: 550px">
                                        <div id="pageTitle">
                                            Month :
                                            <asp:Label ID="lblmonth" runat="server" CausesValidation="False"></asp:Label>
                                            <asp:HiddenField ID="mnthLabel" runat="server" />
                                        </div>
                                    </td>
                                    <td colspan="2">
                                        &nbsp;
                                    </td>
                                    <td align="right" height="25" style="width: 350px">
                                        <div id="pageTitle">
                                            Total Project (Actual Spent) &nbsp; : &nbsp;
                                            <asp:Label ID="lblTotalActual" runat="server" CausesValidation="true"></asp:Label>
                                            <asp:Label ID="lblHidden" runat="server" Visible="true"></asp:Label>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <table width="100%">
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            <asp:GridView ID="dgResourceDetails" runat="server" ShowHeaderWhenEmpty="true" EmptyDataText="No Records"
                                OnRowDataBound="Grview_RowDataBound" Width="99%" CellPadding="5" AutoGenerateColumns="false"
                                ShowFooter="true" ForeColor="#333333" TabIndex="-1" AllowSorting="true" OnSorting="dgResourceDetails_Sorting">
                                <RowStyle BackColor="#EFF3FB" />
                                <Columns>
                                    <asp:BoundField HeaderText="Resources" DataField="ResourceName" SortExpression="ResourceName">
                                        <HeaderStyle ForeColor="#FCFF00" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Role" DataField="Role" SortExpression="Role">
                                        <HeaderStyle ForeColor="#FCFF00" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="From Date" DataField="fromDate" SortExpression="fromDate">
                                        <HeaderStyle ForeColor="#FCFF00" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="To Date" DataField="ToDate" SortExpression="ToDate">
                                        <HeaderStyle ForeColor="#FCFF00" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Planned Billing" DataField="PlannedHours" SortExpression="PlannedHours">
                                        <HeaderStyle ForeColor="#FCFF00" />
                                        <ItemStyle HorizontalAlign="right" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Actual Billing" DataField="ActualHours" SortExpression="ActualHours">
                                        <HeaderStyle ForeColor="#FCFF00" />
                                        <ItemStyle HorizontalAlign="right" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Actual Spent" DataField="ActualSpentHours" SortExpression="ActualSpentHours">
                                        <HeaderStyle ForeColor="#FCFF00" />
                                        <ItemStyle HorizontalAlign="right" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="UBT*" DataField="UBT" SortExpression="UBT">
                                        <HeaderStyle ForeColor="#FCFF00" />
                                    </asp:BoundField>
                                    <asp:BoundField HeaderText="Comments" DataField="Comments" SortExpression="Role">
                                        <HeaderStyle ForeColor="#FCFF00" />
                                    </asp:BoundField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:HyperLink ID="hplink" runat="server" NavigateUrl='<%# "BillingDetailsEntry.aspx?BillingEditID="+DataBinder.Eval(Container.DataItem,"BillingID")+"&ResourceEditId="+DataBinder.Eval(Container.DataItem,"ResourceID")+"&ProjectId="+DataBinder.Eval(Container.DataItem,"ProjectID")+"&RoleId="+DataBinder.Eval(Container.DataItem,"RoleID")+"&Month="+Request.QueryString["Month"]+"&Year="+Request.QueryString["year"]+"&opmode=Edit" %>'>Edit </asp:HyperLink>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <a id="hpDelete" runat="server" onclick="javascript: return confirm('Are you sure you want to delete?');"
                                                href='<%# "BillingDetailsEntry.aspx?BillingEditID="+DataBinder.Eval(Container.DataItem,"BillingID")+"&ResourceEditId="+DataBinder.Eval(Container.DataItem,"ResourceID")+"&ProjectId="+DataBinder.Eval(Container.DataItem,"ProjectID")+"&RoleId="+DataBinder.Eval(Container.DataItem,"RoleID")+" &Month="+Request.QueryString["Month"]+"&Year="+Request.QueryString["year"]+"&opmode=Delete" %>'>
                                                Delete</a>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <FooterStyle BackColor="#368ac8" Font-Bold="True" ForeColor="White" />
                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                <HeaderStyle BackColor="#368ac8" Font-Bold="True" ForeColor="White" />
                                <EditRowStyle BackColor="#2461BF" />
                                <AlternatingRowStyle BackColor="White" />
                            </asp:GridView>
                            *UBT-Unutilized Billable Time
                            <br />
                            <br />
                            <table width="100%" style="padding-top: 10px;">
                                <tr runat="server" id="trProjectBilling">
                                    <td>
                                        <table>
                                            <tr>
                                                <td>
                                                    Resource
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:DropDownList ID="ddlResources" runat="server" TabIndex="2" Style="width: 120px;"
                                                        AutoPostBack="True" OnSelectedIndexChanged="ddlSelectedIndexChanged_GetSumOfActualSpentHours">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td>
                                        <table>
                                            <tr>
                                                <td>
                                                    Role
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:DropDownList ID="ddlRole" runat="server" TabIndex="2" Enabled="False">
                                                    </asp:DropDownList>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td nowrap="nowrap">
                                        <table>
                                            <tr>
                                                <td>
                                                    Start Date
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtFromDate" runat="server" TabIndex="3" Text="From Date" Width="60px"
                                                        Height="16px" AutoPostBack="true" OnTextChanged="OnTextChanged_GetSumOfActualSpentHours"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td nowrap="nowrap">
                                        <table>
                                            <tr>
                                                <td>
                                                    End Date
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtToDate" runat="server" TabIndex="4" Text="To Date" Width="60px"
                                                        Height="16px" AutoPostBack="true" OnTextChanged="OnTextChanged_GetSumOfActualSpentHours"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td nowrap="nowrap">
                                        <table>
                                            <tr>
                                                <td>
                                                    Planned Billing
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtPlanned" runat="server" MaxLength="3" onkeyup="return setAvailableHour(event)"
                                                        Width="66px" TabIndex="5" Height="16px"></asp:TextBox><%--onkeypress="return isNumberKey(event)"--%>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td nowrap="nowrap">
                                        <table>
                                            <tr>
                                                <td>
                                                    Actual Billing
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtActual" runat="server" Text="0" MaxLength="3" 
                                                        Width="57px" TabIndex="6" Height="16px"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td nowrap="nowrap">
                                        <table>
                                            <tr>
                                                <td>
                                                    Actual Spent
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtActualSpent" runat="server" Text="0" MaxLength="3" onkeyup="return setAvailableHour(event)"
                                                        Width="50px" TabIndex="7" Height="16px"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td nowrap="nowrap">
                                        <table>
                                            <tr>
                                                <td>
                                                    Available Hours
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="auto-style1">
                                                    <asp:TextBox ID="txtAvailableHour" runat="server" Width="50px" TabIndex="7"
                                                        Height="16px" Enabled="false"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="ResourceValidation" Text="Select Resource" ForeColor="Red" Visible="false" runat="server"></asp:Label>
                                        <div id="dvresource" runat="server" visible="false">
                                            <asp:Label ID="lblResource" runat="server" ForeColor="Red" Text="There is already an entry for this resource with the same role."
                                                Visible="false"></asp:Label>
                                        </div>
                                    </td>
                                    <td>
                                        <asp:Label ID="roleValidation" Visible="false" ForeColor="Red" runat="server" 
                                            Text="Select Role."></asp:Label>
                                    </td>
                                    <td>
                                        <div id="dvError" runat="server" visible="false">
                                            <asp:Label ID="lblerror" runat="server" ForeColor="Red" Text="error." Visible="false"></asp:Label>
                                        </div>
                                        <asp:RequiredFieldValidator ID="rfvtxtFromDate" runat="server" ControlToValidate="txtFromDate"
                                            ErrorMessage="Select From Date." InitialValue="From Date"></asp:RequiredFieldValidator>
                                    </td>
                                    <td nowrap="nowrap" style="width: 110px">
                                        <div id="dvError2" runat="server" visible="false">
                                            <asp:Label ID="lblError2" runat="server" ForeColor="Red" Text="error." Visible="false"></asp:Label>
                                        </div>
                                        <asp:RequiredFieldValidator ID="rfvtxtToDate" runat="server" ControlToValidate="txtToDate"
                                            ErrorMessage="Select To Date." InitialValue="To Date"></asp:RequiredFieldValidator>
                                    </td>
                                    <td nowrap="nowrap" style="position:absolute; width:150px">
                                       
                                        <asp:Label ID="billableValidation" Visible="false" runat="Server" ForeColor="Red" Text="Enter Planned Billable Hrs."></asp:Label>
                                       
                                        <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtPlanned"
                                            Display="Dynamic" ErrorMessage="Enter Integer only." Operator="DataTypeCheck"
                                            Type="Integer" />
                                    </td>
                                    <td nowrap="nowrap" style="position:absolute;">   
                                         <asp:Label ID="lblplanned" runat="server" ForeColor="Red"></asp:Label>
                                        <br />                                     
                                        <asp:Label ID="lblActBilling" runat="server" ForeColor="Red"></asp:Label>
                                        <br />
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                                            ErrorMessage="Enter Integer Only." ControlToValidate="txtActual" ValidationExpression="\d+"></asp:RegularExpressionValidator>
                                        <br />
                                        <asp:Label runat="server" ID="actualbillingValidator" Text="Enter Actual Billing." Visible="false" ForeColor="Red" ></asp:Label>
                                    </td>
                                    <td nowrap="nowrap">
                                        <asp:Label runat="server" ForeColor="Red" Text="Enter Actual Spent Hrs." Visible="false" ID="actualValidator"></asp:Label>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ErrorMessage="Enter numbers." ValidationExpression="\d+(\.\d{1,2})?" ValidationGroup="save" ControlToValidate="txtActualSpent"></asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td nowrap="nowrap" colspan="4">
                                        <table width="100%">
                                            <tr>
                                                <td>
                                                    Comments
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtComments" runat="server" TextMode="MultiLine" Width="470px" TabIndex="8"
                                                        Height="63px"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td colspan="2">
                                        <asp:CheckBox ID="chkValue" runat="server" Text="Unutilized Billable Time"  TabIndex="9" />
                                    </td>
                                    <td>
                                        <asp:Button ID="btnAdd" runat="server" OnClick="btnSave_Click" Text="Add" TabIndex="10"
                                            Height="20px" Width="60px" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblResourceExistMessage" runat="server" Text=" " ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                           <asp:Label ID="lblvalidationMsg" runat="server" Text=" " ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                      <tr>
                        <td align="left">
                           <asp:Label ID="lblvalidationMsg2" runat="server" Text=" " ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                </table>
                </td> </tr> </table>

               <%-- <asp:TextBox visible="false" ID="hiddenProjectID" Text="" runat="server"></asp:TextBox>--%>
                 <asp:HiddenField ID="hiddenProjectID" runat="Server" Value="" />
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
