using Microsoft.VisualStudio.TestTools.UnitTesting;
using FileDataReader;
using System;
using System.Linq;

namespace OSSLibrary.UnitTest
{
    [TestClass]
    public class DataReaderTests
    {
        [TestMethod]
        public void DataReadFromFile_ExcelFile_firstRow()
        {
            //Arrange
            var filepath = @"C:\Users\akhil\Source\Repos\OSSLibrary\OSSLibrary.UnitTest\Files\SampleFile_firstRow.xlsx";
            IDataReader reader = new DataReader();
            var expected = new SampleClass()
            {
                Number = 1,
                DecimalNumber = 1.2m,
                Text = "hello",
                DateTime = new DateTime(2053, 5, 12)
            };

            //Act
            var data = reader.GetDataFromFile<SampleClass>(filepath, FileType.Excel).ToList();

            //Assert
            Assert.AreEqual(data.Count(), 1);
            Assert.AreEqual(data[0].Number, expected.Number);
            Assert.AreEqual(data[0].DecimalNumber, expected.DecimalNumber);
            Assert.AreEqual(data[0].Text, expected.Text);
            Assert.AreEqual(data[0].DateTime, expected.DateTime);
        } 
    }
}
