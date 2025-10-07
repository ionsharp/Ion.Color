using Ion.Numeral;

namespace Ion.Colors;

/// <summary>Specifies an <see cref="Illuminant"/> measured with <see cref="ColorObservers.D10"/>.</summary>
public static class Illuminant10
{
    /// <summary>Incandescent / tungsten (<see cref="Illuminant">2856</see> K).</summary>
    [Description("Incandescent / tungsten")]
    public static Vector2 A => new(0.45117, 0.40594);
    /// <summary>Direct sunlight at noon (<see cref="Illuminant">4874</see> K).</summary>
    [Description("Direct sunlight at noon")]
    public static Vector2 B => new(0.34980, 0.35270);
    /// <summary>Average / North sky daylight (<see cref="Illuminant">6774</see> K).</summary>
    [Description("Average / North sky daylight")]
    public static Vector2 C => new(0.31039, 0.31905);

    ///(Daylight)

    /// <summary> Horizon light (<see cref="Illuminant">5003</see> K).</summary>
    [Description("Horizon light")]
    public static Vector2 D50 => new(0.34773, 0.35952);
    /// <summary> Mid-morning / Mid-afternoon daylight (<see cref="Illuminant">5503</see> K).</summary>
    [Description("Mid-morning / Mid-afternoon daylight")]
    public static Vector2 D55 => new(0.33411, 0.34877);
    /// <summary> Noon daylight (television/sRGB color space) (<see cref="Illuminant">6504</see> K).</summary>
    [Description("Noon daylight (television/sRGB color space)")]
    public static Vector2 D65 => new(0.31382, 0.33100);
    /// <summary> North sky daylight (<see cref="Illuminant">7504</see> K).</summary>
    [Description("North sky daylight")]
    public static Vector2 D75 => new(0.29968, 0.31740);
    /// <summary> High-efficiency blue phosphor monitors (BT.2035) (<see cref="Illuminant">9305</see> K).</summary>
    [Description("High-efficiency blue phosphor monitors (BT.2035)")]
    public static Vector2 D93 => new(0.28327, 0.30043);

    ///(Fluorescent)

    /// <summary> Daylight fluorescent (<see cref="Illuminant">6430</see> K).</summary>
    [Description("Daylight fluorescent")]
    public static Vector2 F1 => new(0.31811, 0.33559);
    /// <summary> Cool white fluorescent (<see cref="Illuminant">4230</see> K).</summary>
    [Description("Cool white fluorescent")]
    public static Vector2 F2 => new(0.37925, 0.36733);
    /// <summary> White fluorescent (<see cref="Illuminant">3450</see> K).</summary>
    [Description("White fluorescent")]
    public static Vector2 F3 => new(0.41761, 0.38324);
    /// <summary> Warm white fluorescent (<see cref="Illuminant">2940</see> K).</summary>
    [Description("Warm white fluorescent")]
    public static Vector2 F4 => new(0.44920, 0.39074);
    /// <summary> Daylight fluorescent (<see cref="Illuminant">6350</see> K).</summary>
    [Description("Daylight fluorescent")]
    public static Vector2 F5 => new(0.31975, 0.34246);
    /// <summary> Light white fluorescent (<see cref="Illuminant">4150</see> K).</summary>
    [Description("Light white fluorescent")]
    public static Vector2 F6 => new(0.38660, 0.37847);
    /// <summary>D65 simulator (daylight simulator) (<see cref="Illuminant">6500</see> K).</summary>
    [Description("D65 simulator (daylight simulator)")]
    public static Vector2 F7 => new(0.31569, 0.32960);
    /// <summary>D50 simulator (Sylvania F40 Design 50) (<see cref="Illuminant">5000</see> K).</summary>
    [Description("D50 simulator (Sylvania F40 Design 50)")]
    public static Vector2 F8 => new(0.34902, 0.35939);
    /// <summary>Cool white deluxe fluorescent (<see cref="Illuminant">4150</see> K).</summary>
    [Description("Cool white deluxe fluorescent")]
    public static Vector2 F9 => new(0.37829, 0.37045);
    /// <summary>Philips TL85, Ultralume 50 (<see cref="Illuminant">5000</see> K).</summary>
    [Description("Philips TL85, Ultralume 50")]
    public static Vector2 F10 => new(0.35090, 0.35444);
    /// <summary>Philips TL84, Ultralume 40 (<see cref="Illuminant">4000</see> K).</summary>
    [Description("Philips TL84, Ultralume 40")]
    public static Vector2 F11 => new(0.38541, 0.37123);
    /// <summary>Philips TL83, Ultralume 30 (<see cref="Illuminant">3000</see> K).</summary>
    [Description("Philips TL83, Ultralume 30")]
    public static Vector2 F12 => new(0.44256, 0.39717);
}