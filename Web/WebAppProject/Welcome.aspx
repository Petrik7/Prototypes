<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Welcome.aspx.cs" Inherits="WebAppProject.Welcome" %>

<html>
<head>
    <!-- To scale page on mobile browsers -->
    <meta name="HandheldFriendly" content="true" />
    <meta name="viewport" content="width=device-width,initial-scale=1.0,maximum-scale=5.0;" />

  <title>Forms Authentication - Default Page</title>
</head>


<script runat="server">
  void Page_Load(object sender, EventArgs e)
  {
      LabelWelcome.Text = "Hello, " + Context.User.Identity.Name;
    LabelPiznaesh.Text = "Quuu?";
  }

</script>

<body>
  <h3>
    Using Forms Authentication</h3>
  <asp:Label ID="LabelWelcome" runat="server" /><p/>
  <asp:Label ID="LabelPiznaesh" runat="server" />
  <form id="Form1" runat="server">
    <asp:Image ID="Image1" runat="server" 
        src="Picture1.jpg" alt="Dog?"/><p/>

    <div></div>
    <asp:LinkButton ID="SignOut" OnClick="Signout_Click" Text="Sign Out" runat="server" /><p/>
  </form>
</body>
</html>
