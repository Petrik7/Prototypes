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

        protected new void Page_Load(object sender, EventArgs e)
        {
            //Thread.CurrentThread.CurrentUICulture = new CultureInfo("en");
            ErrorLabel.Visible = false;
            Calendar.SelectedDate = DateTime.Now;

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
            int price = 0;
            if (!int.TryParse(PriceTextBox.Text, out price))
            {
                ShowError("Error while adding your purchase. Invalid price format");
                return;
            }

            int amount = 0;
            if (!int.TryParse(AmountTextBox.Text, out amount))
            {
                ShowError("Error while adding your purchase. Invalid amount format");
                return;
            }

            int distance = 0;
            if (!int.TryParse(DistanceTextBox.Text, out distance))
            {
                ShowError("Error while adding your purchase. Invalid distance format");
                return;
            }

            if (DB.AddPurchase(Context.User.Identity.Name, price, amount, distance, Calendar.SelectedDate))
                Label1.Text = "Purchase has been added successfully. One more?";
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

        private void ShowError(string message)
        {
            ErrorLabel.Visible = true;
            ErrorLabel.Text = message;
        }
    }
}