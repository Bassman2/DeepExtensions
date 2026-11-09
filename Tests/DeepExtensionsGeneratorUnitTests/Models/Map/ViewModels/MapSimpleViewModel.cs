using CommunityToolkit.Mvvm.ComponentModel;
using DeepExtensions;
using DeepExtensionsGeneratorUnitTests.Models;

namespace DeepExtensionsGeneratorUnitTests.ViewModels;

[DeepMap(typeof(MapSimpleModel))]
public partial class MapSimpleViewModel : ObservableObject
{
    [ObservableProperty]
    [DeepMapProperty]
    public partial string Name { get; set; }

    [ObservableProperty]
    [DeepMapProperty]
    public partial string? Description { get; set; }

    [ObservableProperty]
    [DeepMapProperty]
    public partial int Number { get; set; }

    [ObservableProperty]
    [DeepMapProperty]
    public partial int? No { get; set; }
}
