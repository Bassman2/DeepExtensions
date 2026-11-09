using CommunityToolkit.Mvvm.ComponentModel;
using DeepExtensions;
using DeepExtensionsGeneratorUnitTests.Models;

namespace DeepExtensionsGeneratorUnitTests.ViewModels;

[DeepMap(typeof(MapTreeLeafModel))]
public partial class MapTreeLeafViewModel : ObservableObject
{
    [ObservableProperty]
    [DeepMapProperty]
    public partial string Name { get; set; }
}
