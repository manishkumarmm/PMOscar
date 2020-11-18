<%@ Page Language="C#" MasterPageFile="~/PMOMaster.master" AutoEventWireup="true" CodeBehind="ResourcePlanning.aspx.cs" Inherits="PMOscar.ResourcePlanning" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>



<asp:Content ContentPlaceHolderID="cntBody" runat="server">
    <style>
    .myTableClass tr th {
    padding: 5px;
}
        /* This is the style for the trigger icon. The margin-bottom value causes the icon to shift down to center it. */
        .disabledImageButton
{
     filter: alpha(opacity=30);
     opacity: .30;
     
}
        .blank_row
{
    height: 11px !important; /* overwrites any other rules */
    background-color: #FFFFFF;
}
   </style>


    <div>
        <div>
              <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
           <table width=1500px align="left" style="padding-left:100px; padding-top:15px">
                <tr>
                    <td align="left">
                        <asp:Label ID="Label4" runat="server" Text="Year:"></asp:Label></td>
                    <td align="left">
                        <asp:DropDownList ID="ddlYear" runat="server"
                            OnSelectedIndexChanged="ddlYear_SelectedIndexChanged" AutoPostBack="True"
                            Height="20px" Width="74px">
                        </asp:DropDownList>
                    </td>
                    <td align="left">
                        <asp:Label ID="Label5" runat="server" Text="Month:"></asp:Label></td>
                    <td align="left">

                        <asp:DropDownList ID="ddlMonth" runat="server" OnSelectedIndexChanged="ddlMonth_SelectedIndexChanged"
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

                    <td align="left">
                        <asp:Label ID="Label6" runat="server" Text="Week:"></asp:Label></td>
                    <td align="left">
                        <asp:DropDownList ID="ddlWeek" runat="server" Height="20px"
                            AutoPostBack="True" OnSelectedIndexChanged="ddlWeek_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                    <td align="left">
                        <asp:Button Text="Clone" runat="server" ID="btnClone" />
                    </td>
                    <td>
                        <asp:Button Text="Export" runat="server" ID="btnExport"
                            OnClick="btnExport_Click" />
                    </td>
                     <td>
                        <asp:Button Text="SyncActualHours" OnClientClick="return confirm('Do you want to sync ?');" runat="server" ID="btnAct_hour"
                            OnClick="btngetActualhours" style="height: 22px; " />
                    </td>
                     <td>
                        <asp:Button Text="Report" runat="server" ID="btnReport"
                            OnClick="btnReport_Click" />
                    </td>
                    <td>
                        <asp:LinkButton ID="lnkChng" Text="Change History" runat="server" TabIndex="-1" 
                                            onfocus="SetFocus(this)"></asp:LinkButton>
                    </td>
                </tr>
            </table>

        </div>

        <table width="100%">

            <tr>
                <td colspan="8" align="center">
                    <asp:Label ID="syncSuccess" Font-Bold="true" runat="server" Text="" ></asp:Label></td>   

            </tr>

            <tr>
                <td colspan="8" align="left" style="padding-left:750px"><b>Week:</b>
                    <asp:Label ID="lblWeekStatus" Font-Bold="true" runat="server" Text="Year"></asp:Label></td>
            </tr>

            <tr>
                <td colspan="8">&nbsp;</td>

            </tr>
            <tr>

                <td colspan="8">

                    <asp:GridView ID="gdResources" runat="server"
                        OnRowDataBound="gdResources_RowDataBound"
                        OnRowCreated="gdResources_RowCreated" Width="100%" CellPadding="5" EnableModelValidation="True">

                        <Columns>
                            <asp:TemplateField HeaderText="#">
                                <ItemStyle></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="Label7" runat="server" Text="Label"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Resource Name">
                                <ItemTemplate>
                                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl = "../images/Sync.png" OnClick="btngetActualhours1" ToolTip="Sync Hours"  OnClientClick="return false;" Visible="false" CommandArgument="Button1_Argument"/>

                                    <asp:HyperLink ID="lnkResource" runat="server"
                                        NavigateUrl='<%# "ResourcePlanningEntry.aspx?Id=" + DataBinder.Eval(Container.DataItem,"ResourceId") + " &name=" + DataBinder.Eval(Container.DataItem,"ResourceName")  + " &week=" + ddlWeek.SelectedItem.Text +" &year=" + ddlYear.SelectedItem.Text +" &month="+ ddlMonth.SelectedIndex+" &index="+ ddlWeek.SelectedIndex   %>'><%#Eval("ResourceName")%> </asp:HyperLink>

                                    <asp:Label ID="lblWeeklyComments" runat="server"> </asp:Label>
                                    

                                </ItemTemplate>
                                <ItemStyle Width="200px" />

                            </asp:TemplateField>
                        </Columns>

                    </asp:GridView>

                </td>
                <td></td>
            </tr>

            <tr>
                <td colspan="8">&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>

                <td>&nbsp;</td>
            </tr>

        </table>

    </div>

    <asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="lnkChng"
        PopupControlID="panEdit" BackgroundCssClass="modalBackground" CancelControlID="btnCancel1">
    </asp:ModalPopupExtender>
    <asp:Panel ID="panEdit" runat="server" Height="300px" Width="1100px" CssClass="ModalWindow" onClientClick="return false;" onfocus="SetFocus(this)" style = "display:none">
        <div style="width: 90%; height: 300px; overflow: auto;" >
            <table>
                <tr>
                    <td>
                        <b>Change History</b>
                    </td>
                </tr>
            </table>
             <div>
                
    <table  width="90%">




  <tr>
  <td ><asp:GridView ID="gvTimeTrackerAudit" runat="server"  >
  
      </asp:GridView></td>
   
  </tr>

  </table>
  </div>
 
  </div>
       
      
        <br />
        <asp:Button ID="btnCancel1" runat="server" Text="Close" />
    </asp:Panel>
    <asp:ModalPopupExtender ID="ModalPopupExtender2" runat="server" 
        PopupControlID="Panel1" TargetControlID="btnShowPopup" CancelControlID="btnCancel" BackgroundCssClass="modalBackground"  >
    </asp:ModalPopupExtender>
      
    <asp:Panel ID="Panel1" runat="server" Height="300px" Width="800px"  Style="display:none"  CssClass="ModalWindow">
        <div style="width:800px; height:300px; overflow: auto;">
           
            <table>
                <tr>
                    <td colspan="3"  align="center" style="width: 900px;">
                       <b><asp:Label ID="lblmsg" runat="server" ></asp:Label></b>
                    </td>
                    </tr>
                <tr >
                    
                <td id="lbl1" align="left" runat="server" colspan="2" ><b><asp:Label ID="lblResourceName"  runat="server" ></asp:Label></b></td>
                <td id="lbl2" align="right" runat="server" ><b><asp:Label ID="lblDate"  runat="server" ></asp:Label></b></td>
                    </tr>
                 <tr class="blank_row"><td colspan="3"></td></tr>
               
                <tr>
                    <td align="center" colspan="3" ><asp:GridView Width="100%" Height="100%" class="myTableClass" ID="GridViewbind"  runat="server"   >
                        <RowStyle HorizontalAlign="left" /><HeaderStyle HorizontalAlign="left" />
  
                  </asp:GridView>

                    </td>
                  
                </tr>
                <tr>
                      <td align="center" colspan="3"  ><b><asp:Label ID="lblSyn"  runat="server" ></asp:Label></b></td>
                </tr>
            </table>
                 
          
        </div>
        <br />
     
        <table>
            <tr>
                <td align="center" style="width: 900px;">
                    <asp:Button ID="btnOk" runat="server" Text="OK" OnClick="btnOK_Click" />
                     <asp:Button ID="btnCancel" runat="server"  Text="Cancel"  />
                </td>
            </tr>
        </table>
        
    </asp:Panel>
    <asp:Button ID="btnShowPopup" runat="server" Style="display: none" />

    <div id="divNote" style="background-color: #fdfdd6; padding: 5px 10px 10px 10px; text-align: left; position: absolute; border: solid 1px black; visibility: hidden;">
    </div>

    <script type="text/javascript" language="javascript">
        function showPopup(comment, e) {

            var posx = 0;
            var posy = 0;
            e = (window.event) ? window.event : e;
            posx = e.clientX + document.body.scrollLeft + document.documentElement.scrollLeft;
            posy = e.clientY + document.body.scrollTop + document.documentElement.scrollTop;

            var finalString = "<strong><span onclick=\"HideNotes()\" onmouseover=\"this.style.cursor='pointer';\" style=\"color: red;padding-left:210px;\">Close</span></strong><br /><br />"



            // Split a classnote into array by space
            var words = comment.split(" ")

            // Reset classnote = Empty
            comment = '';

            // Replace double quote(") to single quote (') through LOOP
            for (i = 0; i < words.length; i++) {
                comment = comment + words[i].replace('""', "'") + ' ';
            }


            finalString += comment

            document.getElementById("divNote").innerHTML = finalString;

            //  document.getElementById("divNote").innerHTML.

            document.getElementById("divNote").style.visibility = "visible";
            document.getElementById("divNote").style.top = posy + "px";
            document.getElementById("divNote").style.left = (posx) + "px";
            document.getElementById("divNote").style.width = "250px";
            //  document.getElementById("divNote").style.height = "150px";



            // document.getElementById("divNote").style.width = auto;
            document.getElementById("divNote").style.height = auto;

            document.getElementById("divNote").style.textAlign = "left";

        }

        function HideNotes() {

            document.getElementById("divNote").innerHTML = "";
            document.getElementById("divNote").style.visibility = "hidden";
        }

        function SetFocus(id) {
        }
        function ChangeHistoryPopUp() {
          




            var modalDialog = $find("ModalPopupExtender1");
            if (modalDialog != null) {
                modalDialog.show();
            }

        }
        //For ajsting thw width of Body Part
        var gridwidth = $('#ctl00_cntBody_gdResources').width() + 400;
        $('body').css('width', '' + gridwidth + 'px');
        //Appending First three columns to the last of the table in symmetric order

        $("#ctl00_cntBody_gdResources  > tbody > tr").each(function () {

            var $tds = $(this).children();

            $(this).append($tds.eq(2).clone()).append($tds.eq(1).clone()).append($tds.eq(0).clone());

        });

        
        //Changing the background colour of the Total row
        $("#ctl00_cntBody_gdResources").find('tr:last').css("background-color", "#91a8d5");

        //Appending the copy of Total row as the second row 
        $("#ctl00_cntBody_gdResources").find('tr:last').clone(true).insertAfter('#ctl00_cntBody_gdResources tr:first');

        //Appending the copy of Header row as the last row
        $("#ctl00_cntBody_gdResources").append($("#ctl00_cntBody_gdResources tr:first").clone());

        //Swapping the contents of the header row
        $("#ctl00_cntBody_gdResources tr:last-child th table tbody").each(function () {

            if ($(this).has('table')) {
                var rowcount = $(this).find('tr').length;
                if (rowcount > 1) {
                    var row1 = $(this).find('tr:eq(0)').html();

                    var row2 = $(this).find('tr:eq(1)').html();

                    $(this).find('tr:eq(0)').html(row2)
                    $(this).find('tr:eq(1)').html(row1)
                }
            }
        });


    </script>


</asp:Content>
<asp:Content ContentPlaceHolderID="cntHead" runat="server">
</asp:Content>
