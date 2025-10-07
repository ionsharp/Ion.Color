using Ion.Numeral;
using System;

namespace Ion.Colors;

/// <summary>
/// <b>Luminance (L), Chroma (C), Hue (H)</b>
/// <para>A cylindrical form of 'xyY' that is designed to accord with the human perception of color.</para>
/// <para><see cref="RGB"/> ⇒ <see cref="Lrgb"/> ⇒ <see cref="XYZ"/> ⇒ <see cref="xyY"/> ⇒ <see cref="LCHxy"/></para>
/// </summary>
[ComponentGroup(ComponentGroup.LCH)]
[Description("A cylindrical form of 'xyY' that is designed to accord with the human perception of color.")]
public record class LCHxy(double X, double Y, double Z)
    : LCH<LCHxy, xyY>(X, Y, Z), IColor3<LCHxy, double>, System.Numerics.IMinMaxValue<LCHxy>
{
    public static LCHxy MaxValue => new(100, 100, 360);

    public static LCHxy MinValue => new(0);

    public LCHxy() : this(default, default, default) { }

    public LCHxy(double lch) : this(lch, lch, lch) { }

    public LCHxy(IVector3<double> lch) : this(lch.X, lch.Y, lch.Z) { }

    /// <inheritdoc/>
    public override Vector3 ToLCh(Vector3 input)
    {
        input = new Vector3(input.Z, input.X, input.Y) * new Vector3(100, 200, 200) - new Vector3(0, 100, 100);
        return base.ToLCh(input);
    }

    /// <inheritdoc/>
    public override Vector3 FromLCh(Vector3 input)
    {
        var result = base.FromLCh(input);
        result = new Vector3(result.Y, result.Z, result.X) + new Vector3(100, 100, 0) / new Vector3(200, 200, 100);
        return result;
    }
}