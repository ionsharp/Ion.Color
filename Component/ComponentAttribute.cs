using System;

namespace Ion.Colors;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public sealed class ComponentAttribute(double minimum, double maximum, char unit, string symbol, string name = "") : Attribute()
{
    public string Description { get => Info.Description; set { Info.Description = value; } }

    public Component Info { get; private set; } = new Component(minimum, maximum, unit, symbol, name);

    public ComponentAttribute(string symbol, string name = "") : this(' ', symbol, name) { }

    public ComponentAttribute(char unit, string symbol, string name = "") : this(default, default, unit, symbol, name) { }

    public ComponentAttribute(double maximum, char unit, string symbol, string name = "") : this(0, maximum, unit, symbol, name) { }

    public ComponentAttribute(double maximum, string symbol, string name = "") : this(0, maximum, symbol, name) { }

    public ComponentAttribute(double minimum, double maximum, string symbol, string name = "") : this(minimum, maximum, ' ', symbol, name) { }
}