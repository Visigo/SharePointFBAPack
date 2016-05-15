using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

namespace Visigo.Sharepoint.FormsBasedAuthentication.HIP
{
	/// <summary>Validation control used in conjunction with an HipChallenge.</summary>
	[ToolboxBitmap(typeof(HipValidator), "msdn.bmp")]
	[ToolboxData("<{0}:HipValidator Runat=\"server\" ErrorMessage=\"*\" />")]
	public class HipValidator : BaseValidator
	{
		/// <summary>Backing store for HipChallenge.</summary>
		private string _hipChallenge;

		/// <summary>Gets or sets the ID of the associated HipChallenge.</summary>
		/// <value>The ID of the associated HipChallenge.</value>
		[TypeConverter(typeof(HipChallengeControlConverter))]
		[Category("Behavior")]
		public string HipChallenge
		{
			get { return _hipChallenge; }
			set { _hipChallenge = value; }
		}

		/// <summary>Gets the associated HipChallenge on the page.</summary>
		/// <value>The associated HipChallenge on the page.</value>
		private HipChallenge AssociatedChallenge
		{
			get
			{
				if (HipChallenge == null || HipChallenge.Trim().Length == 0) throw new InvalidOperationException("No challenge control specified.");
				HipChallenge hip = NamingContainer.FindControl(HipChallenge) as HipChallenge;
				if (hip == null) throw new InvalidOperationException("Could not find challenge control.");
				return hip;
			}
		}
	
		/// <summary>Determines whether this validator's source is valid.</summary>
		/// <returns>Whether the validated source is valid.</returns>
		protected override bool EvaluateIsValid()
		{
			// Get the validated control and its value.  If we can get a value, see if
			// it authenticates with the associated HipChallenge.
			string controlName = base.ControlToValidate;
			if (controlName != null)
			{
				string controlValue = base.GetControlValidationValue(controlName);
				if (controlValue != null && ((controlValue = controlValue.Trim()).Length > 0))
				{
					return AssociatedChallenge.Authenticate(controlValue);
				}
			}
			return false;
		}

		/// <summary>TypeConverter used to present developer with list of HipChallenge controls on page.</summary>
		private class HipChallengeControlConverter : ValidatedControlConverter
		{
			/// <summary>Gets a list of all HipChallenge components on the page.</summary>
			/// <param name="container">The IContainer from the designer.</param>
			/// <returns>An array of HipChallenge control IDs.</returns>
			private object[] GetControls(IContainer container)
			{
				ArrayList list = new ArrayList();
				foreach(IComponent comp in container.Components)
				{
					HipChallenge hip = comp as HipChallenge;
					if (hip != null)
					{
						if (hip.ID != null && hip.ID.Trim().Length > 0) list.Add(hip.ID);
					}
				}
				list.Sort(Comparer.Default);
				return list.ToArray();
			}

			/// <summary>Returns a standard collection of values based on the type descriptor context.</summary>
			/// <param name="context">The context.</param>
			/// <returns>A collection of HipChallenge control IDs.</returns>
			public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
			{
				if (context == null || context.Container == null) return null;
				object [] controls = GetControls(context.Container);
				if (controls != null) return new StandardValuesCollection(controls);
				return null;
			}
		}
	}
}