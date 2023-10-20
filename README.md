# IceCream\# — Never use Console.WriteLine() to debug in .NET again.

[![NuGet](https://img.shields.io/nuget/v/IceCream-Sharp.svg)](https://www.nuget.org/packages/IceCream-Sharp/)

IceCream# is a C# port of the [icecream](https://github.com/gruns/icecream) library for Python.

```csharp
using static IceCream.IceCream;

ic("Hello, World!");
```
```
>>> ic| Program > <Main>$:3 > param_0 = Hello, World!
```
