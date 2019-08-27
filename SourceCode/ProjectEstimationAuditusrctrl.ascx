<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProjectEstimationAuditusrctrl.ascx.cs" Inherits="PMOscar.ProjectEstimationAuditusrctrl" %>

<div>
    <table width="100%">
        <tr>
            <td>
                <asp:GridView ID="gvProjectEstimationAudit" runat="server" OnRowDataBound="gvProjectEstimationAudit_RowDataBound" width="100%">
                </asp:GridView>
            </td>
        </tr>
    </table>
</div>
