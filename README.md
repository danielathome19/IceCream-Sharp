# IceCream\# — Never use Console.WriteLine() to debug in .NET again.

[![NuGet](https://img.shields.io/nuget/v/IceCream-Sharp.svg)](https://www.nuget.org/packages/IceCream-Sharp/)
[![CI](https://github.com/danielathome19/IceCream-Sharp/actions/workflows/nuget_push.yml/badge.svg)](https://github.com/danielathome19/IceCream-Sharp/actions/workflows/nuget_push.yml)
[![License](https://img.shields.io/github/license/danielathome19/IceCream-Sharp.svg)](https://github.com/danielathome19/IceCream-Sharp/blob/master/LICENSE.md)

IceCream# is a C# port of the [icecream](https://github.com/gruns/icecream) library for Python.

Do you ever use `Console.WriteLine()` or `Debug.WriteLine()` to debug your code? Of course you do. IceCream, or `ic` for short, makes print debugging a little sweeter.

`ic()` is like `Console.WriteLine()`, but better:

 * It prints both expressions/variable names and their values.
 * It's 60% faster to type.
 * Data structures are pretty printed.
 * Output is syntax highlighted.
 * It optionally includes program context: filename, parent class, parent function, and line number.



```csharp
using static IceCream.IceCream;

ic("Hello, World!");
```
```
>>> ic| Program > <Main>$:3 > param_0 = Hello, World!
```


## Installation

Install the [IceCream-Sharp NuGet package](https://www.nuget.org/packages/IceCream-Sharp/):

```bash
dotnet add package IceCream-Sharp
```


## Usage

```csharp
using static IceCream.IceCream;

ic("Hello, World!");
```


## Documentation

### `ic(params object[] objects)`

`ic()` is the main function of IceCream#.

```csharp
ic("Hello, World!");
```
```
>>> ic| Program > <Main>$:3 > param_0 = Hello, World!
```

All other functions are prefixed with `IC_` to prevent name collisions from static importing.



### `IC_Format(params object[] objects)`

`IC_Format()` formats the given objects for debugging, returning the formatted string.

```csharp
using static IceCream.IceCream;
string formatted = IC_Format("Hello, World!");
Console.WriteLine(formatted);
```
```
>>> ic| Program > <Main>$:2 > param_0 = Hello, World!
```





### `IC_SetPrefix(string value)`

`IC_SetPrefix()` sets a custom string prefix for the debugging output (default: `ic| `).

```csharp
using static IceCream.IceCream;
IC_SetPrefix("DEBUG: ");
ic("Hello, World!");
```
```
DEBUG: Program > <Main>$:3 > param_0 = Hello, World!
```





### `IC_SetPrefix(Func<string> func)`

`IC_SetPrefix()` sets a custom function that returns a string prefix for the debugging output.

```csharp
using static IceCream.IceCream;
IC_SetPrefix(() => $"[{DateTime.Now}] ");
ic("Hello, World!");
IC_SetPrefix(() => $"{DateTimeOffset.Now.ToUnixTimeSeconds()} |> ");
ic("Hello, World!");
```
```
[10/22/2023 12:00:00 AM] Program > <Main>$:3 > param_0 = Hello, World!
1634870400 |> Program > <Main>$:5 > param_0 = Hello, World!
```





### `IC_IncludeContext(bool incContext)`

`IC_IncludeContext()` configures whether to include contextual information (like filename, classname, etc.) in the debugging output (default: `true`).

```csharp
using static IceCream.IceCream;
IC_IncludeContext(false);
ic("Hello, World!");
```
```
>>> ic| param_0 = Hello, World!
```





### `IC_Enable()` and `IC_Disable()`

`IC_Enable()` and `IC_Disable()` enable and disable the `ic()` function, respectively. When disabled, `ic()` will not print to the console.

```csharp
using static IceCream.IceCream;
ic(1)
IC_Disable()
ic(2)
IC_Enable()
ic(3)
```
```
>>> ic| Program > <Main>$:2 > param_0 = 1
>>> ic| Program > <Main>$:6 > param_0 = 3
```





### `IC_IncludeFilename(bool flag)`

`IC_IncludeFilename()` configures whether to include the filename in the debugging output (default: `false`).

```csharp
using static IceCream.IceCream;
IC_IncludeFilename(true);
ic("Hello, World!");
```
```
>>> ic| Program.cs > Program > <Main>$:3 > param_0 = Hello, World!
```





### `IC_IncludeClassName(bool flag)`

`IC_IncludeClassName()` configures whether to include the class name in the debugging output (default: `true`).

```csharp
using static IceCream.IceCream;
IC_IncludeClassName(false);
ic("Hello, World!");
```
```
>>> ic| <Main>$:3 > param_0 = Hello, World!
```





### `IC_IncludeAbsolutePath(bool flag)`

`IC_IncludeAbsolutePath()` configures whether to include the absolute path of the file in the debugging output (default: `false`).

```csharp
using static IceCream.IceCream;
IC_IncludeAbsolutePath(true);
ic("Hello, World!");
```
```
>>> ic| C:\\Absolute\\Path\\To\\Program.cs > Program > <Main>$:3 > param_0 = Hello, World!
```





### `IC_IncludeMethodName(bool flag)`

`IC_IncludeMethodName()` configures whether to include the method name in the debugging output (default: `true`).

```csharp
using static IceCream.IceCream;
IC_IncludeMethodName(false);
ic("Hello, World!");
```
```
>>> ic| Program > 3: param_0 = Hello, World!
```





### `IC_IncludeLineNumber(bool flag)`

`IC_IncludeLineNumber()` configures whether to include the line number in the debugging output (default: `true`).

```csharp
using static IceCream.IceCream;
IC_IncludeLineNumber(false);
ic("Hello, World!");
```
```
>>> ic| Program > <Main>$: param_0 = Hello, World!
```
