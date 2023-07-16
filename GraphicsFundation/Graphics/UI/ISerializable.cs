using System.Xml;

namespace ClientApplication.Graphics.UI
{
    public interface ISerializable
    {
        void Deserialize(XmlElement node);
        XmlElement Serialize(XmlDocument document, XmlElement parent);
    }
}
