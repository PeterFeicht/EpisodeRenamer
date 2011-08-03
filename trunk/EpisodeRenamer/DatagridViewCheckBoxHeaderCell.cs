using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Windows.Forms.VisualStyles;

namespace EpisodeRenamer
{
	public delegate void CheckBoxClickedEventHandler(object sender, DataGridViewCheckBoxHeaderCellEventArgs e);

	/// <summary>
	/// Source: http://techisolutions.blogspot.com/2008/02/datagridview-checkbox-select-all.html
	/// </summary>
	class DatagridViewCheckBoxHeaderCell : DataGridViewColumnHeaderCell
	{
		Point checkBoxLocation;
		Size checkBoxSize;
		bool _checked = false;
		Point _cellLocation = new Point();
		CheckBoxState _cbState = CheckBoxState.UncheckedNormal;

		public event CheckBoxClickedEventHandler OnCheckBoxClicked;

		public DatagridViewCheckBoxHeaderCell()
		{
		}

		protected override void Paint(Graphics graphics,
			Rectangle clipBounds,
			Rectangle cellBounds,
			int rowIndex,
			DataGridViewElementStates dataGridViewElementState,
			object value,
			object formattedValue,
			string errorText,
			DataGridViewCellStyle cellStyle,
			DataGridViewAdvancedBorderStyle advancedBorderStyle,
			DataGridViewPaintParts paintParts)
		{
			base.Paint(graphics, clipBounds, cellBounds, rowIndex, dataGridViewElementState, value,
				formattedValue, errorText, cellStyle, advancedBorderStyle, paintParts);
			
			Point p = new Point();
			Size s = CheckBoxRenderer.GetGlyphSize(graphics, CheckBoxState.UncheckedNormal);

			p.X = cellBounds.Location.X + (cellBounds.Width / 2) - (s.Width / 2);
			p.Y = cellBounds.Location.Y + (cellBounds.Height / 2) - (s.Height / 2);
			_cellLocation = cellBounds.Location;
			checkBoxLocation = p;
			checkBoxSize = s;

			if(_checked)
				_cbState = CheckBoxState.CheckedNormal;
			else
				_cbState = CheckBoxState.UncheckedNormal;

			CheckBoxRenderer.DrawCheckBox(graphics, checkBoxLocation, _cbState);
		}

		protected override void OnMouseClick(DataGridViewCellMouseEventArgs e)
		{
			Point p = new Point(e.X + _cellLocation.X, e.Y + _cellLocation.Y);

			if((p.X >= checkBoxLocation.X) && (p.X <= checkBoxLocation.X + checkBoxSize.Width) &&
				(p.Y >= checkBoxLocation.Y) && (p.Y <= checkBoxLocation.Y + checkBoxSize.Height))
			{
				_checked = !_checked;
				DataGridViewCheckBoxHeaderCellEventArgs ev = new DataGridViewCheckBoxHeaderCellEventArgs(_checked);

				if(OnCheckBoxClicked != null)
				{
					OnCheckBoxClicked(this, ev);
					this.DataGridView.InvalidateCell(this);
				}
			}
			base.OnMouseClick(e);
		}
	}
}
