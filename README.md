[build]:     https://ci.appveyor.com/project/agolaszewski/inquirer-cs
[build-img]: https://img.shields.io/appveyor/ci/agolaszewski/inquirer-cs.svg
[nuget-img]: https://img.shields.io/nuget/v/Inquirer.cs.svg
[nuget]:     https://www.nuget.org/packages/Inquirer.cs/

Inquirer.cs
===========

[![][build-img]][build]
[![][nuget-img]][nuget]

A collection of common interactive command line user interfaces.

Clone of [Inquirer.js](ttps://github.com/SBoudrias/Inquirer.js)

### Installation

```shell
Install-Package Inquirer.cs
```

### Prompt types

#### List

```c#
List<ConsoleColor> colors = Enum.GetValues(typeof(ConsoleColor)).Cast<ConsoleColor>().ToList();
Question.List("Choose favourite color", colors);
```

#### Raw List
```c#
List<ConsoleColor> colors = Enum.GetValues(typeof(ConsoleColor)).Cast<ConsoleColor>().ToList();
Question.RawList("Choose favourite color", colors);
```

#### Expand
```c#
var colors = new Dictionary<ConsoleKey, ConsoleColor>();
            colors.Add(ConsoleKey.B, ConsoleColor.Blue);
            colors.Add(ConsoleKey.C, ConsoleColor.Cyan);
            colors.Add(ConsoleKey.D, ConsoleColor.DarkBlue);

Question.ExtendedList("Choose favourite color", colors);
```
#### Checkbox
```c#
var colors = Enum.GetValues(typeof(ConsoleColor)).Cast<ConsoleColor>().ToList();
Question.Checkbox("Choose favourite colors", colors);
```

#### Confirm
```c#
Question.Confirm("Are you sure?");
```

#### Input
```c#
Question.Input("How are you?");
Question.Input<int>("2+2")
```

#### Password
```c#
Question.Password("Type password");
```
