<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProjectEstimation.ascx.cs" Inherits="PMOscar.ProjectEstimation" %>
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

</script>
<div>
    <input id="hdnProjectEstimationID" runat="server" type="hidden" value="" />
    <table class="style1" cellpadding="2" cellspacing="2">
        <tr>
            <td></td>
            <td></td>
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
                <asp:Label ID="lblValPhase" runat="server" ForeColor="Red" Text="Select Phase"></asp:Label>
            </td>
            <td style="width: 121px" align="left">
                <asp:Label ID="lblPhase0" runat="server" Text="Role"></asp:Label>
            </td>
            <td style="width: 121px" align="left">
                <asp:DropDownList ID="ddlRole" runat="server" Width="200px" TabIndex="30">
                </asp:DropDownList>
            </td>
            <td style="width: 121px" align="left">
                <asp:Label ID="lblValRole" runat="server" ForeColor="Red" Text="Select Role"></asp:Label>
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
            <td style="width: 121px" align="left">Budgeted Hours
            </td>
            <td style="width: 121px" align="left">
                <asp:TextBox ID="txtBudHours" runat="server" Width="191px" TabIndex="32" MaxLength="9"></asp:TextBox>
            </td>
            <td style="width: 121px" align="left">
                <span id="spTxtBudHours" class="fontColor"></span>
            </td>
        </tr>
        <tr>
            <td align="left" style="width: 115px">Revised Budgeted Hours
            </td>
            <td align="left">
                <asp:TextBox ID="txtRevBudHours" runat="server" Width="191px" TabIndex="33" MaxLength="9"></asp:TextBox>
            </td>
            <td style="width: 121px" align="left">
                <span id="spTxtRevBudHours" class="fontColor"></span>
            </td>
            <td style="width: 121px" align="left">Comments
            </td>

            <td style="width: 121px" align="left">
                <asp:TextBox ID="txtComments" runat="server" Width="191px" TabIndex="34"></asp:TextBox>
            </td>
            <td id="Td1" style="width: 140px" align="left" runat="server">
                <asp:Label ID="lblComments" runat="server" ForeColor="Red" Text="Please Enter a Comment"></asp:Label>
            </td>
        </tr>
        <tr>
            <td align="left" style="width: 115px">&nbsp;
            </td>
            <td align="left" style="width: 191px">&nbsp;
            </td>
            <td style="width: 121px" align="left"></td>
            <td style="width: 121px" align="left"></td>
            <td style="width: 121px" align="right" nowrap="nowrap">
                <asp:Button ID="btnEstSave" runat="server" Text="Save" Width="60px" Height="22px" OnClientClick="javascript:return CheckValid();"
                    OnClick="btnAdd_Click" TabIndex="34" />
                &nbsp;&nbsp;
                                    <asp:Button ID="btnEstCancel" runat="server" Width="60px" Text="Clear" CausesValidation="false"
                                        TabIndex="35" Height="22px" OnClientClick="javascript:ClearEstimation(); return false;"/>
            </td>
            <td style="width: 121px">
                <asp:TextBox ID="txtEstID" runat="server" TabIndex="-1" style="display:none;" Width="93px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="center" colspan="6">
                <asp:Label runat="server" ID="lblesterror" ForeColor="#FF3300"></asp:Label>
            </td>
        </tr>
    </table>
    <asp:TextBox ID="TextBox1" runat="server" TabIndex="-1" BorderStyle="None" Height="0px"
        Width="0px"></asp:TextBox>
</div>


