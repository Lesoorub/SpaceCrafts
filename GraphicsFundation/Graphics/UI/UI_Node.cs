using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using SFML.Graphics;
using SFML.System;

namespace ClientApplication.Graphics.UI
{
    public class UI_Node : UIElement
    {
        static int GlobalNameCounter = 0;
        
        public string Name = $"No name ({GlobalNameCounter++})";
        public UI_Node? Parent;
        public readonly List<UIElement> Drawables = new List<UIElement>();
        public AnchorStyles Anchor = AnchorStyles.Left | AnchorStyles.Right;

        readonly List<UI_Node> m_childrens = new List<UI_Node>();

        public IEnumerable<UI_Node> Childrens => this.m_childrens;
        Vector2f AnchoredPosition
        {
            get
            {
                float Calc1D(float size, float parentSize, float parentPosition, AnchorStyles low, AnchorStyles high)
                {
                    var stretch = (high | low);
                    if ((this.Anchor & stretch) == stretch)
                    {
                        return parentPosition;
                    }
                    if ((this.Anchor & low) == low)
                    {
                        return parentPosition;
                    }
                    if ((this.Anchor & high) == high)
                    {
                        return parentSize - size + parentPosition;
                    }
                    return parentPosition + parentSize / 2 - size / 2;
                }


                if (this.Anchor == AnchorStyles.None)
                    return this.Position;

                return new Vector2f(
                    Calc1D(
                        this.Size.X,
                        this.Parent != null ? this.Parent.AnchoredSize.X : GraphicSettings.Default.CurrentResolution.Width,
                        this.Parent != null ? this.Parent.AnchoredPosition.X : this.Position.X,
                        AnchorStyles.Left,
                        AnchorStyles.Right),
                    Calc1D(
                        this.Size.Y,
                        this.Parent != null ? this.Parent.AnchoredSize.Y : GraphicSettings.Default.CurrentResolution.Height,
                        this.Parent != null ? this.Parent.AnchoredPosition.Y : this.Position.Y,
                        AnchorStyles.Top,
                        AnchorStyles.Bottom)
                    );
            }
        }
        Vector2f AnchoredSize
        {
            get
            {
                if (this.Anchor == AnchorStyles.None)
                    return this.Size;

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

                if ((this.Anchor & AnchorStyles.Fill) == AnchorStyles.Fill)
                {
                    return parentSize;
                }
                else
                {
                    if ((this.Anchor & AnchorStyles.StretchX) == AnchorStyles.StretchX)
                    {
                        return new Vector2f(parentSize.X, this.Size.Y);
                    }
                    if ((this.Anchor & AnchorStyles.StretchY) == AnchorStyles.StretchY)
                    {
                        return new Vector2f(this.Size.X, parentSize.Y);
                    }
                }

                return this.Size;
            }
        }

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

        public override string Serialize()
        {
            return base.Serialize();
        }

        public override void Deserialize(string str)
        {
            base.Deserialize(str);
        }

        #region Static

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UI_Node FromJsonBytes(byte[] bytesString)
        {
            return FromJsonStr(Encoding.UTF8.GetString(bytesString));
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UI_Node FromJsonStr(string json)
        {
            return UI_Node_JSON_Converter.FromJsonStr(json);
        }

        public static string ToJson(UI_Node node)
        {
            return UI_Node_JSON_Converter.ToJson(node);
        }

        #endregion

        [Flags]
        public enum AnchorStyles
        {
            None = 0,
            MiddleCenter = 1,
            Left = 2,
            Right = 4,
            Top = 8,
            Bottom = 16,

            TopMiddle = Top,//8
            BottomMiddle = Bottom,//16
            LeftCenter = Left,//2
            RightCenter = Right,//4
            TopLeft = Top | Left,//10
            BottomLeft = Bottom | Left,//18
            TopRight = Top | Right,//12
            BottomRight = Bottom | Right,//20
            BottomStretch = Left | Right | Bottom,//22
            StretchX = Left | Right,//6
            TopStretch = Left | Right | Top,//14
            LeftStretch = Top | Bottom | Left,//26
            StretchY = Top | Bottom,//24
            RightStretch = Top | Bottom | Right,//28
            Fill = StretchX | StretchY,//30
        }
    }
}
