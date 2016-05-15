using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Resources;
using System.Collections.Specialized;
using System.Reflection;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;
using Visigo.Sharepoint.FormsBasedAuthentication.HIP;
using System.Web.Security;


namespace Visigo.Sharepoint.FormsBasedAuthentication
{
    public class MembershipRequestControl : CreateUserWizard
    {
        enum CharMix { upper, lower, mix }

        /// <summary>
        /// Always disable the created user - enable it when it's approved by MembershipRequest
        /// </summary>
        public override bool DisableCreatedUser
        {
            get
            {
                return true;
            }
        }

        #region Fields
        protected string[] _randCharacters = { "A","B","C","D","E","F","G","H","J","K","L","M","N","P","Q","R","S","T","U","V","W","X","Y","Z",
                "2","3","4","5","6","7","8","9",
                "a","b","c","d","e","f","g","h","j","k","m","n","p","q","r","s","t","u","v","w","x","y","z"};
        
        private string _DefaultGroup = string.Empty;
        #endregion

        #region Controls
        private IEditableTextControl _txtFirstName;
        protected IEditableTextControl txtFirstName
        {
            get {
                if (_txtFirstName == null)
                {
                    _txtFirstName = (IEditableTextControl)this.CreateUserStep.ContentTemplateContainer.FindControl("FirstName");
                }
                return _txtFirstName;
            }
        }
        private IEditableTextControl _txtLastName;
        protected IEditableTextControl txtLastName
        {
            get
            {
                if (_txtLastName == null)
                {
                    _txtLastName = (IEditableTextControl)this.CreateUserStep.ContentTemplateContainer.FindControl("LastName");
                }
                return _txtLastName;
            }
        }
        private ITextControl _lblError;
        protected ITextControl lblError
        {
            get
            {
                if (_lblError == null)
                {
                    _lblError = (ITextControl)this.CreateUserStep.ContentTemplateContainer.FindControl("FBAErrorMessage");
                }
                return _lblError;
            }
        }

        private ITextControl _lblCompleteSuccess;
        protected ITextControl lblCompleteSuccess
        {
            get
            {
                if (_lblCompleteSuccess == null)
                {
                    _lblCompleteSuccess = (ITextControl)this.CompleteStep.ContentTemplateContainer.FindControl("CompleteSuccess");
                }
                return _lblCompleteSuccess;
            }
        }

        #endregion

        #region Properties

        public string FirstName
        {
            get { return txtFirstName.Text; }
            set { txtFirstName.Text = value; }
        }

        public string LastName
        {
            get { return txtLastName.Text; }
            set { txtLastName.Text = value; }
        }

        public string DefaultGroup
        {
            get { return _DefaultGroup; }
            set { _DefaultGroup = value; }
        }

        public bool SPLoginCreatedUser { get; set; }

        #endregion

        #region Rendering Methods


        private void SetHipControl()
        {
            ImageHipChallenge imgHip;

            imgHip = this.CreateUserStep.ContentTemplateContainer.FindControl("HipPicture") as ImageHipChallenge;

            if (imgHip != null)
            {
                StringCollection scWords = new StringCollection();
                string randString = GenerateRandomString(6, CharMix.mix);
                scWords.Add(randString);
                imgHip.Words = scWords;
            }
        }

        protected override void CreateControlHierarchy()
        {
            base.CreateControlHierarchy();
            SetHipControl();
        }
        
        #endregion

        #region Events

        private string GenerateRandomString(int NumAlphs, CharMix Mix)
        {
            Random rGen = new Random();

            int iMix = (int)Mix;

            int p = 0;
            string sPass = "";
            int iCharMax = 32;
            if (iMix > 0) iCharMax = 55;
            for (int x = 0; x < NumAlphs; x++)
            {
                p = rGen.Next(0, iCharMax);
                sPass += _randCharacters[p];
            }
            if (iMix < 1)
            {
                sPass = sPass.ToUpper();
            }
            else if (iMix < 2)
            {
                sPass = sPass.ToLower();
            }
            return sPass;
        }
        #endregion

        protected override void OnContinueButtonClick(EventArgs e)
        {
            base.OnContinueButtonClick(e);

            HttpContext.Current.Response.Redirect(FinishDestinationPageUrl);

        }
        
        protected override void OnCreatedUser(EventArgs e)
        {
            // Note: this doesn't run using the privileges of the anonymous user, so we elevate them
            // Also, you can't use the original Site even with elevated privileges, otherwise it reverts back to anonymous.
            SPSecurity.RunWithElevatedPrivileges(delegate()
            {
                using (SPSite site2 = new SPSite(SPContext.Current.Site.ID, SPContext.Current.Site.Zone))
                {
                    using (SPWeb web2 = site2.OpenWeb(SPContext.Current.Web.ID))
                    {
                        // from this point allowunsafeupdates is required because the call is initiated from a browser with
                        // anonymouse rights only
                        web2.AllowUnsafeUpdates = true;

                        MembershipRequest request = new MembershipRequest();
                        request.UserEmail = this.Email;
                        request.UserName = this.UserName;
                        if (System.Web.Security.Membership.RequiresQuestionAndAnswer)
                        {
                            request.PasswordQuestion = this.Question;
                            request.PasswordAnswer = this.Answer;
                        }
                        request.FirstName = this.FirstName;
                        request.LastName = this.LastName;
                        request.DefaultGroup = this._DefaultGroup;
                        
                        request.SiteName = web2.Title;
                        request.SiteURL = web2.Url;

                        MembershipSettings settings = new MembershipSettings(web2);

                        if (settings.ReviewMembershipRequests)
                        {
                            request.LoginCreatedUser = false;
                            
                            if (!MembershipRequest.CopyToReviewList(request))
                            {
                                lblError.Text = this.UnknownErrorMessage;
                                return;
                            }
                            
                        }
                        else
                        {
                            #region Process new user request if we're NOT using the Request List

                            if (!AutoGeneratePassword)
                            {
                                request.Password = this.Password;
                            }

                            request.ChangePasswordURL = Utils.GetAbsoluteURL(web2, settings.ChangePasswordPage);
                            request.LoginCreatedUser = SPLoginCreatedUser;

                            try
                            {
                                MembershipRequest.ApproveMembership(request, web2);
                            }
                            catch (Exception ex)
                            {
                                Utils.LogError(ex);
                                this.lblCompleteSuccess.Text = this.UnknownErrorMessage;
                                return;
                            }


                            #endregion
                            
                        }
                        this.MoveTo(this.CompleteStep);
                    }
                }
            });
        }

    }
}
