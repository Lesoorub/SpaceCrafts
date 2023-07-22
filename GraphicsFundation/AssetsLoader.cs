using SFML.Graphics;

namespace GraphicsFundation.Graphics.Resources
{
    public static class AssetsLoader
    {
        static Dictionary<string, Texture> s_textures = new Dictionary<string, Texture>();
        static Dictionary<Texture, Dictionary<IntRect, Sprite>> s_sprites = new Dictionary<Texture, Dictionary<IntRect, Sprite>>();
        
        public static Texture LoadTexture(string texturePath)
        {
            if (!s_textures.TryGetValue(texturePath, out var texture))
                s_textures.Add(texturePath, texture = new Texture(texturePath));
            return texture;
        }

        public static Sprite LoadSprite(string texturePath, IntRect rect)
        {
            var texture = LoadTexture(texturePath);
            if (!s_sprites.TryGetValue(texture, out var texture_sprites))
                s_sprites.Add(texture, texture_sprites = new Dictionary<IntRect, Sprite>());
            if (!texture_sprites.TryGetValue(rect, out var sprite))
                texture_sprites.Add(rect, sprite = new Sprite(texture, rect));
            return sprite;
        }
    }
}