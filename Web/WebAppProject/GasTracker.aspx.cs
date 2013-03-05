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
            Label labelWelcome = (Label)Page.FindControl("LabelWelcome");
            labelWelcome.Text = "Hi, " + Context.User.Identity.Name + "!";

            Label labelInfo = (Label)Page.FindControl("LabelInfo");

            if (IsPostBack)
            {
                labelInfo.Text = "Page_Load Postback...";
            }
            else
            {
                labelInfo.Text = "Page_Load Initial...";
                SelectMilegeType(GasPurchaseProcessed.MilageType.LitersPerKm);
            }

            LoadPurchasesForMonth();
        }

        public void Signout_Click(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            Response.Redirect("MyLogon.aspx");
        }

        protected void RadioButtonList_MilesKms_SelectedIndexChanged(object sender, EventArgs e)
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
            RadioButtonList radioButtonList_MilesKms = (RadioButtonList)Page.FindControl("RadioButtonList_MilesKms");
            if (radioButtonList_MilesKms != null && radioButtonList_MilesKms.SelectedIndex == 1)
            {
                milegType = GasPurchaseProcessed.MilageType.Miles;
            }

            purchasesList = gasData.GetPurchasesForMonth(Context.User.Identity.Name, now.AddMonths(-1).Month, milegType);

            GridView gasPurchasesGreedView = (GridView)Page.FindControl("GridViewGasPurchases");
            if (gasPurchasesGreedView != null && purchasesList != null)
            {
                gasPurchasesGreedView.DataSource = purchasesList;
                gasPurchasesGreedView.DataBind();
            }
        }

        private void SelectMilegeType(GasPurchaseProcessed.MilageType milageType)
        {
            RadioButtonList RadioButtonList_MilesKms = (RadioButtonList)Page.FindControl("RadioButtonList_MilesKms");
            if (milageType == GasPurchaseProcessed.MilageType.LitersPerKm)
            {
                RadioButtonList_MilesKms.SelectedIndex = 0;
                RadioButtonList_MilesKms.Items[1].Selected = false;
            }
            else
            {
                RadioButtonList_MilesKms.SelectedIndex = 1;
                RadioButtonList_MilesKms.Items[0].Selected = false;
            }
        }

        #endregion
    }
}
