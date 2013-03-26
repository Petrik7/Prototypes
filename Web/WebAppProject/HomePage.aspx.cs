using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace WebAppProject
{
    public partial class HomePage : SignedInBasePage//System.Web.UI.Page
    {
        const string DateTimeFormat = "MMM d yyyy h:mm:ss tt";

        new protected void Page_Load(object sender, EventArgs e)
        {
            SayHello();
            ShowStatistics();
        }

        private void ShowStatistics()
        {
            DataTable accountsTable = DB.GetAccount( Context.User.Identity.Name);
            if(accountsTable == null)
                return;

            DataRow accountRow = accountsTable.Rows[0];

            Label memberSince = (Label)Page.FindControl("MemberSince");
            DateTime created = (DateTime)accountRow[DB.AccountTable.Created];
            memberSince.Text += created.ToLocalTime().ToString(DateTimeFormat);
             
            Label lastUpdated = (Label)Page.FindControl("LastUpdated");
            DateTime updated = (DateTime)accountRow[DB.AccountTable.Updated];
            lastUpdated.Text += updated.ToLocalTime().ToString(DateTimeFormat);
        }

    }
}