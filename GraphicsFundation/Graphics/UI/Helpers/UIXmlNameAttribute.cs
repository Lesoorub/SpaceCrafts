[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
public class UIXmlNameAttribute : Attribute
{
    public readonly string Name;

    public UIXmlNameAttribute(string name)
    {
        this.Name = name;
    }
}