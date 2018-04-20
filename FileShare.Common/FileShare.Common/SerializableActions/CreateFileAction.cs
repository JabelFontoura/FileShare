using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FileShare.Common.SerializableActions
{
    [Serializable]
    public class CreateFileAction : FileAction
    {
        public CreateFileAction(string fileName) : base(fileName, null, null) { }

        public override void Run()
        {
            if (!File.Exists(FileName))
            {
                var newFile = File.Create(FileName);
                newFile.Close();
            }
        }
    }
}
