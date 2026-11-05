//#pragma warning disable IDE0017 



using DeepExtensionsGeneratorUnitTests.Models;

namespace DeepExtensionsGeneratorUnitTests;

[TestClass]
public sealed class DeepCopyTests
{
    [TestMethod]
    public void DeepCopyTest()
    {
        var model = new CopyBaseModel { 
            Name = "Peter",
            Value = 10.5,
            Leaf = new CopyLeafModel { Name = "Leaf", Value = 5.5 },
            LeafNull = null,
            LeaveList = [ new() { Name = "ListLeafA", Value = 2.5 }, new() { Name = "ListLeafB", Value = 7.3 }],
            LeaveArray = [new() { Name = "ArrayLeafA", Value = 9.5 }, new() { Name = "ArrayLeafB", Value = 3.3 }]
        };

        //var copy = model.DeepCopy();

        //Assert.AreEqual(model.Name, copy.Name, nameof(copy.Name));
        //Assert.AreEqual(model.Value, copy.Value, nameof(copy.Value));
        //Assert.AreEqual(model.Leaf?.Name, copy.Leaf?.Name, nameof(copy.Leaf));
        //Assert.AreEqual(model.LeafNull, copy.LeafNull, nameof(copy.LeafNull));
        //Assert.AreEqual(model.LeaveList.Count, copy.LeaveList.Count, nameof(copy.LeaveList));
        //Assert.AreEqual(model.LeaveArray.Length, copy.LeaveArray.Length, nameof(copy.LeaveArray));
    }
}

