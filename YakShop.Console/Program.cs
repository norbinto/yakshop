using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using YakShop.Application;
using YakShop.Domain;

namespace YakShop.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 2)
            {
                throw new ArgumentOutOfRangeException("2 arguments needed");
            }

            var xmlstring = new StringBuilder();
            int days = 0;
            try
            {
                days = Convert.ToInt32(args[1]);
                if (days < 0)
                {
                    throw new FormatException("Days must be a positive integer");
                }
            }
            catch (FormatException ex)
            {
                throw ex;
            }

            using (var reader = new StreamReader(args[0]))
            {
                while (reader.Peek() >= 0)
                    xmlstring.AppendLine(reader.ReadLine());
            }

            Parser parser = new Parser();

            var newYaks = parser.Parse(xmlstring.ToString());
            Stock stock = new Stock(newYaks, new List<Reservation>(), days);
            System.Console.WriteLine("Stock");
            System.Console.WriteLine(stock.ToString());
            System.Console.WriteLine("Herd");
            System.Console.WriteLine(newYaks.HerdStatusByDaysLater(days));
            System.Console.ReadKey();
        }
    }
}
