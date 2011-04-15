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

namespace Visigo.Sharepoint.FormsBasedAuthentication
{
    public class ChangePasswordWebPart : Microsoft.SharePoint.WebPartPages.WebPart
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
        [ResourcesAttribute("CancelButtonImageUrl_FriendlyName", "CancelButtonImageUrl_Category", "CancelButtonImageUrl_Description")]
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
        [ResourcesAttribute("CancelButtonText_FriendlyName", "CancelButtonText_Category", "CancelButtonText_Description")]
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
        [ResourcesAttribute("CancelButtonType_FriendlyName", "CancelButtonType_Category", "CancelButtonType_Description")]
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
        [ResourcesAttribute("CancelDestinationPageUrl_FriendlyName", "CancelDestinationPageUrl_Category", "CancelDestinationPageUrl_Description")]
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
        [ResourcesAttribute("ChangePasswordButtonImageUrl_FriendlyName", "ChangePasswordButtonImageUrl_Category", "ChangePasswordButtonImageUrl_Description")]
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
        [ResourcesAttribute("ChangePasswordButtonText_FriendlyName", "ChangePasswordButtonText_Category", "ChangePasswordButtonText_Description")]
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
        [ResourcesAttribute("ChangePasswordButtonType_FriendlyName", "ChangePasswordButtonType_Category", "ChangePasswordButtonType_Description")]
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
        [ResourcesAttribute("ChangePasswordFailureText_FriendlyName", "ChangePasswordFailureText_Category", "ChangePasswordFailureText_Description")]
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
        [ResourcesAttribute("ChangePasswordTitleText_FriendlyName", "ChangePasswordTitleText_Category", "ChangePasswordTitleText_Description")]
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
        [ResourcesAttribute("ConfirmNewPasswordLabelText_FriendlyName", "ConfirmNewPasswordLabelText_Category", "ConfirmNewPasswordLabelText_Description")]
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
        [ResourcesAttribute("ConfirmPasswordCompareErrorMessage_FriendlyName", "ConfirmPasswordCompareErrorMessage_Category", "ConfirmPasswordCompareErrorMessage_Description")]
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
        [ResourcesAttribute("ConfirmPasswordRequiredErrorMessage_FriendlyName", "ConfirmPasswordRequiredErrorMessage_Category", "ConfirmPasswordRequiredErrorMessage_Description")]
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
        [ResourcesAttribute("ContinueButtonImageUrl_FriendlyName", "ContinueButtonImageUrl_Category", "ContinueButtonImageUrl_Description")]
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
        [ResourcesAttribute("ContinueButtonText_FriendlyName", "ContinueButtonText_Category", "ContinueButtonText_Description")]
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
        [ResourcesAttribute("ContinueDestinationPageUrl_FriendlyName", "ContinueDestinationPageUrl_Category", "ContinueDestinationPageUrl_Description")]
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
        [ResourcesAttribute("CreateUserIconUrl_FriendlyName", "CreateUserIconUrl_Category", "CreateUserIconUrl_Description")]
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
        [ResourcesAttribute("CreateUserText_FriendlyName", "CreateUserText_Category", "CreateUserText_Description")]
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
        [ResourcesAttribute("CreateUserUrl_FriendlyName", "CreateUserUrl_Category", "CreateUserUrl_Description")]
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
        [ResourcesAttribute("DisplayUserName_FriendlyName", "DisplayUserName_Category", "DisplayUserName_Description")]
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
        [ResourcesAttribute("EditProfileIconUrl_FriendlyName", "EditProfileIconUrl_Category", "EditProfileIconUrl_Description")]
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
        [ResourcesAttribute("EditProfileText_FriendlyName", "EditProfileText_Category", "EditProfileText_Description")]
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
        [ResourcesAttribute("EditProfileUrl_FriendlyName", "EditProfileUrl_Category", "EditProfileUrl_Description")]
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
        [ResourcesAttribute("HelpPageIconUrl_FriendlyName", "HelpPageIconUrl_Category", "HelpPageIconUrl_Description")]
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
        [ResourcesAttribute("HelpPageText_FriendlyName", "HelpPageText_Category", "HelpPageText_Description")]
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
        [ResourcesAttribute("HelpPageUrl_FriendlyName", "HelpPageUrl_Category", "HelpPageUrl_Description")]
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
        [ResourcesAttribute("InstructionText_FriendlyName", "InstructionText_Category", "InstructionText_Description")]
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
        [ResourcesAttribute("NewPasswordLabelText_FriendlyName", "NewPasswordLabelText_Category", "NewPasswordLabelText_Description")]
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
        [ResourcesAttribute("NewPasswordRegularExpressionErrorMessage_FriendlyName", "NewPasswordRegularExpressionErrorMessage_Category", "NewPasswordRegularExpressionErrorMessage_Description")]
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
        [ResourcesAttribute("PasswordHintText_FriendlyName", "PasswordHintText_Category", "PasswordHintText_Description")]
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
        [ResourcesAttribute("PasswordLabelText_FriendlyName", "PasswordLabelText_Category", "PasswordLabelText_Description")]
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
        [ResourcesAttribute("PasswordRecoveryIconUrl_FriendlyName", "PasswordRecoveryIconUrl_Category", "PasswordRecoveryIconUrl_Description")]
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
        [ResourcesAttribute("PasswordRecoveryText_FriendlyName", "PasswordRecoveryText_Category", "PasswordRecoveryText_Description")]
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
        [ResourcesAttribute("PasswordRecoveryUrl_FriendlyName", "PasswordRecoveryUrl_Category", "PasswordRecoveryUrl_Description")]
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
        [ResourcesAttribute("PasswordRequiredErrorMessage_FriendlyName", "PasswordRequiredErrorMessage_Category", "PasswordRequiredErrorMessage_Description")]
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
        [ResourcesAttribute("SuccessPageUrl_FriendlyName", "SuccessPageUrl_Category", "SuccessPageUrl_Description")]
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
        [ResourcesAttribute("SuccessText_FriendlyName", "SuccessText_Category", "SuccessText_Description")]
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
        [ResourcesAttribute("UserNameLabelText_FriendlyName", "UserNameLabelText_Category", "UserNameLabelText_Description")]
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
        [ResourcesAttribute("UserNameRequiredErrorMessage_FriendlyName", "UserNameRequiredErrorMessage_Category", "UserNameRequiredErrorMessage_Description")]
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
                ContinueDestinationPageUrl = _resourceManager.GetString("ContinueDestinationPageUrl_DefaultValue");
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
            using (SPWeb _web = new SPSite(this.Page.Request.Url.ToString()).OpenWeb())
            {
                if (_web.CurrentUser == null)
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

        public override string LoadResource(string id)
        {
            return (_resourceManager.GetString(id));
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
