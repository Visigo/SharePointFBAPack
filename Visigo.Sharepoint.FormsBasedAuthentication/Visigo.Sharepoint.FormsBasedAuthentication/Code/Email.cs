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
using System.Net;

namespace Visigo.Sharepoint.FormsBasedAuthentication
{
    public static class Email
    {
        public static bool SendEmail(SPWeb web, string emailTo, string xsltTemplateFile)
        {
            return SendEmail(web, emailTo, xsltTemplateFile, (null as IDictionary));
        }

        public static bool SendEmail(SPWeb web, string emailTo, string xslt, IDictionary xslValues)
        {


            XmlDocument xmlDoc;
            XPathNavigator xpathNavigator;
            XslCompiledTransform xslEmailTransform = new XslCompiledTransform();
            XsltArgumentList xslArguments;
            StringBuilder sbEmail;
            XmlTextWriter xmlWriter;
            XmlNode xmlNodeTitle;
            XmlDocument xmlEmail;
            XsltSettings settings = new XsltSettings(true, true); 
            XmlUrlResolver resolver = new XmlUrlResolver(); 
            string subject = string.Empty;

            try
            {
                xslEmailTransform.Load(new XmlTextReader(xslt, XmlNodeType.Document, null), settings, resolver);

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

                return SendEmail(web, emailTo, subject, sbEmail.ToString());
            }
            catch (Exception ex)
            {
                Utils.LogError(ex);
                return false;
            }
            
        }

        public static bool SendEmail(SPWeb web, string emailTo, string subject, string body)
        {
            if (!SPUtility.IsEmailServerSet(web))
            {
                return false;
            }

            MembershipSettings settings = new MembershipSettings(web);

            StringDictionary parameters = new StringDictionary();

            parameters.Add("subject", subject);
            parameters.Add("to", emailTo);
            parameters.Add("from", settings.MembershipReplyToEmailAddress);

            return SPUtility.SendEmail(web, parameters, body);
        }
    }
}
