using CommunityToolkit.Mvvm.ComponentModel;
using DeepExtensions;
using DeepExtensionsGeneratorUnitTests.Models;

namespace DeepExtensionsGeneratorUnitTests.ViewModels;

[DeepMap(typeof(MapConverterModel))]
public partial class MapConverterFuncDiffNameViewModel : ObservableObject
{
    [ObservableProperty]
    [DeepMapProperty("Value", Converter = "NumConvert")]
    public partial string DiffValue { get; set; }

    private static string GetNumConvert(int value) => value.ToString();
    private static int SetNumConvert(string value) => int.Parse(value);
}

