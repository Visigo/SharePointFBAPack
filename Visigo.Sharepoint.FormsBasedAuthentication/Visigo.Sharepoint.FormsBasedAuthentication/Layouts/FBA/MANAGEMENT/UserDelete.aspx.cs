using System;
using System.Web.Security;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using Microsoft.SharePoint.Administration;
using Microsoft.SharePoint.WebControls;
using System.Web.UI.WebControls;
using System.Web;

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
                deleteMsg.Text = "Confirm that you want to delete the user '" + Request.QueryString["USERNAME"] + "'";
            }
            else
            {
                SPUtility.TransferToErrorPage("User Not Found");
            }
        }

        protected void OnDelete(object sender, EventArgs e)
        {
            string userName = Request.QueryString["USERNAME"];

            try
            {
                // delete user from FBA data store
                Membership.DeleteUser(userName);

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
                        
            SPUtility.Redirect("FBA/Management/UsersDisp.aspx",SPRedirectFlags.RelativeToLayoutsPage, HttpContext.Current);
        }

    }
}
