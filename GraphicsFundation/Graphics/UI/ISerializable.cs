namespace ClientApplication.Graphics.UI
{
    public interface ISerializable
    {
        void Deserialize(string str);
        string Serialize();
    }
}
