using CommunityToolkit.Mvvm.ComponentModel;
using DeepExtensions;
using DeepExtensionsGeneratorUnitTests.Models;

namespace DeepExtensionsGeneratorUnitTests.ViewModels;

[DeepMap(typeof(MapConverterModel))]
public partial class MapConverterClassViewModel : ObservableObject
{
    [ObservableProperty]
    [DeepMapProperty(ConverterType = typeof(IntToStringConverter))]
    public partial string Value { get; set; }

    public class IntToStringConverter : IDeepMapPropertyConverter
    {
        public object Convert(object value) => value?.ToString()!;
        public object ConvertBack(object value) => (value is string input && int.TryParse(input, out int result)) ? result : null!;
    }
}

