using Ion.Numeral;

namespace Ion.Colors;

/// <summary>Specifies an <see cref="Illuminant"/> measured with <see cref="ColorObservers.D2"/>.</summary>
public static class Illuminant2
{
    /// <summary>Incandescent / tungsten (<see cref="Illuminant">2856</see> K).</summary>
    /// <remarks><see cref="ColorObservers.D2"/></remarks>
    [Description("Incandescent / tungsten")]
    public static Vector2 A => new(0.44757, 0.40745);
    /// <summary>Direct sunlight at noon (<see cref="Illuminant">4874</see> K).</summary>
    /// <remarks><see cref="ColorObservers.D2"/></remarks>
    [Description("Direct sunlight at noon")]
    public static Vector2 B => new(0.34842, 0.35161);
    /// <summary>Average / North sky daylight (<see cref="Illuminant">6774</see> K).</summary>
    /// <remarks><see cref="ColorObservers.D2"/></remarks>
    [Description("Average / North sky daylight")]
    public static Vector2 C => new(0.31006, 0.31616);

    ///(Daylight)

    /// <summary> Horizon light (<see cref="Illuminant">5003</see> K).</summary>
    /// <remarks><see cref="ColorObservers.D2"/></remarks>
    [Description("Horizon light")]
    public static Vector2 D50 => new(0.34567, 0.35850);
    /// <summary> Mid-morning / Mid-afternoon daylight (<see cref="Illuminant">5503</see> K).</summary>
    /// <remarks><see cref="ColorObservers.D2"/></remarks>
    [Description("Mid-morning / Mid-afternoon daylight")]
    public static Vector2 D55 => new(0.33242, 0.34743);
    /// <summary> ~<see cref="Illuminant">6000</see> K.</summary>
    /// <remarks><see cref="ColorObservers.D2"/></remarks>
    [Description("~6000 K")]
    public static Vector2 D60 => new(0.32168, 0.33767);
    /// <summary> ~<see cref="Illuminant">6300</see> K.</summary>
    /// <remarks><see cref="ColorObservers.D2"/></remarks>
    [Description("~6300 K")]
    public static Vector2 D63 => new(0.31400, 0.35100);
    /// <summary> Noon daylight (television/sRGB color space) (<see cref="Illuminant">6504</see> K).</summary>
    /// <remarks><see cref="ColorObservers.D2"/></remarks>
    [Description("Noon daylight (television/sRGB color space)")]
    public static Vector2 D65 => new(0.31271, 0.32902);
    /// <summary> North sky daylight (<see cref="Illuminant">7504</see> K).</summary>
    /// <remarks><see cref="ColorObservers.D2"/></remarks>
    [Description("North sky daylight")]
    public static Vector2 D75 => new(0.29902, 0.31485);
    /// <summary> High-efficiency blue phosphor monitors (BT.2035) (<see cref="Illuminant">9305</see> K).</summary>
    /// <remarks><see cref="ColorObservers.D2"/></remarks>
    [Description("High-efficiency blue phosphor monitors (BT.2035)")]
    public static Vector2 D93 => new(0.28315, 0.29711);

    ///(Fluorescent)

    /// <summary> Daylight fluorescent (<see cref="Illuminant">6430</see> K).</summary>
    /// <remarks><see cref="ColorObservers.D2"/></remarks>
    [Description("Daylight fluorescent")]
    public static Vector2 F1 => new(0.31310, 0.33727);
    /// <summary> Cool white fluorescent (<see cref="Illuminant">4230</see> K).</summary>
    /// <remarks><see cref="ColorObservers.D2"/></remarks>
    [Description("Cool white fluorescent")]
    public static Vector2 F2 => new(0.37208, 0.37529);
    /// <summary> White fluorescent (<see cref="Illuminant">3450</see> K).</summary>
    /// <remarks><see cref="ColorObservers.D2"/></remarks>
    [Description("White fluorescent")]
    public static Vector2 F3 => new(0.40910, 0.39430);
    /// <summary> Warm white fluorescent (<see cref="Illuminant">2940</see> K).</summary>
    /// <remarks><see cref="ColorObservers.D2"/></remarks>
    [Description("Warm white fluorescent")]
    public static Vector2 F4 => new(0.44018, 0.40329);
    /// <summary> Daylight fluorescent (<see cref="Illuminant">6350</see> K).</summary>
    /// <remarks><see cref="ColorObservers.D2"/></remarks>
    [Description("Daylight fluorescent")]
    public static Vector2 F5 => new(0.31379, 0.34531);
    /// <summary> Light white fluorescent (<see cref="Illuminant">4150</see> K).</summary>
    /// <remarks><see cref="ColorObservers.D2"/></remarks>
    [Description("Light white fluorescent")]
    public static Vector2 F6 => new(0.37790, 0.38835);
    /// <summary>D65 simulator (daylight simulator) (<see cref="Illuminant">6500</see> K).</summary>
    /// <remarks><see cref="ColorObservers.D2"/></remarks>
    [Description("D65 simulator (daylight simulator)")]
    public static Vector2 F7 => new(0.31292, 0.32933);
    /// <summary>D50 simulator (Sylvania F40 Design 50) (<see cref="Illuminant">5000</see> K).</summary>
    /// <remarks><see cref="ColorObservers.D2"/></remarks>
    [Description("D50 simulator (Sylvania F40 Design 50)")]
    public static Vector2 F8 => new(0.34588, 0.35875);
    /// <summary>Cool white deluxe fluorescent (<see cref="Illuminant">4150</see> K).</summary>
    /// <remarks><see cref="ColorObservers.D2"/></remarks>
    [Description("Cool white deluxe fluorescent")]
    public static Vector2 F9 => new(0.37417, 0.37281);
    /// <summary>Philips TL85, Ultralume 50 (<see cref="Illuminant">5000</see> K).</summary>
    /// <remarks><see cref="ColorObservers.D2"/></remarks>
    [Description("Philips TL85, Ultralume 50")]
    public static Vector2 F10 => new(0.34609, 0.35986);
    /// <summary>Philips TL84, Ultralume 40 (<see cref="Illuminant">4000</see> K).</summary>
    /// <remarks><see cref="ColorObservers.D2"/></remarks>
    [Description("Philips TL84, Ultralume 40")]
    public static Vector2 F11 => new(0.38052, 0.37713);
    /// <summary>Philips TL83, Ultralume 30 (<see cref="Illuminant">3000</see> K).</summary>
    /// <remarks><see cref="ColorObservers.D2"/></remarks>
    [Description("Philips TL83, Ultralume 30")]
    public static Vector2 F12 => new(0.43695, 0.40441);

    ///(LED-B*)

    /// <summary>Phosphor-converted blue (<see cref="Illuminant">2733</see> K).</summary>
    /// <remarks><see cref="ColorObservers.D2"/></remarks>
    [Description("Phosphor-converted blue")]
    public static Vector2 LED_B1 => new(0.4560, 0.4078);
    /// <summary>Phosphor-converted blue (<see cref="Illuminant">2998</see> K).</summary>
    /// <remarks><see cref="ColorObservers.D2"/></remarks>
    [Description("Phosphor-converted blue")]
    public static Vector2 LED_B2 => new(0.4357, 0.4012);
    /// <summary>Phosphor-converted blue (<see cref="Illuminant">4103</see> K).</summary>
    /// <remarks><see cref="ColorObservers.D2"/></remarks>
    [Description("Phosphor-converted blue")]
    public static Vector2 LED_B3 => new(0.3756, 0.3723);
    /// <summary>Phosphor-converted blue (<see cref="Illuminant">5109</see> K).</summary>
    /// <remarks><see cref="ColorObservers.D2"/></remarks>
    [Description("Phosphor-converted blue")]
    public static Vector2 LED_B4 => new(0.3422, 0.3502);
    /// <summary>Phosphor-converted blue (<see cref="Illuminant">6598</see> K).</summary>
    /// <remarks><see cref="ColorObservers.D2"/></remarks>
    [Description("Phosphor-converted blue")]
    public static Vector2 LED_B5 => new(0.3118, 0.3236);

    ///(LED-BH*)

    /// <summary>Mixing of phosphor-converted blue LED and red LED (blue-hybrid) (<see cref="Illuminant">2851</see> K).</summary>
    /// <remarks><see cref="ColorObservers.D2"/></remarks>
    [Description("Mixing of phosphor-converted blue LED and red LED (blue-hybrid)")]
    public static Vector2 LED_BH1 => new(0.4474, 0.4066);

    ///(LED-RGB*)

    /// <summary>Mixing of red, green, and blue LEDs (<see cref="Illuminant">2840</see> K).</summary>
    /// <remarks><see cref="ColorObservers.D2"/></remarks>
    [Description("Mixing of red, green, and blue LEDs")]
    public static Vector2 LED_RGB => new(0.4557, 0.4211);

    ///(LED-V*)

    /// <summary>Phosphor-converted violet (<see cref="Illuminant">2724</see> K).</summary>
    /// <remarks><see cref="ColorObservers.D2"/></remarks>
    [Description("Phosphor-converted violet")]
    public static Vector2 LED_V1 => new(0.4560, 0.4548);
    /// <summary>Phosphor-converted violet (<see cref="Illuminant">4070</see> K).</summary>
    /// <remarks><see cref="ColorObservers.D2"/></remarks>
    [Description("Phosphor-converted violet")]
    public static Vector2 LED_V2 => new(0.3781, 0.3775);
}
