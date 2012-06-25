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

        private LocalizedString _resourceManager = new LocalizedString("FBAPackPasswordRecoveryWebPart");

        private string _questionTemplate = "/_layouts/FBA/WEBPARTS/PasswordRecoveryWebPart/QuestionTemplate.ascx";
        private string _successTemplate = "/_layouts/FBA/WEBPARTS/PasswordRecoveryWebPart/SuccessTemplate.ascx";
        private string _userNameTemplate = "/_layouts/FBA/WEBPARTS/PasswordRecoveryWebPart/UserNameTemplate.ascx";
        private string _generalFailureText = null;
        private string _questionFailureText = null;
        private string _questionInstructionText = null;
        private string _questionLabelText = null;
        private string _questionTitleText = null;
        private string _submitButtonImageUrl = null;
        private string _submitButtonText = null;
        private ButtonType _submitButtonType = ButtonType.Button;
        private string _successText = null;
        private string _userNameFailureText = null;
        private string _userNameInstructionText = null;
        private string _userNameLabelText = null;
        private string _userNameTitleText = null;
        private string _answerRequiredErrorMessage = null;
        private string _userNameRequiredErrorMessage = null;
        #endregion

        #region Properties
        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [LocalizedWebDisplayName("FBAPackPasswordRecoveryWebPart", "QuestionTemplate_FriendlyName")]
        [LocalizedCategory("FBAPackPasswordRecoveryWebPart", "QuestionTemplate_Category")]
        [LocalizedWebDescription("FBAPackPasswordRecoveryWebPart", "QuestionTemplate_Description")]
        public string QuestionTemplate
        {
            get { return _questionTemplate; }
            set { _questionTemplate = value; }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [LocalizedWebDisplayName("FBAPackPasswordRecoveryWebPart", "SuccessTemplate_FriendlyName")]
        [LocalizedCategory("FBAPackPasswordRecoveryWebPart", "SuccessTemplate_Category")]
        [LocalizedWebDescription("FBAPackPasswordRecoveryWebPart", "SuccessTemplate_Description")]
        public string SuccessTemplate
        {
            get { return _successTemplate; }
            set { _successTemplate = value; }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [LocalizedWebDisplayName("FBAPackPasswordRecoveryWebPart", "UserNameTemplate_FriendlyName")]
        [LocalizedCategory("FBAPackPasswordRecoveryWebPart", "UserNameTemplate_Category")]
        [LocalizedWebDescription("FBAPackPasswordRecoveryWebPart", "UserNameTemplate_Description")]
        public string UserNameTemplate
        {
            get { return _userNameTemplate; }
            set { _userNameTemplate = value; }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [LocalizedWebDisplayName("FBAPackPasswordRecoveryWebPart", "GeneralFailureText_FriendlyName")]
        [LocalizedCategory("FBAPackPasswordRecoveryWebPart", "GeneralFailureText_Category")]
        [LocalizedWebDescription("FBAPackPasswordRecoveryWebPart", "GeneralFailureText_Description")]
        public string GeneralFailureText
        {
            get
            {
                if (_generalFailureText != null)
                {
                    return _generalFailureText;
                }
                return _resourceManager.GetString("GeneralFailureText_DefaultValue");
            }

            set { _generalFailureText = value; }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [LocalizedWebDisplayName("FBAPackPasswordRecoveryWebPart", "QuestionFailureText_FriendlyName")]
        [LocalizedCategory("FBAPackPasswordRecoveryWebPart", "QuestionFailureText_Category")]
        [LocalizedWebDescription("FBAPackPasswordRecoveryWebPart", "QuestionFailureText_Description")]
        public string QuestionFailureText
        {
            get
            {
                if (_questionFailureText != null)
                {
                    return _questionFailureText;
                }
                return _resourceManager.GetString("QuestionFailureText_DefaultValue");
            }
            set { _questionFailureText = value; }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [LocalizedWebDisplayName("FBAPackPasswordRecoveryWebPart", "QuestionInstructionText_FriendlyName")]
        [LocalizedCategory("FBAPackPasswordRecoveryWebPart", "QuestionInstructionText_Category")]
        [LocalizedWebDescription("FBAPackPasswordRecoveryWebPart", "QuestionInstructionText_Description")]
        public string QuestionInstructionText
        {
            get
            {
                if (_questionInstructionText != null)
                {
                    return _questionInstructionText;
                }
                return _resourceManager.GetString("QuestionInstructionText_DefaultValue");
            }
            set { _questionInstructionText = value; }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [LocalizedWebDisplayName("FBAPackPasswordRecoveryWebPart", "QuestionLabelText_FriendlyName")]
        [LocalizedCategory("FBAPackPasswordRecoveryWebPart", "QuestionLabelText_Category")]
        [LocalizedWebDescription("FBAPackPasswordRecoveryWebPart", "QuestionLabelText_Description")]
        public string QuestionLabelText
        {
            get
            {
                if (_questionLabelText != null)
                {
                    return _questionLabelText;
                }
                return _resourceManager.GetString("QuestionLabelText_DefaultValue");
            }
            set { _questionLabelText = value; }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [LocalizedWebDisplayName("FBAPackPasswordRecoveryWebPart", "QuestionTitleText_FriendlyName")]
        [LocalizedCategory("FBAPackPasswordRecoveryWebPart", "QuestionTitleText_Category")]
        [LocalizedWebDescription("FBAPackPasswordRecoveryWebPart", "QuestionTitleText_Description")]
        public string QuestionTitleText
        {
            get
            {
                if (_questionTitleText != null)
                {
                    return _questionTitleText;
                }
                return _resourceManager.GetString("QuestionTitleText_DefaultValue");
            }
            set { _questionTitleText = value; }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [LocalizedWebDisplayName("FBAPackPasswordRecoveryWebPart", "SubmitButtonImageUrl_FriendlyName")]
        [LocalizedCategory("FBAPackPasswordRecoveryWebPart", "SubmitButtonImageUrl_Category")]
        [LocalizedWebDescription("FBAPackPasswordRecoveryWebPart", "SubmitButtonImageUrl_Description")]
        public string SubmitButtonImageUrl
        {
            get
            {
                if (_submitButtonImageUrl != null)
                {
                    return _submitButtonImageUrl;
                }
                return _resourceManager.GetString("SubmitButtonImageUrl_DefaultValue");
            }
            set { _submitButtonImageUrl = value; }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [LocalizedWebDisplayName("FBAPackPasswordRecoveryWebPart", "SubmitButtonText_FriendlyName")]
        [LocalizedCategory("FBAPackPasswordRecoveryWebPart", "SubmitButtonText_Category")]
        [LocalizedWebDescription("FBAPackPasswordRecoveryWebPart", "SubmitButtonText_Description")]
        public string SubmitButtonText
        {
            get
            {
                if (_submitButtonText != null)
                {
                    return _submitButtonText;
                }
                return _resourceManager.GetString("SubmitButtonText_DefaultValue");
            }
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
            get
            {
                if (_successText != null)
                {
                    return _successText;
                }
                return _resourceManager.GetString("SuccessText_DefaultValue");
            }
            set { _successText = value; }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [LocalizedWebDisplayName("FBAPackPasswordRecoveryWebPart", "UserNameFailureText_FriendlyName")]
        [LocalizedCategory("FBAPackPasswordRecoveryWebPart", "UserNameFailureText_Category")]
        [LocalizedWebDescription("FBAPackPasswordRecoveryWebPart", "UserNameFailureText_Description")]
        public string UserNameFailureText
        {
            get
            {
                if (_userNameFailureText != null)
                {
                    return _userNameFailureText;
                }
                return _resourceManager.GetString("UserNameFailureText_DefaultValue");
            }
            set { _userNameFailureText = value; }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [LocalizedWebDisplayName("FBAPackPasswordRecoveryWebPart", "UserNameInstructionText_FriendlyName")]
        [LocalizedCategory("FBAPackPasswordRecoveryWebPart", "UserNameInstructionText_Category")]
        [LocalizedWebDescription("FBAPackPasswordRecoveryWebPart", "UserNameInstructionText_Description")]
        public string UserNameInstructionText
        {
            get
            {
                if (_userNameInstructionText != null)
                {
                    return _userNameInstructionText;
                }
                return _resourceManager.GetString("UserNameInstructionText_DefaultValue");
            }
            set { _userNameInstructionText = value; }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [LocalizedWebDisplayName("FBAPackPasswordRecoveryWebPart", "UserNameLabelText_FriendlyName")]
        [LocalizedCategory("FBAPackPasswordRecoveryWebPart", "UserNameLabelText_Category")]
        [LocalizedWebDescription("FBAPackPasswordRecoveryWebPart", "UserNameLabelText_Description")]
        public string UserNameLabelText
        {
            get
            {
                if (_userNameLabelText != null)
                {
                    return _userNameLabelText;
                }
                return _resourceManager.GetString("UserNameLabelText_DefaultValue");
            }
            set { _userNameLabelText = value; }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [LocalizedWebDisplayName("FBAPackPasswordRecoveryWebPart", "UserNameTitleText_FriendlyName")]
        [LocalizedCategory("FBAPackPasswordRecoveryWebPart", "UserNameTitleText_Category")]
        [LocalizedWebDescription("FBAPackPasswordRecoveryWebPart", "UserNameTitleText_Description")]
        public string UserNameTitleText
        {
            get
            {
                if (_userNameTitleText != null)
                {
                    return _userNameTitleText;
                }
                return _resourceManager.GetString("UserNameTitleText_DefaultValue");
            }
            set { _userNameTitleText = value; }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [LocalizedWebDisplayName("FBAPackPasswordRecoveryWebPart", "AnswerRequiredErrorMessage_FriendlyName")]
        [LocalizedCategory("FBAPackPasswordRecoveryWebPart", "AnswerRequiredErrorMessage_Category")]
        [LocalizedWebDescription("FBAPackPasswordRecoveryWebPart", "AnswerRequiredErrorMessage_Description")]
        public string AnswerRequiredErrorMessage
        {
            get
            {
                if (_answerRequiredErrorMessage != null)
                {
                    return _answerRequiredErrorMessage;
                }
                return _resourceManager.GetString("AnswerRequiredErrorMessage_DefaultValue");
            }
            set { _answerRequiredErrorMessage = value; }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [LocalizedWebDisplayName("FBAPackPasswordRecoveryWebPart", "UserNameRequiredErrorMessage_FriendlyName")]
        [LocalizedCategory("FBAPackPasswordRecoveryWebPart", "UserNameRequiredErrorMessage_Category")]
        [LocalizedWebDescription("FBAPackPasswordRecoveryWebPart", "UserNameRequiredErrorMessage_Description")]
        public string UserNameRequiredErrorMessage
        {
            get
            {
                if (_userNameRequiredErrorMessage != null)
                {
                    return _userNameRequiredErrorMessage;
                }
                return _resourceManager.GetString("UserNameRequiredErrorMessage_DefaultValue");
            }
            set { _userNameRequiredErrorMessage = value; }
        }
        #endregion

        #region Methods
        private void AddPasswordRecoveryControl()
        {
            TemplateHelper helper;

            string provider = Utils.GetMembershipProvider(Context);
            //Exit if membership provider not defined
            if (provider == null || !Utils.IsProviderConfigured())
            {
                Controls.Add(new LiteralControl(LocalizedString.GetString("FBAPackFeatures", "MembershipNotConfigured")));
                return;
            }

            /* bms I couldn't figure out how to set the smtp server from code so I added the SendMailError as a hack for now */

            _ctlPasswordRecovery = new System.Web.UI.WebControls.PasswordRecovery();
            _ctlPasswordRecovery.UserNameTemplate = new TemplateLoader(UserNameTemplate, Page);
            _ctlPasswordRecovery.SuccessTemplate = new TemplateLoader(SuccessTemplate, Page);
            _ctlPasswordRecovery.QuestionTemplate = new TemplateLoader(QuestionTemplate, Page);
            //bms Added the event to catch the error and send our own email
            _ctlPasswordRecovery.SendMailError += new SendMailErrorEventHandler(_ctlPasswordRecovery_SendMailError);
            _ctlPasswordRecovery.VerifyingUser += new LoginCancelEventHandler(_ctlPasswordRecovery_VerifyingUser);
            _ctlPasswordRecovery.SendingMail += new MailMessageEventHandler(_ctlPasswordRecovery_SendingMail);
            _ctlPasswordRecovery.MembershipProvider = provider;
            _ctlPasswordRecovery.GeneralFailureText = GeneralFailureText;
            _ctlPasswordRecovery.QuestionFailureText = QuestionFailureText;
            _ctlPasswordRecovery.UserNameFailureText = UserNameFailureText;

            //UsernameTemplate
            helper = new TemplateHelper(_ctlPasswordRecovery.UserNameTemplateContainer);
            helper.SetText("UserNameInstruction", UserNameInstructionText);
            helper.SetText("UserNameLabel", UserNameLabelText);
            helper.SetText("UserNameTitle", UserNameTitleText);
            helper.SetValidation("UserNameRequired", UserNameRequiredErrorMessage, this.UniqueID);
            switch (SubmitButtonType)
            {
                case ButtonType.Button:
                    helper.SetButton("SubmitButton", SubmitButtonText, this.UniqueID);
                    helper.SetVisible("SubmitButton", true);
                    break;

                case ButtonType.Image:
                    helper.SetImageButton("SubmitImageButton", SubmitButtonImageUrl, SubmitButtonText, this.UniqueID);
                    helper.SetVisible("SubmitImageButton", true);
                    break;

                case ButtonType.Link:
                    helper.SetButton("SubmitLinkButton", SubmitButtonText, this.UniqueID);
                    helper.SetVisible("SubmitLinkButton", true);
                    break;
            }

            //QuestionTemplate
            helper = new TemplateHelper(_ctlPasswordRecovery.QuestionTemplateContainer);
            helper.SetText("QuestionInstruction", QuestionInstructionText);
            helper.SetText("UserNameLabel", UserNameLabelText);
            helper.SetText("QuestionLabel", QuestionLabelText);
            helper.SetText("QuestionTitle", QuestionTitleText);
            helper.SetValidation("AnswerRequired", AnswerRequiredErrorMessage, this.UniqueID);
            switch (SubmitButtonType)
            {
                case ButtonType.Button:
                    helper.SetButton("SubmitButton", SubmitButtonText, this.UniqueID);
                    helper.SetVisible("SubmitButton", true);
                    break;

                case ButtonType.Image:
                    helper.SetImageButton("SubmitImageButton", SubmitButtonImageUrl, SubmitButtonText, this.UniqueID);
                    helper.SetVisible("SubmitImageButton", true);
                    break;

                case ButtonType.Link:
                    helper.SetButton("SubmitLinkButton", SubmitButtonText, this.UniqueID);
                    helper.SetVisible("SubmitLinkButton", true);
                    break;
            }

            //SuccessTemplate
            helper = new TemplateHelper(_ctlPasswordRecovery.SuccessTemplateContainer);
            helper.SetText("Success", SuccessText);
            
            this.Controls.Add(_ctlPasswordRecovery);
        }

        /// <summary>
        /// Overrides the built in mail sending and calls our mail sending function
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void _ctlPasswordRecovery_SendingMail(object sender, MailMessageEventArgs e)
        {
            e.Cancel = true;
            SendEmail();
        }

        void _ctlPasswordRecovery_VerifyingUser(object sender, LoginCancelEventArgs e)
        {
            PasswordRecovery prc = (PasswordRecovery)sender;
            MembershipUser currentUser = Utils.BaseMembershipProvider().GetUser(prc.UserName,false);
            string newUserName = null;
            //If the username doesn't work, get the username by email address
            if (currentUser == null)
            {
                newUserName = Utils.BaseMembershipProvider().GetUserNameByEmail(prc.UserName);

                if (newUserName != null)
                {
                    prc.UserName = newUserName;
                }
            }

        }

        /// <summary>
        /// Called if the mailsettings don't exist in the web.config.  This is ok, since we use SharePoint
        /// to send the email. Simply call our send email function.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void _ctlPasswordRecovery_SendMailError(object sender, SendMailErrorEventArgs e)
        {
            e.Handled = true;
            SendEmail();

        }

        private void SendEmail()
        {
            SPSecurity.RunWithElevatedPrivileges(delegate()
            {
                using (SPSite _site = new SPSite(SPContext.Current.Site.ID, SPContext.Current.Site.Zone))
                {
                    using (SPWeb _web = _site.OpenWeb(SPContext.Current.Web.ID))
                    {
                        if (_web != null)
                        {

                            _site.AllowUnsafeUpdates = true;
                            _web.AllowUnsafeUpdates = true;


                            PasswordRecovery prc = _ctlPasswordRecovery;
                            MembershipUser currentUser = Utils.BaseMembershipProvider(_web.Site).GetUser(prc.UserName, false);
                            MembershipRequest membershipitem = MembershipRequest.GetMembershipRequest(currentUser, _web);

                            membershipitem.PasswordQuestion = currentUser.PasswordQuestion;
                            membershipitem.Password = currentUser.ResetPassword(prc.Answer);

                            if (!MembershipRequest.SendPasswordRecoveryEmail(membershipitem, _web))
                            {
                                TemplateHelper helper = new TemplateHelper(_ctlPasswordRecovery.SuccessTemplateContainer);
                                helper.SetText("Success", LocalizedString.GetString("FBAPackPasswordRecoveryWebPart", "ErrorSendingEmail"));
                            }

                            
                        }
                    }
                }
            });      
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
