namespace IceCream.Test;
using System;
using System.IO;
using Xunit;
using IC = IceCream;

[TestCaseOrderer(ordererTypeName: "XUnit.Project.Orderers.AlphabeticalOrderer", ordererAssemblyName: "XUnit.Project")]

public class IceCreamUnitTest {
    public IceCreamUnitTest() {
        // Reset configuration
        IC.SetPrefix("\uD83C\uDF66 ");
        IC.IncludeContext(true);
        IC.IncludeFilename(false);
        IC.IncludeClassName(true);
        IC.IncludeMethodNameAndLineNumber(true);
        IC.Enable();
    }
    
    [Fact]
    public void Test1SimplePrintTest() {
        using var sw = new StringWriter();
        Console.SetOut(sw);
        IC.ic();
        var result = sw.ToString().Trim();
        Assert.Contains("\uD83C\uDF66 IceCream.Test.IceCreamUnitTest > Test1SimplePrintTest:", result);
    }

    [Fact]
    public void Test2WithParamsTest() {
        using var sw = new StringWriter();
        Console.SetOut(sw);
        IC.ic(1, 2, 3);
        var result = sw.ToString().Trim();
        Assert.Contains("\uD83C\uDF66 IceCream.Test.IceCreamUnitTest > Test2WithParamsTest:", result);
        Assert.Contains(" > param_0 = 1 | param_1 = 2 | param_2 = 3", result);
    }

    [Fact]
    public void Test3WithMethodAsParamTest() {
        using var sw = new StringWriter();
        Console.SetOut(sw);
        IC.ic(Concat("1", "2"));
        var result = sw.ToString().Trim();
        Assert.Contains("\uD83C\uDF66 IceCream.Test.IceCreamUnitTest > Test3WithMethodAsParamTest:", result);
        Assert.Contains(" > param_0 = 12", result);
    }

    [Fact]
    public void Test4IncludeFileNameTest() {
        using var sw = new StringWriter();
        Console.SetOut(sw);
        IceCream.IncludeFilename(true);
        IC.ic();
        IceCream.IncludeFilename(false);
        var result = sw.ToString().Trim();
        Assert.Contains("\uD83C\uDF66 IceCreamUnitTest.cs > IceCream.Test.IceCreamUnitTest > Test4IncludeFileNameTest:", result);
    }

    [Fact]
    public void Test5DisableContextTest() {
        using var sw = new StringWriter();
        Console.SetOut(sw);
        IceCream.IncludeClassName(false);
        IceCream.IncludeMethodNameAndLineNumber(false);
        IceCream.IncludeContext(false);
        IceCream.SetPrefix("icc |");
        IC.ic();
        Assert.Equal("icc |", sw.ToString().Trim());
    }

    [Fact]
    public void Test6VerifyThrowsTest() {
        IceCream.IncludeContext(false);
        Assert.Throws<IceCreamException>(() => IceCream.IncludeClassName(false));
        Assert.Throws<IceCreamException>(() => IceCream.IncludeFilename(false));
        Assert.Throws<IceCreamException>(() => IceCream.IncludeMethodNameAndLineNumber(false));
    }

    private static string Concat(string a, string b) => a + b;
    

    [Fact]
    public void Test7MiscellaneousTests()
    {
        IC.ic(1);
        IC.Disable();
        var result = IC.ic(2);
        Assert.Equal(2, result);
        IC.Enable();
        IC.ic(3);
    }
}
