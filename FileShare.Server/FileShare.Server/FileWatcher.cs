using FileShare.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Permissions;
using System.Text;

namespace FileShare.Server
{
    public class DirectoryWatcher
    {
        private string FilePath = "C:\\fileshare\\node2\\";

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
            var fileChanges = File.ReadAllBytes(e.FullPath);

            try
            {
                File.WriteAllBytes(FilePath + e.Name, fileChanges);
            }
            catch (IOException err)
            {
                Console.WriteLine(err.ToString());
            }
        }

        private void OnCreate(object source, FileSystemEventArgs e)
        {
            if (!File.Exists(FilePath + e.Name))
                File.Create(FilePath + e.Name);
        }

        private void OnDelete(object source, FileSystemEventArgs e)
        {
            IFileAction action = new DeleteFileAction(FilePath + e.Name);
            action.Run();
        }

        private void OnRenamed(object source, RenamedEventArgs e)
        {
            if (File.Exists(FilePath + e.OldName))
                File.Move(FilePath + e.OldName, FilePath + e.Name);
        }
    }
}
