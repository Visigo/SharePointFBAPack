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

        #region Fields
        protected string[] _randCharacters = { "A","B","C","D","E","F","G","H","J","K","L","M","N","P","Q","R","S","T","U","V","W","X","Y","Z",
                "2","3","4","5","6","7","8","9",
                "a","b","c","d","e","f","g","h","j","k","m","n","p","q","r","s","t","u","v","w","x","y","z"};
        private string _HipInstructionsLabelText = string.Empty;
        private string _HipPictureLabelText = string.Empty;
        private string _HipPictureDescription = string.Empty;
        private string _HipCharactersLabelText = string.Empty;
        private string _HipErrorMessage = string.Empty;
        private string _HipResetLabelText = string.Empty;
        private string _FirstNameLabelText = String.Empty;
        private string _LastNameLabelText = string.Empty;
        private string _FirstNameRequiredErrorMessage = String.Empty;
        private string _LastNameRequiredErrorMessage = String.Empty;
        private string _DefaultGroup = string.Empty;
        #endregion

        #region Controls
        //HIP controls
        protected ImageHipChallenge imgHip;
        protected HipValidator vldHip;
        protected TextBox txtHip;
        protected Label lblHipPicture;
        protected Label lblHipCharacters;
        protected Label lblHipInstructions;
        protected Label lblHipPictureDescription;
        protected LinkButton lnkHip;
        protected TextBox txtFirstName;
        protected TextBox txtLastName;
        #endregion

        #region Properties
        public string HipInstructionsLabelText
        {
            get { return _HipInstructionsLabelText; }
            set { _HipInstructionsLabelText = value; }
        }

        public string HipPictureLabelText
        {
            get { return _HipPictureLabelText; }
            set { _HipPictureLabelText = value; }
        }

        public string HipPictureDescription
        {
            get { return _HipPictureDescription; }
            set { _HipPictureDescription = value; }
        }

        public string HipCharactersLabelText
        {
            get { return _HipCharactersLabelText; }
            set { _HipCharactersLabelText = value; }
        }

        public string HipErrorMessage
        {
            get { return _HipErrorMessage; }
            set { _HipErrorMessage = value; }
        }

        public string HipResetLabelText
        {
            get { return _HipResetLabelText; }
            set { _HipResetLabelText = value; }
        }
        public string FirstNameLabelText
        {
            get { return _FirstNameLabelText; }
            set { _FirstNameLabelText = value; }
        }

        public string LastNameLabelText
        {
            get { return _LastNameLabelText; }
            set { _LastNameLabelText = value; }
        }

        public string FirstNameRequiredErrorMessage
        {
            get { return _FirstNameRequiredErrorMessage; }
            set { _FirstNameRequiredErrorMessage = value; }
        }

        public string LastNameRequiredErrorMessage
        {
            get { return _LastNameRequiredErrorMessage; }
            set { _LastNameRequiredErrorMessage = value; }
        }

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
        private Table GetCreateUserTable()
        {
            Table tblResult = null;
            Table outertbl = this.CreateUserStep.ContentTemplateContainer.Controls[0] as Table;
            if (outertbl != null)
            {
                Table innertbl = outertbl.Rows[0].Cells[0].Controls[0] as Table;
                if (innertbl != null)
                {
                    tblResult = innertbl;
                }
            }
            return tblResult;
        }

        /// <summary>
        ///     Adds a field and label to the CreateUserWizard control
        /// </summary>
        /// <param name="prompt"></param>
        /// <param name="controlName"></param>
        /// <param name="requiredValuePrompt"></param>
        /// <returns></returns>
        private TableRow AddField(string prompt, Control ctrl, string requiredValuePrompt)
        {
            TableRow tr1 = new TableRow();
            TableCell td1 = new TableCell();
            TableCell td2 = new TableCell();
            Label lblPrompt = new Label();
            TextBox txtField = ctrl as TextBox;
            lblPrompt.Text = prompt;
            lblPrompt.AssociatedControlID = ctrl.ID;
            td1.Controls.Add(lblPrompt);
            td1.HorizontalAlign = HorizontalAlign.Right;
            td2.Controls.Add(txtField);
            tr1.Cells.Add(td1);
            tr1.Cells.Add(td2);
            if (requiredValuePrompt != null)
            {
                RequiredFieldValidator rfv = new RequiredFieldValidator();
                rfv.ControlToValidate = txtField.ID;
                rfv.ID = "rfv" + txtField.ID;
                rfv.ValidationGroup = this.ID;
                rfv.Text = requiredValuePrompt;
                td2.Controls.Add(rfv);
            }
            return tr1;
        }

        private void AddHipControls()
        {
            Table cuwTable = GetCreateUserTable();
            TableRow tr;
            TableCell td;
            TableCell td2;

            lblHipInstructions = new Label();
            lblHipInstructions.Text = HipInstructionsLabelText;

            tr = new TableRow();
            td = new TableCell();
            td2 = new TableCell();
            td2.HorizontalAlign = HorizontalAlign.Center;
            td2.Controls.Add(lblHipInstructions);
            tr.Cells.Add(td);
            tr.Cells.Add(td2);
            cuwTable.Rows.AddAt(11, tr);

            lblHipPicture = new Label();
            lblHipPicture.Text = HipPictureLabelText;
            StringCollection scWords = new StringCollection();
            string randString = GenerateRandomString(6, CharMix.mix);
            scWords.Add(randString);
            imgHip = new ImageHipChallenge();
            imgHip.ID = "imgHip";
            imgHip.Width = Unit.Pixel(210);
            imgHip.Height = Unit.Pixel(70);
            imgHip.Words = scWords;

            tr = new TableRow();
            td = new TableCell();
            td2 = new TableCell();
            td.HorizontalAlign = HorizontalAlign.Right;
            td2.HorizontalAlign = HorizontalAlign.Left;
            td.Controls.Add(lblHipPicture);
            td2.Controls.Add(imgHip);
            tr.Cells.Add(td);
            tr.Cells.Add(td2);
            cuwTable.Rows.AddAt(12, tr);

            lblHipPictureDescription = new Label();
            lblHipPictureDescription.Text = HipPictureDescription;

            tr = new TableRow();
            td = new TableCell();
            td2 = new TableCell();
            td2.HorizontalAlign = HorizontalAlign.Center;
            td.HorizontalAlign = HorizontalAlign.Right;
            td2.Controls.Add(lblHipPictureDescription);
            tr.Cells.Add(td);
            tr.Cells.Add(td2);
            cuwTable.Rows.AddAt(13, tr);

            txtHip = new TextBox();
            txtHip.ID = "txtHip";
            txtHip.MaxLength = 20;
            lblHipCharacters = new Label();
            lblHipCharacters.Text = HipCharactersLabelText;
            vldHip = new HipValidator();
            vldHip.ID = "vldHip";
            vldHip.Display = ValidatorDisplay.Static;
            vldHip.ControlToValidate = txtHip.ID;
            vldHip.ValidationGroup = this.ID;
            vldHip.HipChallenge = imgHip.ID;
            vldHip.Text = HipErrorMessage;
            vldHip.ErrorMessage = HipErrorMessage;

            tr = new TableRow();
            td = new TableCell();
            td2 = new TableCell();
            td.HorizontalAlign = HorizontalAlign.Right;
            td2.HorizontalAlign = HorizontalAlign.Left;
            td.Controls.Add(lblHipCharacters);
            td2.Controls.Add(txtHip);
            td2.Controls.Add(vldHip);
            tr.Cells.Add(td);
            tr.Cells.Add(td2);
            cuwTable.Rows.AddAt(14, tr);

            lnkHip = new LinkButton();
            lnkHip.Click += new EventHandler(lnkHip_Click);
            lnkHip.Text = HipResetLabelText;
            lnkHip.CausesValidation = false;

            tr = new TableRow();
            td = new TableCell();
            td2 = new TableCell();
            td2.HorizontalAlign = HorizontalAlign.Center;
            td2.Controls.Add(lnkHip);
            tr.Cells.Add(td);
            tr.Cells.Add(td2);
            cuwTable.Rows.AddAt(15, tr);
        }

        private void AddNameFields()
        {
            //Add Extra Fields
            txtFirstName = new TextBox();
            txtFirstName.ID = "txtFirstName";
            txtLastName = new TextBox();
            txtLastName.ID = "txtLastName";
            TableRow tr1 = AddField(FirstNameLabelText, txtFirstName, FirstNameRequiredErrorMessage);
            TableRow tr2 = AddField(LastNameLabelText, txtLastName, LastNameRequiredErrorMessage);
            Table cuwTable = GetCreateUserTable();
            cuwTable.Rows.AddAt(3, tr1);
            cuwTable.Rows.AddAt(4, tr2);
        }

        protected override void CreateControlHierarchy()
        {
            base.CreateControlHierarchy();
            AddNameFields();
            AddHipControls();
            this.CreateUserStep.Title = ""; // This needs to be a config item
        }

        protected override void OnCreatingUser(LoginCancelEventArgs e)
        {
            SPWeb web = SPContext.Current.Web;
            MembershipSettings settings = new MembershipSettings(web);

            if (settings.ReviewMembershipRequests)
            {
                /* bms Prevent user from being added to the list multiple times if the user */
                /* is already in use.                                                       */
                if (Utils.GetUser(this.UserName, web.Site) == null)
                {
                    MembershipRequest request = new MembershipRequest();
                    request.UserEmail = this.Email;
                    request.UserName = this.UserName;
                    request.PasswordQuestion = this.Question;
                    request.PasswordAnswer = this.Answer;
                    request.FirstName = this.FirstName;
                    request.LastName = this.LastName;
                    request.DefaultGroup = this._DefaultGroup;
                    request.LoginCreatedUser = SPLoginCreatedUser;
                    MembershipRequest.CopyToReviewList(request);
                }
                this.MoveTo(this.CompleteStep);
            }
            else
            {
                base.OnCreatingUser(e);
                //Add the user to the default group
                // Note: this doesn't run using the privileges of the anonymous user, so we elevate them
                // Also, you can't use the original RootWeb even with elevated privileges, otherwise it reverts back to anonymous.

                //This is done in approvemembership - so not sure why it's being done here before the user is actually created
                //SPSecurity.RunWithElevatedPrivileges(delegate()
                //{
                //    using (SPSite site2 = new SPSite(this.Page.Request.Url.ToString()))
                //    {
                //        using (SPWeb web2 = site2.RootWeb)
                //        {
                //            web2.AllowUnsafeUpdates = true;
                //            web2.Groups[this._DefaultGroup].AddUser(Utils.EncodeUsername(this.UserName.ToLower()), this.Email, this.FirstName + " " + this.LastName, "Self Registration");
                //            web2.Update();
                //        }
                //    }
                //});
            }
        }

        #endregion

        #region Events
        void lnkHip_Click(object sender, EventArgs e)
        {
            //Nothing to do, the image will reset automatically
        }

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
            SPWeb web = SPContext.Current.Web;
            MembershipSettings settings = new MembershipSettings(web);

            if (!settings.ReviewMembershipRequests)
            {
                #region Process new user request if we're NOT using the Request List
                // Note: this doesn't run using the privileges of the anonymous user, so we elevate them
                // Also, you can't use the original Site even with elevated privileges, otherwise it reverts back to anonymous.
                SPSecurity.RunWithElevatedPrivileges(delegate()
                {
                    using (SPSite site2 = new SPSite(SPContext.Current.Site.ID, SPContext.Current.Site.Zone))
                    {
                        using (SPWeb web2 = site2.RootWeb)
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
                            request.SiteName = web2.Title;
                            request.SiteURL = web2.Url;
                            request.ChangePasswordURL = string.Format("{0}/{1}", web2.Url, settings.ChangePasswordPage);
                            request.DefaultGroup = this.DefaultGroup;
                            request.LoginCreatedUser = SPLoginCreatedUser;

                            MembershipRequest.ApproveMembership(request, web2);

                            this.MoveTo(this.CompleteStep);
                        }
                    }
                });
                #endregion
            }
            else
            {
                base.OnCreatedUser(e);
            }
        }
         

    }
}
