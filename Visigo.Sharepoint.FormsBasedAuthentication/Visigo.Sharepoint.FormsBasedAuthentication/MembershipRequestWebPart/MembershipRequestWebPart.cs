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
using Microsoft.SharePoint.WebPartPages;
using Visigo.Sharepoint.FormsBasedAuthentication.HIP;
using Microsoft.SharePoint.Administration;

namespace Visigo.Sharepoint.FormsBasedAuthentication
{
    public class MembershipRequestWebPart : Microsoft.SharePoint.WebPartPages.WebPart
    {
        private ResourceManager _resourceManager;

        #region Fields
        protected string[] _randCharacters = { "A","B","C","D","E","F","G","H","J","K","L","M","N","P","Q","R","S","T","U","V","W","X","Y","Z",
                "2","3","4","5","6","7","8","9",
                "a","b","c","d","e","f","g","h","j","k","m","n","p","q","r","s","t","u","v","w","x","y","z"};
        private string _AnswerLabelText = String.Empty;
        private string _AnswerRequiredErrorMessage = String.Empty;
        private string _CancelButtonImageUrl = String.Empty;
        private string _CancelButtonText = String.Empty;
        private ButtonType _CancelButtonType = ButtonType.Button;
        private string _CancelDestinationPageUrl = String.Empty;
        private string _CompleteSuccessText = String.Empty;
        private string _ConfirmPasswordCompareErrorMessage = String.Empty;
        private string _ConfirmPasswordLabelText = String.Empty;
        private string _ConfirmPasswordRequiredErrorMessage = String.Empty;
        private string _CreateUserButtonImageUrl = String.Empty;
        private string _CreateUserButtonText = String.Empty;
        private ButtonType _CreateUserButtonType = ButtonType.Button;
        private string _CssClass = String.Empty;
        private bool _DisplayCancelButton = false;
        private string _DuplicateEmailErrorMessage = String.Empty;
        private string _DuplicateUserNameErrorMessage = String.Empty;
        private string _EditProfileIconUrl = String.Empty;
        private string _EditProfileText = String.Empty;
        private string _EditProfileUrl = String.Empty;
        private string _EmailLabelText = String.Empty;
        private string _EmailRegularExpressionErrorMessage = String.Empty;
        private string _EmailRequiredErrorMessage = String.Empty;
        private string _FinishDestinationPageUrl = String.Empty;
        private string _HeaderText = String.Empty;
        private string _InstructionText = String.Empty;
        private string _InvalidAnswerErrorMessage = String.Empty;
        private string _InvalidEmailErrorMessage = String.Empty;
        private string _InvalidPasswordErrorMessage = String.Empty;
        private string _InvalidQuestionErrorMessage = String.Empty;
        private bool _LoginCreatedUser = true;
        private string _QuestionLabelText = String.Empty;
        private string _QuestionRequiredErrorMessage = String.Empty;
        private string _UnknownErrorMessage = String.Empty;
        private string _UserNameLabelText = String.Empty;
        private string _UserNameRequiredErrorMessage = String.Empty;
        private string _FirstNameLabelText = String.Empty;
        private string _LastNameLabelText = string.Empty;
        private string _FirstNameRequiredErrorMessage = String.Empty;
        private string _LastNameRequiredErrorMessage = String.Empty;
        private string _HipPictureLabelText = String.Empty;
        private string _HipCharactersLabelText = string.Empty;
        private string _HipInstructionsLabelText = string.Empty;
        private string _HipPictureDescription = string.Empty;
        private string _HipResetLabelText = string.Empty;
        private string _HipErrorMessage = string.Empty;
        private string _GroupName = string.Empty;

        #endregion

        #region Constructors
        public MembershipRequestWebPart()
        {
            _resourceManager = new ResourceManager("Visigo.Sharepoint.FormsBasedAuthentication.MembershipRequestWebPart", Assembly.GetExecutingAssembly());
            try
            {
                AnswerLabelText = _resourceManager.GetString("AnswerLabelText_DefaultValue");
                AnswerRequiredErrorMessage = _resourceManager.GetString("AnswerRequiredErrorMessage_DefaultValue");
                CancelButtonImageUrl = _resourceManager.GetString("CancelButtonImageUrl_DefaultValue");
                CancelButtonText = _resourceManager.GetString("CancelButtonText_DefaultValue");
                CancelButtonType = (ButtonType)Convert.ToInt32(_resourceManager.GetString("CancelButtonType"));
                CancelDestinationPageUrl = _resourceManager.GetString("CancelDestinationPageUrl_DefaultValue");
                CompleteSuccessText = _resourceManager.GetString("CompleteSuccessText_DefaultValue");
                CreateUserButtonImageUrl = _resourceManager.GetString("CreateUserButtonImageUrl_DefaultValue");
                CreateUserButtonText = _resourceManager.GetString("CreateUserButtonText_DefaultValue");
                CreateUserButtonType = (ButtonType)Convert.ToInt32(_resourceManager.GetString("CreateUserButtonType"));
                CssClass = _resourceManager.GetString("CSSClass_DefaultValue");
                DisplayCancelButton = bool.Parse(_resourceManager.GetString("DisplayCancelButton_DefaultValue"));
                DuplicateEmailErrorMessage = _resourceManager.GetString("DuplicateEmailErrorMessage_DefaultValue");
                DuplicateUserNameErrorMessage = _resourceManager.GetString("DuplicateUserNameErrorMessage_DefaultValue");
                EditProfileIconUrl = _resourceManager.GetString("EditProfileIconUrl_DefaultValue");
                EditProfileText = _resourceManager.GetString("EditProfileText_DefaultValue");
                EditProfileUrl = _resourceManager.GetString("EditProfileUrl_DefaultValue");
                EmailLabelText = _resourceManager.GetString("EmailLabelText_DefaultValue");
                EmailRegularExpressionErrorMessage = _resourceManager.GetString("EmailRegularExpressionErrorMessage_DefaultValue");
                EmailRequiredErrorMessage = _resourceManager.GetString("EmailRequiredErrorMessage_DefaultValue");
                HeaderText = _resourceManager.GetString("HeaderText_DefaultValue");
                InstructionText = _resourceManager.GetString("InstructionText_DefaultValue");
                InvalidAnswerErrorMessage = _resourceManager.GetString("InvalidAnswerErrorMessage_DefaultValue");
                InvalidEmailErrorMessage = _resourceManager.GetString("InvalidEmailErrorMessage_DefaultValue");
                InvalidQuestionErrorMessage = _resourceManager.GetString("InvalidQuestionErrorMessage_DefaultValue");
                LoginCreatedUser = bool.Parse(_resourceManager.GetString("LoginCreatedUser_DefaultValue"));
                QuestionLabelText = _resourceManager.GetString("QuestionLabelText_DefaultValue");
                QuestionRequiredErrorMessage = _resourceManager.GetString("QuestionRequiredErrorMessage_DefaultValue");
                UnknownErrorMessage = _resourceManager.GetString("UnknownErrorMessage_DefaultValue");
                UserNameLabelText = _resourceManager.GetString("UserNameLabelText_DefaultValue");
                UserNameRequiredErrorMessage = _resourceManager.GetString("UserNameRequiredErrorMessage_DefaultValue");
                FirstNameLabelText = _resourceManager.GetString("FirstNameLabelText_DefaultValue");
                LastNameLabelText = _resourceManager.GetString("LastNameLabelText_DefaultValue");
                FirstNameRequiredErrorMessage = _resourceManager.GetString("FirstNameRequiredErrorMessage_DefaultValue");
                LastNameRequiredErrorMessage = _resourceManager.GetString("LastNameRequiredErrorMessage_DefaultValue");
                HipPictureLabelText = _resourceManager.GetString("HipPictureLabelText_DefaultValue");
                HipCharactersLabelText = _resourceManager.GetString("HipCharactersLabelText_DefaultValue");
                HipInstructionsLabelText = _resourceManager.GetString("HipInstructionsLabelText_DefaultValue");
                HipPictureDescription = _resourceManager.GetString("HipPictureDescription_DefaultValue");
                HipResetLabelText = _resourceManager.GetString("HipResetLabelText_DefaultValue");
                HipErrorMessage = _resourceManager.GetString("HipErrorMessage_DefaultValue");
                //FinishDestinationPageUrl = _resourceManager.GetString("FinishDestinationPageUrl_DefaultValue");
                //Default to the current url
                FinishDestinationPageUrl = HttpContext.Current.Request.Url.AbsolutePath;

            }
            catch (Exception ex)
            {
                Utils.LogError(ex);
                this.Controls.Add(Utils.CreateErrorMessage(_resourceManager.GetString("Error_Message")));
            }
        }

        #endregion

        #region Properties
        /// <summary>
        ///     This property is set using a custom editor
        /// </summary>
        public string GroupName
        {
            get { return _GroupName; }
            set { _GroupName = value; }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [ResourcesAttribute("AnswerLabelText_FriendlyName", "AnswerLabelText_Category", "AnswerLabelText_Description")]
        public string AnswerLabelText
        {
            get { return _AnswerLabelText; }
            set
            {
                _AnswerLabelText = value;
                if (cuw != null) cuw.AnswerLabelText = value;
            }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [ResourcesAttribute("AnswerRequiredErrorMessage_FriendlyName", "AnswerRequiredErrorMessage_Category", "AnswerRequiredErrorMessage_Description")]
        public string AnswerRequiredErrorMessage
        {
            get { return _AnswerRequiredErrorMessage; }
            set { _AnswerRequiredErrorMessage = value; }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [ResourcesAttribute("CancelButtonImageUrl_FriendlyName", "CancelButtonImageUrl_Category", "CancelButtonImageUrl_Description")]
        public string CancelButtonImageUrl
        {
            get { return _CancelButtonImageUrl; }
            set
            {
                _CancelButtonImageUrl = value;
                if (cuw != null) cuw.CancelButtonImageUrl = value;
            }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [ResourcesAttribute("CancelButtonText_FriendlyName", "CancelButtonText_Category", "CancelButtonText_Description")]
        public string CancelButtonText
        {
            get { return _CancelButtonText; }
            set
            {
                _CancelButtonText = value;
                if (cuw != null) cuw.CancelButtonText = value;
            }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [ResourcesAttribute("CancelButtonType_FriendlyName", "CancelButtonType_Category", "CancelButtonType_Description")]
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
        [ResourcesAttribute("CancelDestinationPageUrl_FriendlyName", "CancelDestinationPageUrl_Category", "CancelDestinationPageUrl_Description")]
        public string CancelDestinationPageUrl
        {
            get { return _CancelDestinationPageUrl; }
            set { _CancelDestinationPageUrl = value; }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [ResourcesAttribute("CompleteSuccessText_FriendlyName", "CompleteSuccessText_Category", "CompleteSuccessText_Description")]
        public string CompleteSuccessText
        {
            get { return _CompleteSuccessText; }
            set { _CompleteSuccessText = value; }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [ResourcesAttribute("CreateUserButtonImageUrl_FriendlyName", "CreateUserButtonImageUrl_Category", "CreateUserButtonImageUrl_Description")]
        public string CreateUserButtonImageUrl
        {
            get { return _CreateUserButtonImageUrl; }
            set
            {
                _CreateUserButtonImageUrl = value;
                if (cuw != null) cuw.CreateUserButtonImageUrl = value;
            }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [ResourcesAttribute("CreateUserButtonText_FriendlyName", "CreateUserButtonText_Category", "CreateUserButtonText_Description")]
        public string CreateUserButtonText
        {
            get { return _CreateUserButtonText; }
            set
            {
                _CreateUserButtonText = value;
                if (cuw != null) cuw.CreateUserButtonText = value;
            }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [ResourcesAttribute("CreateUserButtonType_FriendlyName", "CreateUserButtonType_Category", "CreateUserButtonType_Description")]
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
        [ResourcesAttribute("CssClass_FriendlyName", "CssClass_Category", "CssClass_Description")]
        public new string CssClass
        {
            get { return _CssClass; }
            set
            {
                _CssClass = value;
                if (cuw != null) cuw.CssClass = value;
            }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [ResourcesAttribute("DisplayCancelButton_FriendlyName", "DisplayCancelButton_Category", "DisplayCancelButton_Description")]
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
        [ResourcesAttribute("DuplicateEmailErrorMessage_FriendlyName", "DuplicateEmailErrorMessage_Category", "DuplicateEmailErrorMessage_Description")]
        public string DuplicateEmailErrorMessage
        {
            get { return _DuplicateEmailErrorMessage; }
            set { _DuplicateEmailErrorMessage = value; }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [ResourcesAttribute("DuplicateUserNameErrorMessage_FriendlyName", "DuplicateUserNameErrorMessage_Category", "DuplicateUserNameErrorMessage_Description")]
        public string DuplicateUserNameErrorMessage
        {
            get { return _DuplicateUserNameErrorMessage; }
            set { _DuplicateUserNameErrorMessage = value; }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [ResourcesAttribute("EditProfileIconUrl_FriendlyName", "EditProfileIconUrl_Category", "EditProfileIconUrl_Description")]
        public string EditProfileIconUrl
        {
            get { return _EditProfileIconUrl; }
            set { _EditProfileIconUrl = value; }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [ResourcesAttribute("EditProfileText_FriendlyName", "EditProfileText_Category", "EditProfileText_Description")]
        public string EditProfileText
        {
            get { return _EditProfileText; }
            set
            {
                _EditProfileText = value;
                if (cuw != null) cuw.EditProfileText = value;
            }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [ResourcesAttribute("EditProfileUrl_FriendlyName", "EditProfileUrl_Category", "EditProfileUrl_Description")]
        public string EditProfileUrl
        {
            get { return _EditProfileUrl; }
            set { _EditProfileUrl = value; }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [ResourcesAttribute("EmailLabelText_FriendlyName", "EmailLabelText_Category", "EmailLabelText_Description")]
        public string EmailLabelText
        {
            get { return _EmailLabelText; }
            set
            {
                _EmailLabelText = value;
                if (cuw != null) cuw.EmailLabelText = value;
            }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [ResourcesAttribute("EmailRegularExpressionErrorMessage_FriendlyName", "EmailRegularExpressionErrorMessage_Category", "EmailRegularExpressionErrorMessage_Description")]
        public string EmailRegularExpressionErrorMessage
        {
            get { return _EmailRegularExpressionErrorMessage; }
            set { _EmailRegularExpressionErrorMessage = value; }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [ResourcesAttribute("EmailRequiredErrorMessage_FriendlyName", "EmailRequiredErrorMessage_Category", "EmailRequiredErrorMessage_Description")]
        public string EmailRequiredErrorMessage
        {
            get { return _EmailRequiredErrorMessage; }
            set { _EmailRequiredErrorMessage = value; }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [ResourcesAttribute("FinishDestinationPageUrl_FriendlyName", "FinishDestinationPageUrl_Category", "FinishDestinationPageUrl_Description")]
        public string FinishDestinationPageUrl
        {
            get { return _FinishDestinationPageUrl; }
            set { _FinishDestinationPageUrl = value; }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [ResourcesAttribute("HeaderText_FriendlyName", "HeaderText_Category", "HeaderText_Description")]
        public string HeaderText
        {
            get { return _HeaderText; }
            set
            {
                _HeaderText = value;
                if (cuw != null) cuw.HeaderText = value;
            }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [ResourcesAttribute("InstructionText_FriendlyName", "InstructionText_Category", "InstructionText_Description")]
        public string InstructionText
        {
            get { return _InstructionText; }
            set
            {
                _InstructionText = value;
                if (cuw != null) cuw.InstructionText = value;
            }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [ResourcesAttribute("InvalidAnswerErrorMessage_FriendlyName", "InvalidAnswerErrorMessage_Category", "InvalidAnswerErrorMessage_Description")]
        public string InvalidAnswerErrorMessage
        {
            get { return _InvalidAnswerErrorMessage; }
            set { _InvalidAnswerErrorMessage = value; }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [ResourcesAttribute("InvalidEmailErrorMessage_FriendlyName", "InvalidEmailErrorMessage_Category", "InvalidEmailErrorMessage_Description")]
        public string InvalidEmailErrorMessage
        {
            get { return _InvalidEmailErrorMessage; }
            set { _InvalidEmailErrorMessage = value; }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [ResourcesAttribute("InvalidQuestionErrorMessage_FriendlyName", "InvalidQuestionErrorMessage_Category", "InvalidQuestionErrorMessage_Description")]
        public string InvalidQuestionErrorMessage
        {
            get { return _InvalidQuestionErrorMessage; }
            set { _InvalidQuestionErrorMessage = value; }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [ResourcesAttribute("LoginCreatedUser_FriendlyName", "LoginCreatedUser_Category", "LoginCreatedUser_Description")]
        public bool LoginCreatedUser
        {
            get { return _LoginCreatedUser; }
            set { _LoginCreatedUser = value; }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [ResourcesAttribute("QuestionLabelText_FriendlyName", "QuestionLabelText_Category", "QuestionLabelText_Description")]
        public string QuestionLabelText
        {
            get { return _QuestionLabelText; }
            set
            {
                _QuestionLabelText = value;
                if (cuw != null) cuw.QuestionLabelText = value;
            }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [ResourcesAttribute("QuestionRequiredErrorMessage_FriendlyName", "QuestionRequiredErrorMessage_Category", "QuestionRequiredErrorMessage_Description")]
        public string QuestionRequiredErrorMessage
        {
            get { return _QuestionRequiredErrorMessage; }
            set { _QuestionRequiredErrorMessage = value; }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [ResourcesAttribute("UnknownErrorMessage_FriendlyName", "UnknownErrorMessage_Category", "UnknownErrorMessage_Description")]
        public string UnknownErrorMessage
        {
            get { return _UnknownErrorMessage; }
            set { _UnknownErrorMessage = value; }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [ResourcesAttribute("UserNameLabelText_FriendlyName", "UserNameLabelText_Category", "UserNameLabelText_Description")]
        public string UserNameLabelText
        {
            get { return _UserNameLabelText; }
            set
            {
                _UserNameLabelText = value;
                if (cuw != null) cuw.UserNameLabelText = value;
            }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [ResourcesAttribute("UserNameRequiredErrorMessage_FriendlyName", "UserNameRequiredErrorMessage_Category", "UserNameRequiredErrorMessage_Description")]
        public string UserNameRequiredErrorMessage
        {
            get { return _UserNameRequiredErrorMessage; }
            set { _UserNameRequiredErrorMessage = value; }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [ResourcesAttribute("FirstNameLabelText_FriendlyName", "FirstNameLabelText_Category", "FirstNameLabelText_Description")]
        public string FirstNameLabelText
        {
            get { return _FirstNameLabelText; }
            set { _FirstNameLabelText = value; }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [ResourcesAttribute("LastNameLabelText_FriendlyName", "LastNameLabelText_Category", "LastNameLabelText_Description")]
        public string LastNameLabelText
        {
            get { return _LastNameLabelText; }
            set { _LastNameLabelText = value; }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [ResourcesAttribute("FirstNameRequiredErrorMessage_FriendlyName", "FirstNameRequiredErrorMessage_Category", "FirstNameRequiredErrorMessage_Description")]
        public string FirstNameRequiredErrorMessage
        {
            get { return _FirstNameRequiredErrorMessage; }
            set { _FirstNameRequiredErrorMessage = value; }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [ResourcesAttribute("LastNameRequiredErrorMessage_FriendlyName", "LastNameRequiredErrorMessage_Category", "LastNameRequiredErrorMessage_Description")]
        public string LastNameRequiredErrorMessage
        {
            get { return _LastNameRequiredErrorMessage; }
            set { _LastNameRequiredErrorMessage = value; }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [ResourcesAttribute("HipPictureLabelText_FriendlyName", "HipPictureLabelText_Category", "HipPictureLabelText_Description")]
        public string HipPictureLabelText
        {
            get { return _HipPictureLabelText; }
            set { _HipPictureLabelText = value; }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [ResourcesAttribute("HipCharactersLabelText_FriendlyName", "HipCharactersLabelText_Category", "HipCharactersLabelText_Description")]
        public string HipCharactersLabelText
        {
            get { return _HipCharactersLabelText; }
            set { _HipCharactersLabelText = value; }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [ResourcesAttribute("HipInstructionsLabelText_FriendlyName", "HipInstructionsLabelText_Category", "HipInstructionsLabelText_Description")]
        public string HipInstructionsLabelText
        {
            get { return _HipInstructionsLabelText; }
            set { _HipInstructionsLabelText = value; }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [ResourcesAttribute("HipPictureDescription_FriendlyName", "HipPictureDescription_Category", "HipPictureDescription_Description")]
        public string HipPictureDescription
        {
            get { return _HipPictureDescription; }
            set { _HipPictureDescription = value; }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [ResourcesAttribute("HipResetLabelText_FriendlyName", "HipResetLabelText_Category", "HipResetLabelText_Description")]
        public string HipResetLabelText
        {
            get { return _HipResetLabelText; }
            set { _HipResetLabelText = value; }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [ResourcesAttribute("HipErrorMessage_FriendlyName", "HipErrorMessage_Category", "HipErrorMessage_Description")]
        public string HipErrorMessage
        {
            get { return _HipErrorMessage; }
            set { _HipErrorMessage = value; }
        }
        #endregion

        #region Controls
        protected MembershipRequestControl cuw;
        #endregion

        #region Methods
        private void AddCreateUserControl()
        {
            cuw = new MembershipRequestControl();
            cuw.ID = "fbaCreateUserWizard";
            cuw.AutoGeneratePassword = true;

            cuw.MembershipProvider = Utils.GetMembershipProvider(Context);
            cuw.AnswerLabelText = AnswerLabelText;
            cuw.AnswerRequiredErrorMessage = AnswerRequiredErrorMessage;
            cuw.CancelButtonImageUrl = CancelButtonImageUrl;
            cuw.CancelButtonText = CancelButtonText;
            cuw.CancelButtonType = CancelButtonType;
            cuw.CancelDestinationPageUrl = CancelDestinationPageUrl;
            cuw.CompleteSuccessText = CompleteSuccessText;
            cuw.CreateUserButtonImageUrl = CreateUserButtonImageUrl;
            cuw.CreateUserButtonText = CreateUserButtonText;
            cuw.CreateUserButtonType = CreateUserButtonType;
            cuw.CssClass = CssClass;
            cuw.DisplayCancelButton = DisplayCancelButton;
            cuw.DuplicateEmailErrorMessage = DuplicateEmailErrorMessage;
            cuw.DuplicateUserNameErrorMessage = DuplicateUserNameErrorMessage;
            cuw.EditProfileIconUrl = EditProfileIconUrl;
            cuw.EditProfileText = EditProfileText;
            cuw.EditProfileUrl = EditProfileUrl;
            cuw.EmailLabelText = EmailLabelText;
            cuw.EmailRegularExpressionErrorMessage = EmailRegularExpressionErrorMessage;
            cuw.EmailRequiredErrorMessage = EmailRequiredErrorMessage;
            cuw.FinishDestinationPageUrl = FinishDestinationPageUrl;
            cuw.HeaderText = HeaderText;
            cuw.InstructionText = InstructionText;
            cuw.InvalidAnswerErrorMessage = InvalidAnswerErrorMessage;
            cuw.InvalidEmailErrorMessage = InvalidEmailErrorMessage;
            cuw.InvalidQuestionErrorMessage = InvalidQuestionErrorMessage;
            cuw.LoginCreatedUser = false;
            cuw.SPLoginCreatedUser = LoginCreatedUser;
            cuw.QuestionLabelText = QuestionLabelText;
            cuw.QuestionRequiredErrorMessage = QuestionRequiredErrorMessage;
            cuw.UnknownErrorMessage = UnknownErrorMessage;
            cuw.UserNameLabelText = UserNameLabelText;
            cuw.UserNameRequiredErrorMessage = UserNameRequiredErrorMessage;
            cuw.FirstNameLabelText = FirstNameLabelText;
            cuw.FirstNameRequiredErrorMessage = FirstNameRequiredErrorMessage;
            cuw.LastNameLabelText = LastNameLabelText;
            cuw.LastNameRequiredErrorMessage = LastNameRequiredErrorMessage;
            cuw.HipCharactersLabelText = HipCharactersLabelText;
            cuw.HipErrorMessage = HipErrorMessage;
            cuw.HipInstructionsLabelText = HipInstructionsLabelText;
            cuw.HipPictureDescription = HipPictureDescription;
            cuw.HipPictureLabelText = HipPictureLabelText;
            cuw.HipResetLabelText = HipResetLabelText;
            cuw.DefaultGroup = this._GroupName;
            Controls.Add(cuw);
        }
        #region Rendering Methods
        protected override void CreateChildControls()
        {
            AddCreateUserControl();
        }

        protected override void OnInit(EventArgs e)
        {
            //Initialize the group name.  Calls to the sharepoint object model don't work in the constructor.
            //Only set it if it's empty - as personalization will already have occurred by this point.
            if (GroupName == String.Empty)
            {
                SPSecurity.RunWithElevatedPrivileges(delegate()
                {
                    SPSite site = new SPSite(HttpContext.Current.Request.Url.ToString());
                    SPWeb web = site.OpenWeb();

                    SPGroupCollection groups = web.Groups;
                    if (groups.Count > 0)
                    {
                        GroupName = groups[0].Name;
                    }
                });
            }

            base.OnInit(e);
        }

        protected override void RenderContents(HtmlTextWriter writer)
        {
            EnsureChildControls();
            base.RenderContents(writer);
        }
        #endregion

        public override string LoadResource(string id)
        {
            return (_resourceManager.GetString(id));
        }
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

