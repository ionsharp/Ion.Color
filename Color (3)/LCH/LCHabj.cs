using Ion.Numeral;
using System;

namespace Ion.Colors;

/// <summary>See <see cref="LCHabj{TSelf}"/>.</summary>
/// <remarks>Implements <see cref="IColor3"/>.</remarks>
public interface ILCHabj : IColor3 { }

/// <summary>
/// <para><b>Lightness (L), Chroma (C), Hue (H)</b></para>
/// <para>A cylindrical form of 'Labj' that is designed to accord with the human perception of color.</para>
/// <para><see cref="RGB"/> ⇒ <see cref="Lrgb"/> ⇒ <see cref="XYZ"/> ⇒ <see cref="Labj"/> ⇒ <see cref="LCHabj"/></para>
/// 
/// <i>Alias</i>
/// <list type="bullet">
/// <item>JzCzhz</item>
/// </list>
/// </summary>
/// <remarks>https://observablehq.com/@jrus/jzazbz</remarks>
[ComponentGroup(ComponentGroup.LCH)]
[Description("A cylindrical form of 'Labj' that is designed to accord with the human perception of color.")]
public record class LCHabj(double X, double Y, double Z)
    : LCH<LCHabj, Labj>(X, Y, Z), IColor3<LCHabj, double>, System.Numerics.IMinMaxValue<LCHabj>
{
    public static LCHabj MaxValue => new(100, 100, 360);

    public static LCHabj MinValue => new(0);

    public LCHabj() : this(default, default, default) { }

    public LCHabj(double lch) : this(lch, lch, lch) { }

    public LCHabj(IVector3<double> lch) : this(lch.X, lch.Y, lch.Z) { }

    /// <inheritdoc/>
    public override Vector3 FromLCh(Vector3 input) => base.FromLCh(input) / 100;

    /// <inheritdoc/>
    public override Vector3 ToLCh(Vector3 input) => base.ToLCh(input * 100);
}