using System;
using System.Collections;
using System.Data;
using System.Web.UI;
using System.Web.Security;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;
using Microsoft.SharePoint.Utilities;
using System.Reflection;


namespace Visigo.Sharepoint.FormsBasedAuthentication
{
    /// <summary>
    /// Data source for the User Management user display view. Gets all FBA users and adds basic membership info 
    /// </summary>
    class FBAUsersView : DataSourceView
    {

        public FBAUsersView(IDataSource owner, string viewName) : base(owner, viewName) { }
        
        protected override IEnumerable ExecuteSelect(DataSourceSelectArguments selectArgs)
        {
            // only continue if a membership provider has been configured
            if (!Utils.IsProviderConfigured())
                return null;

            // get site details
            SPSite site = SPContext.Current.Site;
            SPIisSettings settings = Utils.GetFBAIisSettings(site);
            if (settings == null)
                return null;
            
            SPWeb web = site.RootWeb;

            // we only display users that have been added to SharePoint
            // we use the localized name, safe for non-English SharePoint servers
            SPList list = web.Lists[SPUtility.GetLocalizedString("$Resources:userinfo_schema_listtitle", "core", web.Language)];

            // create query list
            SPQuery query = new SPQuery();
            query.Query = string.Format(
                "<Where>" +
                    "<And>" +
                        "<Eq><FieldRef Name='ContentType' /><Value Type='Text'>Person</Value></Eq>" +
                        "<Contains><FieldRef Name='Name' /><Value Type='Text'>{0}</Value></Contains>" +
                    "</And>" +
                "</Where>" +
                "<OrderBy>" +
                    "<FieldRef Name='LinkTitle' />" +
                "</OrderBy>", settings.FormsClaimsAuthenticationProvider.MembershipProvider.ToString());

            query.ViewFields = "<FieldRef Name='Name' /><FieldRef Name='LinkTitle' /><FieldRef Name='Email' /><FieldRef Name='Modified' /><FieldRef Name='Created' />";

            // run query to get table of users
            DataTable users = null;
            try
            {
                users = list.GetItems(query).GetDataTable();
            }
            catch (Exception ex) 
            {
                Utils.LogError(ex);
                return null;
            }

            if (users == null)
            {
                users = new DataTable();
                users.Columns.Add("ID");
                users.Columns.Add("Title");
                users.Columns.Add("Name");
                users.Columns.Add("LinkTitle");
                users.Columns.Add("Email");
                users.Columns.Add("Modified");
                users.Columns.Add("Created");
            }

            users.Columns.Add("Active");
            users.Columns.Add("IsInSharePoint");
            users.Columns.Add("NonProviderName");


            // Add additional user data to table
            foreach (DataRow row in users.Rows)
            {
                // remove provider name to get actual user name
                string userName = Utils.DecodeUsername(row["Name"].ToString());
                row["NonProviderName"] = userName;
            }

            foreach (MembershipUser memberuser in Membership.GetAllUsers())
            {
                bool bFoundMember = false;
                foreach (DataRow row in users.Rows)
                {
                    if (memberuser.UserName.ToLower() == row["NonProviderName"].ToString().ToLower())
                    {
                        row["Name"] = memberuser.UserName;
                        row["Active"] = memberuser.IsApproved ? "Yes" : "No";
                        row["IsInSharePoint"] = "Yes";
                        bFoundMember = true;
                        //users.Rows[i].Delete();
                        break;
                    }
                }
                if (!bFoundMember)
                {
                    //Add member to the data table
                    DataRow datanewuser = users.NewRow();
                    datanewuser["Name"] = memberuser.UserName;
                    datanewuser["Email"] = memberuser.Email;
                    datanewuser["Active"] = memberuser.IsApproved ? "Yes" : "No";
                    datanewuser["IsInSharePoint"] = "No";
                    users.Rows.Add(datanewuser);
                }
                
            }

            //Remove users that weren't found in SharePoint
            for(int i = users.Rows.Count - 1; i >= 0; i--)
            {
                if (users.Rows[i]["IsInSharePoint"].ToString() != "Yes" && users.Rows[i]["IsInSharePoint"].ToString() != "No")
                {
                    users.Rows[i].Delete();
                }
            }

            // sort if a sort expression available
            DataView dataView = new DataView(users);
            if (selectArgs.SortExpression != String.Empty)
            {
                dataView.Sort = selectArgs.SortExpression;
            }

            // return as a DataList            
            return (IEnumerable)dataView;
        }      
    }
}