namespace DeepExtensionsGeneratorUnitTests;

[TestClass]
public sealed class DeepMapTests
{
    /*
    #region Simple

    readonly string Name = "New Name";
    readonly string Description = "New Description";
    readonly int Number = 15;
    readonly int No = 20;
    
    [TestMethod]
    public void SimpleTest()
    {
        var model = new SimpleModel { Name = "Test Name", Description = "Test description", Number = 5, No = 10 };
        var viewModel = new SimpleViewModel(model);

        Assert.AreEqual(model.Name, viewModel.Name, nameof(viewModel.Name));
        Assert.AreEqual(model.Description, viewModel.Description, nameof(viewModel.Description));
        Assert.AreEqual(model.Number, viewModel.Number, nameof(viewModel.Number));
        Assert.AreEqual(model.No, viewModel.No, nameof(viewModel.No));

        viewModel.Name = Name;
        viewModel.Description = Description;
        viewModel.Number = Number;
        viewModel.No = No;

        Assert.AreEqual(Name, model.Name, nameof(model.Name));
        Assert.AreEqual(Description, model.Description, nameof(model.Description));
        Assert.AreEqual(Number, model.Number, nameof(model.Number));
        Assert.AreEqual(No, model.No, nameof(model.No));
    }

    [TestMethod]
    public void SimpleDiffNameTest()
    {
        var model = new SimpleModel { Name = "Test Name", Description = "Test description", Number = 5, No = 10 };
        var viewModel = new SimpleDiffNameViewModel(model);

        Assert.AreEqual(model.Name, viewModel.DiffName, nameof(viewModel.DiffName));
        Assert.AreEqual(model.Description, viewModel.DiffDescription, nameof(viewModel.DiffDescription));
        Assert.AreEqual(model.Number, viewModel.DiffNumber, nameof(viewModel.DiffNumber));
        Assert.AreEqual(model.No, viewModel.DiffNo, nameof(viewModel.DiffNo));

        viewModel.DiffName = Name;
        viewModel.DiffDescription = Description;
        viewModel.DiffNumber = Number;
        viewModel.DiffNo = No;

        Assert.AreEqual(Name, model.Name, nameof(model.Name));
        Assert.AreEqual(Description, model.Description, nameof(model.Description));
        Assert.AreEqual(Number, model.Number, nameof(model.Number));
        Assert.AreEqual(No, model.No, nameof(model.No));
    }

    #endregion

    #region List Simple

    readonly List<string> Names = ["Peter", "Paul"];
    readonly List<string> Descriptions = ["Butcher", "Baker"];
    readonly List<int> Numbers = [15, 20, 30];
    readonly List<int> Nos = [20, 30, 40];

    [TestMethod]
    public void ListSimpleTest()
    {
        var model = new ListSimpleModel { Names = ["Test Name"], Descriptions = ["Test description"], Numbers = [5], Nos = [10] };
        var viewModel = new ListSimpleViewModel(model);

        CollectionAssert.AreEqual(model.Names, viewModel.Names, nameof(viewModel.Names));
        CollectionAssert.AreEqual(model.Descriptions, viewModel.Descriptions, nameof(viewModel.Descriptions));
        CollectionAssert.AreEqual(model.Numbers, viewModel.Numbers, nameof(viewModel.Numbers));
        CollectionAssert.AreEqual(model.Nos, viewModel.Nos, nameof(viewModel.Nos));

        ReplaceAll(viewModel.Names, Names);
        ReplaceAll(viewModel.Descriptions, Descriptions);
        ReplaceAll(viewModel.Numbers, Numbers);
        ReplaceAll(viewModel.Nos, Nos);

        CollectionAssert.AreEqual(Names, model.Names, nameof(model.Names));
        CollectionAssert.AreEqual(Descriptions, model.Descriptions, nameof(model.Descriptions));
        CollectionAssert.AreEqual(Numbers, model.Numbers, nameof(model.Numbers));
        CollectionAssert.AreEqual(Nos, model.Nos, nameof(model.Nos));
    }

    [TestMethod]
    public void ListSimpleDiffNameTest()
    {
        var model = new ListSimpleModel { Names = ["Test Name"], Descriptions = ["Test description"], Numbers = [5], Nos = [10] };
        var viewModel = new ListSimpleDiffNameViewModel(model);

        CollectionAssert.AreEqual(model.Names, viewModel.DiffNames, nameof(viewModel.DiffNames));
        CollectionAssert.AreEqual(model.Descriptions, viewModel.DiffDescriptions, nameof(viewModel.DiffDescriptions));
        CollectionAssert.AreEqual(model.Numbers, viewModel.DiffNumbers, nameof(viewModel.DiffNumbers));
        CollectionAssert.AreEqual(model.Nos, viewModel.DiffNos, nameof(viewModel.DiffNos));

        ReplaceAll(viewModel.DiffNames, Names);
        ReplaceAll(viewModel.DiffDescriptions, Descriptions);
        ReplaceAll(viewModel.DiffNumbers, Numbers);
        ReplaceAll(viewModel.DiffNos, Nos);

        CollectionAssert.AreEqual(Names, model.Names, nameof(model.Names));
        CollectionAssert.AreEqual(Descriptions, model.Descriptions, nameof(model.Descriptions));
        CollectionAssert.AreEqual(Numbers, model.Numbers, nameof(model.Numbers));
        CollectionAssert.AreEqual(Nos, model.Nos, nameof(model.Nos));
    }

    #endregion

    #region Tree

    readonly string initChildName = "Peter";
    readonly string testChildName = "Paul";

    //readonly string initLeafName = "Mary";
    readonly string testLeafName = "Paris";

    readonly List<TreeLeafModel> initChildrenNames = [new TreeLeafModel("Peter"), new TreeLeafModel("Mary")];
    readonly List<TreeLeafModel> testChildrenNames = [new TreeLeafModel("Paul"), new TreeLeafModel("Paris")];

    //readonly List<TreeLeafModel> initLeavesNames = [new TreeLeafModel("A1"), new TreeLeafModel("A2")];
    readonly List<TreeLeafModel> testLeavesNames = [new TreeLeafModel("B1"), new TreeLeafModel("B2")];

    [TestMethod]
    public void TreeTest()
    {
        var model = new TreeRootModel {
            Child = new TreeLeafModel(initChildName),
            Leaf = null,
            Children = initChildrenNames,
            Leaves = null};
        var viewModel = new TreeRootViewModel(model);

        Assert.AreEqual(initChildName, viewModel.Child.Name, nameof(viewModel.Child));
        Assert.AreEqual(string.Empty, viewModel.Leaf.Name, nameof(viewModel.Leaf));
        CollectionAssert.AreEqual(initChildrenNames.Select(i => i.Name).ToList(), viewModel.Children.Select(i => i.Name).ToList(), nameof(viewModel.Children));
        CollectionAssert.AreEqual(Array.Empty<string>(), viewModel.Leaves.Select(i => i.Name).ToList(), nameof(viewModel.Leaves));

        viewModel.Child.Name = testChildName;
        viewModel.Leaf.Name = testLeafName;
        ReplaceAll(viewModel.Children, testChildrenNames.Select(i => new TreeLeafViewModel(i)));
        ReplaceAll(viewModel.Leaves, testLeavesNames.Select(i => new TreeLeafViewModel(i)));
        
        Assert.AreEqual(testChildName, model.Child.Name, nameof(model.Child));
        Assert.AreEqual(testLeafName, model.Leaf!.Name, nameof(model.Leaf));
        CollectionAssert.AreEqual(testChildrenNames, model.Children, nameof(model.Children));
        CollectionAssert.AreEqual(testLeavesNames, model.Leaves, nameof(model.Leaves));
    }

    [TestMethod]
    public void TreeDiffNameTest()
    {
        var model = new TreeRootModel
        {
            Child = new TreeLeafModel(initChildName),
            Leaf = null,
            Children = initChildrenNames,
            Leaves = null
        };
        var viewModel = new TreeRootDiffNameViewModel(model);

        Assert.AreEqual(initChildName, viewModel.DiffChild.Name, nameof(viewModel.DiffChild));
        Assert.AreEqual(string.Empty, viewModel.DiffLeaf.Name, nameof(viewModel.DiffLeaf));
        CollectionAssert.AreEqual(initChildrenNames.Select(i => i.Name).ToList(), viewModel.DiffChildren.Select(i => i.Name).ToList(), nameof(viewModel.DiffChildren));
        CollectionAssert.AreEqual(Array.Empty<string>(), viewModel.DiffLeaves.Select(i => i.Name).ToList(), nameof(viewModel.DiffLeaves));

        viewModel.DiffChild.Name = testChildName;
        viewModel.DiffLeaf.Name = testLeafName;
        ReplaceAll(viewModel.DiffChildren, testChildrenNames.Select(i => new TreeLeafViewModel(i)));
        ReplaceAll(viewModel.DiffLeaves, testLeavesNames.Select(i => new TreeLeafViewModel(i)));

        Assert.AreEqual(testChildName, model.Child.Name, nameof(model.Child));
        Assert.AreEqual(testLeafName, model.Leaf!.Name, nameof(model.Leaf));
        CollectionAssert.AreEqual(testChildrenNames, model.Children, nameof(model.Children));
        CollectionAssert.AreEqual(testLeavesNames, model.Leaves, nameof(model.Leaves));


    }

    #endregion

    #region Converter Func

    [TestMethod]
    public void ConverterFuncTest()
    {
        var model = new ConverterModel { Value = 5 };
        var viewModel = new ConverterFuncViewModel(model);

        Assert.AreEqual(model.Value.ToString(), viewModel.Value, nameof(viewModel.Value));

        viewModel.Value = "20";

        Assert.AreEqual(20, model.Value, nameof(model.Value));
    }

    [TestMethod]
    public void ConverterFuncDiffNameTest()
    {
        var model = new ConverterModel { Value = 5 };
        var viewModel = new ConverterFuncDiffNameViewModel(model);

        Assert.AreEqual(model.Value.ToString(), viewModel.DiffValue, nameof(viewModel.DiffValue));

        viewModel.DiffValue = "20";

        Assert.AreEqual(20, model.Value, nameof(model.Value));
    }
    #endregion

    #region Converter Class

    [TestMethod]
    public void ConverterClassTest()
    {
        var model = new ConverterModel { Value = 5 };
        var viewModel = new ConverterClassViewModel(model);

        Assert.AreEqual(model.Value.ToString(), viewModel.Value, nameof(viewModel.Value));

        viewModel.Value = "20";

        Assert.AreEqual(20, model.Value, nameof(model.Value));
    }

    [TestMethod]
    public void ConverterClassDiffNameTest()
    {
        var model = new ConverterModel { Value = 5 };
        var viewModel = new ConverterClassDiffNameViewModel(model);

        Assert.AreEqual(model.Value.ToString(), viewModel.DiffValue, nameof(viewModel.DiffValue));

        viewModel.DiffValue = "20";

        Assert.AreEqual(20, model.Value, nameof(model.Value));
    }
    #endregion

    #region Helper

    private static void ReplaceAll<T>(ObservableCollection<T> collection, IEnumerable<T> items)
    {
        collection.Clear();
        foreach (var item in items)
        {
            collection.Add(item);
        }
    }

    private static void ReplaceAll<T>(ObservableCollection<T> collection, params T[] items)
    {
        collection.Clear();
        foreach (var item in items)
        {
            collection.Add(item);
        }
    }

    #endregion
    */
}
