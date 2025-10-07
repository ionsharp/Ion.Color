using Ion.Collect;
using Ion.Core;
using Ion.Numeral;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ion.Colors;

public enum HistogramChannel
{
    Red, Green, Blue,
    Luminance, Saturation
}

/// <summary></summary>
[Description(Description)]
public record class Histogram() : Model()
{
    public const string Description = "";

    public ListObservable<Vector2> Red
    { get => Get<ListObservable<Vector2>>(); set => Set(value); }

    public ListObservable<Vector2> Green
    { get => Get<ListObservable<Vector2>>(); set => Set(value); }

    public ListObservable<Vector2> Blue
    { get => Get<ListObservable<Vector2>>(); set => Set(value); }

    public ListObservable<Vector2> Saturation
    { get => Get<ListObservable<Vector2>>(); set => Set(value); }

    public ListObservable<Vector2> Luminance
    { get => Get<ListObservable<Vector2>>(); set => Set(value); }

    private static int[] Smooth(int[] i)
    {
        double[] mask = [0.25, 0.5, 0.25];

        var result = new int[i.Length];
        for (var x = 1; x < i.Length - 1; x++)
        {
            var value = 0.0;
            for (int y = 0; y < mask.Length; y++)
                value += i[x - 1 + y] * mask[y];

            result[x] = (int)value;
        }
        return result;
    }

    public void Update(in ColorMatrix4 colors, ColorProfile profile, bool smooth = true)
    {
        ListObservable<Vector2> rX = [], gX = [], bX = [], sX = [], lX = [];
        IEnumerable<Vector2> rY = null, gY = null, bY = null, sY = null, lY = null;

        int[] r = new int[256], g = new int[256], b = new int[256];
        int[] s = new int[256], l = new int[256];

        RGB rgb = null;
        HSL hsl = null;

        colors.ForEach(color =>
        {
            r[color.X]++; g[color.Y]++; b[color.Z]++;

            rgb = new RGB(color.X, color.Y, color.Z);
            hsl = rgb.To<HSL>(profile);

            var m = Convert.ToInt32(hsl.Y / HSL.MaxValue.S * Convert.ToDouble(byte.MaxValue));
            var n = Convert.ToInt32(hsl.Z / HSL.MaxValue.L * Convert.ToDouble(byte.MaxValue));
            s[m]++; l[n]++;
        });

        rY = GetPoints(r, smooth); gY = GetPoints(g, smooth); bY = GetPoints(b, smooth);
        sY = GetPoints(s, smooth); lY = GetPoints(l, smooth);

        rY?.ForEach(rX.Add); gY?.ForEach(gX.Add); bY?.ForEach(bX.Add);
        sY?.ForEach(sX.Add); lY?.ForEach(lX.Add);

        Red = rX; Green = gX; Blue = bX;
        Saturation = sX; Luminance = lX;
    }

    private static IEnumerable<Vector2> GetPoints(int[] i, bool smooth = true)
    {
        i = smooth ? Smooth(i) : i;
        var maximum = i.Max();

        //First point (lower-left corner)
        yield return new Vector2(0, maximum);

        //Middle points
        for (int x = 0; x < i.Length; x++)
            yield return new Vector2(x, maximum - i[x]);

        //Last point (lower-right corner)
        yield return new Vector2(i.Length - 1, maximum);
    }

    public static IEnumerable<Vector2> GetPoints(HistogramChannel channel, in ColorMatrix4 colors, ColorProfile profile, bool smooth = true)
    {
        int[] result = new int[256];

        RGB rgb = null;
        HSL hsl = null;

        colors.ForEach(color =>
        {
            result[color.X]++;
            if (channel == HistogramChannel.Red)
                result[color.X]++;

            if (channel == HistogramChannel.Green)
                result[color.Y]++;

            if (channel == HistogramChannel.Blue)
                result[color.Z]++;

            if (channel == HistogramChannel.Luminance || channel == HistogramChannel.Saturation)
            {
                rgb = new RGB(color.X, color.Y, color.Z);
                hsl = rgb.To<HSL>(profile);
                if (channel == HistogramChannel.Luminance)
                    result[Convert.ToInt32(hsl.Z / HSL.MaxValue.L * Convert.ToDouble(byte.MaxValue))]++;

                if (channel == HistogramChannel.Saturation)
                    result[Convert.ToInt32(hsl.Y / HSL.MaxValue.S * Convert.ToDouble(byte.MaxValue))]++;
            }
        });
        return GetPoints(result, smooth);
    }
}