namespace NovaEditor.Tests;

/// <summary>The <see cref="OptionsManager"/> tests.</summary>
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

    [Test]
    public void OptionsManagerCalculateOptions_OptionWithPublicField_OptionIsCreated()
    {
        var options = OptionsManager.CalculateOptions(typeof(PublicFieldOption));
        Assert.That(options, Has.Length.EqualTo(1));

        Assert.Multiple(() =>
        {
            Assert.That(options[0].Type, Is.SameAs(typeof(string)));
            Assert.That(options[0].Text, Is.EqualTo(PublicFieldOption.OptionText));
        });
    }
    
    [Test]
    public void OptionsManagerCalculateOptions_OptionWithPrivateField_OptionIsCreated()
    {
        var options = OptionsManager.CalculateOptions(typeof(PrivateFieldOption));
        Assert.That(options, Has.Length.EqualTo(1));

        Assert.Multiple(() =>
        {
            Assert.That(options[0].Type, Is.SameAs(typeof(string)));
            Assert.That(options[0].Text, Is.EqualTo(PrivateFieldOption.OptionText));
        });
    }

    [Test]
    public void OptionsManagerCalculateOptions_OptionWithPublicGetSetProperty_OptionIsCreated()
    {
        var options = OptionsManager.CalculateOptions(typeof(PublicPropertyGetSetOption));
        Assert.That(options, Has.Length.EqualTo(1));

        Assert.Multiple(() =>
        {
            Assert.That(options[0].Type, Is.SameAs(typeof(string)));
            Assert.That(options[0].Text, Is.EqualTo(PublicPropertyGetSetOption.OptionText));
        });
    }
    
    [Test]
    public void OptionsManagerCalculateOptions_OptionWithPrivateGetSetProperty_OptionIsCreated()
    {
        var options = OptionsManager.CalculateOptions(typeof(PrivatePropertyGetSetOption));
        Assert.That(options, Has.Length.EqualTo(1));

        Assert.Multiple(() =>
        {
            Assert.That(options[0].Type, Is.SameAs(typeof(string)));
            Assert.That(options[0].Text, Is.EqualTo(PrivatePropertyGetSetOption.OptionText));
        });
    }

    [Test]
    public void OptionsManagerCalculateOptions_OptionWithGetProperty_OptionIsNotCreated()
    {
        var options = OptionsManager.CalculateOptions(typeof(PropertyGetOption));
        Assert.That(options, Has.Length.Zero);
    }

    [Test]
    public void OptionsManagerCalculateOptions_TwoFieldOptions_OptionsAreCreated()
    {
        var options = OptionsManager.CalculateOptions(typeof(TwoFieldOptions));
        Assert.That(options, Has.Length.EqualTo(2));

        Assert.Multiple(() =>
        {
            Assert.That(options[0].Type, Is.SameAs(typeof(string)));
            Assert.That(options[0].Text, Is.EqualTo(TwoFieldOptions.Option1Text));

            Assert.That(options[1].Type, Is.SameAs(typeof(int)));
            Assert.That(options[1].Text, Is.EqualTo(TwoFieldOptions.Option2Text));
        });
    }

    [Test]
    public void OptionsManagerCalculateOptions_TwoPropertyOptions_OptionsAreCreated()
    {
        var options = OptionsManager.CalculateOptions(typeof(TwoPropertyOptions));
        Assert.That(options, Has.Length.EqualTo(2));

        Assert.Multiple(() =>
        {
            Assert.That(options[0].Type, Is.SameAs(typeof(string)));
            Assert.That(options[0].Text, Is.EqualTo(TwoPropertyOptions.Option1Text));

            Assert.That(options[1].Type, Is.SameAs(typeof(int)));
            Assert.That(options[1].Text, Is.EqualTo(TwoPropertyOptions.Option2Text));
        });
    }

    [Test]
    public void OptionsManagerCalculateOptions_OneFieldOnePropertyOption_OptionsAreCreated()
    {
        var options = OptionsManager.CalculateOptions(typeof(FieldPropertyOptions));
        Assert.That(options, Has.Length.EqualTo(2));

        Assert.Multiple(() =>
        {
            Assert.That(options[0].Type, Is.SameAs(typeof(string)));
            Assert.That(options[0].Text, Is.EqualTo(FieldPropertyOptions.Option1Text));

            Assert.That(options[1].Type, Is.SameAs(typeof(int)));
            Assert.That(options[1].Text, Is.EqualTo(FieldPropertyOptions.Option2Text));
        });
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

[Options(new[] { "asd" })]
file sealed class PublicFieldOption
{
    public const string OptionText = "public field option";

    [Option(OptionText)]
    public string Option;
}

[Options(new[] { "asd" })]
file sealed class PrivateFieldOption
{
    public const string OptionText = "private field option";

    [Option(OptionText)]
    private string Option;
}

[Options(new[] { "asd" })]
file sealed class PublicPropertyGetSetOption
{
    public const string OptionText = "public property get set option";

    [Option(OptionText)]
    public string Option { get; set; }
}

[Options(new[] { "asd" })]
file sealed class PrivatePropertyGetSetOption
{
    public const string OptionText = "private property get set option";

    [Option(OptionText)]
    private string Option { get; set; }
}

[Options(new[] { "asd" })]
file sealed class PropertyGetOption
{
    public const string OptionText = "property get option";

    [Option(OptionText)]
    public string Option { get; }
}

[Options(new[] { "asd" })]
file sealed class TwoFieldOptions
{
    public const string Option1Text = "option 1";
    public const string Option2Text = "option 2";

    [Option(Option1Text)]
    public string Option1;

    [Option(Option2Text)]
    public int Option2;
}

[Options(new[] { "asd" })]
file sealed class TwoPropertyOptions
{
    public const string Option1Text = "option 1";
    public const string Option2Text = "option 2";

    [Option(Option1Text)]
    public string Option1 { get; set; }

    [Option(Option2Text)]
    public int Option2 { get; set; }
}

[Options(new[] { "asd" })]
file sealed class FieldPropertyOptions
{
    public const string Option1Text = "option 1";
    public const string Option2Text = "option 2";

    [Option(Option1Text)]
    public string Option1;

    [Option(Option2Text)]
    public int Option2 { get; set; }
}
