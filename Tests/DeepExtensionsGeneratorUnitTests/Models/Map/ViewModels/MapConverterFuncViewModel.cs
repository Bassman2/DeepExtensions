using CommunityToolkit.Mvvm.ComponentModel;
using DeepExtensions;
using DeepExtensionsGeneratorUnitTests.Models;

namespace DeepExtensionsGeneratorUnitTests.ViewModels;

[DeepMap(typeof(MapConverterModel))]
public partial class MapConverterFuncViewModel : ObservableObject
{
    [ObservableProperty]
    [DeepMapProperty(Converter = "NumConvert")]
    public partial string Value { get; set; }

    private static string GetNumConvert(int value) => value.ToString();
    private static int SetNumConvert(string value) => int.Parse(value);
}

