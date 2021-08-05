using System;
using System.IO;
using System.Linq;

namespace CsvGrep.CSharp.CsvGreppers
{
    /// <summary>
    /// Basic ICsvGrepper implementation
    /// </summary>
    public class BasicCsvGrepper : ICsvGrepper
    {
        // fields
        private readonly CsvConfigs _configs;

        // .ctors
        public BasicCsvGrepper() : this(CsvConfigs.Default)
        {
        }
        
        public BasicCsvGrepper(CsvConfigs configs)
        {
            _configs = configs ?? throw new ArgumentNullException(nameof(configs));
        }

        // public contract
        public string[] GrepOnFile(string filePath, int columnIndex, string columnValue)
        {
            // read file
            Validate.FilePath(filePath);
            var rows = File.ReadAllLines(filePath)
                .Skip(_configs.HasHeader ? 1 : 0)
                .Select(r => r.Replace(";", ""))
                .ToArray();
            
            // read rows
            Validate.Rows(rows);

            var separator = _configs.Separator;
            var columnsCount = _GetColumnsCount(rows.First(), separator);
            Validate.FilterParams(columnIndex, columnValue, columnsCount);

            // filter rows
            var result = rows.Where(row => string.Equals(row.Split(separator, StringSplitOptions.TrimEntries)[columnIndex], columnValue, StringComparison.InvariantCultureIgnoreCase))
                .Select(r => $"{r};")
                .ToArray();

            return result;
        }

        // private implementations
        private int _GetColumnsCount(string row, char separator)
        {
            return row.ToCharArray().Count(separator.Equals) + 1;
        }
    }
}