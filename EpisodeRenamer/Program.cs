using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace EpisodeRenamer
{
	static class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main(string[] args)
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			MainForm frmMain = null;

			if(args != null && args.Length > 0)
			{
				if(args[0] == "-l" || args[0] == "--log")
					frmMain = new MainForm(true);
			}

			if(frmMain == null)
				frmMain = new MainForm();

			Application.Run(frmMain);
		}
	}
}
