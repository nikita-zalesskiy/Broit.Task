namespace TestTask;

internal class Program
{
    public static void Main()
    {
        //var countTableCache = new SymbolCountTableCache();

        //countTableCache.Initialize();

        //var crossTotalPrize = ReelInfoCalculator.GetCrossTotalPrize(countTableCache);

        //Console.WriteLine(crossTotalPrize);

        //var meanTotalPrize = ReelInfoCalculator.GetMeanTotalPrize(countTableCache);

        //Console.WriteLine(meanTotalPrize);

        var countTableCache = new SymbolCountFreeSpinTableCache();

        countTableCache.Initialize();

        var crossTotalPrize = ReelInfoCalculator.GetCrossTotalPrize(countTableCache, freeSpinSymbolCount: 5);

        Console.WriteLine(crossTotalPrize);
    }
}
