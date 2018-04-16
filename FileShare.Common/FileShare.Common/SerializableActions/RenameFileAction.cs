using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FileShare.Common.SerializableActions
{
    [Serializable]
    public class RenameFileAction : IFileAction
    {
        public string FileName { get; set; }
        public string OldFileName { get; set; }

        public RenameFileAction(string fileName)
        {
            FileName = fileName;
        }

        public void Run()
        {
            if (File.Exists(OldFileName))
                File.Move(OldFileName, FileName);
        }
    }
}
