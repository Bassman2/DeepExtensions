using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using DeepExtensions;
using DeepExtensionsGeneratorUnitTests.Models;

namespace DeepExtensionsGeneratorUnitTests.ViewModels;

[DeepMap(typeof(MapTreeRootModel))]
public partial class MapTreeRootDiffNameViewModel : ObservableObject
{
    [ObservableProperty]
    [DeepMapProperty("Child")]
    public partial MapTreeLeafViewModel DiffChild { get; set; }

    [ObservableProperty]
    [DeepMapProperty("Leaf")]
    public partial MapTreeLeafViewModel DiffLeaf { get; set; }

    [DeepMapProperty("Children")]
    public ObservableCollection<MapTreeLeafViewModel> DiffChildren { get; }

    [DeepMapProperty("Leaves")]
    public ObservableCollection<MapTreeLeafViewModel> DiffLeaves { get; }
}
