using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FileShare.Common.SerializableActions
{
    [Serializable]
    public class CreateFileAction : IFileAction
    {
        public string FileName { get; set; }

        public CreateFileAction(string fileName)
        {
            FileName = fileName;
        }

        public void Run()
        {
            if (!File.Exists(FileName))
                File.Create(FileName);
        }
    }
}
