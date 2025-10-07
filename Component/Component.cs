using Ion.Numeral;

namespace Ion.Colors;

public record class Component(char Unit, string Symbol, string Name)
{
    public string Description { get; set; }

    public double Maximum { get; }

    public double Minimum { get; }

    public string Name { get; } = Name;

    public Range<double> Range => new(Minimum, Maximum);

    public string Symbol { get; } = Symbol;

    public char Unit { get; } = Unit;

    public Component(double minimum, double maximum, char unit, string symbol, string name)
        : this(unit, symbol, name) { Minimum = minimum; Maximum = maximum; }
}