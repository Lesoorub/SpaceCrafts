using Color = SFML.Graphics.Color;

namespace GraphicsFundation.Graphics.UI.Helpers
{
    public static class ColorConverter
    {
        public static string Color2String(Color c)
        {
            if (KnowedColors.ContainsValue(c))
                return KnowedColors.First(x => x.Value == c).Key;
            return $"#{c.R:X2}{c.G:X2}{c.B:X2}";
        }
        public static Color String2Color(string hex)
        {
            if (KnowedColors.TryGetValue(hex, out var color))
                return color;

            if (hex.Length != 7 || hex[0] != '#')
                return Color.Black;

            byte r = byte.Parse(hex.Substring(1, 2), System.Globalization.NumberStyles.HexNumber);
            byte g = byte.Parse(hex.Substring(3, 2), System.Globalization.NumberStyles.HexNumber);
            byte b = byte.Parse(hex.Substring(5, 2), System.Globalization.NumberStyles.HexNumber);

            return new Color(r, g, b);
        }
        static readonly Dictionary<string, Color> KnowedColors = new Dictionary<string, Color>()
        {
            { "black", Color.Black },
            { "white", Color.White },
            { "red", Color.Red },
            { "green", Color.Green },
            { "blue", Color.Blue },
            { "yellow", Color.Yellow },
            { "cyan", Color.Cyan },
            { "magenta", Color.Magenta },
            { "transparent", Color.Transparent },
        };
    }
}
