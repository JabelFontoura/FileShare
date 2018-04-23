using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace FileShare.Common.SerializableActions
{
    [Serializable]
    public class ModifyFileAction : FileAction
    {
        public ModifyFileAction(string fileName, byte[] fileChanges) : base(fileName, null, fileChanges) { }

        public override void Run()
        {
            var teste = ContentHasChanged();
            if (FileChanges.Length > 0 && ContentHasChanged())
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

        private bool ContentHasChanged()
        {
            try
            {
                var data = File.ReadAllBytes(FileName);

                return !data.SequenceEqual(FileChanges);
            }
            catch (IOException e)
            {
                return false;
            }
        }
    }
}
