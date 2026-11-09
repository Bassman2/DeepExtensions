using DeepExtensionsGeneratorUnitTests.Models;

namespace DeepExtensionsGeneratorUnitTests;

[TestClass]
public sealed class DeepEqualTests
{
    [TestMethod]
    public void DeepEqualTest()
    {
        var model = new EqualRootModel
        {
            BaseName = "Paul",
            BaseValue = 15.5,
            BaseLeaf = new EqualLeafModel { Name = "BaseLeaf", Value = 5.7 },
            BaseLeafNull = null,
            BaseLeaveList = [new() { Name = "BaseListLeafA", Value = 2.8 }, new() { Name = "BaseListLeafB", Value = 7.7 }],
            BaseLeaveArray = [new() { Name = "BaseArrayLeafA", Value = 9.35 }, new() { Name = "BaseArrayLeafB", Value = 3.15 }],
            Name = "Peter",
            Value = 10.5,
            Leaf = new EqualLeafModel { Name = "Leaf", Value = 5.5 },
            LeafNull = null,
            LeaveList = [new() { Name = "ListLeafA", Value = 2.5 }, new() { Name = "ListLeafB", Value = 7.3 }],
            LeaveArray = [new() { Name = "ArrayLeafA", Value = 9.5 }, new() { Name = "ArrayLeafB", Value = 3.3 }]
        };

        var copy = model.DeepCopy();

        Assert.IsTrue(model.IsDeepEqual(copy), "DeepEqual failed");
    }
}
