using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FileShare.Common.SerializableActions
{
    [Serializable]
    public class ModifyFileAction : IFileAction
    {
        public string FileName { get; set; }

        public ModifyFileAction(string fileName)
        {
            FileName = fileName;
        }

        public void Run()
        {
            var fileChanges = File.ReadAllBytes(FileName);

            try
            {
                File.WriteAllBytes(FileName, fileChanges);
            }
            catch (IOException err)
            {
                Console.WriteLine(err.ToString());
            }
        }
    }
}
