using System;
using Microsoft.SharePoint.WebControls;
using System.Web.Security;
using System.Web.UI.WebControls;
using Microsoft.SharePoint.Utilities;
using System.Web;

namespace Visigo.Sharepoint.FormsBasedAuthentication
{
    /// <summary>
    /// Code behind for RoleDelete.aspx
    /// </summary>
    public partial class RoleDelete : LayoutsPageBase
    {

        protected override bool RequireSiteAdministrator
        {
            get { return true; }
        }

        protected override void OnLoad(EventArgs e)
        {
            this.CheckRights();

            // display role being deleted and number of users in that role
            txtRole.Text = Request.QueryString["ROLE"];
            lblMessage.Text = string.Format(localizedMsg.Text, Utils.BaseRoleProvider().GetUsersInRole(txtRole.Text).Length);
        }

        protected void OnDelete(object sender, EventArgs e)
        {
            // get role and users
            string[] roleName = new string[1];
            roleName[0] = Request.QueryString["ROLE"];
            string[] usersInRole = Utils.BaseRoleProvider().GetUsersInRole(roleName[0]);

            if (Utils.BaseRoleProvider().RoleExists(roleName[0]))
            {
                try
                {
                    // remove all users from role if needed
                    if (usersInRole.Length > 0)
                    {
                        Utils.BaseRoleProvider().RemoveUsersFromRoles(usersInRole, roleName);
                    }
                    // delete role
                    Utils.BaseRoleProvider().DeleteRole(roleName[0],false);
                    SPUtility.Redirect("FBA/Management/RolesDisp.aspx", SPRedirectFlags.RelativeToLayoutsPage | SPRedirectFlags.UseSource, this.Context);
                }
                catch (Exception ex)
                {
                    Utils.LogError(ex, true);
                }
            }
            else
            {
                lblMessage.Text = notExistMsg.Text;
            }
        }
    }
}
