using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

namespace WebAppProject
{
    public partial class AddPurchase_Click : SignedInBasePage //System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Label labelWelcome = (Label)Page.FindControl("LabelWelcome");
            labelWelcome.Text = "Hi, " + Context.User.Identity.Name + "!";
        }

        protected void InsertButton_Click(object sender, EventArgs e)
        {
            //if (String.IsNullOrEmpty(FirstNameTextBox.Text) ||
            //   String.IsNullOrEmpty(LastNameTextBox.Text)) { return; }

            //int employeeID = EmployeeList[EmployeeList.Count - 1].EmployeeID + 1;

            //string lastName = Server.HtmlEncode(FirstNameTextBox.Text);
            //string firstName = Server.HtmlEncode(LastNameTextBox.Text);

            //FirstNameTextBox.Text = String.Empty;
            //LastNameTextBox.Text = String.Empty;

            //EmployeeList.Add(new Employee(employeeID, lastName, firstName));
            //ViewState["EmployeeList"] = EmployeeList;

            //EmployeesGridView.DataBind();
            //EmployeesGridView.PageIndex = EmployeesGridView.PageCount;
        }

        protected void CancelButton_Click(object sender, EventArgs e)
        {
            AmountTextBox.Text = String.Empty;
            PriceTextBox.Text = String.Empty;
            DistanceTextBox.Text = String.Empty;
        }

        public void Signout_Click(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            Response.Redirect("MyLogon.aspx");
        }

        public void Directory_Click(object sender, EventArgs e)
        {
            Response.Redirect("Directory.aspx");
        }

        public void GasTracker_Click(object sender, EventArgs e)
        {
            Response.Redirect("GasTracker.aspx");
        }
    }
}