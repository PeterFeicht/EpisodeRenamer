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

		public string Description
		{
			get
			{
				return label1.Text;
			}
			set
			{
				label1.Text = value;
			}
		}

		public bool HasCancel
		{
			get
			{
				return btnCancel.Visible;
			}
			set
			{
				btnCancel.Visible = value;
				// Move OK button to the middle/side
				if(value)
				{
					btnOK.Left = ClientSize.Width - btnCancel.Left - btnOK.Width;
				}
				else
				{
					btnOK.Left = (ClientSize.Width - btnOK.Width) / 2;
				}
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

		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			if(keyData == Keys.Escape)
			{
				Close();
				return true;
			}
			return base.ProcessCmdKey(ref msg, keyData);
		}
	}
}
