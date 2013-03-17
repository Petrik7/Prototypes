using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebAppProject
{
    public partial class SignedInBasePage : System.Web.UI.Page
    {
        protected override void OnLoad(EventArgs e)
        {
            // ... add custom logic here ...

            // Be sure to call the base class's OnLoad method!
            base.OnLoad(e);
        }

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void SayHello()
        {
            Label labelWelcome = (Label)Page.FindControl("LabelHello");
            labelWelcome.Text = "Hi, " + Context.User.Identity.Name + "!";
        }

        public void Signout_Click(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            Response.Redirect("MyLogon.aspx");
        }

        public void Signin_Click(object sender, EventArgs e)
        {
            Response.Redirect("MyLogon.aspx");
        }
    }
}