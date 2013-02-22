<%@ Page Language="C#" %>
<html>
<head>
  <title>Forms Authentication - Default Page</title>
</head>

<!-- To scale page on mobile browsers -->
<meta name="HandheldFriendly" content="true" />
<meta name="viewport" content="width=device-width,initial-scale=1.0,maximum-scale=5.0;" />

<script runat="server">
  void Page_Load(object sender, EventArgs e)
  {
    Welcome.Text = "Hello, " + Context.User.Identity.Name;
    LabelPiznaesh.Text = "Quuu?";
  }

  void Signout_Click(object sender, EventArgs e)
  {
    FormsAuthentication.SignOut();
    Response.Redirect("MyLogon.aspx");
  }
</script>

<body>
  <h3>
    Using Forms Authentication</h3>
  <asp:Label ID="Welcome" runat="server" /><p/>
  <asp:Label ID="LabelPiznaesh" runat="server" />
  <form id="Form1" runat="server">
    <asp:Image ID="Image1" runat="server" 
        src="Picture1.jpg" alt="Dog?"/><p/>

    <div></div>
    <asp:Button ID="SignOut" OnClick="Signout_Click" Text="Sign Out" runat="server" /><p/>
  </form>
</body>
</html>
