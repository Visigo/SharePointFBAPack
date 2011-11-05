using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI;
using System.Collections;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;
using System.Data;
using System.Web.Security;

namespace Visigo.Sharepoint.FormsBasedAuthentication
{
    /// <summary>
    /// Data source for the User Management role display view. Gets all FBA roles.
    /// </summary>
    class FBARolesView : DataSourceView
    {
        public FBARolesView(IDataSource owner, string viewName) : base(owner, viewName) { }
        
        protected override IEnumerable ExecuteSelect(DataSourceSelectArguments selectArgs)
        {
            // only continue if a membership provider has been configured
            if (!Utils.IsProviderConfigured())
                return null;
                          
            // get roles and build data table
            DataTable dataTable = new DataTable();
            String[] roles = Utils.BaseRoleProvider().GetAllRoles();
            dataTable.Columns.Add("Role");
            dataTable.Columns.Add("UsersInRole");

            // add users in role counts
            for (int i = 0; i < roles.Length; i++)
            {
                DataRow row = dataTable.NewRow();
                row["Role"] = roles[i];
                row["UsersInRole"] = Utils.BaseRoleProvider().GetUsersInRole(roles[i].ToString()).Length;
                dataTable.Rows.Add(row);
            }
            dataTable.AcceptChanges();
            DataView dataView = new DataView(dataTable);

            // sort if a sort expression available
            if (selectArgs.SortExpression != String.Empty)
            {
                dataView.Sort = selectArgs.SortExpression;
            }

            // return as a DataList
            return (IEnumerable) dataView;
        }      
    }
}
