using System;
using System.IO;
using System.IO.Abstractions;
using Till.BusinessLogic;
using Till.Data;

namespace Till.APIInterface
{

    public class ConsoleInterface : IConsoleInterface
    {
        private ITillContext TillContext;
        private ITillBL TillBL;

        public ConsoleInterface(ITillContext tillContext, ITillBL tillBL)
        {
            this.TillContext = tillContext;
            this.TillBL = tillBL;
        }

        public void Run()
        {
            Console.WriteLine("Welcome to Fruit Store Till");
            LoadPrices();
            ReadInvoiceLine();
            PrintInvoice();
        }

        private void PrintInvoice()
        {
            Console.WriteLine("Sale finished.");
            Console.WriteLine("You have brought");
            Console.WriteLine($"\titem\t\tquantity\tunitary price\ttotal price");
            foreach (var item in TillBL.CurrentInvoice.Items)
            {
                Console.WriteLine($"\t{item.Fruit.Name}\t\t{item.Quantity}\t\t{item.Fruit.Price}\t\t{item.InvoiceRowAmount}");
            }
            Console.WriteLine($"Total invoice {TillBL.CurrentInvoice.Total}");
            Console.WriteLine("Thanks for shopping. Press any key to close the app");
            Console.ReadKey();
        }

        private void ReadInvoiceLine()
        {
            Console.WriteLine("Please enter <quantity> <fruit> desired, or end to finish");
            var invoiceLine = Console.ReadLine();
            if (invoiceLine != "end")
            {
                var ln = invoiceLine.Split(' ');
                try
                {
                    TillBL.AddLine(ln[1], ln[0]);
                }
                catch (TillBLException e)
                {
                    Console.WriteLine($"Sorry I didn't understand what you said '{invoiceLine}' because '{e.Message}'");
                    Console.WriteLine();
                }
                ReadInvoiceLine();
            }
        }

        private void LoadPrices()
        {
            Console.WriteLine("First we need to load the prices. Please enter the path of a valid file with the prices of the fruits");
            Console.WriteLine($"The current directory is {Directory.GetCurrentDirectory()}");
            var fileName = Console.ReadLine();
            try
            {
                TillContext.LoadCsv(new FileSystem(), fileName);
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine($"We cannot load the file {Directory.GetCurrentDirectory()}\\{fileName}.");
                Console.WriteLine("Please try again with a correct file name");
                Console.WriteLine();
                LoadPrices();
            }
        }
    }
}