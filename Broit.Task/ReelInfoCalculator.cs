namespace TestTask;

internal sealed class ReelInfoCalculator
{
    private const string c_defaultReelSymbols = "GSEDGDBWGFEAHGBCHCBFACGGEFSCAWAHEFDWCGEEGFFGHDCHGBGCHCBHDDDBFHSWHSADCHABCDSEHBDADAEFDHGCEHDSFCAAFCHCBAFDGEFDGBECBSFECWSFWFEBHCDHGGGSGBHGFAHAAAFCFHBADAWDBWEBAEAEBFCWBDWCBFHESDEEBEAG";

    private static readonly string s_sideReelSymbols = c_defaultReelSymbols.Replace("W", string.Empty);

    private const int c_windowActivePositions = 3;

    private const int c_reelCount = 5;

    private const int c_maxFreeSpinInWindow = 2;

    public static SymbolCountTable GetSymbolCountTable(SymbolPosition firstPosition
        , SymbolPosition secondPosition, bool isSideReelUsed)
    {
        var countTable = new SymbolCountTable();

        var reelWindow = new char[c_windowActivePositions];

        var reelSymbols = isSideReelUsed ? s_sideReelSymbols : c_defaultReelSymbols;

        var windowCount = reelSymbols.Length;

        for (var rowIndex = 0; rowIndex < SymbolHelper.SymbolCount; ++rowIndex)
        {
            var firstSymbol = SymbolHelper.GetSymbol(rowIndex);

            for (var columnIndex = 0; columnIndex < SymbolHelper.SymbolCount; ++columnIndex)
            {
                var matchCount = 0;

                var secondSymbol = SymbolHelper.GetSymbol(columnIndex);

                for (var windowIndex = 0; windowIndex < windowCount; ++windowIndex)
                {
                    for (var symbolIndex = 0; symbolIndex < c_windowActivePositions; ++symbolIndex)
                    {
                        var reelSymbolIndex = (windowIndex + symbolIndex) % windowCount;

                        reelWindow[symbolIndex] = reelSymbols[reelSymbolIndex];
                    }

                    if (HasMatch(firstPosition, firstSymbol, reelWindow)
                        && HasMatch(secondPosition, secondSymbol, reelWindow))
                    {
                        matchCount++;
                    }
                }

                countTable.SetCount(rowIndex, columnIndex, matchCount);
            }
        }

        return countTable;
    }

    public static SymbolCountTable GetSymbolCountTable(SymbolPosition position, bool isSideReelUsed)
    {
        var countTable = new SymbolCountTable(c_maxFreeSpinInWindow + 1);

        var reelWindow = new char[c_windowActivePositions];

        var reelSymbols = isSideReelUsed ? s_sideReelSymbols : c_defaultReelSymbols;

        var windowCount = reelSymbols.Length;

        for (var rowIndex = 0; rowIndex < SymbolHelper.SymbolCount; ++rowIndex)
        {
            var firstSymbol = SymbolHelper.GetSymbol(rowIndex);

            for (var columnIndex = 0; columnIndex <= c_maxFreeSpinInWindow; ++columnIndex)
            {
                var matchCount = 0;

                var secondSymbol = SymbolHelper.GetSymbol(columnIndex);

                for (var windowIndex = 0; windowIndex < windowCount; ++windowIndex)
                {
                    for (var symbolIndex = 0; symbolIndex < c_windowActivePositions; ++symbolIndex)
                    {
                        var reelSymbolIndex = (windowIndex + symbolIndex) % windowCount;

                        reelWindow[symbolIndex] = reelSymbols[reelSymbolIndex];
                    }

                    var hasFreeSpinSymbolMatch = reelWindow
                        .Count(symbol => symbol.Equals('S'))
                        .Equals(columnIndex);

                    if (hasFreeSpinSymbolMatch
                        && HasMatch(position, firstSymbol, reelWindow))
                    {
                        matchCount++;
                    }
                }

                countTable.SetCount(rowIndex, columnIndex, matchCount);
            }
        }

        return countTable;
    }

    private static bool HasMatch(SymbolPosition symbolPosition, char expectedSymbol, char[] reelWindow)
    {
        var currentSymbol = reelWindow[symbolPosition.Value];

        var hasMatch = currentSymbol.Equals(expectedSymbol) || currentSymbol.Equals('W');

        return symbolPosition.SymbolInclusion switch
        {
            SymbolInclusion.Include => hasMatch,
            SymbolInclusion.Exclude => !hasMatch,
            SymbolInclusion.Any => true,
            _ => throw new InvalidOperationException(),
        };
    }

    public static PrizeCombinationInfo GetPrizeInfo(WinningLine firstLine
        , WinningLine secondLine, SymbolCountTableCache countTableCache)
    {
        var combinationCounts = new int[c_reelCount];

        for (var reelIndex = 0; reelIndex < c_reelCount; ++reelIndex)
        {
            var firstPosition = firstLine.GetSymbolPosition(reelIndex);

            var secondPosition = secondLine.GetSymbolPosition(reelIndex);

            var isSideReelUsed = reelIndex is 0 or (c_reelCount - 1);

            var countTable = countTableCache.GetCountTable(firstPosition, secondPosition, isSideReelUsed);

            combinationCounts[reelIndex] = countTable.GetCount(firstLine.SymbolIndex, secondLine.SymbolIndex);
        }

        return new(firstLine.LinePrize * secondLine.LinePrize, combinationCounts);
    }

    public static PrizeCombinationInfo GetPrizeInfo(WinningLine winningLine
        , SymbolFreeSpinInclusion freeSpinSymbolInclusion, SymbolCountFreeSpinTableCache countTableCache)
    {
        var combinationCounts = new int[c_reelCount];

        for (var reelIndex = 0; reelIndex < c_reelCount; ++reelIndex)
        {
            var symbolPosition = winningLine.GetSymbolPosition(reelIndex);

            var freeSpinSymbolCount = freeSpinSymbolInclusion.GetSymbolCount(reelIndex);

            var isSideReelUsed = reelIndex is 0 or (c_reelCount - 1);

            var countTable = countTableCache.GetCountTable(symbolPosition, isSideReelUsed);

            combinationCounts[reelIndex] = countTable.GetCount(winningLine.SymbolIndex, freeSpinSymbolCount);
        }

        return new(winningLine.LinePrize, combinationCounts);
    }

    public static double GetCrossTotalPrize(SymbolCountTableCache countTableCache)
    {
        double totalPrize = 0;

        foreach (var firstLine in WinningLine.GetAllWinningLines())
        {
            foreach (var secondLine in WinningLine.GetAllWinningLines())
            {
                var prizeInfo = GetPrizeInfo(firstLine, secondLine, countTableCache);

                checked
                {
                    totalPrize += prizeInfo.GetTotalPrize();
                }
            }
        }

        return totalPrize / 100.0;
    }

    public static double GetCrossTotalPrize(SymbolCountFreeSpinTableCache countTableCache, int freeSpinSymbolCount)
    {
        var symbolInclusions = freeSpinSymbolCount switch
        {
            3 => SymbolFreeSpinInclusion.ThreeSymbolConfigurations
            , 4 => SymbolFreeSpinInclusion.FourSymbolConfigurations
            , 5 => SymbolFreeSpinInclusion.FiveSymbolConfigurations
            , _ => throw new InvalidOperationException()
        };

        double totalPrize = 0;

        foreach (var firstLine in WinningLine.GetAllWinningLines())
        {
            foreach (var symbolInclusion in symbolInclusions)
            {
                var prizeInfo = GetPrizeInfo(firstLine, symbolInclusion, countTableCache);

                checked
                {
                    totalPrize += prizeInfo.GetTotalPrize();
                }
            }
        }

        return totalPrize / 10.0;
    }

    public static object GetMeanTotalPrize(SymbolCountTableCache countTableCache)
    {
        long totalPrize = 0;

        var dumbLine = new WinningLine(0, WinningLineSymbolPosition.Configurations[0], WinningLineSymbolInclusion.Dumb);

        foreach (var firstLine in WinningLine.GetAllWinningLines())
        {
            var prizeInfo = GetPrizeInfo(firstLine, dumbLine, countTableCache);

            checked
            {
                totalPrize += prizeInfo.GetTotalPrize();
            }
        }

        return totalPrize / dumbLine.LinePrize / 10;
    }
}
