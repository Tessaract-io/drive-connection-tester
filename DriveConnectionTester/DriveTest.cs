using System;
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;

namespace DriveConnectionTester
{
    internal class DriveTest
    {
        private int ConnectionTimeout = 5000;
        public bool TestConnection(string domain)
        {
            IPAddress ip_address = GetIPv4(domain);
            if (ip_address == null)
            {
                return false;
            }

            Socket socket = new Socket(ip_address.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            IAsyncResult result = socket.BeginConnect(ip_address, 445, null, null);

            bool _ = result.AsyncWaitHandle.WaitOne(ConnectionTimeout, true);
            if (socket.Connected)
            {
                socket.Close();
                return true;
            } else
            {
                socket.Close();
                return false;
            }
        }

        private IPAddress GetIPv4(string domain)
        {
            if (domain == null || domain == string.Empty)
            {
                MessageBox.Show("Drive domain cannot be empty", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }   
            // if domain contains http:// or https://, alert user and return null
            if (domain.Contains("http://") || domain.Contains("https://"))
            {
                MessageBox.Show("Drive domain should not contain https:// or http://", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            // common input: \\ut-drive.tessaract.io\BFPL-UT
            // common output: ut-drive.tessaract.io
            domain = domain.Replace(@"\\", "");
            domain = domain.Split('\\')[0];

            // check if domain is an IP address, if so, return it
            IPAddress ip;
            if (IPAddress.TryParse(domain, out ip))
            {
                return ip;
            }

            IPAddress ipAddress;
            try
            {
                IPHostEntry host = Dns.GetHostEntry(domain);
                ipAddress = host.AddressList[0];
            } catch (Exception e)
            {
                MessageBox.Show("Error: " + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

            return ipAddress;
        }
    }
}
