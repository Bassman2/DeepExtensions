using DeepExtensions;

namespace DeepExtensionsGeneratorUnitTests.Models;

public partial class EqualBaseModel
{
    public string BaseName { get; set; } = "";
    public double BaseValue { get; set; }

    public EqualLeafModel BaseLeaf { get; set; } = new EqualLeafModel();

    public EqualLeafModel? BaseLeafNull { get; set; } = null;

    public List<EqualLeafModel> BaseLeaveList { get; set; } = [];

    public EqualLeafModel[] BaseLeaveArray { get; set; } = [];

    [DeepEqualIgnore, DeepCopyIgnore]
    public string BaseNameX { get; set; } = "";

    [DeepEqualIgnore, DeepCopyIgnore]
    public double BaseValueX { get; set; }
}
