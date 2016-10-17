using System;
using System.Threading;

namespace Legacy.Core
{
    public class StockCheck : IStockCheck
    {
        public void Monitor(long itemID)
        {
            while (true)
            {
                Console.WriteLine("Stock check done");
                Thread.Sleep(TimeSpan.FromSeconds(2));
            }
        }
    }
}
