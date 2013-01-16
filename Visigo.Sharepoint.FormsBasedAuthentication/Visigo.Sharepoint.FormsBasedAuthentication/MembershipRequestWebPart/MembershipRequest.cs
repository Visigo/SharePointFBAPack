using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Security;
using Microsoft.SharePoint;
using System.Collections;
using System.Web;
using System.Reflection;


namespace Visigo.Sharepoint.FormsBasedAuthentication
{
    public class MembershipRequest
    {
        #region Class Level Variables

        protected string _UserName = String.Empty;
        protected string _FirstName = String.Empty;
        protected string _LastName = String.Empty;
        protected string _SiteURL = String.Empty;
        protected string _SiteName = String.Empty;
        protected string _Password = String.Empty;
        protected string _ChangePasswordURL = String.Empty;
        protected string _ThankYouURL = String.Empty;
        protected string _PasswordQuestionURL = String.Empty;
        protected string _UserEmail = String.Empty;
        protected string _PasswordQuestion = String.Empty;
        protected string _PasswordAnswer = String.Empty;
        protected string _DefaultGroup = String.Empty;
        protected bool _LoginCreatedUser = false;

        #endregion

        #region Constructors

        public MembershipRequest()
        {
        }

        public MembershipRequest(SPWeb _web)
        {
            SiteName = _web.Name;
            SiteURL = _web.Url;

            MembershipSettings settings = new MembershipSettings(_web);

            ChangePasswordURL = settings.ChangePasswordPage;
            PasswordQuestionURL = settings.PasswordQuestionPage;
            ThankYouURL = settings.ThankYouPage;
        }

        #endregion

        #region Properties

        public string UserName
        {
            get
            {
                return _UserName;
            }
            set
            {
                _UserName = value;
            }
        }
        public bool LoginCreatedUser {
            get
            {
                return _LoginCreatedUser;
            }
            set
            {
                _LoginCreatedUser = value;
            }
            
            }
        public string FirstName
        {
            get
            {
                return _FirstName;
            }
            set
            {
                _FirstName = value;
            }
        }

        public string LastName
        {
            get
            {
                return _LastName;
            }
            set
            {
                _LastName = value;
            }
        }

        public string SiteURL
        {
            get
            {
                return _SiteURL;
            }
            set
            {
                _SiteURL = value;
            }
        }

        public string SiteName
        {
            get
            {
                return _SiteName;
            }
            set
            {
                _SiteName = value;
            }
        }

        public string Password
        {
            get
            {
                return _Password;
            }
            set
            {
                _Password = value;
            }
        }

        public string ChangePasswordURL
        {
            get
            {
                return _ChangePasswordURL;
            }
            set
            {
                _ChangePasswordURL = value;
            }
        }

        public string ThankYouURL
        {
            get
            {
                return _ThankYouURL;
            }
            set
            {
                _ThankYouURL = value;
            }
        }

        public string PasswordQuestionURL
        {
            get
            {
                return _PasswordQuestionURL;
            }
            set
            {
                _PasswordQuestionURL = value;
            }
        }

        public string UserEmail
        {
            get
            {
                return _UserEmail;
            }
            set
            {
                _UserEmail = value;
            }
        }

        public string PasswordQuestion
        {
            get { return _PasswordQuestion; }
            set { _PasswordQuestion = value; }
        }

        public string PasswordAnswer
        {
            get { return _PasswordAnswer; }
            set { _PasswordAnswer = value; }
        }


        public string DefaultGroup
        {
            get { return _DefaultGroup; }
            set { _DefaultGroup = value; }
        }
        #endregion

        #region Methods
        public static void ApproveMembership(MembershipRequest request, SPWeb web)
        {
            Hashtable xsltValues;
            MembershipCreateStatus createStatus;
            SPListItem debuggingInfoItem = null;
            MembershipSettings settings = new MembershipSettings(web);
            MembershipProvider membership = Utils.BaseMembershipProvider(web.Site);
            /* This is just for debugging */
            try
            {
                SPList memberlist = web.GetList(Utils.GetAbsoluteURL(web,MembershipList.MEMBERSHIPREVIEWLIST));
                
                if (memberlist.Fields.ContainsField("LastError"))
                {
                    foreach (SPListItem addItem in memberlist.Items)
                    {
                        if (addItem["User Name"].ToString() == request.UserName)
                        {
                            debuggingInfoItem = addItem;
                            /* bms added break to only loop through items needed */
                            break;
                        }
                    }
                }
            }
            catch
            {
            }
            /* Above is for debugging */

            try
            {
                if (string.IsNullOrEmpty(request.UserName))
                {
                    throw new Exception("User name must not be null or empty.");
                }

                /* rdcpro: Allows providers that don't have password and question */
                if (membership.RequiresQuestionAndAnswer && string.IsNullOrEmpty(request.PasswordQuestion))
                {
                    throw new Exception("You must specify a password question.");
                }

                if (membership.RequiresQuestionAndAnswer && string.IsNullOrEmpty(request.PasswordAnswer))
                {
                    throw new Exception("You must specify a password answer.");
                }

                if (string.IsNullOrEmpty(request.UserEmail))
                {
                    throw new Exception("Email address must not be null or empty.");
                }
                //create account
                /* bms Create password at a minimum of 7 characters or Min from provider if greater */
                int passwordLength = 14;
                if (passwordLength < membership.MinRequiredPasswordLength)
                {
                    passwordLength = membership.MinRequiredPasswordLength;
                }
                if (passwordLength < membership.MinRequiredNonAlphanumericCharacters)
                {
                    passwordLength = membership.MinRequiredNonAlphanumericCharacters;
                }
                if (String.IsNullOrEmpty(request.Password))
                {
                    request.Password = System.Web.Security.Membership.GeneratePassword(passwordLength, membership.MinRequiredNonAlphanumericCharacters);
                }
                MembershipUser existingUser = Utils.BaseMembershipProvider(web.Site).GetUser(request.UserName,false);
                if (existingUser != null)
                {
                    membership.DeleteUser(request.UserName, true);
                }
                MembershipUser newUser;
                //This section is to transaction Creating the user and sending the email
                try
                {
                    // rdcpro: Changes to support providers that don't require question and answer.
                    if (membership.RequiresQuestionAndAnswer)
                    {
                        //membership.CreateUser(request.UserName, tempPassword, request.UserEmail, request.PasswordQuestion, request.PasswordAnswer, true, out createStatus);
                        newUser = membership.CreateUser(request.UserName, request.Password, request.UserEmail, request.PasswordQuestion, request.PasswordAnswer, true, null, out createStatus);                    
                    }
                    else
                    {
                        //  With this method the MembershipCreateUserException will take care of things if the user can't be created, so no worry that createStatus is set to success
                        //membership.CreateUser(.CreateUser(request.UserName, tempPassword, request.UserEmail);
                        newUser = membership.CreateUser(request.UserName, request.Password, request.UserEmail, null, null, true, null, out createStatus);
                        createStatus = MembershipCreateStatus.Success;
                    }

                    if (debuggingInfoItem != null)
                    {
                        if (debuggingInfoItem.Fields.ContainsField("LastError"))
                        {
                            
                            debuggingInfoItem["LastError"] = "Created User";
                            debuggingInfoItem.SystemUpdate();
                        }
                    }

                    if (createStatus == MembershipCreateStatus.Success)
                    {
                        newUser.IsApproved = true;
                        membership.UpdateUser(newUser);

                        //Add the user to the default group
                        if (!String.IsNullOrEmpty(request.DefaultGroup))
                        {
                            web.SiteGroups[request.DefaultGroup].AddUser(Utils.EncodeUsername(request.UserName.ToLower(), web.Site), request.UserEmail, request.FirstName + " " + request.LastName, "Self Registration");
                            
                            //Login the user if selected
                            if (request.LoginCreatedUser)
                            {
                                Microsoft.SharePoint.IdentityModel.SPClaimsUtility.AuthenticateFormsUser(new Uri(web.Url), request.UserName, request.Password);
                            }                            
                        }
                        if (debuggingInfoItem != null)
                        {
                            if (debuggingInfoItem.Fields.ContainsField("LastError"))
                            {
                                if (!String.IsNullOrEmpty(request.DefaultGroup))
                                {
                                    debuggingInfoItem["LastError"] = "Add User Has No Groups";
                                }
                                else
                                {
                                    debuggingInfoItem["LastError"] = "Add User To Groups";
                                }
                                debuggingInfoItem.SystemUpdate();
                            }
                        }

                        //email user to confirm that request is approved
                        xsltValues = new Hashtable(1);
                        xsltValues.Add("fba:MembershipRequest", request);
                        bool bSentMail = Email.SendEmail(web, request.UserEmail, settings.MembershipApprovedEmail, xsltValues);

                        if (!bSentMail)
                        {
                            Utils.LogError("SendEmail failed");
                            throw new Exception("Error sending mail notification");
                        }
                        if (debuggingInfoItem != null)
                        {
                            if (debuggingInfoItem.Fields.ContainsField("LastError"))
                            {
                                debuggingInfoItem["LastError"] = "Sent Email To New User: " + bSentMail;
                                debuggingInfoItem.SystemUpdate();
                            }
                        }
                    }
                    else
                    {
                        throw new Exception("Error creating user: " + createStatus);
                    }
                }
                catch(Exception AdduserExp)
                {
                    MembershipUser addUser = Utils.BaseMembershipProvider(web.Site).GetUser(request.UserName, false);
                    if (addUser != null)
                    {
                        membership.DeleteUser(request.UserName, true);
                    }

                    //Add error information to list
                    try
                    {
                        SPList memberlist = web.GetList(Utils.GetAbsoluteURL(web, MembershipList.MEMBERSHIPREVIEWLIST));
                        if (memberlist.Fields.ContainsField("LastError"))
                        {
                            foreach (SPListItem addItem in memberlist.Items)
                            {
                                if (addItem["User Name"].ToString() == request.UserName)
                                {
                                    addItem["LastError"] = AdduserExp.Message.ToString();
                                    addItem.SystemUpdate();
                                    break;
                                }
                            }
                        }
                    }
                    catch
                    {
                    }

                    // TODO: if CreateUser fails, the user in the MemberShipRequest list needs to be marked somehow so that the approver knows what the problem is.  
                    // Maybe the list should always have the "LastError" field, or else the status can have an extra error value in addition to pending | approved | rejected
                    // Then in the calling code, we must not delete the item from the list!
                    // Also, if we're handling an exception, we should set the status back to "Pending".
                    // For now, we rethrow the exception which will cause the caller to fail, and prevent the delete.
                    throw new Exception(AdduserExp.Message);
                }
            }
            catch (Exception ex)
            {
                //Add error information to list
                try
                {
                    SPList memberlist = web.GetList(Utils.GetAbsoluteURL(web, MembershipList.MEMBERSHIPREVIEWLIST));
                    if (memberlist.Fields.ContainsField("LastError"))
                    {
                        foreach (SPListItem addItem in memberlist.Items)
                        {
                            if (addItem["User Name"].ToString() == request.UserName)
                            {
                                // This overwrites anything already in the LastError field.
                                addItem["LastError"] = ex.Message.ToString();
                                addItem.SystemUpdate();
                            }
                        }
                    }
                }
                catch
                {
                }

                Utils.LogError(ex);
                throw new Exception(ex.Message);
            }
        }

        public static bool RejectMembership(MembershipRequest request, SPWeb web)
        {
            Hashtable xsltValues;
            MembershipSettings settings = new MembershipSettings(web);

            try
            {
                xsltValues = new Hashtable();
                xsltValues.Add("fba:MembershipRequest", request);
                return Email.SendEmail(web, request.UserEmail, settings.MembershipRejectedEmail, xsltValues);
            }
            catch (Exception ex)
            {
                Utils.LogError(ex);
                return false;
            }
        }

        public static bool SendPendingMembershipEmail(MembershipRequest request, SPWeb web)
        {
            Hashtable xsltValues;
            MembershipSettings settings = new MembershipSettings(web);

            try
            {
                xsltValues = new Hashtable();
                xsltValues.Add("fba:MembershipRequest", request);
                return Email.SendEmail(web, request.UserEmail, settings.MembershipPendingEmail, xsltValues);
            }
            catch (Exception ex)
            {
                Utils.LogError(ex);
                return false;
            }
        }

        public static bool SendPasswordRecoveryEmail(MembershipRequest request, SPWeb web)
        {
            Hashtable xsltValues;
            MembershipSettings settings = new MembershipSettings(web);

            try
            {
                xsltValues = new Hashtable();
                xsltValues.Add("fba:MembershipRequest", request);
                return Email.SendEmail(web, request.UserEmail, settings.PasswordRecoveryEmail, xsltValues);
            }
            catch (Exception ex)
            {
                Utils.LogError(ex);
                return false;
            }
        }

        public static bool SendResetPasswordEmail(MembershipRequest request, SPWeb web)
        {
            Hashtable xsltValues;
            MembershipSettings settings = new MembershipSettings(web);

            try
            {
                xsltValues = new Hashtable();
                xsltValues.Add("fba:MembershipRequest", request);
                return Email.SendEmail(web, request.UserEmail, settings.ResetPasswordEmail, xsltValues);
            }
            catch (Exception ex)
            {
                Utils.LogError(ex);
                return false;
            }
        }

        public static MembershipRequest GetMembershipRequest(MembershipUser user, SPWeb web)
        {
            MembershipRequest request = new MembershipRequest();
            request.UserEmail = user.Email;
            request.UserName = user.UserName;
            request.SiteName = web.Title;
            request.SiteURL = web.Url;

            /* These are the possible set of URLs that are provided to the user and developer in the XSLT */
            MembershipSettings settings = new MembershipSettings(web);
            request.ChangePasswordURL = Utils.GetAbsoluteURL(web, settings.ChangePasswordPage);
            request.PasswordQuestionURL = Utils.GetAbsoluteURL(web, settings.PasswordQuestionPage);
            request.ThankYouURL = Utils.GetAbsoluteURL(web, settings.ThankYouPage);

            return request;
        }


        public static bool CopyToReviewList(MembershipRequest request)
        {
            SPList reviewList;
            SPListItem reviewItem = null;
            bool result = false;

            try
            {
                SPSecurity.RunWithElevatedPrivileges(delegate()
                {
                    using (SPSite site = new SPSite(SPContext.Current.Site.ID, SPContext.Current.Site.Zone))
                    {
                        SPWeb web = site.RootWeb;
                        if (web != null)
                        {

                            site.AllowUnsafeUpdates = true;
                            web.AllowUnsafeUpdates = true;
                            reviewList = web.GetList(Utils.GetAbsoluteURL(web, MembershipList.MEMBERSHIPREVIEWLIST));
                            if (reviewList != null)
                            {
                                using (SPWeb currentWeb = site.OpenWeb(SPContext.Current.Web.ID))
                                {
                                    if (!MembershipRequest.SendPendingMembershipEmail(request, currentWeb))
                                    {
                                        return;
                                    }
                                }

                                reviewItem = reviewList.Items.Add();

                                reviewItem[MembershipReviewListFields.DATESUBMITTED] = DateTime.Now;
                                reviewItem[MembershipReviewListFields.EMAIL] = request.UserEmail;
                                reviewItem[MembershipReviewListFields.REQUESTID] = Guid.NewGuid();
                                reviewItem[MembershipReviewListFields.FIRSTNAME] = request.FirstName;
                                reviewItem[MembershipReviewListFields.LASTNAME] = request.LastName;
                                reviewItem[MembershipReviewListFields.STATUS] = MembershipStatus.Pending.ToString();
                                reviewItem[MembershipReviewListFields.USERNAME] = request.UserName;
                                reviewItem[MembershipReviewListFields.RECOVERPASSWORDQUESTION] = request.PasswordQuestion; ;
                                reviewItem[MembershipReviewListFields.RECOVERPASSWORDANSWER] = request.PasswordAnswer;
                                reviewItem[MembershipReviewListFields.DEFAULTGROUP] = request.DefaultGroup;
                                reviewItem.Update();
                                reviewList.Update();
                                /* bms Removed called to SendPendingMembershipEmail due to call on ItemAdded */
                                result = true;
                            }
                            else
                            {
                                Utils.LogError("Unable to find Membership Review List");
                            }
                        }
                    }
                });
            }
            catch (Exception ex)
            {
                Utils.LogError(ex);
            }

            return result;
        }

        #endregion
    }
}