using System;
using System.IO;
using System.IO.Abstractions;
using Till.Model;

namespace Till.Data
{
    public static class TillContextCsvLoaderExtension
    {
        public static void LoadCsv(this ITillContext till, IFileSystem _fileSystem, string filePath)
        {
            using (StreamReader inputReader = _fileSystem.File.OpenText(filePath))
            {
                var i = 0;
                decimal price = 0;
                while (!inputReader.EndOfStream)
                {
                    var newline = inputReader.ReadLine();
                    var ln = newline.Split(',');
                    if (ln.GetLength(0) != 2)
                    {
                        throw new Exception($"Wrong line format expected <name>,<price> but get'{newline}' ");
                    };

                    if (!Decimal.TryParse(ln[1], out price))
                    {
                        throw new Exception($"Wrong price format expected decimal but get'{ln[1]}' ");
                    };

                    var fruit = new Fruit
                    {
                        Id = i++,
                        Name = ln[0],
                        Price = price
                    };
                    till.Fruits.Add(fruit);
                }
            }
        }
    }
}