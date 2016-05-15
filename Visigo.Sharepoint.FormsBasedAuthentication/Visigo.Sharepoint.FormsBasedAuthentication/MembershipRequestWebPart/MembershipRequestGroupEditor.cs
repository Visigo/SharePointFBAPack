using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Microsoft.SharePoint;
using Microsoft.SharePoint.WebControls;

namespace Visigo.Sharepoint.FormsBasedAuthentication 
{
    class MembershipRequestGroupEditor : EditorPart
    {
        DropDownList ddlGroup;

        /// <summary>
        ///     Editor Part Constructor
        /// </summary>
        public MembershipRequestGroupEditor()
        {
            LocalizedString resourceManager;
            resourceManager = new LocalizedString("FBAPackMembershipRequestWebPart");
            this.ID = "GroupEditor";
            this.Title = resourceManager.GetString("GroupEditor_Title");
            this.Description = resourceManager.GetString("GroupEditor_Description");
        }

        /// <summary>
        ///     This is the standard method for adding controls to a web part.
        /// </summary>
        protected override void CreateChildControls()
        {
            ddlGroup = new DropDownList();
            SPWeb web = SPControl.GetContextWeb(Context);
            SPGroupCollection groups = web.SiteGroups;
            for (int idx = 0; idx < groups.Count; idx++)
            {
                ddlGroup.Items.Add(groups[idx].Name);
            }
            this.Controls.Add(ddlGroup);
        }

        /// <summary>
        ///     This method is called when the user applies changes and sets the property
        ///     of the web part.
        /// </summary>
        /// <returns>True</returns>
        public override bool ApplyChanges()
        {
            EnsureChildControls();
            MembershipRequestWebPart reg = this.WebPartToEdit as MembershipRequestWebPart;
            if (reg != null)
            {
                reg.GroupName = ddlGroup.Text;
            }
            return true;
        }

        /// <summary>
        ///     This method is called when the user edits the web part by retrieving the data
        ///     from the web part
        /// </summary>
        public override void SyncChanges()
        {
            EnsureChildControls();
            MembershipRequestWebPart reg = this.WebPartToEdit as MembershipRequestWebPart;
            if (reg != null)
            {
                ddlGroup.Text = reg.GroupName;
            }
        }
    }
}
