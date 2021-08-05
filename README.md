# CsvGrep #

Welcome to CsvGrep, a small utility to search csv files for matching rows with provided input parameters

## Use ##
To use the tool simply call it from any terminal/console with following parameters:
- FilePath: path to the csv file to filter
- ColumnIndex: index of the column to filter
- ColumnValue: value used to filter each row

ex. 'CsvGrep.exe .\input.csv 2 Albero'

## Variations ##
There are multiple version of the tool, each in a different language.
- CSharp: this version contains 3 implementation of ICsvGrepper:
    - one with basic - all custom behaviour -> BasicCsvGrepper
    - one with the use of the nuget package CsvHelper -> CsvHelperGrepper
    - and at last one very small -> MinimalCsvGrep
- FSharp: this version shows the simplicity of the language and its succint property
- More to be added...