<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MyLogon.aspx.cs" Inherits="MyLogon" %>

<%@ Import Namespace="System.Web.Security" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">

</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <!-- To scale page on mobile browsers -->
    <meta name="HandheldFriendly" content="true" />
    <meta name="viewport" content="width=device-width,initial-scale=1.0,maximum-scale=5.0;" />

  <title>Forms Authentication - Sign in</title>
</head>
<body>
  <form id="form1" runat="server">

    <table align="center" style="border-left: 3px solid PaleGoldenrod; border-right: 3px solid PaleGoldenrod;
                    border-top: 3px solid PaleGoldenrod; border-bottom: 3px solid PaleGoldenrod">
      <tr style="background-color:Tan">
        <td colspan="3"><asp:Label Text="Sign in" font-bold="true" runat="server"></asp:Label></td>
      </tr>
      <tr>
        <td colspan="3" align="center"><asp:ValidationSummary ID="ValidationSummary1" runat="server" 
                                HeaderText="There were errors on the page:" ForeColor="DarkRed"/></td>
      </tr>
      <tr>
        <td>User name or E-mail:</td>
        <td><asp:TextBox ID="UserEmail" runat="server" /></td>
        <td>
          <asp:RequiredFieldValidator ID="RequiredFieldValidator1" 
            ControlToValidate="UserEmail"
            Display="Dynamic" 
            ErrorMessage="User name or E-mail cannot be empty." ForeColor="DarkRed"
            runat="server" >*</asp:RequiredFieldValidator>
        </td>
      </tr>
      <tr>
        <td>Password:</td>
        <td><asp:TextBox ID="UserPassword" TextMode="Password" runat="server" /></td>
        <td>
          <asp:RequiredFieldValidator ID="RequiredFieldValidator2" 
            ControlToValidate="UserPassword"
            ErrorMessage="Password cannot be empty." ForeColor="DarkRed"
            runat="server" >*</asp:RequiredFieldValidator>
        </td>
      </tr>
      <tr>
        <td>Remember me?</td>
        <td><asp:CheckBox ID="Persist" runat="server" /></td>
      </tr>
      <tr>
        <td><asp:Button ID="ButtonLogOn" OnClick="Logon_Click" Text="Log On" runat="server" /></td>
        <td><asp:Button ID="ButtonCreateAccount" OnClick="CreateAccount_Click" Text="Create new account" style="width:100%" CausesValidation="false" runat="server" /></td>
      </tr>
      <tr>
        <td colspan="3">
            <asp:Label ID="ErrorMessage" ForeColor="red" runat="server" /></td>
      </tr>
    </table>

  </form>
</body>
</html>
