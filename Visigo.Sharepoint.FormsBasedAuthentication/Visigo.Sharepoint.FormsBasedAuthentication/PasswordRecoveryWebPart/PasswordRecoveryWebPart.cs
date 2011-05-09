using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebPartPages;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.Security;
using System.Resources;
using System.Globalization;
using System.Reflection;
using Microsoft.SharePoint.Administration;
using Microsoft.SharePoint.Utilities;

namespace Visigo.Sharepoint.FormsBasedAuthentication
{
    public class PasswordRecoveryWebPart : Microsoft.SharePoint.WebPartPages.WebPart
    {
        #region Fields
        private ResourceManager _resourceManager;
        private System.Web.UI.WebControls.PasswordRecovery _ctlPasswordRecovery;

        private string _generalFailureText = string.Empty;
        private string _questionFailureText = string.Empty;
        private string _questionInstructionText = string.Empty;
        private string _questionLabelText = string.Empty;
        private string _questionTitleText = string.Empty;
        private string _submitButtonImageUrl = string.Empty;
        private string _submitButtonText = string.Empty;
        private ButtonType _submitButtonType = ButtonType.Button;
        private string _successText = string.Empty;
        private string _userNameFailureText = string.Empty;
        private string _userNameInstructionText = string.Empty;
        private string _userNameLabelText = string.Empty;
        private string _userNameTitleText = string.Empty;
        private string _answerRequiredErrorMessage = string.Empty;
        private string _userNameRequiredErrorMessage = string.Empty;
        #endregion

        #region Properties
        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [ResourcesAttribute("GeneralFailureText_FriendlyName", "GeneralFailureText_Category", "GeneralFailureText_Description")]
        public string GeneralFailureText
        {
            get { return _generalFailureText; }
            set { _generalFailureText = value; }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [ResourcesAttribute("QuestionFailureText_FriendlyName", "QuestionFailureText_Category", "QuestionFailureText_Description")]
        public string QuestionFailureText
        {
            get { return _questionFailureText; }
            set { _questionFailureText = value; }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [ResourcesAttribute("QuestionInstructionText_FriendlyName", "QuestionInstructionText_Category", "QuestionInstructionText_Description")]
        public string QuestionInstructionText
        {
            get { return _questionInstructionText; }
            set { _questionInstructionText = value; }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [ResourcesAttribute("QuestionLabelText_FriendlyName", "QuestionLabelText_Category", "QuestionLabelText_Description")]
        public string QuestionLabelText
        {
            get { return _questionLabelText; }
            set { _questionLabelText = value; }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [ResourcesAttribute("QuestionTitleText_FriendlyName", "QuestionTitleText_Category", "QuestionTitleText_Description")]
        public string QuestionTitleText
        {
            get { return _questionTitleText; }
            set { _questionTitleText = value; }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [ResourcesAttribute("SubmitButtonImageUrl_FriendlyName", "SubmitButtonImageUrl_Category", "SubmitButtonImageUrl_Description")]
        public string SubmitButtonImageUrl
        {
            get { return _submitButtonImageUrl; }
            set { _submitButtonImageUrl = value; }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [ResourcesAttribute("SubmitButtonText_FriendlyName", "SubmitButtonText_Category", "SubmitButtonText_Description")]
        public string SubmitButtonText
        {
            get { return _submitButtonText; }
            set { _submitButtonText = value; }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [ResourcesAttribute("SubmitButtonType_FriendlyName", "SubmitButtonType_Category", "SubmitButtonType_Description")]
        public ButtonType SubmitButtonType
        {
            get { return _submitButtonType; }
            set { _submitButtonType = value; }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [ResourcesAttribute("SuccessText_FriendlyName", "SuccessText_Category", "SuccessText_Description")]
        public string SuccessText
        {
            get { return _successText; }
            set { _successText = value; }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [ResourcesAttribute("UserNameFailureText_FriendlyName", "UserNameFailureText_Category", "UserNameFailureText_Description")]
        public string UserNameFailureText
        {
            get { return _userNameFailureText; }
            set { _userNameFailureText = value; }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [ResourcesAttribute("UserNameInstructionText_FriendlyName", "UserNameInstructionText_Category", "UserNameInstructionText_Description")]
        public string UserNameInstructionText
        {
            get { return _userNameInstructionText; }
            set { _userNameInstructionText = value; }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [ResourcesAttribute("UserNameLabelText_FriendlyName", "UserNameLabelText_Category", "UserNameLabelText_Description")]
        public string UserNameLabelText
        {
            get { return _userNameLabelText; }
            set { _userNameLabelText = value; }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [ResourcesAttribute("UserNameTitleText_FriendlyName", "UserNameTitleText_Category", "UserNameTitleText_Description")]
        public string UserNameTitleText
        {
            get { return _userNameTitleText; }
            set { _userNameTitleText = value; }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [ResourcesAttribute("AnswerRequiredErrorMessage_FriendlyName", "AnswerRequiredErrorMessage_Category", "AnswerRequiredErrorMessage_Description")]
        public string AnswerRequiredErrorMessage
        {
            get { return _answerRequiredErrorMessage; }
            set { _answerRequiredErrorMessage = value; }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [ResourcesAttribute("UserNameRequiredErrorMessage_FriendlyName", "UserNameRequiredErrorMessage_Category", "UserNameRequiredErrorMessage_Description")]
        public string UserNameRequiredErrorMessage
        {
            get { return _userNameRequiredErrorMessage; }
            set { _userNameRequiredErrorMessage = value; }
        }
        #endregion

        #region Constructors
        public PasswordRecoveryWebPart()
        {
            _resourceManager = new ResourceManager("Visigo.Sharepoint.FormsBasedAuthentication.PasswordRecoveryWebPart", Assembly.GetExecutingAssembly());
            try
            {
                GeneralFailureText = _resourceManager.GetString("GeneralFailureText_DefaultValue");
                QuestionFailureText = _resourceManager.GetString("QuestionFailureText_DefaultValue");
                QuestionInstructionText = _resourceManager.GetString("QuestionInstructionText_DefaultValue");
                QuestionLabelText = _resourceManager.GetString("QuestionLabelText_DefaultValue");
                QuestionTitleText = _resourceManager.GetString("QuestionTitleText_DefaultValue");
                SubmitButtonImageUrl = _resourceManager.GetString("SubmitButtonImageUrl_DefaultValue");
                SubmitButtonText = _resourceManager.GetString("SubmitButtonText_DefaultValue");
                SubmitButtonType = (ButtonType)Convert.ToInt32(_resourceManager.GetString("SubmitButtonType_DefaultValue"), CultureInfo.InvariantCulture);
                SuccessText = _resourceManager.GetString("SuccessText_DefaultValue");
                UserNameFailureText = _resourceManager.GetString("UserNameFailureText_DefaultValue");
                UserNameInstructionText = _resourceManager.GetString("UserNameInstructionText_DefaultValue");
                UserNameLabelText = _resourceManager.GetString("UserNameLabelText_DefaultValue");
                UserNameTitleText = _resourceManager.GetString("UserNameTitleText_DefaultValue");
                AnswerRequiredErrorMessage = _resourceManager.GetString("AnswerRequiredErrorMessage_DefaultValue");
                UserNameRequiredErrorMessage = _resourceManager.GetString("UserNameRequiredErrorMessage_DefaultValue");
            }
            catch (Exception ex)
            {
                Utils.LogError(ex);
            }
        }
        #endregion

        #region Methods
        private void AddPasswordRecoveryControl()
        {


            /* bms I couldn't figure out how to set the smtp server from code so I added the SendMailError as a hack for now */

            _ctlPasswordRecovery = new System.Web.UI.WebControls.PasswordRecovery();
            //bms Added the event to catch the error and send our own email
            _ctlPasswordRecovery.SendMailError += new SendMailErrorEventHandler(_ctlPasswordRecovery_SendMailError);
            _ctlPasswordRecovery.MembershipProvider = Utils.GetMembershipProvider(Context);
            _ctlPasswordRecovery.GeneralFailureText = GeneralFailureText;
            _ctlPasswordRecovery.QuestionFailureText = QuestionFailureText;
            _ctlPasswordRecovery.QuestionInstructionText = QuestionInstructionText;
            _ctlPasswordRecovery.QuestionLabelText = QuestionLabelText;
            _ctlPasswordRecovery.QuestionTitleText = QuestionTitleText;
            _ctlPasswordRecovery.SubmitButtonImageUrl = SubmitButtonImageUrl;
            _ctlPasswordRecovery.SubmitButtonText = SubmitButtonText;
            _ctlPasswordRecovery.SubmitButtonType = SubmitButtonType;
            _ctlPasswordRecovery.SuccessText = SuccessText;
            _ctlPasswordRecovery.UserNameFailureText = UserNameFailureText;
            _ctlPasswordRecovery.UserNameInstructionText = UserNameInstructionText;
            _ctlPasswordRecovery.UserNameLabelText = UserNameLabelText;
            _ctlPasswordRecovery.UserNameTitleText = UserNameTitleText;
            _ctlPasswordRecovery.AnswerRequiredErrorMessage = AnswerRequiredErrorMessage;
            _ctlPasswordRecovery.UserNameRequiredErrorMessage = UserNameRequiredErrorMessage;

            this.Controls.Add(_ctlPasswordRecovery);
        }

        void _ctlPasswordRecovery_SendMailError(object sender, SendMailErrorEventArgs e)
        {
            using (SPWeb _web = new SPSite(this.Page.Request.Url.ToString()).OpenWeb())
            {
                PasswordRecovery prc = (PasswordRecovery)sender;
                MembershipUser currentUser = Utils.GetUser(prc.UserName, _web.Site);
                MembershipRequest membershipitem = new MembershipRequest();
                membershipitem.UserEmail = currentUser.Email;
                membershipitem.UserName = currentUser.UserName;
                membershipitem.SiteName = _web.Title;
                membershipitem.SiteURL = _web.Url;
                membershipitem.PasswordQuestion = currentUser.PasswordQuestion;
                membershipitem.Password = currentUser.ResetPassword(prc.Answer);

                /* These are the possible set of URLs that are provided to the user and developer in the XSLT */
                MembershipSettings settings = new MembershipSettings(_web);
                membershipitem.ChangePasswordURL = string.Format("{0}/{1}", _web.Url, settings.ChangePasswordPage);
                membershipitem.PasswordQuestionURL = string.Format("{0}/{1}", _web.Url, settings.PasswordQuestionPage);
                membershipitem.ThankYouURL = string.Format("{0}/{1}", _web.Url, settings.ThankYouPage);

                if (!MembershipRequest.SendPasswordResetEmail(membershipitem, _web))
                {
                    prc.SuccessText = "There was an error sending the email, please check with your administrator";
                }
                e.Handled = true;
            }
        }

        protected override void CreateChildControls()
        {
            AddPasswordRecoveryControl();
            base.CreateChildControls();
        }

        protected override void RenderContents(HtmlTextWriter writer)
        {
            EnsureChildControls();
            base.RenderContents(writer);
        }

        public override string LoadResource(string id)
        {
            return (_resourceManager.GetString(id));
        }
        #endregion
    }
}
