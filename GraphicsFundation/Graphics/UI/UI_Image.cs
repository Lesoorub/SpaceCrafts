using System.Drawing;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using Newtonsoft.Json;
using SFML.Graphics;
using SFML.System;
using Color = SFML.Graphics.Color;

namespace ClientApplication.Graphics.UI
{
    [UIXmlName("img")]
    public class UI_Image : UIElement, IDisposable
    {
        public override string NodeName => "img";

        public Sprite? Sprite;
        static RectangleShape rectangle = new RectangleShape();
        public Color Color = Color.White;

        public UI_Image()
        {
            this.Origin = new Vector2f(0, 0);
        }
        public UI_Image(Texture texture, IntRect rect) : this()
        {
            this.Sprite = new Sprite(texture, rect);
        }
        public UI_Image(Sprite sprite) : this()
        {
            this.Sprite = sprite;
        }

        public override void Draw(RenderTarget target, RenderStates states)
        {
            if (this.Sprite != null)
            {

                target.Draw(this.Sprite, states);
            }
            else
            {
                rectangle.Position = this.Position;
                rectangle.Size = this.Size;
                rectangle.FillColor = this.Color;
                rectangle.Origin = new Vector2f(
                    this.Origin.X * this.Size.X,
                    this.Origin.Y * this.Size.Y);
                target.Draw(rectangle, states);
            }
        }

        public void Dispose()
        {
            this.Sprite?.Dispose();
        }

        public override XmlElement Serialize(XmlDocument document, XmlElement parent)
        {
            var node = base.Serialize(document, parent);

            var data = new Data()
            {
                Color = Color2String(this.Color)
            };
            node.WriteObject(data);
            return node;
        }

        public override void Deserialize(XmlElement node)
        {
            base.Deserialize(node);
            var data = node.ConvertNode<Data>();
            if (data == null) return;

            if (data.Color != null)
                this.Color = String2Color(data.Color);
        }

        static string Color2String(Color c)
        {
            if (KnowedColors.ContainsValue(c))
                return KnowedColors.First(x => x.Value == c).Key;
            return $"#{c.R:X2}{c.G:X2}{c.B:X2}";
        }
        static Color String2Color(string hex)
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

        private class Data
        {
            [XmlAttribute(AttributeName = "color")]
            public string? Color;
        }
    }
}
