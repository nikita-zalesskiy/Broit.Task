namespace TestTask;

internal sealed class PrizeCombinationInfo
{
    public PrizeCombinationInfo(int prize, params int[] combinationCounts)
    {
        _prize = prize;
        _combinationCounts = combinationCounts;
    }

    private readonly int _prize;
    
    private readonly int[] _combinationCounts;

    public long GetTotalPrize()
    {
        long combinationProduct = 1;

        foreach (var combinationCount in _combinationCounts)
        {
            combinationProduct *= combinationCount;
        }

        return _prize * combinationProduct;
    }
}
