using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;

namespace FileShare.Server
{
    class Program
    {
        static void Main(string[] args)
        {
            new Server().StartListening();
            var connected = true;

            //new Thread(() =>
            //{
            //    new DirectoryWatcher().Watch("C:\\fileshare\\node1", ref connected);

            //}).Start();

            //Console.ReadKey();
            //connected = false;

        }

    }
}

