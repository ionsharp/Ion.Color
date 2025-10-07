using Ion.Numeral;

namespace Ion.Colors;

/// <remarks>Implements <see cref="IColor2{TValue}"/>. Inherits <see cref="Color{TSelf,TValue}"/>.</remarks>
/// <inheritdoc cref="IColor2"/>
[Description(IColor2.Description)]
public abstract record class Color2<TSelf, TNumber>(TNumber X, TNumber Y)
    : Color<TSelf, TNumber>(), IColor2<TNumber>
    where TSelf : IColor2<TSelf, TNumber>, System.Numerics.IMinMaxValue<TSelf>
    where TNumber : System.Numerics.INumber<TNumber>
{
    public override int Length => 2;

    /// <inheritdoc cref="IColor2.X"/>
    public TNumber X { get; set; } = X;

    /// <inheritdoc cref="IColor2.Y"/>
    public TNumber Y { get; set; } = Y;

    /// <inheritdoc cref="IColor2{T}.XY"/>
    public Vector2<TNumber> XY { get => new(X, Y); set { X = value.X; Y = value.Y; } }

    public static implicit operator Vector2(in Color2<TSelf, TNumber> i) => i.NewType(j => j.ToDouble());

    public static implicit operator Vector2<TNumber>(in Color2<TSelf, TNumber> i) => i.XY;

    public override TNumber[] ToArray() => [X, Y];

    /// <see cref="IColor2"/>

    /// <inheritdoc cref="IColor2.X"/>
    double IColor2.X { get => X.ToDouble(); set => X = value.Create<TNumber>(); }

    /// <inheritdoc cref="IColor2.Y"/>
    double IColor2.Y { get => Y.ToDouble(); set => Y = value.Create<TNumber>(); }

    /// <inheritdoc cref="IColor2.XY"/>
    Vector2 IColor2.XY { get => this.NewType(i => i.ToDouble()); set => XY = value.NewType(i => i.Create<TNumber>()); }
}