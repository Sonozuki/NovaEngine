namespace NovaEditor.ComponentUIBuilding;

/// <summary>Represents a UI builder for creating UI for a specified component.</summary>
/// <typeparam name="TComponent">The type of component the UI builder can generate UI for.</typeparam>
public class ComponentUIBuilder<TComponent>
    where TComponent : ComponentBase
{
    /*********
    ** Properties
    *********/
    /// <summary>All members, serialisable and unserialisable, ordered as they're defined.</summary>
    protected ImmutableArray<MemberInfo> AllMembers { get; }

    /// <summary>All serialisable members, ordered as they're defined.</summary>
    protected ImmutableArray<MemberInfo> SerialisableMembers { get; }


    /*********
    ** Constructors
    *********/
    /// <summary>Creates an instance.</summary>
    public ComponentUIBuilder()
    {
        var serialisableFields = typeof(TComponent).GetSerialisableFields();
        var serialisableProperties = typeof(TComponent).GetSerialisableProperties();

        // order fields and properties in the same way as they're defined on the type
        AllMembers = typeof(TComponent).GetMembers(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic).ToImmutableArray();

        var serialisableMembers = new List<MemberInfo>();
        foreach (var member in AllMembers)
            if (serialisableFields.Contains(member) || serialisableProperties.Contains(member))
                serialisableMembers.Add(member);

        SerialisableMembers = serialisableMembers.ToImmutableArray();
    }


    /*********
    ** Protected Internal Methods
    *********/
    /// <summary>Renders the UI to <see cref="GUI"/> for a specific instance.</summary>
    /// <param name="component">The instance of the component to render the UI of.</param>
    protected internal virtual void BuildUI(TComponent component)
    {
        foreach (var serialisableMemberInfo in SerialisableMembers)
            CreateDefaultUIElement(serialisableMemberInfo, component);
    }


    /*********
    ** Protected Methods
    *********/
    /// <summary>Creates the default UI for a member of a specific instance.</summary>
    /// <param name="memberInfo">The member info to create the UI for.</param>
    /// <param name="component">The component instance to create the UI for.</param>
    /// <exception cref="InvalidOperationException">Thrown if <paramref name="memberInfo"/> isn't a field or property.</exception>
    protected void CreateDefaultUIElement(MemberInfo memberInfo, TComponent component)
    {
        Type memberType;
        if (memberInfo is FieldInfo fieldInfo)
            memberType = fieldInfo.FieldType;
        else if (memberInfo is PropertyInfo propertyInfo)
            memberType = propertyInfo.PropertyType;
        else
            throw new InvalidOperationException($"{nameof(memberInfo)} must be a field or property.");

        if (memberType == typeof(float))
        {
            CreateCallbacks<float>(memberInfo, component, out var getValue, out var setValue);
            GUI.CreateFloatBox(memberInfo.Name, getValue, setValue);
        }
        else if (memberType == typeof(Vector3<float>))
        {
            CreateCallbacks<Vector3<float>>(memberInfo, component, out var getValue, out var setValue);
            GUI.CreateVectorBlock(getValue, setValue);
        }
        else
        {
            // TODO: don't just silently ignore, also perhaps make this more extensible so custom types can have GUIs generated
        }
    }


    /*********
    ** Private Methods
    *********/
    /// <summary>Creates the get/set value callbacks for a member of a specific instance.</summary>
    /// <typeparam name="T">The type of the member.</typeparam>
    /// <param name="memberInfo">The member info to create callbacks for.</param>
    /// <param name="component">The component instance to create callbacks for.</param>
    /// <param name="getValue">The created get value callback.</param>
    /// <param name="setValue">The created set value callback.</param>
    /// <exception cref="InvalidOperationException">Thrown if <paramref name="memberInfo"/> isn't a field or property.</exception>
    private void CreateCallbacks<T>(MemberInfo memberInfo, TComponent component, out Func<T> getValue, out Action<T> setValue)
    {
        if (memberInfo is FieldInfo fieldInfo)
        {
            getValue = () => (T)fieldInfo.GetValue(component);
            setValue = value => fieldInfo.SetValue(component, value);
        }
        else if (memberInfo is PropertyInfo propertyInfo)
        {
            getValue = () => (T)propertyInfo.GetValue(component);

            if (propertyInfo.CanWrite)
                setValue = value => propertyInfo.SetValue(component, value);
            else
            {
                var backingField = propertyInfo.GetBackingField();
                setValue = value => backingField.SetValue(component, value);
            }
        }
        else
            throw new InvalidOperationException($"{nameof(memberInfo)} must be a field or property.");
    }
}
