using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EpisodeRenamer
{
	public class DataGridViewCheckBoxHeaderCellEventArgs : EventArgs
	{
		bool _bChecked;

		public DataGridViewCheckBoxHeaderCellEventArgs(bool bChecked)
		{
			_bChecked = bChecked;
		}

		public bool Checked
		{
			get
			{
				return _bChecked;
			}
		}
	}
}
