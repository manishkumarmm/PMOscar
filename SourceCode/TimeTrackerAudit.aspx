﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TimeTrackerAudit.aspx.cs" Inherits="PMOscar.TimeTrackerAudit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
   
   
   <link href="Style/pmoscar_style.css" rel="stylesheet" type="text/css" />

</head>
<body>
 <form id="form1" runat="server">
 <div>
    <table width="100%">




  <tr>
  <td><asp:GridView ID="gvTimeTrackerAudit" runat="server">
      </asp:GridView></td>
   
  </tr>

  </table>
  </div>
  </form>
</body>
</html>
