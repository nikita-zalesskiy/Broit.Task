namespace TestTask;

internal readonly struct SymbolPosition : IEquatable<SymbolPosition>
{
    public SymbolPosition(int value, SymbolInclusion symbolInclusion)
    {
        Value = value;

        SymbolInclusion = symbolInclusion;
    }

    public int Value { get; }

    public SymbolInclusion SymbolInclusion { get; }

    public bool Equals(SymbolPosition other)
    {
        return Value.Equals(other.Value)
            && SymbolInclusion == other.SymbolInclusion;
    }

    public override bool Equals(object? obj)
    {
        return obj is SymbolPosition symbolPosition && Equals(symbolPosition);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Value, SymbolInclusion);
    }
}
