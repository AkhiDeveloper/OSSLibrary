using System;
using System.Collections.Generic;
using excel = Microsoft.Office.Interop.Excel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.IO;

namespace OSSLibrary.FileReader
{
    internal class ExcelDataReadStatergy
        : IDataReadStatergy
    {
        private readonly excel.Application _excelApp;

        public ExcelDataReadStatergy()
        {
            _excelApp = new excel.Application();
        }

        public void Dispose()
        {
            
        }

        public ICollection<T> ReadData<T>(string filePath)
        {
            IList<T> list = new List<T>();
            DirectoryInfo info = new DirectoryInfo(filePath);
            excel.Workbook workbook = _excelApp.Workbooks.Open(filePath);
            excel.Worksheet worksheet = (excel.Worksheet)workbook.Worksheets[1];

            try
            {

                //Opening Used Range Excel WorkSheet
                var range = worksheet.UsedRange;
                int totalColumns = range.Columns.Count;
                int totalRows = range.Rows.Count;

                //getting properties
                var properties = typeof(T).GetProperties();

                //Assigning Column Number
                IDictionary<PropertyInfo, int> propertyColumnPairs = new Dictionary<PropertyInfo, int>();
                for (int column = 1; column <= totalColumns; column++)
                {
                    try
                    {
                        var cellRange = (excel.Range)range.Cells[1, column];
                        string headerName = Convert.ToString(cellRange.Value2);
                        var property = properties
                            .Where(x => x.Name.CompareAlphaNumericOnly(headerName, false))
                            .FirstOrDefault();
                        propertyColumnPairs.Add(property, column);
                    }
                    catch
                    {
                        continue;
                    }
                }

                //Adding Data
                var loopResult = Parallel.For(2, totalRows + 1, (row) =>
                {
                    T data = Activator.CreateInstance<T>();
                    foreach (var propertyColumn in propertyColumnPairs)
                    {
                        try
                        {
                            String cellValue = Convert.ToString(range.Cells[row, propertyColumn.Value].Value);
                            cellValue.Trim();
                            //var converter = propertyColumn.Key;
                            var type = propertyColumn.Key.PropertyType;
                            
                            dynamic parsedValue = Convert.ChangeType(cellValue, type);
                            propertyColumn.Key.SetValue(data, parsedValue, null);
                        }
                        catch
                        {
                            continue;
                        }

                    }
                    list.Add(data);
                });
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                workbook.Close();
            }

        }
    }
}
