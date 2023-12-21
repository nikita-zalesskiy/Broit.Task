namespace TestTask;

internal sealed class WinningLineSymbolPosition
{
    private WinningLineSymbolPosition(int[] symbolPositions)
    {
        _symbolPositions = symbolPositions;
    }

    private readonly int[] _symbolPositions;

    private static readonly int[][] s_symbolPositions = new int[][]
    {
        new[] { 0, 0, 0, 0, 0 }
        , new[] { 1, 1, 1, 1, 1 }
        , new[] { 2, 2, 2, 2, 2 }
        , new[] { 0, 1, 2, 1, 0 }
        , new[] { 2, 1, 0, 1, 2 }
        , new[] { 0, 1, 1, 1, 0 }
        , new[] { 2, 1, 1, 1, 2 }
        , new[] { 0, 0, 1, 2, 2 }
        , new[] { 2, 2, 1, 0, 0 }
    };

    public static readonly WinningLineSymbolPosition[] Configurations = s_symbolPositions
        .Select(symbolPosition => new WinningLineSymbolPosition(symbolPosition))
        .ToArray();

    public int GetSymbolPosition(int reelIndex)
    {
        return _symbolPositions[reelIndex];
    }
}
