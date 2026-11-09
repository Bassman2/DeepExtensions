using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using DeepExtensions;
using DeepExtensionsGeneratorUnitTests.Models;

namespace DeepExtensionsGeneratorUnitTests.ViewModels;

[DeepMap(typeof(ListSimpleModel))]
public partial class MapListSimpleDiffNameViewModel : ObservableObject
{
    [DeepMapProperty("Names")]
    public ObservableCollection<string> DiffNames { get; }

    [DeepMapProperty("Descriptions")]
    public ObservableCollection<string> DiffDescriptions { get; }

    [DeepMapProperty("Numbers")]
    public ObservableCollection<int> DiffNumbers { get; }

    [DeepMapProperty("Nos")]
    public ObservableCollection<int> DiffNos { get; }
}
