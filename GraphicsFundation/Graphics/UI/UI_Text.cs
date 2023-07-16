using System.Xml;
using System.Xml.Serialization;
using SFML.Graphics;

namespace ClientApplication.Graphics.UI
{
    [UIXmlName("label")]
    public class UI_Text : UIElement
    {
        public string DisplayText = string.Empty;

        static Text text = new Text();

        public override void Draw(RenderTarget target, RenderStates states)
        {
            text.Position = this.Position;
            text.DisplayedString = this.DisplayText;
            target.Draw(text, states);
        }

        public override XmlElement Serialize(XmlDocument document, XmlElement parent)
        {
            var node = base.Serialize(document, parent);
            var data = new Data()
            {
                Text = this.DisplayText
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
        }

        private class Data
        {
            [XmlAttribute(AttributeName = "text")]
            public string? Text;
        }
    }
}
