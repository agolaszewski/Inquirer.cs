[build]:     https://ci.appveyor.com/project/agolaszewski/inquirer-cs
[build-img]: https://img.shields.io/appveyor/ci/agolaszewski/inquirer-cs.svg
[nuget-img]: https://img.shields.io/nuget/v/Inquirer.cs.svg
[nuget]:     https://www.nuget.org/packages/Inquirer.cs/

[checkbox-img]: Assets/Screenshots/checkbox.png
[confirm-img]: Assets/Screenshots/confirm.png
[extended-img]: Assets/Screenshots/extended.png
[input-img]: Assets/Screenshots/input.png
[list-img]: Assets/Screenshots/list.png
[password-img]: Assets/Screenshots/password.png
[rawlist-img]: Assets/Screenshots/rawlist.png

Inquirer.cs
===========

[![][build-img]][build]
[![][nuget-img]][nuget]

A collection of common interactive command line user interfaces.

Clone of [Inquirer.js](https://github.com/SBoudrias/Inquirer.js)

### Installation

```shell
Install-Package Inquirer.cs
```

### Prompt types

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
