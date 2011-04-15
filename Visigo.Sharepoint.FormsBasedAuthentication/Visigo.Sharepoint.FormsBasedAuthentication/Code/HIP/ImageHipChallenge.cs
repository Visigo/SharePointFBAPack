using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Visigo.Sharepoint.FormsBasedAuthentication.HIP
{
	/// <summary>A visual Reverse Turing Test (HIP).</summary>
	[ToolboxBitmap(typeof(ImageHipChallenge), "msdn.bmp")]
	[ToolboxData("<{0}:ImageHipChallenge Runat=\"server\" Height=\"100px\" Width=\"300px\" />")]
	public class ImageHipChallenge : HipChallenge
	{
		/// <summary>Default value for the RenderUrl property.</summary>
        private const string RENDERURL_DEFAULT = "/_layouts/FBA/ImageHipChallenge.ashx";
		/// <summary>Query string key for the image width.</summary>
		internal const string WIDTH_KEY = "w";
		/// <summary>Query string key for the image height.</summary>
		internal const string HEIGHT_KEY = "h";
		/// <summary>Query string key for challenge ID.</summary>
		internal const string ID_KEY = "id";

		/// <summary>The dynamically-generated image.</summary>
		private System.Web.UI.WebControls.Image _image;
		/// <summary>Backing store for RenderUrl.</summary>
		private string _renderUrl = RENDERURL_DEFAULT;

		/// <summary>Gets or sets the URL used to render the image to the client.</summary>
		[Category("Behavior")]
		[Description("The URL used to render the image to the client.")]
		[DefaultValue(RENDERURL_DEFAULT)]
		public string RenderUrl 
		{ 
			get { return _renderUrl; } 
			set { _renderUrl = value; } 
		}

		/// <summary>Creates the DynamicImage and HiddenField controls.</summary>
		protected sealed override void CreateChildControls()
		{
			// We're based on BaseValidator/Label, so make sure to render child controls,
			// though most likely no additional controls will be created.
			base.CreateChildControls();

			// Make sure that the size of this control has been properly defined.
			// We need the size in pixels in order to properly generate an image.
			if (this.Width.IsEmpty || this.Width.Type != UnitType.Pixel ||
				this.Height.IsEmpty || this.Height.Type != UnitType.Pixel)
			{
				throw new InvalidOperationException("Must specify size of control in pixels.");
			}

			// Create and configure the dynamic image.  We won't setup the actual
			// Bitmap for it until later.
			_image = new System.Web.UI.WebControls.Image();
			_image.BorderColor = this.BorderColor;
			_image.BorderStyle = this.BorderStyle;
			_image.BorderWidth = this.BorderWidth;
			_image.ToolTip = this.ToolTip;
			_image.EnableViewState = false;
			Controls.Add(_image);
		}

		/// <summary>Render the challenge.</summary>
		/// <param name="id">The ID of the challenge.</param>
		/// <param name="content">The content to render.</param>
		protected sealed override void RenderChallenge(Guid id, string content)
		{
			// Generate the link to the image generation handler
			_image.Width = this.Width;
			_image.Height = this.Height;
			_image.ImageUrl = _renderUrl + "?" + 
				WIDTH_KEY + "=" + (int)Width.Value + "&" + 
				HEIGHT_KEY + "=" + (int)Height.Value + "&" +
				ID_KEY + "=" + id.ToString("N");

		}
	}
}