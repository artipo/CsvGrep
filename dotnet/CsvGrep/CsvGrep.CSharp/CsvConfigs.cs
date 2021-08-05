namespace CsvGrep.CSharp
{
    /// <summary>
    /// Csv file configuration
    /// </summary>
    public class CsvConfigs
    {
        public bool HasHeader { get; set; }
        public char Separator { get; set; }

        public static CsvConfigs Default => new()
        {
            Separator = ',',
            HasHeader = false
        };
    }
}