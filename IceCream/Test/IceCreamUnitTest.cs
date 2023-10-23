namespace IceCream.Test;
using System;
using System.IO;
using Xunit;
using static IceCream;

[TestCaseOrderer(ordererTypeName: "XUnit.Project.Orderers.AlphabeticalOrderer", ordererAssemblyName: "XUnit.Project")]

public class IceCreamUnitTest {
    public IceCreamUnitTest() {
        // Reset configuration
        IC_SetPrefix("\uD83C\uDF66 ");
        IC_IncludeContext(true);
        IC_IncludeFilename(false);
        IC_IncludeAbsolutePath(false);
        IC_IncludeClassName(true);
        IC_IncludeMethodName(true);
        IC_IncludeLineNumber(true);
        IC_Enable();
    }
    
    [Fact]
    public void Test0SimplePrintTest() {
        using var sw = new StringWriter();
        Console.SetOut(sw);
        ic();
        var result = sw.ToString().Trim();
        Assert.Contains("\uD83C\uDF66 IceCream.Test.IceCreamUnitTest > Test0SimplePrintTest:", result);
    }

    [Fact]
    public void Test1WithParamsTest() {
        using var sw = new StringWriter();
        Console.SetOut(sw);
        ic(1, 2, 3);
        var result = sw.ToString().Trim();
        Assert.Contains("\uD83C\uDF66 IceCream.Test.IceCreamUnitTest > Test1WithParamsTest:", result);
        Assert.Contains(" > param_0 = 1 | param_1 = 2 | param_2 = 3", result);
    }

    [Fact]
    public void Test2WithMethodAsParamTest() {
        using var sw = new StringWriter();
        Console.SetOut(sw);
        ic(Concat("1", "2"));
        var result = sw.ToString().Trim();
        Assert.Contains("\uD83C\uDF66 IceCream.Test.IceCreamUnitTest > Test2WithMethodAsParamTest:", result);
        Assert.Contains(" > param_0 = 12", result);
    }
    
    
    [Fact]
    public void Test3FormatTest() {
        using var sw = new StringWriter();
        Console.SetOut(sw);
        ic(1);
        IC_Disable();
        IC_SetPrefix("ic |");
        var result= ic(2); var result2 = IC_Format(2);
        Assert.Equal(result, result2);
        IC_Enable();
        ic(3);
        IC_SetPrefix();
    }
    
    [Fact]
    public void Test4IncludeFileNameTest() {
        using var sw = new StringWriter();
        Console.SetOut(sw);
        IC_IncludeFilename(true);
        ic();
        IC_IncludeFilename(false);
        var result = sw.ToString().Trim();
        Assert.Contains("\uD83C\uDF66 IceCreamUnitTest.cs > IceCream.Test.IceCreamUnitTest > Test4IncludeFileNameTest:", result);
    }
    
    [Fact]
    public void Test5IncludeAbsolutePathTest() {
        using var sw = new StringWriter();
        Console.SetOut(sw);
        IC_IncludeAbsolutePath(true);
        ic();
        IC_IncludeAbsolutePath(false);
        var result = sw.ToString().Trim();
        Assert.Contains("IceCreamUnitTest.cs > IceCream.Test.IceCreamUnitTest > Test5IncludeAbsolutePathTest:", result);
    }
    
    [Fact]
    public void Test6DisableMethodAndLineNumberTest() {
        using var sw = new StringWriter();
        Console.SetOut(sw);
        IC_IncludeMethodName(false);
        ic();
        var result = sw.ToString().Trim();
        Assert.Contains("\uD83C\uDF66 IceCream.Test.IceCreamUnitTest > 93:", result);
        
        sw.GetStringBuilder().Clear();
        IC_IncludeMethodName(true);
        IC_IncludeLineNumber(false);
        ic();
        result = sw.ToString().Trim();
        Assert.Contains("\uD83C\uDF66 IceCream.Test.IceCreamUnitTest > Test6DisableMethodAndLineNumberTest:", result);
    }

    [Fact]
    public void Test7DisableContextTest() {
        using var sw = new StringWriter();
        Console.SetOut(sw);
        IC_IncludeClassName(false);
        IC_IncludeMethodName(false);
        IC_IncludeLineNumber(false);
        IC_IncludeContext(false);
        IC_SetPrefix("icc |");
        ic();
        Assert.Equal("icc |", sw.ToString().Trim());
    }

    [Fact]
    public void Test8FunctionPrefixTest()
    {
        using var sw = new StringWriter();
        Console.SetOut(sw);
        IC_SetPrefix(() => $"{DateTimeOffset.Now.ToUnixTimeSeconds()} |> ");
        ic();
        var result = sw.ToString().Trim();
        Assert.Contains(" |> IceCream.Test.IceCreamUnitTest > Test8FunctionPrefixTest:", result);
    }
    
    [Fact]
    public void Test9VerifyThrowsTest() {
        IC_IncludeContext(false);
        Assert.Throws<IceCreamException>(() => IC_IncludeClassName(false));
        Assert.Throws<IceCreamException>(() => IC_IncludeFilename(false));
        Assert.Throws<IceCreamException>(() => IC_IncludeMethodName(false));
        Assert.Throws<IceCreamException>(() => IC_IncludeLineNumber(false));
    }

    private static string Concat(string a, string b) => a + b;
}
