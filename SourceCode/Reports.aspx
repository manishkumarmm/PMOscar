<%@ Page Language="C#" MasterPageFile="~/PMOMaster.master" AutoEventWireup="true"
    CodeBehind="Reports.aspx.cs" Inherits="PMOscar.Reports" EnableEventValidation="false" %>

<asp:Content ContentPlaceHolderID="cntBody" runat="server">
    <%-- Used to fix problem with ASP.NET Ajax breaking in Chrome/Safari/WebKit --%>
    <script src="Scripts/WebKit.js" type="text/javascript"></script>
    <link href="Style/jquery-ui-1.8.12.custom.css" rel="stylesheet" type="text/css" />

    <script src="Scripts/jquery-1.5.1.min.js" type="text/javascript"></script>
    <script src="Scripts/jquery-ui-1.8.12.custom.min.js" type="text/javascript"></script>
    <link rel="stylesheet" href="<%=ConfigurationManager.AppSettings["openHoursReportStyle"] %>"/>
    <script src="<%=ConfigurationManager.AppSettings["openHoursReportScript"] %>" language="javascript" type="text/javascript"></script>
    <style>
        /* This is the style for the trigger icon. The margin-bottom value causes the icon to shift down to center it. */
        .ui-datepicker-trigger
        {
            margin-left: 5px;
            margin-top: 8px;
            margin-bottom: -3px;
        }
    </style>
    <%--Script section--%>
    <script type="text/javascript" language="javascript">




        $(function () {
            document.getElementById("openHoursReports").style.display = "none";
           // var browserName = navigator.appName;
           // document.getElementById("divBrowser").innerHTML = browserName;
           // if (browserName = "Netscape")
           // {
           //     $('#ctl00_cntBody_gdReport').attr('style', 'border-collapse:separate');
             // $("#ctl00_cntBody_gdReport").css({ "border-collapse": "separate" });

           // }
           // else if(browserName ="Microsoft Internet Explorer")
           // {
           //     $('#ctl00_cntBody_gdReport').attr('style', 'border-collapse:collapse');
           //     //$("#ctl00_cntBody_gdReport").css({"border-collapse":"collapse"});
           // }


            $("#datepickerEndWeek").datepicker({

                dateFormat: 'dd/mm/yy',
                beforeShowDay: disableOtherThanSunday,
                showOn: 'both',
                buttonImage: "Images/calendar.jpg",
                buttonImageOnly: true


            });



            $("#datepickerStartWeek").datepicker({
                dateFormat: 'dd/mm/yy',
                beforeShowDay: disableOtherThanMonday,
                showOn: 'both',
                buttonImage: "Images/calendar.jpg",
                buttonImageOnly: true,

                onSelect: function (selected) {
                    $("#datepickerEndWeek").datepicker("option", "minDate", selected)

                    var startWeekValue = $('[id$="datepickerStartWeek"]').val();
                    //keep selected value in hidden field
                    $('[id$="_hiddenstartWeek"]').val(startWeekValue);


                    var endWeekValue = $('[id$="datepickerEndWeek"]').val();
                    //keep selected value in hidden field
                    $('[id$="_hiddenendWeek"]').val(endWeekValue);

                    $(".ui-datepicker-trigger").mouseover(function () {
                        $(this).css('cursor', 'pointer');
                    });

                }


            });


            //hand icon 
            $(".ui-datepicker-trigger").mouseover(function () {
                $(this).css('cursor', 'pointer');
            });


        });

        function disableOtherThanMonday(date) {
            var day = date.getDay();
            var daysToDisable = [0, 2, 3, 4, 5, 6]; //enable monday only

            for (i = 0; i < daysToDisable.length; i++) {
                if ($.inArray(day, daysToDisable) != -1) {
                    return [false];
                }
            }
            return [true];
        }


        function disableOtherThanSunday(date) {
            var day = date.getDay();
            var daysToDisable = [1, 2, 3, 4, 5, 6]; //enable sunday only

            for (i = 0; i < daysToDisable.length; i++) {
                if ($.inArray(day, daysToDisable) != -1) {
                    return [false];
                }
            }
            return [true];
        }



        $(document).ready(function () {
            $("#tablePeriod").hide();
            $("#tableMonthandYear").hide();

            var ddlReportId = document.getElementById('<%=DropDownListReports.ClientID%>');
            var ddlSelectedReportValue = ddlReportId.options[ddlReportId.selectedIndex].value;

            var reportId = getUrlParameter('reportId');
            if ((!ddlSelectedReportValue || ddlSelectedReportValue == 6) && reportId == 6) {
                dt = new Date();
                document.getElementById("datepickerStartWeek").value = startOfWeek(dt);
                document.getElementById("datepickerEndWeek").value = endOfWeek(dt);
                $('[id$="goButton"]').click();
            }

            if (ddlSelectedReportValue == 4) {
                $("#tablePeriod").hide();
                $("#tableMonthandYear").hide();
                var radioReportType = $("input[name$='reportTypeCmpny']");
                radioReportType[0].checked = true;
            }
            else if (ddlSelectedReportValue == 6 || ddlSelectedReportValue == 7) {
                document.getElementById("goButtonTd").style.paddingBottom = "24px";
                $("#tdOtherReports").hide();
                $("#tablePeriod").hide();
                $("#tableMonthandYear").hide();
            }
            else if (ddlSelectedReportValue == 8) {
                document.getElementById("goButtonTd").style.paddingBottom = "14px";
                $("#tablePeriod").hide();
                $("#tableMonthandYear").hide();
            }
            else {
                var radioReportType = $("input[name$='reportType']");
                radioReportType[0].checked = true;
                $('[id$="_hiddenRadio"]').val(radioReportType[0].value);
                $("#tablePeriod").hide();
                $("#tableMonthandYear").show();
            }

            if (ddlSelectedReportValue == 5) {
                $("#tblProject").css('display', 'block');
            }
            // $("#rdbMonthly").prop("checked", "checked");
            //$("#tablePeriod").hide();


            /* report Type radio button change event */
            $("input[name$='reportType']").change(function () {
                if (document.getElementById('<%=gdReport.ClientID%>') != null) {
                    document.getElementById('<%=gdReport.ClientID%>').style.display = 'none';
                }
                if (document.getElementById('<%=gdCmpnySummReport.ClientID%>') != null) {
                    document.getElementById('<%=gdCmpnySummReport.ClientID%>').style.display = 'none';
                }
                document.getElementById('<%=lblReportTitle.ClientID%>').innerHTML = " ";
                var value = $(this).val();
                if (value == 1) {

                    $("#tablePeriod").hide();
                    $("#tableMonthandYear").show();

                }
                else {

                    $("#tablePeriod").show();
                    $("#tableMonthandYear").hide();

                }

            });

            /* report Type(Company utiliztion Report) radio button change event */
            $("input[name$='reportTypeCmpny']").change(function () {
                if (document.getElementById('<%=gdReport.ClientID%>') != null) {
                    document.getElementById('<%=gdReport.ClientID%>').style.display = 'none';
                }
                if (document.getElementById('<%=gdCmpnySummReport.ClientID%>') != null) {
                    document.getElementById('<%=gdCmpnySummReport.ClientID%>').style.display = 'none';
                }
                document.getElementById('<%=lblReportTitle.ClientID%>').innerHTML = " ";
                var value = $(this).val();
                if (value == 3) {

                    $("#tablePeriod").hide();
                    $("#tableMonthandYear").hide();

                }
                else {

                    $("#tablePeriod").hide();
                    $("#tableMonthandYear").show();

                }

            });

            /* StartWeek drop down change event */
            $('[id$="_ddlStartWeek"]').change(function () {


                // var progId = $('[id$="_ddlProgram"]').val();
                //var projId = $('[id$="_ddlProject"]').val();
                var startWeekId = $(this).val();

                BindEndWeek(startWeekId);

            });

            /* SEndWeek drop down change event */
            $('[id$="_ddlEndWeek"]').change(function () {

                var endWeekId = $('[id$="_ddlEndWeek"]').val();
                //keep selected value in hidden field
                $('[id$="_hiddenendWeek"]').val(endWeekId);


            });


            //Start week change
            $('[id$="datepickerStartWeek"]').change(function () {

                var startWeekValue = $('[id$="datepickerStartWeek"]').val();
                //keep selected value in hidden field
                $('[id$="_hiddenstartWeek"]').val(startWeekValue);


            });



            //End week change
            $('[id$="datepickerEndWeek"]').change(function () {

                var endWeekValue = $('[id$="datepickerEndWeek"]').val();
                //keep selected value in hidden field
                $('[id$="_hiddenendWeek"]').val(endWeekValue);


            });

            //radio button change events
            $('[name$="reportType"]').change(function () {

                var thisval = $(this).val();
                //keep selected value in hidden field
                $('[id$="_hiddenRadio"]').val(thisval);

            });

            
        });

        //To fetch the start date of the current week
        function startOfWeek(date) {
            var diff = date.getDate() - date.getDay() + (date.getDay() === 0 ? -6 : 1);

            return new Date(date.setDate(diff)).toLocaleDateString("en-GB");

        }

        //To fetch the end date of the current week
        function endOfWeek(date) {
            var diff = date.getDate() - date.getDay() + 7;

            return new Date(date.setDate(diff)).toLocaleDateString("en-GB");

        }

        var getUrlParameter = function getUrlParameter(sParam) {
            var sPageURL = window.location.search.substring(1),
                sURLVariables = sPageURL.split('&'),
                sParameterName,
                i;

            for (i = 0; i < sURLVariables.length; i++) {
                sParameterName = sURLVariables[i].split('=');

                if (sParameterName[0] === sParam) {
                    return sParameterName[1] === undefined ? true : decodeURIComponent(sParameterName[1]);
                }
            }
        };

        // function to bind all End week
        function BindEndWeek(startWeekId) {

            $('[id$="_ddlEndWeek"] >option').remove();

            $.ajax({
                type: "POST",
                url: "Reports.aspx/BindEndWeek",
                data: "{startWeekId:" + startWeekId + "}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: true,
                cache: false,
                success: function (result) {

                    if (result.d.length == 0) {
                        AddToDropdown('[id$="_ddlEndWeek"]', 0, "--All--");
                    }

                    for (var i = 0; i < result.d.length; i++) {
                        if (i == 0) {
                            if ($('[id$="_ddlStartWeek"]').val() == 0) {
                                AddToDropdown('[id$="_ddlEndWeek"]', 0, "--All--");
                            }
                            else {
                                AddToDropdown('[id$="_ddlEndWeek"]', 0, "--Select--");
                            }
                        }
                        //var value = result.d[i].DashboardId;
                        var value = result.d[i].Name;
                        var text = result.d[i].Name;
                        AddToDropdown('[id$="_ddlEndWeek"]', value, text);
                    }
                }

            });

        }


        // Add items to dropdown
        function AddToDropdown(id, value, text) {
            $(id).append('<option value=' + value + '>' + text + '</option>');
        }


        //




        //validation for GO button (Report)
        function ValidateGo() {
            document.getElementById("openHoursReports").style.display = "none";
            var reportSelected = document.getElementById("<%= DropDownListReports.ClientID %>").value;
            if (reportSelected != "0")  //report has selected
            {

                var startDate = document.getElementById("datepickerStartWeek").value;
                var endDate = document.getElementById("datepickerEndWeek").value;



                //get which report tyupe is selected
                var reportTypeSelected = $('[id$="_hiddenRadio"]').val();

                if (reportTypeSelected == "2" || reportSelected == "6" || reportSelected == "7") //period wise report
                {

                    if (startDate == "Select Start Date") {
                        alert("Please select start date!");
                        return false;
                    }
                    else if (endDate == "Select End Date" || endDate == "") {
                        alert("Please select end date!");
                        return false;
                    }
                    if (reportSelected == "6") {
                        document.getElementById("openHoursReports").innerHTML = '<app-open-hours fromdate todate></app-open-hours>';
                        document.getElementById("openHoursReports").style.display = "block";
                        document.querySelector('app-open-hours').setAttribute('fromdate', document.getElementById('datepickerStartWeek').value);
                        document.querySelector('app-open-hours').setAttribute('todate', document.getElementById('datepickerEndWeek').value);
                        return false;
                    }
                    else if (reportSelected == "7") {
                        document.getElementById("openHoursReports").innerHTML = '<app-open-hours-breakup fromdate todate></app-open-hours-breakup>';
                        document.getElementById("openHoursReports").style.display = "block";
                        document.querySelector('app-open-hours-breakup').setAttribute('fromdate', document.getElementById('datepickerStartWeek').value);
                        document.querySelector('app-open-hours-breakup').setAttribute('todate', document.getElementById('datepickerEndWeek').value);
                        return false;
                    }
                } else if (reportSelected == "8") {
                    document.getElementById("openHoursReports").innerHTML = '<app-pmo-consolidated></app-pmo-consolidated>';
                    document.getElementById("openHoursReports").style.display = "block";
                    return false;
                }
                else {

                    return true;
                }
            }
            else {

                alert("Please select a report!");
                return false;
            }

        }

        //Validation for Export Button
        function ValidateExport() {
            var Grid = document.getElementById("<%=gdReport.ClientID%>");
            var cmpnyReport = document.getElementById("<%=gdCmpnySummReport.ClientID%>");
            if (Grid != null) {

                if (Grid.rows.length > 1) {
                    return true;
                }
                else {
                    alert("No data to export!");
                    return false;
                }

            }
            else {
                var ddlReportId = document.getElementById('<%=DropDownListReports.ClientID%>');
                var ddlSelectedReportValue = ddlReportId.options[ddlReportId.selectedIndex].value;
                var value = $("input[name$='reportTypeCmpny']").val();
                if (ddlSelectedReportValue == 4 && value == 3) {
                    if (cmpnyReport != null) {
                        if (cmpnyReport.rows.length > 1) {
                            return true;
                        }
                        else {
                            alert("No data to export!");
                            return false;
                        }
                    }
                    else {
                        alert("No data to export!");
                        return false;
                    }
                }
                else {
                    alert("No data to export!");
                    return false;
                }
            }
        }
        //for timeout exception

        if (Sys != undefined)
        {
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(EndRequestHandler);
        }
        function EndRequestHandler(sender, args)
        {
            if (args.get_error() != undefined)
            {
                var errorMessage = args.get_error().message;
                alert('Sorry, an unexpected error has occured, please retry your last action.');
                args.set_errorHandled(true);
            }
        }

    </script>
    <div>
        <div>
             <style type="text/css" media="screen">
   
                BODY {  width:100%}
     </style>
            <%--heading --%>
            <table width="100%">
                <%--Heading --%>
                <tr>
                    <td align="left" colspan="8" class="reportLabel">
                        <b>Reports</b>
                    </td>
                </tr>
            </table>
            <table>
                <tr>
                    <td>
                        <table width="100%;">
                            <tr>
                                <%--     first row--%>
                                <td>
                                    <%--report Type and Report selection--%>
                                    <table width="100%">
                                        <tr>
                                            <%--Reports dropdown--%>
                                            <td align="right" width="0%;">
                                                <asp:Label ID="Label1" runat="server" Text="Select Report:"></asp:Label>
                                            </td>
                                            <td align="left" width="0%;">
                                                <asp:DropDownList ID="DropDownListReports" runat="server" OnSelectedIndexChanged="ddlReports_SelectedIndexChanged"
                                                    AutoPostBack="True" Height="20px">
                                                </asp:DropDownList>
                                            </td>
                                            <td align="right" width="0%;" runat="server" id="tdOtherReports">
                                                Report Type:
                                                <input id="rdbMonthly" type="radio" name="reportType" value="1" runat="server" checked="true" />Month
                                                <input id="rdbPeriod" type="radio" name="reportType" value="2" runat="server" />Period
                                                <input type="hidden" id="hiddenRadio" name="hiddenRadio" value="1" runat="server" />
                                            </td>
                                            <td align="right" width="0%;" runat="server" id="tdCompanyUtilizationReport" visible="false">
                                                Report Type:
                                                <input id="rdbSummaryReport" type="radio" name="reportTypeCmpny" value="3" runat="server"
                                                    checked="true" />Summary Report
                                                <input id="rdbDetailedReport" type="radio" name="reportTypeCmpny" value="4" runat="server" />Detailed
                                                Report
                                            </td>
                                            <td id="tdopenHoursReport" runat="server" visible="false" style="padding-bottom: 6px;">
                                                <table width="100%">
                                                    <tr>
                                                        <td class="label" width="0%;" align="right" style="padding-top: 4px;">Starting Week:
                                                        </td>
                                                        <td class="dropdown" width="0%;" style="padding-bottom: 4px;">
                                                            <input type="text" id="datepickerStartWeek" value="Select Start Date" readonly="readonly" style="width: 125px;" />
                                                            <input type="hidden" id="hidden1" name="hiddenstartWeek" value="select" runat="server" />
                                                        </td>
                                                        <td class="label" width="0%;" align="right" style="padding-top: 4px;">Ending Week:
                                                        </td>
                                                        <td class="dropdown" width="0%;" style="padding-bottom: 4px;">
                                                            <input type="text" id="datepickerEndWeek" value="Select End Date" readonly="readonly" style="width: 125px;" />
                                                            <input type="hidden" id="hidden2" name="hiddenendWeek" value="select" runat="server" />
                                                        </td>
                                                        <%-- hidden variable--%>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <%-- go button--%>
                                <td rowspan="4" valign="bottom" id="goButtonTd">
                                    <table>
                                        <tr>
                                            <%--Go Button  --%>
                                            <td width="4%;" align="right">
                                                <asp:Button ID="goButton" runat="server" Text="Go" OnClientClick="return ValidateGo();"
                                                    OnClick="goButton_Click"/>
                                            </td>
                                            <td>
                                                <%--Export Button--%>
                                                <asp:LinkButton ID="btnExport" runat="server" CssClass="LinkNormalStyle" OnClientClick="return ValidateExport();"
                                                    OnClick="btnExport_Click">
                  <img class="reportExportImage" src="images/excel.png" alt="Export" />Export</asp:LinkButton>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <%--second row--%>
                                <td>
                                    <%--year and month dropdown--%>
                                    <table width="100%" id="tableMonthandYear">
                                        <tr>
                                            <td class="label" width="18%;" align="right">
                                                Year:
                                            </td>
                                            <td class="dropdown" width="0%;">
                                                <asp:DropDownList ID="ddlYear" runat="server" Height="20px" Width="74px">
                                                </asp:DropDownList>
                                            </td>
                                            <td class="label" width="0%;" align="right">
                                                Month:
                                            </td>
                                            <td class="dropdown" width="0%;">
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
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <%-- 3rd row--%>
                                <td>
                                    <%--period dropdown --%>
                                    <table width="100%" id="tablePeriod">
                                        <tr>
                                            <td class="label" width="0%;" align="right">
                                                Starting Week:
                                            </td>
                                            <td class="dropdown" width="0%;">
                                                <input type="text" id="datepickerStartWeek" value="Select Start Date" readonly="readonly" style="width: 125px;" />
                                                <input type="hidden" id="hiddenstartWeek" name="hiddenstartWeek" value="select" runat="server" />
                                            </td>
                                            <td class="label" width="0%;" align="right">
                                                Ending Week:
                                            </td>
                                            <td class="dropdown" width="0%;">
                                                <input type="text" id="datepickerEndWeek" value="Select End Date" readonly="readonly" style="width: 125px;" />
                                                <input type="hidden" id="hiddenendWeek" name="hiddenendWeek" value="select" runat="server" />
                                            </td>
                                            <%-- hidden variable--%>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table width="100%" id="tblProject" style="display:none;">
                                        <tr>
                                            <td class="label" width="20%" align="right">
                                                Project:
                                            </td>
                                            <td class="dropdown">
                                                &nbsp;<asp:DropDownList ID="ddlProject" runat="server" Height="20px" Width="305px"></asp:DropDownList>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <table>
                <tr>
                </tr>
            </table>
        </div>
        <table width="100%">
            <tr>
                <td colspan="8">
                </td>
            </tr>
            <tr>
                <td colspan="8">
                    <asp:ScriptManager ID="ScriptManager1" runat="server" AsyncPostBackTimeout="30000"  EnablePageMethods="true" ScriptMode="Release"/>
                    <asp:UpdatePanel runat="server" ID="UpdatePanel" UpdateMode="Conditional">
                        <ContentTemplate>
                            <div align="center" class="reportTiltle">
                                <asp:Label ID="lblReportTitle" runat="server"></asp:Label></div>
                                <div id="openHoursReports" style="width:100%"></div>
                              <asp:GridView ID="gdReport" runat="server" OnRowDataBound="gdReport_RowDataBound"
                                OnRowCreated="gdReport_RowCreated" Width="100%" CellPadding="5" AllowSorting="true" cellspacing="0" 
                                OnSorting="gdReport_Sorting" style="border-collapse:initial;border-bottom: black 1px solid; border-left: black 1px solid;border-top: black 1px solid;">
                             
                                  <Columns>
                                    <%-- <asp:BoundField HeaderText="Project Name" DataField="ProjectName" />--%>
                                </Columns>
                                <EmptyDataTemplate>
                                    <div align="center">
                                        No data available</div>
                                </EmptyDataTemplate>
                            </asp:GridView>
                            
                        
                            <asp:GridView ID="gdCmpnySummReport" runat="server" CellSpacing="0" CellPadding="5"
                                BorderWidth="1px" Width="90%" AutoGenerateColumns="False" HorizontalAlign="Center"
                                OnRowDataBound="gdCmpnySummReport_RowDataBound" OnRowCommand="gdCmpnySummReport_RowCommand"
                                DataKeyNames="Date,Finalize">
                                <AlternatingRowStyle BackColor="#b2d2e9" ForeColor="Black" />
                                <Columns>
                                    <asp:BoundField DataField="Month_Year" HeaderText="Month Year" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Left" ItemStyle-CssClass="CompnySummaryReportItems"></asp:BoundField>
                                    <asp:BoundField DataField="AvailableHours" HeaderText="Available" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="CompnySummaryReportItems"></asp:BoundField>
                                    <asp:BoundField DataField="BilledHours" HeaderText="Billed" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="CompnySummaryReportItems"></asp:BoundField>
                                    <asp:BoundField DataField="UtilizedButNotBilledHours" HeaderText="Utilized But Not Billed"
                                        HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="CompnySummaryReportItems"></asp:BoundField>
                                    <asp:BoundField DataField="Admin" HeaderText="Admin" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="CompnySummaryReportItems"></asp:BoundField>
                                    <asp:BoundField DataField="Open+VAS" HeaderText="Open+VAS" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="CompnySummaryReportItems"></asp:BoundField>
                                    <asp:BoundField DataField="Proposal" HeaderText="Proposal" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="CompnySummaryReportItems"></asp:BoundField>
                                    <asp:BoundField DataField="UtilizationPer" HeaderText="Utilization %" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Right" ItemStyle-CssClass="CompnySummaryReportItems"></asp:BoundField>
                                    <asp:TemplateField HeaderText="Actions" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lnkRefresh" runat="server" CommandArgument='<%# Eval("Date")%>'
                                                CommandName="Refresh" Visible='<%# !(Convert.ToBoolean(Eval("Finalize"))) %>'>Refresh</asp:LinkButton>
                                            <asp:LinkButton ID="lnkFinalize" runat="server" CommandArgument='<%# Eval("Date")%>'
                                                CommandName="Finalize" Visible='<%# !(Convert.ToBoolean(Eval("Finalize"))) %>'>Finalize</asp:LinkButton>
                                            <asp:Label ID="lblFinalize" runat="server" Text="Finalized" Visible='<%# (Convert.ToBoolean(Eval("Finalize"))) %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataRowStyle HorizontalAlign="Center" />
                                <HeaderStyle BackColor="#539cd1" ForeColor="#FFFFFF" />
                                <RowStyle BackColor="#eff3fb" ForeColor="Black" />
                            </asp:GridView>
                            <asp:UpdateProgress ID="updateProgress" runat="server">
                                <ProgressTemplate>
                                    <%-- <div style="position: fixed; text-align: center; height: 100%; width: 100%; top: 0; right: 0; left: 0; z-index: 9999999; background-color: #000000; opacity: 0.7;">
                                  <span style="border-width: 0px; position: fixed; padding: 50px; background-color: #FFFFFF; font-size: 36px; left: 40%; top: 40%;">Loading ...</span>
                            </div>--%>
                                    <div id="divLoading" class="ajax-loader">
                                        <img src="Images/loader.gif" />"
                                    </div>
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="goButton" EventName="Click"></asp:AsyncPostBackTrigger>
                        </Triggers>
                    </asp:UpdatePanel>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td colspan="8">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ContentPlaceHolderID="cntHead" runat="server">
</asp:Content>
