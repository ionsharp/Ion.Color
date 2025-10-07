using Ion.Numeral;

namespace Ion.Colors;

/// <remarks>Implements <see cref="IColor4{TValue}"/>. Inherits <see cref="Color{TSelf,TValue}"/>.</remarks>
/// <inheritdoc cref="IColor4"/>
[Description(IColor4.Description)]
public abstract record class Color4<TSelf, TNumber>(TNumber X, TNumber Y, TNumber Z, TNumber W)
    : Color3<TSelf, TNumber>(X, Y, Z), IColor4<TNumber>
    where TSelf : IColor4<TSelf, TNumber>, System.Numerics.IMinMaxValue<TSelf>
    where TNumber : System.Numerics.INumber<TNumber>
{
    public override int Length => 4;

    /// <inheritdoc cref="IColor4.W"/>
    public TNumber W { get; set; } = W;

    /// <inheritdoc cref="IColor4{T}.XYZW"/>
    public Vector4<TNumber> XYZW { get => new(X, Y, Z, W); set { X = value.X; Y = value.Y; Z = value.Z; W = value.W; } }

    public static implicit operator Vector4(in Color4<TSelf, TNumber> i) => i.NewType(j => j.ToDouble());

    public static implicit operator Vector4<TNumber>(in Color4<TSelf, TNumber> i) => i.XYZW;

    public override TNumber[] ToArray() => [X, Y, Z, W];

    /// <see cref="IColor4"/>

    /// <inheritdoc cref="IColor4.W"/>
    double IColor4.W { get => W.ToDouble(); set => X = value.Create<TNumber>(); }

    /// <inheritdoc cref="IColor4.XYZW"/>
    Vector4 IColor4.XYZW { get => this.NewType(i => i.ToDouble()); set => XYZW = value.NewType(i => i.Create<TNumber>()); }
}