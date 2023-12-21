namespace TestTask;

internal class SymbolFreeSpinInclusion
{
    private SymbolFreeSpinInclusion(int[] symbolPositions)
    {
        _symbolPositions = symbolPositions;
    }

    private readonly int[] _symbolPositions;

    private static readonly int[][] s_threeFreeSpinSymbolPositions = new int[][]
    {
        new[] { 2, 1, 0, 0, 0 }
        , new[] { 2, 0, 1, 0, 0 }
        , new[] { 2, 0, 0, 1, 0 }
        , new[] { 2, 0, 0, 0, 1 }

        , new[] { 1, 0, 0, 0, 2 }
        , new[] { 0, 1, 0, 0, 2 }
        , new[] { 0, 0, 1, 0, 2 }
        , new[] { 0, 0, 0, 1, 2 }

        , new[] { 0, 0, 1, 1, 1 }
        , new[] { 0, 1, 0, 1, 1 }
        , new[] { 0, 1, 1, 0, 1 }
        , new[] { 0, 1, 1, 1, 0 }

        , new[] { 1, 0, 0, 1, 1 }
        , new[] { 1, 0, 1, 0, 1 }
        , new[] { 1, 0, 1, 1, 0 }

        , new[] { 1, 1, 0, 0, 1 }
        , new[] { 1, 1, 0, 1, 0 }

        , new[] { 1, 1, 1, 0, 0 }
    };

    private static readonly int[][] s_fourFreeSpinSymbolPositions = new int[][]
    {
        new[] { 2, 0, 0, 0, 2 }

        , new[] { 2, 0, 0, 1, 1 }
        , new[] { 2, 0, 1, 0, 1 }
        , new[] { 2, 0, 1, 1, 0 }

        , new[] { 2, 1, 0, 0, 1 }
        , new[] { 2, 1, 0, 1, 0 }

        , new[] { 2, 1, 1, 0, 0 }

        , new[] { 0, 0, 1, 1, 2 }
        , new[] { 0, 1, 0, 1, 2 }
        , new[] { 0, 1, 1, 0, 2 }

        , new[] { 1, 0, 0, 1, 2 }
        , new[] { 1, 0, 1, 0, 2 }

        , new[] { 1, 1, 0, 0, 2 }

        , new[] { 0, 1, 1, 1, 1 }
        , new[] { 1, 0, 1, 1, 1 }
        , new[] { 1, 1, 0, 1, 1 }
        , new[] { 1, 1, 1, 0, 1 }
        , new[] { 1, 1, 1, 1, 0 }
    };

    private static readonly int[][] s_fiveFreeSpinSymbolPositions = new int[][]
    {
        new[] { 2, 1, 0, 0, 2 }
        , new[] { 2, 0, 1, 0, 2 }
        , new[] { 2, 0, 0, 1, 2 }

        , new[] { 2, 0, 1, 1, 1 }
        , new[] { 2, 1, 0, 1, 1 }
        , new[] { 2, 1, 1, 0, 1 }
        , new[] { 2, 1, 1, 1, 0 }

        , new[] { 0, 1, 1, 1, 2 }
        , new[] { 1, 0, 1, 1, 2 }
        , new[] { 1, 1, 0, 1, 2 }
        , new[] { 1, 1, 1, 0, 2 }

        , new[] { 1, 1, 1, 1, 1 }
    };

    public static readonly SymbolFreeSpinInclusion[] ThreeSymbolConfigurations = s_threeFreeSpinSymbolPositions
        .Select(symbolPosition => new SymbolFreeSpinInclusion(symbolPosition))
        .ToArray();

    public static readonly SymbolFreeSpinInclusion[] FourSymbolConfigurations = s_fourFreeSpinSymbolPositions
        .Select(symbolPosition => new SymbolFreeSpinInclusion(symbolPosition))
        .ToArray();

    public static readonly SymbolFreeSpinInclusion[] FiveSymbolConfigurations = s_fiveFreeSpinSymbolPositions
        .Select(symbolPosition => new SymbolFreeSpinInclusion(symbolPosition))
        .ToArray();

    public int GetSymbolCount(int reelIndex)
    {
        return _symbolPositions[reelIndex];
    }
}
