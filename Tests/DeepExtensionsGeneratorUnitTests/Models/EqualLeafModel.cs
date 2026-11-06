namespace DeepExtensionsGeneratorUnitTests.Models;

public partial class EqualLeafModel
{
    public string Name { get; set; } = "";
    public double Value { get; set; }

    [DeepEqualIgnore]
    public string NameX { get; set; } = "";

    [DeepEqualIgnore]
    public double ValueX { get; set; }
}
