using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebAppProject;
using System.Web.Security;
using System.Data;

public partial class MyLogon : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected void CreateAccount_Click(object sender, EventArgs e)
    {
        Response.Redirect("CreateAccount.aspx");
    }

    protected void Logon_Click(object sender, EventArgs e)
    {
        TextBox userName = (TextBox)Page.FindControl("UserEmail");
        TextBox password = (TextBox)Page.FindControl("UserPassword");

        if (DB.AccessIsAllowed(userName.Text, password.Text))
        {
            CheckBox persist = (CheckBox)Page.FindControl("Persist");
            FormsAuthentication.RedirectFromLoginPage
               (userName.Text, persist.Checked);
        }
        else
        {
            Label errorMessage = (Label)Page.FindControl("ErrorMessage");
            errorMessage.Text = "Invalid credentials. Please try again.";
        }
    }
}