using Ion;
using Ion.Core;
using Ion.Numeral;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ion.Colors;

/// <summary>A set of position-dependent colors.</summary>
[Description(Description)]
public record class Gradient() : Model(), IReset
{
    public const string Description = "A set of position-dependent colors.";

    /// <see cref="Region.Property"/>

    public static Gradient Default => new(new GradientStep(0, ByteVector4.White), new GradientStep(1, ByteVector4.Black));

    public static Gradient Rainbow => new
    (
        new GradientStep(0.000, new ByteVector4(255, 0, 0)),
        new GradientStep(0.166, new ByteVector4(255, 255, 0)),
        new GradientStep(0.332, new ByteVector4(0, 255, 0)),
        new GradientStep(0.500, new ByteVector4(0, 255, 255)),
        new GradientStep(0.666, new ByteVector4(0, 0, 255)),
        new GradientStep(0.832, new ByteVector4(255, 0, 255)),
        new GradientStep(1.000, new ByteVector4(255, 0, 0))
    );

    ///

    public static Line<double> Horizontal => new(0, 0.5, 1, 0.5);

    public static Line<double> Vertical => new(0.5, 0, 0.5, 1);

    public GradientStepCollection Steps { get => Get(new GradientStepCollection()); private set => Set(value); }

    /// <see cref="Region.Constructor"/>

    public Gradient(params GradientStep[] i) : this()
        => i?.ForEach(Steps.Add);

    public Gradient(GradientStepCollection i) : this()
        => i?.ForEach(j => Steps.Add(new GradientStep(j.Offset, j.Color)));

    /// <see cref="Region.Method"/>

    public void CopyFrom(Gradient input)
    {
        Steps.Clear();
        input.Steps.ForEach(i => Steps.Add(new GradientStep(i.Offset, i.Color)));
    }

    public virtual void Reset() => CopyFrom(Default);

    /// <see cref="Region.Method.Static"/>

    public static string GetName(Gradient gradient)
    {
        var name = "Untitled";
        foreach (var i in gradient.Steps)
        {
            if (name == "Untitled")
                name = "";

            var color = i.Color.GetName();

            if (i.Color.A == 0)
                name += $"{nameof(System.Drawing.Color.Transparent)}";

            else if (color != null)
                name += $"{color}";

            else if (color is null)
                name += i.Color.GetName();

            name += ", ";
        }

        if (name.EndsWith(", "))
            name = name[..^2];

        return name;
    }

    public static IEnumerable<Gradient> New(Type type)
    {
        ArgumentNullException.ThrowIfNull(type);

        var colors = Enumerable.Select(type.GetFields(), i => (ByteVector4)i.GetValue(null)).ToArray();
        foreach (var i in New(colors))
            yield return i;

        foreach (var i in colors)
        {
            foreach (var j in New([i, .. ByteVector4.Neutral], i))
                yield return j;
        }
    }

    public static IEnumerable<Gradient> New(IList<ByteVector4> colors, ByteVector4? require = null, bool reverse = true)
    {
        for (int i = 0, n = colors.Count; i < n; i++)
        {
            var a = colors[i];
            for (int j = i + 1; j < n; j++)
            {
                var b = colors[j];
                if (require is null || require == a || require == b)
                {
                    yield return new Gradient(new GradientStep(0, a), new GradientStep(1, b));
                    if (reverse)
                        yield return new Gradient(new GradientStep(0, b), new GradientStep(1, a));
                }
                for (int k = j + 1; k < n; k++)
                {
                    var c = colors[k];
                    if (require is null || require == a || require == b || require == c)
                    {
                        yield return new Gradient(new GradientStep(0, a), new GradientStep(0.5, b), new(1, c));
                        yield return new Gradient(new GradientStep(0, a), new GradientStep(0.5, c), new(1, b));
                        yield return new Gradient(new GradientStep(0, b), new GradientStep(0.5, a), new(1, c));
                        if (reverse)
                        {
                            yield return new Gradient(new GradientStep(0, c), new GradientStep(0.5, b), new(1, a));
                            yield return new Gradient(new GradientStep(0, b), new GradientStep(0.5, c), new(1, a));
                            yield return new Gradient(new GradientStep(0, c), new GradientStep(0.5, a), new(1, b));
                        }
                    }
                }
            }
        }
    }
}