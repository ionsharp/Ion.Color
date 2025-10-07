using Ion.Numeral;
using System.Collections.Generic;
using static Ion.Numeral.Number;
using static System.Math;

namespace Ion.Colors;

/// <summary>
/// <para><b>Correlated Color Temperature</b> (CCT)</para>
/// Get chromacity (<see cref="XY"/>) of <b>Correlated Color Temperature</b>.
/// </summary>
/// <remarks>https://github.com/tompazourek/Colourful</remarks>
public static class ChromacityTemperature
{
    /// <summary>Gets chromaticity of given CCT (in K).</summary>
    /// <remarks>http://en.wikipedia.org/wiki/Planckian_locus#Approximation</remarks>
    public static XY GetChromacity(in double kelvin)
    {
        double x_c;

        if (kelvin <= 4000) // correctly 1667 <= T <= 4000
            x_c = -0.2661239 * (1000000000 / kelvin.Pow3()) - 0.2343580 * (1000000 / kelvin.Pow2()) + 0.8776956 * (1000 / kelvin) + 0.179910;

        else // correctly 4000 <= T <= 25000
            x_c = -3.0258469 * (1000000000 / kelvin.Pow3()) + 2.1070379 * (1000000 / kelvin.Pow2()) + 0.2226347 * (1000 / kelvin) + 0.240390;

        double y_c;

        if (kelvin <= 2222) // correctly 1667 <= T <= 2222
            y_c = -1.1063814 * x_c.Pow3() - 1.34811020 * x_c.Pow2() + 2.18555832 * x_c - 0.20219683;

        else if (kelvin <= 4000) // correctly 2222 <= T <= 4000
            y_c = -0.9549476 * x_c.Pow3() - 1.37418593 * x_c.Pow2() + 2.09137015 * x_c - 0.16748867;

        else // correctly 4000 <= T <= 25000
            y_c = +3.0817580 * x_c.Pow3() - 5.87338670 * x_c.Pow2() + 3.75112997 * x_c - 0.37001483;

        return IColor.New<XY>(x_c, y_c);
    }

    /// <summary>Gets CCT (in K) of given chromaticity (usually in range [0, 25000]).</summary>
    /// <remarks>http://en.wikipedia.org/wiki/Color_temperature#Approximation</remarks>
    public static double GetTemperature(in XY chromaticity)
    {
        const double xe = 0.3366;
        const double ye = 0.1735;
        const double A0 = -949.86315;
        const double A1 = 6253.80338;
        const double t1 = 0.92159;
        const double A2 = 28.70599;
        const double t2 = 0.20039;
        const double A3 = 0.00004;
        const double t3 = 0.07125;
        var n = (chromaticity.X - xe) / (chromaticity.Y - ye);
        var cct = A0 + A1 * Exp(-n / t1) + A2 * Exp(-n / t2) + A3 * Exp(-n / t3);
        return cct;
    }

    ///

    static readonly Dictionary<int, ByteVector3> Colors = new()
    {
        { 1000, new(255, 56, 0) },
        { 1100, new(255, 71, 0) },
        { 1200, new(255, 83, 0) },
        { 1300, new(255, 93, 0) },
        { 1400, new(255, 101, 0) },
        { 1500, new(255, 109, 0) },
        { 1600, new(255, 115, 0) },
        { 1700, new(255, 121, 0) },
        { 1800, new(255, 126, 0) },
        { 1900, new(255, 131, 0) },
        { 2000, new(255, 138, 18) },
        { 2100, new(255, 142, 33) },
        { 2200, new(255, 147, 44) },
        { 2300, new(255, 152, 54) },
        { 2400, new(255, 157, 63) },
        { 2500, new(255, 161, 72) },
        { 2600, new(255, 165, 79) },
        { 2700, new(255, 169, 87) },
        { 2800, new(255, 173, 94) },
        { 2900, new(255, 177, 101) },
        { 3000, new(255, 180, 107) },
        { 3100, new(255, 184, 114) },
        { 3200, new(255, 187, 120) },
        { 3300, new(255, 190, 126) },
        { 3400, new(255, 193, 132) },
        { 3500, new(255, 196, 137) },
        { 3600, new(255, 199, 143) },
        { 3700, new(255, 201, 148) },
        { 3800, new(255, 204, 153) },
        { 3900, new(255, 206, 159) },
        { 4000, new(255, 209, 163) },
        { 4100, new(255, 211, 168) },
        { 4200, new(255, 213, 173) },
        { 4300, new(255, 215, 177) },
        { 4400, new(255, 217, 182) },
        { 4500, new(255, 219, 186) },
        { 4600, new(255, 221, 190) },
        { 4700, new(255, 223, 194) },
        { 4800, new(255, 225, 198) },
        { 4900, new(255, 227, 202) },
        { 5000, new(255, 228, 206) },
        { 5100, new(255, 230, 210) },
        { 5200, new(255, 232, 213) },
        { 5300, new(255, 233, 217) },
        { 5400, new(255, 235, 220) },
        { 5500, new(255, 236, 224) },
        { 5600, new(255, 238, 227) },
        { 5700, new(255, 239, 230) },
        { 5800, new(255, 240, 233) },
        { 5900, new(255, 242, 236) },
        { 6000, new(255, 243, 239) },
        { 6100, new(255, 244, 242) },
        { 6200, new(255, 245, 245) },
        { 6300, new(255, 246, 247) },
        { 6400, new(255, 248, 251) },
        { 6500, new(255, 249, 253) },
        { 6600, new(254, 249, 255) },
        { 6700, new(252, 247, 255) },
        { 6800, new(249, 246, 255) },
        { 6900, new(247, 245, 255) },
        { 7000, new(245, 243, 255) },
        { 7100, new(243, 242, 255) },
        { 7200, new(240, 241, 255) },
        { 7300, new(239, 240, 255) },
        { 7400, new(237, 239, 255) },
        { 7500, new(235, 238, 255) },
        { 7600, new(233, 237, 255) },
        { 7700, new(231, 236, 255) },
        { 7800, new(230, 235, 255) },
        { 7900, new(228, 234, 255) },
        { 8000, new(227, 233, 255) },
        { 8100, new(225, 232, 255) },
        { 8200, new(224, 231, 255) },
        { 8300, new(222, 230, 255) },
        { 8400, new(221, 230, 255) },
        { 8500, new(220, 229, 255) },
        { 8600, new(218, 229, 255) },
        { 8700, new(217, 227, 255) },
        { 8800, new(216, 227, 255) },
        { 8900, new(215, 226, 255) },
        { 9000, new(214, 225, 255) },
        { 9100, new(212, 225, 255) },
        { 9200, new(211, 224, 255) },
        { 9300, new(210, 223, 255) },
        { 9400, new(209, 223, 255) },
        { 9500, new(208, 222, 255) },
        { 9600, new(207, 221, 255) },
        { 9700, new(207, 221, 255) },
        { 9800, new(206, 220, 255) },
        { 9900, new(205, 220, 255) },
        { 10000, new(207, 218, 255) },
        { 10100, new(207, 218, 255) },
        { 10200, new(206, 217, 255) },
        { 10300, new(205, 217, 255) },
        { 10400, new(204, 216, 255) },
        { 10500, new(204, 216, 255) },
        { 10600, new(203, 215, 255) },
        { 10700, new(202, 215, 255) },
        { 10800, new(202, 214, 255) },
        { 10900, new(201, 214, 255) },
        { 11000, new(200, 213, 255) },
        { 11100, new(200, 213, 255) },
        { 11200, new(199, 212, 255) },
        { 11300, new(198, 212, 255) },
        { 11400, new(198, 212, 255) },
        { 11500, new(197, 211, 255) },
        { 11600, new(197, 211, 255) },
        { 11700, new(197, 210, 255) },
        { 11800, new(196, 210, 255) },
        { 11900, new(195, 210, 255) },
        { 12000, new(195, 209, 255) }
    };

    public static RGB GetColor(in double kelvin)
    {
        int index = 0;
        double m = -1;

        foreach (var i in Colors)
        {
            var x = Abs(kelvin - i.Key);
            if (m == -1 || x < m)
            {
                index = i.Key;
                m = x;
            }
        }

        return new(Colors[index]);
    }
}