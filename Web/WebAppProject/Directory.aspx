<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Directory.aspx.cs" Inherits="WebAppProject.Directory" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <!-- To scale page on mobile browsers -->
    <meta name="HandheldFriendly" content="true" />
    <meta name="viewport" content="width=device-width,initial-scale=1.0,maximum-scale=5.0;" />
    <title>This is Directory</title>
</head>

<body>
    <form id="form1" runat="server">
    <table align="center">
        <tr>
            <td><asp:Label ID="LabelHello" runat="server" /></td>
            <td><asp:LinkButton ID="SignIn" OnClick="Signin_Click" Text="Sign In" runat="server" /><p/></td>
            <td><asp:LinkButton ID="SignOut" OnClick="Signout_Click" Text="Sign Out" runat="server" /><p/></td>
        </tr>
        <tr>
            <td><asp:Label ID="PaageDirLabel" cellpadding="3" Text="Pages Directory" runat="server" Font-Bold="true"/></td>
        </tr>

        <tr>
            <td><asp:HyperLink ID="LinkToWelcome" NavigateUrl="./HomePage.aspx" Text="Home Page" Target="_self" runat="server" /></td>
        </tr>
        <tr>
            <td><asp:HyperLink ID="LinkToGasTracker" NavigateUrl="./GasTracker.aspx" Text="Gas Tracker" Target="_self" runat="server" /></td>
        </tr>
    </table>

    </form>
</body>
</html>
