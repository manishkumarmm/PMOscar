<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Default" EnableViewStateMac="false" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">



<html xml:lang="en" xmlns="http://www.w3.org/1999/xhtml">
<head>
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>Welcome</title>
<link href="style/pmoscar_style.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style1
        {
        }
        .style2
        {
            width: 76px;
        }
    </style>
</head>
<body >

<table width="100%" border="0" align="center" cellpadding="0" cellspacing="0" background="images/bg_searchBar.gif" style="background-repeat:repeat-x;">
  <tr>
    <td>&nbsp;</td>
  </tr>
  <tr>
    <td height="60"><table width="937" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
          <td width="250" rowspan="2" valign="top"><img src="images/logo_pmoscar.gif" alt="" width="180" height="49" /></td>
          <td height="30" colspan="2" align="right" valign="bottom">&nbsp;</td>
        </tr>
      </table></td>
  </tr>
  <tr>
    <td>&nbsp;</td>
  </tr>
</table>
<!--Menu_start_here-->
<table width="100%" border="0" cellpadding="0" cellspacing="0" id="menuHolder">
  <tr>
    <td height="36">&nbsp;</td>
  </tr>
</table>

<!--Menu_ends_here-->

<table width="100%" border="0" align="center" cellpadding="0" cellspacing="0">
  <tr>
    <td height="10" style="background-color:#FFFFFF;" >&nbsp;</td>
  </tr>
  <tr>
    <td height="45" background="images/bg_searchBar.gif">&nbsp;</td>
  </tr>
  <tr>
    <td height="450" valign="top"  align="center"class="contentArea" ><div class="loginBox" style="background:url(images/bg_searchBar.gif) repeat-x;">
        <form id="form1" runat="server">
          <table width="375" border="0" align="center" cellpadding="5" cellspacing="0">
            <tr align="left">
              <td colspan="2" height="40" class="loginTitle"><b>LOGIN</b><br />
              </td>
              <td height="40" class="style2">&nbsp;</td>
            </tr>
            <tr>
              <td align="left"><asp:Label ID="lblUserName" runat="server">User Name</asp:Label></td>
              <td align="left" class="style1">
                  <asp:TextBox ID="txtUserName" runat="server" Width="200px" 
                      Height="16px"  ></asp:TextBox></td>
              <td align="left" class="style2">
                  <asp:RequiredFieldValidator ID="RFUsername" runat="server" 
                      ControlToValidate="txtUserName" ErrorMessage="*"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
              <td align="left"><asp:Label ID="lblPassword" runat="server" >Password</asp:Label></td>
              <td align="left" class="style1"><asp:TextBox ID="txtPassword" runat="server" 
                      Width="200px" Height="16px" TextMode="Password" ></asp:TextBox></td>
              <td align="left" class="style2">
                  <asp:RequiredFieldValidator ID="RFPassword" runat="server" 
                      ControlToValidate="txtPassword" ErrorMessage="*"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
              <td  height="50" ></td>
              <td align="left" class="style1" colspan="2">
              <asp:Button runat="server" Text="Login" ID="btnLogin" Width="50px" Height="25px" 
                      onclick="btnLogin_Click" />
                  <asp:Label ID="lblmsg" runat="server" ForeColor="#FF3300"></asp:Label>
              </td>
            </tr>
          </table>
        </form>
      </div></td>
  </tr>
  <tr>
    <td height="15">&nbsp;</td>
  </tr>
  <tr>
    <td height="25" background="images/bg_footer.gif"><table width="937" border="0" align="center" cellpadding="0" cellspacing="0">
        <tr>
          <td  align="center"><span class="colorcodewyte">Powered by NAICO</span></td>
        </tr>
      </table></td>
  </tr>
</table>

</body>
</html>
