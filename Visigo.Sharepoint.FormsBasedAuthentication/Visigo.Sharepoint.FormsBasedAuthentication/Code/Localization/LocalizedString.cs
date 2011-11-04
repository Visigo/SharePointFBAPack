using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SharePoint.Utilities;
using System.Globalization;

namespace Visigo.Sharepoint.FormsBasedAuthentication
{
    class LocalizedString
    {
        private string _source;
        public LocalizedString(string source)
        {
            _source = source;
        }

        public string GetString(string key)
        {
            return GetString(_source, key);
        }

        /// <summary>
        /// Return a localized string by using source key
        /// </summary>
        /// <param name="sourceKey"></param>
        /// <returns></returns>
        public static string GetString(string source, string key)
        {
            return SPUtility.GetLocalizedString(
                string.Format("$Resources:{0}", key),
                source,
                (uint)CultureInfo.CurrentUICulture.LCID);
        }

        public static string GetGlobalString(string source, string key)
        {
            return System.Web.HttpContext.GetGlobalResourceObject(source, key).ToString();
        }
    }
}
