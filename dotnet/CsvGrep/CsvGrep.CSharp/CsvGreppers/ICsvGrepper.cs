namespace CsvGrep.CSharp.CsvGreppers
{
    public interface ICsvGrepper
    {
        /// <summary>
        /// Filter csv file based on <paramref name="columnValue"/> of column <paramref name="columnIndex"/>
        /// </summary>
        string[] GrepOnFile(string filePath, int columnIndex, string columnValue);
    }
}