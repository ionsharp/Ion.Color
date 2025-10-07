using Ion.Numeral;
using System;

namespace Ion.Colors;

/// <summary>
/// <b>Luminance (L), Chroma (C), Hue (H)</b>
/// <para>A cylindrical form of 'rgG' that is designed to accord with the human perception of color.</para>
/// <para><see cref="RGB"/> ⇒ <see cref="Lrgb"/> ⇒ <see cref="rgG"/> ⇒ <see cref="LCHrg"/></para>
/// </summary>
[ComponentGroup(ComponentGroup.LCH)]
[Description("A cylindrical form of 'rgG' that is designed to accord with the human perception of color.")]
public record class LCHrg(double X, double Y, double Z)
    : LCH<LCHrg, rgG>(X, Y, Z), IColor3<LCHrg, double>, System.Numerics.IMinMaxValue<LCHrg>
{
    public static LCHrg MaxValue => new(100, 100, 360);

    public static LCHrg MinValue => new(0);

    public LCHrg() : this(default, default, default) { }

    public LCHrg(double lch) : this(lch, lch, lch) { }

    public LCHrg(IVector3<double> lch) : this(lch.X, lch.Y, lch.Z) { }

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