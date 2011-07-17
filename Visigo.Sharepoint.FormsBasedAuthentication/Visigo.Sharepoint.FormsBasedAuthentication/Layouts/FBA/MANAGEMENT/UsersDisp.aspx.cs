using Microsoft.SharePoint.WebControls;
using System.Web.Security;
using System.Web.UI.WebControls;

namespace Visigo.Sharepoint.FormsBasedAuthentication
{
    /// <summary>
    /// Code behind for UsersDisp.aspx
    /// </summary>
    public partial class UsersDisp : LayoutsPageBase
    {

        protected override void OnInit(System.EventArgs e)
        {
            // display error if membership provider not configured
            if (!Utils.IsProviderConfigured())
            {
                lblMessage.Text = "A Membership Provider has not been configured correctly. Check the web.config setttings for this web application.";
                MemberGrid.Visible = false;
                ToolBarPlaceHolder.Visible = false;
            }
            base.OnInit(e);
        }

        protected void Search_Click(object sender, System.EventArgs e)
        {
            UserDataSource.SearchText = SearchText.Text;
            MemberGrid.DataBind();
        }
        
        protected override bool RequireSiteAdministrator
        {
            get { return true; }
        }
    }
}