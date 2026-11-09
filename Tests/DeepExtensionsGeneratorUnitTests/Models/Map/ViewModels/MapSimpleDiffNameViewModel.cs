using CommunityToolkit.Mvvm.ComponentModel;
using DeepExtensions;
using DeepExtensionsGeneratorUnitTests.Models;

namespace DeepExtensionsGeneratorUnitTests.ViewModels;

[DeepMap(typeof(MapSimpleModel))]
public partial class MapSimpleDiffNameViewModel : ObservableObject
{
    [ObservableProperty]
    [DeepMapProperty("Name")]
    public partial string DiffName { get; set; }

    [ObservableProperty]
    [DeepMapProperty("Description")]
    public partial string? DiffDescription { get; set; }

    [ObservableProperty]
    [DeepMapProperty("Number")]
    public partial int DiffNumber { get; set; }

    [ObservableProperty]
    [DeepMapProperty("No")]
    public partial int? DiffNo { get; set; }
}
