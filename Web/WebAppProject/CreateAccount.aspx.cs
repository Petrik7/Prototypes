using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Data.SqlClient;

namespace WebAppProject
{
    public partial class CreateAccount : System.Web.UI.Page
    {
        const int MIN_PASSWORD_LENGHT = 3;
        const int MIN_USERNAME_LENGHT = 4;

        protected void Page_Load(object sender, EventArgs e)
        {
            RegularExpressionValidator passwordValidator = (RegularExpressionValidator)Page.FindControl("RegularExpression_PasswordValidator");
            if (passwordValidator != null)
            {
                passwordValidator.ValidationExpression = GetPasswordRegExp_MinX_MaxY(MIN_PASSWORD_LENGHT, DB.MAX_PASSWORD_LENGHT);
                passwordValidator.ErrorMessage = string.Format("Password must be {0} - {1} characters long", MIN_PASSWORD_LENGHT, DB.MAX_PASSWORD_LENGHT);
            }

            RegularExpressionValidator userNameValidator = (RegularExpressionValidator)Page.FindControl("RegularExpression_UserNameValidator");
            if (userNameValidator != null)
            {
                userNameValidator.ValidationExpression = GetUserNameRegExp_MinX_MaxY(MIN_USERNAME_LENGHT, DB.MAX_USERNAME_LENGHT);
                userNameValidator.ErrorMessage = string.Format("User name must be {0} - {1} characters long", MIN_USERNAME_LENGHT, DB.MAX_USERNAME_LENGHT);
            }

            Label errorOnThePage = (Label)Page.FindControl("ErrorOnThePage");
            errorOnThePage.Visible = false;
            Label serverSideValidation = (Label)Page.FindControl("ServerSideValidation");
            serverSideValidation.Visible = false;
        }

        protected void CreateAccount_Click(object sender, EventArgs e)
        {
            TextBox userName = (TextBox)Page.FindControl("UserEmail");
            TextBox password = (TextBox)Page.FindControl("Password");
            TextBox passwordConfirmation = (TextBox)Page.FindControl("ConfirmPassword");
            string validationError = string.Empty;

            if (!PassedValidation(userName.Text, password.Text, passwordConfirmation.Text, out validationError))
                ShowServerError(validationError);

            try
            {
                DB.AddAccount(userName.Text, password.Text);
                CheckBox persist = (CheckBox)Page.FindControl("Persist");
                FormsAuthentication.RedirectFromLoginPage(userName.Text, persist.Checked);
            }
            catch (SqlException ex)
            {
                if (ex.Message.Contains("duplicate"))
                    validationError = "User name is already taken, please try another one";
                else
                    validationError = "Error while creating new account, please try again";
                ShowServerError(validationError);
            }
        }

        private void ShowServerError(string validationError)
        {
            Label errorOnThePage = (Label)Page.FindControl("ErrorOnThePage");
            errorOnThePage.Visible = true;
            Label serverSideValidation = (Label)Page.FindControl("ServerSideValidation");
            serverSideValidation.Text += validationError;
            serverSideValidation.Visible = true;
        }

        private bool PassedValidation(string userName, string password, string passwordConfirmation, out string errorMessage)
        {
            if (userName.Length > DB.MAX_USERNAME_LENGHT || userName.Length < MIN_USERNAME_LENGHT)
            {
                errorMessage = string.Format("User name must be {0} - {1} characters long", MIN_USERNAME_LENGHT, DB.MAX_USERNAME_LENGHT);;
                return false;
            }

            if(string.Equals(userName, "admin", StringComparison.InvariantCultureIgnoreCase))
            {
                errorMessage = @"Your cannot use 'admin' as user name.Please try different user name";
                return false;
            }

            if (password.Length > DB.MAX_PASSWORD_LENGHT || password.Length < MIN_PASSWORD_LENGHT)
            {
                errorMessage = string.Format("Password must be {0} - {1} characters long", MIN_PASSWORD_LENGHT, DB.MAX_PASSWORD_LENGHT); ;
                return false;
            }

            if (password != passwordConfirmation)
            {
                errorMessage = "Your passwords do not match up!";
                return false;
            }

            errorMessage = string.Empty;
            return true;
        }

        //      Validation regexps
        #region Validation regexps

        private string GetPasswordRegExp_MinX_MaxY(int minLenght, int maxLenght)
        {
            string regexp = @"^[!-~]{" + minLenght.ToString() + "," + maxLenght.ToString() + "}";
            return regexp;
        }

        private string GetUserNameRegExp_MinX_MaxY(int minLenght, int maxLenght)
        {
            string regexp = @"^[0-9a-zA-Z@._]{" + minLenght.ToString() + "," + maxLenght.ToString() + "}";
            return regexp;
        }

        #endregion Validation regexps
    }
}