using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EpisodeRenamer
{
	public class DataGridViewCheckBoxHeaderCellEventArgs : EventArgs
	{
		bool mChecked;

		public DataGridViewCheckBoxHeaderCellEventArgs(bool bChecked) {
			mChecked = bChecked;
		}

		public bool Checked {
			get {
				return mChecked;
			}
		}
	}
}
