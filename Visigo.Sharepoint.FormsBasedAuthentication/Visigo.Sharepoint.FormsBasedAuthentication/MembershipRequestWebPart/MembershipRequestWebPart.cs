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
        private string _ContinueButtonImageUrl = String.Empty;
        private string _ContinueButtonText = String.Empty;
        private ButtonType _ContinueButtonType = ButtonType.Button;
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
            try
            {
                //Default to the current url
                FinishDestinationPageUrl = SPUtility.GetPageUrlPath(HttpContext.Current);
            }
            catch (Exception ex)
            {
                Utils.LogError(ex);
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
        [LocalizedWebDisplayName("FBAPackMembershipRequestWebPart", "CreateUserStepTemplate_FriendlyName")]
        [LocalizedCategory("FBAPackMembershipRequestWebPart", "CreateUserStepTemplate_Category")]
        [LocalizedWebDescription("FBAPackMembershipRequestWebPart", "CreateUserStepTemplate_Description")]
        public string CreateUserStepTemplate { get; set; }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [LocalizedWebDisplayName("FBAPackMembershipRequestWebPart", "CompleteStepTemplate_FriendlyName")]
        [LocalizedCategory("FBAPackMembershipRequestWebPart", "CompleteStepTemplate_Category")]
        [LocalizedWebDescription("FBAPackMembershipRequestWebPart", "CompleteStepTemplate_Description")]
        public string CompleteStepTemplate { get; set; }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [LocalizedWebDisplayName("FBAPackMembershipRequestWebPart", "AnswerLabelText_FriendlyName")]
        [LocalizedCategory("FBAPackMembershipRequestWebPart", "AnswerLabelText_Category")]
        [LocalizedWebDescription("FBAPackMembershipRequestWebPart", "AnswerLabelText_Description")]
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
        [LocalizedWebDisplayName("FBAPackMembershipRequestWebPart", "AnswerRequiredErrorMessage_FriendlyName")]
        [LocalizedCategory("FBAPackMembershipRequestWebPart", "AnswerRequiredErrorMessage_Category")]
        [LocalizedWebDescription("FBAPackMembershipRequestWebPart", "AnswerRequiredErrorMessage_Description")]
        public string AnswerRequiredErrorMessage
        {
            get { return _AnswerRequiredErrorMessage; }
            set { _AnswerRequiredErrorMessage = value; }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [LocalizedWebDisplayName("FBAPackMembershipRequestWebPart", "CancelButtonImageUrl_FriendlyName")]
        [LocalizedCategory("FBAPackMembershipRequestWebPart", "CancelButtonImageUrl_Category")]
        [LocalizedWebDescription("FBAPackMembershipRequestWebPart", "CancelButtonImageUrl_Description")]
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
        [LocalizedWebDisplayName("FBAPackMembershipRequestWebPart", "CancelButtonText_FriendlyName")]
        [LocalizedCategory("FBAPackMembershipRequestWebPart", "CancelButtonText_Category")]
        [LocalizedWebDescription("FBAPackMembershipRequestWebPart", "CancelButtonText_Description")]
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
            get { return _CancelDestinationPageUrl; }
            set { _CancelDestinationPageUrl = value; }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [LocalizedWebDisplayName("FBAPackMembershipRequestWebPart", "CompleteSuccessText_FriendlyName")]
        [LocalizedCategory("FBAPackMembershipRequestWebPart", "CompleteSuccessText_Category")]
        [LocalizedWebDescription("FBAPackMembershipRequestWebPart", "CompleteSuccessText_Description")]
        public string CompleteSuccessText
        {
            get { return _CompleteSuccessText; }
            set { _CompleteSuccessText = value; }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [LocalizedWebDisplayName("FBAPackMembershipRequestWebPart", "ContinueButtonImageUrl_FriendlyName")]
        [LocalizedCategory("FBAPackMembershipRequestWebPart", "ContinueButtonImageUrl_Category")]
        [LocalizedWebDescription("FBAPackMembershipRequestWebPart", "ContinueButtonImageUrl_Description")]
        public string ContinueButtonImageUrl
        {
            get { return _ContinueButtonImageUrl; }
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
            get { return _ContinueButtonText; }
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
            get { return _CreateUserButtonImageUrl; }
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
            get { return _CreateUserButtonText; }
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
            get { return _CssClass; }
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
            get { return _DuplicateEmailErrorMessage; }
            set { _DuplicateEmailErrorMessage = value; }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [LocalizedWebDisplayName("FBAPackMembershipRequestWebPart", "DuplicateUserNameErrorMessage_FriendlyName")]
        [LocalizedCategory("FBAPackMembershipRequestWebPart", "DuplicateUserNameErrorMessage_Category")]
        [LocalizedWebDescription("FBAPackMembershipRequestWebPart", "DuplicateUserNameErrorMessage_Description")]
        public string DuplicateUserNameErrorMessage
        {
            get { return _DuplicateUserNameErrorMessage; }
            set { _DuplicateUserNameErrorMessage = value; }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [LocalizedWebDisplayName("FBAPackMembershipRequestWebPart", "EditProfileIconUrl_FriendlyName")]
        [LocalizedCategory("FBAPackMembershipRequestWebPart", "EditProfileIconUrl_Category")]
        [LocalizedWebDescription("FBAPackMembershipRequestWebPart", "EditProfileIconUrl_Description")]
        public string EditProfileIconUrl
        {
            get { return _EditProfileIconUrl; }
            set { _EditProfileIconUrl = value; }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [LocalizedWebDisplayName("FBAPackMembershipRequestWebPart", "EditProfileText_FriendlyName")]
        [LocalizedCategory("FBAPackMembershipRequestWebPart", "EditProfileText_Category")]
        [LocalizedWebDescription("FBAPackMembershipRequestWebPart", "EditProfileText_Description")]
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
        [LocalizedWebDisplayName("FBAPackMembershipRequestWebPart", "EditProfileUrl_FriendlyName")]
        [LocalizedCategory("FBAPackMembershipRequestWebPart", "EditProfileUrl_Category")]
        [LocalizedWebDescription("FBAPackMembershipRequestWebPart", "EditProfileUrl_Description")]
        public string EditProfileUrl
        {
            get { return _EditProfileUrl; }
            set { _EditProfileUrl = value; }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [LocalizedWebDisplayName("FBAPackMembershipRequestWebPart", "EmailLabelText_FriendlyName")]
        [LocalizedCategory("FBAPackMembershipRequestWebPart", "EmailLabelText_Category")]
        [LocalizedWebDescription("FBAPackMembershipRequestWebPart", "EmailLabelText_Description")]
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
        [LocalizedWebDisplayName("FBAPackMembershipRequestWebPart", "EmailRegularExpressionErrorMessage_FriendlyName")]
        [LocalizedCategory("FBAPackMembershipRequestWebPart", "EmailRegularExpressionErrorMessage_Category")]
        [LocalizedWebDescription("FBAPackMembershipRequestWebPart", "EmailRegularExpressionErrorMessage_Description")]
        public string EmailRegularExpressionErrorMessage
        {
            get { return _EmailRegularExpressionErrorMessage; }
            set { _EmailRegularExpressionErrorMessage = value; }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [LocalizedWebDisplayName("FBAPackMembershipRequestWebPart", "EmailRequiredErrorMessage_FriendlyName")]
        [LocalizedCategory("FBAPackMembershipRequestWebPart", "EmailRequiredErrorMessage_Category")]
        [LocalizedWebDescription("FBAPackMembershipRequestWebPart", "EmailRequiredErrorMessage_Description")]
        public string EmailRequiredErrorMessage
        {
            get { return _EmailRequiredErrorMessage; }
            set { _EmailRequiredErrorMessage = value; }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [LocalizedWebDisplayName("FBAPackMembershipRequestWebPart", "FinishDestinationPageUrl_FriendlyName")]
        [LocalizedCategory("FBAPackMembershipRequestWebPart", "FinishDestinationPageUrl_Category")]
        [LocalizedWebDescription("FBAPackMembershipRequestWebPart", "FinishDestinationPageUrl_Description")]
        public string FinishDestinationPageUrl
        {
            get { return _FinishDestinationPageUrl; }
            set { _FinishDestinationPageUrl = value; }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [LocalizedWebDisplayName("FBAPackMembershipRequestWebPart", "HeaderText_FriendlyName")]
        [LocalizedCategory("FBAPackMembershipRequestWebPart", "HeaderText_Category")]
        [LocalizedWebDescription("FBAPackMembershipRequestWebPart", "HeaderText_Description")]
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
        [LocalizedWebDisplayName("FBAPackMembershipRequestWebPart", "InstructionText_FriendlyName")]
        [LocalizedCategory("FBAPackMembershipRequestWebPart", "InstructionText_Category")]
        [LocalizedWebDescription("FBAPackMembershipRequestWebPart", "InstructionText_Description")]
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
        [LocalizedWebDisplayName("FBAPackMembershipRequestWebPart", "InvalidAnswerErrorMessage_FriendlyName")]
        [LocalizedCategory("FBAPackMembershipRequestWebPart", "InvalidAnswerErrorMessage_Category")]
        [LocalizedWebDescription("FBAPackMembershipRequestWebPart", "InvalidAnswerErrorMessage_Description")]
        public string InvalidAnswerErrorMessage
        {
            get { return _InvalidAnswerErrorMessage; }
            set { _InvalidAnswerErrorMessage = value; }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [LocalizedWebDisplayName("FBAPackMembershipRequestWebPart", "InvalidEmailErrorMessage_FriendlyName")]
        [LocalizedCategory("FBAPackMembershipRequestWebPart", "InvalidEmailErrorMessage_Category")]
        [LocalizedWebDescription("FBAPackMembershipRequestWebPart", "InvalidEmailErrorMessage_Description")]
        public string InvalidEmailErrorMessage
        {
            get { return _InvalidEmailErrorMessage; }
            set { _InvalidEmailErrorMessage = value; }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [LocalizedWebDisplayName("FBAPackMembershipRequestWebPart", "InvalidQuestionErrorMessage_FriendlyName")]
        [LocalizedCategory("FBAPackMembershipRequestWebPart", "InvalidQuestionErrorMessage_Category")]
        [LocalizedWebDescription("FBAPackMembershipRequestWebPart", "InvalidQuestionErrorMessage_Description")]
        public string InvalidQuestionErrorMessage
        {
            get { return _InvalidQuestionErrorMessage; }
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
            get { return _QuestionLabelText; }
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
            get { return _QuestionRequiredErrorMessage; }
            set { _QuestionRequiredErrorMessage = value; }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [LocalizedWebDisplayName("FBAPackMembershipRequestWebPart", "UnknownErrorMessage_FriendlyName")]
        [LocalizedCategory("FBAPackMembershipRequestWebPart", "UnknownErrorMessage_Category")]
        [LocalizedWebDescription("FBAPackMembershipRequestWebPart", "UnknownErrorMessage_Description")]
        public string UnknownErrorMessage
        {
            get { return _UnknownErrorMessage; }
            set { _UnknownErrorMessage = value; }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [LocalizedWebDisplayName("FBAPackMembershipRequestWebPart", "UserNameLabelText_FriendlyName")]
        [LocalizedCategory("FBAPackMembershipRequestWebPart", "UserNameLabelText_Category")]
        [LocalizedWebDescription("FBAPackMembershipRequestWebPart", "UserNameLabelText_Description")]
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
        [LocalizedWebDisplayName("FBAPackMembershipRequestWebPart", "UserNameRequiredErrorMessage_FriendlyName")]
        [LocalizedCategory("FBAPackMembershipRequestWebPart", "UserNameRequiredErrorMessage_Category")]
        [LocalizedWebDescription("FBAPackMembershipRequestWebPart", "UserNameRequiredErrorMessage_Description")]
        public string UserNameRequiredErrorMessage
        {
            get { return _UserNameRequiredErrorMessage; }
            set { _UserNameRequiredErrorMessage = value; }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [LocalizedWebDisplayName("FBAPackMembershipRequestWebPart", "FirstNameLabelText_FriendlyName")]
        [LocalizedCategory("FBAPackMembershipRequestWebPart", "FirstNameLabelText_Category")]
        [LocalizedWebDescription("FBAPackMembershipRequestWebPart", "FirstNameLabelText_Description")]
        public string FirstNameLabelText
        {
            get { return _FirstNameLabelText; }
            set { _FirstNameLabelText = value; }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [LocalizedWebDisplayName("FBAPackMembershipRequestWebPart", "LastNameLabelText_FriendlyName")]
        [LocalizedCategory("FBAPackMembershipRequestWebPart", "LastNameLabelText_Category")]
        [LocalizedWebDescription("FBAPackMembershipRequestWebPart", "LastNameLabelText_Description")]
        public string LastNameLabelText
        {
            get { return _LastNameLabelText; }
            set { _LastNameLabelText = value; }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [LocalizedWebDisplayName("FBAPackMembershipRequestWebPart", "FirstNameRequiredErrorMessage_FriendlyName")]
        [LocalizedCategory("FBAPackMembershipRequestWebPart", "FirstNameRequiredErrorMessage_Category")]
        [LocalizedWebDescription("FBAPackMembershipRequestWebPart", "FirstNameRequiredErrorMessage_Description")]
        public string FirstNameRequiredErrorMessage
        {
            get { return _FirstNameRequiredErrorMessage; }
            set { _FirstNameRequiredErrorMessage = value; }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [LocalizedWebDisplayName("FBAPackMembershipRequestWebPart", "LastNameRequiredErrorMessage_FriendlyName")]
        [LocalizedCategory("FBAPackMembershipRequestWebPart", "LastNameRequiredErrorMessage_Category")]
        [LocalizedWebDescription("FBAPackMembershipRequestWebPart", "LastNameRequiredErrorMessage_Description")]
        public string LastNameRequiredErrorMessage
        {
            get { return _LastNameRequiredErrorMessage; }
            set { _LastNameRequiredErrorMessage = value; }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [LocalizedWebDisplayName("FBAPackMembershipRequestWebPart", "HipPictureLabelText_FriendlyName")]
        [LocalizedCategory("FBAPackMembershipRequestWebPart", "HipPictureLabelText_Category")]
        [LocalizedWebDescription("FBAPackMembershipRequestWebPart", "HipPictureLabelText_Description")]
        public string HipPictureLabelText
        {
            get { return _HipPictureLabelText; }
            set { _HipPictureLabelText = value; }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [LocalizedWebDisplayName("FBAPackMembershipRequestWebPart", "HipCharactersLabelText_FriendlyName")]
        [LocalizedCategory("FBAPackMembershipRequestWebPart", "HipCharactersLabelText_Category")]
        [LocalizedWebDescription("FBAPackMembershipRequestWebPart", "HipCharactersLabelText_Description")]
        public string HipCharactersLabelText
        {
            get { return _HipCharactersLabelText; }
            set { _HipCharactersLabelText = value; }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [LocalizedWebDisplayName("FBAPackMembershipRequestWebPart", "HipInstructionsLabelText_FriendlyName")]
        [LocalizedCategory("FBAPackMembershipRequestWebPart", "HipInstructionsLabelText_Category")]
        [LocalizedWebDescription("FBAPackMembershipRequestWebPart", "HipInstructionsLabelText_Description")]
        public string HipInstructionsLabelText
        {
            get { return _HipInstructionsLabelText; }
            set { _HipInstructionsLabelText = value; }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [LocalizedWebDisplayName("FBAPackMembershipRequestWebPart", "HipPictureDescription_FriendlyName")]
        [LocalizedCategory("FBAPackMembershipRequestWebPart", "HipPictureDescription_Category")]
        [LocalizedWebDescription("FBAPackMembershipRequestWebPart", "HipPictureDescription_Description")]
        public string HipPictureDescription
        {
            get { return _HipPictureDescription; }
            set { _HipPictureDescription = value; }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [LocalizedWebDisplayName("FBAPackMembershipRequestWebPart", "HipResetLabelText_FriendlyName")]
        [LocalizedCategory("FBAPackMembershipRequestWebPart", "HipResetLabelText_Category")]
        [LocalizedWebDescription("FBAPackMembershipRequestWebPart", "HipResetLabelText_Description")]
        public string HipResetLabelText
        {
            get { return _HipResetLabelText; }
            set { _HipResetLabelText = value; }
        }

        [Personalizable(PersonalizationScope.Shared), WebBrowsable()]
        [LocalizedWebDisplayName("FBAPackMembershipRequestWebPart", "HipErrorMessage_FriendlyName")]
        [LocalizedCategory("FBAPackMembershipRequestWebPart", "HipErrorMessage_Category")]
        [LocalizedWebDescription("FBAPackMembershipRequestWebPart", "HipErrorMessage_Description")]
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
            TemplateHelper helper;

            cuw = new MembershipRequestControl();
            cuw.CreateUserStep.ContentTemplate = new TemplateLoader(CreateUserStepTemplate, Page);
            cuw.CompleteStep.ContentTemplate = new TemplateLoader(CompleteStepTemplate, Page);
            cuw.ID = "FBACreateUserWizard";
            cuw.AutoGeneratePassword = true;
            cuw.MembershipProvider = Utils.GetMembershipProvider(Context);
            cuw.DuplicateEmailErrorMessage = DuplicateEmailErrorMessage;
            cuw.DuplicateUserNameErrorMessage = DuplicateUserNameErrorMessage;
            cuw.EmailRegularExpressionErrorMessage = EmailRegularExpressionErrorMessage;
            cuw.InvalidAnswerErrorMessage = InvalidAnswerErrorMessage;
            cuw.InvalidEmailErrorMessage = InvalidEmailErrorMessage;
            cuw.InvalidQuestionErrorMessage = InvalidQuestionErrorMessage;
            cuw.LoginCreatedUser = false;
            cuw.SPLoginCreatedUser = LoginCreatedUser;
            cuw.UnknownErrorMessage = UnknownErrorMessage;
            cuw.FinishDestinationPageUrl = FinishDestinationPageUrl;
            cuw.CancelDestinationPageUrl = CancelDestinationPageUrl;
            cuw.DefaultGroup = this._GroupName;
            cuw.CreateUserStep.CustomNavigationTemplateContainer.Controls[0].Visible = false;

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

            if (Utils.BaseMembershipProvider().RequiresQuestionAndAnswer)
            {
                helper.SetVisible("QuestionRow", true);
                helper.SetVisible("AnswerRow", true);
                helper.SetText("QuestionLabel", QuestionLabelText);
                helper.SetValidation("QuestionRequired", QuestionRequiredErrorMessage, this.UniqueID);
                helper.SetText("AnswerLabel", AnswerLabelText);
                helper.SetValidation("AnswerRequired", AnswerRequiredErrorMessage, this.UniqueID);
            }

            helper.SetText("HipPictureLabel", HipPictureLabelText);
            helper.SetText("HipInstructionsLabel", HipInstructionsLabelText);
            helper.SetText("HipPictureDescriptionLabel", HipPictureDescription);
            helper.SetText("HipAnswerLabel", HipCharactersLabelText);
            helper.SetValidation("HipAnswerValidator", HipErrorMessage, this.UniqueID);
            helper.SetButton("HipReset", HipResetLabelText, "");

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

        protected override void OnInit(EventArgs e)
        {
            //Initialize the group name.  Calls to the sharepoint object model don't work in the constructor.
            //Only set it if it's empty - as personalization will already have occurred by this point.
            if (GroupName == String.Empty)
            {
                SPSecurity.RunWithElevatedPrivileges(delegate()
                {
                    using (SPSite site = new SPSite(SPContext.Current.Site.ID, SPContext.Current.Site.Zone))
                    {
                        using (SPWeb web = site.OpenWeb())
                        {

                            SPGroupCollection groups = web.SiteGroups;
                            if (groups.Count > 0)
                            {
                                GroupName = groups[0].Name;
                            }
                        }
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

