using System;

namespace Ion.Colors;

/// <summary>Group of related components shared by one or more colors.</summary>
/// <remarks><b>Calculation may vary between colors that share <see cref="ComponentGroup"/>.</b></remarks>
[Description("Group of related components shared by one or more colors.")]
[Flags]
public enum ComponentGroup
{
    [Hide]
    None = 0,
    /// <summary>An <see cref="IColor"/> with <b>Chroma</b> (A) and <b>Chroma</b> (B).</summary>
    [Description(ComponentGroupAttribute.DescriptionPrefix + "Red (R), Green (G), and Blue (B).")]
    AB = 1,
    /// <summary>An <see cref="IColor"/> with <b>Cyan</b> (C), <b>Magenta</b> (M), and <b>Yellow</b> (Y).</summary>
    [Description(ComponentGroupAttribute.DescriptionPrefix + "Cyan, Magenta, and Yellow.")]
    CMY = 2,
    /// <summary>An <see cref="IColor"/> with <b>Hue</b> (H).</summary>
    [Description(ComponentGroupAttribute.DescriptionPrefix + "Hue.")]
    H = 4,
    /// <summary>An <see cref="IColor"/> with <b>Hue</b> (H) and <b>Chroma</b> (C).</summary>
    [Description(ComponentGroupAttribute.DescriptionPrefix + "Hue and Chroma.")]
    HC = 8,
    /// <summary>An <see cref="IColor"/> with <b>Hue</b> (H) and <b>Saturation</b> (S).</summary>
    [Description(ComponentGroupAttribute.DescriptionPrefix + "Hue (H) and Saturation (S).")]
    HS = 16,
    /// <summary>An <see cref="IColor"/> with <b>Lightness</b> (L).</summary>
    [Description(ComponentGroupAttribute.DescriptionPrefix + "Lightness (L).")]
    Lightness = 32,
    /// <summary>An <see cref="IColor"/> with <b>Luminance</b> (L).</summary>
    [Description(ComponentGroupAttribute.DescriptionPrefix + "Luminance (L).")]
    Luminance = 64,
    /// <summary>An <see cref="IColor"/> with <b>Lightness</b> (L), <b>Chroma</b> (C), and <b>Hue</b> (H).</summary>
    [Description(ComponentGroupAttribute.DescriptionPrefix + "Lightness (L), Chroma (C), and Hue (H).")]
    LCH = 128,
    /// <summary>An <see cref="IColor"/> with <b>Red</b> (R) and <b>Blue</b> (B).</summary>
    [Description(ComponentGroupAttribute.DescriptionPrefix + "Red (R) and Blue (B).")]
    RB = 256,
    /// <summary>An <see cref="IColor"/> with <b>Red</b> (R), <b>Green</b> (G), and <b>Blue</b> (B).</summary>
    [Description(ComponentGroupAttribute.DescriptionPrefix + "Red (R), Green (G), and Blue (B).")]
    RGB = 512,
    /// <summary>An <see cref="IColor"/> with <b>Saturation</b> (S) and <b>Brightness</b> (B).</summary>
    [Description(ComponentGroupAttribute.DescriptionPrefix + "Saturation (S) and Brightness (L).")]
    SB = 1024,
    /// <summary>An <see cref="IColor"/> with <b>Saturation</b> (S) and <b>Lightness</b> (L).</summary>
    [Description(ComponentGroupAttribute.DescriptionPrefix + "Saturation (S) and Lightness (L).")]
    SL = 2048,
    /// <summary>An <see cref="IColor"/> with <b>Whiteness</b> (W) and <b>Blackness</b> (B).</summary>
    [Description(ComponentGroupAttribute.DescriptionPrefix + "Whiteness (W) and Blackness (B).")]
    WB = 4096,
    [Hide]
    All = AB | CMY | H | HC | HS | LCH | Lightness | Luminance | RB | RGB | SB | SL | WB
}