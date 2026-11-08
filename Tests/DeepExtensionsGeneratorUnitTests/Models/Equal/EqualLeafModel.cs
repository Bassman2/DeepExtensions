namespace DeepExtensionsGeneratorUnitTests.Models;

public partial class EqualLeafModel
{
    public string Name { get; set; } = "";
    public double Value { get; set; }

    [DeepEqualIgnore, DeepCopyIgnore]
    public string NameX { get; set; } = "";

    [DeepEqualIgnore, DeepCopyIgnore]
    public double ValueX { get; set; }
}
