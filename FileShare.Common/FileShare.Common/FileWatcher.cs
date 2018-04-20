using FileShare.Common;
using FileShare.Common.SerializableActions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Permissions;
using System.Text;

namespace FileShare.Server
{
    public class DirectoryWatcher
    {
        private string FilePath { get; set; }
        public ISocketHelper SocketHelper { get; set; }

        public DirectoryWatcher(ISocketHelper socketHelper)
        {
            SocketHelper = socketHelper;
        }

        public void Watch(string path, ref bool connected)
        {
            var watcher = new FileSystemWatcher();
            watcher.Path = path;
            watcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.DirectoryName | NotifyFilters.Size;

            watcher.Changed += new FileSystemEventHandler(OnChanged);
            watcher.Created += new FileSystemEventHandler(OnCreate);
            watcher.Deleted += new FileSystemEventHandler(OnDelete);
            watcher.Renamed += new RenamedEventHandler(OnRenamed);

            watcher.EnableRaisingEvents = true;
            while (connected) ;
        }

        private void OnChanged(object source, FileSystemEventArgs e)
        {
            var tryAgain = true;
            while(tryAgain)
            {
                try
                {
                    var fileChanges = File.ReadAllBytes(e.FullPath);
                    SocketHelper.Send(new ModifyFileAction(e.Name, fileChanges));
                    tryAgain = false;
                }
                catch (IOException ioe)
                {
                    Console.WriteLine(ioe.Message);
                }
            }

        }

        private void OnCreate(object source, FileSystemEventArgs e)
        {
            SocketHelper.Send(new CreateFileAction(e.Name));
        }

        private void OnDelete(object source, FileSystemEventArgs e)
        {
            SocketHelper.Send(new DeleteFileAction(e.Name));
        }

        private void OnRenamed(object source, RenamedEventArgs e)
        {
            SocketHelper.Send(new RenameFileAction(e.Name, e.OldName));
        }
    }
}
