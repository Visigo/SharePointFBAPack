using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.Collections;
using System.Web.Security;
using Microsoft.SharePoint.Administration;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Utilities;

namespace Visigo.Sharepoint.FormsBasedAuthentication
{
    /// <summary>
    /// Provides data sources for User and Role display pages
    /// </summary>
    public class FBADataSource : DataSourceControl
    {
        private string _viewName;
        private DataSourceView _view = null;

        public FBADataSource() : base() { }

        public string ViewName
        {
            get { return _viewName; }
            set { _viewName = value; }
        }

        public string SearchText { 
            get {
                string s = (string)ViewState["SearchText"];
                return (s != null) ? s : String.Empty;
            }

            set { ViewState["SearchText"] = value; }
        }

        /// <summary>
        /// return a strongly typed view for the current data source control
        /// </summary>
        /// <param name="viewName"></param>
        /// <returns></returns> 
        protected override DataSourceView GetView(string viewName)
        {
            // only retrieve a view if a membership provider can be found
            if (_view == null)
            {
                
                try
                {
                    if (ViewName == "FBAUsersView")
                        _view = new FBAUsersView(this, viewName);
                    else if (ViewName == "FBARolesView")
                        _view = new FBARolesView(this, viewName);
                }
                catch (Exception ex)
                {
                    Utils.LogError(ex, true);
                }
            }
            return _view;
        }

        /// <summary>
        /// return a collection of available views
        /// </summary>
        /// <returns></returns> 
        protected override ICollection GetViewNames()
        {
            ArrayList views = new ArrayList(2);
            views.Add("FBAUsersView");
            views.Add("FBARolesView");
            return views as ICollection;
        }
    }

}
