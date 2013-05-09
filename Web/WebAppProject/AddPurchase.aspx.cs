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
using WebAppProject.App_DataSrc;

namespace WebAppProject
{
    public partial class AddPurchase_Click : SignedInBasePage //System.Web.UI.Page
    {
        private const string RegExpr_NNNN_nn = @"^([0-9]){1,4}(\.+[0-9][0-9]?)?";
        private const string Miles = "Miles"; // common
        private const string Gallons = "Gallons";
        private const int PurchaseNoteMaxLen = 64;

        private SortedDictionary<int, KeyValuePair<GasPurchase.Grade, string>> _indexGrade = new SortedDictionary<int, KeyValuePair<GasPurchase.Grade, string> >();

        protected new void Page_Load(object sender, EventArgs e)
        {
            //Thread.CurrentThread.CurrentUICulture = new CultureInfo("en");
            ErrorLabel.Visible = false;
            //Calendar.SelectedDate = DateTime.Now;
            InitGradeMap();
            if (!IsPostBack)
            {
                Calendar.SelectedDate = DateTime.Today;
                Calendar.VisibleDate = Calendar.SelectedDate;
                PopulateGradeDropDownList();
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

            Page.DataBind();
        }

        private void InitGradeMap()
        {
            int index = 0;
            _indexGrade.Add(index++, new KeyValuePair<GasPurchase.Grade, string>(GasPurchase.Grade.MidGrade, "Mid-grade(89)")); // Will be selected By Default
            _indexGrade.Add(index++, new KeyValuePair<GasPurchase.Grade, string>(GasPurchase.Grade.Regular, "Regular (87)"));
            _indexGrade.Add(index++, new KeyValuePair<GasPurchase.Grade, string>(GasPurchase.Grade.Premium, "Premium (92)"));
            _indexGrade.Add(index++, new KeyValuePair<GasPurchase.Grade, string>(GasPurchase.Grade.Diesel, "Diesel"));
        }

        private void PopulateGradeDropDownList()
        {
            foreach(KeyValuePair<GasPurchase.Grade, string> grade in _indexGrade.Values)
                DropDownListGrade.Items.Add(grade.Value);
        }

        protected void InsertButton_Click(object sender, EventArgs e)
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

            int grade = (int)(_indexGrade[DropDownListGrade.SelectedIndex].Key);

            if (DB.AddPurchase(Context.User.Identity.Name, price, amount, distance, grade, Calendar.SelectedDate, NoteTextBox.Text))
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
            Response.Redirect(Pages.HomePage);
        }

        public void GasTracker_Click(object sender, EventArgs e)
        {
            Response.Redirect(Pages.GasTracker);
        }

        private void ShowError(string message)
        {
            ErrorLabel.Visible = true;
            ErrorLabel.Text = message;
        }
    }
}