using System;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Security;
using Microsoft.SharePoint.Administration;

namespace Visigo.Sharepoint.FormsBasedAuthentication.Features.FBADiagnosticsService
{
    /// <summary>
    /// This class handles events raised during feature activation, deactivation, installation, uninstallation, and upgrade.
    /// </summary>
    /// <remarks>
    /// The GUID attached to this class may be used during packaging and should not be modified.
    /// </remarks>

    [Guid("0d07a58f-4cf8-4a44-8b3d-d03c4d6011fb")]
    public class FBADiagnosticsServiceEventReceiver : SPFeatureReceiver
    {
        // Uncomment the method below to handle the event raised after a feature has been activated.

        public override void FeatureActivated(SPFeatureReceiverProperties properties)
        {

            SPWebService parentService = properties.Feature.Parent as SPWebService;

            if (parentService != null)
            {
                SPFarm farm = parentService.Farm;

                string serviceId = Visigo.Sharepoint.FormsBasedAuthentication.FBADiagnosticsService.ServiceName;
                // remove service if it allready exists
                foreach (SPService service in farm.Services)
                {
                    if (service is Visigo.Sharepoint.FormsBasedAuthentication.FBADiagnosticsService)
                    {
                        if (service.Name == serviceId)
                        {
                            service.Delete();
                        }
                    }
                }

                // install the service
                farm.Services.Add(Visigo.Sharepoint.FormsBasedAuthentication.FBADiagnosticsService.Local);
            }

        }


        // Uncomment the method below to handle the event raised before a feature is deactivated.

        public override void FeatureDeactivating(SPFeatureReceiverProperties properties)
        {
            ////Remove Diagnostics Service

            SPWebService parentService = properties.Feature.Parent as SPWebService;

            if (parentService != null)
            {
                SPFarm farm = parentService.Farm;

                string serviceId = Visigo.Sharepoint.FormsBasedAuthentication.FBADiagnosticsService.ServiceName;
                // remove service if it exists
                foreach (SPService service in farm.Services)
                {
                    if (service is Visigo.Sharepoint.FormsBasedAuthentication.FBADiagnosticsService)
                    {
                        if (service.Name == serviceId)
                        {
                            service.Delete();
                        }
                    }
                }
            }


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
