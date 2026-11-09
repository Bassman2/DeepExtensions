using DeepExtensions;

namespace DeepExtensionsGeneratorUnitTests.Models;

[DeepCopy]
public partial class CopyRootModel : CopyBaseModel
{
    public string Name { get; set; } = "";
    public double Value { get; set; }

    public CopyLeafModel Leaf { get; set; } = new CopyLeafModel();

    public CopyLeafModel? LeafNull { get; set; } = null;

    public List<CopyLeafModel> LeaveList { get; set; } = [];

    public CopyLeafModel[] LeaveArray { get; set; } = [];

    [DeepCopyIgnore]
    public string NameX { get; set; } = "";

    [DeepCopyIgnore]
    public double ValueX { get; set; }
}
