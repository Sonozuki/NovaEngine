namespace NovaEditor.Managers;

/// <summary>Manages editor options.</summary>
internal static class OptionsManager
{
    /*********
    ** Properties
    *********/
    /// <summary>The root categories of the options tree.</summary>
    public static ImmutableArray<OptionsCategory> RootOptionCategories { get; private set; }


    /*********
    ** Constructors
    *********/
    /// <summary>Initialises a class.</summary>
    static OptionsManager()
    {
        var optionTypes = new Dictionary<Type, OptionsAttribute>();

        var types = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(assembly => assembly.GetTypes())
            .Where(type => type.IsClass && !type.IsAbstract);

        foreach (var type in types)
        {
            var optionsAttribute = type.GetCustomAttribute<OptionsAttribute>();
            if (optionsAttribute == null)
                continue;

            optionTypes[type] = optionsAttribute;
        }

        RootOptionCategories = CalculateCategoryTree(optionTypes);
    }


    /*********
    ** Public Methods
    *********/
    /// <summary>Calculates the options present in a type.</summary>
    /// <param name="type">The type whose options should be calculated.</param>
    /// <returns>The options present in <paramref name="type"/>.</returns>
    public static ImmutableArray<Option> CalculateOptions(Type type)
    {
        var options = new List<Option>();

        var fieldInfos = type
            .GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
            .Where(fieldInfo => fieldInfo.HasCustomAttribute<OptionAttribute>());
        var propertyInfos = type
            .GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
            .Where(propertyInfo => propertyInfo.HasCustomAttribute<OptionAttribute>() && propertyInfo.CanWrite);

        foreach (var fieldInfo in fieldInfos)
            options.Add(new Option(fieldInfo.FieldType, fieldInfo.GetCustomAttribute<OptionAttribute>().Text));
        foreach (var propertyInfo in propertyInfos)
            options.Add(new Option(propertyInfo.PropertyType, propertyInfo.GetCustomAttribute<OptionAttribute>().Text));

        return options.ToImmutableArray();
    }


    /*********
    ** Internal Methods
    *********/
    /// <summary>Calculates an options tree from a dictionary of types with an <see cref="OptionsAttribute"/>.</summary>
    /// <param name="optionTypes">The dictionary of types and its corresponding <see cref="OptionsAttribute"/>.</param>
    /// <returns>An options tree.</returns>
    internal static ImmutableArray<OptionsCategory> CalculateCategoryTree(Dictionary<Type, OptionsAttribute> optionTypes)
    {
        var rootOptionCategories = new List<OptionsCategory>();

        foreach (var (optionType, optionAttribute) in optionTypes)
            AddOption(rootOptionCategories, optionAttribute.OptionsCategories, optionType);

        return rootOptionCategories.ToImmutableArray();
    }

    /// <summary>Recursively adds an option to a collection of categories.</summary>
    /// <param name="currentCategories">The categories to add the option to, based on <paramref name="requestedOptionCategoriesNames"/>.</param>
    /// <param name="requestedOptionCategoriesNames">The names of the categories to add the option to, each subsequent element being a child category of the previous.</param>
    /// <param name="optionType">The type containing the options to add.</param>
    internal static void AddOption(List<OptionsCategory> currentCategories, IEnumerable<string> requestedOptionCategoriesNames, Type optionType)
    {
        var requestedCategoryName = requestedOptionCategoriesNames.First();

        var existingCategory = currentCategories.FirstOrDefault(category => category.Name == requestedCategoryName);
        if (existingCategory == null)
        {
            var newCategory = new OptionsCategory(requestedCategoryName);
            currentCategories.Add(newCategory);
            existingCategory = newCategory;
        }

        if (requestedOptionCategoriesNames.Count() > 1)
            AddOption(existingCategory.SubCategories, requestedOptionCategoriesNames.Skip(1), optionType);
        else
            existingCategory.Options.AddRange(CalculateOptions(optionType));
    }
}
