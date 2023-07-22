using System;
using GraphicsFundation.Graphics.Resources;
using SFML.Graphics;
using SFML.System;
using static System.Net.Mime.MediaTypeNames;

namespace GraphicsFundation.Graphics.Forms
{
    public class Picture : Control
    {
        Sprite sprite = new Sprite();

        private string? m_texturePath;

        static VertexArray noImage = new VertexArray(PrimitiveType.LineStrip, 8);

        static Picture()
        {
            for (uint k = 0; k < noImage.VertexCount; k++)
            {
                var vertex = noImage[k];
                vertex.Color = Color.Red;
                noImage[k] = vertex;
            }
        }

        public PictureStretch SizeMode { get; set; } = PictureStretch.StretchImage;

        public Texture? Texture
        {
            get => this.sprite.Texture;
            set => this.sprite.Texture = value;
        }

        public string? TexturePath
        {
            get => this.m_texturePath;
            set
            {
                this.m_texturePath = value;
                if (value != null)
                    this.sprite.Texture = AssetsLoader.LoadTexture(value);
                else
                    this.sprite.Texture = null;
            }
        }

        public IntRect SpriteRect
        {
            get => this.sprite.TextureRect;
            set => this.sprite.TextureRect = value;
        }

        public override void Draw(RenderTarget target, RenderStates states)
        {
                var vertex = noImage[0];
                vertex.Position = this.GlobalPosition;
                noImage[0] = vertex;
                vertex.Position = this.GlobalPosition + new Vector2f(this.Size.X, 0);
                noImage[1] = vertex;
                vertex.Position = this.GlobalPosition + new Vector2f(0, this.Size.Y);
                noImage[2] = vertex;
                vertex.Position = this.GlobalPosition + new Vector2f(this.Size.X, this.Size.Y);
                noImage[3] = vertex;
                vertex.Position = this.GlobalPosition;
                noImage[4] = vertex;
                vertex.Position = this.GlobalPosition + new Vector2f(0, this.Size.Y);
                noImage[5] = vertex;
                vertex.Position = this.GlobalPosition + new Vector2f(this.Size.X, this.Size.Y);
                noImage[6] = vertex;
                vertex.Position = this.GlobalPosition + new Vector2f(this.Size.X, 0);
                noImage[7] = vertex;
                noImage.Draw(target, states);
            if (this.TexturePath == null)
            {
            }
            else
            {
                this.sprite.Position = new Vector2f(0, 0);
                this.sprite.Scale = new Vector2f(1, 1);
                switch (this.SizeMode)
                {
                    case PictureStretch.Normal:
                        this.sprite.Position = this.GlobalPosition;
                        this.sprite.Scale = new Vector2f(1, 1);
                        break;
                    case PictureStretch.Zoom:
                        this.sprite.Position = this.GlobalPosition;
                        float zoom = Math.Min(this.Size.X, this.Size.Y) / Math.Min(this.SpriteRect.Width, this.SpriteRect.Height);
                        this.sprite.Scale = new Vector2f(zoom, zoom);
                        break;
                    case PictureStretch.StretchImage:
                        this.sprite.Position = this.GlobalPosition;
                        this.sprite.Scale = new Vector2f(
                            this.Size.X / this.SpriteRect.Width,
                            this.Size.Y / this.SpriteRect.Height);
                        break;
                    case PictureStretch.AutoSize:
                        this.sprite.Position = this.GlobalPosition;
                        this.Size = new Vector2f(this.SpriteRect.Width, this.SpriteRect.Height);
                        break;
                    case PictureStretch.CenterImage:
                        this.sprite.Position = this.GlobalPosition + this.Size / 2 - new Vector2f(this.SpriteRect.Width, this.SpriteRect.Height) / 2;
                        break;
                }
                this.sprite.Draw(target, states);
            }
            base.Draw(target, states);
        }
    }
    public enum PictureStretch
    {
        /// <summary>
        /// The image is placed in the upper-left corner of the PictureBox. The 
        /// image is clipped if it is larger than the PictureBox it is contained in.
        /// </summary>
        Normal = 0,
        /// <summary>
        /// The image within the PictureBox is stretched or shrunk to fit the size 
        /// of the PictureBox.
        /// </summary>
        StretchImage = 1,
        /// <summary>
        /// The PictureBox is sized equal to the size of the image that it contains.`
        /// </summary>
        AutoSize = 2,
        /// <summary>
        /// The image is displayed in the center if the PictureBox is larger than
        /// the image. If the image is larger than the PictureBox, the picture is
        /// placed in the center of the PictureBox and the outside edges are clipped.
        /// </summary>
        CenterImage = 3,
        /// <summary>
        /// The size of the image is increased or decreased maintaining the size ratio.
        /// </summary>
        Zoom = 4,
    }
}
