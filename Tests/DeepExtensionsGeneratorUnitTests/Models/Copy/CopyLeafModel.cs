using DeepExtensions;

namespace DeepExtensionsGeneratorUnitTests.Models;

public partial class CopyLeafModel
{
    public string Name { get; set; } = "";
    public double Value { get; set; }

    [DeepCopyIgnore]
    public string NameX { get; set; } = "";

    [DeepCopyIgnore]
    public double ValueX { get; set; }
}
