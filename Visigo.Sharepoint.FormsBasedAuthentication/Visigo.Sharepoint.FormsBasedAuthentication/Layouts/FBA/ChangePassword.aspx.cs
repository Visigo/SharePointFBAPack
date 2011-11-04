using Microsoft.SharePoint.WebControls;
using System.Web.Security;
using System.Web.UI.WebControls;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;
using System.Globalization;

namespace Visigo.Sharepoint.FormsBasedAuthentication
{
    /// <summary>
    /// Code behind for ChangePassword.aspx
    /// </summary>
    public partial class ChangePassword : LayoutsPageBase
    {
        //protected override void OnPreInit(System.EventArgs e)
        //{
        //    base.OnPreInit(e);

        //    // use the master page of the current site
        //    this.MasterPageFile = this.Web.MasterUrl;            
        //}

        protected override void OnLoad(System.EventArgs e)
        {
            base.OnLoad(e);

            TitleArea.Text = SPUtility.GetLocalizedString(
                "$Resources:ChangePassword_Title",
                "FBAPackChangePasswordWebPart",
                (uint)CultureInfo.CurrentUICulture.LCID);

        }
    }
}