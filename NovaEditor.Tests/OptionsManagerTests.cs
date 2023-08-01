namespace NovaEditor.Tests;

/// <summary>The <see cref="OptionsManager"/> option tree generation tests.</summary>
public class OptionsManagerTests
{
    /*********
    ** Public Methods
    *********/
    [Test]
    public void OptionsManagerCalculateCategoryTree_AddOptionWithOneCategory_CategoryIsCreatedContainingType()
    {
        var optionTypeData = CalculateTypeData(typeof(Category_1_Group_1));
        var categoryTree = OptionsManager.CalculateCategoryTree(optionTypeData);

        Assert.That(categoryTree, Has.Length.EqualTo(1));

        PerformCategoryTests(categoryTree[0], Category_1_Group_1.CategoryName, 0, 1);
        PerformGroupTests(categoryTree[0].Groups[0], Category_1_Group_1.GroupName, new[] { typeof(Category_1_Group_1) });
    }

    [Test]
    public void OptionsManagerCalculateCategoryTree_AddOptionWithTwoCategories_NestedCategoriesAreCreatedContainingType()
    {
        var optionTypeData = CalculateTypeData(typeof(Category_1_Category_2_Group_1));
        var categoryTree = OptionsManager.CalculateCategoryTree(optionTypeData);

        Assert.That(categoryTree, Has.Length.EqualTo(1));
        var subCategories = categoryTree[0].SubCategories;
        Assert.That(subCategories, Has.Count.EqualTo(1));

        PerformCategoryTests(categoryTree[0], Category_1_Category_2_Group_1.Category1Name, 1, 0);
        PerformCategoryTests(subCategories[0], Category_1_Category_2_Group_1.Category2Name, 0, 1);
        PerformGroupTests(subCategories[0].Groups[0], Category_1_Category_2_Group_1.GroupName, new[] { typeof(Category_1_Category_2_Group_1) });
    }

    [Test]
    public void OptionsManagerCalculateCategoryTree_AddTwoOptionsWithSameCategoryDifferentGroups_CategoryIsCreatedContainingTypes()
    {
        var optionTypeData = CalculateTypeData(typeof(Category_1_Group_1), typeof(Category_1_Group_2));
        var categoryTree = OptionsManager.CalculateCategoryTree(optionTypeData);

        Assert.That(categoryTree, Has.Length.EqualTo(1));

        PerformCategoryTests(categoryTree[0], Category_1_Group_1.CategoryName, 0, 2);
        PerformGroupTests(categoryTree[0].Groups[0], Category_1_Group_1.GroupName, new[] { typeof(Category_1_Group_1) });
        PerformGroupTests(categoryTree[0].Groups[1], Category_1_Group_2.GroupName, new[] { typeof(Category_1_Group_2) });
    }
    
    [Test]
    public void OptionsManagerCalculateCategoryTree_AddTwoOptionsWithSameCategorySameGroups_CategoryIsCreatedContainingTypes()
    {
        var optionTypeData = CalculateTypeData(typeof(Category_1_Group_1), typeof(Category_1_Group_1_2));
        var categoryTree = OptionsManager.CalculateCategoryTree(optionTypeData);

        Assert.That(categoryTree, Has.Length.EqualTo(1));

        PerformCategoryTests(categoryTree[0], Category_1_Group_1.CategoryName, 0, 1);
        PerformGroupTests(categoryTree[0].Groups[0], Category_1_Group_1.GroupName, new[] { typeof(Category_1_Group_1), typeof(Category_1_Group_1_2) });
    }

    [Test]
    public void OptionsManagerCalculateCategoryTree_AddTwoOptionsWithDifferentCategories_CategoriesAreCreatedContainingTypes()
    {
        var optionTypeData = CalculateTypeData(typeof(Category_1_Group_1), typeof(Category_2_Group_1));
        var categoryTree = OptionsManager.CalculateCategoryTree(optionTypeData);

        Assert.That(categoryTree, Has.Length.EqualTo(2));

        PerformCategoryTests(categoryTree[0], Category_1_Group_1.CategoryName, 0, 1);
        PerformCategoryTests(categoryTree[1], Category_2_Group_1.CategoryName, 0, 1);
        PerformGroupTests(categoryTree[0].Groups[0], Category_1_Group_1.GroupName, new[] { typeof(Category_1_Group_1) });
        PerformGroupTests(categoryTree[1].Groups[0], Category_2_Group_1.GroupName, new[] { typeof(Category_2_Group_1) });
    }


    /*********
    ** Private Methods
    *********/
    /// <summary>Creates a dictionary of types and their attributes used for creating an options tree.</summary>
    /// <param name="optionTypes">The types to use to create the dictionary.</param>
    /// <returns>A dictionary to use when creating an options tree.</returns>
    private Dictionary<Type, OptionsAttribute> CalculateTypeData(params Type[] optionTypes) =>
        optionTypes.ToDictionary(
            optionType => optionType,
            optionType => (OptionsAttribute)optionType.GetCustomAttributes(typeof(OptionsAttribute), false).First());

    /// <summary>Performs assertions to ensure a category is valid.</summary>
    /// <param name="category">The category to check.</param>
    /// <param name="expectedCategoryName">The name the category should have.</param>
    /// <param name="expectedSubCategoryCount">The number of sub-categories the category should have.</param>
    /// <param name="expectedGroupCount">The number of groups the category should have.</param>
    private void PerformCategoryTests(OptionsCategory category, string expectedCategoryName, int expectedSubCategoryCount, int expectedGroupCount) =>
        Assert.Multiple(() =>
        {
            Assert.That(category.Name, Is.EqualTo(expectedCategoryName));
            Assert.That(category.SubCategories, Has.Count.EqualTo(expectedSubCategoryCount));
            Assert.That(category.Groups, Has.Count.EqualTo(expectedGroupCount));
        });

    /// <summary>Performs assertions to ensure a group is valid.</summary>
    /// <param name="group">The group to check.</param>
    /// <param name="expectedGroupName">The name the group should have.</param>
    /// <param name="expectedGroupTypes">The types the group should have.</param>
    private void PerformGroupTests(OptionsGroup group, string expectedGroupName, Type[] expectedGroupTypes) =>
        Assert.Multiple(() =>
        {
            Assert.That(group.Name, Is.EqualTo(expectedGroupName));
            Assert.That(group.TempTypes, Is.EquivalentTo(expectedGroupTypes));
        });
}

[Options(new[] { CategoryName }, GroupName)]
file sealed class Category_1_Group_1
{
    public const string CategoryName = "category 1";
    public const string GroupName = "group 1";
}

[Options(new[] { CategoryName }, GroupName)]
file sealed class Category_1_Group_1_2
{
    public const string CategoryName = "category 1";
    public const string GroupName = "group 1";
}

[Options(new[] { CategoryName }, GroupName)]
file sealed class Category_1_Group_2
{
    public const string CategoryName = "category 1";
    public const string GroupName = "group 2";
}

[Options(new[] { Category1Name, Category2Name }, GroupName)]
file sealed class Category_1_Category_2_Group_1
{
    public const string Category1Name = "category 1";
    public const string Category2Name = "category 1";
    public const string GroupName = "group 1";
}

[Options(new[] { CategoryName }, GroupName)]
file sealed class Category_2_Group_1
{
    public const string CategoryName = "category 2";
    public const string GroupName = "group 1";
}
