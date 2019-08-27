<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/PMOMaster.master"
    CodeBehind="Resource.aspx.cs" Inherits="PMOscar.Resource" %>

<asp:Content ContentPlaceHolderID="cntBody" runat="server">
      <link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css">
  <link rel="stylesheet" href="Style/jquery-ui-1.8.12.custom.css" type="text/css">
  <script src="https://code.jquery.com/jquery-1.12.4.js"></script>
  <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
    <script type="text/javascript">
        function clearError() {
            $('span[name="errorSpan"]').text('');
        }

        function CheckTextValue(e, myfield) {
            var key;
            key = e.which ? e.which : e.keyCode;
            keychar = String.fromCharCode(key);

            if ((key == 9) || (key == 8) || (key == 32)) {
                return true;
            }
            else if ((("abcdefghijklmnopqrstuvwxyz").indexOf(keychar) > -1)) {
                return true;
            }
            else if ((("ABCDEFGHIJKLMNOPQRSTUVWXYZ").indexOf(keychar) > -1)) { // numbers
                return true;
            }
            else {
                return false;
            }
        }
        function CheckNemericValue(e, myfield) {
            var key;
            key = e.which ? e.which : e.keyCode;
            keychar = String.fromCharCode(key);

            if ((key == null) || (key == 0) || (key == 8) || (key == 9) || (key == 13) || (key == 27) || (key == 37) || (key == 38) || (key == 39) || (key == 40)) {
                return true;
            }                
            else if (("0123456789").indexOf(keychar) > -1) {// Numbers
                return true;
            }                
            else if ((".").indexOf(keychar) > -1) {// Checking whether the number of points in textbox is greater than 1   
                var str = myfield.value + keychar;
                var array = str.trim().split('.');
             
                if (array.length > 2)
                    return false;
                return true;
            }
            else {
                return false;
            }

        }
        function BindPanel()
        {
            if ($('#ctl00_cntBody_txtJoinDate').val() == "") {
                currentdate = new Date();
                var today = new Date();
                var todaydd = today.getDate();
                var todaymm = today.getMonth() + 1; //January is 0!
                var todayyyyy = today.getFullYear();
                var txtVal = todaydd + '/' + todaymm + '/' + todayyyyy;

            }
            else {
                var txtVal = $('#ctl00_cntBody_txtJoinDate').val();
            }

            var parts = txtVal.split('/');
            var mydate = new Date(parts[2], parts[1] - 1, parts[0])
            var startdate = mydate.getDate() + '/' + (mydate.getMonth() + 1) + '/' + mydate.getFullYear();
            var mydatedd = mydate.getDate();
            var mydatemm = mydate.getMonth() + 1;
            if (mydatedd < 10) {
                mydatedd = '0' + mydatedd;
            }
            if (mydatemm < 10) {
                mydatemm = '0' + mydatemm;
            }
            $('#ctl00_cntBody_txtStartdate').val(mydatedd + '/' + mydatemm + '/' + mydate.getFullYear());
            mydate.setMonth(mydate.getMonth() + 6);
            var dd = mydate.getDate();
            var mm = mydate.getMonth() + 1;
            if (dd < 10) {
                dd = '0' + dd;
            }
            if (mm < 10) {
                mm = '0' + mm;
            }
            //  var txtdate = mydate.getDate() + '/' + (mydate.getMonth() + 1) + '/' + mydate.getFullYear()

            var txtdate = dd + '/' + mm + '/' + mydate.getFullYear()
            $('#ctl00_cntBody_txtEnddate').val(txtdate)
            $('#ctl00_cntBody_txtUtilization').val(0)

            mydate.setMonth(mydate.getMonth())
            var dd1 = mydate.getDate() + 1;
            var mm1 = mydate.getMonth() + 1;
            if (dd1 < 10) {
                dd1 = '0' + dd1;
            }
            if (mm1 < 10) {
                mm1 = '0' + mm1;
            }
            var txtdate1 = dd1 + '/' + mm1 + '/' + mydate.getFullYear()
            $('#ctl00_cntBody_txtStartDate1').val(txtdate1)
            mydate.setMonth(mydate.getMonth() + 3)
            var dd2 = mydate.getDate();
            var mm2 = mydate.getMonth() + 1;
            if (dd2 < 10) {
                dd2 = '0' + dd2;
            }
            if (mm2 < 10) {
                mm2 = '0' + mm2;
            }
            var txtdate2 = dd2 + '/' + mm2 + '/' + mydate.getFullYear()
            $('#ctl00_cntBody_txtEnddate1').val(txtdate2)
            $('#ctl00_cntBody_txtUtilization1').val(25)

            mydate.setMonth(mydate.getMonth())
            var dd3 = mydate.getDate() + 1;
            var mm3 = mydate.getMonth() + 1;
            if (dd3 < 10) {
                dd3 = '0' + dd3;
            }
            if (mm3 < 10) {
                mm3 = '0' + mm3;
            }
            var txtdate3 = dd3 + '/' + mm3 + '/' + mydate.getFullYear()
            $('#ctl00_cntBody_txtStartDate2').val(txtdate3)
            mydate.setMonth(mydate.getMonth() + 3)
            var dd4 = mydate.getDate();
            var mm4 = mydate.getMonth() + 1;
            if (dd4 < 10) {
                dd4 = '0' + dd4;
            }
            if (mm4 < 10) {
                mm4 = '0' + mm4;
            }
            var txtdate4 = dd4 + '/' + mm4 + '/' + mydate.getFullYear()
            $('#ctl00_cntBody_txtEndDate2').val(txtdate4)
            $('#ctl00_cntBody_txtUtilization2').val(50)

            mydate.setMonth(mydate.getMonth())
            var dd5 = mydate.getDate() + 1;
            var mm5 = mydate.getMonth() + 1;
            if (dd5 < 10) {
                dd5 = '0' + dd5;
            }
            if (mm5 < 10) {
                mm5 = '0' + mm5;
            }
            var txtdate5 = dd5 + '/' + mm5 + '/' + mydate.getFullYear()
            $('#ctl00_cntBody_txtStartDate3').val(txtdate5)
            mydate.setMonth(mydate.getMonth() + 3)
            var dd6 = mydate.getDate();
            var mm6 = mydate.getMonth() + 1;
            if (dd6 < 10) {
                dd6 = '0' + dd6;
            }
            if (mm6 < 10) {
                mm6 = '0' + mm6;
            }
            var txtdate6 = dd6 + '/' + mm6 + '/' + mydate.getFullYear()
            $('#ctl00_cntBody_txtEndDate3').val(txtdate6)
            $('#ctl00_cntBody_txtUtilization3').val(75)
            mydate.setMonth(mydate.getMonth())
            var dd7 = mydate.getDate() + 1;
            var mm7 = mydate.getMonth() + 1;
            if (dd7 < 10) {
                dd7 = '0' + dd7;
            }
            if (mm7 < 10) {
                mm7 = '0' + mm7;
            }
            var txtdate7 = dd7 + '/' + mm7 + '/' + mydate.getFullYear()
            $('#ctl00_cntBody_txtStartDate4').val(txtdate7)
            var txtdate8 = "31" + '/' + "12" + '/' + "2099";
            $('#ctl00_cntBody_txtEndDate4').val(txtdate8)
            $('#ctl00_cntBody_txtUtilization4').val(100);
        }

        $(function () {
            $("#<%=txtJoinDate.ClientID %>").datepicker({
                dateFormat: 'dd/mm/yy',
                showOn: "button",
                buttonImage: "Images/calendar.jpg",
                buttonImageOnly: true,

            });
             $("#<%=txtExitDate.ClientID %>").datepicker({
                dateFormat: 'dd/mm/yy',
                showOn: "button",
                buttonImage: "Images/calendar.jpg",
                buttonImageOnly: true,
             });
            var text = $("#ctl00_cntBody_txtExitDate").val()
            var role = $('#ctl00_cntBody_ddlRole :selected').text()
            var checkBoxStatus = $("#ctl00_cntBody_checkBox1").is(':checked');
            if (role != 'Developer Trainee' && role != 'Quality Analyst Trainee' && checkBoxStatus==false )
            {
                $('#<%=PnlEdit.ClientID %>').hide();
            }
            else
            {
                BindPanel();
          
            }

            
            $("#ctl00_cntBody_txtStartdate").prop("readonly", true);
            $("#ctl00_cntBody_txtEnddate").prop("readonly", true);
            $("#ctl00_cntBody_txtUtilization").prop("readonly", true);
            $("#ctl00_cntBody_txtStartDate1").prop("readonly", true); 
            $("#ctl00_cntBody_txtEnddate1").prop("readonly", true);
            $("#ctl00_cntBody_txtUtilization1").prop("readonly", true);
            $("#ctl00_cntBody_txtStartDate2").prop("readonly", true);
            $("#ctl00_cntBody_txtEndDate2").prop("readonly", true);
            $("#ctl00_cntBody_txtUtilization2").prop("readonly", true);
            $("#ctl00_cntBody_txtStartDate3").prop("readonly", true);
            $("#ctl00_cntBody_txtEndDate3").prop("readonly", true);
            $("#ctl00_cntBody_txtUtilization3").prop("readonly", true);
            $("#ctl00_cntBody_txtStartDate4").prop("readonly", true);
            $("#ctl00_cntBody_txtEndDate4").prop("readonly", true);
            $("#ctl00_cntBody_txtUtilization4").prop("readonly", true);

            $("#ctl00_cntBody_txtJoinDate").datepicker();
           
        });
        $(document).on('change', "#ctl00_cntBody_checkBox1", function () {
        
            if ($(this).is(":checked")) {
                $('#<%=PnlEdit.ClientID %>').show()
                BindPanel();
            }
            else {
                var x = confirm("If you change this, resource utilization slab won't be applied to this employee. Are you sure, you don't want to apply resource utilization slab ? ");
                if (x == true) {
                    $('#<%=PnlEdit.ClientID %>').hide();
                }
                else
                {
                    $('#ctl00_cntBody_checkBox1').prop('checked', true);
                }
            }
        });

        $(document).on('change', '#ctl00_cntBody_ddlRole', function () {
            var text = $('#ctl00_cntBody_ddlRole :selected').text()
            var checkBoxStatus = $("#ctl00_cntBody_checkBox1").is(':checked');
            if (checkBoxStatus == true)
            {
                $('#<%=PnlEdit.ClientID %>').show();
                BindPanel();
            }
           else if (text != 'Developer Trainee' && text != 'Quality Analyst Trainee')
            {
                 $('#<%=PnlEdit.ClientID %>').hide()
                $('#ctl00_cntBody_txtStartdate').val("")
                $('#ctl00_cntBody_txtEnddate').val("")
                $('#ctl00_cntBody_txtUtilization').val("")
                $('#ctl00_cntBody_txtStartDate1').val("")
                $('#ctl00_cntBody_txtEnddate1').val("")
                $('#ctl00_cntBody_txtStartDate2').val("")
                $('#ctl00_cntBody_txtEndDate2').val("")
                $('#ctl00_cntBody_txtUtilization1').val("")
                $('#ctl00_cntBody_txtUtilization2').val("")
                $('#ctl00_cntBody_txtUtilization3').val("")
                $('#ctl00_cntBody_txtStartDate4').val("")
                $('#ctl00_cntBody_txtStartDate3').val("")
                $('#ctl00_cntBody_txtEndDate3').val("")
                $('#ctl00_cntBody_txtEndDate4').val("")
                $('#ctl00_cntBody_txtUtilization4').val("")
            }
            else {
               $('#<%=PnlEdit.ClientID %>').show();
               BindPanel();
            }
        });

        $(document).on('change', '#ctl00_cntBody_txtJoinDate', function () {
            var pattern = /^(?:(?:31(\/|-|\.)(?:0?[13578]|1[02]))\1|(?:(?:29|30)(\/|-|\.)(?:0?[1,3-9]|1[0-2])\2))(?:(?:1[6-9]|[2-9]\d)?\d{2})$|^(?:29(\/|-|\.)0?2\3(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00))))$|^(?:0?[1-9]|1\d|2[0-8])(\/|-|\.)(?:(?:0?[1-9])|(?:1[0-2]))\4(?:(?:1[6-9]|[2-9]\d)?\d{2})$/;

            var validateDate = pattern.test($(this).val());

            if (!validateDate) {
                alert('Joining date format is incorrect.')
                $("#ctl00_cntBody_txtJoinDate").val("")
            }
            else {
                var parts = $('#ctl00_cntBody_txtJoinDate').val().split("/");
                var startDate = new Date(parts[2], parts[1] - 1, parts[0]);
                var parts1 = $('#ctl00_cntBody_txtExitDate').val().split("/");
                var endDate = new Date(parts1[2], parts1[1] - 1, parts1[0]);

                if (startDate > endDate) {
                    alert('Releaving date should be greater than joining date.')
                    $('#ctl00_cntBody_txtExitDate').val("")
                }

            }
            var text = $('#ctl00_cntBody_ddlRole :selected').text()
            if (text == 'Developer Trainee' || text == 'Quality Analyst Trainee') {
                var txtVal = $('#ctl00_cntBody_txtJoinDate').val()
                $('#ctl00_cntBody_txtStartdate').val(txtVal)
                var parts = txtVal.split('/');
                var mydate = new Date(parts[2], parts[1] - 1, parts[0])
                mydate.setMonth(mydate.getMonth() + 6);
                //var txtdate=mydate.getDate() + '/' + (mydate.getMonth()+1) + '/' + mydate.getFullYear()
                var dd = mydate.getDate();
                var mm = mydate.getMonth() + 1;
                if (dd < 10) {
                    dd = '0' + dd;
                }
                if (mm < 10) {
                    mm = '0' + mm;
                }
                //  var txtdate = mydate.getDate() + '/' + (mydate.getMonth() + 1) + '/' + mydate.getFullYear()

                var txtdate = dd + '/' + mm + '/' + mydate.getFullYear()
                $('#ctl00_cntBody_txtEnddate').val(txtdate)
                $('#ctl00_cntBody_txtUtilization').val(0)

                mydate.setMonth(mydate.getMonth())
                var dd1 = mydate.getDate() + 1;
                var mm1 = mydate.getMonth() + 1;
                if (dd1 < 10) {
                    dd1 = '0' + dd1;
                }
                if (mm1 < 10) {
                    mm1 = '0' + mm1;
                }
                var txtdate1 = dd1 + '/' + mm1 + '/' + mydate.getFullYear()
              //  var txtdate1 = (mydate.getDate()+1) + '/' + (mydate.getMonth()+1) + '/' + mydate.getFullYear()
                $('#ctl00_cntBody_txtStartDate1').val(txtdate1)
                mydate.setMonth(mydate.getMonth() + 3)
                var dd2 = mydate.getDate();
                var mm2 = mydate.getMonth() + 1;
                if (dd2 < 10) {
                    dd2 = '0' + dd2;
                }
                if (mm2 < 10) {
                    mm2 = '0' + mm2;
                }
                var txtdate2 = dd2 + '/' + mm2 + '/' + mydate.getFullYear()
                $('#ctl00_cntBody_txtEnddate1').val(txtdate2)
                $('#ctl00_cntBody_txtUtilization1').val(25)

                mydate.setMonth(mydate.getMonth())
                var dd3 = mydate.getDate() + 1;
                var mm3 = mydate.getMonth() + 1;
                if (dd3 < 10) {
                    dd3 = '0' + dd3;
                }
                if (mm3 < 10) {
                    mm3 = '0' + mm3;
                }
                var txtdate3 = dd3 + '/' + mm3 + '/' + mydate.getFullYear()
                $('#ctl00_cntBody_txtStartDate2').val(txtdate3)
                mydate.setMonth(mydate.getMonth() + 3)
                var dd4 = mydate.getDate();
                var mm4 = mydate.getMonth() + 1;
                if (dd4 < 10) {
                    dd4 = '0' + dd4;
                }
                if (mm4 < 10) {
                    mm4 = '0' + mm4;
                }
                var txtdate4 = dd4 + '/' + mm4 + '/' + mydate.getFullYear()
                $('#ctl00_cntBody_txtEndDate2').val(txtdate4)
                $('#ctl00_cntBody_txtUtilization2').val(50)

                mydate.setMonth(mydate.getMonth())
                var dd5 = mydate.getDate() + 1;
                var mm5 = mydate.getMonth() + 1;
                if (dd5 < 10) {
                    dd5 = '0' + dd5;
                }
                if (mm5 < 10) {
                    mm5 = '0' + mm5;
                }
                var txtdate5 = dd5 + '/' + mm5 + '/' + mydate.getFullYear()
                $('#ctl00_cntBody_txtStartDate3').val(txtdate5)
                mydate.setMonth(mydate.getMonth() + 3)
                var dd6 = mydate.getDate();
                var mm6 = mydate.getMonth() + 1;
                if (dd6 < 10) {
                    dd6 = '0' + dd6;
                }
                if (mm6 < 10) {
                    mm6 = '0' + mm6;
                }
                var txtdate6 = dd6 + '/' + mm6 + '/' + mydate.getFullYear()
                $('#ctl00_cntBody_txtEndDate3').val(txtdate6)
                $('#ctl00_cntBody_txtUtilization3').val(75)
                mydate.setMonth(mydate.getMonth())
                var dd7 = mydate.getDate() + 1;
                var mm7 = mydate.getMonth() + 1;
                if (dd7 < 10) {
                    dd7 = '0' + dd7;
                }
                if (mm7 < 10) {
                    mm7 = '0' + mm7;
                }
                var txtdate7 = dd7 + '/' + mm7 + '/' + mydate.getFullYear()
                $('#ctl00_cntBody_txtStartDate4').val(txtdate7)
               
                var txtdate8 = "31" + '/' + "12" + '/' + "2099";

                $('#ctl00_cntBody_txtEndDate4').val(txtdate8)
                $('#ctl00_cntBody_txtUtilization4').val(100)
            }
            else {
                $('#ctl00_cntBody_txtStartdate').val("")
                $('#ctl00_cntBody_txtEnddate').val("")
                $('#ctl00_cntBody_txtUtilization').val("")
                $('#ctl00_cntBody_txtStartDate1').val("")
                $('#ctl00_cntBody_txtEnddate1').val("")
                $('#ctl00_cntBody_txtStartDate2').val("")
                $('#ctl00_cntBody_txtEndDate2').val("")
                $('#ctl00_cntBody_txtUtilization1').val("")
                $('#ctl00_cntBody_txtUtilization2').val("")
                $('#ctl00_cntBody_txtUtilization3').val("")
                $('#ctl00_cntBody_txtStartDate4').val("")
                $('#ctl00_cntBody_txtStartDate3').val("")
                $('#ctl00_cntBody_txtEndDate3').val("")
                $('#ctl00_cntBody_txtEndDate4').val("")
                $('#ctl00_cntBody_txtUtilization4').val("")
            }

        });

        $(document).on('change', '#ctl00_cntBody_txtExitDate', function () {
            var pattern = /^(?:(?:31(\/|-|\.)(?:0?[13578]|1[02]))\1|(?:(?:29|30)(\/|-|\.)(?:0?[1,3-9]|1[0-2])\2))(?:(?:1[6-9]|[2-9]\d)?\d{2})$|^(?:29(\/|-|\.)0?2\3(?:(?:(?:1[6-9]|[2-9]\d)?(?:0[48]|[2468][048]|[13579][26])|(?:(?:16|[2468][048]|[3579][26])00))))$|^(?:0?[1-9]|1\d|2[0-8])(\/|-|\.)(?:(?:0?[1-9])|(?:1[0-2]))\4(?:(?:1[6-9]|[2-9]\d)?\d{2})$/;

            var validateDate = pattern.test($(this).val());

            if (!validateDate) {
                alert('Releaving date format is incorrect.')
                $("#ctl00_cntBody_txtExitDate").val("")
            }
            else {
                var parts = $('#ctl00_cntBody_txtJoinDate').val().split("/");
                var startDate = new Date(parts[2], parts[1] - 1, parts[0]);
                var parts1 = $('#ctl00_cntBody_txtExitDate').val().split("/");
                var endDate = new Date(parts1[2], parts1[1] - 1, parts1[0]);

                if (startDate > endDate) {
                    alert('Releaving date should be greater than joining date.')
                    $('#ctl00_cntBody_txtExitDate').val("")
                }

            }

        });
        
       

    </script>
    <div style="width: 100%;">
         <style type="text/css" media="screen">
   
                BODY {  width:100%}
             .auto-style1 {
                 height: 18px;
             }
             .auto-style2 {
                 height: 21px;
             }
             .auto-style3 {
                 height: 18px;
                 width: 162px;
             }
             .auto-style4 {
                 width: 162px;
             }
             .auto-style5 {
                 height: 21px;
                 width: 162px;
             }
        .ui-datepicker-trigger
        {
            margin-left: 160px;
            margin-top: -17px;
            margin-bottom: 0px;
            margin-right: -17px;

        }
             .auto-style6 {
                 width: 162px;
                 height: 22px;
             }
             .ui-datepicker ui-widget ui-widget-content ui-helper-clearfix ui-corner-all
             {
                 
                 left: 892px !important; 
                 
             }
     </style>
        <table width="60%">
            <tr>
                <td align="left" style="font-size: 15px; border-bottom: 1px solid #ccc;">
                    <b>
                        <asp:Label ID="lblResourceStatus" runat="server" Text="Create Resource"></asp:Label></b>
                </td>
            </tr>
        </table>
        <table class="style1">
            <tr>
                <td class="auto-style1">&nbsp;
                </td>
                <td class="auto-style3">&nbsp;
                </td>
                <td class="auto-style1">&nbsp;
                </td>
            </tr>
            <tr>
                <td class="style2" align="left">Employee Code :
                </td>
                <td align="left" class="auto-style4">
                    <asp:TextBox ID="txtemployeecode" runat="server" Width="143px" TabIndex="1"></asp:TextBox>
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="EmployeeCodeValidator" runat="server" ErrorMessage="Enter Employee Code."
                        ControlToValidate="txtemployeecode"></asp:RequiredFieldValidator>
                </td>
            </tr>

            <tr>
                <td class="style2" align="left">Resource Name :
                </td>
                <td align="left" class="auto-style4">
                    <asp:TextBox ID="txtResourceName" runat="server" Width="143px" TabIndex="1"></asp:TextBox>
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="ResourceNameFieldValidator" runat="server" ErrorMessage="Enter Resource Name."
                        ControlToValidate="txtResourceName"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="style2" align="left">Default Role :
                </td>
                <td valign="middle" align="left" nowrap="nowrap" class="auto-style4">
                    <asp:DropDownList ID="ddlRole" OnSelectedIndexChanged="ddlRole_SelectedIndexChanged" AutoPostBack="true"  runat="server" Style="width: 153px;"   TabIndex="2"
                         >
                    </asp:DropDownList>

                </td>
                <td align="left">
                    <asp:LinkButton ID="lnkAddRole" Text="Add Role" runat="server" TabIndex="-1"></asp:LinkButton>
                    <asp:RequiredFieldValidator ID="rfvRole" ControlToValidate="ddlRole" InitialValue="0"
                        runat="server" ErrorMessage="Select Role."></asp:RequiredFieldValidator>
                </td>
                
            </tr>
            <%--  Code to bind team--%>
            <tr>
                <td class="style2" align="left">Team :
                </td>
                <td valign="middle" align="left" nowrap="nowrap" class="auto-style4">
                    <asp:DropDownList ID="ddlTeam" runat="server" Style="width: 153px;" TabIndex="2">
                    </asp:DropDownList>

                </td>
                <td align="left">
                    <asp:RequiredFieldValidator ID="rfvTeam" ControlToValidate="ddlTeam" InitialValue="0"
                        runat="server" ErrorMessage="Select Team."></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td align="left">Billing Start Month :
                </td>
                <td align="left" class="auto-style4">
                    <asp:DropDownList ID="ddlMonth" runat="server" Style="width: 90px;" TabIndex="2" onchange="javascript:return clearError();">
                        <asp:ListItem Value="0">Select Month</asp:ListItem>
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
                    &nbsp;
                    <asp:DropDownList ID="ddlYear" runat="server" Style="width: 50px;" TabIndex="2">
                    </asp:DropDownList>
                </td>
                <td align="left">
                    <span style="width: 100%; color: red;" id="errorSpan" runat="server" name="errorSpan"></span>
                    <asp:RequiredFieldValidator ID="rfvMonth" ControlToValidate="ddlMonth" InitialValue="0"
                        runat="server" ErrorMessage="Select Month."></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="style2" align="left">Weekly Hours :
                </td>
                <td valign="middle" align="left" nowrap="nowrap" class="auto-style4">
                    <asp:TextBox ID="txtWeeklyHours" runat="server" Width="143px" TabIndex="1" onkeypress="return CheckNemericValue(event,this);" MaxLength="4"></asp:TextBox>
                </td>
                <td align="left">
                    <span style="width: 100%; color: red;" id="errorSpanweekly" runat="server" name="errorSpanweekly"></span>
                    <asp:RequiredFieldValidator ID="WeeklyHoursValidator" runat="server" ErrorMessage="Enter Weekly Hours."
                        ControlToValidate="txtWeeklyHours"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="style2" align="left" style="height: 21px">Cost Centre :
                </td>
                <td valign="middle" align="left" nowrap="nowrap" class="auto-style5">
                    <asp:DropDownList ID="ddlCostCentre" runat="server" Style="width: 153px;" TabIndex="2">
                    </asp:DropDownList>

                </td>
                <td align="left" style="height: 21px">
                    <asp:RequiredFieldValidator ID="CostCentreValidator" ControlToValidate="ddlCostCentre" InitialValue="0"
                        runat="server" ErrorMessage="Select Cost Centre."></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="style2" align="left" style="height: 21px">Joining Date :
                </td>
                <td valign="middle" align="left" nowrap="nowrap" class="auto-style4">
                    <asp:TextBox ID="txtJoinDate" runat="server" Width="143px" TabIndex="1"></asp:TextBox>
                </td>
                <td align="left" style="height: 21px">
                     &nbsp; &nbsp; &nbsp;
                     <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ErrorMessage="Enter Joining Date." ControlToValidate="txtJoinDate"></asp:RequiredFieldValidator>  

                    &nbsp;</td>
            </tr>
               <tr>
                <td class="style2" align="left" style="height: 21px">Releaving Date :
                </td>
                <td valign="middle" align="left" nowrap="nowrap" class="auto-style4">
                    <asp:TextBox ID="txtExitDate" runat="server" Width="143px" TabIndex="1"></asp:TextBox>
                </td>
                   <td align="left" style="height: 21px">
                       &nbsp; &nbsp; &nbsp;
                   </td>
            </tr>
            <%--<tr>
                <td class="style2" align="left" style="height: 21px">Billing Role :
                </td>
                <td class="auto-style5" align="left">
                <asp:DropDownList ID="ddlBillingRole" runat="server" Style="width: 153px;" TabIndex="2"></asp:DropDownList>
                </td>
                <td align="left" style="height: 21px">
                    <asp:LinkButton ID="lnkAddbillingrole" Text="Add Billing Role" runat="server" TabIndex="-1"></asp:LinkButton>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="ddlBillingRole" InitialValue="0"
                        runat="server" ErrorMessage="Select Billing Role."></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="auto-style2" align="left">Billing Group :
                </td>
                <td class="auto-style5" align="left">
                <asp:DropDownList ID="ddlBillingGroup" runat="server" Style="width: 153px;" TabIndex="2"></asp:DropDownList>
                </td>
                <td align="left" style="height: 21px">
                    <asp:LinkButton ID="lnkBillinggroup" Text="Add Billing Group" runat="server" TabIndex="-1"></asp:LinkButton>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ControlToValidate="ddlBillinggroup" InitialValue="0"
                        runat="server" ErrorMessage="Select Billing Group."></asp:RequiredFieldValidator>
                </td>
            </tr>--%>
            <tr>
                <td class="style2" align="left">Status :
                </td>
                <td valign="middle" align="left" nowrap="nowrap" class="auto-style4">
                    <table>
                        <tr>
                            <td>
                                <asp:RadioButton ID="RdActive" runat="server" Text=" Active" Checked="True" GroupName="Status"
                                    TabIndex="3" />
                            </td>
                            <td>
                                <asp:RadioButton ID="RdInActive" runat="server" Text="Inactive" GroupName="Status"
                                    TabIndex="4" />
                            </td>
                        </tr>
                    </table>
                </td>
                <td>&nbsp;
                </td>
            </tr>
            <tr>
                <td height="15px" colspan="3">
                    <asp:Label ID="lblEmployeecode" runat="server" Visible="false" ForeColor="Red"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    
                    <asp:CheckBox ID="checkBox1"  visible="false"   runat="server" />
                    <asp:Label runat="server" Font-Bold="true" ID="checkBoxText" Visible="false"  Text="Apply resource utilization slab."></asp:Label>
                        
                </td>
            </tr>
    </table>
    </div>
    <asp:Panel ID="PnlEdit" visible="true" runat="server">
        <table class="style1">
            <tr>
                <td class="style2" align="left">
                </td>
                <td class="style2" align="left">
                </td>
                <td class="style2" align="left">
                </td>
            </tr>
            <tr>
                <td class="style2" align="left">
                </td>
                <td class="style2" align="left">
                </td>
                <td class="style2" align="left">
                </td>
            </tr>
        </table>

        <table class="style1">
            <tr>
                <td class="style2" align="Middle"><b>Start Date</b>
                </td>
                <td class="style2" align="Middle"><b>End Date</b>
                </td>
                <td class="style2" align="Middle"><b>Utilization Percentage</b>
                </td>
            </tr>
            <tr>
                <td valign="middle" align="left" nowrap="nowrap" style="width: 162px;">
                    <asp:TextBox ID="txtStartdate" runat="server" Width="143px" TabIndex="1"></asp:TextBox>
                </td>
                <td valign="middle" align="left" nowrap="nowrap" style="width: 162px;">
                    <asp:TextBox ID="txtEnddate" runat="server" Width="143px" TabIndex="1"></asp:TextBox>
                </td>
                <td valign="middle" align="left" nowrap="nowrap" style="width: 162px;">
                    <asp:TextBox ID="txtUtilization" runat="server" Width="143px" TabIndex="1"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td valign="middle" align="left" nowrap="nowrap" style="width: 162px;">
                    <asp:TextBox ID="txtStartDate1" runat="server" Width="143px" TabIndex="1"></asp:TextBox>
                </td>
                <td valign="middle" align="left" nowrap="nowrap" style="width: 162px;">
                    <asp:TextBox ID="txtEnddate1" runat="server" Width="143px" TabIndex="1"></asp:TextBox>
                </td>
                <td valign="middle" align="left" nowrap="nowrap" style="width: 162px;">
                    <asp:TextBox ID="txtUtilization1" runat="server" Width="143px" TabIndex="1"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td valign="middle" align="left" nowrap="nowrap" class="auto-style6">
                    <asp:TextBox ID="txtStartDate2" runat="server" Width="143px" TabIndex="1"></asp:TextBox>
                </td>
                <td valign="middle" align="left" nowrap="nowrap" class="auto-style6">
                    <asp:TextBox ID="txtEndDate2" runat="server" Width="143px" TabIndex="1"></asp:TextBox>
                </td>
                <td valign="middle" align="left" nowrap="nowrap" class="auto-style6">
                    <asp:TextBox ID="txtUtilization2" runat="server" Width="143px" TabIndex="1"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td valign="middle" align="left" nowrap="nowrap" class="auto-style6">
                    <asp:TextBox ID="txtStartDate3" runat="server" Width="143px" TabIndex="1"></asp:TextBox>
                </td>
                <td valign="middle" align="left" nowrap="nowrap" class="auto-style6">
                    <asp:TextBox ID="txtEndDate3" runat="server" Width="143px" TabIndex="1"></asp:TextBox>
                </td>
                <td valign="middle" align="left" nowrap="nowrap" class="auto-style6">
                    <asp:TextBox ID="txtUtilization3" runat="server" Width="143px" TabIndex="1"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td valign="middle" align="left" nowrap="nowrap" style="width: 162px;">
                    <asp:TextBox ID="txtStartDate4" runat="server" Width="143px" TabIndex="1"></asp:TextBox>
                </td>
                <td valign="middle" align="left" nowrap="nowrap" style="width: 162px;">
                    <asp:TextBox ID="txtEndDate4" runat="server" Width="143px" TabIndex="1"></asp:TextBox>
                </td>
                <td valign="middle" align="left" nowrap="nowrap" style="width: 162px;">
                    <asp:TextBox ID="txtUtilization4" runat="server" Width="143px" TabIndex="1"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="3" align="center">
                    &nbsp;&nbsp; &nbsp;&nbsp;
                </td>
                <td>&nbsp;</td>
            </tr>
        </table>

    </asp:Panel>
    <table class="style1">
        <tr>
            <td colspan="3" align="center">
                    &nbsp;&nbsp;
                    <asp:Button ID="btnSave" runat="server" Text="Save" Width="60px" TabIndex="5" OnClick="btnSave_Click"  style="margin-left: 28px" />
                    &nbsp;&nbsp;
                    <asp:Button ID="btnCancel" runat="server" Width="73px" Text="Cancel" CausesValidation="false"
                        OnClick="btnCancel_Click" TabIndex="6" style="margin-left: 25px" />
                </td>
                <td>&nbsp;</td>
        </tr>
    </table>
</asp:Content>
