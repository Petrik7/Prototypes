<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MyLogon.aspx.cs" Inherits="MyLogon" %>

<%@ Import Namespace="System.Web.Security" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<!-- To scale page on mobile browsers -->
<meta name="HandheldFriendly" content="true" />
<meta name="viewport" content="width=device-width,initial-scale=1.0,maximum-scale=5.0;" />

<script runat="server">
  void Logon_Click(object sender, EventArgs e)
  {
    if ((UserEmail.Text == "mas") && 
            (UserPass.Text == "mmm"))
      {
          FormsAuthentication.RedirectFromLoginPage 
             (UserEmail.Text, Persist.Checked);
      }
      else
      {
          Msg.Text = "Invalid credentials. Please try again.";
      }
  }
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
  <title>Forms Authentication - Login</title>
</head>
<body>
  <form id="form1" runat="server">
    <h3 align="center">
      Logon Page</h3>
    <table align="center">
      <tr>
        <td>
          E-mail address:</td>
        <td>
          <asp:TextBox ID="UserEmail" runat="server" /></td>
        <td>
          <asp:RequiredFieldValidator ID="RequiredFieldValidator1" 
            ControlToValidate="UserEmail"
            Display="Dynamic" 
            ErrorMessage="Cannot be empty." 
            runat="server" />
        </td>
      </tr>
      <tr>
        <td>
          Password:</td>
        <td>
          <asp:TextBox ID="UserPass" TextMode="Password" 
             runat="server" />
        </td>
        <td>
          <asp:RequiredFieldValidator ID="RequiredFieldValidator2" 
            ControlToValidate="UserPass"
            ErrorMessage="Cannot be empty." 
            runat="server" />
        </td>
      </tr>
      <tr>
        <td>
          Remember me?</td>
        <td>
          <asp:CheckBox ID="Persist" runat="server" /></td>
      </tr>
      <tr>
        <td>
            <asp:Button ID="ButtonLogOn" OnClick="Logon_Click" Text="Log On" runat="server" /></td>
      </tr>
    </table>

    <p>
      <asp:Label ID="Msg" ForeColor="red" runat="server" />
    </p>
  </form>
</body>
</html>
