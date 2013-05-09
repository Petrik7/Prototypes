using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebAppProject
{
    public partial class InfoPage : SignedInBasePage
    {
        private const string UnknownErrorText = "Oups.. Unknown Error";

        SortedDictionary<int, string> _errors;

        new protected void Page_Load(object sender, EventArgs e)
        {
            _errors = new SortedDictionary<int, string>();
            _errors.Add(InfoMessages.PurchaseNotFound, "Sorry your purchase is not found.");
            _errors.Add(InfoMessages.PurchaseWasSuccessfullyModified, "Purchase was successfully modified");
            _errors.Add(InfoMessages.PurchaseWasNotModified, "Ooops.. Error. Purchase was not modified");
            _errors.Add(InfoMessages.PurchaseWasSuccessfullyDeleted, "Purchase was successfully deleted");

            string errorIdString = Request["ID"];
            int errorId = -1;
            
            if (int.TryParse(errorIdString, out errorId) )
            {
                string errorText = string.Empty;
                Label errorTextLabel = (Label)Page.FindControl("ErrorTextLabel");
                if (errorTextLabel == null)
                    return;
                if(_errors.TryGetValue(errorId, out errorText))
                    errorTextLabel.Text = errorText;
                else
                    errorTextLabel.Text = UnknownErrorText;
            }
        }

        public void HomePage_Click(object sender, EventArgs e)
        {
            Response.Redirect(Pages.HomePage);
        }

        public void GasTracker_Click(object sender, EventArgs e)
        {
            Response.Redirect(Pages.GasTracker);
        }
    }
}