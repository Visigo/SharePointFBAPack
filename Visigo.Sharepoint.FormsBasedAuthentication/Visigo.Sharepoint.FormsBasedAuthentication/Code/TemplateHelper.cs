using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Visigo.Sharepoint.FormsBasedAuthentication
{
    class TemplateHelper
    {
        private Control _templateContainer;

        public TemplateHelper(Control templateContainer)
        {
            _templateContainer = templateContainer;
        }

        public void SetText(string controlID, string text)
        {
            ITextControl control = _templateContainer.FindControl(controlID) as ITextControl;

            if (control != null)
            {
                control.Text = text;
            }
        }

        public void SetValidation(string controlID, string errorMessage, string validationGroup)
        {
            BaseValidator control = _templateContainer.FindControl(controlID) as BaseValidator;

            if (control != null)
            {
                control.ErrorMessage = errorMessage;
                control.ValidationGroup = validationGroup;
            }
        }

        public void SetVisible(string controlID, bool visible)
        {
            Control control = _templateContainer.FindControl(controlID) as Control;

            if (control != null)
            {
                control.Visible = visible;
            }
        }

        public void SetButton(string controlID, string text, string validationGroup)
        {
            IButtonControl control = _templateContainer.FindControl(controlID) as IButtonControl;

            if (control != null)
            {
                control.Text = text;
                control.ValidationGroup = validationGroup;
            }
        }

        public void SetImage(string controlID, string imageUrl, string altText, bool visibleIfBlank)
        {
            Image control = _templateContainer.FindControl(controlID) as Image;

            if (control != null)
            {
                if (String.IsNullOrEmpty(imageUrl) && !visibleIfBlank)
                {
                    control.Visible = false;
                }
                control.ImageUrl = imageUrl;
                control.AlternateText = altText;
            }
        }

        public void SetImageButton(string controlID, string imageUrl, string altText, string validationGroup)
        {
            this.SetImage(controlID, imageUrl, altText, true);

            this.SetButton(controlID, "", validationGroup);
        }

        public void SetLink(string controlID, string text, string url)
        {
            HyperLink control = _templateContainer.FindControl(controlID) as HyperLink;

            if (control != null)
            {
                control.Text = text;
                control.NavigateUrl = url;
            }
        }

        public void SetCSS(string controlID, string cssClass)
        {
            WebControl control = _templateContainer.FindControl(controlID) as WebControl;

            if (control != null)
            {
                control.CssClass = cssClass;
            }
        }
    }
}
