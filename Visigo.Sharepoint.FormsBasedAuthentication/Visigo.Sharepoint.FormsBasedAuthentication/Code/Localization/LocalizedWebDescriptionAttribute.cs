using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls.WebParts;
using System.Resources;

namespace Visigo.Sharepoint.FormsBasedAuthentication
{
    internal class LocalizedWebDescriptionAttribute : WebDescriptionAttribute
    {
        private bool _localized;
        private Type _resourceSource;

        public LocalizedWebDescriptionAttribute(Type resourceSource, string description)
            : base(description)
        {
            _resourceSource = resourceSource;
        }

        public override string Description
        {
            get
            {
                if (!_localized)
                {
                    ResourceManager manager = new ResourceManager(_resourceSource);
                    DescriptionValue = manager.GetString(base.Description);
                    _localized = true;
                }
                return base.Description;
            }
        }
    }
}
