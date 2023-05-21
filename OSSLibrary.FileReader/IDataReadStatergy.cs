using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OSSLibrary.FileReader
{
    internal interface IDataReadStatergy
        : IDisposable
    {
        ICollection<T> ReadData<T>(string filePath);
    }
}
