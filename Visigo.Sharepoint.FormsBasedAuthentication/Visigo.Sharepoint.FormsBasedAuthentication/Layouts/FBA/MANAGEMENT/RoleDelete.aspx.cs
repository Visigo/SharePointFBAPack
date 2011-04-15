using System;
using Microsoft.SharePoint.WebControls;
using System.Web.Security;
using System.Web.UI.WebControls;
using Microsoft.SharePoint.Utilities;

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
            lblMessage.Text = "There are " + Roles.GetUsersInRole(txtRole.Text).Length + " users in this role.";
        }

        protected void OnDelete(object sender, EventArgs e)
        {
            // get role and users
            string roleName = Request.QueryString["ROLE"];
            string[] usersInRole = Roles.GetUsersInRole(roleName);

            if (Utils.RoleExists(roleName))
            {
                try
                {
                    // remove all users from role if needed
                    if (Roles.GetUsersInRole(roleName).Length > 0)
                    {
                        Roles.RemoveUsersFromRole(usersInRole, roleName);
                    }
                    // delete role
                    Roles.DeleteRole(roleName);
                    Response.Redirect("RolesDisp.aspx");
                }
                catch (Exception ex)
                {
                    Utils.LogError(ex, true);
                }
            }
            else
            {
                lblMessage.Text = "Role doesn't exist";
            }
        }
    }
}
