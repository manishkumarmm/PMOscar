<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="ProjectAudituserctrl.ascx.cs" Inherits="PMOscar.ProjectAudituserctrl" %>

  
<div>
    <table width="100%">
        <tr>
            <td>
                <asp:GridView ID="gvProjectAudit" runat="server" width="100%"
                    onrowdatabound="gvProjectAudit_RowDataBound">
                </asp:GridView>
            </td>
        </tr>
    </table>
</div>
