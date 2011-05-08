using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;
using System.Web;
using Microsoft.SharePoint.Administration;
using System.Diagnostics;
using System.Reflection;
using Microsoft.SharePoint;
using System.Web.Configuration;
using System.Configuration;
using System.Web.Security;
using Microsoft.SharePoint.Utilities;
using Microsoft.SharePoint.Administration.Claims;

namespace Visigo.Sharepoint.FormsBasedAuthentication
{
    class Utils
    {
        //TODO: Inherit from Sharepoint membership provider and fix all unimplemented functions/properties.  Then go through code and get rid 
        //of all references to Utils that are used to work around this.

        public static MembershipProvider BaseMembershipProvider()
        {
            return Membership.Providers[GetMembershipProvider()];
        }

        public static RoleProvider BaseRoleProvider()
        {
            return Roles.Providers[GetRoleProvider()];
        }

        public static MembershipProvider BaseMembershipProvider(SPSite site)
        {
            return Membership.Providers[GetMembershipProvider(site)];
        }

        public static RoleProvider BaseRoleProvider(SPSite site)
        {
            return Roles.Providers[GetRoleProvider(site)];
        }

        public static string DecodeUsername(string username)
        {
            if (SPClaimProviderManager.IsEncodedClaim(username))
            {
                return SPClaimProviderManager.Local.DecodeClaim(username).Value;
            }
            else
            {
                return username;
            }
        }

        public static string EncodeUsername(string username)
        {
            SPClaim claim = new SPClaim(SPClaimTypes.UserLogonName, username, "http://www.w3.org/2001/XMLSchema#string", SPOriginalIssuers.Format(SPOriginalIssuerType.Forms, GetMembershipProvider()));
            return SPClaimProviderManager.Local.EncodeClaim(claim);
        }

        public static string GetCurrentUsername()
        {
            return DecodeUsername(SPContext.Current.Web.CurrentUser.LoginName);
        }

        public static string EncodeUsername(string username, SPSite site)
        {
            SPClaim claim = new SPClaim(SPClaimTypes.UserLogonName, username, "http://www.w3.org/2001/XMLSchema#string", SPOriginalIssuers.Format(SPOriginalIssuerType.Forms, GetMembershipProvider(site)));
            return SPClaimProviderManager.Local.EncodeClaim(claim);
        }

        public static Table CreateErrorMessage(string ErrorMessage)
        {
            Table tbl = new Table();
            TableRow tr = new TableRow();
            TableCell td = new TableCell();

            td.Text = ErrorMessage;

            tr.Cells.Add(td);
            tbl.Rows.Add(tr);
            tbl.CssClass = "ms-WPBody";

            return tbl;
        }

        public static string GetMembershipProvider()
        {
            return GetMembershipProvider(SPContext.Current.Site);
        }

        public static string GetRoleProvider()
        {
            return GetRoleProvider(SPContext.Current.Site);
        }

        public static string GetRoleProvider(SPSite site)
        {
            // get role provider of whichever zone in the web app is fba enabled 
            SPIisSettings settings = GetFBAIisSettings(site);
            return settings.FormsClaimsAuthenticationProvider.RoleProvider;
        }


        public static string GetMembershipProvider(HttpContext context)
        {
            using (SPSite site = new SPSite(context.Request.Url.AbsoluteUri))
            {
                SPIisSettings settings = GetFBAIisSettings(site);
                return settings.FormsClaimsAuthenticationProvider.MembershipProvider;
            }
        }

        public static string GetMembershipProvider(SPSite site)
        {
            // get membership provider of whichever zone in the web app is fba enabled 
            SPIisSettings settings = GetFBAIisSettings(site);
            return settings.FormsClaimsAuthenticationProvider.MembershipProvider;
        }

        public static SPIisSettings GetFBAIisSettings(SPSite site)
        {
            SPIisSettings settings = null;

            // try and get FBA IIS settings from current site zone
            try
            {
                settings = site.WebApplication.IisSettings[site.Zone];
                if (settings.AuthenticationMode == AuthenticationMode.Forms)
                    return settings;
            }
            catch 
            { 
                // expecting errors here so do nothing                 
            }

            // check each zone type for an FBA enabled IIS site
            foreach (SPUrlZone zone in Enum.GetValues(typeof(SPUrlZone)))
            {
                try
                {
                    settings = site.WebApplication.IisSettings[(SPUrlZone)zone];
                    if (settings.AuthenticationMode == AuthenticationMode.Forms)
                        return settings;
                }
                catch
                { 
                    // expecting errors here so do nothing                 
                }
            }
        
            // return null if FBA not enabled
            return null;
        }

        /// <summary>
        /// check current site to see if a provider has been specified in the web.config
        /// </summary>
        /// <returns></returns>
        public static bool IsProviderConfigured()
        {                     
            // attempt to get current users details
            int numUsers;
            try
            {
                Membership.GetAllUsers(0,1,out numUsers);
            }
            catch 
            {
                // if fails membership provider is not configured correctly
                return false;
            }
            
            // if no error provider is ok
            return true;
        }    
    
        public static bool IsWebAppFBAEnabled(SPSite site)
        {
            SPIisSettings settings = site.WebApplication.IisSettings[site.Zone];
            return (settings.AuthenticationMode == AuthenticationMode.Forms);
        }

        public static void LogError(string errorMessage, string errorCategory)
        {
            // log error to ULS log
            TraceProvider.WriteTrace(0, TraceProvider.TraceSeverity.CriticalEvent, Guid.NewGuid(), Assembly.GetExecutingAssembly().FullName, "InternetExtranetEdition", errorCategory, errorMessage);
        }

        public static void LogError(Exception ex)
        {
            // create error message
            string errorMessage = ex.Message + " " + ex.StackTrace;
           
            // add any inner exceptions
            Exception innerException = ex.InnerException;
            while (innerException != null)
            {
                errorMessage += "Inner Error: " + innerException.Message + " " + innerException.StackTrace;
                innerException = innerException.InnerException;
            }

            // log error
            LogError(errorMessage, ex.GetType().ToString());
        }

        public static void LogError(Exception ex, bool transferToErrorPage)
        {
            LogError(ex);
            SPUtility.TransferToErrorPage(ex.Message);
        }

        public static MembershipUser GetUser(string username, bool userIsOnline, SPSite site)
        {
            return BaseMembershipProvider(site).GetUser(username, userIsOnline);
        }

        public static MembershipUser GetUser(string username, SPSite site)
        {
            return GetUser(username,false, site);
        }

        public static MembershipUser GetUser(string username)
        {
            return GetUser(username, false, SPContext.Current.Site);
        }

        public static string GetWebProperty(string key, string defaultValue, SPWeb web)
        {
            string value = null;
            value = web.Properties[key];
            if (value == null) value = defaultValue;
            return value;
        }

        public static string GetWebProperty(string key, string defaultValue)
        {
            return GetWebProperty(key, defaultValue, SPContext.Current.Web);
        }

        public static bool GetSiteProperty(string key, bool defaultValue)
        {
            return Boolean.Parse(GetWebProperty(key, defaultValue.ToString(), SPContext.Current.Site.RootWeb));
        }

        public static bool GetSiteProperty(string key, bool defaultValue, SPSite site)
        {
            return Boolean.Parse(GetWebProperty(key, defaultValue.ToString(), site.RootWeb));
        }

        public static void SetWebProperty(string key, string value, SPWeb web)
        {
            web.Properties[key] = value;
               
            web.Properties.Update();
        }

        public static void SetWebProperty(string key, string value)
        {
            SetWebProperty(key, value, SPContext.Current.Web);
        }

        public static void SetSiteProperty(string key, bool value, SPSite site)
        {
            SetWebProperty(key, value.ToString(), site.RootWeb);
        }

        public static void SetSiteProperty(string key, bool value)
        {
            SetWebProperty(key, value.ToString(), SPContext.Current.Site.RootWeb);
        }

        public static bool RoleExists(string roleName)
        {
            return BaseRoleProvider(SPContext.Current.Site).RoleExists(roleName);
        }

        public static bool RoleExists(string roleName, SPSite site)
        {
            return BaseRoleProvider(site).RoleExists(roleName);
        }
    }
}
