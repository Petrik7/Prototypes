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

    <h3>Pages Directory</h3>
    <div>
    <asp:HyperLink ID="LinkToWelcome" NavigateUrl="./Welcome.aspx" Text="Welcome" Target="_self" runat="server" /><p/>
    <asp:HyperLink ID="LinkToGasTracker" NavigateUrl="./GasTracker.aspx" Text="Gas Tracker" Target="_self" runat="server" /><p/>
    </div>

    <div></div>
    <asp:LinkButton ID="SignOut" OnClick="Signout_Click" Text="Sign Out" runat="server" /><p/>
    </form>
</body>
</html>
