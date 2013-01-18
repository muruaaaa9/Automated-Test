using System;
using System.Net;
using System.Net.Sockets;

namespace Journey.Test.Support
{
    public class NetworkUtil
    {
        public string GetLocalIpAddress()
        {
            string localIpAddress = String.Empty;
            var hostEntry = Dns.GetHostEntry(Dns.GetHostName());
            if (hostEntry.HostName.Contains("PEG-PC")) return "";  //Check to see running under localhost
            foreach (var ipAddress in hostEntry.AddressList)
            {
                if (ipAddress.AddressFamily == AddressFamily.InterNetwork)
                {
                    localIpAddress = ipAddress.ToString();
                }
            }
            return localIpAddress;
        }
    }
}