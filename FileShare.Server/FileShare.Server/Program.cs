using FileShare.Common;
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
            //    new DirectoryWatcher(new Teste()).Watch("C:\\fileshare\\node1", ref connected);

            //}).Start();

            //Console.ReadKey();
            //connected = false;

        }

    }
    internal class Teste : ISocketHelper
    {
        public void Send(FileAction action)
        {
            throw new System.NotImplementedException();
        }
    }

}