<%@ Page Language="C#" AutoEventWireup="true" EnableEventValidation="false" MasterPageFile="~/PMOMaster.master"
    CodeBehind="ProjectDashboard.aspx.cs" Inherits="PMOscar.ProjectDashboard1" %>

<asp:Content ContentPlaceHolderID="cntBody" runat="server">
    <script type="text/javascript" charset="utf-8">
        $(function () {

            // By default, set Project Dashboard as selected
            $("#rdbProject").prop("checked", "checked");
            $("#divProjectDashboard").show();
            $("#divPeriodDashboard").hide();

            var tabContainers = $('div.tabs > div');
            tabContainers.hide().filter(':first').show();

            $('div.tabs ul.tabNavigation a').click(function () {
                tabContainers.hide();
                tabContainers.filter(this.hash).show();
                $('div.tabs ul.tabNavigation a').removeClass('selected');
                $(this).addClass('selected');
                return false;
            }).filter(':first').click();

            // Hide Loading image initailly
            $('#divLoading').hide();
            
            /* Period tabs */
            var periodTabContainers = $('div.periodTabs > div');
            periodTabContainers.hide().filter(':first').show();

            $('div.periodTabs ul.tabNavigation a').click(function () {
                periodTabContainers.hide();
                periodTabContainers.filter(this.hash).show();
                $('div.periodTabs ul.tabNavigation a').removeClass('selected');
                $(this).addClass('selected');
                return false;
            }).filter(':first').click();

            // Disable Include Proposal & VAS checkbox intially
            $('#chkInclude').attr("disabled", true);


            /* Dashboard type radio button change */
            $("input[name$='dashboardType']").change(function () {
                var value = $(this).val();

                // Hide comments tooltip popup
                HideNotes();

                // Hide Loading image initailly
                $('#divLoading').hide();

                if (value == 1) {
                    $("#divProjectDashboard").hide();
                    $("#divPeriodDashboard").show();
                    $(this).prop("checked", "checked");
                    $("#rdbProject").removeAttr('checked');
                    $('div.periodTabs ul.tabNavigation a').filter(':first').click();

                    // Clear Project, StartWeek and EndWeek dropdowns
                    $('[id$="_ddlProgram"]')[0].selectedIndex = 0;

                    ClearPeriodDashboardDropdowns();
                    BindProgram();

                    // Hide lables
                    $("#lblProgram").html("");
                    $("#lblProject").html("");
                    $("#spnOwner").hide();
                    $("#spnManager").hide();
                    $("#spnPhase").hide();

                    // Disable Include Proposal & VAS checkbox
                    $('#chkInclude').attr('checked', false);
                    $('#chkInclude').attr("disabled", true);

                    // Clear period tabs contents
                    var noRecordHTML = "<center><span style='text-align:center'>No Records Found!</span></center>";

                    $('#PT_Summary').html(noRecordHTML);
                    $('#PT_PeriodEffortPhase').html(noRecordHTML);
                    $('#PT_PeriodEffortRole').html(noRecordHTML);
                    $('#PT_ProjectEffortPhase').html(noRecordHTML);
                    $('#PT_ProjectEffortRole').html(noRecordHTML);

                }
                else {
                    $("#divPeriodDashboard").hide();
                    $("#divProjectDashboard").show();
                    $(this).prop("checked", "checked");
                    $("#rdbPeriod").removeAttr('checked');
                    $('div.tabs ul.tabNavigation a').filter(':first').click();
                }

            }
                );

            /* Project drop down change event */
            $('[id$="_ddlProject"]').change(function () {

                var progId = $('[id$="_ddlProgram"]').val();
                var projId = $(this).val();
                BindStartWeek(progId, projId);
                BindEndWeek(progId, projId, 0);

            });

            /* StartWeek drop down change event */
            $('[id$="_ddlStartWeek"]').change(function () {

                var progId = $('[id$="_ddlProgram"]').val();
                var projId = $('[id$="_ddlProject"]').val();
                var startWeekId = $(this).val();

                BindEndWeek(progId, projId, startWeekId);

            });


            function BindPeriodDashboardDetails() {
                // Bind data into Period Dashboard Tabs
                var isChecked = $('#chkInclude').is(':checked');
                var progId = $('[id$="_ddlProgram"]').val();
                var projId = $('[id$="_ddlProject"]').val();
                var startWeekId = $('[id$="_ddlStartWeek"]').val();
                var endWeekId = $('[id$="_ddlEndWeek"]').val();

                BindPeriodDashboardTabs(progId, projId, startWeekId, endWeekId, isChecked);
            }
            /* Go button click event */
            $("#btnGo").click(function () {
                // Validations
                if ($('[id$="_ddlProgram"]').val() == 0) {
                    alert("Please select a Program");
                    return false;
                }
                else if ($('[id$="_ddlStartWeek"]').val() > 0 && $('[id$="_ddlEndWeek"]').val() == 0) {
                    alert("Please select End Week");
                    return false;
                }
                else {
                    $("#lblProgram").html($('[id$="_ddlProgram"] option:selected').text());

                    if ($('[id$="_ddlProject"]').val() == 0) {
                        $("#lblProject").html("All");

                        // Hide lables
                        $("#spnOwner").hide();
                        $("#spnManager").hide();
                        $("#spnPhase").hide();
                    }
                    else {
                        $("#lblProject").html($('[id$="_ddlProject"] option:selected').text());
                        SetProjectDetails($('[id$="_ddlProject"]').val());

                        // Show lables
                        $("#spnOwner").show();
                        $("#spnManager").show();
                        $("#spnPhase").show();
                    }
                }

                /* Show/ Hide Loading image */
                $('#divLoading').show();

                // Enable Include Proposal & VAS checkbox
                $('#chkInclude').removeAttr("disabled");

                // Bind data into Tabs
                var isChecked = $('#chkInclude').is(':checked');

                var progId = $('[id$="_ddlProgram"]').val();
                var projId = $('[id$="_ddlProject"]').val();
                var startWeekId = $('[id$="_ddlStartWeek"]').val();
                var endWeekId = $('[id$="_ddlEndWeek"]').val();

                BindPeriodDashboardTabs(progId, projId, startWeekId, endWeekId, isChecked);

            });

            /* Include VAS & Proposal check change event */
            $('#chkInclude').change(function () {

                /* Show/ Hide Loading image */
                $('#divLoading').show();

                if ($('[id$="_ddlProgram"]').val() != 0) {
                    var isChecked = $(this).is(':checked');
                    var progId = $('[id$="_ddlProgram"]').val();
                    var projId = $('[id$="_ddlProject"]').val();
                    var startWeekId = $('[id$="_ddlStartWeek"]').val();
                    var endWeekId = $('[id$="_ddlEndWeek"]').val();

                    BindPeriodDashboardTabs(progId, projId, startWeekId, endWeekId, isChecked);
                }

            });

        });


        /* Bind Period details into Period tabs */

        function BindPeriodDashboardTabs(progId, projId, startWeekId, endWeekId, checked) {
            $.ajax({
                type: "POST",
                url: "ProjectDashboard.aspx/BindPeriodDashboardTabs",
                data: "{programId:" + progId + ", projectId:" + projId + ", startWeekId:" + startWeekId + ", endWeekId:" + endWeekId + ", isChecked:" + checked + "}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: true,
                cache: false,
                success: function (result) {
                    $('#PT_Summary').html(result.d[0].toString());
                    $('#PT_PeriodEffortPhase').html(result.d[1].toString());
                    $('#PT_PeriodEffortRole').html(result.d[2].toString());
                    $('#PT_ProjectEffortPhase').html(result.d[3].toString());
                    $('#PT_ProjectEffortRole').html(result.d[4].toString());

                    /* Show/ Hide Loading image */
                    $('#divLoading').hide();
                },
                error: function (result) {
                    $('#divLoading').hide();
	        }

            });
            
        }

      
        /* Populate Period dashboard dropdowns */

        function BindPeriodDashboardDropdowns(programId) {
            if (programId == 0) {

                ClearPeriodDashboardDropdowns();
            }

            else {
                // Bind dropdowns
                BindProject(programId);
                BindStartWeek(programId,0);
                BindEndWeek(programId, 0, 0);
            }
        }

        // Clear Period dashboard dropdowns
        function ClearPeriodDashboardDropdowns() {
            $('[id$="_ddlProject"] >option').remove();
            AddToDropdown('[id$="_ddlProject"]', 0, "--Select Program--");

            $('[id$="_ddlStartWeek"] >option').remove();
            AddToDropdown('[id$="_ddlStartWeek"]', 0, "--Select--");

            $('[id$="_ddlEndWeek"] >option').remove();
            AddToDropdown('[id$="_ddlEndWeek"]', 0, "--Select--");
        }

        // Funtcion to bind program dropdown
        function BindProgram() {
            $('[id$="_ddlProgram"] >option').remove();

            $.ajax({
                type: "POST",
                url: "ProjectDashboard.aspx/BindProgram",
                data: {},
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: true,
                cache: false,
                success: function (result) {
                    if (result.d.length == 0) {
                        AddToDropdown('[id$="_ddlProgram"]', 0, "--Select--");
                    }

                    for (var i = 0; i < result.d.length; i++) {
                        if (i == 0) {
                            AddToDropdown('[id$="_ddlProgram"]', 0, "--Select--");
                        }
                        var value = result.d[i].ProgramId;
                        var text = result.d[i].ProgramName;
                        AddToDropdown('[id$="_ddlProgram"]', value, text);
                    }
                }

            });
        }

        // function to bind Project dropdown
        function BindProject(progId) {
            $('[id$="_ddlProject"] >option').remove();

            $.ajax({
                type: "POST",
                url: "ProjectDashboard.aspx/BindProjectName",
                data: "{programId:" + progId + "}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: true,
                cache: false,
                success: function (result) {
                    if (result.d.length == 0) {
                        AddToDropdown('[id$="_ddlProject"]', 0, "--All--");
                    }

                    for (var i = 0; i < result.d.length; i++) {
                        if (i == 0) {
                            AddToDropdown('[id$="_ddlProject"]', 0, "--All--");
                        }
                        var value = result.d[i].ProjectId;
                        var text = result.d[i].ProjectName;
                        AddToDropdown('[id$="_ddlProject"]', value, text);
                    }
                }

            });
        }

        // function to bind all Start week for selected program
        function BindStartWeek(progId, projId) {
            $('[id$="_ddlStartWeek"] >option').remove();

            $.ajax({
                type: "POST",
                url: "ProjectDashboard.aspx/BindStartWeek",
                data: "{programId:" + progId + ",projectId:" + projId + "}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: true,
                cache: false,
                success: function (result) {
                    if (result.d.length == 0) {
                        AddToDropdown('[id$="_ddlStartWeek"]', 0, "--All--");
                    }

                    for (var i = 0; i < result.d.length; i++) {
                        if (i == 0) {
                            AddToDropdown('[id$="_ddlStartWeek"]', 0, "--All--");
                        }
                        var value = result.d[i].DashboardId;
                        var text = result.d[i].Name;
                        AddToDropdown('[id$="_ddlStartWeek"]', value, text);
                    }
                }

            });
        }

        // function to bind all End week for selected program
        function BindEndWeek(progId, projId, startWeekId) {
            $('[id$="_ddlEndWeek"] >option').remove();

            $.ajax({
                type: "POST",
                url: "ProjectDashboard.aspx/BindEndWeek",
                data: "{programId:" + progId + ", projectId:" + projId + ", startWeekId:" + startWeekId + "}",
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
                        var value = result.d[i].DashboardId;
                        var text = result.d[i].Name;
                        AddToDropdown('[id$="_ddlEndWeek"]', value, text);
                    }
                }

            });
        }

        function SetProjectDetails(id) {
            $.ajax({
                type: "POST",
                url: "ProjectDashboard.aspx/GetProjectById",
                data: "{projectId:"+ id +"}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: true,
                cache: false,
                success: function (result) {
                    if (result != null)
                    {
                        $("#lblOwner").html(result.d.ProjectOwner.toString());
                        $("#lblManager").html(result.d.ProjectManager.toString());
                        $("#lblPhase").html(result.d.CurrentPhase.toString());
                    }
                }
            });
        }
          
        // Add items to Department dropdown
        function AddToDropdown(id,value, text) {
            $(id).append('<option value=' + value + '>' + text + '</option>');
        }        

    </script>
    <div id="divNote" style="background-color: #fdfdd6; padding: 5px 10px 10px 10px; text-align: left; position: absolute; border: solid 1px black; 
        visibility: hidden; white-space:normal; z-index:1000; max-width: 700px; word-wrap: break-word; max-height: 700px; overflow-y: scroll;">
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
    <div class="dashboardType">
        Dashboard Type:
        <label><input id="rdbPeriod" type="radio" name="dashboardType" value="1" />Period</label>
        <label><input id="rdbProject" type="radio" name="dashboardType"  checked="checked" value="2" />Project</label>
    </div>
    <div id="divProjectDashboard" style="display:none">
        <style type="text/css" media="screen">
   
                BODY {  width:100%}
     </style>
        <table width="100%">
            <tr>
                <td>
                    <div>
                        <asp:Panel ID="pnlMaster" runat="server">
                            <table id="Table1" runat="server" width="917">
                                <tr>
                                    <td align="left" style="height: 20px; width: 337px;">
                                        <asp:Label ID="lblPeriod" runat="server" Text="Period"></asp:Label>
                                    </td>
                                    <td style="height: 20px; width: 145px;" align="left ">
                                        <asp:DropDownList ID="ddlProjectPeriod" runat="server" AutoPostBack="True" Height="20px"
                                            OnSelectedIndexChanged="ddlProjectPeriod_SelectedIndexChanged" Width="136px"
                                            TabIndex="1">
                                            <asp:ListItem Value="0">Weekly</asp:ListItem>
                                            <asp:ListItem Value="1">Multi-Week</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                    <td style="width: 403px">
                                        <asp:Label ID="Label13" runat="server" Text="Available Reports"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlReports" runat="server" AutoPostBack="True" Height="16px"
                                            OnSelectedIndexChanged="ddlReports_SelectedIndexChanged" Width="135px" TabIndex="2">
                                        </asp:DropDownList>
                                    </td>
                                    <td style="width: 40px;">
                                    </td>
                                    <td align="left" style="height: 20px; width: 80px;">
                                    </td>
                                    <td style="height: 20px; width: 125px;">
                                        <asp:Button ID="btnDashboard" runat="server" Text="Create Dashboard" TabIndex="3"
                                            Width="106px" />
                                    </td>
                                    <td align="left" style="height: 20px; width: 125px;">
                                        <asp:Button ID="btnRefresh" runat="server" OnClientClick="Javascript:return confirm('Do you want to reload the dashboard ?')" OnClick="btnRefresh_Click" TabIndex="4"
                                            Text="Reload Dashboard" />
                                    </td>
                                    <td align="left" style="height: 20px; width: 125px;">
                                        <asp:Button ID="btnFinalize" runat="server" OnClientClick="Javascript:return confirm('Do you want to finalize the dashboard ?')" OnClick="btnFinalize_Click" TabIndex="5"
                                            Text="Finalize Dashboard" Width="108px" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" colspan="7">
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </div>
                </td>
            </tr>
            <tr>
                <td style="width: 917px">
                    <div>
                        <asp:Panel runat="server">
                            <table width="100%">
                                <tr>
                                    <td align="left">
                                        <asp:Label ID="lblHeading" runat="server" Text="Label" Font-Bold="true"></asp:Label>
                                    </td>
                                    <td align="left">
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </div>
                </td>
            </tr>
            <tr>
                <td style="width: 917px">
                    <div style="height: 25px; vertical-align: middle; text-align: center;">
                        <asp:Label ID="lblMsg" runat="server" ForeColor="#009900"></asp:Label>
                    </div>
                    <div style="vertical-align: middle; text-align: right; width: 770px;">
                        <asp:CheckBox ID="cbProposalAndVas" runat="server" Checked="false" AutoPostBack="true"
                            Text="Include Proposal &amp; VAS" OnCheckedChanged="cbProposalAndVas_CheckedChanged" />
                    </div>
                </td>
            </tr>
            <tr>
                <td style="width: 917px">
                    <div>
                        <%#Eval("ProjectName")%>
                        <div class="tabs">
                            <ul class="tabNavigation">
                                <li><a href="#Review">Overview</a></li>
                                <li><a href="#ProjectEffortPhase" tabindex="5">Project Effort (Phase)</a></li>
                                <li><a href="#PeriodEffortPhase" tabindex="6">Period Effort (Phase)</a></li>
                                <li><a href="#ProjEffortRole" tabindex="7">Project Effort (Role)</a></li>
                                <li><a href="#PeriodEffortRole" tabindex="8">Period Effort (Role)</a></li>
                            </ul>
                            <div id="Review" style="border: 3px solid #368ac8;">
                                <table width="100%">
                                    <tr>
                                        <td align="left">
                                            <asp:GridView ID="dgOverView" runat="server" CellPadding="3" ForeColor="#333333"
                                                OnRowDataBound="dgOverView_RowDataBound" OnRowCreated="dgOverView_RowCreated"
                                                TabIndex="-1" AllowSorting="true" OnSorting="dgOverView_Sorting">
                                                <RowStyle BackColor="#EFF3FB" />
                                                <Columns>
                                                    <asp:BoundField DataField="" HeaderText="#" />
                                                    <asp:TemplateField HeaderText="Project Name" SortExpression="ProjectName">
                                                        <ItemTemplate>
                                                            <asp:HyperLink ID="lnkProject" runat="server" NavigateUrl='<%# "Project.aspx?ProjectEditId=" + DataBinder.Eval(Container.DataItem,"ProjectID")%>'><%#Eval("ProjectName")%></asp:HyperLink>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="100px" />
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
                            <div id="ProjectEffortPhase" style="overflow: auto;">
                                <table style="border: 3px solid #368ac8;">
                                    <tr>
                                        <td align="left">
                                            <asp:GridView ID="dgProjectEffortPhase" runat="server" CellPadding="4" ForeColor="#333333"
                                                OnRowCreated="dgProjectEffortPhase_RowCreated" OnRowDataBound="dgProjectEffortPhase_RowDataBound"
                                                TabIndex="-1">
                                                <RowStyle BackColor="#EFF3FB" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="#">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label7" runat="server" Text="Label"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Project Name">
                                                        <ItemTemplate>
                                                            <asp:HyperLink ID="lnkProject" runat="server" NavigateUrl='<%# "Project.aspx?ProjectEditId=" + DataBinder.Eval(Container.DataItem,"ProjectID")%>'><%#Eval("ProjectName")%></asp:HyperLink>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="200px" />
                                                    </asp:TemplateField>
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
                                </table>
                            </div>
                            <div id="PeriodEffortPhase">
                                <table style="border: 3px solid #368ac8;">
                                    <tr>
                                        <td align="left">
                                            <asp:GridView ID="dgPeriodEffortPhase" runat="server" CellPadding="4" ForeColor="#333333"
                                                OnRowCreated="dgPeriodEffortPhase_RowCreated" OnRowDataBound="dgPeriodEffortPhase_RowDataBound"
                                                TabIndex="-1">
                                                <RowStyle BackColor="#EFF3FB" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="#">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label7" runat="server" Text="Label"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Project Name">
                                                        <ItemTemplate>
                                                            <asp:HyperLink ID="lnkProject" runat="server" NavigateUrl='<%# "Project.aspx?ProjectEditId=" + DataBinder.Eval(Container.DataItem,"ProjectID")%>'><%#Eval("ProjectName")%></asp:HyperLink>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="200px" />
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
                            <div id="ProjEffortRole">
                                <table style="border: 3px solid #368ac8;">
                                    <tr>
                                        <td align="left">
                                            <asp:GridView ID="dgProjectEffortRole" runat="server" CellPadding="4" ForeColor="#333333"
                                                OnRowCreated="dgProjectEffortRole_RowCreated" OnRowDataBound="dgProjectEffortRole_RowDataBound"
                                                TabIndex="-1">
                                                <RowStyle BackColor="#EFF3FB" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="#">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label14" runat="server" Text="Label"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Project Name">
                                                        <ItemTemplate>
                                                            <asp:HyperLink ID="lnkProject" runat="server" NavigateUrl='<%# "Project.aspx?ProjectEditId=" + DataBinder.Eval(Container.DataItem,"ProjectID")%>'><%#Eval("ProjectName")%></asp:HyperLink>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="200px" />
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
                            <div id="PeriodEffortRole">
                                <table style="border: 3px solid #368ac8;">
                                    <tr>
                                        <td align="left">
                                            <asp:GridView ID="dgPeriodEffortRole" runat="server" CellPadding="4" ForeColor="#333333"
                                                OnRowCreated="dgPeriodEffortRole_RowCreated" OnRowDataBound="dgPeriodEffortRole_RowDataBound"
                                                TabIndex="-1">
                                                <RowStyle BackColor="#EFF3FB" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="#">
                                                        <ItemTemplate>
                                                            <asp:Label ID="Label15" runat="server" Text="Label"></asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Project Name">
                                                        <ItemTemplate>
                                                            <asp:HyperLink ID="lnkProject" runat="server" NavigateUrl='<%# "Project.aspx?ProjectEditId=" + DataBinder.Eval(Container.DataItem,"ProjectID")%>'><%#Eval("ProjectName")%></asp:HyperLink>
                                                        </ItemTemplate>
                                                        <ItemStyle Width="200px" />
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
                        </div>
                        <%#Eval("ProjectName")%>
                    </div>
                </td>
            </tr>
            <asp:HiddenField ID="HiddenField1" runat="server" Value="C" />
            <asp:HiddenField ID="HiddenField2" runat="server" />
        </table>
    </div>
    <div id="divPeriodDashboard"  class="periodDashboard">
        <div class="dropdown">
            <table width="100%">
                <tr>
                    <td class="label">
                        Program Name:
                    </td>
                    <td class="dropdown">
                        <asp:DropDownList ID="ddlProgram" runat="server" CssClass="periodDropdown"  TabIndex="1" onchange="BindPeriodDashboardDropdowns(this.value);"></asp:DropDownList>
                    </td>
                    <td class="label">
                        Project Name:
                    </td>
                    <td class="dropdown">
                        <asp:DropDownList ID="ddlProject" runat="server" CssClass="periodDropdown" TabIndex="2"></asp:DropDownList>
                    </td>
                    <td class="label">Starting Week:
                    </td>
                    <td class="dropdown">
                        <asp:DropDownList ID="ddlStartWeek" runat="server" CssClass="periodDropdown" TabIndex="3"></asp:DropDownList>
                    </td>
                    <td class="label">
                        Ending Week:
                    </td>
                    <td class="dropdown">
                        <asp:DropDownList ID="ddlEndWeek" runat="server" CssClass="periodDropdown" TabIndex="4"></asp:DropDownList>
                    </td>
                    <td align="left" style="height: 20px; width: 125px;">
                        <input class="goButton" type="button" id="btnGo" value="Go"/>
                    </td>
                </tr>                
            </table>
        </div>
        <div class="info">
            <table width="100%">
            <tr>
                    <td class="info">
                        Program: <label id="lblProgram"/>
                    </td>                   
                    <td class="info">
                        Project: <label id="lblProject"/>
                    </td>
                    <td class="info"><span id="spnOwner">Project Owner: <label id="lblOwner"/></span>
                    </td>
                    <td class="info">
                        <span id="spnManager">Project Manager: <label id="lblManager"/></span>
                    </td>
                    <td class="info">
                        <span id="spnPhase">Current Phase: <label id="lblPhase"/></span>
                    </td>
                    <td></td>
                </tr>
            </table>
        </div>
        <div>
            <div class="periodTabs">
                <ul class="tabNavigation">
                    <li><a href="#PT_Summary">Summary</a></li>
                    <li><a href="#PT_PeriodEffortPhase" tabindex="5">Period Effort (Phase)</a></li>
                    <li><a href="#PT_PeriodEffortRole" tabindex="6">Period Effort (Role)</a></li>
                    <li><a href="#PT_ProjectEffortPhase" tabindex="7">Project Effort (Phase)</a></li>
                    <li><a href="#PT_ProjectEffortRole" tabindex="8">Project Effort (Role)</a></li>
                    <li><input id="chkInclude" type="checkbox"/>Include Proposal &amp; VAS</li>
                </ul>
                
                <!-- Perid Tabs Start -->
                <div id="PT_Summary" class="dashboardBorder" style="overflow:hidden">
                    <center><span style="text-align:center">No Records Found!</span></center>
                </div>
                <div id="PT_PeriodEffortPhase" class="dashboardBorder">
                    <center><span style="text-align:center">No Records Found!</span></center>
                </div>
                <div id="PT_PeriodEffortRole" class="dashboardBorder">
                    <center><span style="text-align:center">No Records Found!</span></center>
                </div>
                <div id="PT_ProjectEffortPhase" class="dashboardBorder">
                    <center><span style="text-align:center">No Records Found!</span></center>
                </div>
                <div id="PT_ProjectEffortRole" class="dashboardBorder">
                    <center><span style="text-align:center">No Records Found!</span></center>
                </div>
                <!-- Perid Tabs End -->
            </div>
           
        </div>
    </div>
    <div id="divLoading" class="ajax-loader">
        <img src="Images/loader.gif" alt="" />
    </div>
</asp:Content>
