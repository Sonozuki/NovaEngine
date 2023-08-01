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
        var optionTypeData = CalculateTypeData(typeof(Category_1));
        var categoryTree = OptionsManager.CalculateCategoryTree(optionTypeData);

        Assert.That(categoryTree, Has.Length.EqualTo(1));
        PerformCategoryTests(categoryTree[0], Category_1.CategoryName, 0);
    }

    [Test]
    public void OptionsManagerCalculateCategoryTree_AddOptionWithTwoCategories_NestedCategoriesAreCreatedContainingType()
    {
        var optionTypeData = CalculateTypeData(typeof(Category_1_Category_2));
        var categoryTree = OptionsManager.CalculateCategoryTree(optionTypeData);

        Assert.That(categoryTree, Has.Length.EqualTo(1));
        var subCategory = categoryTree[0].SubCategories;
        Assert.That(subCategory, Has.Count.EqualTo(1));

        PerformCategoryTests(categoryTree[0], Category_1_Category_2.Category1Name, 1);
        PerformCategoryTests(subCategory[0], Category_1_Category_2.Category2Name, 0);
    }

    [Test]
    public void OptionsManagerCalculateCategoryTree_AddTwoOptionsWithSameCategories_CategoryIsCreatedContainingTypes()
    {
        var optionTypeData = CalculateTypeData(typeof(Category_1), typeof(Category_1_2));
        var categoryTree = OptionsManager.CalculateCategoryTree(optionTypeData);

        Assert.That(categoryTree, Has.Length.EqualTo(1));

        PerformCategoryTests(categoryTree[0], Category_1.CategoryName, 0);
    }

    [Test]
    public void OptionsManagerCalculateCategoryTree_AddTwoOptionsWithDifferentCategories_CategoriesAreCreatedContainingTypes()
    {
        var optionTypeData = CalculateTypeData(typeof(Category_1), typeof(Category_2));
        var categoryTree = OptionsManager.CalculateCategoryTree(optionTypeData);

        Assert.That(categoryTree, Has.Length.EqualTo(2));

        PerformCategoryTests(categoryTree[0], Category_1.CategoryName, 0);
        PerformCategoryTests(categoryTree[1], Category_2.CategoryName, 0);
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
    private void PerformCategoryTests(OptionsCategory category, string expectedCategoryName, int expectedSubCategoryCount) =>
        Assert.Multiple(() =>
        {
            Assert.That(category.Name, Is.EqualTo(expectedCategoryName));
            Assert.That(category.SubCategories, Has.Count.EqualTo(expectedSubCategoryCount));
        });
}

[Options(new[] { CategoryName })]
file sealed class Category_1
{
    public const string CategoryName = "category 1";
}

[Options(new[] { CategoryName })]
file sealed class Category_1_2
{
    public const string CategoryName = "category 1";
}

[Options(new[] { CategoryName })]
file sealed class Category_1_Group_2
{
    public const string CategoryName = "category 1";
}

[Options(new[] { Category1Name, Category2Name })]
file sealed class Category_1_Category_2
{
    public const string Category1Name = "category 1";
    public const string Category2Name = "category 2";
}

[Options(new[] { CategoryName })]
file sealed class Category_2
{
    public const string CategoryName = "category 2";
}
