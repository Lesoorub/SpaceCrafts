using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json.Serialization;
using System.Xml;
using System.Xml.Serialization;
using Newtonsoft.Json;
using SFML.Graphics;
using SFML.System;

namespace ClientApplication.Graphics.UI
{
    public class UI_Node : UIElement
    {
        #region Fields

        #region Static

        static int GlobalNameCounter = 0;

        #endregion

        #region Public

        public string Name = $"No name ({GlobalNameCounter++})";

        public UI_Node? Parent;

        public readonly List<UIElement> Drawables = new List<UIElement>();

        public HorizontalAnchor Horizontal;

        public VerticalAnchor Vertical;

        #endregion

        #region Private

        readonly List<UI_Node> m_childrens = new List<UI_Node>();

        #endregion

        #endregion

        #region Properties

        public override string NodeName => "node";
        public UI_Node root
        {
            get
            {
                if (this.Parent == null)
                    return this;
                var cur = this;
                while (cur.Parent != null)
                    cur = cur.Parent;
                return cur;
            }
        }
        public IEnumerable<UI_Node> Childrens => this.m_childrens;
        Vector2f AnchoredPosition
        {
            get
            {
                float Calc1D(
                    float size, 
                    float position,
                    float parentSize, 
                    float parentPosition, 
                    byte anchor, 
                    byte low,
                    byte middle, 
                    byte high)
                {
                    var stretch = (high | low);
                    if ((anchor & stretch) == stretch)
                        return parentPosition;
                    if ((anchor & low) == low)
                        return parentPosition;
                    if ((anchor & high) == high)
                        return parentSize - size + parentPosition;
                    if ((anchor & middle) == middle)
                        return parentPosition + parentSize / 2 - size / 2;
                    return position;
                }


                if (this.Horizontal == HorizontalAnchor.None &&
                    this.Vertical == VerticalAnchor.None)
                    return this.Position;

                return new Vector2f(
                    Calc1D(
                        this.Size.X,
                        this.Position.X,
                        this.Parent != null ? this.Parent.AnchoredSize.X : GraphicSettings.Default.CurrentResolution.Width,
                        this.Parent != null ? this.Parent.AnchoredPosition.X : this.Position.X,
                        (byte)this.Horizontal,
                        (byte)HorizontalAnchor.Left,
                        (byte)HorizontalAnchor.Middle,
                        (byte)HorizontalAnchor.Right
                        ),
                    Calc1D(
                        this.Size.Y,
                        this.Position.X,
                        this.Parent != null ? this.Parent.AnchoredSize.Y : GraphicSettings.Default.CurrentResolution.Height,
                        this.Parent != null ? this.Parent.AnchoredPosition.Y : this.Position.Y,
                        (byte)this.Vertical,
                        (byte)VerticalAnchor.Top,
                        (byte)VerticalAnchor.Middle,
                        (byte)VerticalAnchor.Bottom
                        )
                    );
            }
        }
        Vector2f AnchoredSize
        {
            get
            {
                float GetSize1D(float size, float parentSize, byte anchor, byte stretch)
                {
                    if ((anchor & stretch) == stretch)
                        return parentSize;
                    return size;
                }

                Vector2f parentSize;

                if (this.Parent != null)
                {
                    parentSize = this.Parent.Size;
                }
                else
                {
                    parentSize = new Vector2f(
                        GraphicSettings.Default.CurrentResolution.Width,
                        GraphicSettings.Default.CurrentResolution.Height);
                }

                return new Vector2f(
                    GetSize1D(
                        this.Size.X,
                        parentSize.X,
                        (byte)this.Horizontal,
                        (byte)HorizontalAnchor.Stretch),
                    GetSize1D(
                        this.Size.Y,
                        parentSize.Y,
                        (byte)this.Vertical,
                        (byte)VerticalAnchor.Stretch)
                    );
            }
        }

        #region Static

        static Lazy<Dictionary<string, Type>> s_drawableTypes = new Lazy<Dictionary<string, Type>>(() =>
        {
            var asm = Assembly.GetExecutingAssembly();
            var types = asm.GetTypes();
            var dict = new Dictionary<string, Type>();
            foreach (var type in types)
            {
                var attr = type.GetCustomAttribute<UIXmlNameAttribute>();
                if (attr == null) continue;
                dict.Add(attr.Name, type);
            }
            return dict;
        });

        #endregion

        #endregion

        #region Methods

        #region Public

        public UI_Node AddChild(UI_Node child)
        {
            child.Parent = this;
            this.m_childrens.Add(child);
            return this;
        }

        public void RemoveParent()
        {
            this.Parent?.m_childrens.Remove(this);
            this.Parent = null;
        }

        public UI_Node AddUIElement(UIElement child)
        {
            this.Drawables.Add(child);
            return this;
        }

        public bool TryFindNode(string name, out UI_Node result)
        {
            if (string.Equals(this.Name, name))
            {
                result = this;
                return true;
            }
            foreach (var item in this.Childrens)
            {
                if (item.TryFindNode(name, out result))
                    return true;
            }
            result = default!;
            return false;
        }

        #endregion

        #region Overrides

        public override void Draw(RenderTarget target, RenderStates states)
        {
            var pos = this.AnchoredPosition;
            var size = this.AnchoredSize;
            foreach (var drawable in this.Drawables)
            {
                drawable.Position = pos;
                drawable.Size = size;
                drawable.Draw(target, states);
            }
            foreach (var child in this.Childrens)
                child.Draw(target, states);
        }
        
        public override XmlElement Serialize(XmlDocument document, XmlElement parent)
        {
            var node = base.Serialize(document, parent);
            node.WriteObject(new Data()
            {
                Horizontal = this.Horizontal.ToString().ToLower(),
                Vertical = this.Vertical.ToString().ToLower(),
                Name = this.Name.ToString(),
            });

            foreach (var child in this.Childrens)
                node.AppendChild(child.Serialize(document, node));

            return node;
        }

        public override void Deserialize(XmlElement node)
        {
            base.Deserialize(node);
            var data = node.ConvertNode<Data>();
            if (data == null) return;

            if (data.Name != null)
                this.Name = data.Name;
            if (data.Vertical != null)
                Enum.TryParse(data.Vertical, ignoreCase: true, out this.Vertical);
            if (data.Horizontal != null)
                Enum.TryParse(data.Horizontal, ignoreCase: true, out this.Horizontal);

            if (node.Name != this.NodeName)
            {
                if (s_drawableTypes.Value.TryGetValue(node.Name, out var drawableType))
                {
                    var el = (UIElement)Activator.CreateInstance(drawableType)!;
                    el.Deserialize(node);
                    this.AddUIElement(el);
                }
            }

            foreach (XmlElement child in node.ChildNodes)
            {
                var n = new UI_Node();
                n.Deserialize(child);
                this.AddChild(n);
            }
        }

        #endregion

        #endregion

        [Flags]
        public enum HorizontalAnchor : byte
        {
            None = 0,
            Left = 1,
            Right = 2,
            Stretch = Left | Right,
            Middle = 4,
        }
        [Flags]
        public enum VerticalAnchor : byte
        {
            None = 0,
            Top = 1,
            Bottom = 2,
            Stretch = Top | Bottom,
            Middle = 4,
        }

        private class Data
        {
            [XmlAttribute(AttributeName = "name")]
            public string? Name;

            [XmlAttribute(AttributeName = "horizontal")]
            public string? Horizontal;
            [XmlAttribute(AttributeName = "vertical")]
            public string? Vertical;
        }
    }
}
