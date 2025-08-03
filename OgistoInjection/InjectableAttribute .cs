namespace OgistoInjection;

[AttributeUsage(AttributeTargets.Class)]
public class InjectableAttribute : Attribute
{
    public DITypes Type { get; }

    public InjectableAttribute(DITypes type)
    {
        Type = type;
    }
}
