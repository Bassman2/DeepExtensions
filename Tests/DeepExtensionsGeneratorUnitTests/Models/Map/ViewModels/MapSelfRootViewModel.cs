using CommunityToolkit.Mvvm.ComponentModel;
using DeepExtensions;
using DeepExtensionsGeneratorUnitTests.Models;

namespace DeepExtensionsGeneratorUnitTests.ViewModels;

[DeepMap(typeof(ListSimpleModel))]
public partial class MapSelfRootViewModel : ObservableObject
{
    [DeepMapProperty]
    public List<MapSelfLeafModel> Sections { get; }

}
