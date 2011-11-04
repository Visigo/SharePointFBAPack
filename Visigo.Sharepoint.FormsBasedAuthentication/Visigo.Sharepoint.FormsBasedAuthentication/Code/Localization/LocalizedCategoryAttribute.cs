using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Resources;

namespace Visigo.Sharepoint.FormsBasedAuthentication
{
    internal class LocalizedCategoryAttribute : CategoryAttribute
    {
        private string _resourceSource;

        public LocalizedCategoryAttribute(string resourceSource, string category)
            : base(category)
        {
            _resourceSource = resourceSource;
        }

        protected override string  GetLocalizedString(string value)
        {
            return LocalizedString.GetString(_resourceSource,base.Category);
        }

    }
}
