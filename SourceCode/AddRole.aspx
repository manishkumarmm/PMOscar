<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddRole.aspx.cs" Inherits="PMOscar.AddRole" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Create Role</title>
   
   <link href="Style/pmoscar_style.css" rel="stylesheet" type="text/css" />

    <style type="text/css">
        .auto-style1 {
            width: 16px;
        }
        .fnt-size{
            font-size:12px;
        }
    </style>

    <script type="text/javascript">

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

</head>
<body>
 <form id="form1" runat="server">
    <div>
        <table class="style1" >
            <tr>
                <td class="style2">
                    <div>
                        <asp:Label ID="Label1" runat="server" Text="Add Role" Font-Bold="true"></asp:Label>
                    </div>
                </td>
            </tr>
            <tr>
                <td class="style2 fnt-size" align="left">Role :</td>      
                <td align="left"><asp:TextBox ID="txtRole" runat="server" Width="143px" TabIndex="2"></asp:TextBox>
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Enter Role." ControlToValidate="txtRole"></asp:RequiredFieldValidator>
                &nbsp;&nbsp;&nbsp;
                </td>
            </tr>

            <tr>
                <td class="style2 fnt-size" align="left">Short Name :</td> 
                <td align="left"><asp:TextBox ID="txtShortName" runat="server" Width="143px" TabIndex="2"></asp:TextBox>
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ErrorMessage="Enter Shortname." ControlToValidate="txtShortName"></asp:RequiredFieldValidator></td>
            </tr>

            <tr>
                <td class="style2 fnt-size" align="left">Description :</td> 
                <td align="left"><asp:TextBox ID="txtDescription" runat="server" Width="143px" TabIndex="2"></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td class="style2 fnt-size" align="left">Estimation Percentage : </td> 
                <td align="left"> <asp:TextBox ID="txtEstpercent" runat="server" Width="143px" TabIndex="2" onkeypress="return CheckNemericValue(event,this);" MaxLength="4"></asp:TextBox>
                </td>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Enter Estimation Percentage." ControlToValidate="txtEstpercent"></asp:RequiredFieldValidator>
                </td>
                <td>
                    <asp:Label ID="lblEstimation" runat="server" Visible="false" Forecolor="Red"></asp:Label>

                </td>
               
            </tr>

            <tr>
                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Enter Decimal only....!" ValidationExpression="\d+(\.\d{1,2})?" ValidationGroup="save" ControlToValidate="txtEstpercent"></asp:RegularExpressionValidator>     
                <td align ="center">
                    <br />
                    
                    <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" Width="60px" TabIndex="5" style="margin-left: 28px" />
                    </td>
                    <td align ="center">
                        <br />
                        <asp:Button ID="Button1" runat="server" Text="Cancel" OnClick="btnCancel_Click" Width="73px" TabIndex="5" style="margin-left: 28px" CausesValidation="false"/>
                    
                    
                </td>

            </tr>
            <tr>
                <td colspan="2" align="left">
                    <asp:Label ID="lblmsg" runat="server" ForeColor="Red"></asp:Label></td>
            </tr>
        </table>
       
    </div>
    </form>
</body>
</html>
