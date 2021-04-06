[build]:     https://ci.appveyor.com/project/agolaszewski/inquirer-cs
[build-img]: https://ci.appveyor.com/api/projects/status/ou95jchcrvug0e9g/branch/master?svg=true
[nuget-img]: https://img.shields.io/nuget/v/Inquirer.cs.svg
[nuget]:     https://www.nuget.org/packages/Inquirer.cs/

[checkbox-img]: Assets/Screenshots/checkbox.png
[confirm-img]: Assets/Screenshots/confirm.PNG
[extended-img]: Assets/Screenshots/extended.png
[input-img]: Assets/Screenshots/input.png
[list-img]: Assets/Screenshots/list.png
[password-img]: Assets/Screenshots/password.PNG
[rawlist-img]: Assets/Screenshots/rawlist.png

Inquirer.cs
===========

[![][build-img]][build]


A collection of common interactive command line user interfaces.


Forked from https://github.com/agolaszewski/Inquirer.cs and updated.

Clone of [Inquirer.js](https://github.com/SBoudrias/Inquirer.js)

## Installation

```shell
Install-Package Inquirer.cs
```

## Prompt types

#### List

```csharp
List<ConsoleColor> colors = Enum.GetValues(typeof(ConsoleColor)).Cast<ConsoleColor>().ToList();
Question.List("Choose favourite color", colors);
```
![][list-img]

#### Raw List
```csharp
List<ConsoleColor> colors = Enum.GetValues(typeof(ConsoleColor)).Cast<ConsoleColor>().ToList();
Question.RawList("Choose favourite color", colors);
```
![][rawlist-img]

#### Expand
```csharp
var colors = new Dictionary<ConsoleKey, ConsoleColor>();
            colors.Add(ConsoleKey.B, ConsoleColor.Blue);
            colors.Add(ConsoleKey.C, ConsoleColor.Cyan);
            colors.Add(ConsoleKey.D, ConsoleColor.DarkBlue);

Question.ExtendedList("Choose favourite color", colors);
```
![][extended-img]

#### Checkbox
```csharp
var colors = Enum.GetValues(typeof(ConsoleColor)).Cast<ConsoleColor>().ToList();
Question.Checkbox("Choose favourite colors", colors);
```
![][checkbox-img]

#### Confirm
```csharp
Question.Confirm("Are you sure?");
```
![][confirm-img]

#### Input
```csharp
Question.Input("How are you?");
Question.Input<int>("2+2")
```
![][input-img]

#### Password
```csharp
Question.Password("Type password");
```
![][password-img]

#### Menu

```csharp
private static void MenuTest()
        {
            Question.Menu("Choose")
               .AddOption("PagingCheckboxTest", () => { PagingCheckboxTest(); })
               .AddOption("PagingRawListTest", () => { PagingRawListTest(); })
               .AddOption("PagingListTest", () => { PagingListTest(); })
               .AddOption("InputTest", () => { InputTest(); })
               .AddOption("PasswordTest", () => { PasswordTest(); })
               .AddOption("ListTest", () => { ListTest(); })
               .AddOption("ListRawTest", () => { ListRawTest(); })
               .AddOption("ListCheckboxTest", () => { ListCheckboxTest(); })
               .AddOption("ListExtendedTest", () => { ListExtendedTest(); })
               .AddOption("ConfirmTest", () => { ConfirmTest(); }).Prompt();
        }
```

## Extensions

Change the number of lines that will be rendered when using list, rawList, or checkbox.
```csharp
.Page(int pageSize)
```

Default value(s) to use if nothing is entered.
```csharp
.WithDefaultValue(...)
args:
TResult defaultValue
List<TResult> defaultValues : for list types and types implementing IComparable
Func<TResult, bool> compareTo : for list types not implementing IComparable

```
Chosen value displayed for final confirmation

For password type, user have to confirm password by typing it again

```csharp
.WithConfirmation()
```

Set display name for complex type
```csharp
.WithConvertToString(Func<TResult, string> fn)
```

Should return true if the value is valid, and an error message (String) otherwise. If false is returned, a default error message is provided.

```csharp
.WithValidation(Func<TResult, bool> fn, string errorMessage)
.WithValidation(Func<TResult, bool> fn, Func<TResult, string> errorMessageFn)
```

## Inquirer

```csharp
_test = new Inquirer();
```
Inquirer is for preserving history
It supports 
- navigation
- optional prompts
- hierarchical prompts


```csharp
string answer = string.Empty;
Inquirer.Prompt(Question.Input("1")).Bind(() => answer);
Inquirer.Prompt(Question.Input("2")).Bind(() => answer);
Inquirer.Prompt(() =>
{
    if (answer.Length > 5)
    {
        return Question.Input("3");
    }

    return null;
}).Then(answer2 =>
{
    Inquirer.Prompt(Question.Input("3.1")).Bind(() => answer);
    Inquirer.Prompt(Question.Input("3.2")).Bind(() => answer);
    Inquirer.Prompt(Question.Input("3.3")).Bind(() => answer);
});
Inquirer.Go();
```





