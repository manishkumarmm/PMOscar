<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreateDashboard.aspx.cs" Inherits="PMOscar.CreateDashboard" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Create Dashboard</title>
   
   <link href="Style/pmoscar_style.css" rel="stylesheet" type="text/css" />

</head>
<body>
 <form id="form1" runat="server">
 <div>
    <table width="100%">
    <tr>
    <td>
       <div >   
        <asp:label ID="Label8" runat="server" Text="Create New Dashboard" Font-Bold="True"></asp:label>     
   
      </div>
  </td>
  <td \>
  </td>
  <td>
  </td>
  <td>
  </td>
  </tr>
  
  <tr>
  <td align="left" class="style1" >
         <asp:Label ID="Label7" runat="server" Text="Period"></asp:Label>
     </td>
      <td align="left" class="style3" >
          <asp:DropDownList ID="ddlProjectPeriod" runat="server" Height="20px" 
              Width="136px" AutoPostBack="True" 
              onselectedindexchanged="ddlProjectPeriod_SelectedIndexChanged" 
              TabIndex="1">            
              <asp:ListItem Value="0">Weekly</asp:ListItem>
              <asp:ListItem Value="1">Multi-Week</asp:ListItem>
          </asp:DropDownList>
          <asp:Button ID="btnClose" runat="server" CssClass="submitButton"  
              onclick="btnClose_Click" Text="Close" TabIndex="2" />
     </td>
     
  </tr>
  
  <tr>
  <td align="left" class="style1" >
         &nbsp;</td>
      <td align="left" class="style3" >
          &nbsp;</td>
     
  </tr>
  
  <tr>
  <td  colspan="2">
  <div>
   <asp:Panel ID="pnlWeeklySearch" runat="server">
      <div >
       <table width="100%">  
   <tr  align="left">
        <td align="left" style="width: 87px"><asp:Label ID="Label4" runat="server" Text="Year:"></asp:Label></td><td align="left" style="width: 120px" >  
                  <asp:DropDownList ID="ddlYear" runat="server" 
                      onselectedindexchanged="ddlYear_SelectedIndexChanged" AutoPostBack="True" 
                      Height="20px" Width="74px" TabIndex="3" >                 
                 
                </asp:DropDownList>
            </td >
          <td align="left" style="width: 83px"><asp:Label ID="Label5" runat="server" Text="Month:"></asp:Label></td><td align="left" style="width: 116px" >  
                  
                  <asp:DropDownList ID="ddlMonth" runat="server" onselectedindexchanged="ddlMonth_SelectedIndexChanged" 
                  AutoPostBack="True" Height="20px" TabIndex="4">
               
                  <asp:ListItem Value="1">January</asp:ListItem><asp:ListItem Value="2">February</asp:ListItem><asp:ListItem Value="3">March</asp:ListItem><asp:ListItem Value="4">April</asp:ListItem><asp:ListItem Value="5">May</asp:ListItem><asp:ListItem Value="6">June</asp:ListItem><asp:ListItem Value="7">July</asp:ListItem><asp:ListItem Value="8">August</asp:ListItem><asp:ListItem Value="9">September</asp:ListItem><asp:ListItem Value="10">October</asp:ListItem><asp:ListItem Value="11">November</asp:ListItem><asp:ListItem Value="12">December</asp:ListItem></asp:DropDownList></td><td align="left" style="width: 77px"><asp:Label ID="Label6" runat="server" Text="Week:"></asp:Label></td><td align="left" style="width: 95px">  
        <asp:DropDownList ID="ddlWeek" runat="server" Height="20px" 
                      AutoPostBack="True" 
            onselectedindexchanged="ddlWeek_SelectedIndexChanged" TabIndex="5">
              
                
                </asp:DropDownList>
            </td>
           <td>
               <asp:Button ID="btnCreate" runat="server" CssClass="submitButton" 
                   onclick="btnCreate_Click" Text="Create" TabIndex="6" />
        </td>
   </tr>
   </table>         
     </div>   
    </asp:Panel>
  </div>
  </td>
  </tr>
  <tr>
  <td class="style2" colspan="2">
  <div>
   <asp:Panel ID="pnlMultiWeelySearch" runat="server">
      <div >
       <table width="100%">  
   <tr >
        <td align="left" style="width: 86px"><asp:Label ID="Label1" runat="server" Text="Year:"></asp:Label></td><td align="left" style="width: 122px" >  
                  <asp:DropDownList ID="ddlFromYear" runat="server" 
                      onselectedindexchanged="ddlFromYear_SelectedIndexChanged" AutoPostBack="True" 
                      Height="20px" Width="74px" TabIndex="1" >                 
                 
                </asp:DropDownList>
            </td >
          <td align="left" style="width: 83px"><asp:Label ID="Label2" runat="server" Text="Month:"></asp:Label></td><td align="left" style="width: 114px" >  
                  
                  <asp:DropDownList ID="ddlfromMonth" runat="server" onselectedindexchanged="ddlFromMonth_SelectedIndexChanged" 
                  AutoPostBack="True" Height="20px" TabIndex="2">
               
                  <asp:ListItem Value="1">January</asp:ListItem><asp:ListItem Value="2">February</asp:ListItem><asp:ListItem Value="3">March</asp:ListItem><asp:ListItem Value="4">April</asp:ListItem><asp:ListItem Value="5">May</asp:ListItem><asp:ListItem Value="6">June</asp:ListItem><asp:ListItem Value="7">July</asp:ListItem><asp:ListItem Value="8">August</asp:ListItem><asp:ListItem Value="9">September</asp:ListItem><asp:ListItem Value="10">October</asp:ListItem><asp:ListItem Value="11">November</asp:ListItem><asp:ListItem Value="12">December</asp:ListItem></asp:DropDownList></td><td align="left" style="width: 79px"><asp:Label ID="Label3" runat="server" Text="Week:"></asp:Label></td><td align="left" style="width: 95px">  
                  <asp:DropDownList ID="ddlFromWeek" runat="server" Height="20px" 
                      AutoPostBack="True" 
                      onselectedindexchanged="ddlFromWeek_SelectedIndexChanged" TabIndex="3">
              
                
                </asp:DropDownList>
            </td>
           <td>
               &nbsp;</td></tr><tr >
        <td align="left" style="width: 86px"><asp:Label ID="Label9" runat="server" Text="Year:"></asp:Label></td><td align="left" style="width: 122px" >  
                  <asp:DropDownList ID="ddlToYear" runat="server" 
                      onselectedindexchanged="ddlToYear_SelectedIndexChanged" AutoPostBack="True" 
                      Height="20px" Width="74px" TabIndex="4" >                 
                 
                </asp:DropDownList>
            </td >
          <td align="left" style="width: 83px"><asp:Label ID="Label10" runat="server" Text="Month:"></asp:Label></td><td align="left" style="width: 114px">  
                  
                  <asp:DropDownList ID="ddlToMonth" runat="server" onselectedindexchanged="ddlToMonth_SelectedIndexChanged" 
                  AutoPostBack="True" Height="20px" TabIndex="5">
               
                  <asp:ListItem Value="1">January</asp:ListItem><asp:ListItem Value="2">February</asp:ListItem><asp:ListItem Value="3">March</asp:ListItem><asp:ListItem Value="4">April</asp:ListItem><asp:ListItem Value="5">May</asp:ListItem><asp:ListItem Value="6">June</asp:ListItem><asp:ListItem Value="7">July</asp:ListItem><asp:ListItem Value="8">August</asp:ListItem><asp:ListItem Value="9">September</asp:ListItem><asp:ListItem Value="10">October</asp:ListItem><asp:ListItem Value="11">November</asp:ListItem><asp:ListItem Value="12">December</asp:ListItem></asp:DropDownList></td><td align="left" style="width: 79px"><asp:Label ID="Label11" runat="server" Text="Week:"></asp:Label></td><td align="left" style="width: 95px">  
                  <asp:DropDownList ID="ddlToWeek" runat="server" Height="20px" 
                      AutoPostBack="True" 
                      onselectedindexchanged="ddlToWeek_SelectedIndexChanged" TabIndex="6">
              
                
                </asp:DropDownList>
            </td>
           <td align="left">
               <asp:Button ID="btnMultiweekCreate" runat="server" CssClass="submitButton" 
                   onclick="btnMultiweekCreate_Click" Text="Create" TabIndex="7" />
        </td>
   </tr>
     
   </table>         
     </div>   
    </asp:Panel>
  </div>
  </td>
  </tr>
  <tr>
  <td colspan="2" align="center">
 <asp:Label ID="lblMsg" runat="server" ForeColor="Green" Text="&quot;&quot;"></asp:Label>
  </td>
        <td>
            &nbsp;</td>
  </tr>
  </table>
  </div>
  </form>
</body>
</html>
