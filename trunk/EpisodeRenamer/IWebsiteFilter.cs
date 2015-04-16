using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EpisodeRenamer
{
	/// <summary>
	/// Provides methods to filter episode information out of websites.
	/// </summary>
	interface IWebsiteFilter : IEnumerable<string>
	{
		/// <summary>
		/// Gets the URL to retrieve episode information from.
		/// </summary>
		string URL {
			get;
		}

		/// <summary>
		/// Gets a value indicating, whether episode information has been filtered from the website.
		/// </summary>
		bool Filtered {
			get;
		}

		/// <summary>
		/// Gets the number of episode names present in this instance.
		/// </summary>
		int Count {
			get;
		}

		/// <summary>
		/// Gets an episode name from the list.
		/// </summary>
		/// <param name="idx">Zero-based index of the element to retrieve.</param>
		/// <returns>The episode name at the specified position.</returns>
		string this[int idx] {
			get;
		}

		/// <summary>
		/// Filters any episode information from the URL set for this instance.
		/// </summary>
		/// <returns>The number of episode names filtered.</returns>
		int Filter();
	}
}
