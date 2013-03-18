using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebAppProject;
using WebAppProject.App_SrcCode;

namespace WebAppProject
{
    public partial class GasTracker : SignedInBasePage //System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Label labelLoadType = (Label)Page.FindControl("LabelLoadType");

            if (IsPostBack)
            {
                TextBox tb = (TextBox)Page.FindControl("UserEmail");
                labelLoadType.Text = "Updated by postback: ";
            }
            else
            {
                labelLoadType.Text = "Loaded: ";
                //SelectMilegeType(GasPurchaseProcessed.MilageType.LitersPerKm);
            }

            //UpdateRadioList();
            
            LoadPurchasesForMonth();
        }

        public void Signout_Click(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            Response.Redirect("MyLogon.aspx");
        }

        public void HomePage_Click(object sender, EventArgs e)
        {
            Response.Redirect("HomePage.aspx");
        }

        public void AddPurchase_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddPurchase.aspx");
        }

        protected void DropDownList_MilesKms_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadPurchasesForMonth();
        }

        #region Private
        private void LoadPurchasesForMonth()
        {
            IEnumerable<GasPurchaseProcessed> purchasesList = null;
            DateTime now = DateTime.Now;
            GasData gasData = new GasData(new HardCodedData());
            GasPurchaseProcessed.MilageType milegType = GasPurchaseProcessed.MilageType.LitersPerKm;
            milegType = GetMilageType();

            purchasesList = gasData.GetPurchasesForMonth(Context.User.Identity.Name, now.AddMonths(-1).Month, milegType);

            GridView gasPurchasesGreedView = (GridView)Page.FindControl("GridViewGasPurchases");
            if (gasPurchasesGreedView != null && purchasesList != null)
            {
                gasPurchasesGreedView.DataSource = purchasesList;
                gasPurchasesGreedView.DataBind();
            }
        }

        private GasPurchaseProcessed.MilageType GetMilageType()
        {
            DropDownList dropDownList_MilesKms = (DropDownList)Page.FindControl("DropDownList_MilesKms");
            if (dropDownList_MilesKms != null && dropDownList_MilesKms.SelectedIndex == 1)
                return GasPurchaseProcessed.MilageType.Miles;
            else
                return GasPurchaseProcessed.MilageType.LitersPerKm;
        }

        //private void SelectMilegeType(GasPurchaseProcessed.MilageType milageType)
        //{
        //    RadioButtonList RadioButtonList_MilesKms = (RadioButtonList)Page.FindControl("RadioButtonList_MilesKms");
        //    if (milageType == GasPurchaseProcessed.MilageType.LitersPerKm)
        //    {
        //        //RadioButtonList_MilesKms.SelectedIndex = 0;
        //        RadioButtonList_MilesKms.Items.FindByText("Liters/100 Kms").Selected = true;
        //        RadioButtonList_MilesKms.Items[1].Selected = false;
        //    }
        //}

        #endregion
    }
}
