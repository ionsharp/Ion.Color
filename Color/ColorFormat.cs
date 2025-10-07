namespace Ion.Colors;

//// <summary>A format for colors displayed on pixel-based surfaces.</summary>
public enum ColorFormat
{
    /// <summary>
    /// The format that is best suited for the particular operation.
    /// </summary>
    Default,
    /// <summary>
    /// 1 bit of data per pixel as either black or white.
    /// </summary>
    BlackWhite,
    /// <summary>
    /// An <see cref="ColorProfiles.sRGB"/> format with 32 bits per pixel. 
    /// Each color channel (blue, green, and red) is allocated 10 bits per pixel.
    /// </summary>
    Bgr101010,
    ///
    /// <summary>
    /// An <see cref="ColorProfiles.sRGB"/> format with 24 bits per pixel. 
    /// Each color channel (blue, green, and red) is allocated 8 bits per pixel.
    /// </summary>
    Bgr24,
    /// <summary>
    /// An <see cref="ColorProfiles.sRGB"/> format with 32 bits per pixel. 
    /// Each color channel (blue, green, and red) is allocated 8 bits per pixel.
    /// </summary>
    Bgr32,
    /// <summary>
    /// An <see cref="ColorProfiles.sRGB"/> format with 16 bits per pixel. 
    /// Each color channel (blue, green, and red) is allocated 5 bits per pixel.
    /// </summary>
    Bgr555,
    /// <summary>
    /// An <see cref="ColorProfiles.sRGB"/> format with 16 bits per pixel. 
    /// Each color channel (blue, green, and red) is allocated 5, 6, and 5 bits per pixel respectively.
    /// </summary>
    Bgr565,
    /// <summary>
    /// An <see cref="ColorProfiles.sRGB"/> format with 32 bits per pixel. 
    /// Each channel (blue, green, red, and alpha) is allocated 8 bits per pixel.
    /// </summary>
    Bgra32,
    /// <summary>
    /// 32 bits per pixel with each color channel (cyan, magenta, yellow, and black) allocated 8 bits per pixel.
    /// </summary>
    Cmyk32,
    /// <summary>
    /// A 2 bits per pixel grayscale channel, allowing 4 shades of gray.
    /// </summary>
    Gray2,
    /// <summary>
    /// A 4 bits per pixel grayscale channel, allowing 16 shades of gray.
    /// </summary>
    Gray4,
    /// <summary>
    /// An 8 bits per pixel grayscale channel, allowing 256 shades of gray.
    /// </summary>
    Gray8,
    /// <summary>
    /// A 16 bits per pixel grayscale channel, allowing 65536 shades of gray. 
    /// This format has a gamma of 1.0.
    /// </summary>
    Gray16,
    /// <summary>
    /// A 32 bits per pixel grayscale channel, allowing over 4 billion shades of gray. 
    /// This format has a gamma of 1.0.
    /// </summary>
    Gray32Float,
    /// <summary>
    /// A paletted bitmap with 2 colors.</summary>
    Indexed1,
    /// <summary>
    /// A paletted bitmap with 4 colors.
    /// </summary>
    Indexed2,
    /// <summary>
    /// A paletted bitmap with 16 colors.
    /// </summary>
    Indexed4,
    /// <summary>
    /// A paletted bitmap with 256 colors.
    /// </summary>
    Indexed8,
    /// <summary>
    /// An <see cref="ColorProfiles.sRGB"/> format with 24 bits per pixel. 
    /// Each color channel (red, green, and blue) is allocated 8 bits per pixel.
    /// </summary>
    Rgb24,
    /// <summary>
    /// An <see cref="ColorProfiles.sRGB"/> format with 48 bits per pixel. 
    /// Each color channel (red, green, and blue) is allocated 16 bits per pixel. 
    /// This format has a gamma of 1.0.
    /// </summary>
    Rgb48,
    /// <summary>
    /// An <see cref="ColorProfiles.scRGB"/> format with 128 bits per pixel. 
    /// Each color channel is allocated 32 BPP. This format has a gamma of 1.0.
    /// </summary>
    Rgb128Float,
    /// <summary>
    /// An <see cref="ColorProfiles.sRGB"/> format with 64 bits per pixel. 
    /// Each channel (red, green, blue, and alpha) is allocated 16 bits per pixel. 
    /// This format has a gamma of 1.0.
    /// </summary>
    Rgba64,
    /// <summary>
    /// An <see cref="ColorProfiles.scRGB"/> format with 128 bits per pixel. 
    /// Each color channel is allocated 32 bits per pixel. This format has a gamma of 1.0.
    /// </summary>
    Rgba128Float,
    /// <summary>
    /// An <see cref="ColorProfiles.sRGB"/> format with 32 bits per pixel. 
    /// Each channel (blue, green, red, and alpha) is allocated 8 bits per pixel. 
    /// Each color channel is pre-multiplied by the alpha value.
    /// </summary>
    Pbgra32,
    /// <summary>
    /// An <see cref="ColorProfiles.sRGB"/> format with 64 bits per pixel. 
    /// Each channel (blue, green, red, and alpha) is allocated 32 bits per pixel. 
    /// Each color channel is pre-multiplied by the alpha value. This format has a gamma of 1.0.
    /// </summary>
    Prgba64,
    /// <summary>
    /// An <see cref="ColorProfiles.scRGB"/> format with 128 bits per pixel. 
    /// Each channel (red, green, blue, and alpha) is allocated 32 bits per pixel. 
    /// Each color channel is pre-multiplied by the alpha value. This format has a gamma of 1.0.
    /// </summary>
    Prgba128Float,
}