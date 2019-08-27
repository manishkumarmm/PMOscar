<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/PMOMaster.master" CodeBehind="AccessDenied.aspx.cs" Inherits="PMOscar.AccessDenied" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cntBody" runat="server">

    <div>
    
        <asp:Label ID="Label1" runat="server" Font-Bold="True" ForeColor="#FF3300" 
            Text="You are not authorized to view this page !" Font-Size="Large"></asp:Label>
    
    </div>
</asp:Content>