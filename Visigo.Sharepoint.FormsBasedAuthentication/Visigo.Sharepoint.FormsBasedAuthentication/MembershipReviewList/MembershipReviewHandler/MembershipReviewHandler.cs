using System;
using System.Security.Permissions;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Security;
using Microsoft.SharePoint.Utilities;
using Microsoft.SharePoint.Workflow;

namespace Visigo.Sharepoint.FormsBasedAuthentication
{
    /// <summary>
    /// List Item Events
    /// </summary>
    public class MembershipReviewHandler : SPItemEventReceiver
    {

        public override void ItemUpdated(SPItemEventProperties properties)
        {
            this.EventFiringEnabled = false;
            SPListItem item = null;
            SPList list = null;
            MembershipStatus status;

            try
            {
                item = properties.ListItem;
                if (item != null)
                {
                    SPSecurity.RunWithElevatedPrivileges(delegate()
                    {
                        using (SPSite site = new SPSite(item.Web.Site.ID, item.Web.Site.Zone))
                        {
                            using (SPWeb web = site.OpenWeb(item.Web.ID))
                            {
                                if (web != null)
                                {

                                    site.AllowUnsafeUpdates = true;
                                    web.AllowUnsafeUpdates = true;

                                    list = item.ParentList;
                                    status = (MembershipStatus)Utils.GetChoiceIndex(list.Fields.GetFieldByInternalName(MembershipReviewListFields.STATUS) as SPFieldChoice, item[MembershipReviewListFields.STATUS].ToString());
                                    switch (status)
                                    {
                                        case MembershipStatus.Approved:
                                            // TODO: rdcpro: if CreateUser in the ApproveMembership call fails, the user in the MemberShipRequest list needs to be marked somehow so that the approver knows what the problem is.  
                                            // Maybe the list should have the "LastError" field which will get the error info, or else the status can have an extra error value in addition to pending | approved | rejected
                                            // Then in the calling code, we must not delete the item from the list!
                                            // It would have been better if ApproveMembership returned a status code, rather than use exception handling, but here we are.
                                            MembershipRequest.ApproveMembership(GetMembershipRequest(web, item), web);
                                            item.Delete();
                                            list.Update();

                                            break;
                                        case MembershipStatus.Pending:
                                            break;
                                        case MembershipStatus.Rejected:
                                            if (!MembershipRequest.RejectMembership(GetMembershipRequest(web, item), web))
                                            {
                                                throw new Exception("Error rejecting membership");
                                            }
                                            //bms Removed Delete from Reject Membership to allow administrators to approve user later and delete with UI
                                            //item.Delete();
                                            //list.Update();
                                            break;
                                    }
                                }
                            }
                        }
                    });
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating item in Membership Request List", ex);
            }
            finally
            {
                this.EventFiringEnabled = true;
            }
        }

        private static MembershipRequest GetMembershipRequest(SPWeb web, SPListItem item)
        {
            return GetMembershipRequest(web, item, string.Empty);
        }

        private static MembershipRequest GetMembershipRequest(SPWeb web, SPListItem item, string password)
        {
            MembershipSettings settings = new MembershipSettings(web);
            MembershipRequest request = new MembershipRequest();

            /* These are the core fields that are part of the membership provider request */
            request.FirstName = item[MembershipReviewListFields.FIRSTNAME].ToString();
            request.LastName = item[MembershipReviewListFields.LASTNAME].ToString();
            request.Password = password;
            /* bms Updated the web property of Title for the site name instead of Name */
            request.SiteName = web.Title;
            request.SiteURL = web.Url;
            request.UserEmail = item[MembershipReviewListFields.EMAIL].ToString();
            request.UserName = item[MembershipReviewListFields.USERNAME].ToString();

            /* These fields may not be avaliable based on the membership provider */
            if (item[MembershipReviewListFields.RECOVERPASSWORDQUESTION] != null)
            {
                request.PasswordQuestion = item[MembershipReviewListFields.RECOVERPASSWORDQUESTION].ToString();
            }
            if (item[MembershipReviewListFields.RECOVERPASSWORDANSWER] != null)
            {
                request.PasswordAnswer = item[MembershipReviewListFields.RECOVERPASSWORDANSWER].ToString();
            }
            if (item[MembershipReviewListFields.DEFAULTGROUP] != null)
            {
                request.DefaultGroup = item[MembershipReviewListFields.DEFAULTGROUP].ToString();
            }

            /* These are the possible set of URLs that are provided to the user and developer in the XSLT */
            request.ChangePasswordURL = Utils.GetAbsoluteURL(web, settings.ChangePasswordPage);
            request.PasswordQuestionURL = Utils.GetAbsoluteURL(web, settings.PasswordQuestionPage);
            request.ThankYouURL = Utils.GetAbsoluteURL(web, settings.ThankYouPage);

            return request;
        }


    }
}
