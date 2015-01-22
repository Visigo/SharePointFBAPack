using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SharePoint.WebControls;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration.Claims;
using System.Web;
using Microsoft.IdentityModel.Claims;
using Microsoft.SharePoint.Utilities;

namespace Visigo.Sharepoint.FormsBasedAuthentication
{
    class ChangePasswordMenuItem : System.Web.UI.WebControls.WebControl
    {
        protected override void CreateChildControls()
        {
            
            // return when user is not a forms based user
            IClaimsIdentity claimsIdentity = (HttpContext.Current.User != null) ? (HttpContext.Current.User.Identity as IClaimsIdentity) : null;
            if (claimsIdentity != null)
            {
                SPClaimProviderManager mgr = SPClaimProviderManager.Local;
                SPClaim sPClaim = mgr.DecodeClaimFromFormsSuffix(claimsIdentity.Name);
                if (SPOriginalIssuers.GetIssuerType(sPClaim.OriginalIssuer) == SPOriginalIssuerType.Windows)
                {
                    return;
                }
            }

            
            string changePasswordPage = "";

            SPSecurity.RunWithElevatedPrivileges(delegate()
            {
                using (SPSite site = new SPSite(SPContext.Current.Site.ID, SPContext.Current.Site.Zone))
                {
                    MembershipSettings setting = new MembershipSettings(site.RootWeb);

                    if (setting == null || string.IsNullOrEmpty(setting.ChangePasswordPage))
                        return;

                    changePasswordPage = setting.ChangePasswordPage;
                }
            });

            

            // generate return url
            string source = SPUtility.OriginalServerRelativeRequestUrl;
            string target = Utils.GetAbsoluteURL(SPContext.Current.Web, changePasswordPage);

            MenuItemTemplate  changePasswordItem = new MenuItemTemplate();
            changePasswordItem.Text = LocalizedString.GetString("FBAPackMenus", "FBAChangePassword_Title");
            changePasswordItem.Description = LocalizedString.GetString("FBAPackMenus", "FBAChangePassword_Desc");
            changePasswordItem.Sequence = 1;
            changePasswordItem.ClientOnClickNavigateUrl = target + "?Source=" + SPHttpUtility.UrlKeyValueEncode(source);

            this.Controls.Add(changePasswordItem);
        }
    }
}
