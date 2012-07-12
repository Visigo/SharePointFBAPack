using System;
using System.Runtime.InteropServices;
using System.Web.UI;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;
using System.Web.Security;
using System.Web;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Globalization;
using System.Security.Permissions;
using System.Resources;
using System.Reflection;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using Visigo.Sharepoint.FormsBasedAuthentication.HIP;
using Microsoft.SharePoint.Administration;
using Microsoft.SharePoint.Utilities;

namespace Visigo.Sharepoint.FormsBasedAuthentication
{
    public class MembershipRequestWebPart : System.Web.UI.WebControls.WebParts.WebPart
    {
        #region Fields
        private LocalizedString _resourceManager = new LocalizedString("FBAPackMembershipRequestWebPart");
        MembershipSettings _Settings = new MembershipSettings(SPContext.Current.Web);

        protected string[] _randCharacters = { "A","B","C","D","E","F","G","H","J","K","L","M","N","P","Q","R","S","T","U","V","W","X","Y","Z",
                "2","3","4","5","6","7","8","9",
                "a","b","c","d","e","f","g","h","j","k","m","n","p","q","r","s","t","u","v","w","x","y","z"};

        private string _CreateUserStepTemplate = "/_layouts/FBA/WEBPARTS/MembershipRequestWebPart/CreateUserStepTemplate.ascx";
        private string _CompleteStepTemplate = "/_layouts/FBA/WEBPARTS/MembershipRequestWebPart/CompleteStepTemplate.ascx";
        private string _AnswerLabelText = null;
        private string _AnswerRequiredErrorMessage = null;
        private bool _AutoGeneratePassword = true;
        private string _CancelButtonImageUrl = null;
        private string _CancelButtonText = null;
        private ButtonType _CancelButtonType = ButtonType.Button;
        private string _CancelDestinationPageUrl = null;
        private bool _CaptchaValidation = true;
        private string _CompleteSuccessText = null;
        private string _ContinueButtonImageUrl = null;
        private string _ContinueButtonText = null;
        private ButtonType _ContinueButtonType = ButtonType.Button;
        private string _ConfirmPasswordCompareErrorMessage = null;
        private string _ConfirmPasswordLabelText = null;
        private string _ConfirmPasswordRequiredErrorMessage = null;
        private string _PasswordLabelText = null;
        private string _PasswordRequiredErrorMessage = null;
        private string _CreateUserButtonImageUrl = null;
        private string _CreateUserButtonText = null;
        private ButtonType _CreateUserButtonType = ButtonType.Button;
        private string _CssClass = null;
        private bool _DisplayCancelButton = false;
        private string _DuplicateEmailErrorMessage = null;
        private string _DuplicateUserNameErrorMessage = null;
        private string _EditProfileIconUrl = null;
        private string _EditProfileText = null;
        private string _EditProfileUrl = null;
        private string _EmailLabelText = null;
        private string _EmailRegularExpressionErrorMessage = null;
        private string _EmailRequiredErrorMessage = null;
        private string _FinishDestinationPageUrl = null;
        private string _HeaderText = null;
        private string _InstructionText = null;
        private string _InvalidAnswerErrorMessage = null;
        private string _InvalidEmailErrorMessage = null;
        private string _InvalidPasswordErrorMessage = null;
        private string _InvalidQuestionErrorMessage = null;
        private bool _LoginCreatedUser = false;
        private string _QuestionLabelText = null;
        private string _QuestionRequiredErrorMessage = null;
        private string _UnknownErrorMessage = null;
        private string _UserNameLabelText = null;
        private string _UserNameRequiredErrorMessage = null;
        private string _FirstNameLabelText = null;
        private string _LastNameLabelText = null;
        private string _FirstNameRequiredErrorMessage = null;
        private string _LastNameRequiredErrorMessage = null;
        private string _HipPictureLabelText = null;
        private string _HipCharactersLabelText = null;
        private string _HipInstructionsLabelText = null;
        private string _HipPictureDescription = null;
        private string _HipResetLabelText = null;
        private string _HipErrorMessage = null;
        private string _GroupName = null;

        #endregion


        #region Properties
        /// <summary>
        ///     This property is set using a custom editor
        /// </summary>
        public string GroupName
        {
            get
            {
                if (_GroupName != null)
                {
                    return _GroupName;
                }

                //Set default group name to first group in group list
                _GroupName = "";
                SPSecurity.RunWithElevatedPrivileges(delegate()
                {
                    using (SPSite site = new SPSite(SPContext.Current.Site.ID, SPContext.Current.Site.Zone))
                    {
                        using (SPWeb web = site.OpenWeb())
                        {

                            SPGroupCollection groups = web.SiteGroups;
                            if (groups.Count > 0)
                            {
                                _GroupName = groups[0].Name;
                            }
                        }
                    }
                });
                return _GroupName;
            }
            set
            {
                _GroupName = value;
            }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [LocalizedWebDisplayName("FBAPackMembershipRequestWebPart", "CreateUserStepTemplate_FriendlyName")]
        [LocalizedCategory("FBAPackMembershipRequestWebPart", "CreateUserStepTemplate_Category")]
        [LocalizedWebDescription("FBAPackMembershipRequestWebPart", "CreateUserStepTemplate_Description")]
        public string CreateUserStepTemplate
        {
            get
            {
                return _CreateUserStepTemplate;
            }
            set
            {
                _CreateUserStepTemplate = value;
            }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [LocalizedWebDisplayName("FBAPackMembershipRequestWebPart", "CompleteStepTemplate_FriendlyName")]
        [LocalizedCategory("FBAPackMembershipRequestWebPart", "CompleteStepTemplate_Category")]
        [LocalizedWebDescription("FBAPackMembershipRequestWebPart", "CompleteStepTemplate_Description")]
        public string CompleteStepTemplate
        {
            get
            {
                return _CompleteStepTemplate;
            }
            set
            {
                _CompleteStepTemplate = value;
            }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [LocalizedWebDisplayName("FBAPackMembershipRequestWebPart", "AnswerLabelText_FriendlyName")]
        [LocalizedCategory("FBAPackMembershipRequestWebPart", "AnswerLabelText_Category")]
        [LocalizedWebDescription("FBAPackMembershipRequestWebPart", "AnswerLabelText_Description")]
        public string AnswerLabelText
        {
            get
            {
                if (_AnswerLabelText != null)
                {
                    return _AnswerLabelText;
                }
                return _resourceManager.GetString("AnswerLabelText_DefaultValue");
            }
            set
            {
                _AnswerLabelText = value;
                if (cuw != null) cuw.AnswerLabelText = value;
            }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [LocalizedWebDisplayName("FBAPackMembershipRequestWebPart", "AnswerRequiredErrorMessage_FriendlyName")]
        [LocalizedCategory("FBAPackMembershipRequestWebPart", "AnswerRequiredErrorMessage_Category")]
        [LocalizedWebDescription("FBAPackMembershipRequestWebPart", "AnswerRequiredErrorMessage_Description")]
        public string AnswerRequiredErrorMessage
        {
            get
            {
                if (_AnswerRequiredErrorMessage != null)
                {
                    return _AnswerRequiredErrorMessage;
                }
                return _resourceManager.GetString("AnswerRequiredErrorMessage_DefaultValue");
            }
            set { _AnswerRequiredErrorMessage = value; }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [LocalizedWebDisplayName("FBAPackMembershipRequestWebPart", "AutoGeneratePassword_FriendlyName")]
        [LocalizedCategory("FBAPackMembershipRequestWebPart", "AutoGeneratePassword_Category")]
        [LocalizedWebDescription("FBAPackMembershipRequestWebPart", "AutoGeneratePassword_Description")]
        public bool AutoGeneratePassword
        {
            get
            {
                //Passwords must be auto generated if reviewing membership requests
                if (_Settings.ReviewMembershipRequests)
                {
                    return true;
                }
                return _AutoGeneratePassword;
            }
            set { _AutoGeneratePassword = value; }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [LocalizedWebDisplayName("FBAPackMembershipRequestWebPart", "CancelButtonImageUrl_FriendlyName")]
        [LocalizedCategory("FBAPackMembershipRequestWebPart", "CancelButtonImageUrl_Category")]
        [LocalizedWebDescription("FBAPackMembershipRequestWebPart", "CancelButtonImageUrl_Description")]
        public string CancelButtonImageUrl
        {
            get
            {
                if (_CancelButtonImageUrl != null)
                {
                    return _CancelButtonImageUrl;
                }
                return _resourceManager.GetString("CancelButtonImageUrl_DefaultValue");
            }
            set
            {
                _CancelButtonImageUrl = value;
                if (cuw != null) cuw.CancelButtonImageUrl = value;
            }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [LocalizedWebDisplayName("FBAPackMembershipRequestWebPart", "CancelButtonText_FriendlyName")]
        [LocalizedCategory("FBAPackMembershipRequestWebPart", "CancelButtonText_Category")]
        [LocalizedWebDescription("FBAPackMembershipRequestWebPart", "CancelButtonText_Description")]
        public string CancelButtonText
        {
            get
            {
                if (_CancelButtonText != null)
                {
                    return _CancelButtonText;
                }
                return _resourceManager.GetString("CancelButtonText_DefaultValue");
            }
            set
            {
                _CancelButtonText = value;
                if (cuw != null) cuw.CancelButtonText = value;
            }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [LocalizedWebDisplayName("FBAPackMembershipRequestWebPart", "CancelButtonType_FriendlyName")]
        [LocalizedCategory("FBAPackMembershipRequestWebPart", "CancelButtonType_Category")]
        [LocalizedWebDescription("FBAPackMembershipRequestWebPart", "CancelButtonType_Description")]
        public ButtonType CancelButtonType
        {
            get { return _CancelButtonType; }
            set
            {
                _CancelButtonType = value;
                if (cuw != null) cuw.CancelButtonType = value;
            }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [LocalizedWebDisplayName("FBAPackMembershipRequestWebPart", "CancelDestinationPageUrl_FriendlyName")]
        [LocalizedCategory("FBAPackMembershipRequestWebPart", "CancelDestinationPageUrl_Category")]
        [LocalizedWebDescription("FBAPackMembershipRequestWebPart", "CancelDestinationPageUrl_Description")]
        public string CancelDestinationPageUrl
        {
            get
            {
                if (_CancelDestinationPageUrl != null)
                {
                    return _CancelDestinationPageUrl;
                }
                return _resourceManager.GetString("CancelDestinationPageUrl_DefaultValue");
            }
            set { _CancelDestinationPageUrl = value; }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [LocalizedWebDisplayName("FBAPackMembershipRequestWebPart", "CaptchaValidation_FriendlyName")]
        [LocalizedCategory("FBAPackMembershipRequestWebPart", "CaptchaValidation_Category")]
        [LocalizedWebDescription("FBAPackMembershipRequestWebPart", "CaptchaValidation_Description")]
        public bool CaptchaValidation
        {
            get { return _CaptchaValidation; }
            set { _CaptchaValidation = value; }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [LocalizedWebDisplayName("FBAPackMembershipRequestWebPart", "CompleteSuccessText_FriendlyName")]
        [LocalizedCategory("FBAPackMembershipRequestWebPart", "CompleteSuccessText_Category")]
        [LocalizedWebDescription("FBAPackMembershipRequestWebPart", "CompleteSuccessText_Description")]
        public string CompleteSuccessText
        {
            get
            {
                if (_CompleteSuccessText != null)
                {
                    return _CompleteSuccessText;
                }
                return _resourceManager.GetString("CompleteSuccessText_DefaultValue");
            }
            set { _CompleteSuccessText = value; }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [LocalizedWebDisplayName("FBAPackMembershipRequestWebPart", "ConfirmPasswordLabelText_FriendlyName")]
        [LocalizedCategory("FBAPackMembershipRequestWebPart", "ConfirmPasswordLabelText_Category")]
        [LocalizedWebDescription("FBAPackMembershipRequestWebPart", "ConfirmPasswordLabelText_Description")]
        public string ConfirmPasswordLabelText
        {
            get
            {
                if (_ConfirmPasswordLabelText != null)
                {
                    return _ConfirmPasswordLabelText;
                }
                return _resourceManager.GetString("ConfirmPasswordLabelText_DefaultValue");
            }
            set { _ConfirmPasswordLabelText = value; }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [LocalizedWebDisplayName("FBAPackMembershipRequestWebPart", "ConfirmPasswordCompareErrorMessage_FriendlyName")]
        [LocalizedCategory("FBAPackMembershipRequestWebPart", "ConfirmPasswordCompareErrorMessage_Category")]
        [LocalizedWebDescription("FBAPackMembershipRequestWebPart", "ConfirmPasswordCompareErrorMessage_Description")]
        public string ConfirmPasswordCompareErrorMessage
        {
            get
            {
                if (_ConfirmPasswordCompareErrorMessage != null)
                {
                    return _ConfirmPasswordCompareErrorMessage;
                }
                return _resourceManager.GetString("ConfirmPasswordCompareErrorMessage_DefaultValue");
            }
            set { _ConfirmPasswordCompareErrorMessage = value; }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [LocalizedWebDisplayName("FBAPackMembershipRequestWebPart", "ConfirmPasswordRequiredErrorMessage_FriendlyName")]
        [LocalizedCategory("FBAPackMembershipRequestWebPart", "ConfirmPasswordRequiredErrorMessage_Category")]
        [LocalizedWebDescription("FBAPackMembershipRequestWebPart", "ConfirmPasswordRequiredErrorMessage_Description")]
        public string ConfirmPasswordRequiredErrorMessage
        {
            get
            {
                if (_ConfirmPasswordRequiredErrorMessage != null)
                {
                    return _ConfirmPasswordRequiredErrorMessage;
                }
                return _resourceManager.GetString("ConfirmPasswordRequiredErrorMessage_DefaultValue");
            }
            set { _ConfirmPasswordRequiredErrorMessage = value; }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [LocalizedWebDisplayName("FBAPackMembershipRequestWebPart", "PasswordLabelText_FriendlyName")]
        [LocalizedCategory("FBAPackMembershipRequestWebPart", "PasswordLabelText_Category")]
        [LocalizedWebDescription("FBAPackMembershipRequestWebPart", "PasswordLabelText_Description")]
        public string PasswordLabelText
        {
            get
            {
                if (_PasswordLabelText != null)
                {
                    return _PasswordLabelText;
                }
                return _resourceManager.GetString("PasswordLabelText_DefaultValue");
            }
            set { _PasswordLabelText = value; }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [LocalizedWebDisplayName("FBAPackMembershipRequestWebPart", "PasswordRequiredErrorMessage_FriendlyName")]
        [LocalizedCategory("FBAPackMembershipRequestWebPart", "PasswordRequiredErrorMessage_Category")]
        [LocalizedWebDescription("FBAPackMembershipRequestWebPart", "PasswordRequiredErrorMessage_Description")]
        public string PasswordRequiredErrorMessage
        {
            get
            {
                if (_PasswordRequiredErrorMessage != null)
                {
                    return _PasswordRequiredErrorMessage;
                }
                return _resourceManager.GetString("PasswordRequiredErrorMessage_DefaultValue");
            }
            set { _PasswordRequiredErrorMessage = value; }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [LocalizedWebDisplayName("FBAPackMembershipRequestWebPart", "ContinueButtonImageUrl_FriendlyName")]
        [LocalizedCategory("FBAPackMembershipRequestWebPart", "ContinueButtonImageUrl_Category")]
        [LocalizedWebDescription("FBAPackMembershipRequestWebPart", "ContinueButtonImageUrl_Description")]
        public string ContinueButtonImageUrl
        {
            get
            {
                if (_ContinueButtonImageUrl != null)
                {
                    return _ContinueButtonImageUrl;
                }
                return _resourceManager.GetString("ContinueButtonImageUrl_DefaultValue");
            }
            set
            {
                _ContinueButtonImageUrl = value;
                if (cuw != null) cuw.ContinueButtonImageUrl = value;
            }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [LocalizedWebDisplayName("FBAPackMembershipRequestWebPart", "ContinueButtonText_FriendlyName")]
        [LocalizedCategory("FBAPackMembershipRequestWebPart", "ContinueButtonText_Category")]
        [LocalizedWebDescription("FBAPackMembershipRequestWebPart", "ContinueButtonText_Description")]
        public string ContinueButtonText
        {
            get
            {
                if (_ContinueButtonText != null)
                {
                    return _ContinueButtonText;
                }
                return _resourceManager.GetString("ContinueButtonText_DefaultValue");
            }
            set
            {
                _ContinueButtonText = value;
                if (cuw != null) cuw.ContinueButtonText = value;
            }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [LocalizedWebDisplayName("FBAPackMembershipRequestWebPart", "ContinueButtonType_FriendlyName")]
        [LocalizedCategory("FBAPackMembershipRequestWebPart", "ContinueButtonType_Category")]
        [LocalizedWebDescription("FBAPackMembershipRequestWebPart", "ContinueButtonType_Description")]
        public ButtonType ContinueButtonType
        {
            get { return _ContinueButtonType; }
            set
            {
                _ContinueButtonType = value;
                if (cuw != null) cuw.ContinueButtonType = value;
            }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [LocalizedWebDisplayName("FBAPackMembershipRequestWebPart", "CreateUserButtonImageUrl_FriendlyName")]
        [LocalizedCategory("FBAPackMembershipRequestWebPart", "CreateUserButtonImageUrl_Category")]
        [LocalizedWebDescription("FBAPackMembershipRequestWebPart", "CreateUserButtonImageUrl_Description")]
        public string CreateUserButtonImageUrl
        {
            get
            {
                if (_CreateUserButtonImageUrl != null)
                {
                    return _CreateUserButtonImageUrl;
                }
                return _resourceManager.GetString("CreateUserButtonImageUrl_DefaultValue");
            }
            set
            {
                _CreateUserButtonImageUrl = value;
                if (cuw != null) cuw.CreateUserButtonImageUrl = value;
            }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [LocalizedWebDisplayName("FBAPackMembershipRequestWebPart", "CreateUserButtonText_FriendlyName")]
        [LocalizedCategory("FBAPackMembershipRequestWebPart", "CreateUserButtonText_Category")]
        [LocalizedWebDescription("FBAPackMembershipRequestWebPart", "CreateUserButtonText_Description")]
        public string CreateUserButtonText
        {
            get
            {
                if (_CreateUserButtonText != null)
                {
                    return _CreateUserButtonText;
                }
                return _resourceManager.GetString("CreateUserButtonText_DefaultValue");
            }
            set
            {
                _CreateUserButtonText = value;
                if (cuw != null) cuw.CreateUserButtonText = value;
            }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [LocalizedWebDisplayName("FBAPackMembershipRequestWebPart", "CreateUserButtonType_FriendlyName")]
        [LocalizedCategory("FBAPackMembershipRequestWebPart", "CreateUserButtonType_Category")]
        [LocalizedWebDescription("FBAPackMembershipRequestWebPart", "CreateUserButtonType_Description")]
        public ButtonType CreateUserButtonType
        {
            get { return _CreateUserButtonType; }
            set
            {
                _CreateUserButtonType = value;
                if (cuw != null) cuw.CreateUserButtonType = value;
            }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [LocalizedWebDisplayName("FBAPackMembershipRequestWebPart", "CssClass_FriendlyName")]
        [LocalizedCategory("FBAPackMembershipRequestWebPart", "CssClass_Category")]
        [LocalizedWebDescription("FBAPackMembershipRequestWebPart", "CssClass_Description")]
        public new string CssClass
        {
            get
            {
                if (_CssClass != null)
                {
                    return _CssClass;
                }
                return _resourceManager.GetString("CssClass_DefaultValue");
            }
            set
            {
                _CssClass = value;
                if (cuw != null) cuw.CssClass = value;
            }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [LocalizedWebDisplayName("FBAPackMembershipRequestWebPart", "DisplayCancelButton_FriendlyName")]
        [LocalizedCategory("FBAPackMembershipRequestWebPart", "DisplayCancelButton_Category")]
        [LocalizedWebDescription("FBAPackMembershipRequestWebPart", "DisplayCancelButton_Description")]
        public bool DisplayCancelButton
        {
            get { return _DisplayCancelButton; }
            set
            {
                _DisplayCancelButton = value;
                if (cuw != null)
                    cuw.DisplayCancelButton = value;
            }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [LocalizedWebDisplayName("FBAPackMembershipRequestWebPart", "DuplicateEmailErrorMessage_FriendlyName")]
        [LocalizedCategory("FBAPackMembershipRequestWebPart", "DuplicateEmailErrorMessage_Category")]
        [LocalizedWebDescription("FBAPackMembershipRequestWebPart", "DuplicateEmailErrorMessage_Description")]
        public string DuplicateEmailErrorMessage
        {
            get
            {
                if (_DuplicateEmailErrorMessage != null)
                {
                    return _DuplicateEmailErrorMessage;
                }
                return _resourceManager.GetString("DuplicateEmailErrorMessage_DefaultValue");
            }
            set { _DuplicateEmailErrorMessage = value; }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [LocalizedWebDisplayName("FBAPackMembershipRequestWebPart", "DuplicateUserNameErrorMessage_FriendlyName")]
        [LocalizedCategory("FBAPackMembershipRequestWebPart", "DuplicateUserNameErrorMessage_Category")]
        [LocalizedWebDescription("FBAPackMembershipRequestWebPart", "DuplicateUserNameErrorMessage_Description")]
        public string DuplicateUserNameErrorMessage
        {
            get
            {
                if (_DuplicateUserNameErrorMessage != null)
                {
                    return _DuplicateUserNameErrorMessage;
                }
                return _resourceManager.GetString("DuplicateUserNameErrorMessage_DefaultValue");
            }
            set { _DuplicateUserNameErrorMessage = value; }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [LocalizedWebDisplayName("FBAPackMembershipRequestWebPart", "EditProfileIconUrl_FriendlyName")]
        [LocalizedCategory("FBAPackMembershipRequestWebPart", "EditProfileIconUrl_Category")]
        [LocalizedWebDescription("FBAPackMembershipRequestWebPart", "EditProfileIconUrl_Description")]
        public string EditProfileIconUrl
        {
            get
            {
                if (_EditProfileIconUrl != null)
                {
                    return _EditProfileIconUrl;
                }
                return _resourceManager.GetString("EditProfileIconUrl_DefaultValue");
            }
            set { _EditProfileIconUrl = value; }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [LocalizedWebDisplayName("FBAPackMembershipRequestWebPart", "EditProfileText_FriendlyName")]
        [LocalizedCategory("FBAPackMembershipRequestWebPart", "EditProfileText_Category")]
        [LocalizedWebDescription("FBAPackMembershipRequestWebPart", "EditProfileText_Description")]
        public string EditProfileText
        {
            get
            {
                if (_EditProfileText != null)
                {
                    return _EditProfileText;
                }
                return _resourceManager.GetString("EditProfileText_DefaultValue");
            }
            set
            {
                _EditProfileText = value;
                if (cuw != null) cuw.EditProfileText = value;
            }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [LocalizedWebDisplayName("FBAPackMembershipRequestWebPart", "EditProfileUrl_FriendlyName")]
        [LocalizedCategory("FBAPackMembershipRequestWebPart", "EditProfileUrl_Category")]
        [LocalizedWebDescription("FBAPackMembershipRequestWebPart", "EditProfileUrl_Description")]
        public string EditProfileUrl
        {
            get
            {
                if (_EditProfileUrl != null)
                {
                    return _EditProfileUrl;
                }
                return _resourceManager.GetString("EditProfileUrl_DefaultValue");
            }
            set { _EditProfileUrl = value; }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [LocalizedWebDisplayName("FBAPackMembershipRequestWebPart", "EmailLabelText_FriendlyName")]
        [LocalizedCategory("FBAPackMembershipRequestWebPart", "EmailLabelText_Category")]
        [LocalizedWebDescription("FBAPackMembershipRequestWebPart", "EmailLabelText_Description")]
        public string EmailLabelText
        {
            get
            {
                if (_EmailLabelText != null)
                {
                    return _EmailLabelText;
                }
                return _resourceManager.GetString("EmailLabelText_DefaultValue");
            }
            set
            {
                _EmailLabelText = value;
                if (cuw != null) cuw.EmailLabelText = value;
            }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [LocalizedWebDisplayName("FBAPackMembershipRequestWebPart", "EmailRegularExpressionErrorMessage_FriendlyName")]
        [LocalizedCategory("FBAPackMembershipRequestWebPart", "EmailRegularExpressionErrorMessage_Category")]
        [LocalizedWebDescription("FBAPackMembershipRequestWebPart", "EmailRegularExpressionErrorMessage_Description")]
        public string EmailRegularExpressionErrorMessage
        {
            get
            {
                if (_EmailRegularExpressionErrorMessage != null)
                {
                    return _EmailRegularExpressionErrorMessage;
                }
                return _resourceManager.GetString("EmailRegularExpressionErrorMessage_DefaultValue");
            }
            set { _EmailRegularExpressionErrorMessage = value; }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [LocalizedWebDisplayName("FBAPackMembershipRequestWebPart", "EmailRequiredErrorMessage_FriendlyName")]
        [LocalizedCategory("FBAPackMembershipRequestWebPart", "EmailRequiredErrorMessage_Category")]
        [LocalizedWebDescription("FBAPackMembershipRequestWebPart", "EmailRequiredErrorMessage_Description")]
        public string EmailRequiredErrorMessage
        {
            get
            {
                if (_EmailRequiredErrorMessage != null)
                {
                    return _EmailRequiredErrorMessage;
                }
                return _resourceManager.GetString("EmailRequiredErrorMessage_DefaultValue");
            }
            set { _EmailRequiredErrorMessage = value; }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [LocalizedWebDisplayName("FBAPackMembershipRequestWebPart", "FinishDestinationPageUrl_FriendlyName")]
        [LocalizedCategory("FBAPackMembershipRequestWebPart", "FinishDestinationPageUrl_Category")]
        [LocalizedWebDescription("FBAPackMembershipRequestWebPart", "FinishDestinationPageUrl_Description")]
        public string FinishDestinationPageUrl
        {
            get
            {
                if (_FinishDestinationPageUrl != null)
                {
                    return _FinishDestinationPageUrl;
                }
                return _resourceManager.GetString("FinishDestinationPageUrl_DefaultValue");
            }
            set { _FinishDestinationPageUrl = value; }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [LocalizedWebDisplayName("FBAPackMembershipRequestWebPart", "HeaderText_FriendlyName")]
        [LocalizedCategory("FBAPackMembershipRequestWebPart", "HeaderText_Category")]
        [LocalizedWebDescription("FBAPackMembershipRequestWebPart", "HeaderText_Description")]
        public string HeaderText
        {
            get
            {
                if (_HeaderText != null)
                {
                    return _HeaderText;
                }
                return _resourceManager.GetString("HeaderText_DefaultValue");
            }
            set
            {
                _HeaderText = value;
                if (cuw != null) cuw.HeaderText = value;
            }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [LocalizedWebDisplayName("FBAPackMembershipRequestWebPart", "InstructionText_FriendlyName")]
        [LocalizedCategory("FBAPackMembershipRequestWebPart", "InstructionText_Category")]
        [LocalizedWebDescription("FBAPackMembershipRequestWebPart", "InstructionText_Description")]
        public string InstructionText
        {
            get
            {
                if (_InstructionText != null)
                {
                    return _InstructionText;
                }
                return _resourceManager.GetString("InstructionText_DefaultValue");
            }
            set
            {
                _InstructionText = value;
                if (cuw != null) cuw.InstructionText = value;
            }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [LocalizedWebDisplayName("FBAPackMembershipRequestWebPart", "InvalidAnswerErrorMessage_FriendlyName")]
        [LocalizedCategory("FBAPackMembershipRequestWebPart", "InvalidAnswerErrorMessage_Category")]
        [LocalizedWebDescription("FBAPackMembershipRequestWebPart", "InvalidAnswerErrorMessage_Description")]
        public string InvalidAnswerErrorMessage
        {
            get
            {
                if (_InvalidAnswerErrorMessage != null)
                {
                    return _InvalidAnswerErrorMessage;
                }
                return _resourceManager.GetString("InvalidAnswerErrorMessage_DefaultValue");
            }
            set { _InvalidAnswerErrorMessage = value; }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [LocalizedWebDisplayName("FBAPackMembershipRequestWebPart", "InvalidEmailErrorMessage_FriendlyName")]
        [LocalizedCategory("FBAPackMembershipRequestWebPart", "InvalidEmailErrorMessage_Category")]
        [LocalizedWebDescription("FBAPackMembershipRequestWebPart", "InvalidEmailErrorMessage_Description")]
        public string InvalidEmailErrorMessage
        {
            get
            {
                if (_InvalidEmailErrorMessage != null)
                {
                    return _InvalidEmailErrorMessage;
                }
                return _resourceManager.GetString("InvalidEmailErrorMessage_DefaultValue");
            }
            set { _InvalidEmailErrorMessage = value; }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [LocalizedWebDisplayName("FBAPackMembershipRequestWebPart", "InvalidPasswordErrorMessage_FriendlyName")]
        [LocalizedCategory("FBAPackMembershipRequestWebPart", "InvalidPasswordErrorMessage_Category")]
        [LocalizedWebDescription("FBAPackMembershipRequestWebPart", "InvalidPasswordErrorMessage_Description")]
        public string InvalidPasswordErrorMessage
        {
            get
            {
                if (_InvalidPasswordErrorMessage != null)
                {
                    return _InvalidPasswordErrorMessage;
                }
                return _resourceManager.GetString("InvalidPasswordErrorMessage_DefaultValue");
            }
            set { _InvalidPasswordErrorMessage = value; }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [LocalizedWebDisplayName("FBAPackMembershipRequestWebPart", "InvalidQuestionErrorMessage_FriendlyName")]
        [LocalizedCategory("FBAPackMembershipRequestWebPart", "InvalidQuestionErrorMessage_Category")]
        [LocalizedWebDescription("FBAPackMembershipRequestWebPart", "InvalidQuestionErrorMessage_Description")]
        public string InvalidQuestionErrorMessage
        {
            get
            {
                if (_InvalidQuestionErrorMessage != null)
                {
                    return _InvalidQuestionErrorMessage;
                }
                return _resourceManager.GetString("InvalidQuestionErrorMessage_DefaultValue");
            }
            set { _InvalidQuestionErrorMessage = value; }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [LocalizedWebDisplayName("FBAPackMembershipRequestWebPart", "LoginCreatedUser_FriendlyName")]
        [LocalizedCategory("FBAPackMembershipRequestWebPart", "LoginCreatedUser_Category")]
        [LocalizedWebDescription("FBAPackMembershipRequestWebPart", "LoginCreatedUser_Description")]
        public bool LoginCreatedUser
        {
            get { return _LoginCreatedUser; }
            set { _LoginCreatedUser = value; }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [LocalizedWebDisplayName("FBAPackMembershipRequestWebPart", "QuestionLabelText_FriendlyName")]
        [LocalizedCategory("FBAPackMembershipRequestWebPart", "QuestionLabelText_Category")]
        [LocalizedWebDescription("FBAPackMembershipRequestWebPart", "QuestionLabelText_Description")]
        public string QuestionLabelText
        {
            get
            {
                if (_QuestionLabelText != null)
                {
                    return _QuestionLabelText;
                }
                return _resourceManager.GetString("QuestionLabelText_DefaultValue");
            }
            set
            {
                _QuestionLabelText = value;
                if (cuw != null) cuw.QuestionLabelText = value;
            }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [LocalizedWebDisplayName("FBAPackMembershipRequestWebPart", "QuestionRequiredErrorMessage_FriendlyName")]
        [LocalizedCategory("FBAPackMembershipRequestWebPart", "QuestionRequiredErrorMessage_Category")]
        [LocalizedWebDescription("FBAPackMembershipRequestWebPart", "QuestionRequiredErrorMessage_Description")]
        public string QuestionRequiredErrorMessage
        {
            get
            {
                if (_QuestionRequiredErrorMessage != null)
                {
                    return _QuestionRequiredErrorMessage;
                }
                return _resourceManager.GetString("QuestionRequiredErrorMessage_DefaultValue");
            }
            set { _QuestionRequiredErrorMessage = value; }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [LocalizedWebDisplayName("FBAPackMembershipRequestWebPart", "UnknownErrorMessage_FriendlyName")]
        [LocalizedCategory("FBAPackMembershipRequestWebPart", "UnknownErrorMessage_Category")]
        [LocalizedWebDescription("FBAPackMembershipRequestWebPart", "UnknownErrorMessage_Description")]
        public string UnknownErrorMessage
        {
            get
            {
                if (_UnknownErrorMessage != null)
                {
                    return _UnknownErrorMessage;
                }
                return _resourceManager.GetString("UnknownErrorMessage_DefaultValue");
            }
            set { _UnknownErrorMessage = value; }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [LocalizedWebDisplayName("FBAPackMembershipRequestWebPart", "UserNameLabelText_FriendlyName")]
        [LocalizedCategory("FBAPackMembershipRequestWebPart", "UserNameLabelText_Category")]
        [LocalizedWebDescription("FBAPackMembershipRequestWebPart", "UserNameLabelText_Description")]
        public string UserNameLabelText
        {
            get
            {
                if (_UserNameLabelText != null)
                {
                    return _UserNameLabelText;
                }
                return _resourceManager.GetString("UserNameLabelText_DefaultValue");
            }
            set
            {
                _UserNameLabelText = value;
                if (cuw != null) cuw.UserNameLabelText = value;
            }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [LocalizedWebDisplayName("FBAPackMembershipRequestWebPart", "UserNameRequiredErrorMessage_FriendlyName")]
        [LocalizedCategory("FBAPackMembershipRequestWebPart", "UserNameRequiredErrorMessage_Category")]
        [LocalizedWebDescription("FBAPackMembershipRequestWebPart", "UserNameRequiredErrorMessage_Description")]
        public string UserNameRequiredErrorMessage
        {
            get
            {
                if (_UserNameRequiredErrorMessage != null)
                {
                    return _UserNameRequiredErrorMessage;
                }
                return _resourceManager.GetString("UserNameRequiredErrorMessage_DefaultValue");
            }
            set { _UserNameRequiredErrorMessage = value; }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [LocalizedWebDisplayName("FBAPackMembershipRequestWebPart", "FirstNameLabelText_FriendlyName")]
        [LocalizedCategory("FBAPackMembershipRequestWebPart", "FirstNameLabelText_Category")]
        [LocalizedWebDescription("FBAPackMembershipRequestWebPart", "FirstNameLabelText_Description")]
        public string FirstNameLabelText
        {
            get
            {
                if (_FirstNameLabelText != null)
                {
                    return _FirstNameLabelText;
                }
                return _resourceManager.GetString("FirstNameLabelText_DefaultValue");
            }
            set { _FirstNameLabelText = value; }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [LocalizedWebDisplayName("FBAPackMembershipRequestWebPart", "LastNameLabelText_FriendlyName")]
        [LocalizedCategory("FBAPackMembershipRequestWebPart", "LastNameLabelText_Category")]
        [LocalizedWebDescription("FBAPackMembershipRequestWebPart", "LastNameLabelText_Description")]
        public string LastNameLabelText
        {
            get
            {
                if (_LastNameLabelText != null)
                {
                    return _LastNameLabelText;
                }
                return _resourceManager.GetString("LastNameLabelText_DefaultValue");
            }
            set { _LastNameLabelText = value; }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [LocalizedWebDisplayName("FBAPackMembershipRequestWebPart", "FirstNameRequiredErrorMessage_FriendlyName")]
        [LocalizedCategory("FBAPackMembershipRequestWebPart", "FirstNameRequiredErrorMessage_Category")]
        [LocalizedWebDescription("FBAPackMembershipRequestWebPart", "FirstNameRequiredErrorMessage_Description")]
        public string FirstNameRequiredErrorMessage
        {
            get
            {
                if (_FirstNameRequiredErrorMessage != null)
                {
                    return _FirstNameRequiredErrorMessage;
                }
                return _resourceManager.GetString("FirstNameRequiredErrorMessage_DefaultValue");
            }
            set { _FirstNameRequiredErrorMessage = value; }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [LocalizedWebDisplayName("FBAPackMembershipRequestWebPart", "LastNameRequiredErrorMessage_FriendlyName")]
        [LocalizedCategory("FBAPackMembershipRequestWebPart", "LastNameRequiredErrorMessage_Category")]
        [LocalizedWebDescription("FBAPackMembershipRequestWebPart", "LastNameRequiredErrorMessage_Description")]
        public string LastNameRequiredErrorMessage
        {
            get
            {
                if (_LastNameRequiredErrorMessage != null)
                {
                    return _LastNameRequiredErrorMessage;
                }
                return _resourceManager.GetString("LastNameRequiredErrorMessage_DefaultValue");
            }
            set { _LastNameRequiredErrorMessage = value; }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [LocalizedWebDisplayName("FBAPackMembershipRequestWebPart", "HipPictureLabelText_FriendlyName")]
        [LocalizedCategory("FBAPackMembershipRequestWebPart", "HipPictureLabelText_Category")]
        [LocalizedWebDescription("FBAPackMembershipRequestWebPart", "HipPictureLabelText_Description")]
        public string HipPictureLabelText
        {
            get
            {
                if (_HipPictureLabelText != null)
                {
                    return _HipPictureLabelText;
                }
                return _resourceManager.GetString("HipPictureLabelText_DefaultValue");
            }
            set { _HipPictureLabelText = value; }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [LocalizedWebDisplayName("FBAPackMembershipRequestWebPart", "HipCharactersLabelText_FriendlyName")]
        [LocalizedCategory("FBAPackMembershipRequestWebPart", "HipCharactersLabelText_Category")]
        [LocalizedWebDescription("FBAPackMembershipRequestWebPart", "HipCharactersLabelText_Description")]
        public string HipCharactersLabelText
        {
            get
            {
                if (_HipCharactersLabelText != null)
                {
                    return _HipCharactersLabelText;
                }
                return _resourceManager.GetString("HipCharactersLabelText_DefaultValue");
            }
            set { _HipCharactersLabelText = value; }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [LocalizedWebDisplayName("FBAPackMembershipRequestWebPart", "HipInstructionsLabelText_FriendlyName")]
        [LocalizedCategory("FBAPackMembershipRequestWebPart", "HipInstructionsLabelText_Category")]
        [LocalizedWebDescription("FBAPackMembershipRequestWebPart", "HipInstructionsLabelText_Description")]
        public string HipInstructionsLabelText
        {
            get
            {
                if (_HipInstructionsLabelText != null)
                {
                    return _HipInstructionsLabelText;
                }
                return _resourceManager.GetString("HipInstructionsLabelText_DefaultValue");
            }
            set { _HipInstructionsLabelText = value; }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [LocalizedWebDisplayName("FBAPackMembershipRequestWebPart", "HipPictureDescription_FriendlyName")]
        [LocalizedCategory("FBAPackMembershipRequestWebPart", "HipPictureDescription_Category")]
        [LocalizedWebDescription("FBAPackMembershipRequestWebPart", "HipPictureDescription_Description")]
        public string HipPictureDescription
        {
            get
            {
                if (_HipPictureDescription != null)
                {
                    return _HipPictureDescription;
                }
                return _resourceManager.GetString("HipPictureDescription_DefaultValue");
            }
            set { _HipPictureDescription = value; }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [LocalizedWebDisplayName("FBAPackMembershipRequestWebPart", "HipResetLabelText_FriendlyName")]
        [LocalizedCategory("FBAPackMembershipRequestWebPart", "HipResetLabelText_Category")]
        [LocalizedWebDescription("FBAPackMembershipRequestWebPart", "HipResetLabelText_Description")]
        public string HipResetLabelText
        {
            get
            {
                if (_HipResetLabelText != null)
                {
                    return _HipResetLabelText;
                }
                return _resourceManager.GetString("HipResetLabelText_DefaultValue");
            }
            set { _HipResetLabelText = value; }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [LocalizedWebDisplayName("FBAPackMembershipRequestWebPart", "HipErrorMessage_FriendlyName")]
        [LocalizedCategory("FBAPackMembershipRequestWebPart", "HipErrorMessage_Category")]
        [LocalizedWebDescription("FBAPackMembershipRequestWebPart", "HipErrorMessage_Description")]
        public string HipErrorMessage
        {
            get
            {
                if (_HipErrorMessage != null)
                {
                    return _HipErrorMessage;
                }
                return _resourceManager.GetString("HipErrorMessage_DefaultValue");
            }
            set { _HipErrorMessage = value; }
        }
        #endregion

        #region Controls
        protected MembershipRequestControl cuw;
        #endregion

        #region Methods
        private void AddCreateUserControl()
        {
            TemplateHelper helper;
            string provider = Utils.GetMembershipProvider(Context);
            //Exit if membership provider not defined
            if (provider == null || !Utils.IsProviderConfigured())
            {
                Controls.Add(new LiteralControl(LocalizedString.GetString("FBAPackFeatures", "MembershipNotConfigured")));
                return;
            }

            cuw = new MembershipRequestControl();
            cuw.CreateUserStep.ContentTemplate = new TemplateLoader(CreateUserStepTemplate, Page);
            cuw.CompleteStep.ContentTemplate = new TemplateLoader(CompleteStepTemplate, Page);
            cuw.ID = "FBACreateUserWizard";
            cuw.AutoGeneratePassword = AutoGeneratePassword;
            cuw.MembershipProvider = provider;
            cuw.DuplicateEmailErrorMessage = DuplicateEmailErrorMessage;
            cuw.DuplicateUserNameErrorMessage = DuplicateUserNameErrorMessage;
            cuw.EmailRegularExpressionErrorMessage = EmailRegularExpressionErrorMessage;
            cuw.InvalidPasswordErrorMessage = _InvalidPasswordErrorMessage;
            cuw.InvalidAnswerErrorMessage = InvalidAnswerErrorMessage;
            cuw.InvalidEmailErrorMessage = InvalidEmailErrorMessage;
            cuw.InvalidQuestionErrorMessage = InvalidQuestionErrorMessage;
            cuw.LoginCreatedUser = false;
            cuw.SPLoginCreatedUser = LoginCreatedUser;
            cuw.UnknownErrorMessage = UnknownErrorMessage;
            cuw.DefaultGroup = GroupName;
            cuw.CreateUserStep.CustomNavigationTemplateContainer.Controls[0].Visible = false;

            if (!String.IsNullOrEmpty(FinishDestinationPageUrl))
            {
                cuw.FinishDestinationPageUrl = FinishDestinationPageUrl;
            }
            else
            {
                string url = SPUtility.OriginalServerRelativeRequestUrl;
                SPUtility.DetermineRedirectUrl(url, SPRedirectFlags.UseSource, this.Context, null, out url);
                cuw.FinishDestinationPageUrl = url;
            }

            if (!String.IsNullOrEmpty(CancelDestinationPageUrl))
            {
                cuw.CancelDestinationPageUrl = CancelDestinationPageUrl;
            }
            else
            {
                string url = SPUtility.GetPageUrlPath(HttpContext.Current);
                SPUtility.DetermineRedirectUrl(url, SPRedirectFlags.UseSource, this.Context, null, out url);
                cuw.CancelDestinationPageUrl = url;
            }

            //CreateUserStep
            helper = new TemplateHelper(cuw.CreateUserStep.ContentTemplateContainer);

            helper.SetCSS("MembershipRequestTable", CssClass);
            helper.SetText("Header", HeaderText);
            helper.SetText("Instruction", InstructionText);
            helper.SetText("UserNameLabel", UserNameLabelText);
            helper.SetValidation("UserNameRequired", UserNameRequiredErrorMessage, this.UniqueID);
            helper.SetText("FirstNameLabel", FirstNameLabelText);
            helper.SetValidation("FirstNameRequired", FirstNameRequiredErrorMessage, this.UniqueID); 
            helper.SetText("LastNameLabel", LastNameLabelText);
            helper.SetValidation("LastNameRequired", LastNameRequiredErrorMessage, this.UniqueID);
            helper.SetText("EmailLabel", EmailLabelText);
            helper.SetValidation("EmailRequired", EmailRequiredErrorMessage, this.UniqueID);

            if (!AutoGeneratePassword)
            {
                helper.SetVisible("PasswordRow", true);
                helper.SetVisible("ConfirmPasswordRow", true);
                helper.SetVisible("ConfirmPasswordCompareRow", true);
                helper.SetText("PasswordLabel", PasswordLabelText);
                helper.SetValidation("PasswordRequired", PasswordRequiredErrorMessage, this.UniqueID);
                helper.SetText("ConfirmPasswordLabel", ConfirmPasswordLabelText);
                helper.SetValidation("ConfirmPasswordRequired", ConfirmPasswordRequiredErrorMessage, this.UniqueID);
                helper.SetValidation("ConfirmPasswordCompare", ConfirmPasswordCompareErrorMessage, this.UniqueID);
            }

            if (Utils.BaseMembershipProvider().RequiresQuestionAndAnswer)
            {
                helper.SetVisible("QuestionRow", true);
                helper.SetVisible("AnswerRow", true);
                helper.SetText("QuestionLabel", QuestionLabelText);
                helper.SetValidation("QuestionRequired", QuestionRequiredErrorMessage, this.UniqueID);
                helper.SetText("AnswerLabel", AnswerLabelText);
                helper.SetValidation("AnswerRequired", AnswerRequiredErrorMessage, this.UniqueID);
            }

            if (CaptchaValidation)
            {
                helper.SetVisible("HipPictureRow", true);
                helper.SetVisible("HipAnswerRow", true);
                helper.SetText("HipPictureLabel", HipPictureLabelText);
                helper.SetText("HipInstructionsLabel", HipInstructionsLabelText);
                helper.SetText("HipPictureDescriptionLabel", HipPictureDescription);
                helper.SetText("HipAnswerLabel", HipCharactersLabelText);
                helper.SetValidation("HipAnswerValidator", HipErrorMessage, this.UniqueID);
                helper.SetButton("HipReset", HipResetLabelText, "");
            }
            switch (CreateUserButtonType)
            {
                case ButtonType.Button:
                    helper.SetButton("CreateUserButton", CreateUserButtonText, this.UniqueID);
                    helper.SetVisible("CreateUserButton", true);
                    break;

                case ButtonType.Image:
                    helper.SetImageButton("CreateUserImageButton", CreateUserButtonImageUrl, CreateUserButtonText, this.UniqueID);
                    helper.SetVisible("CreateUserImageButton", true);
                    break;

                case ButtonType.Link:
                    helper.SetButton("CreateUserLinkButton", CreateUserButtonText, this.UniqueID);
                    helper.SetVisible("CreateUserLinkButton", true);
                    break;
            }

            if (DisplayCancelButton)
            {
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
            }

            //SuccessTemplate
            helper = new TemplateHelper(cuw.CompleteStep.ContentTemplateContainer);
            helper.SetText("CompleteSuccess", CompleteSuccessText);

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
            
            Controls.Add(cuw);
        }
        #region Rendering Methods
        protected override void CreateChildControls()
        {
            AddCreateUserControl();
        }

        protected override void RenderContents(HtmlTextWriter writer)
        {
            EnsureChildControls();
            base.RenderContents(writer);
        }
        #endregion

        #endregion


        /// <summary>
        ///     This method adds the custom editor part to the collection
        /// </summary>
        /// <returns></returns>
        public override EditorPartCollection CreateEditorParts()
        {
            List<EditorPart> editors = new List<EditorPart>();
            editors.Add(new MembershipRequestGroupEditor());
            return new EditorPartCollection(editors);
        }
    }
}

