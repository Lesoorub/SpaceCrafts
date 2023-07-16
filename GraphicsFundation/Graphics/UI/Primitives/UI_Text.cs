using System.Resources;
using System.Xml;
using System.Xml.Serialization;
using ClientApplication.Graphics.UI;
using GraphicsFundation.Graphics.UI.Helpers;
using GraphicsFundation.Properties;
using SFML.Graphics;
using static GraphicsFundation.Graphics.UI.Helpers.ColorConverter;

namespace GraphicsFundation.Graphics.UI.Primitives
{
    [UIXmlName("label")]
    public class UI_Text : UIElement
    {
        public string DisplayText = string.Empty;
        public uint FontSize = 14;
        public Font Font = default_font;
        public Color ForeColor = Color.White;

        static Font default_font = new Font(Resources.consolas);
        static Text text = new Text();

        public override void Draw(RenderTarget target, RenderStates states)
        {
            text.Position = this.Position;
            text.DisplayedString = this.DisplayText;
            text.CharacterSize = this.FontSize;
            text.Font = this.Font;
            text.FillColor = this.ForeColor;
            target.Draw(text, states);
        }

        public override XmlElement Serialize(XmlDocument document, XmlElement parent)
        {
            var node = base.Serialize(document, parent);
            var data = new Data()
            {
                Text = this.DisplayText,
                FontSize = this.FontSize,
                Color = Color2String(this.ForeColor),
            };
            node.WriteObject(data);
            return node;
        }

        public override void Deserialize(XmlElement node)
        {
            base.Deserialize(node);
            var data = node.ConvertNode<Data>();
            if (data == null) return;
            if (data.Text != null)
                this.DisplayText = data.Text;
            if (data.FontSize.HasValue)
                this.FontSize = data.FontSize.Value;
            if (data.Color != null)
                this.ForeColor = String2Color(data.Color);
        }

        private class Data
        {
            [XmlAttribute(AttributeName = "text")]
            public string? Text;
            [XmlAttribute(AttributeName = "font-size")]
            public uint? FontSize;
            [XmlAttribute(AttributeName = "color")]
            public string? Color;
        }
    }
}
