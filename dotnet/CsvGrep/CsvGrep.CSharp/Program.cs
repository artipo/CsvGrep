using System;
using CsvGrep.CSharp.CsvGreppers;

// ReSharper disable InlineOutVariableDeclaration

namespace CsvGrep.CSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args is null || args.Length != 3)
            {
                Console.WriteLine("wrong cmd args");
                Console.WriteLine("use \"CsvGrep.CSharp.exe [filePath] [columnIndex] [columnValue]\"");
                return;
            }

            // args
            var filePath = args[0];
            int columnIndex;
            if (!int.TryParse(args[1], out columnIndex))
            {
                Console.WriteLine("wrong columnIndex, insert a integer number");
                return;
            }
            var columnValue = args[2];
            
            try
            {
                // grep
                // ICsvGrepper grepper = new BasicCsvGrepper();
                // ICsvGrepper grepper = new MinimalCsvGrepper();
                ICsvGrepper grepper = new CsvHelperGrepper();
                var result = grepper.GrepOnFile(filePath, columnIndex, columnValue);

                // print result
                Console.WriteLine("Rows mathcing:");
                foreach (var r in result)
                {
                    Console.WriteLine(r);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error encountered:");
                Console.WriteLine(e.ToString());
            }
        }
    }
}