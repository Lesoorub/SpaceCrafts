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
        public Color Color = Color.White;
        public string? TexturePath;
        public IntRect? SpriteRect;
        static RectangleShape rectangle = new RectangleShape();
        public float Angle;

        public UI_Image()
        {
            this.Origin = new Vector2f(0, 0);
        }
        public UI_Image(string texturePath, IntRect rect) : this()
        {
            this.TexturePath = texturePath;
            this.SpriteRect = rect;
            this.Sprite = AssetsLoader.LoadSprite(texturePath, rect);
        }
        public UI_Image(Sprite sprite) : this()
        {
            this.Sprite = sprite;
        }

        public override void Draw(RenderTarget target, RenderStates states)
        {
            if (this.Sprite != null)
            {
                this.Sprite.Position = this.Position;
                if (this.SpriteRect != null)
                {
                    this.Sprite.Scale = new Vector2f(
                        this.Size.X / this.SpriteRect.Value.Width,
                        this.Size.Y / this.SpriteRect.Value.Height);
                }
                this.Sprite.Rotation = this.Angle;
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
                rectangle.Rotation = this.Angle;
                target.Draw(rectangle, states);
            }
        }

        public void Dispose()
        {
            this. Sprite?.Dispose();
        }

        public override XmlElement Serialize(XmlDocument document, XmlElement parent)
        {
            var node = base.Serialize(document, parent);

            var data = new Data()
            {
                Color = Color2String(this.Color),
                Texture = this.TexturePath,
                Rect = this.SpriteRect.HasValue ? 
                    $"{this.SpriteRect.Value.Left};" +
                    $"{this.SpriteRect.Value.Top};" +
                    $"{this.SpriteRect.Value.Width};" +
                    $"{this.SpriteRect.Value.Height}" 
                    : string.Empty,
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
            if (data.Texture != null)
                this.TexturePath = data.Texture;
            if (data.Angle.HasValue)
                this.Angle = data.Angle.Value;
            if (data.Rect != null)
            {
                var sp = data.Rect.Split(';');
                if (sp != null &&
                    sp.Length == 4 &&
                    int.TryParse(sp[0], out var left) &&
                    int.TryParse(sp[1], out var top) &&
                    int.TryParse(sp[2], out var width) &&
                    int.TryParse(sp[3], out var heigth))
                {
                    this.SpriteRect = new IntRect(left, top, width, heigth);
                }
            }

            if (this.TexturePath != null && this.SpriteRect != null)
            {
                this.Sprite = AssetsLoader.LoadSprite(this.TexturePath, this.SpriteRect.Value);
            }
        }


        private class Data
        {
            [XmlAttribute(AttributeName = "color")]
            public string? Color;
            [XmlAttribute(AttributeName = "texture")]
            public string? Texture;
            [XmlAttribute(AttributeName = "rect")]
            public string? Rect;
            [XmlAttribute(AttributeName = "angle")]
            public float? Angle;
        }
    }
}
