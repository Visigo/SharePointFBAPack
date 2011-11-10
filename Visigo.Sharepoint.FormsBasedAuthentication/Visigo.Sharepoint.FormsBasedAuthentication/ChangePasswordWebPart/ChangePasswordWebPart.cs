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
using Microsoft.SharePoint.Utilities;

namespace Visigo.Sharepoint.FormsBasedAuthentication
{
    public class ChangePasswordWebPart : System.Web.UI.WebControls.WebParts.WebPart
    {
        #region Fields
        private string _cancelButtonImageUrl = string.Empty;
        private string _cancelButtonText = string.Empty;
        private ButtonType _cancelButtonType = ButtonType.Button;
        private string _cancelDestinationPageUrl = string.Empty;
        private string _changePasswordButtonImageUrl = string.Empty;
        private string _changePasswordButtonText = string.Empty;
        private ButtonType _changePasswordButtonType = ButtonType.Button;
        private string _changePasswordFailureText = string.Empty;
        private string _changePasswordTitleText = string.Empty;
        private string _confirmNewPasswordLabelText = string.Empty;
        private string _confirmPasswordCompareErrorMessage = string.Empty;
        private string _confirmPasswordRequiredErrorMessage = string.Empty;
        private string _newPasswordRequiredErrorMessage = string.Empty;
        private string _continueButtonImageUrl = string.Empty;
        private string _continueButtonText = string.Empty;
        private ButtonType _continueButtonType = ButtonType.Button;
        private string _continueDestinationPageUrl = string.Empty;
        private string _createUserIconUrl = string.Empty;
        private string _createUserText = string.Empty;
        private string _createUserUrl = string.Empty;
        private bool _displayUserName = false;
        private string _editProfileIconUrl = string.Empty;
        private string _editProfileText = string.Empty;
        private string _editProfileUrl = string.Empty;
        private string _helpPageIconUrl = string.Empty;
        private string _helpPageText = string.Empty;
        private string _helpPageUrl = string.Empty;
        private string _instructionText = string.Empty;
        private string _newPasswordLabelText = string.Empty;
        private string _newPasswordRegularExpressionErrorMessage = string.Empty;
        private string _passwordHintText = string.Empty;
        private string _passwordLabelText = string.Empty;
        private string _passwordRecoveryIconUrl = string.Empty;
        private string _passwordRecoveryText = string.Empty;
        private string _passwordRecoveryUrl = string.Empty;
        private string _passwordRequiredErrorMessage = string.Empty;
        private string _successPageUrl = string.Empty;
        private string _successText = string.Empty;
        //private string _toolTip = string.Empty;
        private string _userNameLabelText = string.Empty;
        private string _userNameRequiredErrorMessage = string.Empty;
        #endregion

        #region Controls
        private System.Web.UI.WebControls.ChangePassword _ctlChangePassword;
        #endregion

        #region Properties
        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [LocalizedWebDisplayName("FBAPackChangePasswordWebPart", "ChangePasswordTemplate_FriendlyName")]
        [LocalizedCategory("FBAPackChangePasswordWebPart", "ChangePasswordTemplate_Category")]
        [LocalizedWebDescription("FBAPackChangePasswordWebPart", "ChangePasswordTemplate_Description")]
        public string ChangePasswordTemplate { get; set; }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [LocalizedWebDisplayName("FBAPackChangePasswordWebPart", "SuccessTemplate_FriendlyName")]
        [LocalizedCategory("FBAPackChangePasswordWebPart", "SuccessTemplate_Category")]
        [LocalizedWebDescription("FBAPackChangePasswordWebPart", "SuccessTemplate_Description")]
        public string SuccessTemplate { get; set; }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [LocalizedWebDisplayName("FBAPackChangePasswordWebPart", "CancelButtonImageUrl_FriendlyName")]
        [LocalizedCategory("FBAPackChangePasswordWebPart", "CancelButtonImageUrl_Category")]
        [LocalizedWebDescription("FBAPackChangePasswordWebPart", "CancelButtonImageUrl_Description")]
        public string CancelButtonImageUrl
        {
            get
            {
                return _cancelButtonImageUrl;
            }
            set
            {
                _cancelButtonImageUrl = value;
            }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [LocalizedWebDisplayName("FBAPackChangePasswordWebPart", "CancelButtonText_FriendlyName")]
        [LocalizedCategory("FBAPackChangePasswordWebPart", "CancelButtonText_Category")]
        [LocalizedWebDescription("FBAPackChangePasswordWebPart", "CancelButtonText_Description")]        
        public string CancelButtonText
        {
            get
            {
                return _cancelButtonText;
            }
            set
            {
                _cancelButtonText = value;
            }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [LocalizedWebDisplayName("FBAPackChangePasswordWebPart", "CancelButtonType_FriendlyName")]
        [LocalizedCategory("FBAPackChangePasswordWebPart", "CancelButtonType_Category")]
        [LocalizedWebDescription("FBAPackChangePasswordWebPart", "CancelButtonType_Description")]
        public ButtonType CancelButtonType
        {
            get
            {
                return _cancelButtonType;
            }
            set
            {
                _cancelButtonType = value;
            }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [LocalizedWebDisplayName("FBAPackChangePasswordWebPart", "CancelDestinationPageUrl_FriendlyName")]
        [LocalizedCategory("FBAPackChangePasswordWebPart", "CancelDestinationPageUrl_Category")]
        [LocalizedWebDescription("FBAPackChangePasswordWebPart", "CancelDestinationPageUrl_Description")]
        public string CancelDestinationPageUrl
        {
            get
            {
                return _cancelDestinationPageUrl;
            }
            set
            {
                _cancelDestinationPageUrl = value;
            }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [LocalizedWebDisplayName("FBAPackChangePasswordWebPart", "ChangePasswordButtonImageUrl_FriendlyName")]
        [LocalizedCategory("FBAPackChangePasswordWebPart", "ChangePasswordButtonImageUrl_Category")]
        [LocalizedWebDescription("FBAPackChangePasswordWebPart", "ChangePasswordButtonImageUrl_Description")]
        public string ChangePasswordButtonImageUrl
        {
            get
            {
                return _changePasswordButtonImageUrl;
            }
            set
            {
                _changePasswordButtonImageUrl = value;
            }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [LocalizedWebDisplayName("FBAPackChangePasswordWebPart", "ChangePasswordButtonText_FriendlyName")]
        [LocalizedCategory("FBAPackChangePasswordWebPart", "ChangePasswordButtonText_Category")]
        [LocalizedWebDescription("FBAPackChangePasswordWebPart", "ChangePasswordButtonText_Description")]
        public string ChangePasswordButtonText
        {
            get
            {
                return _changePasswordButtonText;
            }
            set
            {
                _changePasswordButtonText = value;
            }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [LocalizedWebDisplayName("FBAPackChangePasswordWebPart", "ChangePasswordButtonType_FriendlyName")]
        [LocalizedCategory("FBAPackChangePasswordWebPart", "ChangePasswordButtonType_Category")]
        [LocalizedWebDescription("FBAPackChangePasswordWebPart", "ChangePasswordButtonType_Description")]
        public ButtonType ChangePasswordButtonType
        {
            get
            {
                return _changePasswordButtonType;
            }
            set
            {
                _changePasswordButtonType = value;
            }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [LocalizedWebDisplayName("FBAPackChangePasswordWebPart", "ChangePasswordFailureText_FriendlyName")]
        [LocalizedCategory("FBAPackChangePasswordWebPart", "ChangePasswordFailureText_Category")]
        [LocalizedWebDescription("FBAPackChangePasswordWebPart", "ChangePasswordFailureText_Description")]
        public string ChangePasswordFailureText
        {
            get
            {
                return _changePasswordFailureText;
            }
            set
            {
                _changePasswordFailureText = value;
            }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [LocalizedWebDisplayName("FBAPackChangePasswordWebPart", "ChangePasswordTitleText_FriendlyName")]
        [LocalizedCategory("FBAPackChangePasswordWebPart", "ChangePasswordTitleText_Category")]
        [LocalizedWebDescription("FBAPackChangePasswordWebPart", "ChangePasswordTitleText_Description")]
        public string ChangePasswordTitleText
        {
            get
            {
                return _changePasswordTitleText;
            }
            set
            {
                _changePasswordTitleText = value;
            }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [LocalizedWebDisplayName("FBAPackChangePasswordWebPart", "ConfirmNewPasswordLabelText_FriendlyName")]
        [LocalizedCategory("FBAPackChangePasswordWebPart", "ConfirmNewPasswordLabelText_Category")]
        [LocalizedWebDescription("FBAPackChangePasswordWebPart", "ConfirmNewPasswordLabelText_Description")]
        public string ConfirmNewPasswordLabelText
        {
            get
            {
                return _confirmNewPasswordLabelText;
            }
            set
            {
                _confirmNewPasswordLabelText = value;
            }
        }


        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [LocalizedWebDisplayName("FBAPackChangePasswordWebPart", "ConfirmPasswordCompareErrorMessage_FriendlyName")]
        [LocalizedCategory("FBAPackChangePasswordWebPart", "ConfirmPasswordCompareErrorMessage_Category")]
        [LocalizedWebDescription("FBAPackChangePasswordWebPart", "ConfirmPasswordCompareErrorMessage_Description")]
        public string ConfirmPasswordCompareErrorMessage
        {
            get
            {
                return _confirmPasswordCompareErrorMessage;
            }
            set
            {
                _confirmPasswordCompareErrorMessage = value;
            }
        }


        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [LocalizedWebDisplayName("FBAPackChangePasswordWebPart", "ConfirmPasswordRequiredErrorMessage_FriendlyName")]
        [LocalizedCategory("FBAPackChangePasswordWebPart", "ConfirmPasswordRequiredErrorMessage_Category")]
        [LocalizedWebDescription("FBAPackChangePasswordWebPart", "ConfirmPasswordRequiredErrorMessage_Description")]
        public string ConfirmPasswordRequiredErrorMessage
        {
            get
            {
                return _confirmPasswordRequiredErrorMessage;
            }
            set
            {
                _confirmPasswordRequiredErrorMessage = value;
            }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [LocalizedWebDisplayName("FBAPackChangePasswordWebPart", "NewPasswordRequiredErrorMessage_FriendlyName")]
        [LocalizedCategory("FBAPackChangePasswordWebPart", "NewPasswordRequiredErrorMessage_Category")]
        [LocalizedWebDescription("FBAPackChangePasswordWebPart", "NewPasswordRequiredErrorMessage_Description")]
        public string NewPasswordRequiredErrorMessage
        {
            get
            {
                return _newPasswordRequiredErrorMessage;
            }
            set
            {
                _newPasswordRequiredErrorMessage = value;
            }
        }


        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [LocalizedWebDisplayName("FBAPackChangePasswordWebPart", "ContinueButtonImageUrl_FriendlyName")]
        [LocalizedCategory("FBAPackChangePasswordWebPart", "ContinueButtonImageUrl_Category")]
        [LocalizedWebDescription("FBAPackChangePasswordWebPart", "ContinueButtonImageUrl_Description")]
        public string ContinueButtonImageUrl
        {
            get
            {
                return _continueButtonImageUrl;
            }
            set
            {
                _continueButtonImageUrl = value;
            }
        }


        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [LocalizedWebDisplayName("FBAPackChangePasswordWebPart", "ContinueButtonText_FriendlyName")]
        [LocalizedCategory("FBAPackChangePasswordWebPart", "ContinueButtonText_Category")]
        [LocalizedWebDescription("FBAPackChangePasswordWebPart", "ContinueButtonText_Description")]
        public string ContinueButtonText
        {
            get
            {
                return _continueButtonText;
            }
            set
            {
                _continueButtonText = value;
            }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [LocalizedWebDisplayName("FBAPackChangePasswordWebPart", "ContinueButtonType_FriendlyName")]
        [LocalizedCategory("FBAPackChangePasswordWebPart", "ContinueButtonType_Category")]
        [LocalizedWebDescription("FBAPackChangePasswordWebPart", "ContinueButtonType_Description")]
        public ButtonType ContinueButtonType
        {
            get
            {
                return _continueButtonType;
            }
            set
            {
                _continueButtonType = value;
            }
        }


        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [LocalizedWebDisplayName("FBAPackChangePasswordWebPart", "ContinueDestinationPageUrl_FriendlyName")]
        [LocalizedCategory("FBAPackChangePasswordWebPart", "ContinueDestinationPageUrl_Category")]
        [LocalizedWebDescription("FBAPackChangePasswordWebPart", "ContinueDestinationPageUrl_Description")]
        public string ContinueDestinationPageUrl
        {
            get
            {
                return _continueDestinationPageUrl;
            }
            set
            {
                _continueDestinationPageUrl = value;
            }
        }


        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [LocalizedWebDisplayName("FBAPackChangePasswordWebPart", "CreateUserIconUrl_FriendlyName")]
        [LocalizedCategory("FBAPackChangePasswordWebPart", "CreateUserIconUrl_Category")]
        [LocalizedWebDescription("FBAPackChangePasswordWebPart", "CreateUserIconUrl_Description")]
        public string CreateUserIconUrl
        {
            get
            {
                return _createUserIconUrl;
            }
            set
            {
                _createUserIconUrl = value;
            }
        }


        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [LocalizedWebDisplayName("FBAPackChangePasswordWebPart", "CreateUserText_FriendlyName")]
        [LocalizedCategory("FBAPackChangePasswordWebPart", "CreateUserText_Category")]
        [LocalizedWebDescription("FBAPackChangePasswordWebPart", "CreateUserText_Description")]
        public string CreateUserText
        {
            get
            {
                return _createUserText;
            }
            set
            {
                _createUserText = value;
            }
        }


        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [LocalizedWebDisplayName("FBAPackChangePasswordWebPart", "CreateUserUrl_FriendlyName")]
        [LocalizedCategory("FBAPackChangePasswordWebPart", "CreateUserUrl_Category")]
        [LocalizedWebDescription("FBAPackChangePasswordWebPart", "CreateUserUrl_Description")]
        public string CreateUserUrl
        {
            get
            {
                return _createUserUrl;
            }
            set
            {
                _createUserUrl = value;
            }
        }


        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [LocalizedWebDisplayName("FBAPackChangePasswordWebPart", "DisplayUserName_FriendlyName")]
        [LocalizedCategory("FBAPackChangePasswordWebPart", "DisplayUserName_Category")]
        [LocalizedWebDescription("FBAPackChangePasswordWebPart", "DisplayUserName_Description")]
        public bool DisplayUserName
        {
            get
            {
                return _displayUserName;
            }
            set
            {
                _displayUserName = value;
            }
        }


        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [LocalizedWebDisplayName("FBAPackChangePasswordWebPart", "EditProfileIconUrl_FriendlyName")]
        [LocalizedCategory("FBAPackChangePasswordWebPart", "EditProfileIconUrl_Category")]
        [LocalizedWebDescription("FBAPackChangePasswordWebPart", "EditProfileIconUrl_Description")]
        public string EditProfileIconUrl
        {
            get
            {
                return _editProfileIconUrl;
            }
            set
            {
                _editProfileIconUrl = value;
            }
        }


        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [LocalizedWebDisplayName("FBAPackChangePasswordWebPart", "EditProfileText_FriendlyName")]
        [LocalizedCategory("FBAPackChangePasswordWebPart", "EditProfileText_Category")]
        [LocalizedWebDescription("FBAPackChangePasswordWebPart", "EditProfileText_Description")]
        public string EditProfileText
        {
            get
            {
                return _editProfileText;
            }
            set
            {
                _editProfileText = value;
            }
        }


        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [LocalizedWebDisplayName("FBAPackChangePasswordWebPart", "EditProfileUrl_FriendlyName")]
        [LocalizedCategory("FBAPackChangePasswordWebPart", "EditProfileUrl_Category")]
        [LocalizedWebDescription("FBAPackChangePasswordWebPart", "EditProfileUrl_Description")]
        public string EditProfileUrl
        {
            get
            {
                return _editProfileUrl;
            }
            set
            {
                _editProfileUrl = value;
            }
        }


        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [LocalizedWebDisplayName("FBAPackChangePasswordWebPart", "HelpPageIconUrl_FriendlyName")]
        [LocalizedCategory("FBAPackChangePasswordWebPart", "HelpPageIconUrl_Category")]
        [LocalizedWebDescription("FBAPackChangePasswordWebPart", "HelpPageIconUrl_Description")]
        public string HelpPageIconUrl
        {
            get
            {
                return _helpPageIconUrl;
            }
            set
            {
                _helpPageIconUrl = value;
            }
        }


        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [LocalizedWebDisplayName("FBAPackChangePasswordWebPart", "HelpPageText_FriendlyName")]
        [LocalizedCategory("FBAPackChangePasswordWebPart", "HelpPageText_Category")]
        [LocalizedWebDescription("FBAPackChangePasswordWebPart", "HelpPageText_Description")]
        public string HelpPageText
        {
            get
            {
                return _helpPageText;
            }
            set
            {
                _helpPageText = value;
            }
        }


        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [LocalizedWebDisplayName("FBAPackChangePasswordWebPart", "HelpPageUrl_FriendlyName")]
        [LocalizedCategory("FBAPackChangePasswordWebPart", "HelpPageUrl_Category")]
        [LocalizedWebDescription("FBAPackChangePasswordWebPart", "HelpPageUrl_Description")]
        public string HelpPageUrl
        {
            get
            {
                return _helpPageUrl;
            }
            set
            {
                _helpPageUrl = value;
            }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [LocalizedWebDisplayName("FBAPackChangePasswordWebPart", "InstructionText_FriendlyName")]
        [LocalizedCategory("FBAPackChangePasswordWebPart", "InstructionText_Category")]
        [LocalizedWebDescription("FBAPackChangePasswordWebPart", "InstructionText_Description")]
        public string InstructionText
        {
            get
            {
                return _instructionText;
            }
            set
            {
                _instructionText = value;
            }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [LocalizedWebDisplayName("FBAPackChangePasswordWebPart", "NewPasswordLabelText_FriendlyName")]
        [LocalizedCategory("FBAPackChangePasswordWebPart", "NewPasswordLabelText_Category")]
        [LocalizedWebDescription("FBAPackChangePasswordWebPart", "NewPasswordLabelText_Description")]
        public string NewPasswordLabelText
        {
            get
            {
                return _newPasswordLabelText;
            }
            set
            {
                _newPasswordLabelText = value;
            }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [LocalizedWebDisplayName("FBAPackChangePasswordWebPart", "NewPasswordRegularExpressionErrorMessage_FriendlyName")]
        [LocalizedCategory("FBAPackChangePasswordWebPart", "NewPasswordRegularExpressionErrorMessage_Category")]
        [LocalizedWebDescription("FBAPackChangePasswordWebPart", "NewPasswordRegularExpressionErrorMessage_Description")]
        public string NewPasswordRegularExpressionErrorMessage
        {
            get
            {
                return _newPasswordRegularExpressionErrorMessage;
            }
            set
            {
                _newPasswordRegularExpressionErrorMessage = value;
            }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [LocalizedWebDisplayName("FBAPackChangePasswordWebPart", "PasswordHintText_FriendlyName")]
        [LocalizedCategory("FBAPackChangePasswordWebPart", "PasswordHintText_Category")]
        [LocalizedWebDescription("FBAPackChangePasswordWebPart", "PasswordHintText_Description")]
        public string PasswordHintText
        {
            get
            {
                return _passwordHintText;
            }
            set
            {
                _passwordHintText = value;
            }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [LocalizedWebDisplayName("FBAPackChangePasswordWebPart", "PasswordLabelText_FriendlyName")]
        [LocalizedCategory("FBAPackChangePasswordWebPart", "PasswordLabelText_Category")]
        [LocalizedWebDescription("FBAPackChangePasswordWebPart", "PasswordLabelText_Description")]
        public string PasswordLabelText
        {
            get
            {
                return _passwordLabelText;
            }
            set
            {
                _passwordLabelText = value;
            }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [LocalizedWebDisplayName("FBAPackChangePasswordWebPart", "PasswordRecoveryIconUrl_FriendlyName")]
        [LocalizedCategory("FBAPackChangePasswordWebPart", "PasswordRecoveryIconUrl_Category")]
        [LocalizedWebDescription("FBAPackChangePasswordWebPart", "PasswordRecoveryIconUrl_Description")]
        public string PasswordRecoveryIconUrl
        {
            get
            {
                return _passwordRecoveryIconUrl;
            }
            set
            {
                _passwordRecoveryIconUrl = value;
            }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [LocalizedWebDisplayName("FBAPackChangePasswordWebPart", "PasswordRecoveryText_FriendlyName")]
        [LocalizedCategory("FBAPackChangePasswordWebPart", "PasswordRecoveryText_Category")]
        [LocalizedWebDescription("FBAPackChangePasswordWebPart", "PasswordRecoveryText_Description")]
        public string PasswordRecoveryText
        {
            get
            {
                return _passwordRecoveryText;
            }
            set
            {
                _passwordRecoveryText = value;
            }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [LocalizedWebDisplayName("FBAPackChangePasswordWebPart", "PasswordRecoveryUrl_FriendlyName")]
        [LocalizedCategory("FBAPackChangePasswordWebPart", "PasswordRecoveryUrl_Category")]
        [LocalizedWebDescription("FBAPackChangePasswordWebPart", "PasswordRecoveryUrl_Description")]
        public string PasswordRecoveryUrl
        {
            get
            {
                return _passwordRecoveryUrl;
            }
            set
            {
                _passwordRecoveryUrl = value;
            }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [LocalizedWebDisplayName("FBAPackChangePasswordWebPart", "PasswordRequiredErrorMessage_FriendlyName")]
        [LocalizedCategory("FBAPackChangePasswordWebPart", "PasswordRequiredErrorMessage_Category")]
        [LocalizedWebDescription("FBAPackChangePasswordWebPart", "PasswordRequiredErrorMessage_Description")]
        public string PasswordRequiredErrorMessage
        {
            get
            {
                return _passwordRequiredErrorMessage;
            }
            set
            {
                _passwordRequiredErrorMessage = value;
            }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [LocalizedWebDisplayName("FBAPackChangePasswordWebPart", "SuccessPageUrl_FriendlyName")]
        [LocalizedCategory("FBAPackChangePasswordWebPart", "SuccessPageUrl_Category")]
        [LocalizedWebDescription("FBAPackChangePasswordWebPart", "SuccessPageUrl_Description")]
        public string SuccessPageUrl
        {
            get
            {
                return _successPageUrl;
            }
            set
            {
                _successPageUrl = value;
            }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [LocalizedWebDisplayName("FBAPackChangePasswordWebPart", "SuccessText_FriendlyName")]
        [LocalizedCategory("FBAPackChangePasswordWebPart", "SuccessText_Category")]
        [LocalizedWebDescription("FBAPackChangePasswordWebPart", "SuccessText_Description")]
        public string SuccessText
        {
            get
            {
                return _successText;
            }
            set
            {
                _successText = value;
            }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [LocalizedWebDisplayName("FBAPackChangePasswordWebPart", "UserNameLabelText_FriendlyName")]
        [LocalizedCategory("FBAPackChangePasswordWebPart", "UserNameLabelText_Category")]
        [LocalizedWebDescription("FBAPackChangePasswordWebPart", "UserNameLabelText_Description")]
        public string UserNameLabelText
        {
            get
            {
                return _userNameLabelText;
            }
            set
            {
                _userNameLabelText = value;
            }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [LocalizedWebDisplayName("FBAPackChangePasswordWebPart", "UserNameRequiredErrorMessage_FriendlyName")]
        [LocalizedCategory("FBAPackChangePasswordWebPart", "UserNameRequiredErrorMessage_Category")]
        [LocalizedWebDescription("FBAPackChangePasswordWebPart", "UserNameRequiredErrorMessage_Description")]
        public string UserNameRequiredErrorMessage
        {
            get
            {
                return _userNameRequiredErrorMessage;
            }
            set
            {
                _userNameRequiredErrorMessage = value;
            }
        }

        #endregion

        #region Constructors
        public ChangePasswordWebPart()
        {
            try
            {
                //Default to the current url
                ContinueDestinationPageUrl = SPUtility.GetPageUrlPath(HttpContext.Current);
            }
            catch (Exception ex)
            {
                Utils.LogError(ex);
            }
        }
        #endregion

        #region Methods
        private void AddChangePasswordControl()
        {
            TemplateHelper helper;

            if (SPContext.Current.Web.CurrentUser == null)
            {
                //Login Control won't work with SP2010, so for now, just don't show a control at all
                //Login ctlLogin = new Login();
                //this.Controls.Add(ctlLogin);
            }
            else
            {
                _ctlChangePassword = new System.Web.UI.WebControls.ChangePassword();
                _ctlChangePassword.ChangePasswordTemplate = new TemplateLoader(ChangePasswordTemplate, Page);
                _ctlChangePassword.SuccessTemplate = new TemplateLoader(SuccessTemplate, Page);
                _ctlChangePassword.MembershipProvider = Utils.GetMembershipProvider(Context);
                _ctlChangePassword.CancelDestinationPageUrl = CancelDestinationPageUrl;
                _ctlChangePassword.ChangePasswordFailureText = ChangePasswordFailureText;
                _ctlChangePassword.ContinueDestinationPageUrl = ContinueDestinationPageUrl;
                
                _ctlChangePassword.ToolTip = ToolTip;
                _ctlChangePassword.SuccessPageUrl = SuccessPageUrl;
                _ctlChangePassword.NewPasswordRegularExpressionErrorMessage = NewPasswordRegularExpressionErrorMessage;

                //ChangePasswordTemplate
                //have to initially force DisplayUserName true to access template
                _ctlChangePassword.DisplayUserName = true;
                helper = new TemplateHelper(_ctlChangePassword.ChangePasswordTemplateContainer);
                _ctlChangePassword.DisplayUserName = DisplayUserName;
                helper.SetText("ChangePasswordTitle", ChangePasswordTitleText);
                helper.SetText("Instruction", InstructionText);
                helper.SetVisible("UserNameRow", DisplayUserName);
                helper.SetText("UserNameLabel", UserNameLabelText);
                helper.SetValidation("UserNameRequired", UserNameRequiredErrorMessage, this.UniqueID);
                helper.SetText("CurrentPasswordLabel", PasswordLabelText);
                helper.SetValidation("CurrentPasswordRequired", PasswordRequiredErrorMessage, this.UniqueID);
                helper.SetText("NewPasswordLabel", NewPasswordLabelText);
                helper.SetValidation("NewPasswordRequired", NewPasswordRequiredErrorMessage, this.UniqueID);
                helper.SetText("PasswordHint", PasswordHintText);
                helper.SetVisible("PasswordHintRow", !String.IsNullOrEmpty(PasswordHintText));
                helper.SetText("ConfirmNewPasswordLabel", ConfirmNewPasswordLabelText);
                helper.SetValidation("ConfirmNewPasswordRequired", ConfirmPasswordRequiredErrorMessage, this.UniqueID);
                helper.SetValidation("ConfirmNewPasswordCompare", ConfirmPasswordCompareErrorMessage, this.UniqueID);

                switch (ChangePasswordButtonType)
                {
                    case ButtonType.Button:
                        helper.SetButton("ChangePasswordButton", ChangePasswordButtonText, this.UniqueID);
                        helper.SetVisible("ChangePasswordButton", true);
                        break;

                    case ButtonType.Image:
                        helper.SetImageButton("ChangePasswordImageButton", ChangePasswordButtonImageUrl, ChangePasswordButtonText, this.UniqueID);
                        helper.SetVisible("ChangePasswordImageButton", true);
                        break;

                    case ButtonType.Link:
                        helper.SetButton("ChangePasswordLinkButton", ChangePasswordButtonText, this.UniqueID);
                        helper.SetVisible("ChangePasswordLinkButton", true);
                        break;
                }

                switch (CancelButtonType)
                {
                    case ButtonType.Button:
                        helper.SetButton("CancelButton", CancelButtonText, this.UniqueID);
                        helper.SetVisible("CancelButton", true);
                        break;

                    case ButtonType.Image:
                        helper.SetImageButton("CancelImageButton", CancelButtonImageUrl, CancelButtonText, this.UniqueID);
                        helper.SetVisible("CancelImageButton", true);
                        break;

                    case ButtonType.Link:
                        helper.SetButton("CancelLinkButton", CancelButtonText, this.UniqueID);
                        helper.SetVisible("CancelLinkButton", true);
                        break;
                }

                helper.SetVisible("EditProfileRow", !String.IsNullOrEmpty(EditProfileUrl));
                helper.SetImage("EditProfileIcon", EditProfileIconUrl, EditProfileText,false);
                helper.SetLink("EditProfileLink", EditProfileText, EditProfileUrl);

                helper.SetVisible("HelpPageRow", !String.IsNullOrEmpty(HelpPageUrl));
                helper.SetImage("HelpPageIcon", HelpPageIconUrl, HelpPageText, false);
                helper.SetLink("HelpPageLink", HelpPageText, HelpPageUrl);

                helper.SetVisible("CreateUserRow", !String.IsNullOrEmpty(CreateUserUrl));
                helper.SetImage("CreateUserIcon", CreateUserIconUrl, CreateUserText, false);
                helper.SetLink("CreateUserLink", CreateUserText, CreateUserUrl);

                helper.SetVisible("PasswordRecoveryRow", !String.IsNullOrEmpty(PasswordRecoveryUrl));
                helper.SetImage("PasswordRecoveryIcon", PasswordRecoveryIconUrl, PasswordRecoveryText, false);
                helper.SetLink("PasswordRecoveryLink", PasswordRecoveryText, PasswordRecoveryUrl);

                //SuccessTemplate
                helper = new TemplateHelper(_ctlChangePassword.SuccessTemplateContainer);
                helper.SetText("Success", SuccessText);

                helper.SetVisible("EditProfileRow", !String.IsNullOrEmpty(EditProfileUrl));
                helper.SetImage("EditProfileIcon", EditProfileIconUrl, EditProfileText, false);
                helper.SetLink("EditProfileLink", EditProfileText, EditProfileUrl);

                switch (ContinueButtonType)
                {
                    case ButtonType.Button:
                        helper.SetButton("ContinueButton", ContinueButtonText, this.UniqueID);
                        helper.SetVisible("ContinueButton", true);
                        break;

                    case ButtonType.Image:
                        helper.SetImageButton("ContinueImageButton", ContinueButtonImageUrl, ContinueButtonText, this.UniqueID);
                        helper.SetVisible("ContinueImageButton", true);
                        break;

                    case ButtonType.Link:
                        helper.SetButton("ContinueLinkButton", ContinueButtonText, this.UniqueID);
                        helper.SetVisible("ContinueLinkButton", true);
                        break;
                }

                //_ctlChangePassword.ChangingPassword += new LoginCancelEventHandler(ctlChangePassword_ChangingPassword);
                _ctlChangePassword.ChangedPassword += new EventHandler(_ctlChangePassword_ChangedPassword);
                _ctlChangePassword.Load += new EventHandler(_ctlChangePassword_Load);
                    
                this.Controls.Add(_ctlChangePassword);
            }
            
        }

        void _ctlChangePassword_ChangedPassword(object sender, EventArgs e)
        {
            FormsAuthentication.SignOut();
            Microsoft.SharePoint.IdentityModel.SPClaimsUtility.AuthenticateFormsUser(new Uri(SPContext.Current.Web.Url), _ctlChangePassword.UserName, _ctlChangePassword.NewPassword);
        }

        void _ctlChangePassword_Load(object sender, EventArgs e)
        {
            _ctlChangePassword.UserName = Utils.GetCurrentUsername();
        }

        protected override void CreateChildControls()
        {
            AddChangePasswordControl();
        }

        protected override void RenderContents(HtmlTextWriter writer)
        {
            EnsureChildControls();
            base.RenderContents(writer);
        }

        #endregion

        #region Events
        //void ctlChangePassword_ChangingPassword(object sender, LoginCancelEventArgs e)
        //{
        //    string userLoginName = string.Empty;
        //    string userName = string.Empty;

        //    try
        //    {
        //        userLoginName = SPContext.Current.Web.CurrentUser.LoginName;
        //        userName = _ctlChangePassword.UserName;
        //        if (userLoginName.ToLower() == userName.ToLower())
        //        {
        //            userName = Utils.DecodeUsername(userName);
        //            _ctlChangePassword.UserName = userName;
        //        }
        //        else
        //        {
        //            //e.Cancel = true;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Utils.LogError(ex);
        //        this.Controls.Add(Utils.CreateErrorMessage(_resourceManager.GetString("Error_Message")));
        //    }
        //}
        #endregion
    }
}
