namespace DeepExtensionsGeneratorUnitTests.Models;

public class MapTreeLeafModel(string? name = null)
{
    public string Name { get; set; } = name ?? string.Empty;

    public override int GetHashCode()
    {
        return Name.GetHashCode();
    }

    public override bool Equals(object? obj)
    {
        if (obj is MapTreeLeafModel other)
        {
            return Name == other.Name;
        }
        return false;
    }
}
