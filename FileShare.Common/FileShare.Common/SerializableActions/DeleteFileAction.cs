using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Text;

namespace FileShare.Common
{
    [Serializable]
    public class DeleteFileAction : IFileAction
    {
        public string FileName { get; set; }

        public DeleteFileAction(string fileName)
        {
            FileName = fileName;
        }

        public void Run()
        {
            if (File.Exists(FileName))
                File.Delete(FileName);
        }
    }
}
