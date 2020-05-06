<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/PMOMaster.master"
    CodeBehind="Project.aspx.cs" Inherits="PMOscar.Project" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="ProjectAudituserctrl.ascx" TagName="ProjectAudituserctrl" TagPrefix="uc2" %>
<%@ Register Src="ProjectEstimationAuditusrctrl.ascx" TagName="ProjectEstimationAuditusrctrl"
    TagPrefix="uc1" %>
<%@ Register Src="ActualHoursuctrl.ascx" TagName="ActualHoursuctrl" TagPrefix="uc3" %>
<%@ Register Src="NotAdjustedActualHours.ascx" TagName="NotAdjustedActualHours" TagPrefix="uc4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cntBody" runat="server">
    <br />
    <script type="text/javascript">

        function CheckTextValue(e, myfield) {

            var key;
            key = e.which ? e.which : e.keyCode;
            keychar = String.fromCharCode(key);


            if ((key == 9) || (key == 8) || (key == 32) || (key == 13))
                return true;

            else if ((("abcdefghijklmnopqrstuvwxyz").indexOf(keychar) > -1))
                return true;


            // numbers
            else if ((("ABCDEFGHIJKLMNOPQRSTUVWXYZ").indexOf(keychar) > -1))
                return true;

            else if ((("0123456789").indexOf(keychar) > -1))
                return true;

            else if (((".';:!@#$%&*^(){}|/-_=+~[]<>?,\"\\").indexOf(keychar) > -1))
                return true;

            else {
                return false;
            }
        }
    </script>

    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js"></script>
    <script src="https://qa-apps-angularjs.naicotech.com:4457/HTML/intranet/angular8-common-app.js"></script>
    <link rel="stylesheet" href="https://qa-apps-angularjs.naicotech.com:4457/HTML/intranet/angular8-common-app.css">
    <script>
        $(document).ready(function () {

           
            $("#test").click(function () { 
                $("#ctl00_cntBody_ClProjectStart,#ctl00_cntBody_ClDelivery,#ctl00_cntBody_ClRevisedDelivery,#ctl00_cntBody_ClMaintClosing").css("display", "none");
            });
            
           
          
        });

        
    </script>
    <script type="text/javascript" id=" ">

        style = " height: 20px; width: 220px;"
        function CheckNemericValue(e, myfield) {
            var key;
            key = e.which ? e.which : e.keyCode;
            keychar = String.fromCharCode(key);

            if ((keychar == ".")) {
                return false;
            }
            if ((key == null) || (key == 0) || (key == 8) || (key == 9) || (key == 13) || (key == 27) || (key == 46) || (key == 37) ||
            (key == 38) || (key == 39) || (key == 40)) {
                return true;
            }
            else if ((("0123456789").indexOf(keychar) > -1)) {// Numbers
                return true;
            }
            else {
                return false;
            }
        }

        function SetrReload() {

            alert(document.getElementById("ctl00_cntBody_testreload"));
            document.getElementById("ctl00_cntBody_testreload").value = "1";
            return true;
        }
        function CheckValid() {


            var isValid = true;
            var txtBillableCode = document.getElementById("<%=txtBillableHrs.ClientID %>").value;
            var txtBudHoursCode = document.getElementById("<%=txtBudHours.ClientID %>").value;
            var txtRevBudHoursCode = document.getElementById("<%=txtRevBudHours.ClientID %>").value;
            var txtCommentsCode = document.getElementById("<%=txtComments.ClientID %>").value;

            if (isNaN(txtBillableCode)) {


                document.getElementById("spTxtBillableHrs").innerHTML = "Enter in Numbers";

                isValid = false
            }
            else {
                document.getElementById("spTxtBillableHrs").innerHTML = "";

            }

            if (isNaN(txtBudHoursCode)) {

                document.getElementById("spTxtBudHours").innerHTML = "Enter in Numbers";
                isValid = false;

            }
            else {
                document.getElementById("spTxtBudHours").innerHTML = "";
            }
            if (isNaN(txtRevBudHoursCode)) {

                document.getElementById("spTxtRevBudHours").innerHTML = "Enter in Numbers";
                isValid = false;
            }
            else {
                document.getElementById("spTxtRevBudHours").innerHTML = "";
            }
            return isValid;
        }


        function ClearCloneProjName() {
            //document.getElementById("<%=txtCloneProjectName.ClientID %>").focus();
            var objProjectname = document.getElementById("<%=txtCloneProjectName.ClientID %>");
            var objProjectShortName = document.getElementById("<%=txtCloneProjectShortName.ClientID%>");
            objProjectname.value = "";
            objProjectShortName.value = "";
            objProjectname.focus;
            //document.forms[0].CloneProjectName.focus();

            return false;
        }
        function ValidateCloneProjName() {
            var objProjectName = document.getElementById("<%=txtCloneProjectName.ClientID%>");
            var objProjectShortName = document.getElementById("<%=txtCloneProjectShortName.ClientID%>");
            if (objProjectName.value == null || objProjectName.value == "") {
                alert("Please enter project name");
                return false;
            }
            else if (objProjectShortName.value == null || objProjectShortName.value == "") {
                alert("Please enter project short name");
                return false;
            }
            else {
                return true;
            }
            /*else {
                
            return  PageMethods.CheckProjectDuplication(objProjectName.value, objProjectShortName.value, OnSuccess, OnFailure);
            //alert(strStatus.toString());
            if (status == "1") 
            {
            // alert("Project name already exists!");
            return false;
            }
            else if (status == "2") 
            {
            alert("Project short name already exists!");
            return false;
            }
            //                       else 
            //                       {
            //                           return true;
            //                       }
            alert(result);
            return false; 
                
            }*/
        }

        /*
        // get the XMLHttpRequestObject and store it in a variable
        var ajaxObj = getXMLHttpRequestObject();            
        
        if(ajaxObj)
        {
        // continue if the object is idle
        if (ajaxObj.readyState == 4 || ajaxObj.readyState == 0)
        {
        // build the �GET� request URL
        var URL = "Project.aspx" + "?name=" + objProjectName.value + "&sname=" + objProjectShortName.value;

        // open connection and send �GET� request to server
        ajaxObj.open("GET", URL, true);

        // set the function to be called on a change in ajaxObj state
        ajaxObj.onreadystatechange = handleResponse;

        // set additional parameters (to be sent to server) to null
        ajaxObj.send(null);
        alert(ajaxObj.readyState);
        // check if the request is complete
        if (ajaxObj.readyState == 4) {
        // continue if the response is healthy
        if (ajaxObj.status == 200) {
        // store the server�s text response
        var textResponse = ajaxObj.responseText;

        if (textResponse == "1") {
        alert("Project name already exists!");
        return false;
        }
        else if (textResponse == "2") {
        alert("Project short name already exists!");
        return false;
        }
        else {
        return true;
        }
        }
        } 
                    
        }
        }
        //true;
           
           
        }
        
        }*/

        function OnSuccess(result) {
            /* var retVal = false;
            if (result) 
            {           
            if (result == "1") 
            {
            alert("Project name already exists!");                        
            return false;
            //window.event.returnValue = false;
            }
            else if (result == "2") 
            {
            alert("Project short name already exists!");                        
            //return false;
            window.event.returnValue = false;               
            }
            else 
            {
            return true;
            }
            }
            */
            //status = result;

            return false;
        }

        function OnFailure(error) {
            return false;
        }

        function ShowPopup() {
            var objModelPopup = document.getElementById("mpCloneProject");
            objModelPopup.show();
        }


        /*
        function getXMLHttpRequestObject()
        {
        // initially set the object to false

        var XMLHttpRequestObject = false;

        if (window.XMLHttpRequest)
        {
        // check for Safari, Mozilla, Opera�
        XMLHttpRequestObject = new XMLHttpRequest();
        }
        else if (window.ActiveXObject)
        {
        // check for Internet Explorer
        XMLHttpRequestObject = new ActiveXObject("Microsoft.XMLHTTP");
        }

        if (!XMLHttpRequestObject)
        {
        alert('Your browser does not support Ajax.');
        // return false in case of failure
        return false;
        }
        // return the object in case of success
        return XMLHttpRequestObject;

        }
        */


        // function called when there is a change ajaxObj state
        /*
        function handleResponse()
        {
        // check if the request is complete
        if (ajaxObj.readyState == 4) {
        // continue if the response is healthy
        if (ajaxObj.status == 200) {
        // store the server�s text response
        var textResponse = ajaxObj.responseText;

        if (textResponse == "1") {
        alert("Project name already exists!");
        return false;
        }
        else if (textResponse == "2") {
        alert("Project short name already exists!");
        return false;
        }
        else {
        return true;
        }
        }
        }       
        }
        */

    </script>
    <script type="text/javascript" language="javascript">
        function showCommentPopup(comment, e) {
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
                //comment = comment + words[i].replace('"', "'") + ' ';
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
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true">
    </asp:ScriptManager>
    <input runat="server" id="testreload" type="hidden" value="" />
    <input id="hdnProject" type="hidden" value="" />
    <div style="width: 100%;" id="test">
        
         <style type="text/css" media="screen">
   
                BODY {  width:100%}
     </style>
        <div style="width: 95%; text-align: right">
        </div>
        <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>
        <table width="100%">
            <tr>
                <td align="left" style="border-bottom: 1px solid #ccc;">
                    <b>
                        <asp:Label ID="lblProjectStatus" runat="server" Text="Create Project" Width="150px"></asp:Label></b>
                    &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                    &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                    &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                    &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                    &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                    &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                    &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                    &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                    &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                    <asp:LinkButton ID="lbBack" Text="Go to Resource Planning" runat="server" OnClick="lbBack_Click"
                        CausesValidation="false" TabIndex="-1"></asp:LinkButton>&nbsp
                    <asp:Button ID="btnEdit" runat="server" Text="Edit" OnClick="btnEdit_Click" CausesValidation="False"
                        Height="22px" Width="38px" />
                    <asp:Button ID="btnClone" runat="server" Text="Clone" CausesValidation="False" OnClientClick="ClearCloneProjName();"
                        OnClick="btnClone_Click" Height="22px" Width="38px" />
                    <asp:ModalPopupExtender ID="mdlClonePopup" BehaviorID="mpCloneProject" runat="server"
                        TargetControlID="btnClone" PopupControlID="pnlCloneProject" CancelControlID="btnCloneCancel"
                        BackgroundCssClass="modalBackground" />
                </td>
            </tr>
            <tr>
                <td align="left" style="border-bottom: 1px solid #ccc; height: 19px;">
                    &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                    &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                    &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                    &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                    &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                    &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                    &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                    &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                    &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                    &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                    &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp<asp:LinkButton ID="lnkChangeHistory" Text="Change History"
                        runat="server" CausesValidation="false" TabIndex="-1" OnClick="LinkButton2_Click"></asp:LinkButton>&nbsp&nbsp&nbsp&nbsp&nbsp
                        <a  href="<%=ConfigurationManager.AppSettings["PMOscarV2_Url"] %>Account/LoginSession?Email=<%=Session["UserName"]%>&Password=<%=Session["EncryptedPassword"]%>&returnUrl=/WorkOrder/WorkOrderCustomView/<%=Session["WorkOrderID"]%>"
                       class="" tabindex="-1">View Work Order</a>&nbsp&nbsp&nbsp&nbsp&nbsp
                        <%-- <a  href="<%=ConfigurationManager.AppSettings["PMOscarV2_Url"] %>Account/LoginSession?Email=<%=Session["UserName"]%>&Password=<%=Session["EncryptedPassword"]%>&returnUrl=/listbudgetrevision/<%=Session["ProjectEditId"]%>"
                       class="" tabindex="-1">Budget Revisions</a>&nbsp&nbsp&nbsp&nbsp&nbsp--%>
                    <asp:LinkButton ID="lnkResourceHistory" Text="Allocation Change History"
                        runat="server" CausesValidation="false" TabIndex="-1" OnClick="ResourceHistory"></asp:LinkButton>
                    <asp:Button ID="resourceAudit" runat="server" Style="display: none" />

                       <%--  <asp:LinkButton ID="lnkViewWorkOrder" Text="View Work Order" runat="server" CausesValidation="false"                         
                        TabIndex="-1" OnClick="LinkButtonToViewWorkorder_Click"></asp:LinkButton>--%>
                </td>
            </tr>
        </table>
        <%--</ContentTemplate>
        </asp:UpdatePanel>--%>
        <asp:Panel ID="PnlEdit" runat="server">
            <table class="style1" cellpadding="2" cellspacing="2">
                <tr>
                    <td class="style2" align="left" style="height: 10px">
                        Project Name
                    </td>
                    <td style="height: 10px; width: 37px;" align="left">
                        <asp:TextBox ID="txtProjectName" runat="server" Width="191px" TabIndex="1" OnTextChanged="txtProjectName_TextChanged" ></asp:TextBox>
                    </td>
                    <td style="width: 68px" align="left">
                        <asp:RequiredFieldValidator ID="ProjectNameFieldValidator" runat="server" ErrorMessage="Enter Project Name."
                            ControlToValidate="txtProjectName" SetFocusOnError="true"></asp:RequiredFieldValidator>
                        <asp:Label ID="lblPctName" runat="server" Text="Project Name already exist." ForeColor="Red"
                            Visible="False"></asp:Label>
                    </td>
                    <td style="width: 121px" align="left">
                        Project Code
                    </td>
                    <td style="width: 121px" align="left">
                        <asp:TextBox ID="txtProjectCode" runat="server" Width="191px" TabIndex="2" OnTextChanged="txtProjectCode_TextChanged"></asp:TextBox>
                    </td>
                    <td style="width: 121px" align="left">
                        <asp:RequiredFieldValidator ID="RqProjectCode0" ControlToValidate="txtProjectCode"
                            InitialValue="0" runat="server" ErrorMessage="Select Project Code." SetFocusOnError="true"></asp:RequiredFieldValidator>
                        <asp:Label ID="lblPjctCode" runat="server" Text="Project Code already exist." ForeColor="Red"
                            Visible="False"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="style2" align="left">
                        Project Short Name
                    </td>
                    <td align="left" style="width: 37px">
                        <asp:TextBox ID="txtProjectShortName" runat="server" Width="191px" TabIndex="3" OnTextChanged="txtProjectShortName_TextChanged"></asp:TextBox>
                    </td>
                    <td style="width: 68px">
                        <asp:Label ID="lblSName" runat="server" Text="short Name already exist." ForeColor="Red"
                            Visible="False"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:Label ID="lblCurrentPhase" runat="server" Text="Current Phase"></asp:Label>
                    </td>
                    <td align="left" style="width: 37px;">
                        <asp:DropDownList ID="ddlCurrentPhase" runat="server" TabIndex="7" Width="200px">
                        </asp:DropDownList>
                    </td>
                    <td style="width: 68px;" align="left">
                        <asp:RequiredFieldValidator runat="server" ErrorMessage="Select Phase." ID="phaseValidationId" ControlToValidate="ddlCurrentPhase" InitialValue="0" SetFocusOnError="true"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td style="width: 121px" align="left">
                        <asp:Label ID="lblProjectType" runat="server" Text="Project Type"></asp:Label>
                    </td>
                    <td style="width: 121px" align="left">
                        <asp:DropDownList ID="ddlProjectType" runat="server" Width="200px" TabIndex="4">
                            <asp:ListItem Value="0">Select</asp:ListItem>
                            <asp:ListItem Value="F">Fixed Cost</asp:ListItem>
                            <asp:ListItem Value="T">T &amp; M</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td style="width: 121px" align="left">
                        <asp:RequiredFieldValidator ID="rfvProjectType" ControlToValidate="ddlProjectType"
                            InitialValue="0" runat="server" ErrorMessage="Select Project Type." SetFocusOnError="true"></asp:RequiredFieldValidator>
                    </td>
                    <td align="left">
                        <asp:Label ID="Label8" runat="server" Text="Category"></asp:Label>
                    </td>
                    <td align="left" style="width: 37px;">
                        <asp:DropDownList ID="ddlUtilization" runat="server" Width="200px" TabIndex="11">
                            <asp:ListItem Value="0">Select</asp:ListItem>
                            <asp:ListItem Value="C">Commercial</asp:ListItem>
                            <asp:ListItem Value="S">Semi Commercial</asp:ListItem>
                            <asp:ListItem Value="I">Internal</asp:ListItem>
                            <asp:ListItem Value="G">GIS</asp:ListItem>
                            <asp:ListItem Value="P">Product</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td style="width: 121px;" align="left">
                    </td>
                </tr>
                <tr>
                    <td align="left" style="height: 20px">
                        <asp:Label ID="lblProjectOwner" runat="server" Text="Project Owner"></asp:Label>
                    </td>
                    <td align="left" style="height: 20px; width: 37px;">
                        <asp:DropDownList ID="ddlProjectOwner" runat="server" DataTextField="FirstName" DataValueField="UserId"
                            Width="200px" TabIndex="5">
                        </asp:DropDownList>
                    </td>
                    <td style="width: 121px; height: 20px;" align="left" nowrap="nowrap">
                        <asp:RequiredFieldValidator ID="rfvProjectOwner" ControlToValidate="ddlProjectOwner"
                            InitialValue="0" runat="server" ErrorMessage="Select Project Owner." SetFocusOnError="true"></asp:RequiredFieldValidator>
                    </td>
                    <td style="width: 121px; height: 20px;" align="left">
                        <asp:Label ID="lblProjectManager" runat="server" Text="Project Manager"></asp:Label>
                    </td>
                    <td style="width: 121px; height: 20px;" align="left">
                        <asp:DropDownList ID="ddlProjectManager" runat="server" Width="200px" DataTextField="FirstName"
                            DataValueField="UserId" TabIndex="6">
                        </asp:DropDownList>
                    </td>
                    <td style="width: 121px; height: 20px;" align="left" nowrap="nowrap">
                        <asp:RequiredFieldValidator ID="rfvProjectManager" ControlToValidate="ddlProjectManager"
                            InitialValue="0" runat="server" ErrorMessage="Select Project Manager." SetFocusOnError="true"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td align="left" style="height: 20px">
                        <asp:Label ID="Label6" runat="server" Text="Client"></asp:Label>
                    </td>
                    <td align="left" style="height: 20px; width: 37px;">
                        <asp:DropDownList ID="ddlClient" runat="server" DataTextField="ClientName" DataValueField="ClientId"
                            Width="200px" TabIndex="10" autopostback="true" onselectedindexchanged="ddlClient_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                     <td style="width: 121px; height: 20px;" align="left" nowrap="nowrap">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ControlToValidate="ddlClient" InitialValue="0"
                            runat="server" ErrorMessage="Select Client." SetFocusOnError="true"></asp:RequiredFieldValidator>
                    </td>
                    <td style="width: 121px; height: 20px;" align="left">
                        <asp:Label ID="Label7" runat="server" Text="Program"></asp:Label>
                    </td>
                    <td style="width: 121px; height: 20px;" align="left">
                        <asp:DropDownList ID="ddlProgram" runat="server" Width="200px" DataTextField="ProgName"
                            DataValueField="ProgId" TabIndex="11" AutoPostBack="true" OnSelectedIndexChanged="ddlProgram_SelectedIndexChanged" >
                        </asp:DropDownList>
                    </td>
                    <td style="width: 121px; height: 20px;" align="left" nowrap="nowrap">
                        <asp:RequiredFieldValidator ID="rfvProgram" ControlToValidate="ddlProgram" InitialValue="0"
                            runat="server" ErrorMessage="Select Program." SetFocusOnError="true"></asp:RequiredFieldValidator>
                    </td>
                  
                </tr>
                <tr>
                    <td align="left" style="height: 20px">
                        <asp:Label ID="LabelCostCentre" runat="server" Text="Cost Centre"></asp:Label>
                    </td>
                    <td align="left" style="height: 20px; width: 37px;">
                        <asp:DropDownList ID="ddlcostcentre" runat="server" DataTextField="CostCentre" DataValueField="CostCentreID"
                            Width="200px" TabIndex="10">
                        </asp:DropDownList>
                    </td>
                    <td style="width: 121px; height: 20px;" align="left" nowrap="nowrap">
                        <asp:RequiredFieldValidator ID="CostCentreValidator" ControlToValidate="ddlCostCentre"
                            InitialValue="0" runat="server" ErrorMessage="Select Cost Centre." SetFocusOnError="true"></asp:RequiredFieldValidator>
                    </td>
                    <td style="width: 121px; height: 20px;" align="left">
                        Status &nbsp;
                    </td>
                    <td style="width: 121px;" align="left">
                        <table style="margin-top: 15px" cellpadding="0" cellspacing="0">
                            <tr>
                                <td>
                                    <asp:RadioButton ID="optActive" runat="server" Checked="True" GroupName="Status"
                                        TabIndex="8" Text="Active" />
                                </td>
                                <td width="25px">
                                </td>
                                <td>
                                    <asp:RadioButton ID="OptInactive" runat="server" GroupName="Status" TabIndex="9"
                                        Text="Inactive (Hold)" />
                                </td>
                            </tr>
                        </table>
                        &nbsp;
                    </td>
                    <td style="width: 121px;" align="left" nowrap="nowrap">
                    </td>
                    <td style="width: 121px;" align="left">
                    </td>
                    <td style="width: 121px;" align="left">
                    </td>
                    <td style="width: 121px;" align="left" nowrap="nowrap">
                    </td>
                </tr>
                <tr>
                    <td align="left" style="height: 20px">
                        <asp:Label ID="Label12" runat="server" Text="Work Order Name"></asp:Label>
                    </td>
                    <td align="left" style="height: 20px; width: 37px;">
                        <asp:DropDownList ID="workOrderList" runat="server" DataTextField="WorkOrderName"
                            DataValueField="WorkOrderID" Width="200px" TabIndex="12">
                        </asp:DropDownList>
                    </td>
                    <td align="left" style="height: 20px">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Select Work Order." ControlToValidate="workOrderList" InitialValue="0" SetFocusOnError="true"></asp:RequiredFieldValidator>
                    </td>
                    <td style="height: 20px">Add Resources</td>
                    <td style="height: 20px">
                    <asp:ListBox ID="lstResource" runat="server" Height="120px" SelectionMode="Multiple" Width="200px" TabIndex="11" ></asp:ListBox>  
                    <app-check-list></app-check-list>   
                </td>
                 
                    <td align="left" style="height: 20px; width: 37px;" colspan="2"></td>
                </tr>
                <tr>
                    <td  align="left" style="height: 20px">
                        <asp:Label ID="WorkLabel" runat="server" Text="Bugzilla Project"></asp:Label>

                    </td>
                   <td align="left" style="height: 20px; width: 37px;">
                        <asp:DropDownList ID="BugzillaList" runat="server" DataTextField="name"
                            DataValueField="id" Width="200px" TabIndex="12">
                        </asp:DropDownList>
                    </td>
                   <%-- <td align="left" style="height: 20px">
                        <asp:RequiredFieldValidator ID="bugzillaidRequired" runat="server" ErrorMessage="Select Bugzilla Project." ControlToValidate="BugzillaList" InitialValue="0" SetFocusOnError="true"></asp:RequiredFieldValidator>
                    </td>--%>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:CheckBox ID="chkShowinDashboard" runat="server" Text="Show in Dashboard" Checked="true" />
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td align="left" colspan="6" style="border-bottom: 1px solid #ccc; height: 21px;">
                        &nbsp;
                       
                        <asp:Label ID="lblDateError" runat="server" ForeColor="Red"></asp:Label>
                       
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <asp:Label ID="lblDeliveryDate0" runat="server" Text="Project Start Date"></asp:Label>
                    </td>
                    <td align="left" style="width: 37px">
                        <table>
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtProjStartdate" runat="server" TabIndex="11"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Button ID="btnProjStart" runat="server" CausesValidation="False" OnClick="btnProjStart_Click"
                                        TabIndex="12" Text="..." />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    (dd/mm/yyyy)
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td align="left">
                        <asp:RequiredFieldValidator ID="projectStartDateValidate" runat="server" ErrorMessage="Select project Start Date." ControlToValidate="txtProjStartdate" SetFocusOnError="true"></asp:RequiredFieldValidator>
                    </td>
                    <td align="left">
                        Delivery Date
                    </td>
                    <td align="left" colspan="2">
                        <table>
                            <tr>
                                <td align="left">
                                    <asp:TextBox ID="txtDelCal" runat="server" TabIndex="13"></asp:TextBox>
                                </td>
                                <td align="left">
                                    <asp:Button ID="bthDelVisible" runat="server" CausesValidation="False" OnClick="Button1_Click"
                                        TabIndex="13" Text="..." />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    (dd/mm/yyyy)
                                </td>
                            </tr>
                        </table>
                        <asp:Label ID="lblDateError2" runat="server" ForeColor="Red"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        &nbsp;
                    </td>
                    <td align="left" style="width: 37px">
                        <asp:Calendar ID="ClProjectStart" runat="server" OnSelectionChanged="ClProjectStart_SelectionChanged"
                            TabIndex="14" Visible="False" OnVisibleMonthChanged="ClProjectStart_VisibleMonthChanged"></asp:Calendar>
                    </td>
                    <td align="left">
                        &nbsp;
                    </td>
                    <td align="left">
                        &nbsp;
                    </td>
                    <td align="left">
                        <asp:Calendar ID="ClDelivery" runat="server" OnSelectionChanged="ClDelivery_SelectionChanged"
                            SelectedDate="2011-03-08" TabIndex="15" Visible="False" OnVisibleMonthChanged="ClDelivery_VisibleMonthChanged"></asp:Calendar>
                    </td>
                    <td align="left">
                        &nbsp;
                        <br />
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        Revised Delivery Date
                    </td>
                    <td align="left" style="width: 37px">
                        <table>
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtRDel" runat="server" TabIndex="15"></asp:TextBox>
                                </td>
                                <td align="left">
                                    <asp:Button ID="btnRDel" runat="server" CausesValidation="False" OnClick="btnRDel_Click"
                                        TabIndex="15" Text="..." Height="22px" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    (dd/mm/yyyy)
                                </td>
                            </tr>
                        </table>
                        <asp:Label ID="lblDateError4" runat="server" ForeColor="red"></asp:Label>
                    </td>
                    <td align="left">
                        &nbsp;
                    </td>
                    <td align="left">
                        Maintenance Closing Date
                    </td>
                    <td align="left" colspan="1">
                        <table>
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtMaintClosingDate" runat="server" TabIndex="17"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Button ID="btnManiteClosing" runat="server" CausesValidation="False" OnClick="btnManiteClosing_Click"
                                        TabIndex="17" Text="..." />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    (dd/mm/yyyy)
                                </td>
                            </tr>             
                        </table>
                        <asp:Label ID="lblDateError3" runat="server" ForeColor="red"></asp:Label>
                        </td>
                        <td align="left">
                            <asp:RequiredFieldValidator ID="MaintClosingDateValidate" runat="server" ErrorMessage="Select Maintenance Closing Date." ControlToValidate="txtMaintClosingDate" SetFocusOnError="true"></asp:RequiredFieldValidator>
                        </td>                    
                </tr>
                <tr>
                    <td>
                    </td>
                    <td align="left" style="width: 37px">
                        <asp:Calendar ID="ClRevisedDelivery" runat="server" OnSelectionChanged="ClRevisedDelivery_SelectionChanged"
                            SelectedDate="2011-03-08" TabIndex="18" Visible="False" OnVisibleMonthChanged="ClRevisedDelivery_VisibleMonthChanged"></asp:Calendar>
                    </td>
                    <td style="width: 68px">
                    </td>
                    <td>
                    </td>
                    <td>
                        <asp:Calendar ID="ClMaintClosing" runat="server" OnSelectionChanged="ClMaintClosing_SelectionChanged"
                            SelectedDate="2011-03-08" TabIndex="19" Visible="False" OnVisibleMonthChanged="ClMaintClosing_VisibleMonthChanged"></asp:Calendar>
                    </td>
                    <td align="left">
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <asp:Label ID="Label3" runat="server" Text="PM Comments"></asp:Label>
                    </td>
                    <td colspan="2" align="left">
                        <asp:TextBox ID="txtPMComments" runat="server" TextMode="MultiLine" Width="341px"
                            TabIndex="20" Height="86px"></asp:TextBox>
                    </td>
                    <td style="width: 121px" align="left">
                        <asp:Label ID="Label4" runat="server" Text="Delivery  Comments"></asp:Label>
                    </td>
                    <td align="left" colspan="2">
                        <asp:TextBox ID="txtDeliveryCommments" runat="server" TabIndex="21" TextMode="MultiLine"
                            Width="341px" Height="86px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="left" style="height: 36px">
                        <asp:Label ID="Label5" runat="server" Text="PO Comments"></asp:Label>
                    </td>
                    <td colspan="2" align="left" style="height: 36px">
                        <asp:TextBox ID="txtPOComments" runat="server" TextMode="MultiLine" Width="341px"
                            TabIndex="22" Height="86px"></asp:TextBox>
                    </td>
                    <td style="width: 121px; height: 36px;" align="left">
                        Project Details
                    </td>
                    <td align="left" colspan="2" style="height: 36px">
                        <asp:TextBox ID="txtProjectDetails" runat="server" TextMode="MultiLine" Width="341px"
                            TabIndex="22" Height="86px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="left" colspan="6" style="border-bottom: 1px solid #ccc;">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td colspan="6" align="left">
                        <b>Project Environments</b>
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        Dev URL
                    </td>
                    <td align="left" colspan="2">
                        <asp:TextBox ID="txtdevURL" runat="server" OnTextChanged="txtProjectName_TextChanged"
                            TabIndex="23" Width="341px" TextMode="MultiLine"></asp:TextBox>
                    </td>
                    <td align="left">
                        &nbsp;&nbsp;&nbsp;&nbsp; QA URL
                    </td>
                    <td align="left" colspan="2">
                        <asp:TextBox ID="txtQAUrl" runat="server" OnTextChanged="txtProjectName_TextChanged"
                            TabIndex="24" Width="341px" TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        Demo URL
                    </td>
                    <td align="left" colspan="2">
                        <asp:TextBox ID="txtDemoUrl" runat="server" OnTextChanged="txtProjectName_TextChanged"
                            TabIndex="25" Width="341px" TextMode="MultiLine"></asp:TextBox>
                    </td>
                    <td align="left">
                        &nbsp;&nbsp;&nbsp;&nbsp; Production URL
                    </td>
                    <td align="left" colspan="2">
                        <asp:TextBox ID="txtProductionurl" runat="server" OnTextChanged="txtProjectName_TextChanged"
                            TabIndex="26" Width="341px" TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        SVN Path
                    </td>
                    <td align="left" colspan="2">
                        <asp:TextBox ID="txtSVNUrl" runat="server" TabIndex="25" Width="341px" TextMode="MultiLine"></asp:TextBox>
                    </td>
                    <td align="left">
                    </td>
                    <td align="left" colspan="2">
                    </td>
                </tr>
                <tr>
                    <td align="left" colspan="2">
                        <asp:Label ID="lbErorMsg" runat="server" ForeColor="Red"></asp:Label>
                        &nbsp;
                    </td>
                    <td align="left" style="width: 68px">
                        &nbsp;
                    </td>
                    <td align="left" style="width: 121px">
                        &nbsp;
                    </td>
                    <td align="right" colspan="2">
                        <asp:Button ID="Button1" runat="server" OnClick="btnSave_Click" TabIndex="27" Height="22px"
                            Text="Save" Width="60px" />
                        &nbsp;&nbsp;
                        <asp:Button ID="Button2" runat="server" CausesValidation="false" Height="22px" OnClick="btnCancel_Click"
                            TabIndex="28" Text="Cancel" Width="60px" />
                        &nbsp;
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel ID="pnlEst" runat="server">
            <table class="style1" cellpadding="2" cellspacing="2">
                <tr>
                    <td align="left" colspan="6" style="border-bottom: 1px solid #ccc;">
                        <asp:Label ID="Label1" runat="server" Font-Bold="True" Text="Estimation"></asp:Label>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td align="left" style="width: 115px">
                        <asp:Label ID="lblPhase" runat="server" Text="Phase"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="ddlPhase" runat="server" Width="200px" TabIndex="29">
                        </asp:DropDownList>
                    </td>
                    <td style="width: 72px" align="left">
                        <asp:Label ID="lblValPhase" runat="server" ForeColor="Red" Text="Select Phase."></asp:Label>
                    </td>
                    <td style="width: 121px" align="left">
                        <asp:Label ID="lblPhase0" runat="server" Text="Role"></asp:Label>
                    </td>
                    <td style="width: 121px" align="left">
                        <asp:DropDownList ID="ddlRole" runat="server" Width="200px" TabIndex="30">
                        </asp:DropDownList>
                    </td>
                    <td style="width: 121px" align="left">
                        <asp:Label ID="lblValRole" runat="server" ForeColor="Red" Text="Select Role."></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="left" style="width: 115px">
                        <asp:Label ID="Label2" runat="server" Text="Billable Hrs"></asp:Label>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtBillableHrs" runat="server" Width="191px" TabIndex="31" MaxLength="9"></asp:TextBox>
                    </td>
                    <td style="width: 121px" align="left">
                        <span id="spTxtBillableHrs" class="fontColor"></span>
                    </td>
                    <td style="width: 121px" align="left">
                        Budgeted Hours
                    </td>
                    <td style="width: 121px" align="left">
                        <asp:TextBox ID="txtBudHours" runat="server" Width="191px" TabIndex="32" MaxLength="9"></asp:TextBox>
                    </td>
                    <td style="width: 121px" align="left">
                        <span id="spTxtBudHours" class="fontColor"></span>
                    </td>
                </tr>
                <tr>
                    <td align="left" style="width: 115px">
                        Revised Budgeted Hours
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtRevBudHours" runat="server" Width="191px" TabIndex="33" MaxLength="9"></asp:TextBox>
                    </td>
                    <td style="width: 121px" align="left">
                        <span id="spTxtRevBudHours" class="fontColor"></span>
                    </td>
                    <td style="width: 121px" align="left">
                        Comments
                    </td>
                    <td style="width: 121px" align="left">
                        <asp:TextBox ID="txtComments" runat="server" Width="191px" TabIndex="34"></asp:TextBox>
                    </td>
                    <td style="width: 140px" align="left" runat="server">
                        <asp:Label ID="lblComments" runat="server" ForeColor="Red" Text="Please Enter a Comment."></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="left" style="width: 115px">
                        &nbsp;
                    </td>
                    <td align="left" style="width: 191px">
                        &nbsp;
                    </td>
                    <td style="width: 121px" align="left">
                    </td>
                    <td style="width: 121px" align="left">
                    </td>
                    <td style="width: 121px" align="right" nowrap="nowrap">
                        <asp:Button ID="btnEstSave" runat="server" Text="Save" Width="60px" Height="22px"
                            OnClientClick="javascript:return CheckValid();" OnClick="btnAdd_Click" TabIndex="34" />
                        &nbsp;&nbsp;
                        <asp:Button ID="btnEstCancel" runat="server" Width="60px" Text="Clear" CausesValidation="false"
                            OnClick="btnEstCancel_Click" TabIndex="35" Height="22px" />
                    </td>
                    <td style="width: 121px">
                        <asp:TextBox ID="txtEstID" runat="server" TabIndex="-1" Visible="False" Width="93px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="6">
                        <asp:Label runat="server" ID="lblesterror" ForeColor="#FF3300"></asp:Label>
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <table width="100%">
            <tr>
                <td align="left" style="width: 115px">
                    &nbsp;
                    <asp:HiddenField ID="HFPjctId" runat="server" />
                </td>
                <td align="left" style="width: 200px">
                    &nbsp;
                </td>
                <td style="width: 121px" align="left">
                    &nbsp;
                </td>
                <td style="width: 121px" align="left">
                    <asp:LinkButton ID="LinkButton1" Text="Change History" runat="server" CausesValidation="false"
                        TabIndex="36" OnClick="LinkButton1_Click"></asp:LinkButton>
                    &nbsp;
                </td>
            </tr>
        </table>
        <asp:Panel ID="pnlEst2" runat="server">
            <table width="100%">
                <tr>
                    <td align="left" style="width: 115px">
                        &nbsp;</td>
                    <td align="left" style="width: 100px">
                        &nbsp;
                    </td>
                    <td style="width: 121px" align="left">
                        &nbsp;
                    </td>
                    <td style="width: 121px" align="left">
                        <asp:CheckBox ID="chkValue" runat="server" Text="Include Proposal &amp; VAS" AutoPostBack="true"
                            OnCheckedChanged="chkValue_CheckedChanged" />
                        &nbsp;
                    </td>
                </tr>
            </table>
            <table>
                <tr>
                    <td align="left" colspan="3" style="height: 143px">
                        <asp:GridView ID="grdEstimation" runat="server" AutoGenerateColumns="False" Width="100%"
                            CellPadding="4" AllowSorting="True" ForeColor="#333333" TabIndex="-1" OnRowDataBound="grdEstimation_RowDataBound"
                            ShowFooter="True" OnRowCreated="grdEstimation_RowCreated" OnSorting="grdEstimation_Sorting" >
                            <RowStyle BackColor="#EFF3FB" />
                            <Columns>
                                <asp:BoundField HeaderText="#" />
                                <asp:BoundField HeaderText="ProjectEstimationID" DataField="ProjectEstimationID" />
                                <asp:BoundField HeaderText="Phase" DataField="Phase" SortExpression="Phase">
                                    <HeaderStyle ForeColor="#FCFF00" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Role" DataField="Role" SortExpression="Role">
                                    <HeaderStyle ForeColor="#FCFF00" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Billable Hrs" DataField="BillableHours" SortExpression="BillableHours">
                                    <HeaderStyle ForeColor="#FCFF00" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Budget Hrs" DataField="BudgetHours" SortExpression="BudgetHours">
                                    <HeaderStyle ForeColor="#FCFF00" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Revised Budg. Hrs" DataField="RevisedBudgetHours" SortExpression="RevisedBudgetHours">
                                    <HeaderStyle ForeColor="#FCFF00" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Revised Budg. Hrs" DataField="ProjectID" SortExpression="RevisedBudgetHours">
                                    <HeaderStyle ForeColor="#FCFF00" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Revised Budg. Hrs" DataField="PhaseID" SortExpression="RevisedBudgetHours">
                                    <HeaderStyle ForeColor="#FCFF00" />
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Revised Budg. Hrs" DataField="RoleID" SortExpression="RevisedBudgetHours">
                                    <HeaderStyle ForeColor="#FCFF00" />
                                </asp:BoundField>                                                             
                                <asp:TemplateField HeaderText="Actual Hrs (Adjusted)"  SortExpression="ActualHours" >
                                    <HeaderStyle ForeColor="#FCFF00" />                                   
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkActualHours" runat="server"  HeaderText="Actual Hrs (Adjusted)"
                                            Text='<%# Eval("ActualHours", "{0:#0.00}") %>' CausesValidation="false" OnClick="BtnViewDetails_Click"/>
                                        <asp:HiddenField ID="hdnPhase" Value='<%# Bind("ActualHours") %>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Actual Hours" SortExpression="ActualHours1">
                                    <HeaderStyle ForeColor="#FCFF00" />
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lnkActualHours1" runat="server" HeaderText="Actual Hours" Text='<%#Bind("ActualHours1") %>'
                                            OnClick="BtnViewDetails1_Click" />
                                        <asp:HiddenField ID="hdnPhase1" Value='<%# Bind("ActualHours1") %>' runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Comment" SortExpression="Comments">
                                    <HeaderStyle ForeColor="#FCFF00" />
                                    <ItemTemplate>
                                        <a onclick="showCommentPopup('<%# DataBinder.Eval(Container.DataItem, "Comments").ToString().Replace("'","&quot;&quot;") %>', event);">
                                            <%# DataBinder.Eval(Container.DataItem, "Comments").ToString().Length > 8 ? DataBinder.Eval(Container.DataItem, "Comments").ToString().Substring(0, 8) + "..." : DataBinder.Eval(Container.DataItem, "Comments")%></a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemStyle Width="5%" />
                                    <ItemTemplate>
                                        <asp:HyperLink ID="hplink" runat="server" NavigateUrl='<%# "Project.aspx?ProjectEditId="+DataBinder.Eval(Container.DataItem,"ProjectId")+" &ProjectEstimationID=" + 
                                         DataBinder.Eval(Container.DataItem,"ProjectEstimationID")+ " &opmode=Edit" %>'>Edit</asp:HyperLink>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemStyle Width="5%" />
                                    <ItemTemplate>
                                        <a id="hpDelete" runat="server" onclick="javascript: return confirm('Are you sure you want to delete?');"
                                            href='<%# "Project.aspx?ProjectEditId="+DataBinder.Eval(Container.DataItem,"ProjectId")+" &ProjectEstimationID=" + 
                                         DataBinder.Eval(Container.DataItem,"ProjectEstimationID")+ " &opmode=Delete" %>'>Delete</a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%-- <asp:BoundField HeaderText="" DataField="ActualHours">
                                    <HeaderStyle ForeColor="#FCFF00" />
                                </asp:BoundField>     --%>
                            </Columns>
                            <FooterStyle BackColor="#539cd1" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                            <HeaderStyle BackColor="#539cd1" Font-Bold="True" ForeColor="White" BorderColor="Black" />
                            <EditRowStyle BackColor="#2461BF" />
                            <AlternatingRowStyle BackColor="White" />
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        &nbsp;
                    </td>
                    <td align="center">
                        &nbsp;
                    </td>
                    <td align="center">
                        &nbsp;
                    </td>
                </tr>
            </table>
            <table>
                <tr>
                    <td style="text-align: left; padding-left: 270px" pa>
                        <asp:Label ID="lblActStatus" runat="server" Text="* Actual Hours are not adjusted for T & M projects, so 'Actual Hours (Adjusted)' column is excluded."
                            Font-Italic="True" Visible="False"></asp:Label>
                    </td>
                    <td align="left">
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>
    <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="btnShowPopup"
        PopupControlID="panEdit" BackgroundCssClass="modalBackground" CancelControlID="btnCancel1">
    </asp:ModalPopupExtender>
    <asp:Panel ID="panEdit" runat="server" Height="500px" Width="1200px" CssClass="ModalWindow">
        <div style="width: 100%; height: 500px; overflow: auto;">
            <table>
                <tr>
                    <td align="center" style="width: 1200px;">
                        <b>Change History</b>
                    </td>
                </tr>
            </table>
            <uc2:ProjectAudituserctrl ID="ProjectAudituserctrl1" runat="server" />
        </div>
        <br />
        <table>
            <tr>
                <td align="center" style="width: 1200px;">
                    <asp:Button ID="btnCancel1" runat="server" Text="Close" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:ModalPopupExtender ID="ModalPopupExtender2" runat="server" TargetControlID="btnShowPopup"
        PopupControlID="Panel1" BackgroundCssClass="modalBackground" CancelControlID="Button3">
    </asp:ModalPopupExtender>
    <asp:Panel ID="Panel1" runat="server" Height="300px" Width="1250px" CssClass="ModalWindow">
        <div style="width: 100%; height: 300px; overflow: auto;">
            <table>
                <tr>
                    <td align="center" style="width: 900px;">
                        <b>Change History</b>
                    </td>
                </tr>
            </table>
            <uc1:ProjectEstimationAuditusrctrl ID="ProjectEstimationAuditusrctrl1" runat="server" />
        </div>
        <br />
        <table>
            <tr>
                <td align="center" style="width: 900px;">
                    <asp:Button ID="Button3" runat="server" Text="Close" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:ModalPopupExtender ID="mdlPopup" runat="server" TargetControlID="btnShowPopup"
        PopupControlID="pnlPopup1" CancelControlID="btnClose" BackgroundCssClass="modalBackground" />
    <asp:Panel ID="pnlPopup1" runat="server" Height="150px" Width="350px" CssClass="ModalWindow">
        <div style="width: 350px; height: 150px; overflow: auto;">
            <table>
                <tr>
                    <td align="center" style="width: 500px;">
                        <b>Change History</b>
                    </td>
                </tr>
            </table>
            <uc3:ActualHoursuctrl ID="ActualHoursuctrl1" runat="server" />
        </div>
        <br />
        <table>
            <tr>
                <td align="center" style="width: 500px;">
                    <asp:Button ID="btnClose" runat="server" Text="Close" Width="50px" />
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:ModalPopupExtender ID="mdlPopup1" runat="server" TargetControlID="btnShowPopup"
        PopupControlID="pnlPopup2" CancelControlID="btnClose" BackgroundCssClass="modalBackground" />
    <asp:Panel ID="pnlPopup2" runat="server" Height="150px" Width="350px" CssClass="ModalWindow">
        <div style="width: 350px; height: 150px; overflow: scroll;">
            <table>
                <tr>
                    <td align="center" style="width: 500px;">
                        <b>Change History</b>
                    </td>
                </tr>
            </table>
            <uc4:NotAdjustedActualHours ID="NotAdjustedActualHours1" runat="server" />
        </div>
        <br />
        <table>
            <tr>
                <td align="center" style="width: 500px;">
                    <asp:Button ID="Button5" runat="server" Text="Close" Width="50px" />
                </td>
            </tr>
        </table>
    </asp:Panel>

    <asp:Button ID="btnShowPopup" runat="server" Style="display: none" />
    <table>
        <tr>
            <td align="center">
                <asp:TextBox ID="TextBox1" runat="server" TabIndex="-1" BorderStyle="None" Height="0px"
                    Width="0px"></asp:TextBox>
            </td>
        </tr>
    </table>
    <asp:Panel ID="pnlCloneProject" runat="server" Style="padding: 0px; border-width: 2px;"
        CssClass="ModalWindow">
        <table style="width: 350px; height: 80px;" cellpadding="5" cellspacing="0">
            <tr style="color: white; background-color: #539cd1;">
                <td colspan="2">
                    <b>Clone Project</b>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                </td>
            </tr>
            <tr>
                <td>
                    Project Name:
                </td>
                <td>
                    <asp:TextBox ID="txtCloneProjectName" Width="190px" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    Project Short Name:
                </td>
                <td>
                    <asp:TextBox ID="txtCloneProjectShortName" Width="190px" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <asp:Button ID="btnCloneProject" Text="Submit" OnClientClick="return ValidateCloneProjName();"
                        OnClick="btnCloneProject_Click" CausesValidation="false" runat="server" />&nbsp;<asp:Button
                            ID="btnCloneCancel" Text="Cancel" CausesValidation="false" OnClientClick="return false;"
                            runat="server" />
                </td>
            </tr>
        </table>
    </asp:Panel>

    
    
    <asp:ModalPopupExtender ID="ModalPopupExtender3" runat="server" 
        PopupControlID="Panel2" TargetControlID="resourceAudit"  BackgroundCssClass="modalBackground"  >
    </asp:ModalPopupExtender>
      
    <asp:Panel ID="Panel2" runat="server" Height="300px" Width="800px"  Style="display:none"  CssClass="ModalWindow">
        <div style="width:800px; height:300px; overflow: auto;">
           
            <table>
                <tr>
                    <td colspan="3"  align="center" style="width: 900px;">
                       <b><asp:Label ID="lblmsg" runat="server" ></asp:Label></b>
                    </td>
                    </tr>
                 <tr >
                    
                <td id="lbl1" align="center" runat="server" colspan="2" ><b><asp:Label ID="lblHeader" Text="Change History"  runat="server" ></asp:Label></b></td>
                </tr>
                 <tr class="blank_row"><td colspan="3"></td></tr>
               
                <tr>
                    <td align="center" colspan="3" ><asp:GridView Width="100%" Height="100%" class="myTableClass" ID="GridViewbind"  runat="server"   >
                        <RowStyle HorizontalAlign="left" /><HeaderStyle HorizontalAlign="left" />
  
                  </asp:GridView>

                    </td>
                  
                </tr>
                <tr>
                      <td align="center" colspan="3"  ><b><asp:Label ID="lblSyn"  runat="server" ></asp:Label></b></td>
                </tr>
            </table>
                 
          
        </div>
        <br />
     
        <table>
            <tr>
                <td align="center" style="width: 900px;">
                     <asp:Button ID="btnCancelpopup" OnClick="btnCancelpopup_Click" runat="server"  Text="Close" CausesValidation="false"  />
                </td>
            </tr>
        </table>
        
    </asp:Panel>

    <div id="divNote" style="background-color: #fdfdd6; padding: 5px 10px 10px 10px;
        text-align: left; position: absolute; border: solid 1px black; visibility: hidden;
        white-space: normal; z-index: 1000; max-width: 700px; word-wrap: break-word;
        max-height: 700px; overflow-y: scroll;">
    </div>
</asp:Content>
