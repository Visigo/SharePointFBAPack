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
        private Type _resourceSource;

        public LocalizedCategoryAttribute(Type resourceSource, string category)
            : base(category)
        {
            _resourceSource = resourceSource;
        }

        protected override string  GetLocalizedString(string value)
        {
 	        ResourceManager manager = new ResourceManager(_resourceSource);
            return manager.GetString(base.Category);
        }

    }
}
