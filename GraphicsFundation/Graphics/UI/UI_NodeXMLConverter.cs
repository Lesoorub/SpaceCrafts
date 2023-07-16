using System.Xml.Serialization;
using System.Xml;
using System.Reflection;
using System.Text;

namespace ClientApplication.Graphics.UI
{
    public static class UI_NodeXMLConverter
    {
        public static UI_Node FromXml(byte[] bytes)
        {
            return FromXml(Encoding.UTF8.GetString(bytes));
        }
        public static UI_Node FromXml(string xml)
        {
            var doc = new XmlDocument();
            doc.LoadXml(xml);
            var node = new UI_Node();
            if (doc.DocumentElement != null)
                node.Deserialize(doc.DocumentElement);
            return node;
        }
        public static T? ConvertNode<T>(this XmlNode node) where T : class, new()
        {
            var attrs = node.Attributes;
            var t = new T();
            if (attrs != null)
            {
                var fields = typeof(T)
                    .GetFields(BindingFlags.Public | BindingFlags.Instance)
                    .ToDictionary(
                    x =>
                    {
                        var name = x.Name;

                        var attr = x.GetCustomAttribute<XmlAttributeAttribute>();
                        if (attr != null)
                            name = attr.AttributeName;

                        return name;
                    },
                    x => x);

                foreach (XmlAttribute attr in attrs)
                {
                    if (!fields.TryGetValue(attr.Name, out var field))
                        continue;
                    if (field.FieldType == typeof(string))
                        field.SetValue(t, (string)attr.Value);
                    else if (field.FieldType == typeof(int) && int.TryParse(attr.Value, out var intV))
                        field.SetValue(t, intV);
                    else if (field.FieldType == typeof(bool) && bool.TryParse(attr.Value, out var boolV))
                        field.SetValue(t, boolV);
                }
            }
            return t;
        }

        public static void WriteObject(this XmlElement node, object obj)
        {
            foreach (var item in obj.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance))
            {
                var name = item.Name;
                var attr = item.GetCustomAttribute<XmlAttributeAttribute>();
                if (attr != null)
                {
                    name = attr.AttributeName;
                }
                var val = item.GetValue(obj);
                if (val != null)
                    node.SetAttribute(name, val.ToString());
            }
        }
    }
}
