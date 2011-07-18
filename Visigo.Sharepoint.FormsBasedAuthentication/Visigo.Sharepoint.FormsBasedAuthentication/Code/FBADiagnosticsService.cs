using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;
using System.Runtime.InteropServices;

namespace Visigo.Sharepoint.FormsBasedAuthentication
{
    class FBADiagnosticsService : SPDiagnosticsServiceBase
    {

        public static string AreaName = "SharePoint 2010 FBA Pack";

        public static string ServiceName = "SharePoint 2010 FBA Pack Diagnostics Service";

        public enum FBADiagnosticsCategory
        {
            General
        } // enum MyDiagnosticsCategory

        public FBADiagnosticsService()
            : base(ServiceName, SPFarm.Local)
        {
        } // ctor()

        public FBADiagnosticsService(string name, SPFarm parent)
            : base(name, parent)
        {
            // SPDiagnosticsServiceBase.GetLocal() wants the default ctor and this one
        } // ctor()

        protected override bool HasAdditionalUpdateAccess()
        {
            // Without this SPDiagnosticsServiceBase.GetLocal<MyDiagnosticsService>()
            // throws a SecurityException, see
            // http://share2010.wordpress.com/tag/sppersistedobject/
            return true;
        } // HasAdditionalUpdateAccess()

        public static FBADiagnosticsService Local
        {
            get
            {
                // SPUtility.ValidateFormDigest(); doesn't work here
                if (SPContext.Current != null)
                {
                    SPContext.Current.Web.AllowUnsafeUpdates = true;
                }
                // (Else assume this is called from a FeatureReceiver)
                return SPDiagnosticsServiceBase.GetLocal<FBADiagnosticsService>();
            }
        } // Local

        public void WriteTrace(ushort id, FBADiagnosticsCategory fbaDiagnosticsCategory, TraceSeverity traceSeverity, string message, params object[] data)
        {
            if (traceSeverity != TraceSeverity.None)
            {
                // traceSeverity==TraceSeverity.None would cause an ArgumentException:
                // "Specified value is not supported for the severity parameter."
                SPDiagnosticsCategory category = Local.Areas[AreaName].Categories[fbaDiagnosticsCategory.ToString()];
                Local.WriteTrace(id, category, traceSeverity, message, data);
            }
        } // LogMessage()

        protected override IEnumerable<SPDiagnosticsArea> ProvideAreas()
        {
            List<SPDiagnosticsCategory> categories = new List<SPDiagnosticsCategory>();
            categories.Add(new SPDiagnosticsCategory(FBADiagnosticsCategory.General.ToString(), TraceSeverity.Verbose, EventSeverity.Verbose));

            SPDiagnosticsArea area = new SPDiagnosticsArea(AreaName, 0, 0, false, categories);

            List<SPDiagnosticsArea> areas = new List<SPDiagnosticsArea>();

            areas.Add(area);

            return areas;
        } // ProvideAreas()

    }
}
