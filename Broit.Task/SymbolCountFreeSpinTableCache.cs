using SymbolCountTableDictionary = System.Collections.Generic.Dictionary<TestTask.SymbolPosition, TestTask.SymbolCountTable>;

namespace TestTask;

internal sealed class SymbolCountFreeSpinTableCache
{
    private SymbolCountTableDictionary _defaultCountTables = new();

    private SymbolCountTableDictionary _sideCountTables = new();

    public void Initialize()
    {
        _defaultCountTables = GetSymbolCountTables(isSideReelUsed: false);

        _sideCountTables = GetSymbolCountTables(isSideReelUsed: true);
    }

    public SymbolCountTable GetCountTable(SymbolPosition symbolPosition, bool isSideReelUsed)
    {
        var countTables = isSideReelUsed ? _sideCountTables : _defaultCountTables;

        return countTables[symbolPosition];
    }

    private static SymbolCountTableDictionary GetSymbolCountTables(bool isSideReelUsed)
    {
        var countTables = new SymbolCountTableDictionary();

        foreach (var symbolPosition in SymbolHelper.GetAllSymbolPositions())
        {
            var countTable = ReelInfoCalculator.GetSymbolCountTable(symbolPosition, isSideReelUsed);

            countTables.Add(symbolPosition, countTable);
        }

        return countTables;
    }
}

