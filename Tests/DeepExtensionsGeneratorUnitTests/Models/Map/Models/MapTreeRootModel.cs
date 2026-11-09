namespace DeepExtensionsGeneratorUnitTests.Models;

public class MapTreeRootModel
{
    public MapTreeLeafModel Child { get; set; } = new MapTreeLeafModel();
    public MapTreeLeafModel? Leaf { get; set; } = null;
    public List<MapTreeLeafModel> Children { get; set; } = [];
    public List<MapTreeLeafModel>? Leaves { get; set; } = [];
}
