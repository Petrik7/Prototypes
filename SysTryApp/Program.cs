using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Threading;

namespace SysTryApp
{
	static class Program
	{
		private static bool _createdNew = true;
		private static Mutex _singelAppMutex = null;

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
            _singelAppMutex = new Mutex(true, "SysTryFormWhichShowsEmailIconInSystemTry", out _createdNew);
			if (!_createdNew)
			{
				return;
			}

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new SysTryForm());
		}
	}
}