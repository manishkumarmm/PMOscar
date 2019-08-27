<%@ Page Title="" Language="C#" MasterPageFile="~/PMOMaster.master" AutoEventWireup="true" CodeBehind="UserEdit.aspx.cs" Inherits="PMOscar.UserEdit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cntBody" runat="server">

  <script type="text/javascript">

    function CheckTextValue(e, myfield) {
        
        var key;
        key = e.which ? e.which : e.keyCode;
        keychar = String.fromCharCode(key);
  if ((key == 9)||(key == 8))
              return true;

           else
        if ((("abcdefghijklmnopqrstuvwxyz").indexOf(keychar) > -1))
            return true;


        // numbers
        else if ((("ABCDEFGHIJKLMNOPQRSTUVWXYZ").indexOf(keychar) > -1))
            return true;

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
    
     <table>
       
       <tr>
                <td align="left" style=" border-bottom: 1px solid #ccc;">
                    <b>
                        <asp:Label ID="lblProjectStatus" runat="server" Text="Edit User"></asp:Label></b>
                    &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                    &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                    &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                    &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                    &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                    &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                    &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                    &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                    &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                     &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                    &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                   
                </td>
            </tr>
            </table>
    
    
    
    
    
        <table class="style1">
        
        
        <tr><td style="height:25px;"></td><td></td></tr>
             <tr>
         
            <td  align = "left">
                <asp:Label ID="LabelFirstName" runat="server" Text="Label">Name :</asp:Label>
            </td>
            
            
            
            
            <td>
                <asp:TextBox ID="FirstName" runat="server" Width="196px" TabIndex="1"></asp:TextBox> </td>
                
                <td>
                <asp:RequiredFieldValidator ID="RequiredFieldValidatorFirstName" runat="server" ControlToValidate="FirstName"
                    ErrorMessage="Enter Name."></asp:RequiredFieldValidator>
                    
                 </td>   
                    
                       
        </tr>
             <tr>
           
           <td  align = "left">
                <asp:Label ID="LabelLastName" runat="server" Text="Label">Last Name :</asp:Label>
            </td>
           
            <td>
                <asp:TextBox ID="LastName" runat="server" Width="196px"></asp:TextBox>
            </td>
           
        
        </tr>
           
             <tr>
               <td align="left">
                                Role :
                            </td>
                            <td align="left" nowrap="nowrap"  >
                            
                            <asp:DropDownList ID="ddlRole" runat="server"  
                                    TabIndex="2" Width="205px">
                                </asp:DropDownList>                               
                            
                                
                            </td>
                            <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" 
                                    ControlToValidate="ddlRole" InitialValue="0"
                                    runat="server" ErrorMessage="Select Role."></asp:RequiredFieldValidator>
                            
                            </td>
                           </tr>        
            
       
       

        <tr>
           
           <td  align = "left">
               <asp:Label ID="lblEmployeecode" runat="server" Text="Employee Code"></asp:Label>
                </td>
           
            <td>
                <asp:TextBox ID="txtEmployeecode" runat="server" Width="196px" onkeypress="return CheckNemericValue(event,this);"></asp:TextBox>
            </td>
             <td>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Enter Employee Code." ControlToValidate="txtEmployeeCode"></asp:RequiredFieldValidator>
            </td>
        
        </tr>

         <tr>
        
            <td align = "left">
                <asp:Label ID="LabelUserName" runat="server" Text="User Name(Email ID):"></asp:Label>
                
            </td>
        
            
            <td>
              <asp:TextBox ID="UserName" runat="server" Width="196px" TabIndex="3" ></asp:TextBox>
            </td>
            <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Enter Username." ControlToValidate="UserName"></asp:RequiredFieldValidator>
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                        ControlToValidate="UserName" ErrorMessage="Invalid Email ID." 
                        ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
            </td>
        </tr>

           <tr>
        
                <td  align = "left">
                    <asp:Label ID="LabelPassword" runat="server" Text="Password :"></asp:Label>
            
                </td>
                <td>                    
                     <asp:TextBox ID="Password" runat="server" Width="196px" TabIndex="4" 
                         TextMode="Password"></asp:TextBox> 
                </td>
                <td>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidatorPassword" runat="server" ErrorMessage="Enter Password." ControlToValidate="Password"></asp:RequiredFieldValidator>
                 </td>
           </tr>
<%--
            <tr>
            
                    <td  align = "right">
                        <asp:Label ID="LabelRetypePassword" runat="server" Text="Re-type Password :"></asp:Label>
                    </td>
            
                    <td>                      
                         <asp:TextBox ID="RetypePassword" runat="server"></asp:TextBox> 
                    </td>
                <td>
                 <asp:CompareValidator ID="CompareValidatorPassword" runat="server" ErrorMessage="*" ControlToCompare="Password" ControlToValidate="RetypePassword"></asp:CompareValidator>
                 </td>
        
          </tr>

            <tr>
     
                    <td align = "left">
                        <asp:Label ID="LabelEmailID" runat="server" Text="Email ID :"></asp:Label>
                    </td>
        
                    <td style="width: 135px">
                           <asp:TextBox ID="EmailID" runat="server" Width="196px"></asp:TextBox>
                    </td>
          </tr>--%>

            
            <tr>
     
                    <td align = "left">
                        Status:</td>
        
                    <td style="width: 135px">
                          <table>
                          <tr>
                          <td>
                          <asp:RadioButton ID="rdActive" runat="server" Text=" Active" Checked="True" 
                            GroupName="Status" TabIndex="5"/>
                          </td>
                          <td>
                           <asp:RadioButton ID="rdInActive" runat="server" Text="Inactive" 
                            GroupName="Status" TabIndex="6"/>
                          </td>
                          </tr>
                          </table></td>
          </tr>

            
            <tr>
                <td colspan="3" align="center">
                    &nbsp;&nbsp;
                    <asp:Button ID="btnSave" runat="server" Text="Save" Width="60px" 
                        OnClick="Submit" TabIndex="7" />
                    &nbsp;&nbsp;
                    <asp:Button ID="btnCancel" runat="server" Width="60px" Text="Cancel" CausesValidation="false"
                        OnClick="btnCancel_Click" TabIndex="8" />
                </td>
            </tr>

            
            <tr>
                <td colspan="3" align="center">
                    <asp:Label ID="lblError" runat="server" ForeColor="#FF3300"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="ContentHeader" runat="server" ContentPlaceHolderID="cntHead">
   
</asp:Content>
