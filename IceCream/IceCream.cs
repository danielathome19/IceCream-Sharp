namespace IceCream;
using System;
using System.IO;
using System.Text;
using System.Diagnostics;

public static class IceCream {
    private static Func<string> prefix = () => "ic| ";
    private static bool enabled = true;
    private static bool includeContext = true;
    private static bool includeFilename = false;
    private static bool includeClassName = true;
    private static bool includeAbsolutePath = false;
    private static bool includeMethodName = true;
    private static bool includeLineNumber = true;
    private const string ERROR_CONTEXT_DISABLED = "includeContext needs to be enabled to set this config";

    public static void IC_SetPrefix(string value="\uD83C\uDF66 ") => prefix = () => value;
    public static void IC_SetPrefix(Func<string> func) => prefix = func;
    public static void IC_IncludeContext(bool incContext) => includeContext = incContext;
    public static void IC_Enable() => enabled = true;
    public static void IC_Disable() => enabled = false;
    public static string IC_Format(params object[] objects) => Format(offset: 3, objects);
    
    public static void IC_IncludeFilename(bool flag) {
        if (!includeContext) throw new IceCreamException(ERROR_CONTEXT_DISABLED);
        includeFilename = flag;
    }

    public static void IC_IncludeClassName(bool flag) {
        if (!includeContext) throw new IceCreamException(ERROR_CONTEXT_DISABLED);
        includeClassName = flag;
    }
    
    public static void IC_IncludeAbsolutePath(bool flag) {
        if (!includeContext) throw new IceCreamException(ERROR_CONTEXT_DISABLED);
        includeAbsolutePath = flag;
    }

    public static void IC_IncludeMethodName(bool flag) {
        if (!includeContext) throw new IceCreamException(ERROR_CONTEXT_DISABLED);
        includeMethodName = flag;
    }
    
    public static void IC_IncludeLineNumber(bool flag) {
        if (!includeContext) throw new IceCreamException(ERROR_CONTEXT_DISABLED);
        includeLineNumber = flag;
    }

    public static object ic(params object[] objects) {
        if (!enabled) return Format(offset: 3, objects);
        Console.WriteLine(Format(offset: 3, objects));  // Maybe switch to logger
        return objects.Length == 1 ? objects[0] : objects;
    }

    private static CallerDetails GetCallerDetails(int stackFrameOffset=3) {
        var currentStackFrame = new StackFrame(stackFrameOffset, true);
        return new CallerDetails()
            .SetClassName(currentStackFrame.GetMethod().DeclaringType.FullName)
            .SetMethodName(currentStackFrame.GetMethod().Name)
            .SetLineNumber(currentStackFrame.GetFileLineNumber().ToString())
            .SetFileName(currentStackFrame.GetFileName());
    }

    private static string Format(int offset=2, params object[] objects) {
        CallerDetails callerDetails = GetCallerDetails(offset);
        StringBuilder builder = new StringBuilder(prefix());
        if (includeContext) {
            if (includeFilename && !includeAbsolutePath)
                builder.Append(Path.GetFileName(callerDetails.FileName)).Append(" > ");
            else if (includeAbsolutePath)
                builder.Append(callerDetails.FileName).Append(" > ");
            if (includeClassName)
                builder.Append(callerDetails.ClassName).Append(" > ");
            if (includeMethodName && includeLineNumber)
                builder.Append(callerDetails.MethodName).Append(':').Append(callerDetails.LineNumber).Append(' ');
            else if (includeMethodName)
                builder.Append(callerDetails.MethodName).Append(": ");
            else if (includeLineNumber)
                builder.Append(callerDetails.LineNumber).Append(": ");
            if (objects.Length > 0)
                builder.Append("> ");
        }

        for (int i = 0; i < objects.Length; i++) {
            builder.Append("param_").Append(i).Append(" = ").Append(objects[i]);
            if ((i + 1) != objects.Length) builder.Append(" | ");
        }

        return builder.ToString();
    }
}