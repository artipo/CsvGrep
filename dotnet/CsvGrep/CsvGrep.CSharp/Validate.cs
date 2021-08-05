using System;
using System.IO;
using System.Linq;

namespace CsvGrep.CSharp
{
    public static class Validate
    {
        public static void FilePath(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
            {
                throw new ArgumentNullException(nameof(filePath));
            }
            
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("File not found", filePath);
            }
            
            if (!string.Equals(Path.GetExtension(filePath), ".csv", StringComparison.InvariantCultureIgnoreCase))
            {
                throw new FileLoadException("File is not a csv", filePath);
            }
        }
        
        public static void Rows(string[] rows)
        {
            if (rows is null)
            {
                throw new ArgumentNullException(nameof(rows));
            }

            if (!rows.Any())
            {
                throw new InvalidDataException("There are no csv rows");
            }
        }

        public static void FilterParams(int columnIndex, string columnValue, int columnsCount)
        {
            if (string.IsNullOrWhiteSpace(columnValue))
            {
                throw new ArgumentNullException(nameof(columnValue));
            }
            
            if (columnIndex < 0 || columnIndex > columnsCount)
            {
                throw new InvalidOperationException("Column index must be between 0 and columns count");
            }
        }
    }
}