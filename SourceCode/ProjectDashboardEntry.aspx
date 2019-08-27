<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/PMOMaster.master" CodeBehind="ProjectDashboardEntry.aspx.cs" Inherits="PMOscar.ProjectDashboard" %>

<asp:Content ContentPlaceHolderID="cntBody" runat="server">
    <div style="width:95%; text-align:right">
    <asp:LinkButton ID="lbBack" Text="<< Back" runat="server" 
        OnClick="lbBack_Click" CausesValidation="false" TabIndex="21"></asp:LinkButton>
</div>
    <div style="width:100%;">
         <style type="text/css" media="screen">
   
                BODY {  width:100%}
     </style>
         <table width = "100%">
         
            <tr>
                 <td align ="left"style="font-size:15px; border-bottom:1px solid #ccc;">
                   <b>
                     <asp:Label ID="lblProjectStatus" runat="server" Text="Project Status"></asp:Label></b>
                   
                   </td>
                   </tr>
         
            <tr>
                 <td align ="left"style="font-size:15px; height: 1px;">
                     </td>
                   </tr>
                   <tr> <td>
                       <asp:Label ID="lblheading" runat="server" Text="Project Name:" Font-Bold="true"></asp:Label>
                       </td>
                      
                     </tr>
                   <tr> <td>
                       <asp:Label ID="lblDates" runat="server" Text="Project Name:" Font-Bold="true"></asp:Label></td>
                      
                     </tr>
                     </table>
       <table  cellpadding="5" cellspacing="5">
       
       <tr>
       <td align="left" nowrap="nowrap">
        
           <asp:Label ID="Label1" runat="server" Text="Client Status"></asp:Label>
        
       </td>
       <td align="left" nowrap="nowrap">
       <table width="100%">
       <tr>
       <td>
         <asp:RadioButton ID="RBCG" runat="server" Checked="True" 
               GroupName="ClientStatus" TabIndex="1" />
       </td>
       <td>
       <asp:Image ID="Image1" runat="server" ImageUrl="~/Images/clientgreen.png" />
       </td>
       <td>
         <asp:RadioButton ID="RBCY" runat="server" GroupName="ClientStatus" 
               TabIndex="2" />
       </td>
       <td>
       <asp:Image ID="Image2" runat="server" ImageUrl="~/Images/clientyellow.png" />
       </td>
       <td>
         <asp:RadioButton ID="RBCR" runat="server" GroupName="ClientStatus" TabIndex="3" />
       </td>
       <td>
       <asp:Image ID="Image3" runat="server" ImageUrl="~/Images/clientred.png" />
       </td>
       </tr>
       </table>
         
         
       </td>
       </tr>
         <tr>
       <td align="left" nowrap="nowrap">
        
           <asp:Label ID="Label2" runat="server" Text="Timeline Status"></asp:Label>
        
       </td>
       <td align="left" nowrap="nowrap">
       <table width="100%">
       <tr>
       <td>
         <asp:RadioButton ID="RBTG" runat="server" Checked="True" 
               GroupName="TimelineStatus" TabIndex="4" />
       </td>
       <td>
       <asp:Image ID="Image4" runat="server" ImageUrl="~/Images/timelinegreen.png" />
       </td>
       <td>
         <asp:RadioButton ID="RBTY" runat="server" GroupName="TimelineStatus" 
               TabIndex="5" />
       </td>
       <td>
       <asp:Image ID="Image5" runat="server" ImageUrl="~/Images/timelineyellow.png" />
       </td>
       <td>
         <asp:RadioButton ID="RBTR" runat="server" GroupName="TimelineStatus" 
               TabIndex="6" />
       </td>
       <td>
       <asp:Image ID="Image6" runat="server" ImageUrl="~/Images/timelinered.png" />
       </td>
       </tr>
       </table>
         
         
       </td>
       </tr>   
        <tr>
       <td align="left" nowrap="nowrap">
        
           <asp:Label ID="lblBudget" runat="server" Text="Budget Status"></asp:Label>
        
       </td>
       <td align="left" nowrap="nowrap">
       <table width="100%">
       <tr>
       <td>
         <asp:RadioButton ID="RBBG" runat="server" Checked="True" 
               GroupName="BudgetStatus" TabIndex="7" />
       </td>
       <td>
       <asp:Image ID="Image7" runat="server" ImageUrl="~/Images/budgetgreen.png" />
       </td>
       <td>
         <asp:RadioButton ID="RBBY" runat="server" GroupName="BudgetStatus" 
                TabIndex="8" />
       </td>
       <td>
       <asp:Image ID="Image8" runat="server" ImageUrl="~/Images/budgetyellow.png" />
       </td>
       <td>
         <asp:RadioButton ID="RBBR" runat="server" GroupName="BudgetStatus" TabIndex="9" />
       </td>
       <td>
       <asp:Image ID="Image9" runat="server" ImageUrl="~/Images/budgetred.png" />
       </td>
       </tr>
       </table>
         
         
       </td>
       </tr>   
        <tr>
       <td align="left" nowrap="nowrap">
        
           <asp:Label ID="Label3" runat="server" Text="Escalate Status"></asp:Label>
        
       </td>
       <td align="left" nowrap="nowrap">
       <table width="100%">
       <tr>
       <td>
         <asp:RadioButton ID="RBEG" runat="server" Checked="True" 
               GroupName="EscalateStatus" TabIndex="10" />
       </td>
       <td>
       <asp:Image ID="Image10" runat="server" ImageUrl="~/Images/escalategreen.png" />
       </td>
       <td>
         <asp:RadioButton ID="RBEY" runat="server" GroupName="EscalateStatus" 
                TabIndex="11" />
       </td>
       <td>
       <asp:Image ID="Image11" runat="server" ImageUrl="~/Images/escalateyellow.png" />
       </td>
       <td>
         <asp:RadioButton ID="RBER" runat="server" GroupName="EscalateStatus" 
               TabIndex="12" />
       </td>
       <td>
       <asp:Image ID="Image12" runat="server" ImageUrl="~/Images/escalatered.png" />
       </td>
       </tr>
       </table>
         
         
       </td>
       </tr>   
        <tr>
       <td align="left" nowrap="nowrap">
        
           <asp:Label ID="Label4" runat="server" Text="Weekly Comments"></asp:Label>
        
       </td>
       <td align="left" nowrap="nowrap">
           <asp:TextBox ID="txtWeeklyComments" runat="server" TextMode="MultiLine" 
               Width="303px" TabIndex="13"></asp:TextBox>
         
       </td>
       </tr>   
        <tr>
       <td align="left" nowrap="nowrap">
        
           &nbsp;</td>
       <td align="left" nowrap="nowrap">
           <asp:Button ID="btnSave" runat="server" Text="Save" onclick="btnSave_Click" 
               TabIndex="14" />
           <asp:Button ID="btnCancel" runat="server" Text="Cancel" TabIndex="15" 
               Visible="False" />
         
       </td>
       </tr>   
        <tr>
        <td></td>
       <td align="center" nowrap="nowrap" colspan="2">
        
           <asp:Label ID="lblMsg" runat="server" ForeColor="#009900"></asp:Label>
            </td>
       </tr>   
        </table>
    </div>
</asp:Content>
