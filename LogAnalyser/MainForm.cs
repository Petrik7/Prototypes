using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace LogAnalizer
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            tbLogFolder.Text = @"c:\";
        }

        SortedDictionary<string, int> _webRequestsAccumulated = new SortedDictionary<string, int>(); // ip - num requests

        private void btSelectLogFolder_Click(object sender, EventArgs e)
        {
            if (DialogResult.OK == LogFolderBrowserDialog.ShowDialog())
            {
                tbLogFolder.Text = LogFolderBrowserDialog.SelectedPath;
            }
        }

        private void btRun_Click(object sender, EventArgs e)
        {
            string[] logFilePaths = Directory.GetFiles(tbLogFolder.Text, "is*.txt");
            foreach (string logFile in logFilePaths)
            {
                ISLogAnaliser isLogAnaliser = new ISLogAnaliser(logFile);
                isLogAnaliser.Execute();
                lbLogFiles.Items.Add(logFile + " : " + isLogAnaliser.GetNumUniqIps());

                CollectionHelpers.MergeFirstIntoSecond(isLogAnaliser.GetWebRequestIps(), _webRequestsAccumulated);
            }

            PrintResults();
        }

        private void PrintResults()
        {
            int numTotalRequests = 0;
            lblNumberOfIPs.Text = _webRequestsAccumulated.Count.ToString();
            foreach (KeyValuePair<string, int> ipCount in _webRequestsAccumulated)
            {
                numTotalRequests += ipCount.Value;
                lbIPs.Items.Add(ipCount.Key + " : " + ipCount.Value.ToString());
            }

            lblNumberOfTotalRequests.Text = numTotalRequests.ToString();
        }
    }
}