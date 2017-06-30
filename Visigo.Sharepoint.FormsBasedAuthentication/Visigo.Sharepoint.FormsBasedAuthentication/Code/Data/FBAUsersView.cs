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
using System.Collections.Generic;


namespace Visigo.Sharepoint.FormsBasedAuthentication
{
    /// <summary>
    /// Data source for the User Management user display view. Gets all FBA users and adds basic membership info 
    /// </summary>
    class FBAUsersView : DataSourceView
    {
        private FBADataSource _owner;

        private System.Web.Caching.Cache _cache = System.Web.HttpRuntime.Cache;

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

            string cacheKey = String.Format("Visigo.SharePoint.FormsBasedAuthentication.FBAUsersView.{0}", provider);

            Dictionary<string, SPListItem> spUsers = _cache.Get(cacheKey) as Dictionary<string, SPListItem>;

            //Reload site user info list or grab from cache
            if (_owner.ResetCache || spUsers == null)
            {
                spUsers = new Dictionary<string, SPListItem>();

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
                    "</Where>", provider);

                query.ViewFields = "<FieldRef Name='Name' /><FieldRef Name='LinkTitle' /><FieldRef Name='Email' /><FieldRef Name='Modified' /><FieldRef Name='Created' />";
                query.RowLimit = 100000;
                //Convert SPListItemCollection to dictionary for fast lookup

                try
                {
                    SPListItemCollection userList = list.GetItems(query);

                    if (userList != null)
                    {
                        foreach (SPListItem item in userList)
                        {

                            string username = item["Name"] as string;
                            string decodedName = Utils.DecodeUsername(username);
                            if (username != decodedName)
                            {
                                spUsers.Add(decodedName, item);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Utils.LogError(ex);
                    return null;
                }

                _cache.Add(cacheKey, spUsers, null,
                  DateTime.UtcNow.AddMinutes(1.0),
                  System.Web.Caching.Cache.NoSlidingExpiration,
                  System.Web.Caching.CacheItemPriority.Normal, null);
            }

            //Create a datatable for returning the results
            DataTable users = new DataTable();
            users.Columns.Add("Title");
            users.Columns.Add("Name");
            users.Columns.Add("Email");
            users.Columns.Add("Modified", typeof(DateTime));
            users.Columns.Add("Created", typeof(DateTime));
            users.Columns.Add("Active");
            users.Columns.Add("Locked");
            users.Columns.Add("LastLogin", typeof(DateTime));
            users.Columns.Add("IsInSharePoint");

            int totalRecords = 0;
            int spUsersCount = spUsers.Count;
            int spUsersFound = 0;

            users.BeginLoadData();

            //Add all membership users to the datatable
            foreach (MembershipUser memberuser in Utils.BaseMembershipProvider(site).GetAllUsers(0,100000, out totalRecords))
            {
                string title = null;
                string email = memberuser.Email;
                DateTime? modified = null;
                DateTime? created = null;
                string isInSharepoint = no;

                SPListItem spUser = null;

                //See if there is a matching sharepoint user - if so grab the values
                if (spUsersFound < spUsersCount)
                {
                    if (spUsers.TryGetValue(memberuser.UserName.ToLower(), out spUser))
                    {
                        spUsersFound++;
                        title = spUser["Title"] as string;
                        created = spUser["Created"] as DateTime?;
                        modified = spUser["Modified"] as DateTime?;
                        isInSharepoint = yes;
                        //Make sure the SharePoint email field has a value before copying it over
                        string spEmail = spUser["EMail"] as string;
                        if (!String.IsNullOrEmpty(spEmail))
                        {
                            email = spEmail;
                        }
                        
                    }
                }

                //Add the matched up membership + sharepoint data to the datatable
                users.LoadDataRow(new object[] {
                    title,
                    memberuser.UserName,
                    email, 
                    modified, 
                    created, 
                    memberuser.IsApproved ? yes : no, 
                    memberuser.IsLockedOut ? yes : no, 
                    memberuser.LastLoginDate, 
                    isInSharepoint
                }, false);

            }

            users.EndLoadData();

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