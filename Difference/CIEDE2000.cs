using Ion.Numeral;
using static Ion.Numeral.Number;
using static System.Math;

namespace Ion.Colors;

/// <summary>CIE Delta-E 2000 color difference formula.</summary>
/// <remarks>https://github.com/tompazourek/Colourful</remarks>
[Name("CIE Delta-E 2000")]
public record class CIEDE2000ColorDifference() : IColorDifference<Lab>, IColorDifference
{
    /// Parametric weighting factors
    private const double k_H = 1, k_L = 1, k_C = 1;

    /// <param name="x">Reference color.</param>
    /// <param name="y">Sample color.</param>
    /// <remarks>Implemented according to: Sharma, Gaurav; Wencheng Wu, Edul N. Dalal (2005). "The CIEDE2000 color-difference formula: Implementation notes, supplementary test data, and mathematical observations" (http://www.ece.rochester.edu/~gsharma/ciede2000/ciede2000noteCRNA.pdf)</remarks>
    /// <returns>Delta-E (2000) color difference.</returns>
    public double ComputeDifference(in Lab x, in Lab y)
    {
        // 1. Calculate C_prime, h_prime
        Calculate_a_prime(x.Y, y.Y, x.Z, y.Z, out var a_prime0, out var a_prime1);
        Calculate_C_prime(in a_prime0, in a_prime1, x.Z, y.Z, out var C_prime0, out var C_prime1);
        Calculate_h_prime(in a_prime0, in a_prime1, x.Z, y.Z, out var h_prime0, out var h_prime1);

        // 2. Calculate dL_prime, dC_prime, dH_prime
        var dL_prime = y.X - x.X; // eq. (8)
        var dC_prime = C_prime1 - C_prime0; // eq. (9)
        var dh_prime = Calculate_dh_prime(in C_prime0, in C_prime1, in h_prime0, in h_prime1);
        var dH_prime = 2 * Sqrt(C_prime0 * C_prime1) * (dh_prime / 2).DSin(); // eq. (11)

        // 3. Calculate CIEDE2000 Color-Difference dE00
        var L_prime_mean = (x.X + y.X) / 2; // eq. (12)
        var C_prime_mean = (C_prime0 + C_prime1) / 2; // eq. (13)
        var h_prime_mean = Calculate_h_prime_mean(in h_prime0, in h_prime1, in C_prime0, in C_prime1);
        var T = 1 - 0.17 * (h_prime_mean - 30).DCos() + 0.24 * (2 * h_prime_mean).DCos()
            + 0.32 * (3 * h_prime_mean + 6).DCos() - 0.20 * (4 * h_prime_mean - 63).DCos(); // eq. (15)

        var dTheta = 30 * Exp(-((h_prime_mean - 275) / 25).Pow2()); // eq. (16)
        var R_C = 2 * ((C_prime_mean).Pow7() /((C_prime_mean).Pow7() + 25.Pow7())).Root2(); // eq. (17)
        var S_L = 1 + 0.015 * (L_prime_mean - 50).Pow2() / Sqrt(20 + (L_prime_mean - 50).Pow2()); // eq. (18)
        var S_C = 1 + 0.045 * C_prime_mean; // eq. (19)
        var S_H = 1 + 0.015 * C_prime_mean * T; // eq. (20)
        var R_T = -(2 * dTheta).DSin() * R_C; // eq. (21)

        var dE00 = Sqrt(
            (dL_prime / (k_L * S_L)).Pow2() +
            (dC_prime / (k_C * S_C)).Pow2() +
            (dH_prime / (k_H * S_H)).Pow2() +
            R_T * (dC_prime / (k_C * S_C)) * (dH_prime / (k_H * S_H))
        ); // eq. (22)

        return dE00;
    }

    private static void Calculate_a_prime(in double a0,
        in double a1,
        in double b0,
        in double b1,
        out double a_prime0,
        out double a_prime1)
    {
        var C_ab0 = Sqrt(a0 * a0 + b0 * b0); // eq. (2)
        var C_ab1 = Sqrt(a1 * a1 + b1 * b1);

        var C_ab_mean = (C_ab0 + C_ab1) / 2; // eq. (3)

        var G = 0.5d * (1 - Sqrt((C_ab_mean).Pow7() / ((C_ab_mean).Pow7() + 25.Pow7()))); // eq. (4)

        a_prime0 = (1 + G) * a0; // eq. (5)
        a_prime1 = (1 + G) * a1;
    }

    private static void Calculate_C_prime(in double a_prime0,
        in double a_prime1,
        in double b0,
        in double b1,
        out double C_prime0,
        out double C_prime1)
    {
        C_prime0 = Sqrt(a_prime0 * a_prime0 + b0 * b0); // eq. (6)
        C_prime1 = Sqrt(a_prime1 * a_prime1 + b1 * b1);
    }

    private static void Calculate_h_prime(in double a_prime0,
        in double a_prime1,
        in double b0,
        in double b1,
        out double h_prime0,
        out double h_prime1)
    {
        // eq. (7)
        var hRadians = Atan2(b0, a_prime0);
        var hDegrees = new Angle(hRadians, AngleType.Radian).Convert(AngleType.Degree);
        h_prime0 = hDegrees;

        hRadians = Atan2(b1, a_prime1);
        hDegrees = new Angle(hRadians, AngleType.Radian).Convert(AngleType.Degree);
        h_prime1 = hDegrees;
    }

    private static double Calculate_dh_prime(in double C_prime0, in double C_prime1, in double h_prime0, in double h_prime1)
    {
        // eq. (10)
        if (C_prime0 * C_prime1 == 0d)
            return 0;

        var delta = h_prime1 - h_prime0;
        if (Abs(delta) <= 180)
            return delta;

        if (delta > 180)
            return delta - 360;

        // note: no need to check for (delta < -180), it's always true
        return delta + 360;
    }

    private static double Calculate_h_prime_mean(in double h_prime0, in double h_prime1, in double C_prime0, in double C_prime1)
    {
        // eq. (14)
        var sum = h_prime0 + h_prime1;
        if (C_prime0 * C_prime1 == 0d)
            return sum;

        var delta = h_prime0 - h_prime1;
        if (Abs(delta) <= 180)
            return sum / 2;

        if (Abs(delta) > 180 &&
            sum < 360)
            return (sum + 360) / 2;

        // note: no need to check for (Abs(delta) > 180 && sum >= 360), it's always true
        return (sum - 360) / 2;
    }

    /// <inheritdoc/>
    double IColorDifference.ComputeDifference(in IColor x, in IColor y) => ComputeDifference((Lab)x, (Lab)y);
}