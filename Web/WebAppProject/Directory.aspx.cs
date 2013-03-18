using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Diagnostics;

namespace WebAppProject
{
    public partial class Directory : SignedInBasePage
    {
        public Directory()
        { }

        new protected void Page_Load(object sender, EventArgs e)
        {
            LinkButton buttonToHide = null;

            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated &&
                !string.IsNullOrEmpty(HttpContext.Current.User.Identity.Name))
            {
                buttonToHide = (LinkButton)Page.FindControl("SignIn");
            }
            else
            {
                buttonToHide = (LinkButton)Page.FindControl("SignOut");
            }

            Debug.Assert(buttonToHide != null);
            buttonToHide.Visible = false;
        }
    }
}