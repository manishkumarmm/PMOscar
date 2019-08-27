<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProjectEstimationAudit.aspx.cs" Inherits="PMOscar.ProjectEstimationAudit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
   
   
   <link href="Style/pmoscar_style.css" rel="stylesheet" type="text/css" />

</head>
<body>
 <form id="form1" runat="server">
 <div style = "width:100%;height:600px;overflow:scroll;" >
    <table width="100%">

  <tr>
  <td><asp:GridView ID="gvProjectEstimationAudit" runat="server">
      </asp:GridView></td>
   
  </tr>

  </table>
  </div>
  </form>
</body>
</html>
