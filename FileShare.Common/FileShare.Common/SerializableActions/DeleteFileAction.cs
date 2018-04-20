using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Text;

namespace FileShare.Common
{
    [Serializable]
    public class DeleteFileAction : FileAction
    {
        public DeleteFileAction(string fileName) : base(fileName, null, null) { }

        public override void Run()
        {
            if (File.Exists(FileName))
                File.Delete(FileName);
        }
    }
}
