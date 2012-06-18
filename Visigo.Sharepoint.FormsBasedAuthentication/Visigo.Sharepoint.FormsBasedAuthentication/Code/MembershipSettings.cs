using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using System.Globalization;

namespace Visigo.Sharepoint.FormsBasedAuthentication
{
    public enum MembershipStatus
    {
        Pending = 0,
        Approved = 1,
        Rejected = 2
    }

    public struct MembershipOptions
    {
        public const string ENABLEROLES = "EnableRoles";
        public const string REVIEWMEMBERSHIPREQUESTS = "ReviewMembershipRequests";
    }

    public struct MembershipReviewListFields
    {
        public const string REQUESTID = "RequestID";
        public const string FIRSTNAME = "FirstName";
        public const string LASTNAME = "LastNamePhonetic";
        public const string USERNAME = "Title";
        public const string EMAIL = "Email";
        public const string DATESUBMITTED = "_DCDateCreated";
        public const string STATUS = "_Status";
        public const string RECOVERPASSWORDQUESTION = "RecoverPasswordSecretQuestion";
        public const string RECOVERPASSWORDANSWER = "RecoverPasswordSecretAnswer";
        public const string DEFAULTGROUP = "DefaultGroup";
    }

    public struct MembershipReviewSiteURL
    {
        public const string CHANGEPASSWORDPAGE = "ChangePasswordPage";
        public const string PASSWORDQUESTIONPAGE = "PasswordReminderPage";
        public const string THANKYOUPAGE = "ThankYouPage";
    }

    public struct MembershipReviewSiteXSLTEmail
    {
        public const string MEMBERSHIPREPLYTO = "MembershipReplyTo";
        public const string MEMBERSHIPAPPROVED = "MembershipApproved";
        public const string MEMBERSHIPERROR = "MembershipError";
        public const string MEMBERSHIPPENDING = "MembershipPending";
        public const string MEMBERSHIPREJECTED = "MembershipRejected";
        public const string PASSWORDRECOVERY = "PasswordRecovery";
    }

    public struct MembershipList
    {
        public const string MEMBERSHIPREVIEWLIST = "Site Membership Review List";
    }

    public class MembershipSettings
    {
        private SPWeb _web;
        private SPSite _site;
        
        private static Dictionary<Guid,string> _membershipApprovedDefault = new Dictionary<Guid,string>();
        private static Dictionary<Guid, string> _membershipErrorDefault = new Dictionary<Guid,string>();
        private static Dictionary<Guid, string> _membershipPendingDefault = new Dictionary<Guid,string>();
        private static Dictionary<Guid, string> _membershipRejectedDefault = new Dictionary<Guid,string>();
        private static Dictionary<Guid, string> _passwordRecoveryDefault = new Dictionary<Guid,string>();

        public MembershipSettings(SPWeb web)
        {
            _web = web;
            _site = _web.Site;

            //Initialize default values for web if not previously initialized
            if (!_membershipApprovedDefault.ContainsKey(_web.ID))
            {
                _membershipApprovedDefault[_web.ID] = GetTemplateDefaultPath("MembershipApproved.xslt", _web);
                _membershipErrorDefault[_web.ID] = GetTemplateDefaultPath("MembershipError.xslt", _web);
                _membershipPendingDefault[_web.ID] = GetTemplateDefaultPath("MembershipPending.xslt", _web);
                _membershipRejectedDefault[_web.ID] = GetTemplateDefaultPath("MembershipRejected.xslt", _web);
                _passwordRecoveryDefault[_web.ID] = GetTemplateDefaultPath("PasswordRecovery.xslt", _web);
            }
        }

        private string GetTemplateDefaultPath(string filename, SPWeb web)
        {
            string path = string.Format("/_layouts/FBA/emails/{0}/{1}", CultureInfo.CurrentUICulture.LCID.ToString(), filename);

            //Return the localized path if it exists, otherwise return the default path
            try
            {
                if (System.IO.File.Exists(System.Web.HttpContext.Current.Server.MapPath(path)))
                {
                    return path;
                }
            }
            catch
            {
            }

            return string.Format("/_layouts/FBA/emails/{0}", filename);
        }        

        public bool EnableRoles
        {
            get
            {
                return Utils.GetSiteProperty(MembershipOptions.ENABLEROLES, false, _site);
            }

            set
            {
                Utils.SetSiteProperty(MembershipOptions.ENABLEROLES, value, _site);
            }
        }

        public bool ReviewMembershipRequests
        {
            get
            {
                return Utils.GetSiteProperty(MembershipOptions.REVIEWMEMBERSHIPREQUESTS, false, _site);
            }

            set
            {
                Utils.SetSiteProperty(MembershipOptions.REVIEWMEMBERSHIPREQUESTS, value, _site);
            }
        }

        public string ChangePasswordPage
        {
            get
            {
                return Utils.GetWebProperty(MembershipReviewSiteURL.CHANGEPASSWORDPAGE, "_Layouts/FBA/ChangePassword.aspx", _web);
            }

            set
            {
                Utils.SetWebProperty(MembershipReviewSiteURL.CHANGEPASSWORDPAGE, value, _web);
            }
        }

        public string PasswordQuestionPage
        {
            get
            {
                return Utils.GetWebProperty(MembershipReviewSiteURL.PASSWORDQUESTIONPAGE, "Pages/PasswordQuestion.aspx", _web);
            }

            set
            {
                Utils.SetWebProperty(MembershipReviewSiteURL.PASSWORDQUESTIONPAGE, value, _web);
            }
        }

        public string ThankYouPage
        {
            get
            {
                return Utils.GetWebProperty(MembershipReviewSiteURL.THANKYOUPAGE, "Pages/Thankyou.aspx", _web);
            }

            set
            {
                Utils.SetWebProperty(MembershipReviewSiteURL.THANKYOUPAGE, value, _web);
            }
        }

        public string MembershipReplyToEmailAddress
        {
            get
            {
                return Utils.GetWebProperty(MembershipReviewSiteXSLTEmail.MEMBERSHIPREPLYTO, _web.Site.WebApplication.OutboundMailReplyToAddress, _web);
            }

            set
            {
                Utils.SetWebProperty(MembershipReviewSiteXSLTEmail.MEMBERSHIPREPLYTO, value, _web);
            }
        }

        public string MembershipApprovedEmail
        {
            get
            {
                return Utils.GetWebProperty(MembershipReviewSiteXSLTEmail.MEMBERSHIPAPPROVED, _membershipApprovedDefault[_web.ID], _web);
            }

            set
            {
                Utils.SetWebProperty(MembershipReviewSiteXSLTEmail.MEMBERSHIPAPPROVED, value, _web);
            }
        }

        public string MembershipErrorEmail
        {
            get
            {
                return Utils.GetWebProperty(MembershipReviewSiteXSLTEmail.MEMBERSHIPERROR, _membershipErrorDefault[_web.ID], _web);
            }

            set
            {
                Utils.SetWebProperty(MembershipReviewSiteXSLTEmail.MEMBERSHIPERROR, value, _web);
            }
        }

        public string MembershipPendingEmail
        {
            get
            {
                return Utils.GetWebProperty(MembershipReviewSiteXSLTEmail.MEMBERSHIPPENDING, _membershipPendingDefault[_web.ID], _web);
            }

            set
            {
                Utils.SetWebProperty(MembershipReviewSiteXSLTEmail.MEMBERSHIPPENDING, value, _web);
            }
        }

        public string MembershipRejectedEmail
        {
            get
            {
                return Utils.GetWebProperty(MembershipReviewSiteXSLTEmail.MEMBERSHIPREJECTED, _membershipRejectedDefault[_web.ID], _web);
            }

            set
            {
                Utils.SetWebProperty(MembershipReviewSiteXSLTEmail.MEMBERSHIPREJECTED, value, _web);
            }
        }

        public string PasswordRecoveryEmail
        {
            get
            {
                return Utils.GetWebProperty(MembershipReviewSiteXSLTEmail.PASSWORDRECOVERY, _passwordRecoveryDefault[_web.ID], _web);
            }

            set
            {
                Utils.SetWebProperty(MembershipReviewSiteXSLTEmail.PASSWORDRECOVERY, value, _web);
            }
        }
    }
}
