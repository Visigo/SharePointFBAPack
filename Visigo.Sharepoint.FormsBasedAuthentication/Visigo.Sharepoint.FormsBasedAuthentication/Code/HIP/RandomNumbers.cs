using System;
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
		private byte [] _rd4 = new byte[4], _rd8 = new byte[8];

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
}