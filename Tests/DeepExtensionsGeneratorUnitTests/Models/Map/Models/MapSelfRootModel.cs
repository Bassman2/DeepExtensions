using System;
using System.Collections.Generic;
using System.Text;

namespace DeepExtensionsGeneratorUnitTests.Models;

public class MapSelfRootModel
{
    public List<MapSelfLeafModel> Children { get; set; } = new List<MapSelfLeafModel>();
}
