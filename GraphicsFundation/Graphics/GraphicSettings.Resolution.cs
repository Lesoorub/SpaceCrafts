namespace ClientApplication.Graphics
{
    public partial class GraphicSettings
    {
        public struct Resolution
        {
            public uint Width, Height;

            public Resolution(uint width, uint height)
            {
                this.Width = width;
                this.Height = height;
            }
        }
    }
}
