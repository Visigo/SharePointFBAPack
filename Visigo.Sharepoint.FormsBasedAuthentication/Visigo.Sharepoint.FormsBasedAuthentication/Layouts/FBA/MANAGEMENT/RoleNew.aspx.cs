using System;
using System.Web.Security;
using Microsoft.SharePoint.WebControls;
using System.Web.UI.WebControls;
using Microsoft.SharePoint.Utilities;

namespace Visigo.Sharepoint.FormsBasedAuthentication
{
    /// <summary>
    /// Code behind for RolesNew.aspx
    /// </summary>
    public partial class RoleNew : LayoutsPageBase
    {

        protected void OnSubmit(object sender, EventArgs e)
        {
            // add the role to the membership provider
            if (!Utils.RoleExists(txtRole.Text))
            {
                try
                {
                    Roles.CreateRole(txtRole.Text);
                    // redirect to roles list
                    Response.Redirect("RolesDisp.aspx");
                }
                catch (Exception ex)
                {
                    Utils.LogError(ex, true);
                }
            }
            else
            {
                lblMessage.Text = "Role Already Exists";

            }
        }
    }
}
