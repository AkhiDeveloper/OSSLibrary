using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileDataReader
{
    public interface IDataReader
        : IDisposable
    {
        ICollection<T> GetDataFromFile<T>(string filePath, FileType fileType);
    }

    public enum FileType
    {
        Excel,
    }

}
