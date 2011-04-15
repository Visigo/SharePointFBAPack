using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SharePoint;

namespace Visigo.Sharepoint.FormsBasedAuthentication
{
    public enum MembershipStatus
    {
        Approved = 1,
        Rejected = 2,
        Pending = 3
    }

    public struct MembershipOptions
    {
        public const string ENABLEROLES = "EnableRoles";
        public const string REVIEWMEMBERSHIPREQUESTS = "ReviewMembershipRequests";
    }

    public struct MembershipReviewListFields
    {
        public const string REQUESTID = "RequestID";
        public const string FIRSTNAME = "First Name";
        public const string LASTNAME = "Last Name";
        public const string USERNAME = "User Name";
        public const string EMAIL = "Email";
        public const string DATESUBMITTED = "Date Submitted";
        public const string DATEAPPROVED = "Date Processed";
        public const string STATUS = "Status";
        public const string RECOVERPASSWORDQUESTION = "Recover Password Secret Question";
        public const string RECOVERPASSWORDANSWER = "Recover Password Secret Answer";
        public const string DEFAULTGROUP = "Default Group";
    }

    public struct MembershipReviewSiteURL
    {
        public const string CHANGEPASSWORDPAGE = "ChangePasswordPage";
        public const string PASSWORDQUESTIONPAGE = "PasswordReminderPage";
        public const string THANKYOUPAGE = "ThankYouPage";
    }

    public struct MembershipReviewSiteXSLTEmail
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
        public MembershipSettings(SPWeb web)
        {
            Web = web;
            Site = Web.Site;
        }

        protected SPWeb Web;
        protected SPSite Site;

        public bool EnableRoles
        {
            get
            {
                return Utils.GetSiteProperty(MembershipOptions.ENABLEROLES, false, Site);
            }

            set
            {
                Utils.SetSiteProperty(MembershipOptions.ENABLEROLES, value, Site);
            }
        }

        public bool ReviewMembershipRequests
        {
            get
            {
                return Utils.GetSiteProperty(MembershipOptions.REVIEWMEMBERSHIPREQUESTS, false, Site);
            }

            set
            {
                Utils.SetSiteProperty(MembershipOptions.REVIEWMEMBERSHIPREQUESTS, value, Site);
            }
        }

        public string ChangePasswordPage
        {
            get
            {
                return Utils.GetWebProperty(MembershipReviewSiteURL.CHANGEPASSWORDPAGE, "Pages/ChangePassword.aspx", Web);
            }

            set
            {
                Utils.SetWebProperty(MembershipReviewSiteURL.CHANGEPASSWORDPAGE, value, Web);
            }
        }

        public string PasswordQuestionPage
        {
            get
            {
                return Utils.GetWebProperty(MembershipReviewSiteURL.PASSWORDQUESTIONPAGE, "Pages/PasswordQuestion.aspx", Web);
            }

            set
            {
                Utils.SetWebProperty(MembershipReviewSiteURL.PASSWORDQUESTIONPAGE, value, Web);
            }
        }

        public string ThankYouPage
        {
            get
            {
                return Utils.GetWebProperty(MembershipReviewSiteURL.THANKYOUPAGE, "Pages/Thankyou.aspx", Web);
            }

            set
            {
                Utils.SetWebProperty(MembershipReviewSiteURL.THANKYOUPAGE, value, Web);
            }
        }

        public string MembershipApprovedEmail
        {
            get
            {
                return Utils.GetWebProperty(MembershipReviewSiteXSLTEmail.MEMBERSHIPAPPROVED, "/_layouts/FBA/emails/MembershipApproved.xslt", Web);
            }

            set
            {
                Utils.SetWebProperty(MembershipReviewSiteXSLTEmail.MEMBERSHIPAPPROVED, value, Web);
            }
        }

        public string MembershipErrorEmail
        {
            get
            {
                return Utils.GetWebProperty(MembershipReviewSiteXSLTEmail.MEMBERSHIPERROR, "/_layouts/FBA/emails/MembershipError.xslt", Web);
            }

            set
            {
                Utils.SetWebProperty(MembershipReviewSiteXSLTEmail.MEMBERSHIPERROR, value, Web);
            }
        }

        public string MembershipPendingEmail
        {
            get
            {
                return Utils.GetWebProperty(MembershipReviewSiteXSLTEmail.MEMBERSHIPPENDING, "/_layouts/FBA/emails/MembershipPending.xslt", Web);
            }

            set
            {
                Utils.SetWebProperty(MembershipReviewSiteXSLTEmail.MEMBERSHIPPENDING, value, Web);
            }
        }

        public string MembershipRejectedEmail
        {
            get
            {
                return Utils.GetWebProperty(MembershipReviewSiteXSLTEmail.MEMBERSHIPREJECTED, "/_layouts/FBA/emails/MembershipRejected.xslt", Web);
            }

            set
            {
                Utils.SetWebProperty(MembershipReviewSiteXSLTEmail.MEMBERSHIPREJECTED, value, Web);
            }
        }

        public string PasswordRecoveryEmail
        {
            get
            {
                return Utils.GetWebProperty(MembershipReviewSiteXSLTEmail.PASSWORDRECOVERY, "/_layouts/FBA/emails/PasswordRecovery.xslt", Web);
            }

            set
            {
                Utils.SetWebProperty(MembershipReviewSiteXSLTEmail.PASSWORDRECOVERY, value, Web);
            }
        }
    }
}
