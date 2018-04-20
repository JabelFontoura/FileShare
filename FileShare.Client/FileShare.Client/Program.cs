using FileShare.Common;
using FileShare.Server;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace FileShare.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new Client();
            var connected = true;
            new DirectoryWatcher(client).Watch("C:\\fileshare\\node1", ref connected);
        }
    }
}
