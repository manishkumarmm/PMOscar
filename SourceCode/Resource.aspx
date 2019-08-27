<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/PMOMaster.master"
    CodeBehind="Resource.aspx.cs" Inherits="PMOscar.Resource" %>

<asp:Content ContentPlaceHolderID="cntBody" runat="server">
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
    </script>
    <div style="width: 100%;">
         <style type="text/css" media="screen">
   
                BODY {  width:100%}
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
                <td class="style2">&nbsp;
                </td>
                <td>&nbsp;
                </td>
                <td>&nbsp;
                </td>
            </tr>
            <tr>
                <td class="style2" align="left">Employee Code :
                </td>
                <td align="left">
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
                <td align="left">
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
                <td valign="middle" align="left" nowrap="nowrap" style="width: 162px;">
                    <asp:DropDownList ID="ddlRole" runat="server" Style="width: 153px;" TabIndex="2"
                         OnSelectedIndexChanged="ddlRole_SelectedIndexChanged">
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
                <td valign="middle" align="left" nowrap="nowrap" style="width: 162px;">
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
                <td align="left">
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
                <td valign="middle" align="left" nowrap="nowrap" style="width: 162px;">
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
                <td valign="middle" align="left" nowrap="nowrap" style="width: 162px; height: 21px;">
                    <asp:DropDownList ID="ddlCostCentre" runat="server" Style="width: 153px;" TabIndex="2">
                    </asp:DropDownList>

                </td>
                <td align="left" style="height: 21px">
                    <asp:RequiredFieldValidator ID="CostCentreValidator" ControlToValidate="ddlCostCentre" InitialValue="0"
                        runat="server" ErrorMessage="Select Cost Centre."></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="style2" align="left">Status :
                </td>
                <td valign="middle" align="left" nowrap="nowrap" style="width: 162px;">
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
                <td colspan="3" align="center">
                    &nbsp;&nbsp;
                    <asp:Button ID="btnSave" runat="server" Text="Save" Width="60px" TabIndex="5" OnClick="btnSave_Click" style="margin-left: 28px" />
                    &nbsp;&nbsp;
                    <asp:Button ID="btnCancel" runat="server" Width="73px" Text="Cancel" CausesValidation="false"
                        OnClick="btnCancel_Click" TabIndex="6" style="margin-left: 25px" />
                </td>
                <td>&nbsp;</td>
            </tr>
        </table>
    </div>
</asp:Content>
