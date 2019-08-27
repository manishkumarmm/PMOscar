<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ResourceClone.aspx.cs" Inherits="PMOscar.ResourceClone" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Resource Clone</title>
</head>
<body>
    <form id="form1" runat="server">
 <div>
   <table width="50%">  
   <tr >
        <td align="left"><asp:Label ID="Label4" runat="server" Text="Year:"></asp:Label></td> 
              <td align="left" >  
                  <asp:DropDownList ID="ddlYear" runat="server" 
                      onselectedindexchanged="ddlYear_SelectedIndexChanged" AutoPostBack="True" 
                      Height="20px" Width="74px" >                 
                 
                </asp:DropDownList>
            </td >
          <td align="left"><asp:Label ID="Label5" runat="server" Text="Month:"></asp:Label></td> 
           <td align="left">  
                  
                  <asp:DropDownList ID="ddlMonth" runat="server" onselectedindexchanged="ddlMonth_SelectedIndexChanged" 
                  AutoPostBack="True" Height="20px">
               
                  <asp:ListItem Value="1">January</asp:ListItem>
                  <asp:ListItem Value="2">February</asp:ListItem>
                  <asp:ListItem Value="3">March</asp:ListItem>
                  <asp:ListItem Value="4">April</asp:ListItem>
                  <asp:ListItem Value="5">May</asp:ListItem>
                  <asp:ListItem Value="6">June</asp:ListItem>
                  <asp:ListItem Value="7">July</asp:ListItem>
                  <asp:ListItem Value="8">August</asp:ListItem>
                  <asp:ListItem Value="9">September</asp:ListItem>
                  <asp:ListItem Value="10">October</asp:ListItem>
                  <asp:ListItem Value="11">November</asp:ListItem>
                  <asp:ListItem Value="12">December</asp:ListItem>
                  
                </asp:DropDownList>
                
            </td>
            
          <td align="left"><asp:Label ID="Label6" runat="server" Text="Week:"></asp:Label></td> 
              <td align="left">  <asp:DropDownList ID="ddlWeek" runat="server" Height="20px" 
                      AutoPostBack="True" onselectedindexchanged="ddlWeek_SelectedIndexChanged">
              
                
                </asp:DropDownList>
            </td>
              <td align="left">  
           <asp:Button Text="Clone" runat="server" ID="btnClone" onclick="btnClone_Click"  />
            </td>
           <td>
           <asp:Button Text="Close" runat="server" ID="btnClose" onclick="btnClone_Click" 
                   Visible="False"  />
            </td>
   </tr>
   <tr >
        <td align="left">&nbsp;</td> 
              <td align="left" >  
                  &nbsp;</td >
          <td align="left" colspan="4">
              <asp:Label ID="lblErrormsg" runat="server" ForeColor="Red"></asp:Label>
        </td> 
              <td align="left">  
                  &nbsp;</td>
           <td>
               &nbsp;</td>
   </tr>
   </table>             
           
    </div>
   
    </form>
</body>
</html>
