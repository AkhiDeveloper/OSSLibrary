using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileDataReader
{
    public class DataReader
        : IDataReader
    {
        public void Dispose()
        {
            
        }

        public ICollection<T> GetDataFromFile<T>(string filePath, FileType fileType)
        {
            IDataReadStatergy statergy;
            switch (fileType)
            {
                case FileType.Excel:
                    statergy = new ExcelDataReadStatergy();
                    break;             
                default:
                    throw new NotImplementedException();
            }
            return statergy.ReadData<T>(filePath);
        }
    }
}
