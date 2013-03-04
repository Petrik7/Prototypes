﻿<%@ Import namespace="WebAppProject.App_SrcCode" %>
<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GasTracker.aspx.cs" Inherits="WebAppProject.GasTracker" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <!-- To scale page on mobile browsers -->
    <meta name="HandheldFriendly" content="true" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0,maximum-scale=4.0;" />

    <title>This is Gas Tracker</title>
    <script runat="server">
    </script>
</head>

<body>

    
    <form id="form1" runat="server">
    <div></div>
    
    <asp:ScriptManager ID="ScriptManager" runat="server" EnablePartialRendering="true" />
          
    <table align="center">
         <tr>
            <td align="left"><asp:Label ID="LabelWelcome" runat="server" /></td>
            <td align="right"> <asp:LinkButton ID="ButtonSignOut" Text="Sign Out" OnClick="Signout_Click" runat="server" Width="68px" /></td> 
         </tr>
         <tr><td><asp:Label ID="LabelInfo" runat="server" /></td></tr>
         <tr>
            <td align="right" colspan="2">
            <asp:RadioButtonList ID="RadioButtonList_MilesKms" 
                RepeatDirection="Horizontal" TextAlign="Right" AutoPostBack="true" runat="server"
                OnSelectedIndexChanged="RadioButtonList_MilesKms_SelectedIndexChanged">
                <asp:ListItem>Liters/100 Kms</asp:ListItem>
                <asp:ListItem>Miles/Gallon</asp:ListItem>
            </asp:RadioButtonList> 
            </td>
         </tr>
         <tr>
            <td style="height: 206px" valign="top" colspan="2">
                <asp:UpdatePanel ID="UpdatePanelGasPurchases" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:GridView ID="GridViewGasPurchases" runat="server" BackColor="LightGoldenrodYellow" BorderColor="Tan"
                            BorderWidth="1px" CellPadding="8" ForeColor="Black" GridLines="None" AutoGenerateColumns="False">
                            <FooterStyle BackColor="Tan" />
                            <SelectedRowStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
                            <PagerStyle BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue" HorizontalAlign="Center" />
                            <HeaderStyle BackColor="Tan" Font-Bold="True" />
                            <AlternatingRowStyle BackColor="PaleGoldenrod" />
                            <Columns>
                                <asp:BoundField DataField="When" HeaderText="When" />
                                <asp:BoundField DataField="Price" HeaderText="Price" />
                                <asp:BoundField DataField="Amount" HeaderText="Amount" />
                                <asp:BoundField DataField="Distance" HeaderText="Distance" />
                                <asp:BoundField DataField="Milage" HeaderText="Milage" />
                            </Columns>
                            <PagerSettings PageButtonCount="3" />
                        </asp:GridView>
                        <asp:Label runat="server" ID="Label1"><%=DateTime.Now %></asp:Label>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="RadioButtonList_MilesKms" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
            </td>
           </tr>
           <tr><td></td></tr>
        </table>

    </form>
</body>
</html>
