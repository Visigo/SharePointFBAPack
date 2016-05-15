using Microsoft.SharePoint.WebControls;
using System.Web.Security;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Globalization;
using Microsoft.SharePoint.Utilities;

namespace Visigo.Sharepoint.FormsBasedAuthentication
{
    /// <summary>
    /// Code behind for UsersDisp.aspx
    /// </summary>
    public partial class UsersDisp : LayoutsPageBase
    {

        protected override void OnInit(System.EventArgs e)
        {
            // display error if membership provider not configured
            if (!Utils.IsProviderConfigured())
            {
                lblMessage.Text = LocalizedString.GetGlobalString("FBAPackWebPages","MembershipNotConfigured");
                MemberGrid.Visible = false;
                ToolBarPlaceHolder.Visible = false;
                onetidNavNodesTB.Visible = false;
                SearchControls.Visible = false;
            }
            base.OnInit(e);
        }

        protected void Search_Click(object sender, System.EventArgs e)
        {
            UserDataSource.SearchText = SearchText.Text;
            MemberGrid.DataBind();
        }
        
        protected override bool RequireSiteAdministrator
        {
            get { return true; }
        }

        // ModifiedBySolvion
        // bhi - 20.12.2011
        // remember sort and search settings
        protected override void OnLoad(System.EventArgs e)
        {
            base.OnLoad(e);

            if (!Page.IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["SortField"]))
                {
                    SortDirection dir = SortDirection.Ascending;
                    if (!string.IsNullOrEmpty(Request.QueryString["SortDir"]))
                    {
                        if (Request.QueryString["SortDir"].ToLower() == "desc")
                        {
                            dir = SortDirection.Descending;
                        }
                    }
                    MemberGrid.Sort(Request.QueryString["SortField"], dir);
                }

                if (!string.IsNullOrEmpty(Request.QueryString["PageIndex"]))
                {
                    int pageIndex = 0;
                    if (int.TryParse(Request.QueryString["PageIndex"], out pageIndex))
                    {
                        MemberGrid.PageIndex = pageIndex;
                    }
                }

                if (!string.IsNullOrEmpty(Request.QueryString["k"]))
                {
                    SearchText.Text = Request.QueryString["k"];
                    UserDataSource.SearchText = SearchText.Text;
                }
            }
        }

        protected override void OnPreRender(System.EventArgs e)
        {
            base.OnPreRender(e);
            string source = SPHttpUtility.UrlKeyValueEncode(this.CreateSourceUrl()); //SPHttpUtility.UrlPathEncode(this.CreateSourceUrl(), true);
            (MemberGrid.Columns[0] as SPMenuField).NavigateUrlFormat = "UserEdit.aspx?UserName={0}&Source=" + source;
            Edit.ClientOnClickNavigateUrl = "UserEdit.aspx?UserName=%USERNAME%&Source=" + source;
			ResetPassword.ClientOnClickNavigateUrl = "UserResetPassword.aspx?UserName=%USERNAME%&Source=" + source;
            Delete.ClientOnClickNavigateUrl = "UserDelete.aspx?UserName=%USERNAME%&Source=" + source;
            (idNewNavNode as ToolBarButton).NavigateUrl = "UserNew.aspx?Source=" + source;
        }

        private string CreateSourceUrl()
        {
            string url = "FBA/Management/UsersDisp.aspx";
            SPUtility.DetermineRedirectUrl(url,SPRedirectFlags.RelativeToLayoutsPage, this.Context,null, out url);
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append(url);

            Dictionary<string, string> values = new Dictionary<string, string>();
            if (!string.IsNullOrEmpty(MemberGrid.SortExpression))
            {
                values.Add("SortField", MemberGrid.SortExpression);

                switch (MemberGrid.SortDirection)
                {
                    case SortDirection.Descending:
                        values.Add("SortDir", "Desc");
                        break;
                    default:
                        values.Add("SortDir", "Asc");
                        break;
                }
            }

            if (MemberGrid.PageIndex > 0)
            {
                values.Add("PageIndex", MemberGrid.PageIndex.ToString());
            }

            if (!string.IsNullOrEmpty(UserDataSource.SearchText))
            {
                values.Add("k", UserDataSource.SearchText);

            }

            if (values.Count > 0)
            {
                stringBuilder.Append("?");
                bool flag = true;
                foreach (KeyValuePair<string, string> current in values)
                {
                    if (!flag)
                    {
                        stringBuilder.Append("&");
                    }
                    stringBuilder.Append(string.Format(CultureInfo.InvariantCulture, "{0}={1}", new object[]
		                {
			                HttpUtility.UrlEncode(current.Key), 
			                HttpUtility.UrlEncode(current.Value)
		                }));
                    flag = false;
                }
            }

            return stringBuilder.ToString();
        }
        // EndModifiedBySolvion
    }
}