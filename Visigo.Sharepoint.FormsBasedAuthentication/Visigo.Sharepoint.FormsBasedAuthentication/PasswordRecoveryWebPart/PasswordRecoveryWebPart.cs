using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI;
using Microsoft.SharePoint;
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
    public class PasswordRecoveryWebPart : System.Web.UI.WebControls.WebParts.WebPart
    {
        #region Fields
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
        [LocalizedWebDisplayName("FBAPackPasswordRecoveryWebPart", "GeneralFailureText_FriendlyName")]
        [LocalizedCategory("FBAPackPasswordRecoveryWebPart", "GeneralFailureText_Category")]
        [LocalizedWebDescription("FBAPackPasswordRecoveryWebPart", "GeneralFailureText_Description")]
        public string GeneralFailureText
        {
            get { return _generalFailureText; }
            set { _generalFailureText = value; }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [LocalizedWebDisplayName("FBAPackPasswordRecoveryWebPart", "QuestionFailureText_FriendlyName")]
        [LocalizedCategory("FBAPackPasswordRecoveryWebPart", "QuestionFailureText_Category")]
        [LocalizedWebDescription("FBAPackPasswordRecoveryWebPart", "QuestionFailureText_Description")]
        public string QuestionFailureText
        {
            get { return _questionFailureText; }
            set { _questionFailureText = value; }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [LocalizedWebDisplayName("FBAPackPasswordRecoveryWebPart", "QuestionInstructionText_FriendlyName")]
        [LocalizedCategory("FBAPackPasswordRecoveryWebPart", "QuestionInstructionText_Category")]
        [LocalizedWebDescription("FBAPackPasswordRecoveryWebPart", "QuestionInstructionText_Description")]
        public string QuestionInstructionText
        {
            get { return _questionInstructionText; }
            set { _questionInstructionText = value; }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [LocalizedWebDisplayName("FBAPackPasswordRecoveryWebPart", "QuestionLabelText_FriendlyName")]
        [LocalizedCategory("FBAPackPasswordRecoveryWebPart", "QuestionLabelText_Category")]
        [LocalizedWebDescription("FBAPackPasswordRecoveryWebPart", "QuestionLabelText_Description")]
        public string QuestionLabelText
        {
            get { return _questionLabelText; }
            set { _questionLabelText = value; }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [LocalizedWebDisplayName("FBAPackPasswordRecoveryWebPart", "QuestionTitleText_FriendlyName")]
        [LocalizedCategory("FBAPackPasswordRecoveryWebPart", "QuestionTitleText_Category")]
        [LocalizedWebDescription("FBAPackPasswordRecoveryWebPart", "QuestionTitleText_Description")]
        public string QuestionTitleText
        {
            get { return _questionTitleText; }
            set { _questionTitleText = value; }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [LocalizedWebDisplayName("FBAPackPasswordRecoveryWebPart", "SubmitButtonImageUrl_FriendlyName")]
        [LocalizedCategory("FBAPackPasswordRecoveryWebPart", "SubmitButtonImageUrl_Category")]
        [LocalizedWebDescription("FBAPackPasswordRecoveryWebPart", "SubmitButtonImageUrl_Description")]
        public string SubmitButtonImageUrl
        {
            get { return _submitButtonImageUrl; }
            set { _submitButtonImageUrl = value; }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [LocalizedWebDisplayName("FBAPackPasswordRecoveryWebPart", "SubmitButtonText_FriendlyName")]
        [LocalizedCategory("FBAPackPasswordRecoveryWebPart", "SubmitButtonText_Category")]
        [LocalizedWebDescription("FBAPackPasswordRecoveryWebPart", "SubmitButtonText_Description")]
        public string SubmitButtonText
        {
            get { return _submitButtonText; }
            set { _submitButtonText = value; }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [LocalizedWebDisplayName("FBAPackPasswordRecoveryWebPart", "SubmitButtonType_FriendlyName")]
        [LocalizedCategory("FBAPackPasswordRecoveryWebPart", "SubmitButtonType_Category")]
        [LocalizedWebDescription("FBAPackPasswordRecoveryWebPart", "SubmitButtonType_Description")]
        public ButtonType SubmitButtonType
        {
            get { return _submitButtonType; }
            set { _submitButtonType = value; }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [LocalizedWebDisplayName("FBAPackPasswordRecoveryWebPart", "SuccessText_FriendlyName")]
        [LocalizedCategory("FBAPackPasswordRecoveryWebPart", "SuccessText_Category")]
        [LocalizedWebDescription("FBAPackPasswordRecoveryWebPart", "SuccessText_Description")]
        public string SuccessText
        {
            get { return _successText; }
            set { _successText = value; }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [LocalizedWebDisplayName("FBAPackPasswordRecoveryWebPart", "UserNameFailureText_FriendlyName")]
        [LocalizedCategory("FBAPackPasswordRecoveryWebPart", "UserNameFailureText_Category")]
        [LocalizedWebDescription("FBAPackPasswordRecoveryWebPart", "UserNameFailureText_Description")]
        public string UserNameFailureText
        {
            get { return _userNameFailureText; }
            set { _userNameFailureText = value; }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [LocalizedWebDisplayName("FBAPackPasswordRecoveryWebPart", "UserNameInstructionText_FriendlyName")]
        [LocalizedCategory("FBAPackPasswordRecoveryWebPart", "UserNameInstructionText_Category")]
        [LocalizedWebDescription("FBAPackPasswordRecoveryWebPart", "UserNameInstructionText_Description")]
        public string UserNameInstructionText
        {
            get { return _userNameInstructionText; }
            set { _userNameInstructionText = value; }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [LocalizedWebDisplayName("FBAPackPasswordRecoveryWebPart", "UserNameLabelText_FriendlyName")]
        [LocalizedCategory("FBAPackPasswordRecoveryWebPart", "UserNameLabelText_Category")]
        [LocalizedWebDescription("FBAPackPasswordRecoveryWebPart", "UserNameLabelText_Description")]
        public string UserNameLabelText
        {
            get { return _userNameLabelText; }
            set { _userNameLabelText = value; }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [LocalizedWebDisplayName("FBAPackPasswordRecoveryWebPart", "UserNameTitleText_FriendlyName")]
        [LocalizedCategory("FBAPackPasswordRecoveryWebPart", "UserNameTitleText_Category")]
        [LocalizedWebDescription("FBAPackPasswordRecoveryWebPart", "UserNameTitleText_Description")]
        public string UserNameTitleText
        {
            get { return _userNameTitleText; }
            set { _userNameTitleText = value; }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [LocalizedWebDisplayName("FBAPackPasswordRecoveryWebPart", "AnswerRequiredErrorMessage_FriendlyName")]
        [LocalizedCategory("FBAPackPasswordRecoveryWebPart", "AnswerRequiredErrorMessage_Category")]
        [LocalizedWebDescription("FBAPackPasswordRecoveryWebPart", "AnswerRequiredErrorMessage_Description")]
        public string AnswerRequiredErrorMessage
        {
            get { return _answerRequiredErrorMessage; }
            set { _answerRequiredErrorMessage = value; }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [LocalizedWebDisplayName("FBAPackPasswordRecoveryWebPart", "UserNameRequiredErrorMessage_FriendlyName")]
        [LocalizedCategory("FBAPackPasswordRecoveryWebPart", "UserNameRequiredErrorMessage_Category")]
        [LocalizedWebDescription("FBAPackPasswordRecoveryWebPart", "UserNameRequiredErrorMessage_Description")]
        public string UserNameRequiredErrorMessage
        {
            get { return _userNameRequiredErrorMessage; }
            set { _userNameRequiredErrorMessage = value; }
        }
        #endregion

        #region Methods
        private void AddPasswordRecoveryControl()
        {


            /* bms I couldn't figure out how to set the smtp server from code so I added the SendMailError as a hack for now */

            _ctlPasswordRecovery = new System.Web.UI.WebControls.PasswordRecovery();
            //bms Added the event to catch the error and send our own email
            _ctlPasswordRecovery.SendMailError += new SendMailErrorEventHandler(_ctlPasswordRecovery_SendMailError);
            _ctlPasswordRecovery.VerifyingUser += new LoginCancelEventHandler(_ctlPasswordRecovery_VerifyingUser);
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

        void _ctlPasswordRecovery_VerifyingUser(object sender, LoginCancelEventArgs e)
        {
            PasswordRecovery prc = (PasswordRecovery)sender;
            MembershipUser currentUser = Utils.GetUser(prc.UserName);
            string newUserName = null;
            //If the username doesn't work, get the username by email address
            if (currentUser == null)
            {
                newUserName = Utils.GetUserNameByEmail(prc.UserName);

                if (newUserName != null)
                {
                    prc.UserName = newUserName;
                }
            }

        }

        void _ctlPasswordRecovery_SendMailError(object sender, SendMailErrorEventArgs e)
        {
            using (SPSite _site = new SPSite(SPContext.Current.Site.ID, SPContext.Current.Site.Zone))
            {
                using (SPWeb _web = _site.OpenWeb())
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
                    membershipitem.ChangePasswordURL = Utils.GetAbsoluteURL(_web, settings.ChangePasswordPage);
                    membershipitem.PasswordQuestionURL = Utils.GetAbsoluteURL(_web, settings.PasswordQuestionPage);
                    membershipitem.ThankYouURL = Utils.GetAbsoluteURL(_web, settings.ThankYouPage);

                    if (!MembershipRequest.SendPasswordResetEmail(membershipitem, _web))
                    {
                        prc.SuccessText = LocalizedString.GetString("FBAPackPasswordRecoveryWebPart", "ErrorSendingEmail");
                    }
                    e.Handled = true;
                }
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

        #endregion
    }
}
