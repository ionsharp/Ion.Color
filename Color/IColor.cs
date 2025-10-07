using Ion;
using Ion.Numeral;
using Ion.Reflect;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ion.Colors;

/// <summary>A set of values that represent a color.</summary>
/// <remarks><b>https://en.wikipedia.org/wiki/Color_model</b></remarks>
public interface IColor : IVectorMutable
{
    new public const string Description = "A set of values that represent a color.";

    /// <summary>The namespace where types implementing <see cref="IColor"/> are defined.</summary>
    public const string Namespace = $"{nameof(Ion)}.{nameof(Colors)}";

    /// <see cref="Region.Method"/>

    /// <summary><see cref="Lrgb"/> ⇒ <see langword="this"/></summary>
    public void From(in Lrgb color, ColorProfile profile);

    /// <summary><see cref="RGB"/> ⇒ <see langword="this"/></summary>
    public void From(in RGB color, ColorProfile profile);

    /// <summary><see langword="this"/> ⇒ <see cref="Lrgb"/></summary>
    public Lrgb To(ColorProfile profile);

    /// <summary><see langword="this"/> ⇒ <see cref="Lrgb"/></summary>
    public void To(out Lrgb color, ColorProfile profile);

    /// <summary><see langword="this"/> ⇒ <see cref="RGB"/></summary>
    public void To(out RGB color, ColorProfile profile);

    /// <summary><see langword="this"/> ⇒ <see cref="IColor"/></summary>
    public T To<T>(ColorProfile profile = default) where T : IColor, new();

    /// <summary><see langword="this"/> ⇒ <see cref="IColor"/></summary>
    public IColor To(Type model, ColorProfile profile = default);

    new public double[] ToArray();

    /// <see cref="Region.Method.Static"/>

    private static Dictionary<Type, Vector<Component>> components;
    public static Dictionary<Type, Vector<Component>> Components
    {
        get
        {
            if (components is null)
            {
                components = [];
                Types.ForEach(type => Components.Add(type, new Vector<Component>(type.GetAttributes<ComponentAttribute>()?.Select(i => i.Info).ToArray())));
            }
            return components;
        }
    }

    private readonly static IEnumerable<Type> Types = XAssembly.GetTypes(Namespace, i => !i.IsAbstract && i.Implements<IColor>() && !Instance.IsHidden(i));
    public static IEnumerable<Type> GetTypes() => Types;

    ///

    public static Vector Maximum(Type i) => new(Components[i].NewType((x, y) => y.Maximum));

    public static Vector Minimum(Type i) => new(Components[i].NewType((x, y) => y.Minimum));

    ///

    public static IColor New(in Type i, params double[] values)
    {
        var result = i.Create<IColor>();

        if (result is IColor2 a)
        { a.X = values[0]; a.Y = values[1]; }

        if (result is IColor3 b)
            b.Z = values[2];

        if (result is IColor4 c)
            c.W = values[3];

        return result;
    }

    public static T New<T>(double all) where T : IColor, new()
    {
        T result = [];

        if (result is IColor2 a)
        { a.X = all; a.Y = all; }

        if (result is IColor3 b)
            b.Z = all;

        if (result is IColor4 c)
            c.W = all;

        return result;
    }

    public static T New<T>(double x, double y) where T : IColor2, new()
        => New<T>(new Vector2(x, y));

    public static T New<T>(double x, double y, double z) where T : IColor3, new()
        => New<T>(new Vector3(x, y, z));

    public static T New<T>(double x, double y, double z, double w) where T : IColor4, new()
        => New<T>(new Vector4(x, y, z, w));

    public static T New<T>(in IVector2<double> input) where T : IColor2, new()
    { T result = []; result.X = input.X; result.Y = input.Y; return result; }

    public static T New<T>(in IVector3<double> input) where T : IColor3, new()
    { T result = []; result.X = input.X; result.Y = input.Y; result.Z = input.Z; return result; }

    public static T New<T>(in IVector4<double> input) where T : IColor4, new()
    { T result = []; result.X = input.X; result.Y = input.Y; result.Z = input.Z; result.W = input.W; return result; }
}