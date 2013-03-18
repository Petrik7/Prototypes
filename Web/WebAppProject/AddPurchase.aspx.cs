using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Globalization;
using System.Threading;
using System.Resources;

namespace WebAppProject
{
    public partial class AddPurchase_Click : SignedInBasePage //System.Web.UI.Page
    {
        private const string RegExpr_NNNN_nn = @"^([0-9]){1,4}(\.+[0-9][0-9]?)?";

        protected void Page_Load(object sender, EventArgs e)
        {
            //Thread.CurrentThread.CurrentUICulture = new CultureInfo("en");

            RegularExpressionValidator priceValidator = (RegularExpressionValidator)Page.FindControl("RegularExpression_PriceValidator");
            if (priceValidator != null)
                priceValidator.ValidationExpression = RegExpr_NNNN_nn;
            RegularExpressionValidator amountValidator = (RegularExpressionValidator)Page.FindControl("RegularExpression_AmountValidator");
            if (amountValidator != null)
                amountValidator.ValidationExpression = RegExpr_NNNN_nn;
            RegularExpressionValidator distanceValidator = (RegularExpressionValidator)Page.FindControl("RegularExpression_DistanceValidator");
            if (distanceValidator != null)
                distanceValidator.ValidationExpression = RegExpr_NNNN_nn;
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

        public void HomePage_Click(object sender, EventArgs e)
        {
            Response.Redirect("HomePage.aspx");
        }

        public void GasTracker_Click(object sender, EventArgs e)
        {
            Response.Redirect("GasTracker.aspx");
        }
    }
}