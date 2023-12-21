using SymbolCountTableDictionary = System.Collections.Generic.Dictionary<(TestTask.SymbolPosition, TestTask.SymbolPosition), TestTask.SymbolCountTable>;

namespace TestTask;

internal sealed class SymbolCountTableCache
{
    private SymbolCountTableDictionary _defaultCountTables = new();

    private SymbolCountTableDictionary _sideCountTables = new();

    public void Initialize()
    {
        _defaultCountTables = GetSymbolCountTables(isSideReelUsed: false);

        _sideCountTables = GetSymbolCountTables(isSideReelUsed: true);
    }

    public SymbolCountTable GetCountTable(SymbolPosition firstPosition, SymbolPosition secondPosition, bool isSideReelUsed)
    {
        var countTables = isSideReelUsed ? _sideCountTables : _defaultCountTables;

        return countTables[(firstPosition, secondPosition)];
    }

    private static SymbolCountTableDictionary GetSymbolCountTables(bool isSideReelUsed)
    {
        var countTables = new SymbolCountTableDictionary();

        foreach (var firstPosition in SymbolHelper.GetAllSymbolPositions())
        {
            foreach (var secondPosition in SymbolHelper.GetAllSymbolPositions())
            {
                var countTableKey = (firstPosition, secondPosition);

                var countTable = ReelInfoCalculator.GetSymbolCountTable(firstPosition, secondPosition, isSideReelUsed);

                countTables.Add(countTableKey, countTable);
            }
        }

        return countTables;
    }
}
