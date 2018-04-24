using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace FileShare.Common.SerializableActions
{
    [Serializable]
    public class SyncFilesAction : FileAction
    {
        public List<FileAction> FileActions { get; set; }
        public string Directory { get; set; }

        public SyncFilesAction(string directory) : base(null, null, null)
        {
            Directory = directory;
            FileActions = new List<FileAction>();
            LoadFiles();
        }

        public override void Run()
        {
            FileActions.ForEach(action =>
            {
                action.FileName = FileName + action.FileName;
                action.Run();
            });
        }

        private void LoadFiles()
        {
            DirectoryInfo d = new DirectoryInfo(Directory);
            var files = d.GetFiles("*.*").ToList();

            files.ForEach(file =>
            {
                var content = File.ReadAllBytes(file.FullName);
                FileActions.Add(new ModifyFileAction(file.Name, content));
            });

        }
    }
}
