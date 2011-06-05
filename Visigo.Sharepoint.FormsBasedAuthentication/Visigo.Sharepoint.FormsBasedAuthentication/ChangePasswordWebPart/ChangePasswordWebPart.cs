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
        private ResourceManager _resourceManager;
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
        private string _continueButtonImageUrl = string.Empty;
        private string _continueButtonText = string.Empty;
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
        [LocalizedWebDisplayName(typeof(ChangePasswordWebPart), "CancelButtonImageUrl_FriendlyName")]
        [LocalizedCategory(typeof(ChangePasswordWebPart), "CancelButtonImageUrl_Category")]
        [LocalizedWebDescription(typeof(ChangePasswordWebPart), "CancelButtonImageUrl_Description")]
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
        [LocalizedWebDisplayName(typeof(ChangePasswordWebPart), "CancelButtonText_FriendlyName")]
        [LocalizedCategory(typeof(ChangePasswordWebPart), "CancelButtonText_Category")]
        [LocalizedWebDescription(typeof(ChangePasswordWebPart), "CancelButtonText_Description")]        
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
        [LocalizedWebDisplayName(typeof(ChangePasswordWebPart), "CancelButtonType_FriendlyName")]
        [LocalizedCategory(typeof(ChangePasswordWebPart), "CancelButtonType_Category")]
        [LocalizedWebDescription(typeof(ChangePasswordWebPart), "CancelButtonType_Description")]
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
        [LocalizedWebDisplayName(typeof(ChangePasswordWebPart), "CancelDestinationPageUrl_FriendlyName")]
        [LocalizedCategory(typeof(ChangePasswordWebPart), "CancelDestinationPageUrl_Category")]
        [LocalizedWebDescription(typeof(ChangePasswordWebPart), "CancelDestinationPageUrl_Description")]
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
        [LocalizedWebDisplayName(typeof(ChangePasswordWebPart), "ChangePasswordButtonImageUrl_FriendlyName")]
        [LocalizedCategory(typeof(ChangePasswordWebPart), "ChangePasswordButtonImageUrl_Category")]
        [LocalizedWebDescription(typeof(ChangePasswordWebPart), "ChangePasswordButtonImageUrl_Description")]
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
        [LocalizedWebDisplayName(typeof(ChangePasswordWebPart), "ChangePasswordButtonText_FriendlyName")]
        [LocalizedCategory(typeof(ChangePasswordWebPart), "ChangePasswordButtonText_Category")]
        [LocalizedWebDescription(typeof(ChangePasswordWebPart), "ChangePasswordButtonText_Description")]
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
        [LocalizedWebDisplayName(typeof(ChangePasswordWebPart), "ChangePasswordButtonType_FriendlyName")]
        [LocalizedCategory(typeof(ChangePasswordWebPart), "ChangePasswordButtonType_Category")]
        [LocalizedWebDescription(typeof(ChangePasswordWebPart), "ChangePasswordButtonType_Description")]
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
        [LocalizedWebDisplayName(typeof(ChangePasswordWebPart), "ChangePasswordFailureText_FriendlyName")]
        [LocalizedCategory(typeof(ChangePasswordWebPart), "ChangePasswordFailureText_Category")]
        [LocalizedWebDescription(typeof(ChangePasswordWebPart), "ChangePasswordFailureText_Description")]
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
        [LocalizedWebDisplayName(typeof(ChangePasswordWebPart), "ChangePasswordTitleText_FriendlyName")]
        [LocalizedCategory(typeof(ChangePasswordWebPart), "ChangePasswordTitleText_Category")]
        [LocalizedWebDescription(typeof(ChangePasswordWebPart), "ChangePasswordTitleText_Description")]
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
        [LocalizedWebDisplayName(typeof(ChangePasswordWebPart), "ConfirmNewPasswordLabelText_FriendlyName")]
        [LocalizedCategory(typeof(ChangePasswordWebPart), "ConfirmNewPasswordLabelText_Category")]
        [LocalizedWebDescription(typeof(ChangePasswordWebPart), "ConfirmNewPasswordLabelText_Description")]
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
        [LocalizedWebDisplayName(typeof(ChangePasswordWebPart), "ConfirmPasswordCompareErrorMessage_FriendlyName")]
        [LocalizedCategory(typeof(ChangePasswordWebPart), "ConfirmPasswordCompareErrorMessage_Category")]
        [LocalizedWebDescription(typeof(ChangePasswordWebPart), "ConfirmPasswordCompareErrorMessage_Description")]
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
        [LocalizedWebDisplayName(typeof(ChangePasswordWebPart), "ConfirmPasswordRequiredErrorMessage_FriendlyName")]
        [LocalizedCategory(typeof(ChangePasswordWebPart), "ConfirmPasswordRequiredErrorMessage_Category")]
        [LocalizedWebDescription(typeof(ChangePasswordWebPart), "ConfirmPasswordRequiredErrorMessage_Description")]
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
        [LocalizedWebDisplayName(typeof(ChangePasswordWebPart), "ContinueButtonImageUrl_FriendlyName")]
        [LocalizedCategory(typeof(ChangePasswordWebPart), "ContinueButtonImageUrl_Category")]
        [LocalizedWebDescription(typeof(ChangePasswordWebPart), "ContinueButtonImageUrl_Description")]
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
        [LocalizedWebDisplayName(typeof(ChangePasswordWebPart), "ContinueButtonText_FriendlyName")]
        [LocalizedCategory(typeof(ChangePasswordWebPart), "ContinueButtonText_Category")]
        [LocalizedWebDescription(typeof(ChangePasswordWebPart), "ContinueButtonText_Description")]
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
        [LocalizedWebDisplayName(typeof(ChangePasswordWebPart), "ContinueDestinationPageUrl_FriendlyName")]
        [LocalizedCategory(typeof(ChangePasswordWebPart), "ContinueDestinationPageUrl_Category")]
        [LocalizedWebDescription(typeof(ChangePasswordWebPart), "ContinueDestinationPageUrl_Description")]
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
        [LocalizedWebDisplayName(typeof(ChangePasswordWebPart), "CreateUserIconUrl_FriendlyName")]
        [LocalizedCategory(typeof(ChangePasswordWebPart), "CreateUserIconUrl_Category")]
        [LocalizedWebDescription(typeof(ChangePasswordWebPart), "CreateUserIconUrl_Description")]
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
        [LocalizedWebDisplayName(typeof(ChangePasswordWebPart), "CreateUserText_FriendlyName")]
        [LocalizedCategory(typeof(ChangePasswordWebPart), "CreateUserText_Category")]
        [LocalizedWebDescription(typeof(ChangePasswordWebPart), "CreateUserText_Description")]
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
        [LocalizedWebDisplayName(typeof(ChangePasswordWebPart), "CreateUserUrl_FriendlyName")]
        [LocalizedCategory(typeof(ChangePasswordWebPart), "CreateUserUrl_Category")]
        [LocalizedWebDescription(typeof(ChangePasswordWebPart), "CreateUserUrl_Description")]
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
        [LocalizedWebDisplayName(typeof(ChangePasswordWebPart), "DisplayUserName_FriendlyName")]
        [LocalizedCategory(typeof(ChangePasswordWebPart), "DisplayUserName_Category")]
        [LocalizedWebDescription(typeof(ChangePasswordWebPart), "DisplayUserName_Description")]
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
        [LocalizedWebDisplayName(typeof(ChangePasswordWebPart), "EditProfileIconUrl_FriendlyName")]
        [LocalizedCategory(typeof(ChangePasswordWebPart), "EditProfileIconUrl_Category")]
        [LocalizedWebDescription(typeof(ChangePasswordWebPart), "EditProfileIconUrl_Description")]
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
        [LocalizedWebDisplayName(typeof(ChangePasswordWebPart), "EditProfileText_FriendlyName")]
        [LocalizedCategory(typeof(ChangePasswordWebPart), "EditProfileText_Category")]
        [LocalizedWebDescription(typeof(ChangePasswordWebPart), "EditProfileText_Description")]
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
        [LocalizedWebDisplayName(typeof(ChangePasswordWebPart), "EditProfileUrl_FriendlyName")]
        [LocalizedCategory(typeof(ChangePasswordWebPart), "EditProfileUrl_Category")]
        [LocalizedWebDescription(typeof(ChangePasswordWebPart), "EditProfileUrl_Description")]
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
        [LocalizedWebDisplayName(typeof(ChangePasswordWebPart), "HelpPageIconUrl_FriendlyName")]
        [LocalizedCategory(typeof(ChangePasswordWebPart), "HelpPageIconUrl_Category")]
        [LocalizedWebDescription(typeof(ChangePasswordWebPart), "HelpPageIconUrl_Description")]
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
        [LocalizedWebDisplayName(typeof(ChangePasswordWebPart), "HelpPageText_FriendlyName")]
        [LocalizedCategory(typeof(ChangePasswordWebPart), "HelpPageText_Category")]
        [LocalizedWebDescription(typeof(ChangePasswordWebPart), "HelpPageText_Description")]
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
        [LocalizedWebDisplayName(typeof(ChangePasswordWebPart), "HelpPageUrl_FriendlyName")]
        [LocalizedCategory(typeof(ChangePasswordWebPart), "HelpPageUrl_Category")]
        [LocalizedWebDescription(typeof(ChangePasswordWebPart), "HelpPageUrl_Description")]
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
        [LocalizedWebDisplayName(typeof(ChangePasswordWebPart), "InstructionText_FriendlyName")]
        [LocalizedCategory(typeof(ChangePasswordWebPart), "InstructionText_Category")]
        [LocalizedWebDescription(typeof(ChangePasswordWebPart), "InstructionText_Description")]
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
        [LocalizedWebDisplayName(typeof(ChangePasswordWebPart), "NewPasswordLabelText_FriendlyName")]
        [LocalizedCategory(typeof(ChangePasswordWebPart), "NewPasswordLabelText_Category")]
        [LocalizedWebDescription(typeof(ChangePasswordWebPart), "NewPasswordLabelText_Description")]
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
        [LocalizedWebDisplayName(typeof(ChangePasswordWebPart), "NewPasswordRegularExpressionErrorMessage_FriendlyName")]
        [LocalizedCategory(typeof(ChangePasswordWebPart), "NewPasswordRegularExpressionErrorMessage_Category")]
        [LocalizedWebDescription(typeof(ChangePasswordWebPart), "NewPasswordRegularExpressionErrorMessage_Description")]
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
        [LocalizedWebDisplayName(typeof(ChangePasswordWebPart), "PasswordHintText_FriendlyName")]
        [LocalizedCategory(typeof(ChangePasswordWebPart), "PasswordHintText_Category")]
        [LocalizedWebDescription(typeof(ChangePasswordWebPart), "PasswordHintText_Description")]
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
        [LocalizedWebDisplayName(typeof(ChangePasswordWebPart), "PasswordLabelText_FriendlyName")]
        [LocalizedCategory(typeof(ChangePasswordWebPart), "PasswordLabelText_Category")]
        [LocalizedWebDescription(typeof(ChangePasswordWebPart), "PasswordLabelText_Description")]
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
        [LocalizedWebDisplayName(typeof(ChangePasswordWebPart), "PasswordRecoveryIconUrl_FriendlyName")]
        [LocalizedCategory(typeof(ChangePasswordWebPart), "PasswordRecoveryIconUrl_Category")]
        [LocalizedWebDescription(typeof(ChangePasswordWebPart), "PasswordRecoveryIconUrl_Description")]
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
        [LocalizedWebDisplayName(typeof(ChangePasswordWebPart), "PasswordRecoveryText_FriendlyName")]
        [LocalizedCategory(typeof(ChangePasswordWebPart), "PasswordRecoveryText_Category")]
        [LocalizedWebDescription(typeof(ChangePasswordWebPart), "PasswordRecoveryText_Description")]
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
        [LocalizedWebDisplayName(typeof(ChangePasswordWebPart), "PasswordRecoveryUrl_FriendlyName")]
        [LocalizedCategory(typeof(ChangePasswordWebPart), "PasswordRecoveryUrl_Category")]
        [LocalizedWebDescription(typeof(ChangePasswordWebPart), "PasswordRecoveryUrl_Description")]
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
        [LocalizedWebDisplayName(typeof(ChangePasswordWebPart), "PasswordRequiredErrorMessage_FriendlyName")]
        [LocalizedCategory(typeof(ChangePasswordWebPart), "PasswordRequiredErrorMessage_Category")]
        [LocalizedWebDescription(typeof(ChangePasswordWebPart), "PasswordRequiredErrorMessage_Description")]
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
        [LocalizedWebDisplayName(typeof(ChangePasswordWebPart), "SuccessPageUrl_FriendlyName")]
        [LocalizedCategory(typeof(ChangePasswordWebPart), "SuccessPageUrl_Category")]
        [LocalizedWebDescription(typeof(ChangePasswordWebPart), "SuccessPageUrl_Description")]
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
        [LocalizedWebDisplayName(typeof(ChangePasswordWebPart), "SuccessText_FriendlyName")]
        [LocalizedCategory(typeof(ChangePasswordWebPart), "SuccessText_Category")]
        [LocalizedWebDescription(typeof(ChangePasswordWebPart), "SuccessText_Description")]
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
        [LocalizedWebDisplayName(typeof(ChangePasswordWebPart), "UserNameLabelText_FriendlyName")]
        [LocalizedCategory(typeof(ChangePasswordWebPart), "UserNameLabelText_Category")]
        [LocalizedWebDescription(typeof(ChangePasswordWebPart), "UserNameLabelText_Description")]
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
        [LocalizedWebDisplayName(typeof(ChangePasswordWebPart), "UserNameRequiredErrorMessage_FriendlyName")]
        [LocalizedCategory(typeof(ChangePasswordWebPart), "UserNameRequiredErrorMessage_Category")]
        [LocalizedWebDescription(typeof(ChangePasswordWebPart), "UserNameRequiredErrorMessage_Description")]
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
            _resourceManager = new ResourceManager("Visigo.Sharepoint.FormsBasedAuthentication.ChangePasswordWebPart", Assembly.GetExecutingAssembly());
            try
            {
                CancelButtonImageUrl = _resourceManager.GetString("CancelButtonImageUrl_DefaultValue");
                CancelButtonText = _resourceManager.GetString("CancelButtonText_DefaultValue");
                CancelButtonType = (ButtonType)Convert.ToInt32(_resourceManager.GetString("CancelButtonType_DefaultValue"), CultureInfo.InvariantCulture);
                CancelDestinationPageUrl = _resourceManager.GetString("CancelDestinationPageUrl_DefaultValue");
                ChangePasswordButtonImageUrl = _resourceManager.GetString("ChangePasswordButtonImageUrl_DefaultValue");
                ChangePasswordButtonText = _resourceManager.GetString("ChangePasswordButtonText_DefaultValue");
                ChangePasswordButtonType = (ButtonType)Convert.ToInt32(_resourceManager.GetString("ChangePasswordButtonType_DefaultValue"), CultureInfo.InvariantCulture);
                ChangePasswordFailureText = _resourceManager.GetString("ChangePasswordFailureText_DefaultValue");
                ChangePasswordTitleText = _resourceManager.GetString("ChangePasswordTitleText_DefaultValue");
                ConfirmNewPasswordLabelText = _resourceManager.GetString("ConfirmNewPasswordLabelText_DefaultValue");
                ConfirmPasswordCompareErrorMessage = _resourceManager.GetString("ConfirmPasswordCompareErrorMessage_DefaultValue");
                ConfirmPasswordRequiredErrorMessage = _resourceManager.GetString("ConfirmPasswordRequiredErrorMessage_DefaultValue");
                ContinueButtonImageUrl = _resourceManager.GetString("ContinueButtonImageUrl_DefaultValue");
                ContinueButtonText = _resourceManager.GetString("ContinueButtonText_DefaultValue");
                CreateUserIconUrl = _resourceManager.GetString("CreateUserIconUrl_DefaultValue");
                CreateUserText = _resourceManager.GetString("CreateUserText_DefaultValue");
                CreateUserUrl = _resourceManager.GetString("CreateUserUrl_DefaultValue");
                DisplayUserName = Convert.ToBoolean(_resourceManager.GetString("DisplayUserName_DefaultValue"), CultureInfo.InvariantCulture);
                EditProfileIconUrl = _resourceManager.GetString("EditProfileIconUrl_DefaultValue");
                EditProfileText = _resourceManager.GetString("EditProfileText_DefaultValue");
                EditProfileUrl = _resourceManager.GetString("EditProfileUrl_DefaultValue");
                HelpPageIconUrl = _resourceManager.GetString("HelpPageIconUrl_DefaultValue");
                HelpPageText = _resourceManager.GetString("HelpPageText_DefaultValue");
                HelpPageUrl = _resourceManager.GetString("HelpPageUrl_DefaultValue");
                InstructionText = _resourceManager.GetString("InstructionText_DefaultValue");
                NewPasswordLabelText = _resourceManager.GetString("NewPasswordLabelText_DefaultValue");
                NewPasswordRegularExpressionErrorMessage = _resourceManager.GetString("NewPasswordRegularExpressionErrorMessage_DefaultValue");
                PasswordHintText = _resourceManager.GetString("PasswordHintText_DefaultValue");
                PasswordLabelText = _resourceManager.GetString("PasswordLabelText_DefaultValue");
                PasswordRecoveryIconUrl = _resourceManager.GetString("PasswordRecoveryIconUrl_DefaultValue");
                PasswordRecoveryText = _resourceManager.GetString("PasswordRecoveryText_DefaultValue");
                PasswordRecoveryUrl = _resourceManager.GetString("PasswordRecoveryUrl_DefaultValue");
                PasswordRequiredErrorMessage = _resourceManager.GetString("PasswordRequiredErrorMessage_DefaultValue");
                SuccessPageUrl = _resourceManager.GetString("SuccessPageUrl_DefaultValue");
                SuccessText = _resourceManager.GetString("SuccessText_DefaultValue");
                UserNameLabelText = _resourceManager.GetString("UserNameLabelText_DefaultValue");
                UserNameRequiredErrorMessage = _resourceManager.GetString("UserNameRequiredErrorMessage_DefaultValue");

                //ContinueDestinationPageUrl = _resourceManager.GetString("ContinueDestinationPageUrl_DefaultValue");
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
            if (SPContext.Current.Web.CurrentUser == null)
            {
                //Login Control won't work with SP2010, so for now, just don't show a control at all
                //Login ctlLogin = new Login();
                //this.Controls.Add(ctlLogin);
            }
            else
            {
                _ctlChangePassword = new System.Web.UI.WebControls.ChangePassword();
                _ctlChangePassword.MembershipProvider = Utils.GetMembershipProvider(Context);
                _ctlChangePassword.CancelButtonImageUrl = CancelButtonImageUrl;
                _ctlChangePassword.CancelButtonText = CancelButtonText;
                _ctlChangePassword.CancelButtonType = CancelButtonType;
                _ctlChangePassword.CancelDestinationPageUrl = CancelDestinationPageUrl;
                _ctlChangePassword.ChangePasswordButtonImageUrl = ChangePasswordButtonImageUrl;
                _ctlChangePassword.ChangePasswordButtonText = ChangePasswordButtonText;
                _ctlChangePassword.ChangePasswordButtonType = ChangePasswordButtonType;
                _ctlChangePassword.ChangePasswordFailureText = ChangePasswordFailureText;
                _ctlChangePassword.ChangePasswordTitleText = ChangePasswordTitleText;
                _ctlChangePassword.ConfirmNewPasswordLabelText = ConfirmNewPasswordLabelText;
                _ctlChangePassword.ConfirmPasswordCompareErrorMessage = ConfirmPasswordCompareErrorMessage;
                _ctlChangePassword.ConfirmPasswordRequiredErrorMessage = ConfirmPasswordRequiredErrorMessage;
                _ctlChangePassword.ContinueButtonImageUrl = ContinueButtonImageUrl;
                _ctlChangePassword.ContinueButtonText = ContinueButtonText;
                _ctlChangePassword.ContinueDestinationPageUrl = ContinueDestinationPageUrl;
                _ctlChangePassword.CreateUserIconUrl = CreateUserIconUrl;
                _ctlChangePassword.CreateUserText = CreateUserText;
                _ctlChangePassword.CreateUserUrl = CreateUserUrl;
                _ctlChangePassword.DisplayUserName = DisplayUserName;
                _ctlChangePassword.EditProfileIconUrl = EditProfileIconUrl;
                _ctlChangePassword.EditProfileText = EditProfileText;
                _ctlChangePassword.EditProfileUrl = EditProfileUrl;
                _ctlChangePassword.HelpPageIconUrl = HelpPageIconUrl;
                _ctlChangePassword.HelpPageText = HelpPageText;
                _ctlChangePassword.HelpPageUrl = HelpPageUrl;
                _ctlChangePassword.InstructionText = InstructionText;
                _ctlChangePassword.NewPasswordLabelText = NewPasswordLabelText;
                _ctlChangePassword.NewPasswordRegularExpressionErrorMessage = NewPasswordRegularExpressionErrorMessage;
                _ctlChangePassword.PasswordHintText = PasswordHintText;
                _ctlChangePassword.PasswordLabelText = PasswordLabelText;
                _ctlChangePassword.PasswordRecoveryIconUrl = PasswordRecoveryIconUrl;
                _ctlChangePassword.PasswordRecoveryText = PasswordRecoveryText;
                _ctlChangePassword.PasswordRecoveryUrl = PasswordRecoveryUrl;
                _ctlChangePassword.PasswordRequiredErrorMessage = PasswordRequiredErrorMessage;
                _ctlChangePassword.SuccessPageUrl = SuccessPageUrl;
                _ctlChangePassword.SuccessText = SuccessText;
                _ctlChangePassword.ToolTip = ToolTip;
                _ctlChangePassword.UserNameLabelText = UserNameLabelText;
                _ctlChangePassword.UserNameRequiredErrorMessage = UserNameRequiredErrorMessage;

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
