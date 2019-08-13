This document contains information about the general code style that is used in this project.
The code is all written in C#, thus only information about C# elements is defined in this document.
Should any other language be used, consideration about updating this document accordingly should be taken into account.

# Naming Conventions

<table>
    <tr>
        <td><b>Element Type</b></td>
        <td align="center"><code>public</code></td>
        <td align="center"><code>internal</code></td>
        <td align="center"><code>protected</code><br /><b><code>protected internal</code></b><br /><b><code>protected private</code></b></td>
        <td align="center"><code>private</code></td>
    </tr>
    <tr>
        <td><b>Namespace</b></td>
        <td colspan="4" align="center">PascalCase</td>
    </tr>
    <tr>
        <td><b>Type</b></td>
        <td colspan="4" align="center">PascalCase</td>
    </tr>
    <tr>
        <td><b>Function</b></td>
        <td colspan="4" align="center">PascalCase</td>
    </tr>
    <tr>
        <td><b>Constant</b></td>
        <td colspan="3" align="center">PascalCase</td>
        <td align="center">camelCase</td>
    </tr>
    <tr>
        <td><b>Type Field</b></td>
        <td colspan="3" align="center">PascalCase</td>
        <td align="center">camelCase</td>
    </tr>
    <tr>
        <td><b>Type Property</b></td>
        <td colspan="3" align="center">PascalCase</td>
        <td align="center">camelCase</td>
    </tr>
    <tr>
        <td><b>Type Event</b></td>
        <td colspan="4" align="center">PascalCase</td>
    </tr>
    <tr>
        <td><b>Delegate</b></td>
        <td colspan="4" align="center">PascalCase</td>
    </tr>
    <tr>
        <td><b>Function Argument</b></td>
        <td colspan="4" align="center">camelCase</td>
    </tr>
    <tr>
        <td><b>Local Variable</b></td>
        <td colspan="4" align="center">camelCase</td>
    </tr>
    <tr>
        <td><b>Local Function</b></td>
        <td colspan="4" align="center">PascalCase</td>
    </tr>
</table>

Note: Accessibility modifiers that are in <b>bold</b> shouldn't ever be used, as their usage is excessively specialized and most of the time is bad practice.

Note: The only exception to the rule is `load()` with the `BackgroundDependencyLoader` attribute, because that's how the framework works.

Rule: The names of the elements must never be shortened, e.g.:
- Bad: GDELS
- Good: GDELongString (considering GDE is the name of the project, whilst being a short for GD Edit)

# Code Formatting

## Class/Struct Definition

Ordering of definition of elements:
- Constants (private, protected, public)
- Static non-functions (private, protected, public)
- Fields (private, protected, public)
- Properties (private, protected, public)
- Constructors (grouped based on accessibility, ordered based on argument count)
- Destructor
- Functions (public, protected, private)
- Static functions (public, protected, private)

## Usings

The ordering of using types is the following:
- `using <namespace>;`
- `using static <type>;`
- `using <alias> = <type>;`

Usage of `using static` is recommended for the most part. They should be used when it's clear that it feels overwhelming to read the class' name as well. E.g.:

```csharp
// Good usage
using static System.Math;
int min = Min(0, 1);

// Bad usage - the default height might refer to that of another component,
// or even cause naming collisions, rendering the using useless
using static GDE.App.Main.UI.FileDialogComponents.DrawableItem;
int defaultHeight = DefaultHeight;
```

Usage of the alias definition is not recommended, instead prefer renaming your desired type to avoid collisions. In general, allowing naming collisions under the protection of aliases is bad practice.

## Indentation

An indentation block is 4 spaces and should never be tabs. This is to prohibit stylization inconsistencies across editors, despite increasing the file size by 3 bytes per indent. As if computers cannot store those extra few KBs wasted on indents.

## Whitespace

Between definitions, only use a single blank line if the definitions are not linked. E.g.:

```csharp
private int counter;

private Box background;
private FadeButton button;

// ...

protected override bool OnHover(HoverEvent e)
{
    this.FadeColour(hoverColour, 100);
    return true;
}
protected override void OnHoverLost(HoverLostEvent e)
{
    this.FadeColour(defaultColour, 100);
}

protected override bool OnMouseDown(MouseDownEvent e)
{
    if (!base.OnMouseDown(e))
        return false;
    
    box.FadeColour(highlightColour, 100);
    return true;
}
protected override bool OnMouseUp(MouseUpEvent e)
{
    if (e.Button != MouseButton.Left)
        return false;

    box.FadeColour(Color4.White, 100);
    return base.OnMouseUp(e);
}
```

## Brackets

```csharp
if (condition)
{
    // code
}
```

For single-line statements, brackets are not used. That also includes nested statements, e.g.:

```csharp
// This is just an example, please do not use that algorithm anywhere in your code, or any other O(n^2) or worse alorithms when a better solution exists
for (int i = 0; i < ar.Length; i++)
    for (int j = i + 1; j < ar.Length; j++)
        if (ar[i] == ar[j])
            return true;
```

## Expressions

### Operators

For all binary operators (=, +, -, *, /, %, ^, &, |, &&, ||, ??, <, <=, ==, >=, >, !=, compound assignments) and the ternary operator ?:, a space is used before and after the operator, e.g. a = b;

For all unary operators no space is used.

```csharp
int expression = (3 + 2) / 6 % 13;
bool comparison = (expression += 45 & expression) <= 95;
int b = comparison ? 1 : 0;

int? something = null;
int a = something ?? 0;

int b = -a;
comparison = !comparison;
b++;
double c = (double)b;
string s = default;
var v = default(string);

// I believe no further examples are necessary
```

### Overview

A balance between linearization and simplification must be preserved. Specifically:

- Prefer multiple assignments in the same line, instead of more than one lines, e.g.:

```csharp
private NumberTextBox counterDisplay;
private int counter;

// NO
public int Counter
{
    get => counter;
    set
    {
        counterDisplay.Number = value;
        counter = value;
    }
}

// YES
public int Counter
{
    get => counter;
    set => counter = counterDisplay.Number = value;
}
```

- Prefer not using the ternary operator on huge expressions, e.g.:

```csharp
// Prefer:
if (IsSatisfyingANumberOfConditions(n))
    return ConvertToValidNumber(n);
return line.Split('\\').SkipLast(1).Count - n;

// Over:
return IsSatisfyingANumberOfConditions(n) ? ConvertToValidNumber(n) : line.Split('\\').SkipLast(1).Count - n;
```

## Switch Statement

Did you know that ever since C# was created, it featured a switch statement? And for a good reason:

```csharp
// Please no
if (number == 1)
    return Case.First;
else if (number == 2)
    return Case.Second;
else if (number == 3)
    return Case.Third;
throw new InvalidOperationException("Invalid case");

// Please yes
switch (number)
{
    case 1:
        return Case.First;
    case 2:
        return Case.Second;
    case 3:
        return Case.Third;
}
throw new InvalidOperationException("Invalid case");
```

However, all problems can be solved even better:

```csharp
public static readonly Dictionary<int, Case> Cases;

private void InitializeCasesDictionary()
{
    Cases = new Dictionary<int, Case>
    {
        { 1, Case.First },
        { 2, Case.Second },
        { 3, Case.Third },
    };
}

// Somewhere in the code:
return Cases[number];
```

## Keywords & Verbosity

Keywords are preferred over the official names of the types they represent, e.g.: `string` over `String`, especially `int` over `Int32`.

Accessibility modifiers are always explicitly stated; that means all code elements must have an accessibility modifier, if the compiler demands so (which means only interface members are excluded).

Prefer making helper functions static if they have nothing to do with the class they're contained in.

# Algorithms

The most optimal w.r.t. time algorithm is prefered, for long as space is not completely fucked. It's okay to use Θ(n^2) space, but prefer not going much higher than that.
