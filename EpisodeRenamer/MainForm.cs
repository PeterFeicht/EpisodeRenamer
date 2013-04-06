using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace EpisodeRenamer
{
	public partial class MainForm : Form
	{
		#region imports

		// Imports for setting up a clipboard watch to grab episode names copied

		[DllImport("User32.dll")]
		public static extern int SetClipboardViewer(int hWndNewViewer);

		[DllImport("User32.dll", CharSet = CharSet.Auto)]
		public static extern bool ChangeClipboardChain(IntPtr hWndRemove, IntPtr hWndNewNext);

		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		public static extern int SendMessage(IntPtr hwnd, int wMsg, IntPtr wParam, IntPtr lParam);

		#endregion imports


		#region fields, ctor, properties

		ChangePrefixForm frmChangePrefix;
		CheckClipboardDataForm frmCheckClipboard;

		DatagridViewCheckBoxHeaderCell chkHeader;

		/// <summary>
		/// Use property!
		/// </summary>
		bool namesFromClipboard = false;
		/// <summary>
		/// Use property!
		/// </summary>
		bool monitoringClipboard = false;

		IntPtr nextClipboardViewer;
		bool nextClipboardViewerSet = false;
		string clipboardData = "";
		StringBuilder sbClipboardData = new StringBuilder();
		string folderName = "";
		bool log;

		/// <summary>
		/// A list of regular expressions for filenames that should not be added to the episode list when parsing.
		/// </summary>
		public readonly string[ ] FilenameBlacklist = new string[ ] {
			@"desktop\.ini$",
			@"thumbs\.db$",
			@".*\.txt$",
			@".*\.nfo$",
			@".*\.jpg$",
			@".*\.png$",
			@".*\.ico$" };

		public readonly string defaultNameFileName = "Episode Names.txt";
		private bool dropped;

		public MainForm() : this(false)
		{
			
		}

		public MainForm(bool log)
		{
			InitializeComponent();
			this.log = log;

			try
			{
				if(log)
				{
					// Create logfile
					string filename = string.Format("EpisodeRenamer_{0:yyyy-MM-dd_HH.mm}.log", DateTime.UtcNow);

					Trace.AutoFlush = true;
					Trace.Listeners.Add(new TextWriterTraceListener(filename));
					Trace.WriteLine(filename);
				}
				Trace.WriteLine("EpisodeRenamer Logfile");
				Trace.WriteLine(Application.ExecutablePath);
				Trace.WriteLine("");
			}
			catch
			{
				this.Text += " - unable to create logfile";
			}

			try
			{
				// Try to get the executable icon for use as window icon
				Icon i = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
				this.Icon = i;
			}
			catch(Exception ex)
			{
				Trace.WriteLine("Exception in MainForm constructor: ExtractAssociatedIcon.");
				Trace.WriteLine(ex.Message);
				Trace.WriteLine(ex.StackTrace);
			}

			frmChangePrefix = new ChangePrefixForm();
			frmCheckClipboard = new CheckClipboardDataForm();
			chkHeader = new DatagridViewCheckBoxHeaderCell();

			chkHeader.OnCheckBoxClicked += new CheckBoxClickedEventHandler(chkHeader_OnCheckBoxClicked);
			chkHeader.Value = "";

			dataGridView.Columns[0].HeaderCell = chkHeader;
		}

		private void MainForm_Load(object sender, EventArgs e)
		{
			SetReplaceGroup(false);

			openNameFile.FileName = defaultNameFileName;

			if(Directory.Exists("H:\\"))
				openFolder.SelectedPath = "H:\\";
		}

		bool NamesFromClipboard
		{
			get
			{
				return namesFromClipboard;
			}
			set
			{
				if(namesFromClipboard == value)
					return;

				namesFromClipboard = value;
				txtNameFile.ReadOnly = value;
				btnEditClipboardData.Enabled = value;
				btnSaveClipboardData.Enabled = value;

				txtNameFile.Text = value ? "(Clipboard data)" : "";
			}
		}

		bool MonitoringClipboard
		{
			get
			{
				return monitoringClipboard;
			}
			set
			{
				if(monitoringClipboard == value)
					return;

				monitoringClipboard = value;
				btnPasteNames.Enabled = !value;
				txtNameFile.ReadOnly = value;
				chkUseFolderName.Enabled = !value;

				if(value)
				{
					btnEditClipboardData.Enabled = false;
					btnSaveClipboardData.Enabled = false;
					txtNameFile.Text = "(Monitoring clipboard, click '" + btnReadNames.Text + "' or uncheck the box to finish)";

					nextClipboardViewer = (IntPtr)SetClipboardViewer((int)this.Handle);
					nextClipboardViewerSet = true;
				}
				else
				{
					UnlinkClipboard();
				}
			}
		}
		
		#endregion fields, ctor, properties


		#region methods

		void InvalidateFilenames()
		{
			btnRename.Enabled = false;
			lblEditWrong.Visible = false;
			btnReadNames.Enabled = false;
			episodes.Clear();
		}

		bool ReadFiles(string path)
		{
			Trace.WriteLine("ReadFiles called: " + path);
			string[ ] files;

			try
			{
				files = Directory.GetFiles(path);
				Array.Sort<string>(files);
				folderName = Path.GetFileName(Path.GetFullPath(path));
			}
			catch(Exception ex)
			{
				Trace.WriteLine("Exception (" + ex.GetType() + ") in ReadFiles: list directory.");
				Trace.WriteLine(ex.Message);
				Trace.WriteLine(ex.StackTrace);

				return false;
			}
			Trace.Indent();
			Trace.WriteLine("Directory Listing successful, number of files: " + files.Length);

			foreach(string item in files)
			{
				if(Path.GetExtension(item) != "")
				{
					if(Array.Exists<string>( FilenameBlacklist, (string pat) => Regex.IsMatch(item, pat, RegexOptions.IgnoreCase | RegexOptions.Singleline) ))
						continue;

					Trace.WriteLine(item);
					Trace.Indent();

					EpisodeEntry ep = new EpisodeEntry(item);
					Trace.WriteLine("Series: " + ep.Series);
					Trace.WriteLine("EpisodeNumber: " + ep.EpisodeNumber);

					Trace.Unindent();
					episodes.Add(ep);
				}
			}

			Trace.WriteLine("ReadFiles finished.");
			Trace.Unindent();
			btnReadNames.Enabled = true;
			return true;
		}

		/// <summary>
		/// Matches the episode names in <paramref name="data"/> to <see cref="EpisodeEntry"/> objects in <see cref="episodes"/>
		/// </summary>
		/// <param name="data">The lines to match</param>
		/// <returns><value>false</value> in case <paramref name="data"/> is <value>null</value> or empty, otherwise true.</returns>
		bool ReadNames(string data)
		{
			if(string.IsNullOrWhiteSpace(data))
				return false;

			return ReadNames(data.Split(new string[ ] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries));
		}

		/// <summary>
		/// Matches the episode names in <paramref name="lines"/> to <see cref="EpisodeEntry"/> objects in <see cref="episodes"/>
		/// </summary>
		/// <param name="lines">The lines to match</param>
		/// <returns><value>false</value> in case <paramref name="lines"/> is <value>null</value> or empty, otherwise true.</returns>
		bool ReadNames(string[] lines)
		{
			int count = 0;

			if((lines == null) || (lines.Length < 1))
				return false;

			Regex post = null;

			if(chkPostReplace.Checked)
			{
				try
				{
					post = new Regex(txtSearch.Text, chkIgnoreCase.Checked ? RegexOptions.IgnoreCase : RegexOptions.None);
				}
				catch(ArgumentException ex)
				{
					Trace.WriteLine("ArgumentException in ReadNames: Regex constructor");
					Trace.WriteLine(ex.Message);

					post = null;
				}
			}

			foreach(string line in lines)
			{
				int season;
				int episode;
				string name;

				if(string.IsNullOrWhiteSpace(line))
					continue;

				if(count >= episodes.Count)
					break;

				// Extract season number, episode number and episode name from line
				ExtractEpisodeInformation(line, out season, out episode, out name);

				// No name, continue with next line
				if(string.IsNullOrWhiteSpace(name))
					continue;

				// Either IMDb or generic syntax matched the line and at least an episode name could be extracted
				// so search for the corresponding EpisodeEntry and save the data

				EpisodeEntry e;
				Point p;

				// Only episode name
				if(season < 0 || episode < 0)
				{
					// No episode numbers, so take the next element in the list
					e = (EpisodeEntry)episodes[count];

					// If the episode information is set in the EpisodEentry object, take that one, otherwise guess
					p = new Point(
						e.EpisodeNumber.X,
						e.EpisodeNumber.Y < 0 ? count : e.EpisodeNumber.Y );
				} // Episode name and numbers
				else
				{
					p = new Point(season, episode);
					int idx = -1;

					// Find the EpisodeEntry corresponding to the episode number extracted
					for(int j = 0; j < episodes.Count; j++)
						if(((EpisodeEntry)episodes[j]).EpisodeNumber == p)
						{
							idx = j;
							break;
						}

					// Episode not found, continue with next line
					if(idx < 0)
						continue;

					e = (EpisodeEntry)episodes[idx];
				}

				if(chkUseFolderName.Checked)
					e.Series = folderName;
				else
					e.ResetSeries();

				e.NewNameString = line.Trim();
				e.NewFilename = EpisodeEntry.FormatFilename(e.Series, p, name, Path.GetExtension(e.OldFilename));

				try
				{
					if(post != null)
						e.NewFilename = post.Replace(e.NewFilename, txtReplace.Text);
				}
				catch(Exception ex)
				{
					Trace.WriteLine("Exception (" + ex.GetType() + ") in ReadNames: regex replace.");
					Trace.WriteLine(ex.Message);
					Trace.WriteLine(ex.StackTrace);

					post = null;
				}

				e.Enabled = (e.GetEntryType() != EpisodeEntry.EntryType.Yellow);
				count++;
			}

			UpdateGridView();
			return (count > 0);
		}

		void ExtractEpisodeInformation(string line, out int season, out int episode, out string name)
		{
			season = -1;
			episode = -1;
			name = "";

			// Matches IMDb syntax
			if(Regex.IsMatch(line, @"Season [0-9]+, Episode [0-9]+.*", EpisodeEntry.DefaultRegexOptions))
			{
				Match mSeason = Regex.Match(line, @"(?<=Season )[0-9]+", EpisodeEntry.DefaultRegexOptions);
				Match mEpisode = Regex.Match(line, @"(?<=Episode )[0-9]+", EpisodeEntry.DefaultRegexOptions);

				if(mSeason.Success && mEpisode.Success)
					try
					{
						season = int.Parse(mSeason.Value);
						episode = int.Parse(mEpisode.Value);
					}
					catch(FormatException ex)
					{
						Trace.WriteLine("FormatException in ExtractEpisodeInformation: IMDb syntax season and episode match.");
						Trace.WriteLine(ex.Message);

						season = -1;
						episode = -1;
					}

				Match m = Regex.Match(line, "(?<=: ).*");
				if(m.Success)
					name = m.Value;

			} // Matches TheTVDB syntax
			else if(Regex.IsMatch(line, @"^[0-9]+ x [0-9]+\t.*", EpisodeEntry.DefaultRegexOptions))
			{
				Match mSeason = Regex.Match(line, @"^[0-9]+", EpisodeEntry.DefaultRegexOptions);
				Match mEpisode = Regex.Match(line, @"(?<= x )[0-9]+", EpisodeEntry.DefaultRegexOptions);

				if(mSeason.Success && mEpisode.Success)
					try
					{
						season = int.Parse(mSeason.Value);
						episode = int.Parse(mEpisode.Value);
					}
					catch(FormatException ex)
					{
						Trace.WriteLine("FormatException in ExtractEpisodeInformation: TheTVDB syntax season and episode match.");
						Trace.WriteLine(ex.Message);

						season = -1;
						episode = -1;
					}

				Match m = Regex.Match(line, @"(?<=[0-9]+ x [0-9]+\t)[^\t\n]+");
				if(m.Success)
					name = m.Value;

			} // Matches old TheTVDB syntax
			else if(Regex.IsMatch(line, @"^[0-9]+ - [0-9]+\t.*", EpisodeEntry.DefaultRegexOptions))
			{
				Match mSeason = Regex.Match(line, @"^[0-9]+", EpisodeEntry.DefaultRegexOptions);
				Match mEpisode = Regex.Match(line, @"(?<= - )[0-9]+", EpisodeEntry.DefaultRegexOptions);

				if(mSeason.Success && mEpisode.Success)
					try
					{
						season = int.Parse(mSeason.Value);
						episode = int.Parse(mEpisode.Value);
					}
					catch(FormatException ex)
					{
						Trace.WriteLine("FormatException in ExtractEpisodeInformation: old TheTVDB syntax season and episode match.");
						Trace.WriteLine(ex.Message);

						season = -1;
						episode = -1;
					}

				Match m = Regex.Match(line, @"(?<=[0-9]+ - [0-9]+\t)[^\t\n]+");
				if(m.Success)
					name = m.Value;

			} // Matches generic syntax
			else
			{
				Match m = Regex.Match(line, EpisodeEntry.RegexEpisodeNumberMatchString, EpisodeEntry.DefaultRegexOptions);

				// If episode information is found, extract it
				if(m.Success)
				{
					Point p = EpisodeEntry.GetEpisodeInfo(m.Value);
					season = p.X;
					episode = p.Y;

					if(m.Index + m.Length < line.Length)
						name = line.Substring(m.Index + m.Length);
					else if(line.Length - m.Length > 0)
						name = line.Substring(0, line.Length - m.Length);
					else
						name = "";
				} // No episode information found, so use the whole line as name and guess the numbers
				else
					name = line;
			}
		}

		/// <summary>
		/// Rereads all values and auto-sizes the columns of the dataGridView.
		/// </summary>
		void UpdateGridView()
		{
			int tmp = dataGridView.FirstDisplayedScrollingRowIndex;
			episodes.ResetBindings(false);
			dataGridView.AutoResizeColumns(DataGridViewAutoSizeColumnsMode.AllCells);
			if(episodes.Count > 0)
				dataGridView.FirstDisplayedScrollingRowIndex = (tmp < episodes.Count ? tmp : episodes.Count - 1);
		}

		private void SetReplaceGroup(bool visible)
		{
			Trace.WriteLine("SetReplaceGroup called: " + visible);
			Trace.WriteLine("grpReplace.Visible: " + grpReplace.Visible);

			if(grpReplace.Visible != visible)
			{
				grpReplace.Visible = visible;
				dataGridView.Top += (visible ? grpReplace.Height : -grpReplace.Height);
				dataGridView.Height += (visible ? -grpReplace.Height : grpReplace.Height);
			}
		}

		private bool CheckRegex(string regex)
		{
			try
			{
				Regex r = new Regex(regex);
			}
			catch
			{
				return false;
			}

			return true;
		}

		#endregion methods


		#region events

		private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			UnlinkClipboard();
			dataGridView.DataSource = null;
		}

		private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			Process.Start(@"http://msdn.microsoft.com/en-us/library/az24scfc.aspx");
		}

		// dataGridView
		private void dataGridView_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
		{
			EpisodeEntry entry = episodes[e.RowIndex] as EpisodeEntry;
			if(entry == null)
				return;

			e.PaintParts &= ~DataGridViewPaintParts.Background;

			// Determine whether the cell should be painted
			// with the custom background.
			if(entry.GetEntryType() != EpisodeEntry.EntryType.None)
			{
				// Calculate the bounds of the row.
				Rectangle rowBounds = new Rectangle(
					0,
					e.RowBounds.Top,
					dataGridView.Columns.GetColumnsWidth(DataGridViewElementStates.Visible) - dataGridView.HorizontalScrollingOffset + 1,
					e.RowBounds.Height);

				// Paint the custom background.
				Color color;
				switch(entry.GetEntryType())
				{
					case EpisodeEntry.EntryType.Green:
						color = Color.Green;
						break;
					case EpisodeEntry.EntryType.Red:
						color = Color.Red;
						break;
					case EpisodeEntry.EntryType.Yellow:
						color = Color.Yellow;
						dataGridView.Rows[e.RowIndex].DefaultCellStyle.SelectionForeColor = Color.Red;
						break;
					case EpisodeEntry.EntryType.Gray:
						color = Color.LightGray;
						break;
					case EpisodeEntry.EntryType.Blue:
						color = Color.LightBlue;
						break;
					default:
						color = Color.Cyan;
						break;
				}

				e.Graphics.FillRectangle(new SolidBrush(color), rowBounds);
			}
		}

		private void dataGridView_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
		{
			EpisodeEntry entry = episodes[e.RowIndex] as EpisodeEntry;
			if(entry == null)
				return;

			// Determine whether the cell should be painted
			// with the custom foreground.
			if((entry.GetEntryType() != EpisodeEntry.EntryType.None) && !entry.Enabled)
			{
				// Calculate the bounds of the row.
				Rectangle rowBounds = new Rectangle(
					0,
					e.RowBounds.Top,
					dataGridView.Columns.GetColumnsWidth(DataGridViewElementStates.Visible) - dataGridView.HorizontalScrollingOffset + 1,
					e.RowBounds.Height);

				Color c1 = Color.FromArgb(160, Color.Gray);
				Color c2 = Color.FromArgb(200, Color.Gray);
				Brush br = new HatchBrush(HatchStyle.Weave, c2, c1);
				e.Graphics.FillRectangle(br, rowBounds);
			}
		}

		private void chkHeader_OnCheckBoxClicked(object sender, DataGridViewCheckBoxHeaderCellEventArgs e)
		{
			foreach(EpisodeEntry entry in episodes)
			{
				entry.Enabled = e.Checked;
			}

			dataGridView.EndEdit();
			dataGridView.Refresh();
		}

		private void dataGridView_CurrentCellDirtyStateChanged(object sender, EventArgs e)
		{
			if(dataGridView.CurrentCellAddress.X == 0)
				dataGridView.CommitEdit(DataGridViewDataErrorContexts.Commit);
		}

		// HELP
		private void lblChooseFirst_Click(object sender, EventArgs e)
		{
			string help = @"Firstly, choose the folder containing the episode files to be renamed,
or enter the path manually.
Then you can choose between using a file containing the episode names, pasting the episode names from the clipboard or automatically reading the episode names from the clipboard as they are copied, one at a time.
The episode names must not contain the name of the series, or the matching of the episode numbers will not work.

Secondly, press the 'Read original filenames' and 'Read episode names' buttons to populate the grid with the files to be renamed and the new names respectively. When using the automatic clipboard mode, press 'Read episode names' or uncheck the checkbox to stop monitoring and confirm the data.
You can check 'Use folder name for series title' if the original filenames do not contain an appropriately formatted series name, to use the name of the folder containing the episodes as series title.";

			MessageBox.Show(help, "Help", MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

		private void lblEditWrong_Click(object sender, EventArgs e)
		{
			string help = @"When the filenames and episode names are read, you can edit each file manually to change the new name, add one, or prevent the file from being renamed.
Files being renamed are marked green, files that already have an episode name are marked yellow, files that do not contain season or episode information are marked red and files without a new episode name are marked grey.

You can also select prefixes for the season and episode numbers as well as separators by pressing 'Set prefixes...'.
Note that the selected prefixes do affect the episode matching, so setting the right prefixes before reading files is strongly recommended.";

			MessageBox.Show(help, "Help", MessageBoxButtons.OK, MessageBoxIcon.Information);
		}

		// INPUT
		private void btnChooseFolder_Click(object sender, EventArgs e)
		{
			if(openFolder.ShowDialog() == DialogResult.OK)
			{
				txtEpisodeFolder.Text = openFolder.SelectedPath;
				openNameFile.InitialDirectory = openFolder.SelectedPath;
				saveClipboardData.InitialDirectory = openFolder.SelectedPath;
				btnReadFiles.PerformClick();
			}
		}

		private void btnChooseNameFile_Click(object sender, EventArgs e)
		{
			if(openNameFile.ShowDialog() == DialogResult.OK)
			{
				txtNameFile.ReadOnly = false;
				MonitoringClipboard = false;
				NamesFromClipboard = false;
				chkMonitorClipboard.Checked = false;

				txtNameFile.Text = openNameFile.FileName;
				
				if(btnReadNames.Enabled)
					btnReadNames.PerformClick();
			}
		}

		private void txtEpisodeFolder_TextChanged(object sender, EventArgs e)
		{
			if(!Directory.Exists(txtEpisodeFolder.Text))
				InvalidateFilenames();
		}

		private void txtEpisodeFolder_KeyDown(object sender, KeyEventArgs e)
		{
			switch(e.KeyData)
			{
				case Keys.Enter:
					if(!string.IsNullOrWhiteSpace(txtEpisodeFolder.Text))
						btnReadFiles.PerformClick();
					break;

				default:
					return;
			}

			e.SuppressKeyPress = true;
		}

		private void txtNameFile_TextChanged(object sender, EventArgs e)
		{
			if(File.Exists(txtNameFile.Text) || MonitoringClipboard || NamesFromClipboard || (txtNameFile.Text == ""))
				return;
				
			InvalidateFilenames();
		}

		private void txtNameFile_KeyDown(object sender, KeyEventArgs e)
		{
			switch(e.KeyData)
			{
				case Keys.Enter:
					if(!string.IsNullOrWhiteSpace(txtNameFile.Text))
						btnReadNames.PerformClick();
					break;

				default:
					return;
			}

			e.SuppressKeyPress = true;
		}

		private void btnSetPrefix_Click(object sender, EventArgs e)
		{
			frmChangePrefix.EpisodePrefix = EpisodeEntry.EpisodePrefix;
			frmChangePrefix.SeasonPrefix = EpisodeEntry.SeasonPrefix;
			frmChangePrefix.Separator = EpisodeEntry.Separator;
			frmChangePrefix.EpisodeNumberDigits = EpisodeEntry.EpisodeNumberDigits;

			if(frmChangePrefix.ShowDialog() == DialogResult.OK)
			{
				EpisodeEntry.EpisodePrefix = frmChangePrefix.EpisodePrefix;
				EpisodeEntry.SeasonPrefix = frmChangePrefix.SeasonPrefix;
				EpisodeEntry.Separator = frmChangePrefix.Separator;
				EpisodeEntry.EpisodeNumberDigits = frmChangePrefix.EpisodeNumberDigits;
			}
		}

		private void chkUseFolderName_CheckedChanged(object sender, EventArgs e)
		{
			if(btnReadNames.Enabled)
				btnReadNames.PerformClick();
		}

		private void chkPostReplace_CheckedChanged(object sender, EventArgs e)
		{
			SetReplaceGroup(chkPostReplace.Checked);
		}

		private void txtRegex_TextChanged(object sender, EventArgs e)
		{
			TextBox t = sender as TextBox;

			if(t != null)
			{
				if(CheckRegex(t.Text))
					t.ForeColor = Color.Black;
				else
					t.ForeColor = Color.Red;
			}
		}

		// OUTPUT
		private void btnReadFiles_Click(object sender, EventArgs e)
		{
			InvalidateFilenames();
			if(Directory.Exists(txtEpisodeFolder.Text))
			{
				if(ReadFiles(txtEpisodeFolder.Text))
				{
					if(episodes.Count > 0)
						dataGridView.FirstDisplayedScrollingRowIndex = episodes.Count - 1;
					string episodeNameFileName = Path.Combine(txtEpisodeFolder.Text, defaultNameFileName);
					if(File.Exists(episodeNameFileName))
					{
						if(dropped)
						{
							Trace.WriteLine("In btnReadFiles_Click: dropped == true -> disabling clipboard data.");
							MonitoringClipboard = false;
							NamesFromClipboard = false;
							txtNameFile.Text = episodeNameFileName;
							dropped = false;
						}
						if(!NamesFromClipboard)
							txtNameFile.Text = episodeNameFileName;
						btnReadNames.PerformClick();
					}
					else if(NamesFromClipboard)
						btnReadNames.PerformClick();
					else
						UpdateGridView();
				}
				else
					MessageBox.Show("Error reading the files.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			else
				MessageBox.Show("The specified folder does not exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
		}

		private void btnReadNames_Click(object sender, EventArgs e)
		{
			if(MonitoringClipboard)
			{
				chkMonitorClipboard.Checked = false;
			}

			if(NamesFromClipboard)
			{
				if(ReadNames(clipboardData))
					UpdateGridView();
				else
					return;
				// Set episode file in same folder as episodes in case another one is still selected
				if(Path.GetDirectoryName(txtEpisodeFolder.Text) != Path.GetDirectoryName(saveClipboardData.FileName))
				{
					saveClipboardData.InitialDirectory = txtEpisodeFolder.Text;
					saveClipboardData.FileName = Path.Combine(txtEpisodeFolder.Text, defaultNameFileName);
				}
			}
			else
			{
				if(txtNameFile.Text == string.Empty)
					return;

				if(File.Exists(txtNameFile.Text))
				{
					try
					{
						// Ignore huge files, prevents freeze when video file is dropped
						FileInfo fi = new FileInfo(txtNameFile.Text);
						if(fi.Length > 1024 * 1024)
							return;

						if(ReadNames(File.ReadAllLines(txtNameFile.Text)))
							UpdateGridView();
						else
							throw new Exception("Error reading the file.");
						// Set save file name to the existing file, most likely the same one will be used if it is overwritten with new data.
						saveClipboardData.InitialDirectory = Path.GetDirectoryName(txtNameFile.Text);
						saveClipboardData.FileName = txtNameFile.Text;
					}
					catch(Exception ex)
					{
						MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
						return;
					}
				}
				else
				{
					MessageBox.Show("The specified file doesn't exist.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
					return;
				}
			}

			btnRename.Enabled = true;
			lblEditWrong.Visible = true;
		}

		private void btnRename_Click(object sender, EventArgs e)
		{
			List<Exception> exceptions = new List<Exception>();
			int renamed = 0;
			MessageBoxIcon icon;

			foreach(EpisodeEntry item in episodes)
			{
				if(item.PerformRename())
				{
					if(item.Moved)
						renamed++;
				}
				else
					exceptions.Add(item.MoveException);
			}

			StringBuilder sb = new StringBuilder();
			if(renamed == 1)
				sb.Append("One file has");
			else
				sb.AppendFormat("{0} files have", renamed);
			sb.AppendLine(" been renamed.");

			if(exceptions.Count > 0)
			{
				icon = MessageBoxIcon.Warning;
				sb.AppendLine("One or more errors occurred while renaming the files:");

				foreach(Exception ex in exceptions)
					sb.AppendLine(ex.Message);
			}
			else
			{
				icon = MessageBoxIcon.Information;
				if(renamed == 0)
					sb = new StringBuilder("No files have been renamed.");
				else
					sb.AppendLine("All files have successfully been renamed.");
				episodes.Clear();
				InvalidateFilenames();
				btnReadFiles.PerformClick();
			}

			MessageBox.Show(sb.ToString(), "Finished", MessageBoxButtons.OK, icon);
		}

		#endregion events


		#region clipboard

		/// <summary>
		/// Checks, if the nextClipboardViewer field differs from IntPtr.Zero, and if it does, 
		/// calls ChangeClipboardChain to unlink this window from the clipboard watching chain.
		/// </summary>
		void UnlinkClipboard()
		{
			if(nextClipboardViewerSet)
			{
				ChangeClipboardChain(this.Handle, nextClipboardViewer);
				nextClipboardViewer = IntPtr.Zero;
				nextClipboardViewerSet = false;
			}
		}

		private void btnPasteNames_Click(object sender, EventArgs e)
		{
			try
			{
				frmCheckClipboard.Data = Clipboard.ContainsText() ? Clipboard.GetText() : "";
			}
			catch(Exception ex)
			{
				Trace.WriteLine("Exception (" + ex.GetType() + ") in btnPasteNames_Click: get data from clipboard.");
				Trace.WriteLine(ex.Message);
				Trace.WriteLine(ex.StackTrace);

				frmCheckClipboard.Data = "Error retrieving data from clipboard.";
			}

			if((frmCheckClipboard.ShowDialog() == DialogResult.OK) && (frmCheckClipboard.Data != ""))
			{
				NamesFromClipboard = true;
				clipboardData = frmCheckClipboard.Data;
				InvalidateFilenames();
			}
			else
			{
				if(string.IsNullOrWhiteSpace(clipboardData))
					NamesFromClipboard = false;
			}
		}

		private void btnEditClipboardData_Click(object sender, EventArgs e)
		{
			if(string.IsNullOrEmpty(clipboardData))
			{
				btnEditClipboardData.Enabled = false;
				MessageBox.Show("The clipboard data is empty.");
				NamesFromClipboard = false;
				return;
			}

			frmCheckClipboard.Data = clipboardData;

			if(frmCheckClipboard.ShowDialog() == DialogResult.OK)
			{
				if(string.IsNullOrWhiteSpace(frmCheckClipboard.Data))
					NamesFromClipboard = false;

				clipboardData = frmCheckClipboard.Data;
			}
		}

		private void btnSaveClipboardData_Click(object sender, EventArgs e)
		{
			if(string.IsNullOrWhiteSpace(clipboardData))
			{
				MessageBox.Show("No clipboard data to save.");
				return;
			}

			if(saveClipboardData.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				try
				{
					File.WriteAllText(saveClipboardData.FileName, clipboardData);

				}
				catch(Exception ex)
				{
					MessageBox.Show("Error writing the file:" + Environment.NewLine + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
			}
		}

		private void chkMonitorClipboard_CheckedChanged(object sender, EventArgs e)
		{
			// Stop monitoring and check data
			if(!chkMonitorClipboard.Checked && MonitoringClipboard)
			{
				MonitoringClipboard = false;
				NamesFromClipboard = true;
				clipboardData = sbClipboardData.ToString();
				btnEditClipboardData.PerformClick();
			}

			// Start monitoring
			if(chkMonitorClipboard.Checked && !MonitoringClipboard)
			{
				MonitoringClipboard = true;
				sbClipboardData.Clear();
			}
		}

		protected override void WndProc(ref Message m)
		{
			// defined in winuser.h
			const int WM_DRAWCLIPBOARD = 0x0308;
			const int WM_CHANGECBCHAIN = 0x030D;

			switch(m.Msg)
			{
				case WM_DRAWCLIPBOARD:
					if(Clipboard.ContainsText())
					{
						sbClipboardData.AppendLine(Clipboard.GetText());
					}

					// The message is sent to this window causing stack overflow I guess...
					if(nextClipboardViewer != this.Handle)
						SendMessage(nextClipboardViewer, m.Msg, m.WParam, m.LParam);
					break;

				case WM_CHANGECBCHAIN:
					if(m.WParam == nextClipboardViewer)
						nextClipboardViewer = m.LParam;
					else
						SendMessage(nextClipboardViewer, m.Msg, m.WParam, m.LParam);
					break;

				default:
					base.WndProc(ref m);
					break;
			}
		}

		private void txt_DragDrop(object sender, DragEventArgs e)
		{
			TextBox txt = sender as TextBox;

			if(txt == null)
			{
				if(sender == this)
				{
					txt = txtEpisodeFolder;
				}
				else
				{
					e.Effect = DragDropEffects.None;
					return;
				}
			}

			if((e.AllowedEffect & DragDropEffects.Copy) == DragDropEffects.Copy)
			{
				e.Effect = DragDropEffects.Copy;
				if(e.Data.GetDataPresent(typeof(String)))
				{
					txt.Text = (string)e.Data.GetData(typeof(String));
				}
				else if(e.Data.GetDataPresent(DataFormats.FileDrop))
				{
					string[ ] filenames = (string[ ])e.Data.GetData(DataFormats.FileDrop);
					if(filenames.Length < 1)
						return;
					txt.Text = filenames[0];
				}
				else
				{
					e.Effect = DragDropEffects.None;
					return;
				}

				if(txt == txtEpisodeFolder)
				{
					if(File.Exists(txt.Text))
						txt.Text = Path.GetDirectoryName(txt.Text);

					dropped = true;
					btnReadFiles.PerformClick();
				}
				else if(txt == txtNameFile)
				{
					MonitoringClipboard = false;
					NamesFromClipboard = false;
					if(btnReadNames.Enabled) btnReadNames.PerformClick();
				}
			}
		}

		private void txt_DragEnter(object sender, DragEventArgs e)
		{
			if(e.Data.GetDataPresent(typeof(String)) || e.Data.GetDataPresent(DataFormats.FileDrop))
			{
				if((e.AllowedEffect & DragDropEffects.Copy) == DragDropEffects.Copy)
				{
					e.Effect = DragDropEffects.Copy;
				}
			}
			else
			{
				e.Effect = DragDropEffects.None;
			}
		}

		#endregion clipboard

	}
}
