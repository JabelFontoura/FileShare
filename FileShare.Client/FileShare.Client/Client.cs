using FileShare.Common;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace FileShare.Client
{
    public class Client : ISocketHelper
    {
        public void Send(FileAction action)
        {
            var bytes = new byte[1024];
            try
            {
                var ipAddress = IPAddress.Parse("127.0.0.1");
                var remoteEP = new IPEndPoint(ipAddress, 8080);
                var sender = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                try
                {
                    sender.Connect(remoteEP);

                    Console.WriteLine("Socket connected to {0}", sender.RemoteEndPoint.ToString());

                    sender.Send(FileHelper.Serialize(action));

                    sender.Receive(bytes);

                    sender.Shutdown(SocketShutdown.Both);
                    sender.Close();

                }
                catch (ArgumentNullException ane)
                {
                    Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
                }
                catch (SocketException se)
                {
                    Console.WriteLine("SocketException : {0}", se.ToString());
                }
                catch (Exception e)
                {
                    Console.WriteLine("Unexpected exception : {0}", e.ToString());
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

    }
}
