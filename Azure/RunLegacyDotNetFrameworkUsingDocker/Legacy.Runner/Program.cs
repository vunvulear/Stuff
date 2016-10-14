using Legacy.Core;

namespace Legacy.Runner
{
    class Program
    {
        static void Main()
        {
            IStockCheck stockCheck = new StockCheck();
            stockCheck.Monitor(100);
        }
    }
}
