# Deep Extentions

The Deep Extentions are a collection of three Deep features based on Source Generation.

# Features
* Roslyn Code Generator
* AOT-compatible
* Trimmable
* CommunityToolkit.Mvvm compatible.
* WPF, WinUI, and Avalonia compatible.

# Installation

[Nuget package](https://www.nuget.org/packages/DeepExtensions)


# DeepCopy

Generates code for deep copy.

## Example

```
namespace Demo.Models;

[DeepCopy]
public partial class DemoModel
{
    public string Name { get; set; } = "Peter";

    public SubModel Sub { get; set; } = new SubModel();

    [DeepCopyIgnore]
    public int Num { get; set; } = 45;
}

// Generated

partial class DemoModel : IDeepCopy<DemoModel>
{
    public DemoModel DeepCopy()
    {
        return DemoModel()
        {    
            Name = this.Name;
            Sub = this.Sub.DeepCopy()
        }
    }
}
```

# DeepEqual

Generates code for deep equal.

## Example

```
namespace Demo.Models;

[DeepEqual]
public partial class DemoModel
{
    public string Name { get; set; } = "Peter";

    public SubModel Sub { get; set; } = new SubModel();

    [DeepEqualIgnore]
    public int Num { get; set; } = 45;
}

// Generated

partial class DemoModel : IDeepEqual<DemoModel>
{
    public bool DeepEqual(DemoModel? value)
    {
        if (value is null) return false;
        return
            Name == value.Name &&
            Sub.DeepEqual(value.Sub);
    }
}
```

## DeepMap 
Mapping between Model and ViewModel trees
* Custom converters

### Examples
#### Simple Types
Synchronizing properties with simple data types between the Model and the ViewModel. 
##### Model
```
namespace Demo.Models;

public class DemoModel
{
    public string Name { get; set; } = "Peter";
}
```
#### ViewModel
```
using CommunityToolkit.Mvvm.ComponentModel;
using MvvmToolbox.ComponentModel;
using ObservableModelGeneratorUnitTests.Models;

namespace Demo.ViewModels;

[ObservableModelObject(typeof(DemoModel))]
public partial class DemoViewModel : ObservableObject
{
    [ObservableProperty]
    [ObservableModelProperty]
    public partial string Name { get; set; }
}
```
## List of Simple Types
Synchronizing properties with a list of simple data types between the Model and the ViewModel.
#### Model
```
namespace Demo.Models;

public class DemoModel
{
    public List<string> Names { get; set; } = [ "Peter", "Paul", "Mary"];
}
```
#### ViewModel
```
using CommunityToolkit.Mvvm.ComponentModel;
using MvvmToolbox.ComponentModel;
using ObservableModelGeneratorUnitTests.Models;

namespace Demo.ViewModels;

[ObservableModelObject(typeof(DemoModel))]
public partial class DemoViewModel : ObservableObject
{
    // no partial, no set, no nullable, and no [ObservableProperty]
    [ObservableModelProperty]
    public ObservableCollection<string> Names { get; }
}
```
## Different Names
Synchronizing properties with different names between Model and ViewModel.
#### Model
```
namespace Demo.Models;

public class DemoModel
{
    public string Name { get; set; } = "Peter";
}
```
#### ViewModel
```
using CommunityToolkit.Mvvm.ComponentModel;
using MvvmToolbox.ComponentModel;
using ObservableModelGeneratorUnitTests.Models;

namespace Demo.ViewModels;

[ObservableModelObject(typeof(DemoModel))]
public partial class DemoViewModel : ObservableObject
{
    [ObservableProperty]
    [ObservableModelProperty(nameof(DemoModel.Name))]
    public partial string FirstName { get; set; }
}
```
## Children
Synchronizing properties with children between Model and ViewModel.
#### Model
```
namespace Demo.Models;

public class RootModel
{
    public LeafModel? Leaf { get; set; } = new LeafModel();
}

public class LeafModel
{
    public string Name { get; set; } = "Peter";
}
```
#### ViewModel
```
using CommunityToolkit.Mvvm.ComponentModel;
using MvvmToolbox.ComponentModel;
using ObservableModelGeneratorUnitTests.Models;

namespace Demo.ViewModels;

[ObservableModelObject(typeof(RootModel))]
public partial class RootViewModel : ObservableObject
{
    [ObservableProperty]
    [ObservableModelProperty]
    public partial LeafViewModel? Leaf { get; set; }
}

[ObservableModelObject(typeof(LeafModel))]
public partial class LeafViewModel : ObservableObject
{
    [ObservableProperty]
    [ObservableModelProperty]
    public partial string Name { get; set; }
}
```
## List of Children
Synchronizing properties with a list of children between Model and ViewModel.
#### Model
```
namespace Demo.Models;

public class RootModel
{
    public List<LeafModel> Leafs { get; set; } = [new LeafModel(), new LeafModel()];
}

public class LeafModel
{
    public string Name { get; set; } = "Peter";
}
```
#### ViewModel
```
using CommunityToolkit.Mvvm.ComponentModel;
using MvvmToolbox.ComponentModel;
using ObservableModelGeneratorUnitTests.Models;

namespace Demo.ViewModels;

[ObservableModelObject(typeof(RootModel))]
public partial class RootViewModel : ObservableObject
{
    // no partial, no set, no nullable, and no [ObservableProperty]
    [ObservableModelProperty]
    public ObservableCollection<LeafViewModel> Leafs { get; }
}

[ObservableModelObject(typeof(LeafModel))]
public partial class LeafViewModel : ObservableObject
{
    [ObservableProperty]
    [ObservableModelProperty]
    public partial string Name { get; set; }
}
```
## Converter Functions
Synchronizing properties with converters between the Model and the ViewModel.
The converters can also be applied to lists and "list to simpleType" mappings.
#### Model
```
namespace Demo.Models;

public class DemoModel
{
    public int Num { get; set; } = 25;
}
```
#### ViewModel
```
using CommunityToolkit.Mvvm.ComponentModel;
using MvvmToolbox.ComponentModel;
using ObservableModelGeneratorUnitTests.Models;

namespace Demo.ViewModels;

[ObservableModelObject(typeof(DemoModel))]
public partial class DemoViewModel : ObservableObject
{
    [ObservableProperty]
    [ObservableModelProperty(Converter = "NumConvert")]
    public partial string Num { get; set; }

    private string GetNumConvert(int val) => val.ToString();
    private int SetNumConvert(string val) => int.Parse(val);
}
```

## Converter Interface
Synchronizing properties with converters between the Model and the ViewModel.
The converters can also be applied to lists and "list to simpleType" mappings.
#### Model
```
namespace Demo.Models;

public class DemoModel
{
    public int Num { get; set; } = 25;
}
```
#### ViewModel
```
using CommunityToolkit.Mvvm.ComponentModel;
using MvvmToolbox.ComponentModel;
using ObservableModelGeneratorUnitTests.Models;

namespace Demo.ViewModels;

[ObservableModelObject(typeof(DemoModel))]
public partial class DemoViewModel : ObservableObject
{
    [ObservableProperty]
    [ObservableModelProperty(ConverterType = typeof(IntToStringConverter))]
    public partial string Num { get; set; }

    private string GetNumConvert(int val) => val.ToString();
    private int SetNumConvert(string val) => int.Parse(val);

    public class IntToStringConverter : IPropertyConverter
    {
        public object Convert(object value) => value?.ToString()!;
        public object ConvertBack(object value) => (value is string input && int.TryParse(input, out int result)) ? result : null!;
    }
}
``` 
