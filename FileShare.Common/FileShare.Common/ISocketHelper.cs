using System;
using System.Collections.Generic;
using System.Text;

namespace FileShare.Common
{
    public interface ISocketHelper
    {
        void Send(FileAction action);
    }
}
