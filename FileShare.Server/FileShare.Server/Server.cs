using FileShare.Common;
using FileShare.Common.SerializableActions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace FileShare.Server
{
    public class Server
    {
        private string data = null;

        public void StartListening()
        {
            var bytes = new Byte[1024];
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

                    bytes = new byte[1024];
                    handler.Receive(bytes);

                    var action = (FileAction)FileHelper.Deserialize(bytes);
                    action.FileName = "C:\\fileshare\\node2\\" + action.FileName;
                    action.OldFileName = "C:\\fileshare\\node2\\" + action.OldFileName;

                    action.Run();
                    handler.Send(new byte[2] { 0, 1 } );
                    //handler.Send(FileHelper.Serialize(action));
                    handler.Shutdown(SocketShutdown.Both);
                    handler.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}
