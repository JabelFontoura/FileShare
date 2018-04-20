using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FileShare.Common.SerializableActions
{
    [Serializable]
    public class RenameFileAction : FileAction
    {
        public RenameFileAction(string fileName, string oldFileName) : base(fileName, oldFileName, null) { }

        public override void Run()
        {
            if (File.Exists(OldFileName) && FileName != OldFileName)
                File.Move(OldFileName, FileName);
        }
    }
}
