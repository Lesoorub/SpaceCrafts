using SFML.Graphics;

namespace GraphicsFundation.Graphics.UI.Helpers
{
    public static class AssetsLoader
    {
        static Dictionary<string, Texture> textures = new Dictionary<string, Texture>();
        static Dictionary<Texture, Dictionary<IntRect, Sprite>> sprites = new Dictionary<Texture, Dictionary<IntRect, Sprite>>();
        public static Texture LoadTexture(string texturePath)
        {
            if (!textures.TryGetValue(texturePath, out var texture))
                textures.Add(texturePath, texture = new Texture(texturePath));
            return texture;
        }

        public static Sprite LoadSprite(string texturePath, IntRect rect)
        {
            var texture = LoadTexture(texturePath);
            if (!sprites.TryGetValue(texture, out var texture_sprites))
                sprites.Add(texture, texture_sprites = new Dictionary<IntRect, Sprite>());
            if (!texture_sprites.TryGetValue(rect, out var sprite))
                texture_sprites.Add(rect, sprite = new Sprite(texture, rect));
            return sprite;
        }
    }
}