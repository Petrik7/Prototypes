using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebAppProject
{
    public partial class PurchaseDetails :  SignedInBasePage
    {
        private ThreeDropListCalendar _threeDropListCalendar = null;

        new protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                _threeDropListCalendar = new ThreeDropListCalendar(MonthDropList, DateDropList, YearDropList);
            }
        }


        // Common???
        public void HomePage_Click(object sender, EventArgs e)
        {
            Response.Redirect("HomePage.aspx");
        }

        public void GasTracker_Click(object sender, EventArgs e)
        {
            Response.Redirect("GasTracker.aspx");
        }

        // This page:
        public void UpdateButton_Click(object sender, EventArgs e)
        {
            // TODO:
        }

        public void DeletButton_Click(object sender, EventArgs e)
        {
            // TODO:
        }
    }
}