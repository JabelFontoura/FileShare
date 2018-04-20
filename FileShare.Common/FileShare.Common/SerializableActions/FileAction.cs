using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace FileShare.Common
{
    [Serializable]
    public abstract class FileAction
    {
        public string FileName { get; set; }
        public byte[] FileChanges { get; set; }
        public string OldFileName { get; set; }

        public abstract void Run();

        public FileAction(string fileName, string oldFileName, byte[] fileChanges)
        {
            FileName = fileName;
            OldFileName = oldFileName;
            FileChanges = fileChanges;
        }
    }
}
