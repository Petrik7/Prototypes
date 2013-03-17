using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebAppProject
{
    public partial class HomePage : SignedInBasePage//System.Web.UI.Page
    {
        new protected void Page_Load(object sender, EventArgs e)
        {
            SayHello();
        }
    }
}