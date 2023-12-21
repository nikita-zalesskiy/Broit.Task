namespace TestTask;

internal sealed class WinningLineSymbolInclusion
{
    private WinningLineSymbolInclusion(params SymbolInclusion[] symbolInclusions)
    {
        _symbolInclusions = symbolInclusions;

        var inclusionCount = _symbolInclusions
            .Count(symbolInclusions => symbolInclusions == SymbolInclusion.Include);

        InclusionMultiplier = inclusionCount switch
        {
            3 => 1
            , 4 => 5
            , 5 => 10
            , _ => 1
        };
    }

    private readonly SymbolInclusion[] _symbolInclusions;

    public int InclusionMultiplier { get; }

    public SymbolInclusion GetSymbolInclusion(int reelIndex)
    {
        return _symbolInclusions[reelIndex];
    }

    public static readonly WinningLineSymbolInclusion Dumb = new(SymbolInclusion.Any
        , SymbolInclusion.Any, SymbolInclusion.Any, SymbolInclusion.Any, SymbolInclusion.Any);

    public static readonly WinningLineSymbolInclusion FirstThree = new(SymbolInclusion.Include
        , SymbolInclusion.Include, SymbolInclusion.Include, SymbolInclusion.Exclude, SymbolInclusion.Any);

    public static readonly WinningLineSymbolInclusion LastThree = new(SymbolInclusion.Any
        , SymbolInclusion.Exclude, SymbolInclusion.Include, SymbolInclusion.Include, SymbolInclusion.Include);

    public static readonly WinningLineSymbolInclusion FirstFour = new(SymbolInclusion.Include
        , SymbolInclusion.Include, SymbolInclusion.Include, SymbolInclusion.Include, SymbolInclusion.Exclude);

    public static readonly WinningLineSymbolInclusion LastFour = new(SymbolInclusion.Exclude
        , SymbolInclusion.Include, SymbolInclusion.Include, SymbolInclusion.Include, SymbolInclusion.Include);

    public static readonly WinningLineSymbolInclusion AllFive = new(SymbolInclusion.Include
        , SymbolInclusion.Include, SymbolInclusion.Include, SymbolInclusion.Include, SymbolInclusion.Include);

    public static WinningLineSymbolInclusion[] Configurations = new[] { FirstThree, LastThree, FirstFour, LastFour, AllFive };
}
