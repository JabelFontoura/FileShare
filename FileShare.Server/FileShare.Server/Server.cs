using FileShare.Common;
using FileShare.Common.SerializableActions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;

namespace FileShare.Server
{
    public class Server
    {
        public static List<Socket> Connections { get; set; }
        public string Directory { get; set; }

        public Server(string directory)
        {
            Directory = directory;
            Connections = new List<Socket>();
        }

        public void StartListening()
        {
            var ipAddress = IPAddress.Parse("127.0.0.1");
            var localEndPoint = new IPEndPoint(ipAddress, 8080);

            var listener = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                listener.Bind(localEndPoint);
                listener.Listen(10);

                while (true)
                {
                    var handler = listener.Accept();
                    if (!Connections.Any(socket => socket.Equals(handler)))
                    {
                        Connections.Add(handler);

                        new Thread(() =>
                        {
                            var client = handler;
                            while (true)
                            {
                                try
                                {
                                    var bytes = new Byte[1024];
                                    client.Receive(bytes);
                                    Console.WriteLine("Received " + bytes.Length + "from " + handler.RemoteEndPoint.ToString());

                                    var action = (FileAction)FileHelper.Deserialize(bytes);

                                    var incommingFileName = action.FileName;
                                    var incommingOldFileName = action.OldFileName;

                                    action.FileName = Directory + action.FileName;
                                    action.OldFileName = Directory + action.OldFileName;

                                    action.Run();

                                    action.FileName = incommingFileName;
                                    action.OldFileName = incommingOldFileName;

                                    SendActionToOthers(action, client);
                                }
                                catch (SocketException e)
                                {
                                    client.Shutdown(SocketShutdown.Both);
                                    client.Close();
                                    Connections.Remove(client);
                                    break;
                                }
                            }
                        }).Start();
                    }

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public void SendActionToOthers(FileAction action, Socket exception)
        {
            Connections.Where(socket => socket != exception).ToList().ForEach(connection =>
            {
                connection.Send(FileHelper.Serialize(action));
            });
        }
    }
}
