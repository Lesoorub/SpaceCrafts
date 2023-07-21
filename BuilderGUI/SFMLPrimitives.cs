using SFML.Graphics;
using SFML.System;
using Color = SFML.Graphics.Color;
using Font = SFML.Graphics.Font;
using Image = SFML.Graphics.Image;

namespace BuilderGUI
{
    public static partial class SFMLPrimitives
    {
        static RectangleShape rectangle = new RectangleShape();

        public static void DrawRectangle(RenderTarget window, RectangleDrawData drawdata)
        {
            drawdata.Apply(rectangle);
            window.Draw(rectangle);
        }

        public class RectangleDrawData : TransformableDrawData
        {
            public Vector2f Origin;
            public int BorderTickness;
            public Color BackColor;
            public Color BorderColor;
            public Vector2f Size;
            public float width { get => this.Size.X; set => this.Size.X = value; }
            public float height { get => this.Size.Y; set => this.Size.Y = value; }
            public void Apply(RectangleShape shape)
            {
                base.Apply(shape);
                shape.Origin = this.Origin;
                shape.OutlineThickness = this.BorderTickness;
                shape.OutlineColor = this.BorderColor;
                shape.FillColor = this.BackColor;
                shape.Size = this.Size;
            }
        }
        public class TransformableDrawData
        {
            public float Angle;
            public Vector2f Position;
            public float x { get => this.Position.X; set => this.Position.X = value; }
            public float y { get => this.Position.Y; set => this.Position.Y = value; }

            public void Apply(Transformable transformable)
            {
                transformable.Position = this.Position;
                transformable.Rotation = this.Angle;
            }
        }
    }
    public static partial class SFMLPrimitives
    {
        static Text text = new Text();
        public static void DrawText(RenderTarget window, TextDrawData drawdata)
        {
            drawdata.Apply(text);
            window.Draw(text);
        }
        public static FloatRect TextGetLocalBounds(TextDrawData drawdata)
        {
            drawdata.Apply(text);
            return text.GetLocalBounds();
        }
        public static Vector2f FindCharacterPos(uint index, TextDrawData drawdata)
        {
            drawdata.Apply(text);
            return text.FindCharacterPos(index);
        }
        public class TextDrawData : TransformableDrawData
        {
            public string Text = string.Empty;
            public uint FontSize = 14;
            public Color ForeColor = Color.Black;
            public Font Font = Consolas;

            static Font Consolas = new Font(Properties.Resources.consolas);

            public void Apply(Text text)
            {
                base.Apply(text);
                text.DisplayedString = this.Text;
                text.Font = this.Font;
                text.CharacterSize = this.FontSize;
                text.FillColor = this.ForeColor;
            }
        }
    }

    public static partial class SFMLPrimitives
    {
        static Sprite image = new Sprite();

        public static void DrawImage(RenderTarget window, ImageDrawData drawdata)
        {
            drawdata.Apply(image);
            window.Draw(image);
        }

        public class ImageDrawData : TransformableDrawData
        {
            public Vector2f Origin;
            public string? TexturePath;
            public IntRect Rect;

            public void Apply(Sprite image)
            {
                base.Apply(image);
                image.Origin = this.Origin;
                if (this.TexturePath != null)
                    image.Texture = AssetsLoader.LoadTexture(this.TexturePath);
                image.TextureRect = this.Rect;
            }
        }
    }
}