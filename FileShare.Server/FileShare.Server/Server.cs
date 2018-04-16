using FileShare.Common;
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

                    bytes = ProcessConnection(handler);

                    var action = new DeleteFileAction("C:\\fileshare\\node2\\teste.txt");
                    
                    handler.Send(FileHelper.Serialize(action));
                    handler.Shutdown(SocketShutdown.Both);
                    handler.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public byte[] ProcessConnection(Socket handler)
        {
            var bytes = new Byte[1024];
            data = null;

            while (true)
            {
                bytes = new byte[1024];
                int bytesRec = handler.Receive(bytes);
                data += Encoding.ASCII.GetString(bytes, 0, bytesRec);
                if (data.IndexOf("<EOF>") > -1)
                    break;
            }

            return bytes;
        }

    }
}
