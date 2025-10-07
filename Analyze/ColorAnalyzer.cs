using Ion;
using Ion.Analysis;
using Ion.Numeral;
using Ion.Reflect;
using System;
using System.Collections.Generic;
using System.Linq;
using static System.Math;

namespace Ion.Colors;

public class ColorAnalyzer() : Analyzer<Type, ColorAnalysis>()
{
    /// <see cref="Region.Property"/>

    public uint Depth { get; set; } = 10;

    public int Precision { get; set; } = 3;

    public ColorProfile Profile { get; set; } = ColorProfiles.sRGB;

    public ColorAnalysisType Type { get; set; }

    /// <see cref="Region.Method.Private"/>

    private static void Compare(IColor m, int length, double[] minimum, double[] maximum)
    {
        for (var i = 0; i < length; i++)
        {
            if (m.ToArray()[i] < minimum[i])
                minimum[i] = m.ToArray()[i];

            if (m.ToArray()[i] > maximum[i])
                maximum[i] = m.ToArray()[i];
        }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "<Pending>")]
    private static void Clean(int length, List<IColor> values, double[] minimum, double[] maximum)
    {
        var marked = new List<int>();
        for (var i = 0; i < length; i++)
        {
            var hell = values.Select(j => j.ToArray()[i]);

#pragma warning disable CA1851 // Possible multiple enumerations of 'IEnumerable' collection
            double avg = hell.Average();
            double std = Sqrt(hell.Average(j => Pow(j - avg, 2)));
#pragma warning restore CA1851 // Possible multiple enumerations of 'IEnumerable' collection

            for (var j = values.Count - 1; j >= 0; j--)
            {
                if ((Abs(values[j].ToArray()[i] - avg)) > (2 * std))
                    marked.Add(j);
            }
        }

        for (var i = marked.Count - 1; i >= 0; i--)
            values.RemoveAt(marked[i]);

        foreach (var i in values)
            Compare(i, length, minimum, maximum);
    }

    /// <summary>Converts all <see cref="RGB"/> colors to the given <see cref="IColor">color space</see> and back, and gets an estimate of accuracy for converting back and forth.</summary>
    /// <param name="Depth">A number in the range [1, ∞].</param>
    private double GetAccuracy(Type model, bool log = false)
    {
        Profile = Profile == default ? ColorProfiles.sRGB : Profile;

        try
        {
            var color = model.Create<IColor>();

            double sA = 0, n = 0;
            for (double r = 0; r < Depth; r++)
            {
                for (double g = 0; g < Depth; g++)
                {
                    for (double b = 0; b < Depth; b++)
                    {
                        //RGB > Lrgb > *
                        var x = IColor.New<RGB>(r / (Depth - 1) * 255, g / (Depth - 1) * 255, b / (Depth - 1) * 255);
                        color.From(x, Profile);

                        //* > Lrgb > RGB
                        color.To(out RGB y, Profile);

                        //Normalize
                        var e = x.XYZ.Normalize().New(e2 => e2.ToDouble());
                        var f = y.XYZ.Normalize().New(f2 => f2.ToDouble());

                        //Absolute difference
                        var rD = Clamp(1 - Abs(e.X - f.X).NaN(1), double.MinValue, double.MaxValue);
                        var gD = Clamp(1 - Abs(e.Y - f.Y).NaN(1), double.MinValue, double.MaxValue);
                        var bD = Clamp(1 - Abs(e.Z - e.Z).NaN(1), double.MinValue, double.MaxValue);

                        //Average of [absolute difference]
                        var dA = (rD + gD + bD) / 3;

                        //Sum of (average of [absolute difference])
                        sA += dA;
                        n++;
                    }
                }
            }
            return (sA / n * 100).Round(Precision);
        }
        catch (Exception e)
        {
            log.If(() => Ion.Analysis.Log.Write(e));
            return 0;
        }
    }

    /// <summary>
    /// Converts all <see cref="RGB"/> colors to the given <see cref="IColor">color space</see> and gets the estimated range of that <see cref="IColor">color space</see>.
    /// </summary>
    /// <param name="Depth">A number in the range [1, ∞].</param>
    private Range<Vector4> GetRange(Type model, out double accuracy, bool reverse = false, bool log = false)
    {
        Profile = Profile == default ? ColorProfiles.sRGB : Profile;

        var rgb = new RGB();
        var xyz = model.Create<IColor>();

        var dimension = xyz is IColor2 ? 2 : xyz is IColor3 ? 3 : xyz is IColor4 ? 4 : 0;

        var length = reverse ? 3 : xyz.Length;

        double[] minimum = new double[length], maximum = new double[length];
        for (var i = 0; i < length; i++)
        {
            minimum[i] = double.MaxValue; maximum[i] = double.MinValue;
        }

        var normalRange = new Range<double>(0, 1);
        Vector min = IColor.Minimum(model), max = IColor.Maximum(model);

        List<IColor> values = [];

        void f(IColor m)
        {
            Compare(m, length, minimum, maximum);
            //values.Add(m);
        }

        try
        {
            if (reverse == true)
            {
                double x, y, z, w;
                for (var r = 0.0; r < Depth; r++)
                {
                    for (var g = 0.0; g < Depth; g++)
                    {
                        if (dimension == 2)
                        {
                            x = r / (Depth - 1); y = g / (Depth - 1);

                            x = normalRange.ToRange(min[0], max[0], x);
                            y = normalRange.ToRange(min[1], max[1], y);

                            xyz = IColor.New(model, x, y);
                            xyz.To(out rgb, Profile);
                            f(rgb);
                            continue;
                        }
                        for (var b = 0.0; b < Depth; b++)
                        {
                            if (dimension == 3)
                            {
                                x = r / (Depth - 1); y = g / (Depth - 1); z = b / (Depth - 1);

                                x = normalRange.ToRange(min[0], max[0], x);
                                y = normalRange.ToRange(min[1], max[1], y);
                                z = normalRange.ToRange(min[2], max[2], z);

                                xyz = IColor.New(model, x, y, z);
                                xyz.To(out rgb, Profile);
                                f(rgb);
                                continue;
                            }
                            for (var a = 0.0; a < Depth; a++)
                            {
                                x = r / (Depth - 1); y = g / (Depth - 1); z = b / (Depth - 1); w = a / (Depth - 1);

                                x = normalRange.ToRange(min[0], max[0], x);
                                y = normalRange.ToRange(min[1], max[1], y);
                                z = normalRange.ToRange(min[2], max[2], z);
                                w = normalRange.ToRange(min[3], max[3], w);

                                xyz = IColor.New(model, x, y, z, w);
                                xyz.To(out rgb, Profile);
                                f(rgb);
                            }
                        }
                    }
                }
            }
            else
            {
                for (var r = 0.0; r < Depth; r++)
                {
                    for (var g = 0.0; g < Depth; g++)
                    {
                        for (var b = 0.0; b < Depth; b++)
                        {
                            //[0, 1]
                            double x = r / (Depth - 1), y = g / (Depth - 1), z = b / (Depth - 1);

                            rgb = IColor.New<RGB>(x * 255, y * 255, z * 255);
                            xyz.From(rgb, Profile);
                            f(xyz);
                        }
                    }
                }
            }

            //Clean(length, values, minimum, maximum);

            var aV = new double[length];
            for (var i = 0; i < length; i++)
            {
                var s = Abs(maximum[i]) + Abs(minimum[i]);
                var t = reverse ? 255 : max[i] + Abs(min[i]);
                aV[i] = ((s > t ? t / s : s / t) * 100).Round(Precision);

                minimum[i] = Clamp(minimum[i], -999, 999).Round(Precision);
                maximum[i] = Clamp(maximum[i], -999, 999).Round(Precision);
            }

            accuracy = new Vector(aV).Sum() / Convert.ToDouble(length);
            return new(new(minimum[0], minimum[1], minimum[2], minimum[3]), new(maximum[0], maximum[1], maximum[2], maximum[3]));
        }
        catch (Exception e)
        {
            log.If(() => Ion.Analysis.Log.Write(e));

            accuracy = 0;
            return new(Vector4.Zero);
        }
    }

    /// <see cref="Region.Method.Public"/>

    public override ColorAnalysis Analyze(Type type)
    {
        switch (Type)
        {
            case ColorAnalysisType.Accuracy:
                var a = GetAccuracy(type);
                return new ColorAccuracyAnalysis(type.Name, $"{a}%");
            case ColorAnalysisType.Range:
                var b = GetRange(type, out double x, true);
                return new ColorRangeAnalysis(type.Name, $"{x.Round(Precision)}%", b.Minimum, b.Maximum);
            case ColorAnalysisType.RangeInverse:
                var c = GetRange(type, out double y, false);
                return new ColorRangeInverseAnalysis(type.Name, $"{y.Round(Precision)}%", c.Minimum, c.Maximum, IColor.Minimum(type), IColor.Maximum(type));
        }
        return default;
    }
}