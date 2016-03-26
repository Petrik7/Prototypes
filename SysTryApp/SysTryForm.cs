using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace SysTryApp
{
	public partial class SysTryForm : Form
	{
		[DllImport("USER32.DLL")]
		public static extern bool SetForegroundWindow(IntPtr hWnd);

		[DllImport("user32.dll")]
		private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        private NotifyIcon _trayIcon;
        
        public SysTryForm()
		{
			InitializeComponent();
            ShowEmailIconInSysTray(CreateContextMenu());
		}

        private void ShowEmailIconInSysTray(ContextMenu trayMenu)
        {
            _trayIcon = new NotifyIcon();
            _trayIcon.Text = "EmailNotification";
            _trayIcon.Icon = new Icon(Properties.Resources.MAIL, 32, 32);
            _trayIcon.DoubleClick += new EventHandler(trayIcon_DoubleClick);

            _trayIcon.ContextMenu = trayMenu;
            _trayIcon.Visible = true;
        }

        private ContextMenu CreateContextMenu()
        {
            ContextMenu trayMenu = new ContextMenu();
            trayMenu.MenuItems.Add("Show Outlook and exit", ShowAppAndExit);
            trayMenu.MenuItems.Add("Exit", Exit);
            return trayMenu;
        }
        
        protected static void BringProcessToFront(string title)
		{
			Process[] processes = Process.GetProcessesByName("Outlook");
            if (processes.Length > 0)
			{
                ShowWindow(processes[0].MainWindowHandle, 9);//SW_RESTORE
                SetForegroundWindow(processes[0].MainWindowHandle);
			}
		}

		protected override void OnLoad(EventArgs e)
		{
			Visible = false;
			ShowInTaskbar = false;

			base.OnLoad(e);
		}

        protected void trayIcon_DoubleClick(object sender, EventArgs e)
        {
            ShowAppAndExit(sender, e);
        }
        
        private void ShowAppAndExit(object sender, EventArgs e)
		{
            Process.Start("OUTLOOK.exe", "/recycle");
			Application.Exit();
		}

        private void Exit(object sender, EventArgs e)
        {
            Application.Exit();
        }	
	}
}