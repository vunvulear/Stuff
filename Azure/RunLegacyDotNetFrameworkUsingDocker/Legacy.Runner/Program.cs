using Legacy.Core;
using System;

namespace Legacy.Runner
{
    class Program
    {
        static void Main()
        {
            Console.WriteLine("Start legacy application");

            IStockCheck stockCheck = new StockCheck();
            stockCheck.Monitor(100);

            Console.WriteLine("End legacy application");
        }
    }
}
