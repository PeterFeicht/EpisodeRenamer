using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace EpisodeRenamer
{
	public partial class ChangePrefixForm : Form
	{
		Point p = new Point(2, 15);

		public ChangePrefixForm()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Gets or sets the prefix for season numbers.
		/// </summary>
		public string SeasonPrefix
		{
			get
			{
				return txtSeasonPrefix.Text;
			}
			set
			{
				txtSeasonPrefix.Text = value;
			}
		}

		/// <summary>
		/// Gets or sets the prefix for episode numbers.
		/// </summary>
		public string EpisodePrefix
		{
			get
			{
				return txtEpisodePrefix.Text;
			}
			set
			{
				txtEpisodePrefix.Text = value;
			}
		}

		/// <summary>
		/// Gets or sets the separator between series name and season number or episode number and episode name.
		/// </summary>
		public string Separator
		{
			get
			{
				return txtSeparator.Text;
			}
			set
			{
				txtSeparator.Text = value;
			}
		}

		/// <summary>
		/// Gets or sets the minimum number of digits used for season and episode numbers.
		/// </summary>
		public int EpisodeNumberDigits
		{
			get
			{
				return (int)numDigits.Value;
			}
			set
			{
				if(value > numDigits.Maximum)
					numDigits.Value = numDigits.Maximum;
				else if(value < numDigits.Minimum)
					numDigits.Value = numDigits.Minimum;
				else
					numDigits.Value = value;
			}
		}

		void UpdateExample()
		{
			StringBuilder sb = new StringBuilder("Series");
			sb.Append(Separator).Append(SeasonPrefix);
			sb.Append((2).ToString("D" + EpisodeNumberDigits.ToString())).Append(EpisodePrefix);
			sb.Append((15).ToString("D" + EpisodeNumberDigits.ToString()));
			sb.Append(Separator).Append("Episode Name");

			lblExample.Text = sb.Append(".mkv").ToString();
		}

		private void TextBox_TextChanged(object sender, EventArgs e)
		{
			UpdateExample();
		}

		private void ChangePrefixForm_Load(object sender, EventArgs e)
		{
			if(Owner != null)
				Location = new Point(Owner.Left + (Owner.Width - Width) / 2, Owner.Top + (Owner.Height - Height) / 2);
			UpdateExample();
		}

		private void numDigits_ValueChanged(object sender, EventArgs e)
		{
			UpdateExample();
		}
	}
}
