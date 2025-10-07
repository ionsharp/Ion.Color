using System.Collections.Generic;

namespace Ion.Colors;

public static partial class ColorPreset
{
    /// <summary>https://en.wikipedia.org/wiki/Web_colors#Web-safe_colors</summary>
    public static class WebSafe
    {
        public static readonly List<string> Colors = [];

        static WebSafe()
        {
            var key = new string[] { "0", "3", "6", "9", "C", "F" };

            for (var x = 0; x < 6; x++)
            {
                for (var y = 0; y < 6; y++)
                {
                    for (var z = 0; z < 6; z++)
                        Colors.Add($"{key[x]}{key[y]}{key[z]}");
                }
            }
        }
    }
}