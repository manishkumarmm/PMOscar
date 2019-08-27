<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/PMOMaster.master" CodeBehind="ResetPassword.aspx.cs" Inherits="PMDashBoard.ResetPassword" %>

<asp:Content ContentPlaceHolderID="cntBody" runat="server">
    <div style="width:90%;">
       <table width="65%" style="text-align:left ">
       <tr>
        <td  colspan="3" align ="left"style="font-size:15px; border-bottom:1px solid #ccc;">
                   <b>
                     <asp:Label ID="lblProjectStatus" runat="server" Text="Reset Password"></asp:Label></b>
                     </td>
       </tr>
          <tr><td colspan="3">&nbsp;</td></tr>
          <tr>
              <td style="width: 122px">User Name:</td>
              <td style="width: 187px"><asp:TextBox ID="txtUserName"  runat="server" 
                      CssClass="inputbox" Enabled="false" TabIndex="1" Width="191px"></asp:TextBox></td>
              <td></td>
          </tr>
          <tr>
              <td style="width: 122px">Old Password:</td>
              <td style="width: 187px">
                  <asp:TextBox ID="txtOldPwd" runat="server" 
                      CssClass="inputbox" TabIndex="2" Width="191px" TextMode="Password"></asp:TextBox></td>
              <td><asp:RequiredFieldValidator ID="RequiredFieldValidator1" ErrorMessage="*" ControlToValidate="txtOldPwd" runat="server"></asp:RequiredFieldValidator></td>
          </tr>
          <tr>
              <td style="width: 122px">New Password:</td>
              <td class="style2" style="width: 187px">
                  <asp:TextBox ID="txtNewPwd" runat="server" CssClass="inputbox" 
                      TextMode="Password" TabIndex="3" Width="191px"></asp:TextBox></td>
              <td><asp:RequiredFieldValidator ID="rfvNewPwd" ErrorMessage="*" ControlToValidate="txtNewPwd" runat="server"></asp:RequiredFieldValidator></td>
          </tr>
           <tr>
              <td style="width: 122px">Confirm Password:</td>
              <td class="style2" style="width: 187px">
                  <asp:TextBox ID="txtConfirmPwd" runat="server" 
                      CssClass="inputbox" TextMode="Password" TabIndex="4" Width="191px"></asp:TextBox></td>
              <td><asp:CompareValidator ID="cvPwd" runat="server" ControlToCompare="txtNewPwd" ControlToValidate="txtConfirmPwd" ErrorMessage="Mismatch"></asp:CompareValidator></td>
          </tr>
          <tr>
              <td colspan="2" style="text-align:center; height: 18px;">
                  <asp:Label ID="lblErrorMsg" runat="server" ForeColor="#FF3300"></asp:Label>
              </td>
          </tr>
          <tr>
              <td style="width: 122px">&nbsp;</td>
              <td colspan="2">
                  <asp:Button ID="btnChange" runat="server" Text="Change" 
                      onclick="btnChange_Click" TabIndex="5" />&nbsp;&nbsp;<asp:Button ID="btnCancel" 
                      runat="server" Text="Cancel" CausesValidation="false" 
                      onclick="btnCancel_Click" TabIndex="6" /></td>
          </tr>
       </table>
    </div>
</asp:Content>


