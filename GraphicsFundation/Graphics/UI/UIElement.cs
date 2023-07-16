using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SFML.Graphics;
using SFML.System;

namespace ClientApplication.Graphics.UI
{
    public abstract class UIElement : Drawable, ISerializable
    {
        public bool Visible = true;
        public virtual Vector2f Position { get; set; }
        public virtual Vector2f Size { get; set; }
        public virtual Vector2f Origin { get; set; }
        public int Z;

        public abstract void Draw(RenderTarget target, RenderStates states);
        public virtual bool IsHit(Vector2f point)
        {
            float left = this.Position.X - this.Size.X * this.Origin.X;
            float right = left + this.Size.X;
            float top = this.Position.Y - this.Size.Y * this.Origin.Y;
            float bottom = top + this.Size.Y;

            return point.X >= left && point.X <= right && point.Y >= top && point.Y <= bottom;
        }

        public virtual void Deserialize(string str)
        {
            var data = JsonConvert.DeserializeObject<Data>(str);
            if (data == null) return;

            if (data.Position != null && data.Position.Length == 2)
                this.Position = new Vector2f(data.Position[0], data.Position[1]);

            if (data.Size != null && data.Size.Length == 2)
                this.Size = new Vector2f(data.Size[0], data.Size[1]);

            if (data.Origin != null && data.Origin.Length == 2)
                this.Origin = new Vector2f(data.Origin[0], data.Origin[1]);

            if (data.Visible.HasValue)
                this.Visible = data.Visible.Value;

            if (data.Z.HasValue)
                this.Z = data.Z.Value;
        }

        public virtual string Serialize()
        {
            return JsonConvert.SerializeObject(new Data()
            {
                Visible = this.Visible,
                Position = new float[] { this.Position.X, this.Position.Y },
                Size = new float[] { this.Size.X, this.Size.Y },
                Origin = new float[] { this.Origin.X, this.Origin.Y },
                Z = this.Z
            });
        }

        private class Data
        {
            public bool? Visible;
            public float[]? Position;
            public float[]? Size;
            public float[]? Origin;
            public int? Z;
        }
    }
}
