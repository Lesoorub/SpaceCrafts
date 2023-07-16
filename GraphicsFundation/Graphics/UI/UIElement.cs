using System;
using System.Xml;
using System.Xml.Serialization;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SFML.Graphics;
using SFML.System;
using System.Xml.Linq;
using System.Reflection;

namespace ClientApplication.Graphics.UI
{
    public abstract class UIElement : Drawable, ISerializable
    {
        public virtual string NodeName { get; } = "element";
        public bool Visible = true;
        public virtual Vector2f Position { get; set; }
        public virtual Vector2f Size { get; set; }
        public virtual Vector2f Origin { get; set; }
        public int Z;

        private Data data
        {
            get
            {
                return new Data()
                {
                    Visible = this.Visible,
                    Position = $"{this.Position.X}; {this.Position.Y}",
                    Size = $"{this.Size.X}; {this.Size.Y}",
                    Origin = $"{this.Origin.X}; {this.Origin.Y}",
                    Z = this.Z
                };
            }
            set
            {
                if (value == null) return;

                bool TryFromString(string? value, out Vector2f vector)
                {
                    vector = new Vector2f();
                    if (value == null)
                        return false;
                    var sp = value.Split(';')
                        .Select(x => x.Trim())
                        .ToArray();
                    if (sp == null || sp.Length != 2)
                        return false;
                    return 
                        float.TryParse(sp[0], out vector.X) &&
                        float.TryParse(sp[1], out vector.Y);
                }


                if (TryFromString(value.Position, out var vec))
                    this.Position = vec;

                if (TryFromString(value.Size, out vec))
                    this.Size = vec;

                if (TryFromString(value.Origin, out vec))
                    this.Origin = vec;

                this.Z = value.Z;
                this.Visible = value.Visible;
            }
        }

        public abstract void Draw(RenderTarget target, RenderStates states);
        public virtual bool IsHit(Vector2f point)
        {
            float left = this.Position.X - this.Size.X * this.Origin.X;
            float right = left + this.Size.X;
            float top = this.Position.Y - this.Size.Y * this.Origin.Y;
            float bottom = top + this.Size.Y;

            return point.X >= left && point.X <= right && point.Y >= top && point.Y <= bottom;
        }

        public virtual void Deserialize(XmlElement node)
        {
            var newData = node.ConvertNode<Data>();
            if (newData == null) return;
            this.data = newData;
        }

        public virtual XmlElement Serialize(XmlDocument document, XmlElement parent)
        {
            var node = document.CreateElement(this.NodeName);
            parent.AppendChild(node);
            node.WriteObject(this.data);
            return node;
        }

        private class Data
        {
            [XmlAttribute(AttributeName = "visible")]
            public bool Visible = true;
            [XmlAttribute(AttributeName = "position")]
            public string? Position;
            [XmlAttribute(AttributeName = "size")]
            public string? Size;
            [XmlAttribute(AttributeName = "origin")]
            public string? Origin;
            [XmlAttribute(AttributeName = "z")]
            public int Z;
        }
    }
}
