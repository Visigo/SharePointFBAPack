using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;

namespace Visigo.Sharepoint.FormsBasedAuthentication
{
    /// <summary>
    /// Loads a template from an ASCX, but unlike SimpleTemplate adds controls directly to the specified container so that it will work with ASP.NET Login Controls
    /// (SimpleTemplate adds the controls to a parent control and adds the parent control to the container)
    /// </summary>
    public class TemplateLoader : ITemplate
    {
        private string _virtualPath;
        private Page _page;

        public TemplateLoader(string virtualPath, Page page)
        {
            _virtualPath = virtualPath;
            _page = page;
        }

        void ITemplate.InstantiateIn(Control container)
        {
            ITemplate template = _page.LoadTemplate(_virtualPath);
            Control tempContainer = new Control();
            template.InstantiateIn(tempContainer);

            while (tempContainer.Controls[0].HasControls())
            {
                container.Controls.Add(tempContainer.Controls[0].Controls[0]);
            }
        }
    }

}
