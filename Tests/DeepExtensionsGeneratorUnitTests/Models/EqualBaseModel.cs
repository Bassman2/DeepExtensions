namespace DeepExtensionsGeneratorUnitTests.Models;

[DeepEqual]
public partial class EqualBaseModel
{
    public string Name { get; set; } = "";
    public double Value { get; set; }

    public EqualLeafModel Leaf { get; set; } = new EqualLeafModel();

    public EqualLeafModel? LeafNull { get; set; } = null;

    public List<EqualLeafModel> LeaveList { get; set; } = [];

    public EqualLeafModel[] LeaveArray { get; set; } = [];

    [DeepEqualIgnore]
    public string NameX { get; set; } = "";

    [DeepEqualIgnore]
    public double ValueX { get; set; }
}
