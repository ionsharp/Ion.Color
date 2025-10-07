namespace Ion.Colors;

/// <remarks>Implements <see cref="IConvert{TColor}"/>. Inherits <see cref="Color3{TSelf}"/>.</remarks>
/// <summary>An <see cref="IColor3"/> that derives from another <see cref="IColor3"/>.</summary>
public abstract record class Color3<TSelf, TValue, TOther>(TValue X, TValue Y, TValue Z)
    : Color3<TSelf, TValue>(X, Y, Z), IConvert<TOther>
    where TSelf : IColor3<TSelf, TValue>, System.Numerics.IMinMaxValue<TSelf>
    where TOther : IColor3, new()
    where TValue : System.Numerics.INumber<TValue>
{
    /// <see cref="IConvert{TColor}"/>

    /// <summary><see cref="Lrgb"/> ⇒ <see langword="this"/></summary>
    public sealed override void From(in Lrgb color, ColorProfile profile)
    {
        var other = new TOther();
        other.From(color, profile);
        From(other, profile);
    }

    /// <summary><see langword="this"/> ⇒ <see cref="Lrgb"/></summary>
    public sealed override Lrgb To(ColorProfile profile)
    {
        To(out TOther color, profile);
        return color.To(profile);
    }

    /// <summary><see langword="this"/> ⇒ <b>TOther</b></summary>
    public abstract void From(in TOther color, ColorProfile profile);

    /// <summary><b>TOther</b> ⇒ <see langword="this"/></summary>
    public abstract void To(out TOther color, ColorProfile profile);
}