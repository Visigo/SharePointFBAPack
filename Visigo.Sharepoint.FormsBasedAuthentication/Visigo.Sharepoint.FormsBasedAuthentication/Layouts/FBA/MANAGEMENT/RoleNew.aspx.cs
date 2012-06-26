using System;
using System.Web.Security;
using Microsoft.SharePoint.WebControls;
using System.Web.UI.WebControls;
using Microsoft.SharePoint.Utilities;
using System.Web;

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
            if (!Utils.BaseRoleProvider().RoleExists(txtRole.Text))
            {
                try
                {
                    Utils.BaseRoleProvider().CreateRole(txtRole.Text);
                    // redirect to roles list
                    SPUtility.Redirect("RolesDisp.aspx", SPRedirectFlags.UseSource, HttpContext.Current);
                }
                catch (Exception ex)
                {
                    Utils.LogError(ex, true);
                }
            }
            else
            {
                lblMessage.Visible = true;

            }
        }
    }
}
