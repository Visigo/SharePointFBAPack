using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;
using System.Diagnostics;
using System.Xml;
using System.IO;
using System.Collections;
using Microsoft.SharePoint.Utilities;

namespace Visigo.Sharepoint.FormsBasedAuthentication
{
    /// <summary>
    /// Class to create a SharePoint job which updates the layout.sitemap or admin.sitemap
    /// for sites which have already been created.
    /// 
    /// Typically this call would be used in the FeatureActivated method of SPFeatureReceiver
    /// 
    /// (c) 2007 - Vincent Rothwell - http://blog.thekid.me.uk 
    /// </summary>
    public class UpdateLayoutsSitemap : SPJobDefinition
    {
        [Persisted]
        private System.Collections.Generic.List<string[]> _entries;
        [Persisted]
        private string _uniqueID;
        [Persisted]
        private string _siteUri;
        [Persisted]
        private bool _adminSite;

        private static string _xPath = "//siteMapNode[translate(@url, 'ABCDEFGHIJKLMNOPQRSTUVWXYZ', 'abcdefghijklmnopqrstuvwxyz') = '{0}']";

        /// <summary>
        /// Default constructor...used by the system and should not be used directly.
        /// </summary>
        public UpdateLayoutsSitemap() { }

        /// <summary>
        /// Contructor used to create a stadard instance which modifies the layouts.sitemap.
        /// </summary>
        /// <param name="WebApp">The SPWebApplication which needs to be updated</param>
        public UpdateLayoutsSitemap(SPWebApplication WebApp)
            : this(WebApp, false)
        {
        }

        /// <summary>
        /// Contructor used to create a stadard instance which can modify
        /// either the layouts.sitemap or admin.sitemap.
        /// </summary>
        /// <param name="WebApp">The SPWebApplication which needs to be updated</param>
        /// <param name="AdminSite">True to update admin.sitemap False to update layouts.sitemap</param>
        public UpdateLayoutsSitemap(SPWebApplication WebApp, bool AdminSite)
            : this(WebApp, Guid.NewGuid().ToString(), AdminSite)
        {
        }

        private UpdateLayoutsSitemap(SPWebApplication WebApp, string UniqueID, bool AdminSite)
            : base("ULS_" + UniqueID, WebApp, null, SPJobLockType.None)
        {
            _entries = new List<string[]>();
            _uniqueID = UniqueID;
            // ModifiedBySolvion
            // bhi - 09.01.2012
            // Fixed dispose bug
            #region original
            //_siteUri = WebApp.Sites[0].RootWeb.Url;
            #endregion
            using (SPSite site = WebApp.Sites[0])
            {
                _siteUri = site.RootWeb.Url;
            }
            // EndModifiedBySolvion
            _adminSite = AdminSite;
        }

        /// <summary>
        /// Add a sitemap which has been installed in \Template\Layouts or \Template\Admin
        /// </summary>
        /// <param name="fileName">The filename of the sitemap containing the entries you wish to add</param>
        public void AddSitemap(string fileName)
        {
            string sFolder = (_adminSite) ? "ADMIN" : "LAYOUTS";

            string sSitemapPath = SPUtility.GetGenericSetupPath("Template\\" + sFolder) + "\\" + fileName;
            XmlDocument oDoc = new XmlDocument();
            oDoc.Load(sSitemapPath);
            AddSitemap(oDoc);
        }

        /// <summary>
        /// Add a previously loaded XML document of sitemap entries .
        /// </summary>
        /// <param name="SiteMapEntries">The xml document containing the sitemap entries.</param>
        public void AddSitemap(XmlDocument SiteMapEntries)
        {
            XmlNodeList oEntries = SiteMapEntries.DocumentElement.SelectNodes("siteMapNode");
            foreach (XmlElement oEntry in oEntries)
                AddEntry(oEntry, "/");
        }

        /// <summary>
        /// Add a new entry defined in the XML element
        /// </summary>
        /// <param name="currentElement">The XmlElement containing the sitemap entry</param>
        /// <param name="parentUrl">The parentUrl for this entry</param>
        private void AddEntry(XmlElement currentElement, string parentUrl)
        {
            string sRequiredParameters = currentElement.GetAttribute("requiredParameters");
            if (sRequiredParameters == "") sRequiredParameters = null;

            if (!string.IsNullOrEmpty(currentElement.GetAttribute("parentUrl"))) parentUrl = currentElement.GetAttribute("parentUrl");

            AddEntry(currentElement.GetAttribute("title"), currentElement.GetAttribute("url"), parentUrl, sRequiredParameters);

            XmlNodeList oEntries = currentElement.SelectNodes("siteMapNode");
            foreach (XmlElement oEntry in oEntries)
                AddEntry(oEntry, currentElement.GetAttribute("url"));
        }

        /// <summary>
        /// Adds an entry which will ventually be added to the sitemap.
        /// </summary>
        /// <param name="theTitle">The Title of the entry</param>
        /// <param name="theUrl">The URL of the entry</param>
        /// <param name="parentUrl">The Parent URL of the entry</param>
        public void AddEntry(string theTitle, string theUrl, string parentUrl)
        {
            AddEntry(theTitle, theUrl, parentUrl, null);
        }

        /// <summary>
        /// Add an entry which will eventually be added to the sitemap
        /// </summary>
        /// <param name="theTitle">The Title of the entry</param>
        /// <param name="theUrl">The URL of the entry</param>
        /// <param name="parentUrl">The Parent URL of the entry</param>
        /// <param name="requiredParameters">The parameters required to be added to the URL</param>
        public void AddEntry(string theTitle, string theUrl, string parentUrl, string requiredParameters)
        {
            string[] arEntry = new string[] { theTitle, theUrl, parentUrl, requiredParameters };
            _entries.Add(arEntry);
        }

        /// <summary>
        /// Submits the job and schedules it to run on every server in the Farm.
        /// </summary>
        public void SubmitJob()
        {
            Schedule = new SPOneTimeSchedule(DateTime.Now);
            Title = "Update Layouts Sitemap (" + _uniqueID + ")";
            Update();
        }

        /// <summary>
        /// Called by OWSTIMER when the job runs on a server. Overriden from SPJobDefinition.
        /// </summary>
        public override void Execute(Guid targetInstanceId)
        {
            try
            {
                SPWebApplication o = SPWebApplication.Lookup(new Uri(_siteUri));

                foreach (SPUrlZone z in o.IisSettings.Keys)
                    UpdateIisSite(o.IisSettings[z]);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Failed to update the sitemap");
                Debug.WriteLine(ex);
                throw;
            }
        }

        /// <summary>
        /// Update the IIS site with the new sitemap entries
        /// </summary>
        /// <param name="oSettings">The settings of the IIS application to update</param>
        private void UpdateIisSite(SPIisSettings oSettings)
        {
            string sSiteMapLocation = oSettings.Path + "\\_app_bin\\" + ((_adminSite) ? "admin" : "layouts") + ".sitemap";

            XmlDocument oDoc = new XmlDocument();
            oDoc.Load(sSiteMapLocation);

            foreach (string[] arEntry in _entries)
                AddNewNode(oDoc, arEntry);

            if (_entries.Count > 0) oDoc.Save(sSiteMapLocation);
        }

        /// <summary>
        /// Adds the new sitemap node. Checks the entire documentfor the parent using a 
        /// case insensitive XPath query. Also checks to see if the path already exists
        /// and deletes the current entry.
        /// </summary>
        /// <param name="oDoc">The sitemap XML document</param>
        /// <param name="arEntry">The array describing the new entry</param>
        private void AddNewNode(XmlDocument oDoc, string[] arEntry)
        {
            XmlNode oNode = oDoc.DocumentElement.SelectSingleNode(string.Format(_xPath, arEntry[2].ToLower()));
            if (oNode is XmlElement)
            {
                XmlElement oParent = (XmlElement)oNode;

                // Ensure there isn't a duplicate entry
                XmlNode oExistingNode = oParent.OwnerDocument.SelectSingleNode("//*[@url='" + arEntry[1] + "']");
                if (oExistingNode != null) oExistingNode.ParentNode.RemoveChild(oExistingNode);

                XmlElement oNewNode = oParent.OwnerDocument.CreateElement("siteMapNode");
                oNewNode.SetAttribute("title", arEntry[0]);
                oNewNode.SetAttribute("url", arEntry[1]);
                if (arEntry[3] != null) oNewNode.SetAttribute("requiredParameters", arEntry[3]);

                oParent.AppendChild(oNewNode);
            }
        }
    }
}

