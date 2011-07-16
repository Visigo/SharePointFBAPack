<%@ WebHandler Language="C#" Class="Visigo.Sharepoint.FormsBasedAuthentication.HIP.ImageHipChallengeHandler" %>
using System;
using System.Web;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Collections.Specialized;
using System.Security.Cryptography;

namespace Visigo.Sharepoint.FormsBasedAuthentication.HIP
{
    /// <summary>Provides cryptographically-strong pseudo-random numbers.</summary>
    internal class RandomNumbers
    {
        /// <summary>Random number generator.</summary>
        private RNGCryptoServiceProvider _rand = new RNGCryptoServiceProvider();

        /// <summary>Used by NextRandom and NextRandomDouble.</summary>
        /// <remarks>
        /// RNGCryptoServiceProvider is not thread-safe.  Therefore, we only call to on a single thread.
        /// As a result, we can resuse the same byte arrays for every call.
        /// </remarks>
        private byte[] _rd4 = new byte[4], _rd8 = new byte[8];

        /// <summary>Gets a random non-negative integer less than max.</summary>
        /// <param name="max">The upper-bound for the random number.</param>
        /// <returns>The generated random number.</returns>
        public int Next(int max)
        {
            if (max <= 0) throw new ArgumentOutOfRangeException("max");
            _rand.GetBytes(_rd4);
            int val = BitConverter.ToInt32(_rd4, 0) % max;
            if (val < 0) val = -val;
            return val;
        }

        /// <summary>Gets a random number between min and max, inclusive.</summary>
        /// <param name="min">The minimum possible value.</param>
        /// <param name="max">The maximum possible value.</param>
        /// <returns>The randomly generated number.</returns>
        public int Next(int min, int max)
        {
            if (min > max) throw new ArgumentOutOfRangeException("max");
            return Next(max - min + 1) + min;
        }

        /// <summary>Gets a randomly generated double between 0.0 and 1.1.</summary>
        /// <returns>The random number.</returns>
        public double NextDouble()
        {
            _rand.GetBytes(_rd8);
            return BitConverter.ToUInt64(_rd8, 0) / (double)UInt64.MaxValue;
        }
    }
    
	/// <summary>Handles requests for dynamic images from the ImageHipChallenge control.</summary>
	public class ImageHipChallengeHandler : IHttpHandler
	{
        /// <summary>Default value for the RenderUrl property.</summary>
        private const string RENDERURL_DEFAULT = "ImageHipChallenge.ashx";
        /// <summary>Query string key for the image width.</summary>
        internal const string WIDTH_KEY = "w";
        /// <summary>Query string key for the image height.</summary>
        internal const string HEIGHT_KEY = "h";
        /// <summary>Query string key for challenge ID.</summary>
        internal const string ID_KEY = "id";        
        private RandomNumbers _rand = new RandomNumbers();
        
		/// <summary>Maximum width of an image to generate.</summary>
		private const int MAX_IMAGE_WIDTH = 600;
		/// <summary>Maximum height of an image to generate.</summary>
		private const int MAX_IMAGE_HEIGHT = 600;

		/// <summary>Gets whether this handler is reusable.</summary>
		/// <remarks>This handler is not thread-safe (uses non thread-safe member variables), so it is not reusable.</remarks>
		public bool IsReusable { get { return false; } }

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

        /// <summary>Gets the challenge text for a particular ID.</summary>
        /// <param name="challengeId">The ID of the challenge text to retrieve.</param>
        /// <returns>The text associated with the specified ID; null if no text exists.</returns>
        internal static string GetChallengeText(Guid challengeId)
        {
            HttpContext ctx = HttpContext.Current;
            return (string)ctx.Cache[challengeId.ToString()];
        }
                
		/// <summary>Processes the image request and generates the appropriate image.</summary>
		/// <param name="context">The current HttpContext.</param>
		public void ProcessRequest(HttpContext context)
		{
			// Retrieve query parameters and challenge text
            NameValueCollection queryString = context.Request.QueryString;
            int width = Convert.ToInt32(queryString[WIDTH_KEY]);
            if (width <= 0 || width > MAX_IMAGE_WIDTH) throw new ArgumentOutOfRangeException(WIDTH_KEY);
            int height = Convert.ToInt32(queryString[HEIGHT_KEY]);
            if (height <= 0 || height > MAX_IMAGE_HEIGHT) throw new ArgumentOutOfRangeException(HEIGHT_KEY);
            string text = GetChallengeText(new Guid(queryString[ID_KEY]));

            if (text != null)
            {
                // We successfully retrieved the information, so generate the image and send it to the client.
                HttpResponse resp = context.Response;
                resp.Clear();
                resp.ContentType = "img/jpeg";
                using (Bitmap bmp = GenerateImage(text, new Size(width, height)))
                {
                    bmp.Save(resp.OutputStream, ImageFormat.Jpeg);
                }
            }
		}

		/// <summary>Generates the challenge image.</summary>
		/// <param name="text">The text to be rendered into the image.</param>
		/// <param name="size">The size of the image to generate.</param>
		/// <returns>A dynamically-generated challenge image.</returns>
		public Bitmap GenerateImage(string text, Size size)
		{
			// Create the new Bitmap of the specified size and render to it
			Bitmap bmp = new Bitmap(size.Width, size.Height);
			using (Graphics g = Graphics.FromImage(bmp))
			{
				// Draw the background as a random linear gradient
				using(Brush b = new LinearGradientBrush(
						  new Rectangle(0,0,size.Width,size.Height),
						  Color.FromArgb(NextRandom(256),NextRandom(256),NextRandom(256)),
						  Color.FromArgb(NextRandom(256),NextRandom(256),NextRandom(256)),
						  (float)(NextRandomDouble()*360),false))
				{
					g.FillRectangle(b, 0, 0, bmp.Width, bmp.Height);
				}

				// Select a font family and create the default sized font.  We then need to shrink
				// the font size until the text fits.
				FontFamily ff = _families[NextRandom(_families.Length)];
				int emSize = (int)(size.Width*2 / text.Length);
				Font f = new Font(ff, emSize);
				try
				{
					// Make sure that the font size we have will fit with the selected text.
					SizeF measured = new SizeF(0,0);
					SizeF workingSize = new SizeF(size.Width, size.Height);
					while (emSize > 2 &&
						(measured = g.MeasureString(text, f)).Width > workingSize.Width ||
						measured.Height > workingSize.Height)
					{
						f.Dispose();
						f = new Font(ff, emSize -= 2);
					}

					// Select a color and draw the string into the center of the image
					using(StringFormat fmt = new StringFormat())
					{
						fmt.Alignment = fmt.LineAlignment = StringAlignment.Center;
						using(Brush b = new LinearGradientBrush(
								  new Rectangle(0,0,size.Width/2,size.Height/2),
								  Color.FromArgb(NextRandom(256),NextRandom(256),NextRandom(256)),
								  Color.FromArgb(NextRandom(256),NextRandom(256),NextRandom(256)),
								  (float)(NextRandomDouble()*360),false))
						{
							g.DrawString(text, f, b, new Rectangle(0,0,bmp.Width,bmp.Height), fmt);
						}
					}
				}
				finally
				{
					// Clean up
					f.Dispose();
				}
			}
			
			// Distort the final image and return it.  This distortion amount is fairly arbitrary.
			DistortImage(bmp, NextRandom(5, 10) * (NextRandom(2) == 1 ? 1 : -1) );
			return bmp;
		}

		/// <summary>Distorts the image.</summary>
		/// <param name="b">The image to be transformed.</param>
		/// <param name="distortion">An amount of distortion.</param>
		private static void DistortImage(Bitmap b, double distortion)
		{
			int width = b.Width, height = b.Height;

			// Copy the image so that we're always using the original for source color
			using (Bitmap copy = (Bitmap)b.Clone())
			{
				// Iterate over every pixel
				for (int y = 0; y < height; y++)
				{
					for (int x = 0; x < width; x++)
					{
						// Adds a simple wave
						int newX = (int)(x + (distortion * Math.Sin(Math.PI * y / 64.0)));
						int newY = (int)(y + (distortion * Math.Cos(Math.PI * x / 64.0)));
						if (newX < 0 || newX >= width) newX = 0;
						if (newY < 0 || newY >= height) newY = 0;
						b.SetPixel(x, y, copy.GetPixel(newX, newY));
					}
				}
			}
		}

		/// <summary>List of fonts that can be used for rendering text.</summary>
		/// <remarks>This list can be changed to include any families available on the current system.</remarks>
		private static FontFamily [] _families = { 
													 new FontFamily("Times New Roman")
												 };
	}
}