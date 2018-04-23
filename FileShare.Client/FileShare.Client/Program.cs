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
            Console.WriteLine("Digite um diretorio pra sincronizar seus arquivos: ");
            var directory = "C:\\fileshare\\" + Console.ReadLine();
            var client = new Client(directory);
            var connected = true;
            new DirectoryWatcher(client).Watch(directory, ref connected);
        }
    }
}
