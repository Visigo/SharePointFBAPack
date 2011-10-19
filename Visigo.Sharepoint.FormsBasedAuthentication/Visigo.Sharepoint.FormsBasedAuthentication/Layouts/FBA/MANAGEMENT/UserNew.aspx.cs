using System;
using System.Web.Security;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using Microsoft.SharePoint.Administration;
using System.Web.UI.WebControls;
using Microsoft.SharePoint.Utilities;
using System.Web;

namespace Visigo.Sharepoint.FormsBasedAuthentication
{
    /// <summary>
    /// Code behind for UserNew.aspx
    /// </summary>
    public partial class UserNew : LayoutsPageBase
    {

        protected override bool RequireSiteAdministrator
        {
            get { return true; }
        }

        protected override void OnLoad(EventArgs e)
        {
            this.CheckRights();

            bool _showRoles = (new MembershipSettings(SPContext.Current.Web)).EnableRoles;

            ReqValEmailSubject.Enabled = emailUser.Checked;

            if (!Page.IsPostBack)
            {
                try
                {
                    // if roles activated display roles
                    if (_showRoles)
                    {
                        RolesSection.Visible = true;
                        GroupSection.Visible = false;

                        // load roles
                        rolesList.DataSource = Roles.GetAllRoles();
                        rolesList.DataBind();
                    }
                    // otherwise display groups
                    else
                    {
                        GroupSection.Visible = true;
                        RolesSection.Visible = false;

                        // load groups
                        groupList.DataSource = this.Web.SiteGroups;
                        groupList.DataBind();
                    }

                    // Display Question and answer if required by provider
                    if (System.Web.Security.Membership.RequiresQuestionAndAnswer)
                    {
                        QuestionSection.Visible = true;
                        AnswerSection.Visible = true;
                    }
                    else
                    {
                        QuestionSection.Visible = false;
                        AnswerSection.Visible = false;
                    }
                }
                catch (Exception ex)
                {
                    Utils.LogError(ex, true);
                }
            }
        }

        protected void OnSubmit(object sender, EventArgs e)
        {
            bool _showRoles = (new MembershipSettings(SPContext.Current.Web)).EnableRoles;

            // check to see if username already in use
            MembershipUser user = Utils.GetUser(txtUsername.Text);
            
            if (user == null)
            {
                try
                {
                    // get site reference             
                    SPIisSettings settings = Utils.GetFBAIisSettings(this.Site);
                    string provider = settings.FormsClaimsAuthenticationProvider.MembershipProvider;

                    // create FBA database user
                    MembershipCreateStatus createStatus;

                    if (Membership.RequiresQuestionAndAnswer)
                    {
                        user = Membership.CreateUser(txtUsername.Text, txtPassword.Text, txtEmail.Text, txtQuestion.Text, txtAnswer.Text, isActive.Checked, null, out createStatus);
                    }
                    else
                    {
                        user = Membership.CreateUser(txtUsername.Text, txtPassword.Text, txtEmail.Text, null, null, isActive.Checked, null, out createStatus);
                    }


                    if (createStatus != MembershipCreateStatus.Success)
                    {
                        lblMessage.Text = GetErrorMessage(createStatus);
                        return;
                    }

                    if (user == null)
                    {
                        lblMessage.Text = "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
                        return;
                    }

                    bool groupAdded = false;

                    if (_showRoles)
                    {
                        for (int i = 0; i < rolesList.Items.Count; i++)
                        {
                            if (rolesList.Items[i].Selected)
                            {
                                Roles.AddUserToRole(user.UserName, rolesList.Items[i].Value);
                            }
                        }

                        // add user to SharePoint whether a role was selected or not
                        AddUserToSite(Utils.EncodeUsername(user.UserName), user.Email, txtFullName.Text);
                    }
                    else
                    {
                        // add user to each group that was selected
                        for (int i = 0; i < groupList.Items.Count; i++)
                        {
                            if (groupList.Items[i].Selected)
                            {
                                // add user to group
                                SPGroup group = this.Web.SiteGroups[groupList.Items[i].Value];
                                group.AddUser(
                                    Utils.EncodeUsername(user.UserName),
                                    user.Email,
                                    txtFullName.Text,
                                    "");

                                // update
                                group.Update();
                                groupAdded = true;
                            }
                        }

                        // if no group selected, add to site with no permissions
                        if (!groupAdded)
                        {
                            AddUserToSite(Utils.EncodeUsername(user.UserName), user.Email, txtFullName.Text);
                        }
                    }

                    // Email User
                    if ((emailUser.Checked == true))
                    {
                        //InputFormTextBox txtEmailSubject = (InputFormTextBox)emailUser.FindControl("txtEmailSubject");
                        //InputFormTextBox txtEmailBody = (InputFormTextBox)emailUser.FindControl("txtEmailBody");
                        if ((!string.IsNullOrEmpty(txtEmailSubject.Text)) && (!string.IsNullOrEmpty(txtEmailBody.Text)))
                            Email.SendEmail(this.Web, user.Email, txtEmailSubject.Text, txtEmailBody.Text);
                    }

                    // redirect back to display page
                    SPUtility.Redirect("FBA/Management/UsersDisp.aspx", SPRedirectFlags.RelativeToLayoutsPage, HttpContext.Current);
                }
                catch (Exception ex)
                {
                    Utils.LogError(ex, true);
                }
            }
            else
            {
                lblMessage.Text = "User Already Exists";
            }
        }

        protected string GetErrorMessage(MembershipCreateStatus status)
        {
            switch (status)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "Username already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A username for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }

        /// <summary>
        /// Adds a user to the SharePoint (in no particular group)
        /// </summary>
        /// <param name="login"></param>
        /// <param name="email"></param>
        /// <param name="fullname"></param>
        private void AddUserToSite(string login, string email, string fullname)
        {
            this.Web.AllUsers.Add(
                login,
                email,
                fullname,
                "");
        }
    }
}
