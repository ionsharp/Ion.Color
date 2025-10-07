namespace Ion.Colors;

/// <remarks>Implements <see cref="IConvert{TColor}"/>. Inherits <see cref="Color2{TSelf}"/>.</remarks>
/// <summary>An <see cref="IColor2"/> that derives from an <see cref="IColor3"/>.</summary>
public abstract record class Color2<TSelf, TValue, TOther>(TValue X, TValue Y)
    : Color2<TSelf, TValue>(X, Y), IConvert<TOther>
    where TSelf : IColor2<TSelf, TValue>, System.Numerics.IMinMaxValue<TSelf>
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
        To(out TOther result, profile);
        return result.To(profile);
    }

    /// <summary><see langword="this"/> ⇒ <b>TOther</b></summary>
    public abstract void From(in TOther color, ColorProfile profile);

    /// <summary><b>TOther</b> ⇒ <see langword="this"/></summary>
    public abstract void To(out TOther color, ColorProfile profile);
}