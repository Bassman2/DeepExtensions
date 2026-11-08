namespace DeepExtensionsGeneratorUnitTests;

[TestClass]
public sealed class DeepCopyTests
{
    [TestMethod]
    public void DeepCopyTest()
    {
        var model = new CopyRootModel {
            BaseName = "Paul",
            BaseValue = 15.5,
            BaseLeaf = new CopyLeafModel { Name = "BaseLeaf", Value = 5.7 },
            BaseLeafNull = null,
            BaseLeaveList = [new() { Name = "BaseListLeafA", Value = 2.8 }, new() { Name = "BaseListLeafB", Value = 7.7 }],
            BaseLeaveArray = [new() { Name = "BaseArrayLeafA", Value = 9.35 }, new() { Name = "BaseArrayLeafB", Value = 3.15 }],
            Name = "Peter",
            Value = 10.5,
            Leaf = new CopyLeafModel { Name = "Leaf", Value = 5.5 },
            LeafNull = null,
            LeaveList = [ new() { Name = "ListLeafA", Value = 2.5 }, new() { Name = "ListLeafB", Value = 7.3 }],
            LeaveArray = [new() { Name = "ArrayLeafA", Value = 9.5 }, new() { Name = "ArrayLeafB", Value = 3.3 }]
        };

        var copy = model.DeepCopy();

        Assert.AreEqual(model.BaseName, copy.BaseName, nameof(copy.BaseName));
        Assert.AreEqual(model.BaseValue, copy.BaseValue, nameof(copy.BaseValue));
        Assert.AreEqual(model.BaseLeaf?.Name, copy.BaseLeaf?.Name, nameof(copy.BaseLeaf));
        Assert.AreEqual(model.BaseLeafNull, copy.BaseLeafNull, nameof(copy.BaseLeafNull));
        Assert.HasCount(model.BaseLeaveList.Count, copy.BaseLeaveList, nameof(copy.BaseLeaveList));
        Assert.HasCount(model.BaseLeaveArray.Length, copy.BaseLeaveArray, nameof(copy.BaseLeaveArray));
        Assert.AreEqual(model.Name, copy.Name, nameof(copy.Name));
        Assert.AreEqual(model.Value, copy.Value, nameof(copy.Value));
        Assert.AreEqual(model.Leaf?.Name, copy.Leaf?.Name, nameof(copy.Leaf));
        Assert.AreEqual(model.LeafNull, copy.LeafNull, nameof(copy.LeafNull));
        Assert.HasCount(model.LeaveList.Count, copy.LeaveList, nameof(copy.LeaveList));
        Assert.HasCount(model.LeaveArray.Length, copy.LeaveArray, nameof(copy.LeaveArray));
    }
}

