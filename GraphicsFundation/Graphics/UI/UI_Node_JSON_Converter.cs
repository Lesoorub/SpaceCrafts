using System.Runtime.CompilerServices;
using Newtonsoft.Json;
using SFML.System;
using static ClientApplication.Graphics.UI.UI_Node;

namespace ClientApplication.Graphics.UI
{
    static class UI_NodeXMLConverter
    {

    }
    static class UI_Node_JSON_Converter
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UI_Node FromJsonStr(string json)
        {
            return FromJson(JsonConvert.DeserializeObject<JsonData>(json));
        }
        private static UI_Node FromJson(JsonData? json)
        {
            var node = new UI_Node();
            if (json == null) return node;

            if (json.Position != null && json.Position.Length == 2)
                node.Position = new Vector2f(json.Position[0], json.Position[1]);

            if (json.Size != null && json.Size.Length == 2)
                node.Size = new Vector2f(json.Size[0], json.Size[1]);

            if (!string.IsNullOrWhiteSpace(json.Anchor))
            {
                node.Anchor = (AnchorStyles)json.Anchor.Split('|')
                    .Select(x => x.Trim())
                    .Select(x =>
                    {
                        if (Enum.TryParse(typeof(AnchorStyles), x, true, out var y))
                            return (int)(AnchorStyles)y!;
                        return default;
                    })
                    .Sum();

            }

            if (json.Name != null)
                node.Name = json.Name;

            if (json.Childrens != null)
                foreach (var child in json.Childrens)
                    node.AddChild(FromJson(child));

            return node;
        }
        public static string ToJson(UI_Node node)
        {
            return JsonConvert.SerializeObject(new JsonData()
            {
                Name = node.Name,
                Size = new float[] { node.Size.X, node.Size.Y },
                Position = new float[] { node.Position.X, node.Position.Y },
                Anchor = node.Anchor.ToString(),
            });
        }
        class JsonData
        {
            public string? Name;
            public float[]? Position;
            public float[]? Size;
            public string? Anchor;

            public JsonData[]? Childrens;
        }
    }
}
