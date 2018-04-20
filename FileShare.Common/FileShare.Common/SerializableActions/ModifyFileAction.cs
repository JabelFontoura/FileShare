using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FileShare.Common.SerializableActions
{
    [Serializable]
    public class ModifyFileAction : FileAction
    {
        public ModifyFileAction(string fileName, byte[] fileChanges) : base(fileName, null, fileChanges) { }

        public override void Run()
        {
            if (FileChanges != null)
            {
                try
                {
                    File.WriteAllBytes(FileName, FileChanges);
                }
                catch (IOException err)
                {
                    Console.WriteLine(err.ToString());
                }

            }
        }
    }
}
