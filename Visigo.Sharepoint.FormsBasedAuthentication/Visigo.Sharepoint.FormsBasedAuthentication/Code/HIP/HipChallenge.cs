using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.Security;

namespace Visigo.Sharepoint.FormsBasedAuthentication.HIP
{
	/// <summary>The base class for all Reverse Turing Test challenges.</summary>
	public abstract class HipChallenge : WebControl, INamingContainer
	{
		/// <summary>Default value for AlwaysGenerateNewChallenge.</summary>
		private const bool ALWAYS_GEN_DEFAULT = true;
		/// <summary>Default value for Expiration.</summary>
		private const int EXPIRATION_DEFAULT = 120;

		/// <summary>Stores the challenge ID associated for future requests.</summary>
		private HtmlInputHidden _hiddenData;
		/// <summary>The list of words that can be rendered.</summary>
		private StringCollection _words;

		/// <summary>Whether Authenticate has previously succeeded in this HttpRequest.</summary>
		private bool _authenticated;

		/// <summary>Backing store for Expiration.</summary>
		private int _expiration = EXPIRATION_DEFAULT;

		/// <summary>Random number generator used to create the HIP.</summary>
		private RandomNumbers _rand = new RandomNumbers();

		/// <summary>Gets a random non-negative integer less than max.</summary>
		/// <param name="max">The upper-bound for the random number.</param>
		/// <returns>The generated random number.</returns>
		protected int NextRandom(int max) { return _rand.Next(max); }

		/// <summary>Gets a random number between min and max, inclusive.</summary>
		/// <param name="min">The minimum possible value.</param>
		/// <param name="max">The maximum possible value.</param>
		/// <returns>The randomly generated number.</returns>
		protected int NextRandom(int min, int max) { return _rand.Next(min, max); }

		/// <summary>Gets a randomly generated double between 0.0 and 1.1.</summary>
		/// <returns>The random number.</returns>
		protected double NextRandomDouble() { return _rand.NextDouble(); }

		/// <summary>Gets or sets the duration of time (seconds) a user has before the challenge expires.</summary>
		/// <value>The duration of time (seconds) a user has before the challenge expires.</value>
		[Category("Behavior")]
		[Description("The duration of time (seconds) a user has before the challenge expires.")]
		[DefaultValue(EXPIRATION_DEFAULT)]
		public int Expiration
		{
			get { return _expiration; }
			set { _expiration = value; }
		}

		/// <summary>Gets the list of words the control can use to create challenges.</summary>
		/// <value>The list of words the control can use to create images.</value>
		[EditorBrowsable(EditorBrowsableState.Never)]
		public StringCollection Words
		{
			get
			{
				if (_words == null) _words = new StringCollection();
				return _words;
			}
            set
            {
                _words = value;
            }
		}

		/// <summary>Selects a word to use in an image.</summary>
		/// <returns>The word to use in the challenge.</returns>
		protected virtual string ChooseWord()
		{
			if (Words.Count == 0) throw new InvalidOperationException("No words available for challenge.");

			return Words[NextRandom(Words.Count)];
		}		
		
		/// <summary>Creates the hidden field control that stores the challenge ID.</summary>
		protected override void CreateChildControls()
		{
			_hiddenData = new HtmlInputHidden();
			_hiddenData.EnableViewState = false;
			Controls.Add(_hiddenData);
			base.CreateChildControls();
		}

		/// <summary>Generates a new image and fills in the dynamic image and hidden field appropriately.</summary>
		/// <param name="e">Ignored.</param>
		protected sealed override void OnPreRender(EventArgs e)
		{
			// Gets a word for the challenge, associates it with a new ID, and stores it for the client
			string content = ChooseWord();
			Guid id = Guid.NewGuid();

			SetChallengeText(id, content, DateTime.Now.AddSeconds(Expiration));
			_hiddenData.Value = id.ToString("N");

			// Generates a challenge based on the selected word/phrase.
			RenderChallenge(id, content);
			
			base.OnPreRender(e);
		}

		/// <summary>Gets the challenge text for a particular ID.</summary>
		/// <param name="challengeId">The ID of the challenge text to retrieve.</param>
		/// <returns>The text associated with the specified ID; null if no text exists.</returns>
		internal static string GetChallengeText(Guid challengeId)
		{
			HttpContext ctx = HttpContext.Current;
			return (string)ctx.Cache[challengeId.ToString()];
		}

		/// <summary>Sets the challenge text for a particular ID.</summary>
		/// <param name="challengeId">The ID of the challenge with which this text should be associated.</param>
		/// <param name="text">The text to store along with the challenge ID.</param>
		/// <param name="expiration">The expiration date fo the challenge.</param>
		internal static void SetChallengeText(Guid challengeId, string text, DateTime expiration)
		{
			HttpContext ctx = HttpContext.Current;
			if (text == null) ctx.Cache.Remove(challengeId.ToString());
			else ctx.Cache.Insert(challengeId.ToString(), text, null, expiration, System.Web.Caching.Cache.NoSlidingExpiration);
		}

		/// <summary>Authenticates user-supplied data against that retrieved using the challenge ID.</summary>
		/// <param name="userData">The user-supplied data.</param>
		/// <returns>Whether the user-supplied data matches that retrieved using the challenge ID.</returns>
		internal bool Authenticate(string userData)
		{
			// We want to allow multiple authentication requests within the same HTTP request,
			// so we can the result as a member variable of the class (non-static)
			if (_authenticated == true) return _authenticated;

			// If no authentication has happened previously, and if the user has supplied text,
			// and if the ID is stored correctly in the page, and if the user text matches the challenge text,
			// then set the challenge text, note that we've authenticated, and return true.  Otherwise, failed authentication.
			if (userData != null && userData.Length > 0 &&
				_hiddenData.Value != null && _hiddenData.Value.Length > 0)
			{
				try
				{
					Guid id = new Guid(_hiddenData.Value);
					string text = GetChallengeText(id);
					if (text != null && string.Compare(userData, text, true) == 0)
					{
						_authenticated = true;
						SetChallengeText(id, null, DateTime.MinValue);
						return true;
					}
				} 
				catch(FormatException){}
			}
			return false;
		}

		/// <summary>Generats the challenge and presents it to the user.</summary>
		/// <param name="id">The ID of the challenge.</param>
		/// <param name="content">The content to render.</param>
		protected abstract void RenderChallenge(Guid id, string content);
	}
}