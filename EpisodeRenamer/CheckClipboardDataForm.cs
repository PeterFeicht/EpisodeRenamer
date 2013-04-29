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
	public partial class CheckClipboardDataForm : Form
	{
		public CheckClipboardDataForm()
		{
			InitializeComponent();
		}

		public string Data
		{
			get
			{
				return txtData.Text;
			}
			set
			{
				txtData.Text = value;
			}
		}

		private void txtData_KeyDown(object sender, KeyEventArgs e)
		{
			switch(e.KeyData)
			{
				case Keys.A | Keys.Control:
					txtData.SelectAll();
					break;

				case Keys.Enter | Keys.Control:
					btnOK.PerformClick();
					break;

				default:
					return;
			}

			e.SuppressKeyPress = true;
		}

		private void CheckClipboardDataForm_Load(object sender, EventArgs e)
		{
			if(Owner != null)
				Location = new Point(Owner.Left + (Owner.Width - Width) / 2, Owner.Top + (Owner.Height - Height) / 2);
		}
	}
}
