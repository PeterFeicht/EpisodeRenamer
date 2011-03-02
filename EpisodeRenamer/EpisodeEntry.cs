using System.IO;
using System;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Text;

namespace EpisodeRenamer
{
	class EpisodeEntry
	{
		#region types, fields, ctors

		/// <summary>
		/// Specifies the type of entry.
		/// </summary>
		public enum EntryType
		{
			/// <summary>
			/// Neither an old nor a new filename have been set.
			/// </summary>
			None,
			/// <summary>
			/// The old filenme does not contain an episode name, but contains season and episode information and a new filename is set.
			/// </summary>
			Green,
			/// <summary>
			/// The old filename does not contain season or episode information but a new filename is set.
			/// </summary>
			Red,
			/// <summary>
			/// The old filename contains season and episode information as well as an episode name and a new name is set.
			/// </summary>
			Yellow,
			/// <summary>
			/// No new episode name is set.
			/// </summary>
			Gray
		}

		string oldFilename = "";
		string newFilename = "";
		EntryType typeCache;
		bool typeCacheDirty = true;

		/// <summary>
		/// Initializes a new instance with <see cref="Enabled"/> set to <value>false</value> and empty filenames.
		/// </summary>
		private EpisodeEntry()
		{
			Enabled = false;
			NewNameString = "";
			EpisodeNumber = new Point(-1, -1);
			Moved = false;
			FilePath = "";
			MoveException = null;
		}

		/// <summary>
		/// Initializes a new instance with <see cref="Enabled"/> set to <value>false</value>, the old filename specified and an empty new filename.
		/// </summary>
		/// <param name="oldFilename">The old filename to use for this instance.</param>
		public EpisodeEntry(string oldFilename)
			: this()
		{
			OldFilename = oldFilename;
		}

		/// <summary>
		/// Initializes a new instance with <see cref="Enabled"/> set to <value>true</value> and the old and new filenames specified.
		/// </summary>
		/// <param name="oldFilename">The old filename to use for this instance</param>
		/// <param name="newFilename">The new filename to use for this instance</param>
		public EpisodeEntry(string oldFilename, string newFilename)
			: this(oldFilename, newFilename, true)
		{
		}

		/// <summary>
		/// Initializes a new instance with <see cref="Enabled"/> and the filenames set to the values specified.
		/// </summary>
		/// <param name="oldFilename">The old filename to use for this instance</param>
		/// <param name="newFilename">The new filename to use for this instance</param>
		/// <param name="enabled">The value to use for <see cref="Enabled"/>.</param>
		public EpisodeEntry(string oldFilename, string newFilename, bool enabled)
			: this()
		{
			OldFilename = oldFilename;
			NewFilename = newFilename;
			Enabled = enabled;
		}

		#endregion types, fields, ctors


		#region properties

		/// <summary>
		/// Gets or sets the old filename, must be an absolute path that exists.
		/// </summary>
		/// <exception cref="System.ArgumentException">Thrown, if the specified file does not exist.</exception>
		/// <remarks>No exception is thrown if the path supplied is not absolute. However, there is no guarantee on which working directory 
		/// will be used to calculate the absolute path.</remarks>
		public string OldFilename
		{
			get
			{
				return oldFilename;
			}
			set
			{
				if(!File.Exists(value))
					throw new ArgumentException("The file specified does not exist.");

				oldFilename = Path.GetFileName(value);
				FilePath = Path.GetDirectoryName(Path.GetFullPath(value));
				typeCacheDirty = true;

				string[ ] parts = SplitFilename(oldFilename);

				if(parts.Length >= 2)
				{
					Series = parts[0];

					if(parts.Length > 2)
						for(int j = 2; j < parts.Length; j++)
							parts[1] += " " + parts[j];
					
					EpisodeNumber = GetEpisodeInfo(parts[1]);
				}
			}
		}

		/// <summary>
		/// Gets or sets the new filename. Must be a filename without path information.
		/// </summary>
		public string NewFilename
		{
			get
			{
				return newFilename;
			}
			set
			{
				string temp = Path.GetFileName(value);
				if(string.IsNullOrEmpty(temp))
					throw new ArgumentException("The specified value is not a proper filename.");
				else
				{
					newFilename = temp;
					typeCacheDirty = true;
				}
			}
		}

		/// <summary>
		/// Gets the path containing the file to be renamed.
		/// </summary>
		public string FilePath
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets or sets whether the rename action will be performed if <see cref="PerformRename"/> is called.
		/// </summary>
		public bool Enabled
		{
			get;
			set;
		}

		/// <summary>
		/// Gets whether the file has already been renamed.
		/// </summary>
		public bool Moved
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets the Exception that occurred when trying to move the file in case <see cref="PerformRename"/> returned <c>false</c>, or <c>null</c> if no exception occurred.
		/// </summary>
		public Exception MoveException
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets or sets the line of episode names corresponding to this rename action
		/// </summary>
		public string NewNameString
		{
			get;
			set;
		}

		/// <summary>
		/// Gets the episode number extracted from the old filename.
		/// </summary>
		/// <remarks>The X and Y coordinates of the Point correspond to the Season and Episode numbers respectively.
		/// In case the episode number cannot be extracted from the old filename, the coordinates are set to <c>-1</c>.</remarks>
		public Point EpisodeNumber
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets the name of the series the file belongs to, extracted from the old filename.
		/// </summary>
		/// <remarks>In case the name cannot be extracted the value is set to <c>null</c>.</remarks>
		public string Series
		{
			get;
			private set;
		}

		#endregion properties


		#region methods

		/// <summary>
		/// Renames the file in <see cref="OldFilename"/> to the name in <see cref="NewFilename"/> if <see cref="Enabled"/> is set to <value>true</value>.
		/// </summary>
		/// <returns><c>true</c> if the rename was successful or <see cref="Enabled"/> is set to <c>false</c>, otherwise <c>false</c>.</returns>
		public bool PerformRename()
		{
			if(!Enabled || Moved)
				return true;

			Enabled = false;

			try
			{
				FileInfo f = new FileInfo(Path.Combine(FilePath, oldFilename));
				f.MoveTo(Path.Combine(FilePath, newFilename));
			}
			catch(Exception ex)
			{
				MoveException = ex;
				return false;
			}

			MoveException = null;
			Moved = true;
			return true;
		}

		/// <summary>
		/// Gets the type of this episode entry based on old and new filename, separator and prefixes.
		/// </summary>
		/// <returns>The determined entry type, it is not guaranteed to match the real type, only a guess.</returns>
		/// <remarks>Note that the values of <see cref="EpisodeEntry.Separator"/>, <see cref="EpisodeEntry.SeasonPrefix"/> and 
		/// <see cref="EpisodeEntry.EpisodePrefix"/> do affect the operation of this method.</remarks>
		public EntryType GetEntryType()
		{
			if(typeCacheDirty)
			{
				if(string.IsNullOrEmpty(newFilename))
				{
					if(string.IsNullOrEmpty(oldFilename))
						typeCache = EntryType.None;
					else
						typeCache = EntryType.Gray;
				}
				else
				{
					string name = Path.GetFileNameWithoutExtension(oldFilename);

					string[ ] parts = SplitFilename(name);

					switch(parts.Length)
					{
						case 1:
							// Old filename does not contain season or episode information that could be matched
							typeCache = EntryType.Red;
							break;

						case 2:
							// Try splitting the second part in case the name was matched by generic season number expressions
							if(string.IsNullOrWhiteSpace(EpisodePrefix))
								parts = Regex.Split(parts[1], RegexEpisodeNumberMatchString, DefaultRegexOptions);
							else
								parts = RemoveEmptyStrings(Regex.Split(parts[1], "(.*" + Regex.Escape(EpisodePrefix) + "[0-9]{1,3})([^0-9]+)", DefaultRegexOptions));

							// Second part may contain episode name
							if((parts.Length == 2) && string.IsNullOrEmpty(parts[0]))
								typeCache = EntryType.Yellow;
							else
								typeCache = EntryType.Green;

							break;

						case 3:
							// Old filename may already contain an episode name
							typeCache = EntryType.Yellow;
							break;

						default:
							// Structure of the old filename could not be determined, so make it Red to encourage the user to check the filename
							typeCache = EntryType.Yellow;
							break;
					}
				}

				typeCacheDirty = false;
			}

			return typeCache;
		}

		/// <summary>
		/// Gets the type of this episode entry based on old and new filename, separator and prefixes.
		/// </summary>
		/// <param name="forceRebuild">Specifies, if the cached value should be recalculated, even if nothing has changed.</param>
		/// <returns>The determined entry type, it is not guaranteed to match the real type, only a guess</returns>
		/// <remarks>The type is not automatically recalculated if the static properties <see cref="Separator"/>, 
		/// <see cref="EpisodePrefix"/> or <see cref="SeasonPrefix"/> are changed. Because these properties affect the 
		/// behaviour of GetEntryType, it might be necessary to call it with <paramref name="forceRebuild"/> set to <c>true</c> 
		/// to determine the new entry type.</remarks>
		public EntryType GetEntryType(bool forceRebuild)
		{
			typeCacheDirty = typeCacheDirty || forceRebuild;

			return GetEntryType();
		}

		/// <summary>
		/// Tries to split a filename into parts containing separate series name, episode information and episode name.
		/// </summary>
		/// <param name="name">The filename to split.</param>
		/// <returns>An array containing all the parts of the filename.</returns>
		/// <remarks>All the parts concatenated to a single string may not give the original filename, as parts are removed when matching 
		/// possible separators.</remarks>
		private string[ ] SplitFilename(string name)
		{
			string[ ] parts = name.Split(new string[ ] { Separator }, StringSplitOptions.RemoveEmptyEntries);
			if(!string.IsNullOrWhiteSpace(SeasonPrefix) && (parts.Length == 1))
				parts = RemoveEmptyStrings(Regex.Split(name, "(" + SeasonPrefix + ")([0-9].*)", DefaultRegexOptions));
			if(parts.Length == 1)
				parts = RemoveEmptyStrings(Regex.Split(name, @"\.(s[0-9].*)", DefaultRegexOptions));
			if(parts.Length == 1)
				parts = RemoveEmptyStrings(Regex.Split(name, " (s[0-9].*)", DefaultRegexOptions));

			return parts;
		}

		#endregion methods


		#region static members
		
		static int episodeNumberDigits;

		/// <summary>
		/// A regular expression match string used to match episode information in filenames.
		/// </summary>
		public static readonly string RegexEpisodeNumberMatchString = @"(?<![0-9])[0-9]{1,3}[^0-9]+[0-9]{0,3}(?![0-9])";

		/// <summary>
		/// The default options for regular expression matching used internally.
		/// </summary>
		public static readonly RegexOptions DefaultRegexOptions = RegexOptions.Singleline | RegexOptions.IgnoreCase;

		static EpisodeEntry()
		{
			Separator = " - ";
			EpisodePrefix = "x";
			SeasonPrefix = "";
			EpisodeNumberDigits = 2;
		}

		/// <summary>
		/// Gets or sets the string used to separate the series name from the season prefix and the episode number from the episode name.
		/// The default value is <c>" - "</c>.
		/// </summary>
		/// <remarks>This value is also used when determining the type of the entry.
		/// Common values are <c>" - "</c>, <c>"."</c> or <c>" "</c>.</remarks>
		public static string Separator
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the string used as a prefix to the episode number.
		/// The default value is <c>"x"</c>.
		/// </summary>
		/// <remarks>This value is also used when determining the type of the entry.
		/// Common values are <c>"x"</c> or <c>"e"</c>.</remarks>
		public static string EpisodePrefix
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the string used as a prefix to the season number.
		/// The default value is <c>""</c>.
		/// </summary>
		/// <remarks>This value is also used when determining the type of the entry.
		/// Common values are <c>""</c> or <c>"s"</c>.</remarks>
		public static string SeasonPrefix
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the minimum number of digits to use for epipsode and season numbers when formatting filenames.
		/// The default value is <c>2</c>
		/// </summary>
		/// <remarks>This value is only used for output and has no effect on episode number matching.</remarks>
		public static int EpisodeNumberDigits
		{
			get
			{
				return episodeNumberDigits;
			}
			set
			{
				if(value > 5)
					episodeNumberDigits = 5;
				else if(value < 1)
					episodeNumberDigits = 1;
				else
					episodeNumberDigits = value;
			}
		}

		/// <summary>
		/// Removes all strings that are <c>null</c>, empty or only contain whitespace from an array, returning a new array.
		/// </summary>
		/// <param name="arr">The array from which to remove the strings.</param>
		/// <returns>A new array containing only not empty strings.</returns>
		public static string[ ] RemoveEmptyStrings(string[ ] arr)
		{
			int count = 0;

			foreach(string temp in arr)
			{
				if(string.IsNullOrWhiteSpace(temp))
					count++;
			}

			string[ ] newArray = new string[arr.Length - count];
			count = 0;

			foreach(string temp in arr)
			{
				if(!string.IsNullOrWhiteSpace(temp))
				{
					newArray[count] = temp;
					count++;
				}
			}

			return newArray;
		}

		/// <summary>
		/// Formats a filename from a given series name, episode number and file extension according to the values 
		/// of <see cref="Separator"/>, <see cref="SeasonPrefix"/> and <see cref="EpisodePrefix"/> set.
		/// </summary>
		/// <param name="series">The name of the series.</param>
		/// <param name="episodeNumber">The episode number, the season number being the X- and the episode number being Y-coordinate.</param>
		/// <param name="extension">The file extension, including the period (e.g. ".avi").</param>
		/// <returns>The filename corresponding to the input values.</returns>
		public static string FormatFilename(string series, Point episodeNumber, string extension)
		{
			return FormatFilename(series, episodeNumber, "", extension);
		}

		/// <summary>
		/// Formats a filename from a given series name, episode number, episode name and file extension according to the values 
		/// of <see cref="Separator"/>, <see cref="SeasonPrefix"/> and <see cref="EpisodePrefix"/> set.
		/// </summary>
		/// <param name="series">The name of the series.</param>
		/// <param name="episodeNumber">The episode number, the season number being the X- and the episode number being Y-coordinate.</param>
		/// <param name="episodeName">The episode name.</param>
		/// <param name="extension">The file extension, including the period (e.g. ".avi").</param>
		/// <returns>The filename corresponding to the input values.</returns>
		public static string FormatFilename(string series, Point episodeNumber, string episodeName, string extension)
		{
			StringBuilder sb = new StringBuilder(series.Trim());
			sb.Append(Separator).Append(SeasonPrefix);
			sb.Append(episodeNumber.X.ToString("D" + EpisodeNumberDigits.ToString())).Append(EpisodePrefix);
			sb.Append(episodeNumber.Y.ToString("D" + EpisodeNumberDigits.ToString()));
			if(!string.IsNullOrWhiteSpace(episodeName))
				sb.Append(Separator).Append(episodeName.Trim());
			sb.Append(extension.Trim());

			foreach(char c in Path.GetInvalidFileNameChars())
				sb.Replace(c.ToString(), "");
			foreach(char c in Path.GetInvalidPathChars())
				sb.Replace(c.ToString(), "");
			sb.Replace("  ", " ");
			sb.Replace("..", ".");
			sb.Replace("__", "_");

			return sb.ToString().Trim();
		}

		/// <summary>
		/// Extracts episode information, consisting of season number and episode number, from a filename or part of a filename.
		/// </summary>
		/// <param name="part">The filename or part of a filename to extract the episode information from.</param>
		/// <returns>On success, a <see cref="System.Drawing.Point"/> structure with X and Y coordinates set to season and episode number respectively.
		/// On failure, a <see cref="System.Drawing.Point"/> structure with both X and Y coordinates set to <c>-1</c>.</returns>
		public static Point GetEpisodeInfo(string part)
		{
			Match m = Regex.Match(part, RegexEpisodeNumberMatchString, DefaultRegexOptions);

			if(m.Success)
			{
				string[ ] parts = Regex.Split(m.Value, @"(?<=[0-9])[^0-9]+(?=[0-9])", RegexOptions.Singleline);

				if(parts.Length == 2)
					try
					{
						int x = int.Parse(parts[0]);
						int y = int.Parse(parts[1]);

						return new Point(x, y);
					}
					catch
					{
						return new Point(-1, -1);
					}
			}

			return new Point(-1, -1);
		}

		#endregion static members
	}
}
