using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Security;
using System.Reflection;
using Microsoft.SharePoint.Administration;

namespace Visigo.Sharepoint.FormsBasedAuthentication.Features.FBAManagement
{
    /// <summary>
    /// This class handles events raised during feature activation, deactivation, installation, uninstallation, and upgrade.
    /// </summary>
    /// <remarks>
    /// The GUID attached to this class may be used during packaging and should not be modified.
    /// </remarks>

    [Guid("881a9952-02e9-4418-b5e1-46632793dd31")]
    public class FBAManagementEventReceiver : SPFeatureReceiver
    {

        #region Constants

        private string _ListName = "Site Membership Review List";
        //private string _ListDesc = "Contains list of users requesting a site account.";
        
        #endregion

        /// <summary>
        /// Activate the membership review event receiver here, as the declarative definition will link
        /// the event receiver to all lists within the site, if the feature is scoped to site
        /// </summary>
        /// <param name="properties"></param>
        public void ActivateMembershipReviewList(SPFeatureReceiverProperties properties)
        {
            SPSite site = null;
            SPWeb web = null;
            SPList list = null;

            try
            {
                site = properties.Feature.Parent as SPSite;
                web = site.RootWeb;

                if (web != null)
                {

                    //guidList = web.Lists.Add(_ListName, _ListDesc, _ListName, new Guid("{69CE2076-9A2F-4c71-AEDF-F4252C01DE4E}").ToString(), (int)SPListTemplateType.GenericList, "100");
                    //web.Update();

                    list = web.Lists[_ListName];
                    list.EventReceivers.Add(SPEventReceiverType.ItemUpdated, Assembly.GetExecutingAssembly().FullName, "Visigo.Sharepoint.FormsBasedAuthentication.MembershipReviewHandler");
                    
                    //list.EventReceivers.Add(SPEventReceiverType.ItemAdded, Assembly.GetExecutingAssembly().FullName, "Visigo.Sharepoint.FormsBasedAuthentication.MembershipReviewHandler");
                    //list.ContentTypesEnabled = false;
                    //list.EnableAttachments = false;
                    //list.EnableFolderCreation = false;
                    //list.EnableVersioning = false;
                    //list.NoCrawl = true;

                    //Remove permissions from the list - only site collection admins should have permission
                    list.BreakRoleInheritance(false, true);


                    list.Update();
                }
            }
            catch (Exception ex)
            {
                Utils.LogError(ex);
            }
            finally
            {
                if (web != null)
                    web.Dispose();
                if (site != null)
                    site.Dispose();
            }
        }

        public void DeactivateMembershipReviewList(SPFeatureReceiverProperties properties)
        {
            SPSite site = null;
            SPWeb web = null;
            SPList list;

            try
            {
                site = properties.Feature.Parent as SPSite;
                web = site.RootWeb;

                if (web != null)
                {
                    list = web.Lists[_ListName];
                    if (list != null)
                    {
                        list.Delete();
                        web.Update();
                    }
                }
            }
            catch (Exception ex)
            {
                Utils.LogError(ex);
            }
            finally
            {
                if (web != null)
                    web.Dispose();
                if (site != null)
                    site.Dispose();
            }
        }

        public void ActivateFBAManagement(SPFeatureReceiverProperties properties)
        {


            // get site reference
            SPSite site = properties.Feature.Parent as SPSite;

            /* bms Get a reference to the web */
            // get a web reference
            //SPWeb web = site.OpenWeb(); 

            ///* Set the options in the web properties */
            //UpdateProperty(MembershipOptions.ENABLEROLES, Boolean.TrueString, web);
            //UpdateProperty(MembershipOptions.REVIEWMEMBERSHIPREQUESTS, Boolean.FalseString, web);

            ///* bms Set the URL strings in the web properties */
            ////TODO: Make these resources
            //UpdateProperty(MembershipReviewSiteURL.CHANGEPASSWORDPAGE,"Pages/ChangePassword.aspx",web);
            //UpdateProperty(MembershipReviewSiteURL.PASSWORDQUESTIONPAGE,"Pages/PasswordQuestion.aspx",web);
            //UpdateProperty(MembershipReviewSiteURL.THANKYOUPAGE,"Pages/Thankyou.aspx",web);

            ///* bms Set the XSLT location web properties */
            ////TODO: Make these resources

            //UpdateProperty(MembershipReviewSiteXSLTEmail.MEMBERSHIPAPPROVED,"/_layouts/FBA/emails/MembershipApproved.xslt",web);
            //UpdateProperty(MembershipReviewSiteXSLTEmail.MEMBERSHIPERROR,"/_layouts/FBA/emails/MembershipError.xslt",web);
            //UpdateProperty(MembershipReviewSiteXSLTEmail.MEMBERSHIPPENDING,"/_layouts/FBA/emails/MembershipPending.xslt",web);
            //UpdateProperty(MembershipReviewSiteXSLTEmail.MEMBERSHIPREJECTED,"/_layouts/FBA/emails/MembershipRejected.xslt",web);
            //UpdateProperty(MembershipReviewSiteXSLTEmail.PASSWORDRECOVERY,"/_layouts/FBA/emails/PasswordRecovery.xslt",web);

            // update layouts site map
            try
            {
                //UpdateLayoutsSitemap uls = new UpdateLayoutsSitemap(site.WebApplication);
                //uls.AddSitemap("layouts.sitemap.FBAManagement.xml");
                //uls.SubmitJob();

                SPFarm.Local.Services.GetValue<SPWebService>().
                    ApplyApplicationContentToLocalServer(); 
            }
            catch (Exception ex)
            {
                Utils.LogError(ex);
            }
        }

        //private void UpdateProperty(string key, string value, SPWeb currentWeb)
        //{
        //    if (currentWeb.Properties.ContainsKey(key))
        //    {
        //        currentWeb.Properties[key] = value;
        //    }
        //    else
        //    {
        //        currentWeb.Properties.Add(key, value);

        //    }
        //    currentWeb.Properties.Update();
        //}

        // Uncomment the method below to handle the event raised after a feature has been activated.

        public override void FeatureActivated(SPFeatureReceiverProperties properties)
        {
            ActivateFBAManagement(properties);
            ActivateMembershipReviewList(properties);
        }


        // Uncomment the method below to handle the event raised before a feature is deactivated.

        public override void FeatureDeactivating(SPFeatureReceiverProperties properties)
        {
            DeactivateMembershipReviewList(properties);
        }


        // Uncomment the method below to handle the event raised after a feature has been installed.

        //public override void FeatureInstalled(SPFeatureReceiverProperties properties)
        //{
        //}


        // Uncomment the method below to handle the event raised before a feature is uninstalled.

        //public override void FeatureUninstalling(SPFeatureReceiverProperties properties)
        //{
        //}

        // Uncomment the method below to handle the event raised when a feature is upgrading.

        //public override void FeatureUpgrading(SPFeatureReceiverProperties properties, string upgradeActionName, System.Collections.Generic.IDictionary<string, string> parameters)
        //{
        //}
    }
}
