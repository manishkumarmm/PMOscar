<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="TimeTrackerAudit.ascx.cs" Inherits="PMOscar.TimeTrackerAudit" %>



 <div>
    <table width="100%">




  <tr>
  <td><asp:GridView ID="gvTimeTrackerAudit" runat="server" 
          onrowcreated="gvTimeTrackerAudit_RowCreated">
      </asp:GridView></td>
   
  </tr>

  </table>
  </div>
 
</html>

