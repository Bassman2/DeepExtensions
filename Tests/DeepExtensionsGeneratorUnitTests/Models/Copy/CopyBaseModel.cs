using DeepExtensions;

namespace DeepExtensionsGeneratorUnitTests.Models;

public partial class CopyBaseModel
{
    public string BaseName { get; set; } = "";
    public double BaseValue { get; set; }

    public CopyLeafModel BaseLeaf { get; set; } = new CopyLeafModel();

    public CopyLeafModel? BaseLeafNull { get; set; } = null;

    public List<CopyLeafModel> BaseLeaveList { get; set; } = [];

    public CopyLeafModel[] BaseLeaveArray { get; set; } = [];

    [DeepCopyIgnore]
    public string BaseNameX { get; set; } = ""; 

    [DeepCopyIgnore]
    public double BaseValueX { get; set; }
}
