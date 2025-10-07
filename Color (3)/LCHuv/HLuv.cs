using Ion.Numeral;
using System;
using System.Collections.Generic;
using static System.Math;

namespace Ion.Colors;

[ColorOf<LCHuv>]
[ComponentGroup(ComponentGroup.H | ComponentGroup.HS)]
public abstract record class HLuv<TSelf>(double X, double Y, double Z)
    : Color3<TSelf, double, LCHuv>(X, Y, Z)
    where TSelf : System.Numerics.IMinMaxValue<TSelf>, IColor3<TSelf, double>
{
    protected static readonly Matrix3x3<double> M = new
    (
         3.240969941904521, -1.537383177570093, -0.498610760293000,
        -0.969243636280870,  1.875967501507720,  0.041555057407175,
         0.055630079696993, -0.203976958888970,  1.056971514242878
    );

    protected static IList<double[]> GetBounds(double L)
    {
        var result = new List<double[]>();

        double sub1 = Pow(L + 16, 3) / 1560896;
        double sub2 = sub1 > CIE.IEpsilon ? sub1 : L / CIE.IKappa;

        for (int c = 0; c < 3; ++c)
        {
            var m1 = M[c, 0];
            var m2 = M[c, 1];
            var m3 = M[c, 2];

            for (int t = 0; t < 2; ++t)
            {
                var top1 = (284517 * m1 - 94839 * m3) * sub2;
                var top2 = (838422 * m3 + 769860 * m2 + 731718 * m1) * L * sub2 - 769860 * t * L;
                var bottom = (632260 * m3 - 126452 * m2) * sub2 + 126452 * t;

                result.Add([top1 / bottom, top2 / bottom]);
            }
        }

        return result;
    }

    protected static double GetChroma(double L)
    {
        var bounds = GetBounds(L);
        double min = double.MaxValue;

        for (int i = 0; i < 2; ++i)
        {
            var m1 = bounds[i][0];
            var b1 = bounds[i][1];
            var line = new double[] { m1, b1 };

            double x = GetIntersection(line, [-1 / m1, 0]);
            double length = GetDistance([x, b1 + x * m1]);

            min = Min(min, length);
        }

        return min;
    }

    protected static double GetChroma(double L, double H)
    {
        double hrad = H / 360 * PI * 2;

        var bounds = GetBounds(L);
        double min = double.MaxValue;
        foreach (var bound in bounds)
        {
            if (GetRayLength(hrad, bound, out double length))
                min = Min(min, length);
        }

        return min;
    }

    protected static double GetDistance(IList<double> point)
        => Sqrt(Pow(point[0], 2) + Pow(point[1], 2));

    protected static double GetIntersection(IList<double> lineA, IList<double> lineB)
        => (lineA[1] - lineB[1]) / (lineB[0] - lineA[0]);

    protected static bool GetRayLength(double theta, IList<double> line, out double length)
    {
        length = line[1] / (Sin(theta) - line[0] * Cos(theta));
        return length >= 0;
    }
}