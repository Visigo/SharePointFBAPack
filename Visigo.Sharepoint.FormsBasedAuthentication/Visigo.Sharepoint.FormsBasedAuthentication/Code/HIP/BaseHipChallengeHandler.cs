using System;

namespace Visigo.Sharepoint.FormsBasedAuthentication.HIP
{
	/// <summary>Base class for IHttpHandlers serving content for HipChallenge-derived controls.</summary>
	public abstract class BaseHipChallengeHandler
	{
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
	}
}