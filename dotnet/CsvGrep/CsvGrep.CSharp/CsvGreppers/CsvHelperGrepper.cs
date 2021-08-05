using System;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;
using CsvHelper.Configuration;

namespace CsvGrep.CSharp.CsvGreppers
{
    /// <summary>
    /// ICsvGrepper using nuget package CsvHelper
    /// </summary>
    public class CsvHelperGrepper : ICsvGrepper
    {
        // private classes
        private class CsvRow
        {
            public int Num { get; set; }
            public string LastName { get; set; }
            public string FirstName { get; set; }
            public string DateOfBirth { get; set; }

            public string this[int index] =>
                index switch
                {
                    0 => Num.ToString(),
                    1 => LastName,
                    2 => FirstName,
                    3 => DateOfBirth,
                    _ => string.Empty
                };

            public string ToCsvRow(string sep)
            {
                return $"{Num}{sep}{LastName}{sep}{FirstName}{sep}{DateOfBirth};";
            }
        }
        
        private sealed class CsvRowMap : ClassMap<CsvRow>
        {
            public CsvRowMap()
            {
                Map(r => r.Num).Index(0);
                Map(r => r.LastName).Index(1);
                Map(r => r.FirstName).Index(2);
                Map(r => r.DateOfBirth)
                    .Index(3)
                    .Convert(e =>
                        e.Row.TryGetField<string>(3, out var text)
                            ? text.Replace(";", "")
                            : string.Empty);
            }
        }
        
        // fields
        private readonly CsvConfigs _configs;
        
        // .ctors
        public CsvHelperGrepper() : this(CsvConfigs.Default)
        {
        }
        
        public CsvHelperGrepper(CsvConfigs configs)
        {
            _configs = configs ?? throw new ArgumentNullException(nameof(configs));
        }
        
         // public contract
        public string[] GrepOnFile(string filePath, int columnIndex, string columnValue)
        {
            Validate.FilePath(filePath);

            var rows = _ParseCsv(filePath, _configs);

            var result = rows.Where(r => string.Equals(columnValue, r[columnIndex], StringComparison.InvariantCultureIgnoreCase))
                .Select(r => r.ToCsvRow(_configs.Separator.ToString()))
                .ToArray();

            return result;
        }

        // private implementations
        private static CsvRow[] _ParseCsv(string filePath, CsvConfigs _configs)
        {
            CsvRow[] rows;

            var configuration = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = _configs.Separator.ToString(),
                BadDataFound = null,
                HasHeaderRecord = _configs.HasHeader,
                
            };
            
            using (var reader = new StreamReader(filePath))
            using (var csv = new CsvReader(reader, configuration))
            {
                csv.Context.RegisterClassMap<CsvRowMap>();
                rows = csv.GetRecords<CsvRow>().ToArray();
            }

            return rows;
        }
    }
}