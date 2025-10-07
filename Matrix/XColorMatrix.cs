using Ion;
using Ion.Numeral;
using System;

namespace Ion.Colors;

[Extend(typeof(Matrix<IColor3>),
    typeof(Matrix<IColor4>))]
[Extend(typeof(IMatrix<ByteVector3>),
    typeof(IMatrix<ByteVector4>))]
public static class XColorMatrix
{
    /// <see cref="IMatrix{IColor3}"/>
    /// <see cref="IMatrix{VectorByte3}"/>
    /// <see cref="IMatrix{VectorByte4}"/>

    #region Blend

    public static Matrix<TColor> Blend<TColor>(this IMatrix<TColor> i, IColor3 b, BlendModes blendMode = BlendModes.Normal, double amount = 1, ColorProfile profile = default) where TColor : IColor3, new()
    => i.NewType(j =>
    {
        var a1 = j.To<RGB>(profile);
        var a2 = new ByteVector3(a1.X.ToByte(), a1.Y.ToByte(), a1.Z.ToByte());

        var b1 = b.To<RGB>(profile);
        var b2 = new ByteVector3(b1.X.ToByte(), b1.Y.ToByte(), b1.Z.ToByte());

        var result = a2.Blend(b2, blendMode, amount, profile);
        return new RGB(result).To<TColor>(profile);
    });

    public static Matrix<TColor> Blend<TColor>(this IMatrix<TColor> i, IVector3<byte> b, BlendModes blendMode = BlendModes.Normal, double amount = 1, ColorProfile profile = default) where TColor : IColor3, new()
    => i.NewType(j =>
    {
        var a1 = j.To<RGB>(profile);
        var a2 = new ByteVector3(a1.X.ToByte(), a1.Y.ToByte(), a1.Z.ToByte());

        var result = a2.Blend(b, blendMode, amount, profile);
        return new RGB(result).To<TColor>(profile);
    });

    /// <exception cref="MatrixColumnAndRowMismatch"/>
    public static Matrix<TColor> Blend<TColor>(this IMatrix<TColor> i, IMatrix<TColor> b, BlendModes blendMode = BlendModes.Normal, double amount = 1, ColorProfile profile = default) where TColor : IColor3, new()
    => i.NewType((y, x, j) =>
    {
        Throw.IfNotEqual<MatrixColumnAndRowMismatch>(i.Columns, b.Columns);
        Throw.IfNotEqual<MatrixColumnAndRowMismatch>(i.Rows, b.Rows);

        var a1 = j.To<RGB>(profile);
        var a2 = new ByteVector3(a1.X.ToByte(), a1.Y.ToByte(), a1.Z.ToByte());

        var b1 = b[y, x].To<RGB>(profile);
        var b2 = new ByteVector3(b1.X.ToByte(), b1.Y.ToByte(), b1.Z.ToByte());

        var result = a2.Blend(b2, blendMode, amount, profile);
        return new RGB(result).To<TColor>(profile);
    });

    /// <exception cref="MatrixColumnAndRowMismatch"/>
    public static Matrix<TColor> Blend<TColor>(this IMatrix<TColor> i, IMatrix<ByteVector3> b, BlendModes blendMode = BlendModes.Normal, double amount = 1, ColorProfile profile = default) where TColor : IColor3, new()
    => i.NewType((y, x, j) =>
    {
        Throw.IfNotEqual<MatrixColumnAndRowMismatch>(i.Columns, b.Columns);
        Throw.IfNotEqual<MatrixColumnAndRowMismatch>(i.Rows, b.Rows);

        var a1 = j.To<RGB>(profile);
        var a2 = new ByteVector3(a1.X.ToByte(), a1.Y.ToByte(), a1.Z.ToByte());

        var result = a2.Blend(b[y, x], blendMode, amount, profile);
        return new RGB(result).To<TColor>(profile);
    });

    /// <exception cref="MatrixColumnAndRowMismatch"/>
    public static Matrix<TColor> Blend<TColor>(this IMatrix<TColor> i, IMatrix<ByteVector4> b, BlendModes blendMode = BlendModes.Normal, double amount = 1, ColorProfile profile = default) where TColor : IColor3, new()
    => i.NewType((y, x, j) =>
    {
        Throw.IfNotEqual<MatrixColumnAndRowMismatch>(i.Columns, b.Columns);
        Throw.IfNotEqual<MatrixColumnAndRowMismatch>(i.Rows, b.Rows);

        var a1 = j.To<RGB>(profile);
        var a2 = new ByteVector3(a1.X.ToByte(), a1.Y.ToByte(), a1.Z.ToByte());

        var result = a2.Blend(b[y, x], blendMode, amount, profile);
        return new RGB(result).To<TColor>(profile);
    });

    #endregion

    #region Transform

    public static Matrix<TColor> Transform<TColor>(this IMatrix<ByteVector3> i, ColorProfile profile = default) where TColor : IColor3, new()
        => i.NewType(j => new RGB(j).To<TColor>(profile));

    public static Matrix<TColor> Transform<TColor>(this IMatrix<ByteVector3> i, Func<TColor, TColor> action, ColorProfile profile = default) where TColor : IColor3, new()
        => i.NewType(j => action(new RGB(j).To<TColor>(profile)));

    public static Matrix<TColor> Transform<TColor>(this IMatrix<ByteVector4> i, ColorProfile profile = default) where TColor : IColor3, new()
        => i.NewType(j => new RGB(j).To<TColor>(profile));

    public static Matrix<TColor> Transform<TColor>(this IMatrix<ByteVector4> i, Func<TColor, TColor> action, ColorProfile profile = default) where TColor : IColor3, new()
        => i.NewType(j => action(new RGB(j).To<TColor>(profile)));

    public static ColorMatrix3 Transform<TColor>(this IMatrix<TColor> i, ColorProfile profile = default) where TColor : IColor3, new()
        => i.Transform(j => j, profile);

    public static ColorMatrix3 Transform<TColor>(this IMatrix<TColor> i, Func<ByteVector3, ByteVector3> action, ColorProfile profile = default) where TColor : IColor3, new()
        => new(Array2D.Get(i.Rows, i.Columns, (row, column) =>
        {
            var a = i[row, column];
            var b = a.To<RGB>(profile);
            var result = action(new ByteVector3(b));
            return result;
        }));

    public static Matrix<TNew> Transform<TOld, TNew>(this IMatrix<TOld> i, ColorProfile profile = default) where TOld : IColor where TNew : IColor3, new()
        => i.Transform<TOld, TNew>(j => j, profile);

    public static Matrix<TNew> Transform<TOld, TNew>(this IMatrix<TOld> i, Func<TNew, TNew> action, ColorProfile profile = default) where TOld : IColor where TNew : IColor3, new()
        => i.NewType(j => action(j.To<TNew>(profile)));

    #endregion

    #region X

    public static Matrix<TColor> X<TColor>(this IMatrix<TColor> i, Double1 x, ColorProfile profile = default) where TColor : IColor3, System.Numerics.IMinMaxValue<TColor>, new()
        => i.X(j => x, profile);

    public static Matrix<TColor> X<TColor>(this IMatrix<TColor> i, Func<Double1, Double1> x, ColorProfile profile = default) where TColor : IColor3, System.Numerics.IMinMaxValue<TColor>, new()
        => new(Array2D.Get(i.Rows, i.Columns, (row, column) =>
        {
            double m = TColor.MinValue.X.ToDouble(), n = TColor.MaxValue.X.ToDouble();

            var b = i[row, column];

            double e = b.X.Normalize(m, n), f = x(e).Denormalize(m, n);
            b.X = f;

            var c = b.To<TColor>(profile);
            return c;
        }));

    #endregion

    #region Y

    public static Matrix<TColor> Y<TColor>(this IMatrix<TColor> i, Double1 y, ColorProfile profile = default) where TColor : IColor3, System.Numerics.IMinMaxValue<TColor>, new()
        => i.Y(j => y, profile);

    public static Matrix<TColor> Y<TColor>(this IMatrix<TColor> i, Func<Double1, Double1> y, ColorProfile profile = default) where TColor : IColor3, System.Numerics.IMinMaxValue<TColor>, new()
        => new(Array2D.Get(i.Rows, i.Columns, (row, column) =>
        {
            double m = TColor.MinValue.Y.ToDouble(), n = TColor.MaxValue.Y.ToDouble();

            var b = i[row, column];

            double e = b.Y.Normalize(m, n), f = y(e).Denormalize(m, n);
            b.Y = f;

            var c = b.To<TColor>(profile);
            return c;
        }));

    #endregion

    #region Z

    public static Matrix<TColor> Z<TColor>(this IMatrix<TColor> i, Double1 z, ColorProfile profile = default) where TColor : IColor3, System.Numerics.IMinMaxValue<TColor>, new()
        => i.Z(j => z, profile);

    public static Matrix<TColor> Z<TColor>(this IMatrix<TColor> i, Func<Double1, Double1> z, ColorProfile profile = default) where TColor : IColor3, System.Numerics.IMinMaxValue<TColor>, new()
        => new(Array2D.Get(i.Rows, i.Columns, (row, column) =>
        {
            double m = TColor.MinValue.Z.ToDouble(), n = TColor.MaxValue.Z.ToDouble();

            var b = i[row, column];

            double e = b.Z.Normalize(m, n), f = z(e).Denormalize(m, n);
            b.Z = f;

            var c = b.To<TColor>(profile);
            return c;
        }));

    #endregion

    /// <see cref="IMatrix{VectorByte3}"/>
    /// <see cref="IMatrix{VectorByte4}"/>

    #region X

    public static ColorMatrix3 X<TColor>(this IMatrix<ByteVector3> i, Double1 x, ColorProfile profile = default) where TColor : IColor3, System.Numerics.IMinMaxValue<TColor>, new()
        => i.X<TColor>(j => x, profile);

    public static ColorMatrix3 X<TColor>(this IMatrix<ByteVector3> i, Func<Double1, Double1> x, ColorProfile profile = default) where TColor : IColor3, System.Numerics.IMinMaxValue<TColor>, new()
        => new(Array2D.Get(i.Rows, i.Columns, (row, column) =>
        {
            double m = TColor.MinValue.X.ToDouble(), n = TColor.MaxValue.X.ToDouble();

            var a = new RGB(i[row, column]);
            var b = a.To<TColor>(profile);

            double e = b.X.Normalize(m, n), f = x(e).Denormalize(m, n);
            b.X = f;

            var c = b.To<RGB>(profile);
            return new ByteVector3(c);
        }));

    public static ColorMatrix4 X<TColor>(this IMatrix<ByteVector4> i, Double1 x, ColorProfile profile = default) where TColor : IColor3, System.Numerics.IMinMaxValue<TColor>, new()
        => i.X<TColor>(j => x, profile);

    public static ColorMatrix4 X<TColor>(this IMatrix<ByteVector4> i, Func<Double1, Double1> x, ColorProfile profile = default) where TColor : IColor3, System.Numerics.IMinMaxValue<TColor>, new()
        => new(Array2D.Get(i.Rows, i.Columns, (row, column) =>
        {
            double m = TColor.MinValue.X.ToDouble(), n = TColor.MaxValue.X.ToDouble();

            var a = new RGB(i[row, column]);
            var b = a.To<TColor>(profile);

            double e = b.X.Normalize(m, n), f = x(e).Denormalize(m, n);
            b.X = f;

            var c = b.To<RGB>(profile);
            return new ByteVector4(c).A(i[row, column].A);
        }));

    #endregion

    #region Y

    public static ColorMatrix3 Y<TColor>(this IMatrix<ByteVector3> i, Double1 y, ColorProfile profile = default) where TColor : IColor3, System.Numerics.IMinMaxValue<TColor>, new()
        => i.Y<TColor>(j => y, profile);

    public static ColorMatrix3 Y<TColor>(this IMatrix<ByteVector3> i, Func<Double1, Double1> y, ColorProfile profile = default) where TColor : IColor3, System.Numerics.IMinMaxValue<TColor>, new()
        => new(Array2D.Get(i.Rows, i.Columns, (row, column) =>
        {
            double m = TColor.MinValue.Y.ToDouble(), n = TColor.MaxValue.Y.ToDouble();

            var a = new RGB(i[row, column]);
            var b = a.To<TColor>(profile);

            double e = b.Y.Normalize(m, n), f = y(e).Denormalize(m, n);
            b.Y = f;

            var c = b.To<RGB>(profile);
            return new ByteVector3(c);
        }));

    public static ColorMatrix4 Y<TColor>(this IMatrix<ByteVector4> i, Double1 y, ColorProfile profile = default) where TColor : IColor3, System.Numerics.IMinMaxValue<TColor>, new()
        => i.Y<TColor>(j => y, profile);

    public static ColorMatrix4 Y<TColor>(this IMatrix<ByteVector4> i, Func<Double1, Double1> y, ColorProfile profile = default) where TColor : IColor3, System.Numerics.IMinMaxValue<TColor>, new()
        => new(Array2D.Get(i.Rows, i.Columns, (row, column) =>
        {
            double m = TColor.MinValue.Y.ToDouble(), n = TColor.MaxValue.Y.ToDouble();

            var a = new RGB(i[row, column]);
            var b = a.To<TColor>(profile);

            double e = b.Y.Normalize(m, n), f = y(e).Denormalize(m, n);
            b.Y = f;

            var c = b.To<RGB>(profile);
            return new ByteVector4(c).A(i[row, column].A);
        }));

    #endregion

    #region Z

    public static ColorMatrix3 Z<TColor>(this IMatrix<ByteVector3> i, Double1 z, ColorProfile profile = default) where TColor : IColor3, System.Numerics.IMinMaxValue<TColor>, new()
        => i.Z<TColor>(j => z, profile);

    public static ColorMatrix3 Z<TColor>(this IMatrix<ByteVector3> i, Func<Double1, Double1> z, ColorProfile profile = default) where TColor : IColor3, System.Numerics.IMinMaxValue<TColor>, new()
        => new(Array2D.Get(i.Rows, i.Columns, (row, column) =>
        {
            double m = TColor.MinValue.Z.ToDouble(), n = TColor.MaxValue.Z.ToDouble();

            var a = new RGB(i[row, column]);
            var b = a.To<TColor>(profile);

            double e = b.Z.Normalize(m, n), f = z(e).Denormalize(m, n);
            b.Z = f;

            var c = b.To<RGB>(profile);
            return new ByteVector3(c);
        }));

    public static ColorMatrix4 Z<TColor>(this IMatrix<ByteVector4> i, Double1 z, ColorProfile profile = default) where TColor : IColor3, System.Numerics.IMinMaxValue<TColor>, new()
        => i.Z<TColor>(j => z, profile);

    public static ColorMatrix4 Z<TColor>(this IMatrix<ByteVector4> i, Func<Double1, Double1> z, ColorProfile profile = default) where TColor : IColor3, System.Numerics.IMinMaxValue<TColor>, new()
        => new(Array2D.Get(i.Rows, i.Columns, (row, column) =>
        {
            double m = TColor.MinValue.Z.ToDouble(), n = TColor.MaxValue.Z.ToDouble();

            var a = new RGB(i[row, column]);
            var b = a.To<TColor>(profile);

            double e = b.Z.Normalize(m, n), f = z(e).Denormalize(m, n);
            b.Z = f;

            var c = b.To<RGB>(profile);
            return new ByteVector4(c).A(i[row, column].A);
        }));

    #endregion

    /// <see cref="IMatrix{TSelf,VectorByte3}"/>
    /// <see cref="IMatrix{TSelf,VectorByte4}"/>

    #region Blend

    public static TSelf Blend<TSelf>(this IMatrix<TSelf, ByteVector3> i, IColor3 b, BlendModes blendMode = BlendModes.Normal, double amount = 1, ColorProfile profile = default) where TSelf : IMatrix<TSelf, ByteVector3>
        => i.New(a =>
        {
            var b1 = b.To<RGB>(profile);
            var b2 = new ByteVector3(b1.X.ToByte(), b1.Y.ToByte(), b1.Z.ToByte());

            return a.Blend(b2, blendMode, amount, profile);
        });

    public static TSelf Blend<TSelf>(this IMatrix<TSelf, ByteVector4> i, IColor3 b, BlendModes blendMode = BlendModes.Normal, double amount = 1, ColorProfile profile = default) where TSelf : IMatrix<TSelf, ByteVector4>
        => i.New(a =>
        {
            var b1 = b.To<RGB>(profile);
            var b2 = new ByteVector3(b1.X.ToByte(), b1.Y.ToByte(), b1.Z.ToByte());

            return a.Blend(b2, blendMode, amount, profile);
        });

    public static TSelf Blend<TSelf>(this IMatrix<TSelf, ByteVector3> i, IVector3<byte> b, BlendModes blendMode = BlendModes.Normal, double amount = 1, ColorProfile profile = default) where TSelf : IMatrix<TSelf, ByteVector3>
        => i.New(a => a.Blend(b, blendMode, amount, profile));

    public static TSelf Blend<TSelf>(this IMatrix<TSelf, ByteVector4> i, IVector3<byte> b, BlendModes blendMode = BlendModes.Normal, double amount = 1, ColorProfile profile = default) where TSelf : IMatrix<TSelf, ByteVector4>
        => i.New(a => a.Blend(b, blendMode, amount, profile));

    /// <exception cref="MatrixColumnAndRowMismatch"/>
    public static TSelf Blend<TSelf>(this IMatrix<TSelf, ByteVector3> i, IMatrix<ByteVector3> b, BlendModes blendMode = BlendModes.Normal, double amount = 1, ColorProfile profile = default) where TSelf : IMatrix<TSelf, ByteVector3>
    {
        Throw.IfNotEqual<MatrixColumnAndRowMismatch>(i.Columns, b.Columns);
        Throw.IfNotEqual<MatrixColumnAndRowMismatch>(i.Rows, b.Rows);
        return i.New((y, x, a) => a.Blend(b[y, x], blendMode, amount, profile));
    }

    /// <exception cref="MatrixColumnAndRowMismatch"/>
    public static TSelf Blend<TSelf>(this IMatrix<TSelf, ByteVector3> i, IMatrix<ByteVector4> b, BlendModes blendMode = BlendModes.Normal, double amount = 1, ColorProfile profile = default) where TSelf : IMatrix<TSelf, ByteVector3>
    {
        Throw.IfNotEqual<MatrixColumnAndRowMismatch>(i.Columns, b.Columns);
        Throw.IfNotEqual<MatrixColumnAndRowMismatch>(i.Rows, b.Rows);
        return i.New((y, x, a) => a.Blend(b[y, x], blendMode, amount, profile));
    }

    /// <exception cref="MatrixColumnAndRowMismatch"/>
    public static TSelf Blend<TSelf>(this IMatrix<TSelf, ByteVector4> i, IMatrix<ByteVector3> b, BlendModes blendMode = BlendModes.Normal, double amount = 1, ColorProfile profile = default) where TSelf : IMatrix<TSelf, ByteVector4>
    {
        Throw.IfNotEqual<MatrixColumnAndRowMismatch>(i.Columns, b.Columns);
        Throw.IfNotEqual<MatrixColumnAndRowMismatch>(i.Rows, b.Rows);
        return i.New((y, x, a) => a.Blend(b[y, x], blendMode, amount, profile));
    }

    /// <exception cref="MatrixColumnAndRowMismatch"/>
    public static TSelf Blend<TSelf>(this IMatrix<TSelf, ByteVector4> i, IMatrix<ByteVector4> b, BlendModes blendMode = BlendModes.Normal, double amount = 1, ColorProfile profile = default) where TSelf : IMatrix<TSelf, ByteVector4>
    {
        Throw.IfNotEqual<MatrixColumnAndRowMismatch>(i.Columns, b.Columns);
        Throw.IfNotEqual<MatrixColumnAndRowMismatch>(i.Rows, b.Rows);
        return i.New((y, x, a) => a.Blend(b[y, x], blendMode, amount, profile));
    }

    #endregion

    #region Resize

    [NotImplemented]
    public static unsafe TSelf Resize<TSelf>(this IMatrix<TSelf, ByteVector3> input, ISize<int> size, ColorInterpolation interpolation) where TSelf : IMatrix<TSelf, ByteVector3>
        => throw new NotImplementedException();

    public static unsafe TSelf Resize<TSelf>(this IMatrix<TSelf, ByteVector4> input, ISize<int> size, ColorInterpolation interpolation) where TSelf : IMatrix<TSelf, ByteVector4>
    {
        var result = new ByteVector4[size.Height][];

        var xs = input.Columns.ToSingle() / size.Width.ToSingle();
        var ys = input.Rows.ToSingle() / size.Height.ToSingle();

        float fracx, fracy, ifracx, ifracy, sx, sy, l0, l1, rf, gf, bf;
        int c, x0, x1, y0, y1;
        byte c1a, c1r, c1g, c1b, c2a, c2r, c2g, c2b, c3a, c3r, c3g, c3b, c4a, c4r, c4g, c4b;
        byte a, r, g, b;

        int[] pixels = new int[input.Columns * input.Rows];

        var total = 0;
        Array2D.Do(input.Rows, input.Columns, (y, x) => pixels[total++] = input[y, x].Encode());

        int widthSource = input.Columns, heightSource = input.Rows;

        /// <see cref="ColorInterpolation.NearestNeighbor"/>
        if (interpolation == ColorInterpolation.NearestNeighbor)
        {
            for (var y = 0; y < size.Height; y++)
            {
                result[y] = new ByteVector4[size.Width];
                for (var x = 0; x < size.Width; x++)
                {
                    sx = x * xs;
                    sy = y * ys;

                    x0 = (int)sx;
                    y0 = (int)sy;

                    var i = y0 * widthSource + x0;
                    var j = pixels[i].Decode();
                    result[y][x] = j;
                }
            }
        }

        /// <see cref="ColorInterpolation.Bilinear"/>
        else if (interpolation == ColorInterpolation.Bilinear)
        {
            for (var y = 0; y < size.Height; y++)
            {
                result[y] = new ByteVector4[size.Width];
                for (var x = 0; x < size.Width; x++)
                {
                    sx = x * xs;
                    sy = y * ys;

                    x0 = (int)sx;
                    y0 = (int)sy;

                    //Calculate coordinates of the 4 interpolation points
                    fracx = sx - x0;
                    fracy = sy - y0;

                    ifracx = 1f - fracx;
                    ifracy = 1f - fracy;

                    x1 = x0 + 1;
                    if (x1 >= widthSource)
                        x1 = x0;

                    y1 = y0 + 1;
                    if (y1 >= heightSource)
                        y1 = y0;

                    //Read source Vector4
                    c = pixels[y0 * widthSource + x0];
                    c1a = (byte)(c >> 24);
                    c1r = (byte)(c >> 16);
                    c1g = (byte)(c >> 8);
                    c1b = (byte)(c);

                    c = pixels[y0 * widthSource + x1];
                    c2a = (byte)(c >> 24);
                    c2r = (byte)(c >> 16);
                    c2g = (byte)(c >> 8);
                    c2b = (byte)(c);

                    c = pixels[y1 * widthSource + x0];
                    c3a = (byte)(c >> 24);
                    c3r = (byte)(c >> 16);
                    c3g = (byte)(c >> 8);
                    c3b = (byte)(c);

                    c = pixels[y1 * widthSource + x1];
                    c4a = (byte)(c >> 24);
                    c4r = (byte)(c >> 16);
                    c4g = (byte)(c >> 8);
                    c4b = (byte)(c);


                    // Alpha
                    l0 = ifracx * c1a + fracx * c2a;
                    l1 = ifracx * c3a + fracx * c4a;
                    a = (byte)(ifracy * l0 + fracy * l1);

                    // Red
                    l0 = ifracx * c1r + fracx * c2r;
                    l1 = ifracx * c3r + fracx * c4r;
                    rf = ifracy * l0 + fracy * l1;

                    // Green
                    l0 = ifracx * c1g + fracx * c2g;
                    l1 = ifracx * c3g + fracx * c4g;
                    gf = ifracy * l0 + fracy * l1;

                    // Blue
                    l0 = ifracx * c1b + fracx * c2b;
                    l1 = ifracx * c3b + fracx * c4b;
                    bf = ifracy * l0 + fracy * l1;

                    r = (byte)rf;
                    g = (byte)gf;
                    b = (byte)bf;

                    result[y][x] = new ByteVector4(r, g, b, a);
                }
            }
        }

        return TSelf.Create((TSelf)input, result);
    }

    #endregion
}