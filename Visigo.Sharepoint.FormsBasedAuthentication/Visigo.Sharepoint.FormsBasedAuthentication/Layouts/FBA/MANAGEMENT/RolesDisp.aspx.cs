using Microsoft.SharePoint.WebControls;
using System.Web.UI.WebControls;

namespace Visigo.Sharepoint.FormsBasedAuthentication
{
    /// <summary>
    /// Code behind for RolesDisp.aspx
    /// </summary>
    public partial class RolesDisp : LayoutsPageBase
    {

        protected override void OnInit(System.EventArgs e)
        {
            // display error if membership provider not configured
            if (!Utils.IsProviderConfigured())
            {
                lblMessage.Text = "A Membership Provider has not been configured correctly. Check the web.config setttings for this web application.";
                RoleGrid.Visible = false;
                ToolBarPlaceHolder.Visible = false;
            }
            base.OnInit(e);
        }

        protected override bool RequireSiteAdministrator
        {
            get { return true; }
        }
    }
}
