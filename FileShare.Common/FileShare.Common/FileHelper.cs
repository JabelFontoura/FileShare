using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace FileShare.Common
{
    public class FileHelper
    {
        public static byte[] Serialize(FileAction action)
        {
            if (action == null)
                return null;

            var bf = new BinaryFormatter();
            var ms = new MemoryStream();

            bf.Serialize(ms, action);
            ms.Close();

            return ms.ToArray();
        }

        public static object Deserialize(byte[] arrBytes)
        {
            var ms = new MemoryStream(8192);
            var bf = new BinaryFormatter();

            ms.Write(arrBytes, 0, arrBytes.Length);
            ms.Seek(0, SeekOrigin.Begin);

            return (object) bf.Deserialize(ms);
        }



    }
}
