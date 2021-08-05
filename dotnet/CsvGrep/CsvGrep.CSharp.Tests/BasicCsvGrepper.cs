using System;
using System.IO;
using System.Linq;
using CsvGrep.CSharp.CsvGreppers;
using Xunit;

namespace CsvGrep.CSharp.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void BasicCsvGrepper_WithCorrectData_ReturnCorrectResult()
        {
            // arrange
            var text = @"1,Rossi,Fabio,01/06/1990;
2,Gialli,Alessandro,02/07/1989;
3,Verdi,Alberto,03/08/1987;";

            var dir = Path.GetTempPath();
            var fileName = Path.GetRandomFileName();
            fileName = Path.ChangeExtension(fileName, ".csv");
            var filePath = Path.Combine(dir, fileName);
            File.WriteAllText(filePath, text);

            // act
            var grepper = new BasicCsvGrepper();
            var result = grepper.GrepOnFile(filePath, 2, "Alberto");

            // assert
            Assert.Single(result);
            Assert.Equal("3,Verdi,Alberto,03/08/1987;", result.First());
            
            File.Delete(filePath);
        }
        
        [Fact]
        public void BasicCsvGrepper_WithCorrectData_ButIncorrectArgs_ThrowExceptions()
        {
            // arrange
            var text = @"1,Rossi,Fabio,01/06/1990;
2,Gialli,Alessandro,02/07/1989;
3,Verdi,Alberto,03/08/1987;";

            var dir = Path.GetTempPath();
            var fileName = Path.GetRandomFileName();
            fileName = Path.ChangeExtension(fileName, ".csv");
            var filePath = Path.Combine(dir, fileName);
            File.WriteAllText(filePath, text);

            // act & assert
            var grepper = new BasicCsvGrepper();
            Assert.ThrowsAny<Exception>(() => grepper.GrepOnFile(filePath, -1, null)); 

            // assert
            
            File.Delete(filePath);
        }
    }
}