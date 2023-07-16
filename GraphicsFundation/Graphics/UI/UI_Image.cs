using System.Drawing;
using System.Xml.Linq;
using Newtonsoft.Json;
using SFML.Graphics;
using SFML.System;
using Color = SFML.Graphics.Color;

namespace ClientApplication.Graphics.UI
{
    public class UI_Image : UIElement, IDisposable
    {
        public Sprite? Sprite;
        static RectangleShape rectangle = new RectangleShape();
        public Color Color = Color.White;

        public UI_Image()
        {
            this.Origin = new Vector2f(.5f, .5f);
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

        public override void Deserialize(string str)
        {
            base.Deserialize(str);
            var data = JsonConvert.DeserializeObject<Data>(str);
            if (data == null) return;
            if (data.Texture != null
                && data.Rect != null 
                && data.Rect.Length == 4 
                && data.Rect.All(x => x > 0))
                this.Sprite = AssetsLoader.LoadSprite(data.Texture, new IntRect()
                {
                    Left = data.Rect[0],
                    Top = data.Rect[1],
                    Width = data.Rect[2],
                    Height = data.Rect[3],
                });

        }

        public override string Serialize()
        {
            var data = new Data();

            if (this.Sprite != null)
                data.Rect = new int[] { 
                    this.Sprite.TextureRect.Left, 
                    this.Sprite.TextureRect.Top,
                    this.Sprite.TextureRect.Width,
                    this.Sprite.TextureRect.Height,
                };

            return JsonConvert.SerializeObject(data);
        }

        private class Data
        {
            public string? Texture;
            public int[]? Rect;
        }
    }
}
