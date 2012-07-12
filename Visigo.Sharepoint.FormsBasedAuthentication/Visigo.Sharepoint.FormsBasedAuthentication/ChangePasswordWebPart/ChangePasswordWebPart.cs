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
        private LocalizedString _resourceManager = new LocalizedString("FBAPackChangePasswordWebPart");

        private string _changePasswordTemplate = "/_layouts/FBA/WEBPARTS/ChangePasswordWebPart/ChangePasswordTemplate.ascx";
        private string _successTemplate = "/_layouts/FBA/WEBPARTS/ChangePasswordWebPart/SuccessTemplate.ascx";
        private string _cancelButtonImageUrl = null;
        private string _cancelButtonText = null;
        private ButtonType _cancelButtonType = ButtonType.Button;
        private string _cancelDestinationPageUrl = null;
        private string _changePasswordButtonImageUrl = null;
        private string _changePasswordButtonText = null;
        private ButtonType _changePasswordButtonType = ButtonType.Button;
        private string _changePasswordFailureText = null;
        private string _changePasswordTitleText = null;
        private string _confirmNewPasswordLabelText = null;
        private string _confirmPasswordCompareErrorMessage = null;
        private string _confirmPasswordRequiredErrorMessage = null;
        private string _newPasswordRequiredErrorMessage = null;
        private string _continueButtonImageUrl = null;
        private string _continueButtonText = null;
        private ButtonType _continueButtonType = ButtonType.Button;
        private string _continueDestinationPageUrl = null;
        private string _createUserIconUrl = null;
        private string _createUserText = null;
        private string _createUserUrl = null;
        private bool _displayUserName = false;
        private string _editProfileIconUrl = null;
        private string _editProfileText = null;
        private string _editProfileUrl = null;
        private string _helpPageIconUrl = null;
        private string _helpPageText = null;
        private string _helpPageUrl = null;
        private string _instructionText = null;
        private string _newPasswordLabelText = null;
        private string _newPasswordRegularExpressionErrorMessage = null;
        private string _passwordHintText = null;
        private string _passwordLabelText = null;
        private string _passwordRecoveryIconUrl = null;
        private string _passwordRecoveryText = null;
        private string _passwordRecoveryUrl = null;
        private string _passwordRequiredErrorMessage = null;
        private string _successPageUrl = null;
        private string _successText = null;
        //private string _toolTip = string.Empty;
        private string _userNameLabelText = null;
        private string _userNameRequiredErrorMessage = null;
        #endregion

        #region Controls
        private System.Web.UI.WebControls.ChangePassword _ctlChangePassword;
        #endregion

        #region Properties
        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [LocalizedWebDisplayName("FBAPackChangePasswordWebPart", "ChangePasswordTemplate_FriendlyName")]
        [LocalizedCategory("FBAPackChangePasswordWebPart", "ChangePasswordTemplate_Category")]
        [LocalizedWebDescription("FBAPackChangePasswordWebPart", "ChangePasswordTemplate_Description")]
        public string ChangePasswordTemplate
        {
            get
            {
                return _changePasswordTemplate;
            }
            set
            {
                _changePasswordTemplate = value;
            }
        }


        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [LocalizedWebDisplayName("FBAPackChangePasswordWebPart", "SuccessTemplate_FriendlyName")]
        [LocalizedCategory("FBAPackChangePasswordWebPart", "SuccessTemplate_Category")]
        [LocalizedWebDescription("FBAPackChangePasswordWebPart", "SuccessTemplate_Description")]
        public string SuccessTemplate
        {
            get
            {
                return _successTemplate;
            }
            set
            {
                _successTemplate = value;
            }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [LocalizedWebDisplayName("FBAPackChangePasswordWebPart", "CancelButtonImageUrl_FriendlyName")]
        [LocalizedCategory("FBAPackChangePasswordWebPart", "CancelButtonImageUrl_Category")]
        [LocalizedWebDescription("FBAPackChangePasswordWebPart", "CancelButtonImageUrl_Description")]
        public string CancelButtonImageUrl
        {
            get
            {
                if (_cancelButtonImageUrl != null)
                {
                    return _cancelButtonImageUrl;
                }
                return _resourceManager.GetString("CancelButtonImageUrl_DefaultValue");
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
                if (_cancelButtonText != null)
                {
                    return _cancelButtonText;
                }
                return _resourceManager.GetString("CancelButtonText_DefaultValue");
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
                if (_cancelDestinationPageUrl != null)
                {
                    return _cancelDestinationPageUrl;
                }
                return _resourceManager.GetString("CancelDestinationPageUrl_DefaultValue");
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
                if (_changePasswordButtonImageUrl != null)
                {
                    return _changePasswordButtonImageUrl;
                }
                return _resourceManager.GetString("ChangePasswordButtonImageUrl_DefaultValue");
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
                if (_changePasswordButtonText != null)
                {
                    return _changePasswordButtonText;
                }
                return _resourceManager.GetString("ChangePasswordButtonText_DefaultValue");
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
                if (_changePasswordFailureText != null)
                {
                    return _changePasswordFailureText;
                }
                return _resourceManager.GetString("ChangePasswordFailureText_DefaultValue");
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
                if (_changePasswordTitleText != null)
                {
                    return _changePasswordTitleText;
                }
                return _resourceManager.GetString("ChangePasswordTitleText_DefaultValue");
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
                if (_confirmNewPasswordLabelText != null)
                {
                    return _confirmNewPasswordLabelText;
                }
                return _resourceManager.GetString("ConfirmNewPasswordLabelText_DefaultValue");
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
                if (_confirmPasswordCompareErrorMessage != null)
                {
                    return _confirmPasswordCompareErrorMessage;
                }
                return _resourceManager.GetString("ConfirmPasswordCompareErrorMessage_DefaultValue");
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
                if (_confirmPasswordRequiredErrorMessage != null)
                {
                    return _confirmPasswordRequiredErrorMessage;
                }
                return _resourceManager.GetString("ConfirmPasswordRequiredErrorMessage_DefaultValue");
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
                if (_newPasswordRequiredErrorMessage != null)
                {
                    return _newPasswordRequiredErrorMessage;
                }
                return _resourceManager.GetString("NewPasswordRequiredErrorMessage_DefaultValue");
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
                if (_continueButtonImageUrl != null)
                {
                    return _continueButtonImageUrl;
                }
                return _resourceManager.GetString("ContinueButtonImageUrl_DefaultValue");
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
                if (_continueButtonText != null)
                {
                    return _continueButtonText;
                }
                return _resourceManager.GetString("ContinueButtonText_DefaultValue");
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
                if (_continueDestinationPageUrl != null)
                {
                    return _continueDestinationPageUrl;
                }
                return _resourceManager.GetString("ContinueDestinationPageUrl_DefaultValue");
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
                if (_createUserIconUrl != null)
                {
                    return _createUserIconUrl;
                }
                return _resourceManager.GetString("CreateUserIconUrl_DefaultValue");
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
                if (_createUserText != null)
                {
                    return _createUserText;
                }
                return _resourceManager.GetString("CreateUserText_DefaultValue");
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
                if (_createUserUrl != null)
                {
                    return _createUserUrl;
                }
                return _resourceManager.GetString("CreateUserUrl_DefaultValue");
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
                if (_editProfileIconUrl != null)
                {
                    return _editProfileIconUrl;
                }
                return _resourceManager.GetString("EditProfileIconUrl_DefaultValue");
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
                if (_editProfileText != null)
                {
                    return _editProfileText;
                }
                return _resourceManager.GetString("EditProfileText_DefaultValue");
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
                if (_editProfileUrl != null)
                {
                    return _editProfileUrl;
                }
                return _resourceManager.GetString("EditProfileUrl_DefaultValue");
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
                if (_helpPageIconUrl != null)
                {
                    return _helpPageIconUrl;
                }
                return _resourceManager.GetString("HelpPageIconUrl_DefaultValue");
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
                if (_helpPageText != null)
                {
                    return _helpPageText;
                }
                return _resourceManager.GetString("HelpPageText_DefaultValue");
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
                if (_helpPageUrl != null)
                {
                    return _helpPageUrl;
                }
                return _resourceManager.GetString("HelpPageUrl_DefaultValue");
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
                if (_instructionText != null)
                {
                    return _instructionText;
                }
                return _resourceManager.GetString("InstructionText_DefaultValue");
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
                if (_newPasswordLabelText != null)
                {
                    return _newPasswordLabelText;
                }
                return _resourceManager.GetString("NewPasswordLabelText_DefaultValue");
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
                if (_newPasswordRegularExpressionErrorMessage != null)
                {
                    return _newPasswordRegularExpressionErrorMessage;
                }
                return _resourceManager.GetString("NewPasswordRegularExpressionErrorMessage_DefaultValue");
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
                if (_passwordHintText != null)
                {
                    return _passwordHintText;
                }
                return _resourceManager.GetString("PasswordHintText_DefaultValue");
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
                if (_passwordLabelText != null)
                {
                    return _passwordLabelText;
                }
                return _resourceManager.GetString("PasswordLabelText_DefaultValue");
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
                if (_passwordRecoveryIconUrl != null)
                {
                    return _passwordRecoveryIconUrl;
                }
                return _resourceManager.GetString("PasswordRecoveryIconUrl_DefaultValue");
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
                if (_passwordRecoveryText != null)
                {
                    return _passwordRecoveryText;
                }
                return _resourceManager.GetString("PasswordRecoveryText_DefaultValue");
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
                if (_passwordRecoveryUrl != null)
                {
                    return _passwordRecoveryUrl;
                }
                return _resourceManager.GetString("PasswordRecoveryUrl_DefaultValue");
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
                if (_passwordRequiredErrorMessage != null)
                {
                    return _passwordRequiredErrorMessage;
                }
                return _resourceManager.GetString("PasswordRequiredErrorMessage_DefaultValue");
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
                if (_successPageUrl != null)
                {
                    return _successPageUrl;
                }
                return _resourceManager.GetString("SuccessPageUrl_DefaultValue");
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
                if (_successText != null)
                {
                    return _successText;
                }
                return _resourceManager.GetString("SuccessText_DefaultValue");
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
                if (_userNameLabelText != null)
                {
                    return _userNameLabelText;
                }
                return _resourceManager.GetString("UserNameLabelText_DefaultValue");
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
                if (_userNameRequiredErrorMessage != null)
                {
                    return _userNameRequiredErrorMessage;
                }
                return _resourceManager.GetString("UserNameRequiredErrorMessage_DefaultValue");
            }
            set
            {
                _userNameRequiredErrorMessage = value;
            }
        }

        #endregion

        #region Methods
        private void AddChangePasswordControl()
        {
            TemplateHelper helper;

            string provider = Utils.GetMembershipProvider(Context);
            //Exit if membership provider not defined
            if (provider == null || !Utils.IsProviderConfigured())
            {
                Controls.Add(new LiteralControl(LocalizedString.GetString("FBAPackFeatures", "MembershipNotConfigured")));
                return;
            }

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
                _ctlChangePassword.MembershipProvider = provider;
                _ctlChangePassword.ChangePasswordFailureText = ChangePasswordFailureText;

                if (!String.IsNullOrEmpty(CancelDestinationPageUrl))
                {
                    _ctlChangePassword.CancelDestinationPageUrl = CancelDestinationPageUrl;
                }
                else
                {
                    string url = SPUtility.OriginalServerRelativeRequestUrl;
                    SPUtility.DetermineRedirectUrl(url, SPRedirectFlags.UseSource, this.Context, null, out url);
                    _ctlChangePassword.CancelDestinationPageUrl = url;
                }

                if (!String.IsNullOrEmpty(ContinueDestinationPageUrl))
                {
                    _ctlChangePassword.ContinueDestinationPageUrl = ContinueDestinationPageUrl;
                }
                else
                {
                    string url = SPUtility.OriginalServerRelativeRequestUrl;
                    SPUtility.DetermineRedirectUrl(url, SPRedirectFlags.UseSource, this.Context, null, out url);
                    _ctlChangePassword.ContinueDestinationPageUrl = url;
                }
                
                
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
            //Need to remove authentication cookie created by ChangePassword control.
            FormsAuthentication.SignOut();
            if (_ctlChangePassword.UserName == Utils.GetCurrentUsername())
            {                
                Microsoft.SharePoint.IdentityModel.SPClaimsUtility.AuthenticateFormsUser(new Uri(SPContext.Current.Web.Url), _ctlChangePassword.UserName, _ctlChangePassword.NewPassword);
            }
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
