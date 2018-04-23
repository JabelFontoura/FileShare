﻿using FileShare.Common;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace FileShare.Client
{
    public class Client : ISocketHelper
    {
        public string Directory { get; set; }
        public Socket Sender { get; set; }
        public bool Connected { get; set; }

        public Client(string directory)
        {
            Directory = directory + "\\";

            var ipAddress = IPAddress.Parse("127.0.0.1");
            var remoteEP = new IPEndPoint(ipAddress, 8080);

            Sender = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            Sender.Connect(remoteEP);

            new Thread(() =>
            {
                while (true)
                {
                    var bytes = new byte[1024];
                    try
                    {
                        Sender.Receive(bytes);
                        var receivedAction = (FileAction)FileHelper.Deserialize(bytes);
                        receivedAction.FileName = Directory + receivedAction.FileName;
                        receivedAction.OldFileName = Directory + receivedAction.OldFileName;

                        receivedAction.Run();

                    }
                    catch (SocketException e)
                    {
                        Sender.Shutdown(SocketShutdown.Both);
                        Sender.Close();
                        break;
                    }
                }
            }).Start();
        }

        public void Send(FileAction action)
        {
            try
            {
                Sender.Send(FileHelper.Serialize(action));

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
    }
}
