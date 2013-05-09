using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebAppProject.App_DataSrc;

using System.IO;

namespace WebAppProject
{
    public partial class PurchaseDetails :  SignedInBasePage
    {
        private const string RegExpr_NNNN_nn = @"^([0-9]){1,4}(\.+[0-9][0-9]?)?";
        private const string Miles = "Miles"; // common
        private const string Gallons = "Gallons";

        private GradeUiMapping _gradeUiMapping;
        int _purchaseID = -1;

        new protected void Page_Load(object sender, EventArgs e)
        {
            Page.DataBind();
            _gradeUiMapping = new GradeUiMapping();

            string purchaseIdString = Request["ID"];

            if (!int.TryParse(purchaseIdString, out _purchaseID))
            {
                Response.Redirect(Pages.InfoPage + "?ID=" + InfoMessages.PurchaseNotFound.ToString());
            }

            if (!IsPostBack)
            {
                _gradeUiMapping.PopulateGradeDropDownList(DropDownListGrade);

                GasPurchase purchase = GasPurchasesDBRetriever.GetPurchase(Context.User.Identity.Name, _purchaseID);
                if (purchase == null)
                {
                    Response.Redirect(Pages.InfoPage + "?ID=" + InfoMessages.PurchaseNotFound.ToString());
                }

                Calendar.SelectedDate = purchase.When;
                Calendar.VisibleDate = Calendar.SelectedDate;
                PriceTextBox.Text = purchase.Price.ToString();
                AmountTextBox.Text = purchase.Amount.ToString();
                DistanceTextBox.Text = purchase.Distance.ToString();

                int gradeIndex = _gradeUiMapping.GradeToIndex(purchase.GradeOfFuel);
                DropDownListGrade.SelectedIndex = gradeIndex;
                NoteTextBox.Text = purchase.Note;

            }

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


        // Common???
        public void HomePage_Click(object sender, EventArgs e)
        {
            Response.Redirect(Pages.HomePage);
        }

        public void GasTracker_Click(object sender, EventArgs e)
        {
            Response.Redirect(Pages.GasTracker);
        }

        // This page:
        public void UpdateButton_Click(object sender, EventArgs e)
        {
            decimal price = 0;
            if (!decimal.TryParse(PriceTextBox.Text, out price))
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

            if (KmMileDropDownList.SelectedValue == Miles)
                distance = (int)(distance * 1.6F);

            if (LiterGallonDropDownList.SelectedValue == Gallons)
                amount = (int)(amount * 3.7854F);

            int grade = (int)_gradeUiMapping.IndexToGrade(DropDownListGrade.SelectedIndex);

            if (DB.UpdatePurchase(Context.User.Identity.Name, price, amount, distance, grade, Calendar.SelectedDate, NoteTextBox.Text, _purchaseID))
            {
                Response.Redirect(Pages.InfoPage + "?ID=" + InfoMessages.PurchaseWasSuccessfullyModified.ToString()); 
            }
            else
            {
                Response.Redirect(Pages.InfoPage + "?ID=" + InfoMessages.PurchaseWasNotModified.ToString());
            }
        }

        public void DeletButton_Click(object sender, EventArgs e)
        {
            if (DB.DeletePurchase(Context.User.Identity.Name, _purchaseID))
            {
                Response.Redirect(Pages.InfoPage + "?ID=" + InfoMessages.PurchaseWasSuccessfullyDeleted.ToString());
            }
            else
            {
                Response.Redirect(Pages.InfoPage + "?ID=" + InfoMessages.PurchaseWasNotModified.ToString());
            }
        }

        private void ShowError(string message)
        {
            ErrorLabel.Visible = true;
            ErrorLabel.Text = message;
        }
    }
}