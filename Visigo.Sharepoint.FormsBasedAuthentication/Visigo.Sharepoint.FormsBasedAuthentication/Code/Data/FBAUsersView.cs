using System;
using System.Collections;
using System.Data;
using System.Web.UI;
using System.Web.Security;
using Microsoft.SharePoint;
using Microsoft.SharePoint.Administration;
using Microsoft.SharePoint.Utilities;
using System.Reflection;
using System.Globalization;


namespace Visigo.Sharepoint.FormsBasedAuthentication
{
    /// <summary>
    /// Data source for the User Management user display view. Gets all FBA users and adds basic membership info 
    /// </summary>
    class FBAUsersView : DataSourceView
    {
        private FBADataSource _owner;

        public FBAUsersView(FBADataSource owner, string viewName) : base(owner, viewName) 
        {
            _owner = owner;
        }

        protected override IEnumerable ExecuteSelect(DataSourceSelectArguments selectArgs)
        {
            
            // only continue if a membership provider has been configured
            if (!Utils.IsProviderConfigured())
                return null;

            // get site details
            SPSite site = SPContext.Current.Site;
            string provider = Utils.GetMembershipProvider(site);
            if (provider == null)
                return null;
            
            SPWeb web = site.RootWeb;

            string yes = LocalizedString.GetString("FBAPackFeatures", "Yes");

            string no = LocalizedString.GetString("FBAPackFeatures", "No");

            // we only display users that have been added to SharePoint
            // we use the localized name, safe for non-English SharePoint servers
            SPList list = web.SiteUserInfoList; //web.Lists[SPUtility.GetLocalizedString("$Resources:userinfo_schema_listtitle", "core", web.Language)];

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
                "</OrderBy>", provider);

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
                users.Columns.Add("Modified", typeof(DateTime));
                users.Columns.Add("Created", typeof(DateTime));
            }

            users.Columns.Add("Active");
            users.Columns.Add("Locked");
            users.Columns.Add("LastLogin", typeof(DateTime));
            users.Columns.Add("IsInSharePoint");
            users.Columns.Add("NonProviderName");


            // Add additional user data to table
            foreach (DataRow row in users.Rows)
            {
                // remove provider name to get actual user name
                string userName = Utils.DecodeUsername(row["Name"].ToString());
                row["NonProviderName"] = userName;
            }

            int totalRecords = 0;

            foreach (MembershipUser memberuser in Utils.BaseMembershipProvider(site).GetAllUsers(0,100000, out totalRecords))
            {
                bool bFoundMember = false;
                foreach (DataRow row in users.Rows)
                {
                    if (memberuser.UserName.ToLower() == row["NonProviderName"].ToString().ToLower())
                    {
                        row["Name"] = memberuser.UserName;
                        row["Active"] = memberuser.IsApproved ? yes : no;
                        row["Locked"] = memberuser.IsLockedOut ? yes : no;
                        row["LastLogin"] = memberuser.LastLoginDate;
                        row["IsInSharePoint"] = yes;
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
                    datanewuser["Active"] = memberuser.IsApproved ? yes : no;
                    datanewuser["Locked"] = memberuser.IsLockedOut ? yes : no;
                    datanewuser["LastLogin"] = memberuser.LastLoginDate;
                    datanewuser["IsInSharePoint"] = no;
                    users.Rows.Add(datanewuser);
                }
                
            }

            //Remove users that weren't found in SharePoint
            for(int i = users.Rows.Count - 1; i >= 0; i--)
            {
                if (users.Rows[i]["IsInSharePoint"].ToString() != yes && users.Rows[i]["IsInSharePoint"].ToString() != no)
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

            //Filter the data if a filter is provided
            if (_owner.SearchText.Length > 0)
            {
                dataView.RowFilter = string.Format("Name LIKE '%{0}%' OR Email LIKE '%{0}%' OR Title LIKE '%{0}%'", _owner.SearchText);
            }
            else
            {
                dataView.RowFilter = "";
            }

            // return as a DataList            
            return (IEnumerable)dataView;
        }      
    }
}