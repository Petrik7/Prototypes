<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InfoPage.aspx.cs" Inherits="WebAppProject.InfoPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Purchase details</title>
     <!-- To scale page on mobile browsers -->
    <meta name="HandheldFriendly" content="true" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0,maximum-scale=4.0;" />
    <!-- Style -->
    <style type="text/css">
        body {font-family:Calibri;}
        
        table.TableInfoTable tr.light td {background-color: #FAFAD2;}
        table.TableInfoTable tr.dark0 td {background-color: #EEE8AA;}
    </style>
</head>

<body>
<form id="PurchaseDetailsForm" runat="server">
    <div></div>
    <asp:ScriptManager ID="ScriptManager" runat="server" EnablePartialRendering="true" />
        <asp:Label ID="PageErrorLabel" runat="server" Visible="false"></asp:Label>

        <table id="MainTable" align="center">
            <tr>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <table cellpadding="1" border="0" align="center">
                        <tr>
                            <td align="left" width="58px"><asp:Label ID="LabelHello" runat="server"/></td>
                            <td align="right"> 
                                <asp:LinkButton ID="GasTracker" Text="Gas Tracker" OnClick="GasTracker_Click" runat="server" Width="88px" CausesValidation="false"/>
                                <asp:LinkButton ID="ButtonHomePage" Text="Home Page" OnClick="HomePage_Click" runat="server" CausesValidation="false"/>
                                <asp:LinkButton ID="ButtonSignOut" Text="Sign Out" OnClick="Signout_Click" runat="server" Width="68px" CausesValidation="false"/>
                            </td>
                        </tr>
                     </table>
                 </ContentTemplate>
                 </asp:UpdatePanel>
             </tr>
             <tr>
                <td >
                  <table id="TableInfo" class="TableInfoTable" cellpadding="2" align="center" cellspacing = "0" runat="server" borderwidth="8" width = "300"
                          style="border-left: 1px solid Tan; border-right: 1px solid Tan; border-top: 1px solid Tan; border-bottom: 1px solid Tan">
                        <tr class="dark0">
                            <td align="left" ><asp:Label ID="InfoLabel" text="Info:" runat="server" /></td>
                        </tr>
                        <tr class="light">
                            <td align="left" ><asp:Label ID="ErrorTextLabel" text="Purchase details:" runat="server" /></td>
                        </tr>
                    </table>
                 </td>
             </tr>


         </table>
    </form>
</body>
</html>
