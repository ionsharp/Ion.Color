using Ion;
using Ion.Numeral;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace Ion.Colors;

/// <summary>A 3x3 matrix that maps the chromaticity coordinates (x, y) of a color to its corresponding tristimulus values (X, Y, Z).</summary>
/// <remarks>
/// <para>Used to convert between different color spaces. Can be inverted to transform tristimulus values back to chromaticity coordinates.</para>
/// <para><see href="http://www.brucelindbloom.com/index.html?Eqn_RGB_XYZ_Matrix.html"/></para>
/// </remarks>
[Description(Description)]
public readonly record struct ChromacityMatrix
    : IMatrix3x3<double>, IMatrix2D<double>, IMatrixUnfixedAlias<Matrix<double>, double>, IMatrixImmutable
{
    public const string Description = "A 3x3 matrix that maps the chromaticity coordinates (x, y) of a color to its corresponding tristimulus values (X, Y, Z).";

    /// <see cref="Region.Property"/>

    private readonly double[][] _Value { get; }

    public readonly Vector2 Chromacity { get; }

    public readonly Primary3 Primary => new(PrimaryRed, PrimaryGreen, PrimaryBlue);

    public readonly Vector2 PrimaryRed { get; }

    public readonly Vector2 PrimaryGreen { get; }

    public readonly Vector2 PrimaryBlue { get; }

    public readonly Vector3 White { get; }

    /// <see cref="Region.Property.Indexor"/>

    object IArray.this[int y] => _Value[y];

    object IArray1D.this[int y] => this[default, y];

    object IArray2D.this[int y, int x] => this[y, x];

#pragma warning disable CA1819 /// Properties should not return arrays
    public double[] this[int row] { get => [.. _Value[row]]; set => _Value[row] = value; }
#pragma warning restore CA1819

    public double this[int row, int column] { get => _Value[row][column]; set => _Value[row][column] = value; }

    /// <see cref="Region.Constructor"/>

    public ChromacityMatrix(ColorProfile profile)
        : this(profile.White, profile.Primary) { }

    /// <summary>Get new instance with given chromacity and primary coordinates.</summary>
    public ChromacityMatrix(Vector2 chromacity, Vector2 rPrimary, Vector2 gPrimary, in Vector2 bPrimary)
        : this((XYZ)(xyY)(XY)chromacity, new(rPrimary, gPrimary, bPrimary)) { }

    /// <summary>Get new instance with given chromacity and primary coordinates.</summary>
    public ChromacityMatrix(double x, double y, double rx, double ry, double gx, double gy, double bx, double by)
        : this(new Vector2(x, y), new Vector2(rx, ry), new Vector2(gx, gy), new Vector2(bx, by)) { }

    /// <summary>Get new instance with given chromacity and primary coordinates.</summary>
    /// <exception cref="ArgumentNullException"/>
    public ChromacityMatrix(double x, double y, in IVector2<double> r, in IVector2<double> g, in IVector2<double> b)
    {
        Throw.IfNull(r, nameof(r));
        Throw.IfNull(g, nameof(g));
        Throw.IfNull(b, nameof(b));

        Chromacity = new(x, y);
        PrimaryRed = new(r.X, r.Y); PrimaryGreen = new(g.X, g.Y); PrimaryBlue = new(b.X, b.Y);
        White = (XYZ)(xyY)(XY)Chromacity;

        _Value = New(White, Primary);
    }

    /// <summary>Get new instance with given chromacity and primary coordinates.</summary>
    /// <exception cref="ArgumentNullException"/>
    public ChromacityMatrix(in IVector2<double> chromacity, double rx, double ry, double gx, double gy, double bx, double by)
    {
        Throw.IfNull(chromacity, nameof(chromacity));

        Chromacity = new(chromacity.X, chromacity.Y);
        PrimaryRed = new(rx, ry); PrimaryGreen = new(gx, gy); PrimaryBlue = new(bx, by);
        White = (XYZ)(xyY)(XY)Chromacity;

        _Value = New(White, Primary);
    }

    /// <summary>Get new instance with given chromacity and <see cref="Primary3"/>.</summary>
    /// <exception cref="ArgumentNullException"/>
    public ChromacityMatrix(in IVector2<double> chromacity, Primary3 primary)
    {
        Throw.IfNull(chromacity, nameof(chromacity));

        Chromacity = new(chromacity.X, chromacity.Y);
        PrimaryRed = primary.R; PrimaryGreen = primary.G; PrimaryBlue = primary.B;
        White = (XYZ)(xyY)(XY)Chromacity;

        _Value = New(White, primary);
    }

    /// <summary>Get new instance with given white and <see cref="Primary3"/>.</summary>
    /// <exception cref="ArgumentNullException"/>
    public ChromacityMatrix(in IVector3<double> white, Primary3 primary)
    {
        Throw.IfNull(white, nameof(white));

        Chromacity = ((XY)(xyY)(XYZ)white).ToDouble();
        PrimaryRed = primary.R; PrimaryGreen = primary.G; PrimaryBlue = primary.B;
        White = new(white.X, white.Y, white.Z);

        _Value = New(White, primary);
    }

    private static double[][] New([NotNull] in IVector3<double> white, Primary3 primary)
    {
        double
            xr = primary.R.X,
            xg = primary.G.X,
            xb = primary.B.X,
            yr = primary.R.Y,
            yg = primary.G.Y,
            yb = primary.B.Y;

        var Xr = xr / yr;
        const double Yr = 1;
        var Zr = (1 - xr - yr) / yr;

        var Xg = xg / yg;
        const double Yg = 1;
        var Zg = (1 - xg - yg) / yg;

        var Xb = xb / yb;
        const double Yb = 1;
        var Zb = (1 - xb - yb) / yb;

        Matrix3x3<double> S = new
        (
            Xr, Xg, Xb,
            Yr, Yg, Yb,
            Zr, Zg, Zb
        );

        var W = white;
        var SW = (S as IMatrix3x3<double>).Invert() * W;

        var Sr = SW.X; var Sg = SW.Y; var Sb = SW.Z;

        double[][] M =
        [
            [Sr * Xr, Sg * Xg, Sb * Xb],
            [Sr * Yr, Sg * Yg, Sb * Yb],
            [Sr * Zr, Sg * Zg, Sb * Zb]
        ];
        return M;
    }

    /// <see cref="Region.Operator"/>
    #region

    public static Matrix3x3<double> operator +(ChromacityMatrix a, double b) => a.Do(Operator.Add, b);

    public static Matrix3x3<double> operator +(ChromacityMatrix a, IMatrix3x3<double> b) => a.Do(Operator.Add, b);

    public static Vector3<double> operator +(ChromacityMatrix a, IVector3<double> b) => a.Do(Operator.Add, b);

    public static Matrix3x3<double> operator -(ChromacityMatrix a, double b) => a.Do(Operator.Subtract, b);

    public static Matrix3x3<double> operator -(ChromacityMatrix a, IMatrix3x3<double> b) => a.Do(Operator.Subtract, b);

    public static Vector3<double> operator -(ChromacityMatrix a, IVector3<double> b) => a.Do(Operator.Subtract, b);

    public static Matrix3x3<double> operator *(ChromacityMatrix a, double b) => a.Do(Operator.Multiply, b);

    public static Matrix3x3<double> operator *(ChromacityMatrix a, IMatrix3x3<double> b) => a.Do(Operator.Multiply, b);

    public static Vector3<double> operator *(ChromacityMatrix a, IVector3<double> b) => a.Do(Operator.Multiply, b);

    public static Matrix3x3<double> operator /(ChromacityMatrix a, double b) => a.Do(Operator.Divide, b);

    public static Matrix3x3<double> operator /(ChromacityMatrix a, IMatrix3x3<double> b) => a.Do(Operator.Divide, b);

    public static Vector3<double> operator /(ChromacityMatrix a, IVector3<double> b) => a.Do(Operator.Divide, b);

    public static Matrix3x3<double> operator %(ChromacityMatrix a, double b) => a.Do(Operator.Modulo, b);

    public static Matrix3x3<double> operator %(ChromacityMatrix a, IMatrix3x3<double> b) => a.Do(Operator.Modulo, b);

    public static Vector3<double> operator %(ChromacityMatrix a, IVector3<double> b) => a.Do(Operator.Modulo, b);

    #endregion

    /// <see cref="IArray2D"/>

    object[][] IArray2D.ToArray() => XArray2D.ToArray(_Value, i => (object)i);

    public double[][] ToArray() => [.. _Value];

    public double[] ToArray(int y) => [.. _Value[y]];

    /// <see cref="IEnumerable"/>

    readonly IEnumerator IEnumerable.GetEnumerator() => (this as IEnumerable<double>).GetEnumerator();

    readonly IEnumerator<double> IEnumerable<double>.GetEnumerator() { foreach (double[] y in _Value) { foreach (double x in y) { yield return x; } } }

    /// <see cref="IFormattable"/>

    /// <inheritdoc cref="IMatrix.ToString{}(IMatrix2D{}, string, IFormatProvider)"/>
    public readonly override string ToString() => ToString(NumberFormat.General, CultureInfo.CurrentCulture);

    /// <inheritdoc cref="IMatrix.ToString{}(IMatrix2D{}, string, IFormatProvider)"/>
    public readonly string ToString(string format) => ToString(format, CultureInfo.CurrentCulture);

    /// <inheritdoc cref="IMatrix.ToString{}(IMatrix2D{}, string, IFormatProvider)"/>
    public readonly string ToString(string format, IFormatProvider provider) => IMatrix.ToString(this, format, provider);
}