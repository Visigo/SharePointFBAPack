using System;
using System.Web.Security;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using Microsoft.SharePoint.Administration;
using Microsoft.SharePoint.WebControls;
using System.Web.UI.WebControls;
using System.Web;
using System.Globalization;

namespace Visigo.Sharepoint.FormsBasedAuthentication
{
    /// <summary>
    /// Code behind for UserDelete.aspx
    /// </summary>
    public partial class UserDelete : LayoutsPageBase
    {

        protected override bool RequireSiteAdministrator
        {
            get { return true; }
        }

        protected override void OnLoad(EventArgs e)
        {
            this.CheckRights();
        
            // display error confirmation message
            string userName = Request.QueryString["USERNAME"];
            if (!string.IsNullOrEmpty(userName))
            {
                deleteMsg.Text = string.Format(localizedMsg.Text, userName);
            }
            else
            {
                SPUtility.TransferToErrorPage(LocalizedString.GetGlobalString("FBAPackWebPages", "UserNotFound"));
            }
        }

        protected void OnDelete(object sender, EventArgs e)
        {
            string userName = Request.QueryString["USERNAME"];

            try
            {
                // delete user from FBA data store
                Utils.BaseMembershipProvider().DeleteUser(userName,true);

                // delete user from SharePoint            
                try
                {
                    this.Web.SiteUsers.Remove(Utils.EncodeUsername(userName));
                    this.Web.Update();
                }
                catch
                {
                    //left Empty because the user might not be in the SharePoint site yet.
                }
            }
            catch (Exception ex)
            {
                Utils.LogError(ex, true);                
            }
            
            //Redirect to UsersDisp or Source, as long as source is not UserEdit.aspx - as that will no longer work as the user is deleted
            string url = "FBA/Management/UsersDisp.aspx";

            SPUtility.DetermineRedirectUrl(url, SPRedirectFlags.RelativeToLayoutsPage | SPRedirectFlags.UseSource, this.Context, null, out url);

            if (url.ToLower().Contains("useredit.aspx"))
            {
                url = "FBA/Management/UsersDisp.aspx";

                SPUtility.DetermineRedirectUrl(url, SPRedirectFlags.RelativeToLayoutsPage, this.Context, null, out url);
            }

            SPUtility.Redirect(url, SPRedirectFlags.Default, this.Context);
        }

    }
}
