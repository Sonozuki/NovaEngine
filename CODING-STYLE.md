# Nova Coding Style

When contributing to the source code of any Nova project, you'll be expected to follow the coding styles outlined below. 
[NStyler](https://github.com/Sonozuki/NStyler) has been created to make following the style as simple as possible. It comprises of a diagnostic analyser for as well as as stand-alone rewriter to make following the coding styles as automated as possible.

### Setting up NStyler

TODO: NStyler setup instructions

### Styles

TODO: local functions styles

#### Documentation

1. Delimited comments (`/* */`) shouldn't be used; when a comments spreads to new lines then multiple single-line comments are used.
```cs
// this is valid

// this is a valid
// multiple line comment

/* this is invalid */

/*
this is an invalid
multiple line comment
*/
```

2. Comments should start with a lower case and end with no full-stop, but otherwise follow standard grammar rules. Comments should also always have a single space betwee the `//` and the start of the comment.
```cs
// this is valid

// the first sentence. the second sentence

//this is invalid

// This is also invalid

// this is also invalid.
```

3. "TODO" comments should always be as close to the code that needs seeing to and should be formatted as `// TODO: message`.
```cs
// TODO: a valid todo

// todo: an invalid todo

// TODO:also invalid

// FIXME: also invalid
```

4. Documetation should always start with an upper case and end with a full-stop. Documentation tags should be on the same line as the documentation text unless the text contains a `<br/>` in which case the tags should be on separate lines and a new-line should be after the break tag.
```cs
/// <summary>This is valid.</summary>
```
```cs
/// <summary>this is invalid.</summary>
```
```cs
/// <summary>This is also invalid</summary>
```
```cs
/// <summary>
/// This is also invalid.
/// </summary>
```
```cs
/// <summary>This is also invalid<br/>because this is a single line with a break tag.</summary>
```
```cs
/// <summary>
/// However, this<br/>
/// is valid.
/// <summary>
```

5. Every class, interface, member, etc should have XML documentation, regardless of privacy.
```cs
/// <summary>Some relevant documentation.</summary>
public class Foo
{
    /// <summary>Some more relevant documentation.</summary>
    private string Bar;
}
```

6. Documentation should contains `<see cref|langword=""/>` and `<paramref name=""/>` where relevant.
```cs
/// <summary>Some documentation.</summary>
/// <param name="name">Some more.</param>
/// <exception cref="ArgumentNullException">Thrown if <paramref name="name"/> is <see langword="null"/>.<exception>
public void Bar(string name)
{
    if (name == null)
        throw new ArgumentNullException(nameof(name));
}
```

7. Documentation should inclulde *all* the possible exceptions that a method can throw, either directly or indirectly. All exception documentation should start with `Thrown if ...`.
```cs
/// <summary>Some documentation.</summary>
/// <param name="name">Some more.</param>
/// <exception cref="ArgumentNullException">Thrown if <paramref name="name"/> is <see langword="null"/>.</exception>
/// <exception cref="InvalidOperationException">Thrown if this method is called and <paramref name="name"/> isn't <see langword="null"/>.</exception>
public void Foo(string name)
{
    Bar(name);
    throw new InvalidOperationException();
}

/// <summary>Some documentation.</summary>
/// <param name="name">Some more.</param>
/// <exception cref="ArgumentNullException">Thrown if <paramref name="name"/> is <see langword="null"/>.</exception>
public void Bar(string name)
{
    if (name == null)
        throw new ArgumentNullException(nameof(name));
}
```

8. Class initialiser documentation should always have a summary of: `Initialises the class.`.
```cs
class Foo
{
    /// <summary>Initialises the class.</summary>
    static Foo() { }
}
```

9. Constructor documentation should always have a summary of: `Constructs an instance.`.
```cs
class Foo
{
    /// <summary>Constructs an instance.</summary>
    public Foo() { }
}
```

10. Extension class documentation should always have a summary of: `Extension methods for <see cref="type being extended"/>.`.
```cs
/// <summary>Extension methods for <see cref="string"/>.</summary>
static class StringExtensions { }
```

#### Capitalisation

1. Classes, interfaces, and members should all be `PascalCase` with the only exception being backing fields which are backing a property with the same name which are `_PascalCase`.
```cs
class Foo { } // valid
```
```cs
class foo { } // invalid
```
```cs
class Bar
{
    private string Name // valid
    private string name // invalid

    private int _Height; // valid (due to the property below)
    private int _height; // invalid
    private int _Weight; // invalid (no property called Weight)

    public int Age { get; set; } // valid
    public int age { get; set; } // invalid

    public int Height
    {
        get => _Height;
        set => _Height = value;
    }

    public string PrintDetails() { } // valid
    public string printDetails() { } // invalid
}
```

2. Local variables are always `camelCase`.
```cs
public string PrintDetails()
{
    var fullName = "John Smith"; // valid
    var FullName = "John Smith"; // invalid
}
```

#### Type Declarations

1. Abstract class should always end in `Base`.
```cs
public abstract class FooBase { } // valid
```
```cs
public abstract class Foo { } // invalid
```

2. Interfaces should always be prefixed with `I`.
```cs
public interface IFoo { } // valid
```
```cs
public interface Foo { } // invalid
```

3. Bit field enums should always end in `Flags` and have a `[Flags]` attribute.
```cs
// valid
[Flags]
public enum ButtonFlags
{
    A = 1 << 0,
    B = 1 << 1
}
```
```cs
// invalid
public enum Buttons
{
    A = 1 << 0,
    B = 1 << 1
}
```

4. Bit field enums should have fields set using a bit shifted `1` or with a bit-wise or (|) if possible.
```cs
// valid
[Flags]
public enum ButtonFlags
{
    A = 1 << 0,
    B = 1 << 1,
    All = A | B // valid
    All = 0b11 // invalid
}
```
```cs
// invalid
[Flags]
public enum ButtonFlags
{
    A = 0x1,
    B = 0x2,
    C = 0x4,
}
```
```cs
// invalid
[Flags]
public enum ButtonFlags
{
    A = 0b001,
    B = 0b010,
    C = 0b100
}
```

5. Extension classes should contains *only* extension methods and private utility methods used by the extension methods only; and always be called `[TypeName]Extensions`.
```cs
static class StringExtensions { } // valid
```
```cs
static class StringUtils { } // invalid
```

6. Partial classes should never be used.

7. Records should never be used.

8. Non public classes should always be sealed were possible.
```cs
// valid
internal class Transform { }
internal sealed class UITransform : Transform { }
```
```cs
// invalid
internal class Transform { }
```

#### Formatting

1. All the members in a class, struct, or interface should be grouped by type and each type should follow a specific order. The groups and order of the types are: `Constants`, `Events`, `Delegates`, `Fields`, `Properties`, `Indexers`, `Constructors`, `Public Methods`, `Protected Internal Methods`, `Internal Methods`, `Protected Methods`, `Private Protected Methods`, `Private Methods`, `Operators`.  
The class initialiser should be at the top of the `Constructors` group, and the finaliser should be after the initialiser.  
Each group should be separated by two new lines and have a comment header exactly matching:
```cs
/*********
** [Group Name]
*********/
```
```cs
// invalid (lower case)
/*********
** [group name]
*********/
```
```cs
// invalid (no space)
/*********
**[Group Name]
*********/
```
```cs
// invalid
/*********
* [Group Name]
*********/
```
```cs
// invalid
/****
** [Group Name]
****/
```
```cs
// valid
class Foo
{
    /*********
    ** Fields
    *********/
    private string _Name;
    

    /*********
    ** Properties
    *********/
    public string Name
    {
        get => _Name;
        set => _Name = value ?? "";
    }


    /*********
    ** Constructors
    *********/
    public Foo(string name)
    {
        Name = name;
    }
}
```
```cs
// invalid (wrong order)
class Foo
{
    /*********
    ** Constructors
    *********/
    public Foo(string name)
    {
        Name = name;
    }


    /*********
    ** Fields
    *********/
    private string _Name;
    

    /*********
    ** Properties
    *********/
    public string Name
    {
        get => _Name;
        set => _Name = value ?? "";
    }
}
```
```cs
// invalid (only one new line between groups)
class Foo
{
    /*********
    ** Fields
    *********/
    private string _Name;
    
    /*********
    ** Properties
    *********/
    public string Name
    {
        get => _Name;
        set => _Name = value ?? "";
    }

    /*********
    ** Constructors
    *********/
    public Foo(string name)
    {
        Name = name;
    }
}
```

2. Braces should always be on new lines.
```cs
// valid
class Foo
{

}
```
```cs
// invalid
class Foo {

}
```

3. Always exclude braces on single statement `if`, `for`, `foreach`, and `while` where possible
```cs
// valid, exclude braces even if not all blocks are single statement
if (condition)
    statement;
else
{
    statement1;
    statement2;
    statement3;
}
```
```cs
// exclude braces on nested blocks
if (condition1)
    if (condition2)
        statement1;
    else
        statement2;

if (condition3) // even if the nested block isn't single statment
    if (condition4)
    {
        statement3;
        statement4;
    }
```
```cs
if (condition1) // braces are required here, otherwise the else would be part of if, else if block, not the initial if
{
    if (condition2)
        statement2;
    else if (condition3)
        statement3;
}
else
    statment1;
```

4. Always use string interpolation.
```cs
// valid
public static string FullName(string firstName, string lastName)
{
    return $"{firstName} {lastName}";
}
```
```cs
// invalid
public static string FullName(string firstName, string lastName)
{
    return firstName + " " + lastName;
}
```
```cs
// invalid
public static string FullName(string firstName, string lastName)
{
    return string.Format("{0} {1}", firstName, lastName);
}
```

5. Four spaces should always be used for indentation, tabs shouldn't be used.

6. The accessibility modifier should always be specified.
```cs
internal class Foo { } // valid
```
```cs
class Foo { } // invalid
```
```cs
internal class Bar
{
    private string Name; // valid
    string Name; // invalid
}
```

7. The order of modifiers for a class should always be: `[Access modifier] static abstract|sealed class [Name]`.
```cs
public static class Foo { } // valid
```
```cs
static public class Foo { } // invalid
```
```cs
abstract public class Foo { } // invalid
```

8. The order of modifiers for a struct should always be: `[Access modifier] static ref struct [Name]`.
```cs
public ref struct Bar { } // valid
```
```cs
static public struct Bar { } // invalid
```

9. The order of modifiers for a member should always be: `[Access modifier] static readonly sealed abstract|virtual|override ref [Type] [Name]`.
```cs
// valid
private static int Bar;
protected internal sealed override ref int Foo => ref Bar;
```
```cs
// invalid
static private int Bar;
sealed override protected internal ref int Foo => ref Bar;
```

10. Using directives should always be specified in the .csproj file in there own `<ItemGroup>`, sorted alphabetically.
```cs
using System; // invalid
```
```xml
<!-- valid -->
<PropertyGroup>
    <OutputType>Exe</OutputType>
    <TargetFramework>net6.0</TargetFramework>
</PropertyGroup>

<ItemGroup>
    <Using Include="System" />
</ItemGroup>
```

11. Namespaces should always be file scoped and located at the very top of the file.
```cs
// valid
namespace Foo;

class Bar { }
```
```cs
// invalid
namespace Foo
{
    class Bar { }
}
```

12. Files should only contain one interface or class.
```cs
// valid 

// Bar.cs
class Bar { }

// Foo.cs
class Foo { }
```
```cs
// invalid

// Classes.cs
class Bar { }
class Foo { }
```

13. Non-scoped `using` should always be used.
```cs
// valid
using var stream = new MemoryStream();
```
```cs
// invalid
using (var stream = new MemoryStream())
{

}
```
This also enforces that the variable passed to a using should always be instantiated in that `using`.
```cs
// valid
using var stream = new MemoryStream();
```
```cs
// this is invalid C#
var stream = new MemoryStream();
using stream;
```

14. Use `var` where possible. Use literals if required (`f`, `m`, `u`, `L` (`L` should always be upper-case to prevent confusion with `1`) etc) 
```cs
class Foo : IFoo { }
``` 
```cs
// valid
var foo = new Foo();
var @float = 1f;
var @double = 1d; // never use 1.0
var name = GetName(); // use var even when the right side type isn't clear
```
```cs
// cast if the type of the variable doesn't match the right side
var iFoo = (IFoo)new Foo(); // valid
IFoo iFoo = new Foo(); // invalid
```
```cs
// invalid
string name = "John Smith";
```

15. Use `new()` where possible.
```cs
// valid
class Foo
{
    private List<int> Bar = new(); // use new() where var can't be used
}
```
```cs
// valid
var numbers = new List<int>(); // always use var instead of new() where possible
```
```cs
// invalid, don't use new() instead of var
List<int> numbers = new();
```

16. Use language keywords instead of BCL types.
```cs
var name = (string)GetName(); // valid
var name = (String)GetName(); // invalid
```
```cs
var name = string.Join(" ", new[] { "John", "Smith" }); // valid
var name = String.Join(" ", new[] { "John", "Smith" }); // invalid
```

17. Use `nameof(...)` where relevant.
```cs
public static void FullName(string firstName, string lastName)
{
    if (string.IsNullOrWhiteSpace(firstName))
    {
        Console.WriteLine($"{nameof(firstName)} is empty."); // valid
        return;
    }
    
    if (string.IsNullOrWhiteSpace(lastName))
    {
        Console.WriteLine("lastName is empty."); // invalid
        return;
    }

    return $"{firstName} {lastName}";
}
```

18. Never use `goto`

19. Never use `#region`

20. Avoid using two new lines unless it's to separate member groups (As specified in [Formatting#1](#Formatting))

21. Never used nested classes or structures
```cs
// valid (if in separate files)
class Foo { }

class Bar { }
```
```cs
// invalid
class Foo
{
    class Bar { }
}
```

22. Use `this.` and `base.` only when absolutely neccessary.

23. When a constructor calls another constructor always new line the `: this(...)`. If the constructor has no body that the braces should be on the same line too.
```
// valid
class Foo
{
    public Foo()
        : this(-1) { } // with no body and calling another constructor, braces should be on just here

    private Foo(int number) { } // if the constructor isn't calling another and doesn't have a body then entire definition should be on the same line
}
```
```
// invalid
class Foo
{
    // all of the following are invalid
    public Foo() : this(-1) { }
    public Foo() : this(-1)
    { }
    public Foo()
        : this(-1)
    { }

    private Foo(int number)
    { } 
}
```

24. Never have public fields in a class, use a property instead, even if both the getter and setter are public.
```cs
// valid
class Foo
{
    public string Name { get; set; }
}
```
```cs
// valid (structs are allowed public fields)
struct Bar
{
    public string Name;
}
```
```cs
// invalid
class Foo
{
    public string Name;
}
```
