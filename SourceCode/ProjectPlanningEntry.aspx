<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/PMOMaster.master"
    CodeBehind="ProjectPlanningEntry.aspx.cs" Inherits="PMOscar.ProjectPlanningEntry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="TimeTrackerAudit.ascx" TagName="TimeTrackerAudit" TagPrefix="uc1" %>
<asp:Content ContentPlaceHolderID="cntHead" runat="server">
    <a id="A1" href="ResourcePlanning.aspx" runat="server" /></a>
</asp:Content>
<asp:Content ContentPlaceHolderID="cntBody" runat="server">
    <script type="text/javascript">


    function CheckTextValue(e, myfield) {

        var key;
        key = e.which ? e.which : e.keyCode;
        keychar = String.fromCharCode(key);

  if ((key == 9)||(key == 8))
              return true;

          
       else  if ((("abcdefghijklmnopqrstuvwxyz").indexOf(keychar) > -1))
            return true;


        // numbers
        else if ((("ABCDEFGHIJKLMNOPQRSTUVWXYZ").indexOf(keychar) > -1))
            return true;


        else if (((".';:!@#$%&*^(){}|/").indexOf(keychar) > -1))
            return true;

        else {
            return false;
        }
    }


    function CheckNemericValue(e, myfield) {
        
        var key;
        key = e.which ? e.which : e.keyCode;
        keychar = String.fromCharCode(key);

        if ((key == null) || (key == 0) || (key == 8) ||
                  (key == 9) || (key == 13) || (key == 27) )
            return true;


        // numbers
        else if ((("0123456789").indexOf(keychar) > -1))
            return true;

        else {
            return false;
        }



    }

    function CheckValue() {

        var txt1 = document.getElementById("ctl00_cntBody_txtEHours").value;
        var txt2 = document.getElementById("ctl00_cntBody_txtAHours").value;

        if (parseInt(txt1) < parseInt(txt2)) {

            alert("Actual Hours are more than the Estimated Hours");

            var Budget = document.getElementById("ctl00_cntBody_HfBudgetHours").value;
           
            if ((parseInt(Budget)) < parseInt(txt2)) {
                alert("Actual Hours are more than the Budgeted Hours");
            }

        }
    }

     function ChangeHistoryPopUp()
    {
   
          var modalDialog = $find("ModalPopupExtender1"); 
          if (modalDialog != null) {
          modalDialog.show();
     }
 
    }

    </script>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div>
                <div style="width: 95%; text-align: right">
                    <asp:LinkButton ID="lbBack" Text="<< Back" runat="server" OnClick="lbBack_Click"
                        CausesValidation="false" TabIndex="9"></asp:LinkButton>
                </div>
                <div style="width: 60%;">
                    <table id="Table1" runat="server" width="100%">
                        <tr>
                            <td>
                                <table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td height="50" align="left" style="width: 350px">
                                            <div id="pageTitle">
                                                Project :
                                                <asp:LinkButton ID="lblProject" runat="server" CausesValidation="False" OnClick="lblProject_Click"></asp:LinkButton>
                                            </div>
                                            <asp:HiddenField ID="HfBudgetHours" runat="server" />
                                            <asp:HiddenField ID="HFActualHours" runat="server" />
                                        </td>
                                        <td height="25" align="left" style="width: 276px">
                                            <div id="pageTitle">
                                                Week :
                                                <asp:Label ID="lblWeek" runat="server" Text="Week"></asp:Label>
                                            </div>
                                        </td>
                                        <td height="25" align="left">
                                            &nbsp;
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table width="100%">
                                    <tr>
                                        <td align="left" style="height: 35px; width: 90px;">
                                            <asp:Label ID="Label4" runat="server" Text="Year:"></asp:Label>
                                        </td>
                                        <td align="left" style="height: 5px; width: 80px;">
                                            <asp:DropDownList ID="ddlYear" runat="server" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged"
                                                AutoPostBack="True" Height="20px" Width="74px">
                                            </asp:DropDownList>
                                        </td>
                                        <td align="left" style="height: 5px; width: 90px;">
                                            <asp:Label ID="Label5" runat="server" Text="Month:"></asp:Label>
                                        </td>
                                        <td align="left" style="height: 5px; width: 85px">
                                            <asp:DropDownList ID="ddlMonth" runat="server" OnSelectedIndexChanged="ddlMonth_SelectedIndexChanged"
                                                AutoPostBack="True" Height="20px">
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
                                        <td align="center" style="height: 5px">
                                            <asp:Label ID="Label6" runat="server" Text="Week:"></asp:Label>
                                        </td>
                                        <td align="left" style="height: 5px">
                                            <asp:DropDownList ID="ddlWeek" runat="server" Height="20px" AutoPostBack="True" OnSelectedIndexChanged="ddlWeek_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table id="Table2" width="100%" runat="server">
                                    <tr>
                                        <td align="left" style="width: 60px; height: 19px;">
                                            Resource :
                                        </td>
                                        <td valign="middle" align="left" nowrap="nowrap" style="width: 145px; height: 19px;">
                                            <asp:DropDownList ID="ddlResources" runat="server" Style="width: 120px;" TabIndex="1"
                                                AutoPostBack="True" OnSelectedIndexChanged="ddlResources_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 5%; height: 19px;" align="left">
                                            Role :
                                        </td>
                                        <td valign="middle" align="left" nowrap="nowrap" style="width: 150px; height: 19px;">
                                            <asp:DropDownList ID="ddlRole" runat="server" Style="width: 120px;" TabIndex="2" AutoPostBack="True" OnSelectedIndexChanged="ddlRole_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                        <td align="left" nowrap="nowrap" style="width: 46px; height: 19px;">
                                            Phase :
                                        </td>
                                        <td align="left" nowrap="nowrap" style="height: 19px; width: 145px;">
                                            <asp:DropDownList ID="ddlPhase" runat="server" Style="width: 120px;" TabIndex="3" AutoPostBack="True" OnSelectedIndexChanged="ddlPhase_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </td>
                                        <%-- Team dropdown   --%>
                                        <td align="left" nowrap="nowrap" style="width: 5%">
                                            Team :
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlTeam" TabIndex="4" runat="server">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="rfvProject" ControlToValidate="ddlResources" InitialValue="0"
                                                runat="server" ErrorMessage="Select Resource"></asp:RequiredFieldValidator>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="rfvRole" ControlToValidate="ddlRole" InitialValue="0"
                                                runat="server" ErrorMessage="Select Role"></asp:RequiredFieldValidator>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="RqResEntry" ControlToValidate="ddlPhase" InitialValue="0"
                                                runat="server" ErrorMessage="Select Phase"></asp:RequiredFieldValidator>
                                        </td>
                                        <td>
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator ID="RqTeamEntry" ControlToValidate="ddlTeam" InitialValue="0"
                                                runat="server" ErrorMessage="Select Team"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div style="padding: 10px 0px 25px 0px; float: left; width: 95%">
                                    <table width="100%" style="border: 1px solid black" cellpadding="5">
                                        <tr class="bold">
                                            <td align="left" style="width: 6%">
                                                Week
                                            </td>
                                            <td align="left" style="width: 7%">
                                                Month
                                            </td>
                                            <td nowrap="nowrap" style="width: 15%;" align="center">
                                                Estimated time
                                            </td>
                                            <td nowrap="nowrap" style="width: 17%; padding-top: 15px;">
                                            </td>
                                            <td style="width: 16%" nowrap="nowrap" align="center">
                                                Actual Time
                                            </td>
                                            <td style="width: 17%; padding-top: 15px;">
                                            </td>
                                        </tr>
                                        <tr id="trWeek1" runat="server" align="left">
                                            <td>
                                                <asp:Label ID="lblWeek1" Text="" runat="server" />
                                            </td>
                                            <td>
                                                <asp:Label ID="lblMonth1" Text="" runat="server" />
                                            </td>
                                            <td align="center">
                                                <asp:TextBox ID="txtEHours" runat="server" Width="110px" TabIndex="5" ValidationGroup="v"></asp:TextBox>
                                            </td>
                                            <td valign="middle">
                                                <asp:RequiredFieldValidator ID="rfvEHours" ControlToValidate="txtEHours" runat="server"
                                                    ErrorMessage="Enter Estimated Time"></asp:RequiredFieldValidator>
                                                <br />
                                                <asp:RegularExpressionValidator ID="revEHours" runat="server" ControlToValidate="txtEHours"
                                                    ErrorMessage="Please enter only numbers" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                                            </td>
                                            <td align="center">
                                                <asp:TextBox ID="txtAHours" runat="server" Width="110px" TabIndex="6"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:RegularExpressionValidator ID="revAHours" runat="server" ControlToValidate="txtAHours"
                                                    ErrorMessage="Please enter only numbers" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                                                <asp:HiddenField ID="hdnWeek1StartDate" runat="server" />
                                                <asp:HiddenField ID="hdnWeek1EndDate" runat="server" />
                                            </td>
                                        </tr>
                                        <tr id="trWeek2" runat="server" align="left">
                                            <td>
                                                <asp:Label ID="lblWeek2" Text="" runat="server" />
                                            </td>
                                            <td>
                                                <asp:Label ID="lblMonth2" Text="" runat="server" />
                                            </td>
                                            <td align="center">
                                                <asp:TextBox ID="txtEHours2" runat="server" Width="110px" TabIndex="7" CausesValidation="True"
                                                    ValidationGroup="v" MaxLength="5"></asp:TextBox>
                                            </td>
                                            <td valign="middle">
                                                <asp:RequiredFieldValidator ID="rfvEHours2" ControlToValidate="txtEHours2" runat="server"
                                                    ErrorMessage="Enter Estimated time"></asp:RequiredFieldValidator><br />
                                                <asp:RegularExpressionValidator ID="revEHours2" runat="server" ControlToValidate="txtEHours2"
                                                    ErrorMessage="Please enter only numbers" ValidationExpression="^\d+$"></asp:RegularExpressionValidator>
                                            </td>
                                            <td align="center">
                                                <asp:TextBox ID="txtAHours2" runat="server" Width="110px" TabIndex="8" MaxLength="5"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:RegularExpressionValidator ID="rfvAHours2" runat="server" ControlToValidate="txtAHours2"
                                                    ErrorMessage="Please enter only numbers" ValidationExpression="^\d+$" ValidationGroup="save"></asp:RegularExpressionValidator>
                                                <asp:HiddenField ID="hdnWeek2StartDate" runat="server" />
                                                <asp:HiddenField ID="hdnWeek2EndDate" runat="server" />
                                            </td>
                                        </tr>
                                    </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table>
                                    <tr>
                                        <td nowrap="nowrap" style="width: 14%" align="left">
                                            Comments :
                                        </td>
                                        <td align="left" nowrap="nowrap" colspan="3">
                                            <asp:TextBox ID="txtComments" runat="server" Width="430px" TabIndex="9" TextMode="MultiLine"></asp:TextBox>
                                        </td>
                                        <td style="width: 46px">
                                        <asp:HiddenField ID="hdnSaveCheck" runat="server" />
                                            <asp:Button ID="btnSave" runat="server" Text="Save" class="submitButton" TabIndex="8"
                                                Width="48px" OnClick="btnSave_Click" />
                                        </td>
                                        <td align="left">
                                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click"
                                                class="submitButton" CausesValidation="false" TabIndex="-1" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" nowrap="nowrap" colspan="6">
                                            <asp:Label ID="Label7" runat="server" ForeColor="Red"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" colspan="6" nowrap="nowrap">
                                            <asp:Label ID="lblPjct" runat="server" ForeColor="Red" Visible="False"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" nowrap="nowrap" colspan="6">
                                            <asp:Label ID="lblMsgStatus" runat="server" ForeColor="Red" Visible="False"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" nowrap="nowrap" colspan="6">
                                            <asp:Label ID="lblMsgStatus1" runat="server" ForeColor="Red" Visible="False"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                        <td>
                                        </td>
                                        <td colspan="2">
                                        </td>
                                        <td colspan="2" width="150px">
                                            <asp:LinkButton ID="lnkChng" Text="Change History" runat="server" TabIndex="-1" CausesValidation="false"
                                                OnClientClick="javascript:return ChangeHistoryPopUp();"></asp:LinkButton>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                <asp:GridView ID="dgProjectDetails" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                    ForeColor="#333333" Width="700px" ShowFooter="True" AllowSorting="True" OnRowDataBound="gvProjectDetails_RowDataBound"
                                    OnSorting="gvProjectDetails_Sorting" TabIndex="-1">
                                    <RowStyle BackColor="#EFF3FB" />
                                    <Columns>
                                        <asp:BoundField HeaderText="TimeTrackerID" DataField="TimeTrackerId" />
                                        <asp:BoundField HeaderText="Resource Name" DataField="ResourceName" SortExpression="ResourceName" ItemStyle-Width="15%">
                                            <HeaderStyle ForeColor="#FCFF00" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Role" DataField="Role" SortExpression="Role">
                                            <HeaderStyle ForeColor="#FCFF00" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Week" DataField="Week" SortExpression="Week">
                                            <HeaderStyle ForeColor="#FCFF00" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Phase" DataField="Phase" SortExpression="Phase">
                                            <HeaderStyle ForeColor="#FCFF00" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Team" DataField="Team" SortExpression="Team">
                                            <HeaderStyle ForeColor="#FCFF00" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Estimated Hours" DataField="EstimatedHours" SortExpression="EstimatedHours">
                                            <HeaderStyle ForeColor="#FCFF00" />
                                        </asp:BoundField>
                                        <asp:BoundField HeaderText="Actual Hours" DataField="ActualHours" SortExpression="ActualHours">
                                            <HeaderStyle ForeColor="#FCFF00" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="Edit">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="hplink" runat="server" NavigateUrl='<%# "ProjectPlanningEntry.aspx?TTId=" + 
                                         DataBinder.Eval(Container.DataItem,"TimeTrackerId")+ " &week=" + ddlWeek.SelectedItem.Text +" &year=" + ddlYear.SelectedItem.Text +" &month="+ ddlMonth.SelectedIndex+" &index="+ ddlWeek.SelectedIndex+" &ProjectEditId=" +  projectId + " &Projectname=" + lblProject.Text +"&op=E" %>'>Edit </asp:HyperLink>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Delete">
                                            <ItemTemplate>
                                                <a id="aItemDelete" runat="server" onclick="javascript: return confirm('Are you sure you want to delete?');"
                                                    href='<%# "ProjectPlanningEntry.aspx?TTId=" + DataBinder.Eval(Container.DataItem,"TimeTrackerId")+ " &week=" + ddlWeek.SelectedItem.Text +" &year=" + ddlYear.SelectedItem.Text +" &month="+ ddlMonth.SelectedIndex+" &index="+ ddlWeek.SelectedIndex+"&ProjectDeleteId=" + projectId + " &Projectname=" + lblProject.Text +"&op=D" %>'>
                                                    Delete</a>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="Status" DataField="Status" />
                                        <asp:BoundField HeaderText="IsActive" DataField="IsActive" />
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
            </div>
            <asp:Button ID="btnShowPopup" runat="server" Style="display: none" />
            <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="lnkChng"
                PopupControlID="panEdit" BackgroundCssClass="modalBackground" CancelControlID="btnCancel1">
            </asp:ModalPopupExtender>
            <asp:Panel ID="panEdit" runat="server" Height="300px" Width="1100px" CssClass="ModalWindow">
                <div style="width: 100%; height: 300px; overflow: scroll;">
                    <table>
                        <tr>
                            <td>
                                <b>Change History</b>
                            </td>
                        </tr>
                    </table>
                    <uc1:TimeTrackerAudit ID="TimeTrackerAudit1" runat="server" />
                </div>
                <br />
                <asp:Button ID="btnCancel1" runat="server" Text="Close" CausesValidation="False" />
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
