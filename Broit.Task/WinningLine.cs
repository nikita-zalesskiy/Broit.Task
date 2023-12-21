namespace TestTask;

internal sealed class WinningLine
{
    public WinningLine(int symbolIndex
        , WinningLineSymbolPosition symbolPositions
        , WinningLineSymbolInclusion symbolInclusions)
    {
        SymbolIndex = symbolIndex;

        SymbolPositions = symbolPositions;
        
        SymbolInclusions = symbolInclusions;
    }

    // All prizes multiplied by 10 to make them integers.
    private static readonly int[] s_prizes = new[] { 40, 30, 20, 10, 4, 3, 2, 1 };

    public int SymbolIndex { get; }

    public int LinePrize => SymbolInclusions.InclusionMultiplier * s_prizes[SymbolIndex];

    public WinningLineSymbolPosition SymbolPositions { get; }

    public WinningLineSymbolInclusion SymbolInclusions { get; }

    public SymbolPosition GetSymbolPosition(int reelIndex)
    {
        var symbolPositionValue = SymbolPositions.GetSymbolPosition(reelIndex);

        var symbolInclusion = SymbolInclusions.GetSymbolInclusion(reelIndex);

        return new(symbolPositionValue, symbolInclusion);
    }

    public static IEnumerable<WinningLine> GetAllWinningLines()
    {
        for (var symbolIndex = 0; symbolIndex < SymbolHelper.SymbolCount; ++symbolIndex)
        {
            foreach (var symbolPosition in WinningLineSymbolPosition.Configurations)
            {
                foreach (var symbolInclusion in WinningLineSymbolInclusion.Configurations)
                {
                    yield return new(symbolIndex, symbolPosition, symbolInclusion);
                }
            }
        }
    }
}
