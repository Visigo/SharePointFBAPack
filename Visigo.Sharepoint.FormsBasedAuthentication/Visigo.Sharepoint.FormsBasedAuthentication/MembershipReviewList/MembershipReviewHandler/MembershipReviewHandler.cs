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


        public override void ItemAdded(SPItemEventProperties properties)
        {
            SPListItem item = null;
            try
            {
                item = properties.ListItem;

                /* bms Send email that new item has been added */
                MembershipRequest request = GetMembershipRequest(item.Web, item);
                MembershipRequest.SendPendingMembershipEmail(request, item.Web);
            }
            catch (Exception ex)
            {
                Utils.LogError(ex);
            }
        }

        public override void ItemUpdated(SPItemEventProperties properties)
        {
            this.EventFiringEnabled = false;
            SPListItem item = null;
            SPWeb web = null;
            SPList list = null;
            MembershipStatus status;

            try
            {
                item = properties.ListItem;
                if (item != null)
                {
                    web = item.Web;
                    list = item.ParentList;
                    status = (MembershipStatus)Utils.GetChoiceIndex(list.Fields.GetFieldByInternalName(MembershipReviewListFields.STATUS) as SPFieldChoice, item[MembershipReviewListFields.STATUS].ToString());
                    switch (status)
                    {
                        case MembershipStatus.Approved:
                            // TODO: rdcpro: if CreateUser in the ApproveMembership call fails, the user in the MemberShipRequest list needs to be marked somehow so that the approver knows what the problem is.  
                            // Maybe the list should have the "LastError" field which will get the error info, or else the status can have an extra error value in addition to pending | approved | rejected
                            // Then in the calling code, we must not delete the item from the list!
                            // It would have been better if ApproveMembership returned a status code, rather than use exception handling, but here we are.
                            try
                            {
                                MembershipRequest.ApproveMembership(GetMembershipRequest(web, item), web);
                                item.Delete();
                                list.Update();
                            }
                            catch
                            {
                                // this has already been handled and logged.  We just need to prevent the item from being deleted.
                            }
                            break;
                        case MembershipStatus.Pending:
                            break;
                        case MembershipStatus.Rejected:
                            MembershipRequest.RejectMembership(GetMembershipRequest(web, item), web);
                            //bms Removed Delete from Reject Membership to allow administrators to approve user later and delete with UI
                            //item.Delete();
                            //list.Update();
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Utils.LogError(ex);
                //HttpContext.Current.Response.Redirect(SPContext.Current.Web.Url + "/_layouts/error.aspx");
            }
            finally
            {
                if (web != null)
                {
                    //web.Dispose();
                }
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
