using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using System.Globalization;
using System.Net;
using System.IO;

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
        public const string MEMBERSHIPAPPROVED = "MembershipApprovedXSLT";
        public const string MEMBERSHIPPENDING = "MembershipPendingXSLT";
        public const string MEMBERSHIPREJECTED = "MembershipRejectedXSLT";
        public const string PASSWORDRECOVERY = "PasswordRecoveryXSLT";
        public const string RESETPASSWORD = "ResetPasswordXSLT";
    }

    public struct MembershipReviewMigratedFields
    {
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
        
        public MembershipSettings(SPWeb web)
        {
            _web = web;
            _site = _web.Site;

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

        /// <summary>
        /// Returns the contents of the file specified in the migrated key - if it exists
        /// If the file doesn't exist, then just return the default value
        /// </summary>
        /// <param name="migratedKey"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        private string GetMigratedXSLT(string migratedKey, string defaultValue)
        {
            string result = defaultValue;

            try
            {
                string url = Utils.GetWebProperty(migratedKey, "", _web);

                if (!String.IsNullOrEmpty(url))
                {
                    url = Utils.GetAbsoluteURL(_web, url);
                    string contents = string.Empty;
                    WebRequest request = WebRequest.Create(url);
                    request.Credentials = CredentialCache.DefaultCredentials;
                    using (WebResponse response = request.GetResponse())
                    {
                        using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                        {
                            result = reader.ReadToEnd();
                        }
                        response.Close();
                    }
                }
            }
            catch
            {
            }

            return result;
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
                return Utils.GetWebProperty(MembershipReviewSiteXSLTEmail.MEMBERSHIPAPPROVED, GetMigratedXSLT(MembershipReviewMigratedFields.MEMBERSHIPAPPROVED, LocalizedString.GetGlobalString("FBAPackWebPages", "MembershipApprovedXSLT")), _web, true);
            }

            set
            {
                Utils.SetWebProperty(MembershipReviewSiteXSLTEmail.MEMBERSHIPAPPROVED, value, _web);
            }
        }

        public string MembershipPendingEmail
        {
            get
            {
                return Utils.GetWebProperty(MembershipReviewSiteXSLTEmail.MEMBERSHIPPENDING, GetMigratedXSLT(MembershipReviewMigratedFields.MEMBERSHIPPENDING, LocalizedString.GetGlobalString("FBAPackWebPages", "MembershipPendingXSLT")), _web, true);
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
                return Utils.GetWebProperty(MembershipReviewSiteXSLTEmail.MEMBERSHIPREJECTED, GetMigratedXSLT(MembershipReviewMigratedFields.MEMBERSHIPREJECTED, LocalizedString.GetGlobalString("FBAPackWebPages", "MembershipRejectedXSLT")), _web, true);
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
                return Utils.GetWebProperty(MembershipReviewSiteXSLTEmail.PASSWORDRECOVERY, GetMigratedXSLT(MembershipReviewMigratedFields.PASSWORDRECOVERY, LocalizedString.GetGlobalString("FBAPackWebPages", "PasswordRecoveryXSLT")), _web, true);
            }

            set
            {
                Utils.SetWebProperty(MembershipReviewSiteXSLTEmail.PASSWORDRECOVERY, value, _web);
            }
        }

        public string ResetPasswordEmail
        {
            get
            {
                return Utils.GetWebProperty(MembershipReviewSiteXSLTEmail.RESETPASSWORD, LocalizedString.GetGlobalString("FBAPackWebPages", "ResetPasswordXSLT"), _web, true);
            }

            set
            {
                Utils.SetWebProperty(MembershipReviewSiteXSLTEmail.RESETPASSWORD, value, _web);
            }
        }
    }
}
