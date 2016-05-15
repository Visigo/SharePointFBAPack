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
        private string _resourceSource;

        public LocalizedWebDescriptionAttribute(string resourceSource, string description)
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
                    DescriptionValue = LocalizedString.GetString(_resourceSource,base.Description);
                    _localized = true;
                }
                return base.Description;
            }
        }
    }
}
