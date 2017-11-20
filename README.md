[build]:     https://ci.appveyor.com/project/agolaszewski/inquirer-cs
[build-img]: https://img.shields.io/appveyor/ci/agolaszewski/inquirer-cs.svg
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
[![][nuget-img]][nuget]

A collection of common interactive command line user interfaces.

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

## Extensions

```csharp
.Page(int pageSize)
```
Change the number of lines that will be rendered when using list, rawList, or checkbox.

```csharp
.WithDefaultValue(TResult defaultValue, Func<TResult, TResult, int> compareFn = null)
```
Default value(s) to use if nothing is entered.
compareFn must

```csharp
.WithConfirmation()
```
Choosen value is displayed for final confirmation

```csharp
.WithValidation(Func<TResult, bool> fn, string errorMessage)
.WithValidation(Func<TResult, bool> fn, Func<TResult, string> errorMessageFn)
```
Should return true if the value is valid, and an error message (String) otherwise. If false is returned, a default error message is provided.

## Inquirer

```csharp
new Inquirer<Answers>();

_answers = new Answers();
_test = new Inquirer<Answers>(_answers);
```
Inquirer is for preserving history and assigning answer to TAnswer class.
It works by wrapping  ```csharp Prompt ``` or ```csharp Menu ``` methods in another method.

```csharp
private static void PagingCheckboxTest()
        {
            var colors = Enum.GetValues(typeof(ConsoleColor)).Cast<ConsoleColor>().ToList();
            var answer = _test.Prompt(Question.Checkbox("Choose favourite colors", colors)
                .Page(3)
                .WithDefaultValue(new List<ConsoleColor>() { ConsoleColor.Black, ConsoleColor.DarkGray })
                .WithConfirmation()
                .WithValidation(values => values.Any(item => item == ConsoleColor.Black), "Choose black"))
            .Return();
            MenuTest();
        }
```


#### Menu

```csharp
_test.Menu("Choose")
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
```

#### Prompt
```csharp
public InquirerFor<TAnswers, TResult> Prompt<TResult>(QuestionBase<TResult> question)
```

### For
```csharp
public TResult For(Expression<Func<TAnswers, TResult>> answerProperty)
```


