<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreateAccount.aspx.cs" Inherits="WebAppProject.CreateAccount" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        body {font-family:Calibri;}
    </style>
 </head>
<body>
    <form id="FormCreateAccount" runat="server">
     <table align="center" style="border-left: 3px solid PaleGoldenrod; border-right: 3px solid PaleGoldenrod;
                    border-top: 3px solid PaleGoldenrod; border-bottom: 3px solid PaleGoldenrod" >
          <tr style="background-color:Tan">
            <td colspan="3"><asp:Label text="New account" font-bold="true" runat="server"/></td>
          </tr>
          <tr>
            <td colspan="3" align="center">
                <asp:Label ID="ErrorOnThePage" runat="server" Text="There were errors on the page. " ForeColor="DarkRed" Visible = "false"/></td>
          </tr>
          <tr>
            <td colspan="3" align="center">
                <asp:Label ID="ServerSideValidation" runat="server" Text="" ForeColor="DarkRed" Visible = "false"/></td>
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
              <asp:RegularExpressionValidator ID="RegularExpression_UserNameValidator" 
                runat="server" ControlToValidate="UserEmail"
                ErrorMessage="Empty" Text="*" ForeColor="DarkRed"
                ValidationExpression="empty"/>
            </td>
          </tr>
          <tr>
            <td>Password:</td>
            <td><asp:TextBox ID="Password" TextMode="Password" runat="server" /></td>
            <td>
              <asp:RequiredFieldValidator ID="RequiredFieldValidator2" 
                ControlToValidate="Password"
                ErrorMessage="Password cannot be empty" ForeColor="DarkRed"
                runat="server" >*</asp:RequiredFieldValidator>
              <asp:RegularExpressionValidator ID="RegularExpression_PasswordValidator" 
                runat="server" ControlToValidate="Password"
                ErrorMessage="Empty" Text="*" ForeColor="DarkRed"
                ValidationExpression="empty"/>
            </td>
          </tr>
          <tr>
            <td>Confirm password:</td>
            <td><asp:TextBox ID="ConfirmPassword" TextMode="Password" runat="server" /></td>
            <td>
              <asp:RequiredFieldValidator ID="RequiredFieldValidator3" 
                ControlToValidate="ConfirmPassword"
                ErrorMessage="Password confirmation cannot be empty" 
                ForeColor="DarkRed"
                runat="server" >*</asp:RequiredFieldValidator>
              <asp:CompareValidator id="comparePasswords" 
                runat="server"
                ControlToCompare="Password"
                ControlToValidate="ConfirmPassword"
                ErrorMessage="Your passwords do not match up!"
                Text="*"
                Display="Dynamic" />
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
