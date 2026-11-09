using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using DeepExtensions;
using DeepExtensionsGeneratorUnitTests.Models;

namespace DeepExtensionsGeneratorUnitTests.ViewModels;

[DeepMap(typeof(MapTreeRootModel))]
public partial class MapTreeRootViewModel : ObservableObject
{
    [ObservableProperty]
    [DeepMapProperty]
    public partial MapTreeLeafViewModel Child { get; set; }

    [ObservableProperty]
    [DeepMapProperty]
    public partial MapTreeLeafViewModel Leaf { get; set; }

    [DeepMapProperty]
    public ObservableCollection<MapTreeLeafViewModel> Children { get; }

    [DeepMapProperty]
    public ObservableCollection<MapTreeLeafViewModel> Leaves { get; }
}
