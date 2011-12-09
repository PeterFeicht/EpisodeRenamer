using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace EpisodeRenamer
{
	static class Program
	{
		[DllImport("Kernel32.dll")]
		public static extern bool AttachConsole(int pid);

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

				if(args[0] == "-h" || args[0] == "--help")
				{
					AttachConsole(-1);
					Console.WriteLine();
					Console.WriteLine("usage: EpisodeRenamer [options]\n\noptions are:\n  -l | --log\n    Create a log file and write debugging information.");
					Console.WriteLine("  -h | --help\n    Display this help message.");
					return;
				}
			}

			if(frmMain == null)
				frmMain = new MainForm();

			Application.Run(frmMain);
		}
	}
}
