using System.Xml;
using System.Xml.Serialization;
using ClientApplication.Graphics.UI;
using GraphicsFundation.Graphics.UI.Helpers;
using SFML.Graphics;
using SFML.System;
using Color = SFML.Graphics.Color;
using static GraphicsFundation.Graphics.UI.Helpers.ColorConverter;

namespace GraphicsFundation.Graphics.UI.Primitives
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
            Origin = new Vector2f(0, 0);
        }
        public UI_Image(Texture texture, IntRect rect) : this()
        {
            Sprite = new Sprite(texture, rect);
        }
        public UI_Image(Sprite sprite) : this()
        {
            Sprite = sprite;
        }

        public override void Draw(RenderTarget target, RenderStates states)
        {
            if (Sprite != null)
            {

                target.Draw(Sprite, states);
            }
            else
            {
                rectangle.Position = Position;
                rectangle.Size = Size;
                rectangle.FillColor = Color;
                rectangle.Origin = new Vector2f(
                    Origin.X * Size.X,
                    Origin.Y * Size.Y);
                target.Draw(rectangle, states);
            }
        }

        public void Dispose()
        {
            Sprite?.Dispose();
        }

        public override XmlElement Serialize(XmlDocument document, XmlElement parent)
        {
            var node = base.Serialize(document, parent);

            var data = new Data()
            {
                Color = Color2String(Color)
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
                Color = String2Color(data.Color);
        }


        private class Data
        {
            [XmlAttribute(AttributeName = "color")]
            public string? Color;
        }
    }
}
