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
using System.Collections;

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
            using (SPSite site = new SPSite(SPUtility.GetPageUrlPath(context)))
            {
                return GetMembershipProvider(site);
            }
        }

        public static string GetMembershipProvider(SPSite site)
        {
            // get membership provider of whichever zone in the web app is fba enabled 
            SPIisSettings settings = GetFBAIisSettings(site);
            if (settings == null) return null;
            return settings.FormsClaimsAuthenticationProvider.MembershipProvider;
        }

        private static SPIisSettings GetFBAIisSettings(SPSite site)
        {
            SPIisSettings settings = null;

            // try and get FBA IIS settings from current site zone
            try
            {
                settings = site.WebApplication.IisSettings[site.Zone];
                if (settings.UseFormsClaimsAuthenticationProvider)
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
                    if (settings.UseFormsClaimsAuthenticationProvider)
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
                BaseMembershipProvider().GetAllUsers(0,1,out numUsers);
            }
            catch 
            {
                // if fails membership provider is not configured correctly
                return false;
            }
            
            // if no error provider is ok
            return true;
        }

        public static void ResetUserPassword(string username, string newPassword, bool sendEmail, SPWeb web)
        {
            if (Utils.BaseMembershipProvider().RequiresQuestionAndAnswer || !Utils.BaseMembershipProvider().EnablePasswordReset)
            {
                throw new Exception(LocalizedString.GetGlobalString("FBAPackWebPages", "ResetPasswordUnavailable"));
            }

            MembershipUser user = Utils.BaseMembershipProvider().GetUser(username, false);
            string password = user.ResetPassword();

            //Change the password to the specified password
            if (!String.IsNullOrEmpty(newPassword))
            {
                if (user.ChangePassword(password, newPassword))
                {
                    password = newPassword;
                }
                else
                {
                    throw new Exception(LocalizedString.GetGlobalString("FBAPackWebPages", "ResetPasswordChangePasswordError"));
                }
            }

            if (sendEmail)
            {
                MembershipRequest request = MembershipRequest.GetMembershipRequest(user, web);
                request.Password = password;

                MembershipRequest.SendResetPasswordEmail(request, web);
            }

        }

        public static void LogError(string errorMessage, FBADiagnosticsService.FBADiagnosticsCategory errorCategory)
        {
            // log error to ULS log
            FBADiagnosticsService.Local.WriteTrace(0, errorCategory, TraceSeverity.High, errorMessage,null);
        }

        public static void LogError(Exception ex)
        {
            // log error
            LogError(ex.ToString(), FBADiagnosticsService.FBADiagnosticsCategory.General);
        }

        public static void LogError(string errorMessage)
        {
            LogError(errorMessage, FBADiagnosticsService.FBADiagnosticsCategory.General);
        }

        public static void LogError(Exception ex, bool transferToErrorPage)
        {
            LogError(ex);
            SPUtility.TransferToErrorPage(ex.Message);
        }

        public static string GetWebProperty(string key, string defaultValue, SPWeb web, bool save)
        {
            string value = null;
            value = web.Properties[key];
            if (value == null)
            {
                value = defaultValue;
                if (save)
                {
                    SetWebProperty(key, value, web);
                }
            }
            return value;
        }

        public static string GetWebProperty(string key, string defaultValue, SPWeb web)
        {
            return GetWebProperty(key, defaultValue, web, false);
        }

        public static string GetWebProperty(string key, string defaultValue)
        {
            return GetWebProperty(key, defaultValue, SPContext.Current.Web);
        }

        public static bool GetSiteProperty(string key, bool defaultValue)
        {
            return GetSiteProperty(key, defaultValue, SPContext.Current.Site);
        }

        public static bool GetSiteProperty(string key, bool defaultValue, SPSite site)
        {
            bool result = defaultValue;

            SPSecurity.RunWithElevatedPrivileges(delegate()
            {
                using (SPSite privSite = new SPSite(site.ID, site.Zone))
                {
                    SPWeb web = privSite.RootWeb;
                    result = Boolean.Parse(GetWebProperty(key, defaultValue.ToString(), web));
                }
            });
            return result;
        }

        public static void SetWebProperty(string key, string value, SPWeb web)
        {
            bool unsafeUpdates = web.AllowUnsafeUpdates;

            web.AllowUnsafeUpdates = true;
            web.Properties[key] = value;
               
            web.Properties.Update();
            web.AllowUnsafeUpdates = unsafeUpdates;
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

        public static string GetAbsoluteURL(SPWeb web, string path)
        {
            return SPUtility.ConcatUrls(web.Url, path);
        }

        public static int GetChoiceIndex(SPFieldChoice field, string value)
        {
            if (field == null || value == null)
            {
                return -1;
            }
            for (int i = 0; i < field.Choices.Count; i++)
            {
                if (field.Choices[i] == value)
                {
                    return i;
                }
            }

            return -1;
        }
    }
}
