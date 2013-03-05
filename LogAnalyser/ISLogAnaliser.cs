using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace LogAnalizer
{
    class ISLogAnaliser
    {
        const string StartIpMarker = "] for ";
        const string EndIpMarker = " took ";

        string _logFilename;

        SortedDictionary<string, int> _webRequests = new SortedDictionary<string, int>(); // ip - num requests

        public ISLogAnaliser(string logFilename)
        {
            _logFilename = logFilename;
        }

        public void Execute()
        {
            using (StreamReader sr = new StreamReader(_logFilename))
            {
                String logLine = string.Empty;
                while ((logLine = sr.ReadLine()) != null)
                {
                    if (logLine.Contains(EndIpMarker))
                    {
                        GetIpAndAddToWebRequests(logLine);


                    }
                }
            }
        }

        public int GetNumUniqIps()
        {
            return _webRequests.Count;
        }

        public SortedDictionary<string, int> GetWebRequestIps()
        {
            return _webRequests;
        }

        #region Private

        private void GetIpAndAddToWebRequests(string logLine)
        {
            int ipStart = logLine.IndexOf(StartIpMarker) + StartIpMarker.Length;
            int ipEnd = logLine.IndexOf(':', ipStart);
            string ip = logLine.Substring(ipStart, ipEnd - ipStart);

            if (!_webRequests.ContainsKey(ip))
                _webRequests.Add(ip, 1);
            else
                _webRequests[ip]++;
        }


        #endregion

    }
}
