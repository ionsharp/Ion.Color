using Ion.Numeral;

namespace Ion.Colors;

/// <remarks>Implements <see cref="IColor3{TValue}"/>. Inherits <see cref="Color{TSelf,TValue}"/>.</remarks>
/// <inheritdoc cref="IColor3"/>
[Description(IColor3.Description)]
public abstract record class Color3<TSelf, TNumber>(TNumber X, TNumber Y, TNumber Z)
    : Color2<TSelf, TNumber>(X, Y), IColor3<TNumber>
    where TSelf : IColor3<TSelf, TNumber>, System.Numerics.IMinMaxValue<TSelf>
    where TNumber : System.Numerics.INumber<TNumber>
{
    public override int Length => 3;

    /// <inheritdoc cref="IColor3.Z"/>
    public TNumber Z { get; set; } = Z;

    /// <inheritdoc cref="IColor3{T}.XYZ"/>
    public Vector3<TNumber> XYZ { get => new(X, Y, Z); set { X = value.X; Y = value.Y; Z = value.Z; } }

    public static implicit operator Vector3(in Color3<TSelf, TNumber> i) => i.NewType(j => j.ToDouble());

    public static implicit operator Vector3<TNumber>(in Color3<TSelf, TNumber> i) => i.XYZ;

    public override TNumber[] ToArray() => [X, Y, Z];

    /// <see cref="IColor3"/>

    /// <inheritdoc cref="IColor3.Z"/>
    double IColor3.Z { get => Z.ToDouble(); set => X = value.Create<TNumber>(); }

    /// <inheritdoc cref="IColor3.XYZ"/>
    Vector3 IColor3.XYZ { get => this.NewType(i => i.ToDouble()); set => XYZ = value.NewType(i => i.Create<TNumber>()); }
}