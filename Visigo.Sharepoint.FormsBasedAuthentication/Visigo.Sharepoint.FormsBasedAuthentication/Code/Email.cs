using System;
using System.Collections.Generic;
using System.Collections;
using System.Collections.Specialized;
using Microsoft.SharePoint.Utilities;
using Microsoft.SharePoint;
using System.Text;
using System.Xml;
using System.Xml.Xsl;
using System.Xml.XPath;
using System.IO;
using System.Net.Mail;
using System.Web.Configuration;
using System.Configuration;

namespace Visigo.Sharepoint.FormsBasedAuthentication
{
    public static class Email
    {
        public static bool SendEmail(SPWeb web, string emailTo, string xsltTemplateFile)
        {
            return SendEmail(web, emailTo, xsltTemplateFile, null);
        }

        public static bool SendEmail(SPWeb web, string emailTo, string xsltTemplateFile, IDictionary xslValues)
        {
            if (!SPUtility.IsEmailServerSet(web))
            {
                return false;
            }

            XmlDocument xmlDoc;
            XPathNavigator xpathNavigator;
            XslCompiledTransform xslEmailTransform = new XslCompiledTransform();
            XsltArgumentList xslArguments;
            StringBuilder sbEmail;
            XmlTextWriter xmlWriter;
            XmlNode xmlNodeTitle;
            XmlDocument xmlEmail;
            string subject = string.Empty;

            try
            {
                xslEmailTransform.Load(xsltTemplateFile);

                xmlDoc = new XmlDocument();
                xmlDoc.AppendChild(xmlDoc.CreateElement("DocumentRoot"));
                xpathNavigator = xmlDoc.CreateNavigator();

                xslArguments = new XsltArgumentList();

                if (xslValues != null)
                {
                    foreach (DictionaryEntry xslEntry in xslValues)
                    {
                        xslArguments.AddExtensionObject(xslEntry.Key.ToString(), xslEntry.Value);
                    }
                }

                sbEmail = new StringBuilder();
                xmlWriter = new XmlTextWriter(new StringWriter(sbEmail));

                xslEmailTransform.Transform(xpathNavigator, xslArguments, xmlWriter);

                xmlEmail = new XmlDocument();
                xmlEmail.LoadXml(sbEmail.ToString());
                xmlNodeTitle = xmlEmail.SelectSingleNode("//title");

                subject = xmlNodeTitle.InnerText;

                SPUtility.SendEmail(web, false, false, emailTo, subject, sbEmail.ToString());
            }
            catch (Exception ex)
            {
                Utils.LogError(ex);
                return false;
            }
            return true;
            
        }
    }
}
