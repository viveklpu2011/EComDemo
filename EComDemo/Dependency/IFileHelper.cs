using System;
using System.Collections.Generic;
using System.Text;

namespace EComDemo.Dependency
{
    public interface IFileHelper
    {
        string GetLocalFilePath(string filename);
    }
}
