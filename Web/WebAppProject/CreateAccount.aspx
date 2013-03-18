<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreateAccount.aspx.cs" Inherits="WebAppProject.CreateAccount" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="FormCreateAccount" runat="server">
     <table align="center" style="border-left: 3px solid PaleGoldenrod; border-right: 3px solid PaleGoldenrod;
                    border-top: 3px solid PaleGoldenrod; border-bottom: 3px solid PaleGoldenrod" >
          <tr style="background-color:Tan">
            <td colspan="3"><asp:Label text="New account" font-bold="true" runat="server"/></td>
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
                ErrorMessage="User name or E-mail cannot be empty" ForeColor="DarkRed"
                runat="server" >*</asp:RequiredFieldValidator>
            </td>
          </tr>
          <tr>
            <td>Password:</td>
            <td><asp:TextBox ID="UserPass" TextMode="Password" runat="server" /></td>
            <td>
              <asp:RequiredFieldValidator ID="RequiredFieldValidator2" 
                ControlToValidate="UserPass"
                ErrorMessage="Password cannot be empty" ForeColor="DarkRed"
                runat="server" >*</asp:RequiredFieldValidator>
            </td>
          </tr>
          <tr>
            <td>Confirm password:</td>
            <td><asp:TextBox ID="ConfirmPassword" TextMode="Password" runat="server" /></td>
            <td>
              <asp:RequiredFieldValidator ID="RequiredFieldValidator3" 
                ControlToValidate="ConfirmPassword"
                ErrorMessage="Password confirmation cannot be empty" ForeColor="DarkRed"
                runat="server" >*</asp:RequiredFieldValidator>
            </td>
          </tr>
          <tr>
            <td>Remember me?</td>
            <td><asp:CheckBox ID="Persist" runat="server" /></td>
          </tr>
          <tr>
            <td><asp:Button ID="ButtonCreateAccount" OnClick="CreateAccount_Click" Text="Create new account" style="width:100%" runat="server" /></td>
          </tr>
        </table>
    </form>
</body>
</html>
