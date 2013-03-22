using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebAppProject;

public partial class MyLogon : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        //DB db = new DB();
        //DB.AddAccount("al123456789012345678901234567890", "12345678901234567890123456789012");
    }

    protected void CreateAccount_Click(object sender, EventArgs e)
    {
        Response.Redirect("CreateAccount.aspx");
    }
}