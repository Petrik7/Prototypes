<%@ Page Language="C#" AutoEventWireup="true" CodeFile="HomePage.aspx.cs" Inherits="WebAppProject.HomePage" %>

<html>
<head>
    <!-- To scale page on mobile browsers -->
    <meta name="HandheldFriendly" content="true" />
    <meta name="viewport" content="width=device-width,initial-scale=1.0,maximum-scale=5.0;" />

  <title>Home Page</title>
    <style type="text/css">
        body {font-family:Calibri;}
    </style>
</head>

<%--<script runat="server">
  void Page_Load(object sender, EventArgs e)
  {}
</script>--%>

<body>
  <form id="Form1" runat="server">
    <table align="center">
        <%--Row 1--%>
        <tr>
            <td><asp:Label ID="LabelHello" runat="server" /></td>
            <td><asp:LinkButton ID="SignOut" OnClick="Signout_Click" Text="Sign Out" runat="server" /><p/></td>
        </tr>
        <%--Row 2--%>
        <tr>
            <td><asp:Label ID="MemberSince" cellpadding="3" Text="Member since : " runat="server" Font-Bold="false"/></td>
        </tr>
        <tr>
            <td><asp:Label ID="LastUpdated" cellpadding="3" Text="Last updated   : " runat="server" Font-Bold="false"/></td>
        </tr>
        <tr>
            <td><asp:Label ID="LastLogin" cellpadding="3" Text="Last login   : " runat="server" Font-Bold="false"/></td>
        </tr>
        <%--Row 3--%>
        <tr>
            <td><asp:Image ID="Image1" runat="server" src="Picture1.jpg" alt="Dog?"/></td>
        </tr>
        <%--Row 4--%>
        <tr>
            <td><asp:HyperLink ID="LinkToDirectory" NavigateUrl="./Directory.aspx" Text="Directory" Target="_self" runat="server" /></td>
        </tr>
        <%--Row 5--%>
        <tr>
            <td><asp:HyperLink ID="LinkToGasTracker" NavigateUrl="./GasTracker.aspx" Text="Gas Tracker" Target="_self" runat="server" /></td>
        </tr>
    </table>

  </form>
</body>
</html>
