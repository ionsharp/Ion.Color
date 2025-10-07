using Ion.Numeral;
using System;
using static System.Math;

namespace Ion.Colors;

/// <summary>
/// <para><b>Red (R), Yellow (Y), Blue (B)</b></para>
/// <para>An additive model where the primary colors 'Red' and 'Blue' are added with the secondary color 'Yellow'.</para>
/// <para><see cref="RGB"/> ⇒ <see cref="Lrgb"/> ⇒ <see cref="RYB"/></para>
/// </summary>
/// <remarks>http://www.deathbysoftware.com/colors/index.html</remarks>
[ColorOf<Lrgb>]
[Component(byte.MaxValue, "R", "Red")]
[Component(byte.MaxValue, "Y", "Yellow")]
[Component(byte.MaxValue, "B", "Blue")]
[ComponentGroup(ComponentGroup.RB)]
[Description("An additive model where the primary colors 'Red' and 'Blue' are added with the secondary color 'Yellow'.")]
public record class RYB(byte R, byte Y, byte B)
    : Color3<RYB, byte>(R, Y, B), IColor3<RYB, byte>, System.Numerics.IMinMaxValue<RYB>
{
    public static RYB MaxValue => new(byte.MaxValue);

    public static RYB MinValue => new(byte.MinValue);

    public RYB() : this(default, default, default) { }

    public RYB(byte ryb) : this(ryb, ryb, ryb) { }

    public RYB(IVector3<byte> ryb) : this(ryb.X, ryb.Y, ryb.Z) { }

    /// <summary><see cref="Lrgb"/> ⇒ <see cref="RYB"/></summary>
    public override void From(in Lrgb input, ColorProfile profile)
    {
        var r = input.X; var g = input.Y; var b = input.Z;

        //Remove the white from the color
        var white = Min(r, Min(g, b));

        r -= white;
        g -= white;
        b -= white;

        var mG = Max(r, Max(g, b));

        //Get the yellow out of the red/green

        var y = Min(r, g);

        r -= y;
        g -= y;

        //If blue and green, cut each in half to preserve maximum range
        if (b > 0 && g > 0)
        {
            b /= 2;
            g /= 2;
        }

        //Redistribute the remaining green
        y += g;
        b += g;

        //Normalize
        var mY = Max(r, Max(y, b));

        if (mY > 0)
        {
            var mN = mG / mY;

            r *= mN;
            y *= mN;
            b *= mN;
        }

        //Add the white back in
        r += white;
        y += white;
        b += white;

        XYZ = new Vector3<Double1>(r.Round0(), y.Round0(), b.Round0()).Denormalize<byte>();
    }

    /// <summary><see cref="RYB"/> ⇒ <see cref="Lrgb"/></summary>
    public override Lrgb To(ColorProfile profile)
    {
        var r = X.ToDouble(); var y = Y.ToDouble(); var b = Z.ToDouble();

        //Remove the whiteness
        var white = Min(r, Min(y, b));
        r -= white; y -= white; b -= white;

        var mY = Max(r, Max(y, b));

        //Get the green out of the yellow/blue
        var g = Min(y, b);
        y -= g; b -= g;

        if (b > 0 && g > 0)
        {
            b *= 2.0; g *= 2.0;
        }

        //Redistribute the remaining yellow
        r += y; g += y;

        //Normalize
        var mG = Max(r, Max(g, b));
        if (mG > 0)
        {
            var mN = mY / mG;
            r *= mN; g *= mN; b *= mN;
        }

        //Add the white back in
        r += white; g += white; b += white;
        return new(r.Round0(), g.Round0(), b.Round0());
    }
}