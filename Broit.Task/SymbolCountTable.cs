namespace TestTask;

internal sealed class SymbolCountTable
{
    public SymbolCountTable(int columnCount = c_symbolCount)
    {
        _columnCount = columnCount;

        _table = new int[c_symbolCount, _columnCount];
    }

    private readonly int _columnCount;

    private const int c_symbolCount = 8;
    
    private readonly int[,] _table = new int[c_symbolCount, c_symbolCount];

    public void SetCount(int rowIndex, int columnIndex, int value)
    {
        _table[rowIndex, columnIndex] = value;
    }

    public int GetCount(int rowIndex, int columnIndex)
    {
        return _table[rowIndex, columnIndex];
    }

    public void Print()
    {
        for (var rowIndex = 0; rowIndex < c_symbolCount; ++rowIndex)
        {
            for (var columnIndex = 0; columnIndex < _columnCount; columnIndex++)
            {
                Console.Write(_table[rowIndex, columnIndex]);
                
                Console.Write(" ");
            }

            Console.WriteLine();
        }
    }
}
