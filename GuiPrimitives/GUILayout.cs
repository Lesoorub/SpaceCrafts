using SFML.Graphics;
using SFML.System;

namespace GuiPrimitives
{
    public static class GUILayout
    {
        static RenderTarget? render;
        static InputData inputData = new InputData();
        static Font? defaultFont;
        static Vector2f position;
        static Text text = new Text();
        public static void Init(RenderTarget render)
        {
            GUILayout.render = render;
        }

        public static InputData GetInputData()
        {
            return inputData;
        }

        public static void SetFont(Font font)
        {
            GUILayout.defaultFont = font;
            GUILayout.text = new Text(string.Empty, font);
        }

        public static void SetPosition(float x, float y)
        {
            GUILayout.position = new Vector2f(x, y);
        }
        public static void SetPosition(Vector2f pos)
        {
            GUILayout.position = pos;
        }

        public static void Text(string text)
        {
            if (GUILayout.render == null) return;
            GUILayout.text.Position = position;
            GUILayout.text.DisplayedString = text;
            GUILayout.text.FillColor = Color.White;
            GUILayout.render.Draw(GUILayout.text);
        }

        public static void Text(string text, Vector2f size, HorizontalAligment horizontal, VerticalAligment vertical)
        {
            if (GUILayout.render == null) return;
            GUILayout.text.Position = position;
            GUILayout.text.DisplayedString = text;
            GUILayout.render.Draw(GUILayout.text);
        }
        //public static bool Button(string text, Vector2f size)
        //{

        //}

        public class InputData
        {
            public int MouseX;
            public int MouseY;
            public MouseButton MousePressedButton;
            [Flags]
            public enum MouseButton
            {
                None = 0,
                Left = 1,
                Middle = 2,
                Right = 3,
            }
        }
    }
    public enum HorizontalAligment
    {
        None = 0,
        Left = 1,
        Middle = 2,
        Right = 3,
    }
    public enum VerticalAligment
    {
        None = 0,
        Top = 1,
        Middle = 2,
        Bottom = 3,
    }
}