using System.Diagnostics;
using System.Drawing;
using BuilderGUI.Extentions;
using SFML.Graphics;
using SFML.System;
using Color = SFML.Graphics.Color;
using Font = SFML.Graphics.Font;

namespace BuilderGUI
{
    public class Label : Control
    {
        public string? Text
        {
            get => this.text.DisplayedString;
            set
            {
                this.text.DisplayedString = value == null ? string.Empty : value;
                return;
                //var text = value == null ? string.Empty : value;
                //string formatted = string.Empty;
                //var sp = text.Split(' ');
                //Font font = this.data.Font;
                //var fontSize = this.data.FontSize;

                //if (font == null) this.data.Text = text;

                //FloatRect MergeRectangles(FloatRect rect1, FloatRect rect2)
                //{
                //    float left = Math.Min(rect1.Left, rect2.Left);
                //    float top = Math.Min(rect1.Top, rect2.Top);

                //    float right = Math.Max(rect1.Left + rect1.Width, rect2.Left + rect2.Width);
                //    float bottom = Math.Max(rect1.Top + rect1.Height, rect2.Top + rect2.Height);

                //    return new FloatRect(left, top, right - left, bottom - top);
                //}

                //FloatRect GetWordSize(string word)
                //{
                //    FloatRect rect = new FloatRect();
                //    for (int k = 0; k < word.Length; k++)
                //    {
                //        var glyph = font!.GetGlyph(word[k], fontSize, bold: false, 0);
                //        var b = glyph.Bounds;
                //        b.Left = rect.Width;
                //        b.Top = 0;
                //        rect = MergeRectangles(rect, b);
                //        if (k != word.Length - 1)
                //            rect.Width += glyph.Advance;
                //    }
                //    return rect;
                //}

                //FloatRect rect = new FloatRect();
                //foreach (var s in sp)
                //{
                //    var size = GetWordSize(s);
                //    var newRect = MergeRectangles(rect, size);
                //    if (newRect.Width > this.Size.X)
                //    {

                //    }
                //}

                //this.data.Text = formatted;
            }
        }
        public Color ForeColor
        {
            get => this.text.FillColor;
            set => this.text.FillColor = value;
        }

        public Font Font => this.text.Font;
        public uint FontSize
        {
            get => this.text.CharacterSize;
            set => this.text.CharacterSize = value;
        }

        public Vector2f Size = new Vector2f(200, 50);

        Text text = new Text(string.Empty, Consolas);
        static Font Consolas = new Font(Properties.Resources.consolas);
        public override void Draw(RenderTarget window)
        {
            this.text.Position = base.GlobalPostion;
            window.Draw(this.text);
        }

        public override bool IsHover(float x, float y)
        {
            var pos = base.GlobalPostion;
            return false;
        }

        public FloatRect GetWordSize(string word)
        {
            FloatRect rect = new FloatRect();
            for (int k = 0; k < word.Length; k++)
            {
                var glyph = this.Font.GetGlyph(word[k], this.FontSize, bold: false, 0);
                var b = glyph.Bounds;
                b.Left = rect.Width;
                b.Top = 0;
                rect = rect.Expand(b);
                if (k != word.Length - 1)
                    rect.Width += glyph.Advance;
            }
            return rect;
        }

        public FloatRect[] GetWordRectangles()
        {
            var text = this.Text;
            if (text == null) return null!;
            var font = this.Font;
            var fontSize = this.FontSize;
            List<FloatRect> rects = new List<FloatRect>();

            var bounds = this.text.GetLocalBounds();

            for (uint k = 0; k < text.Length; k++)
            {
                var ch = text[(int)k];
                var glyph = font.GetGlyph(ch, fontSize, bold: false, 0);
                var b = glyph.Bounds;

                var pos = this.text.FindCharacterPos(k);

                b.Left = pos.X + bounds.Left;
                b.Top = bounds.Height - pos.Y + bounds.Top - b.Height;
                rects.Add(b);
            }
            return rects.ToArray();
        }
    }
}