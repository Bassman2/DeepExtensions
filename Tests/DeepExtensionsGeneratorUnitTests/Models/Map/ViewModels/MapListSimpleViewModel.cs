using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using DeepExtensions;
using DeepExtensionsGeneratorUnitTests.Models;

namespace DeepExtensionsGeneratorUnitTests.ViewModels;

[DeepMap(typeof(ListSimpleModel))]
public partial class MapListSimpleViewModel : ObservableObject
{
    [DeepMapProperty]
    public ObservableCollection<string> Names { get; }

    [DeepMapProperty]
    public ObservableCollection<string> Descriptions { get; }

    [DeepMapProperty]
    public ObservableCollection<int> Numbers { get; }

    [DeepMapProperty]
    public ObservableCollection<int> Nos { get; }
}
