namespace TestTask;

internal static class SymbolHelper
{
    public static readonly string Symbols = "ABCDEFGH";

    public static readonly int SymbolCount = Symbols.Length;

    public static char GetSymbol(int symbolIndex)
    {
        return Symbols[symbolIndex];
    }

    public static IEnumerable<SymbolInclusion> GetAllInclusions()
    {
        yield return SymbolInclusion.Include;

        yield return SymbolInclusion.Exclude;

        yield return SymbolInclusion.Any;
    }

    public static IEnumerable<int> GetAllPositions()
    {
        yield return 0;

        yield return 1;

        yield return 2;
    }

    public static IEnumerable<SymbolPosition> GetAllSymbolPositions()
    {
        foreach (var symbolPosition in GetAllPositions())
        {
            foreach (var symbolInclusion in GetAllInclusions())
            {
                yield return new(symbolPosition, symbolInclusion);
            }
        }
    }
}
