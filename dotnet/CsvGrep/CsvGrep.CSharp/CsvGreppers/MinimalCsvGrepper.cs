using System;
using System.IO;
using System.Linq;

namespace CsvGrep.CSharp.CsvGreppers
{
    /// <summary>
    /// Minimalistic - one liner implementation of ICsvGrepper
    /// </summary>
    public class MinimalCsvGrepper : ICsvGrepper
    {
        // fields
        private readonly CsvConfigs _configs;

        // .ctors
        public MinimalCsvGrepper() : this(CsvConfigs.Default)
        {
        }
        
        public MinimalCsvGrepper(CsvConfigs configs)
        {
            _configs = configs ?? throw new ArgumentNullException(nameof(configs));
        }
        
        public string[] GrepOnFile(string filePath, int columnIndex, string columnValue)
        {
            var result = File.ReadAllLines(filePath)
                .Skip(_configs.HasHeader ? 1 : 0)
                .Where(row =>
                    string.Equals(row.Replace(";", "").Split(_configs.Separator, StringSplitOptions.TrimEntries)[columnIndex],
                        columnValue, StringComparison.InvariantCultureIgnoreCase))
                .ToArray();
            return result;
        }
    }
}