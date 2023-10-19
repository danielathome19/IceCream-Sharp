namespace IceCream;
using System;
using System.IO;
using System.Text;
using System.Diagnostics;

public static class IceCream {
    private static string prefix = "ic| ";
    private static bool enabled = true;
    private static bool includeContext = true;
    private static bool includeFilename = false;
    private static bool includeClassName = true;
    private static bool includeMethodNameAndLineNumber = true;
    private const string ERROR_CONTEXT_DISABLED = "includeContext needs to be enabled to set this config";

    public static void SetPrefix(string value="\uD83C\uDF66 ") => prefix = value;
    public static void IncludeContext(bool incContext) => includeContext = incContext;
    public static void Enable() => enabled = true;
    public static void Disable() => enabled = false;
    
    public static void IncludeFilename(bool flag) {
        if (!includeContext)
            throw new IceCreamException(ERROR_CONTEXT_DISABLED);
        includeFilename = flag;
    }

    public static void IncludeClassName(bool flag) {
        if (!includeContext)
            throw new IceCreamException(ERROR_CONTEXT_DISABLED);
        includeClassName = flag;
    }

    public static void IncludeMethodNameAndLineNumber(bool flag) {
        if (!includeContext)
            throw new IceCreamException(ERROR_CONTEXT_DISABLED);
        includeMethodNameAndLineNumber = flag;
    }

    public static object ic(params object[] objects) {
        if (enabled) Console.WriteLine(Format(objects));  // Maybe switch to logger
        return objects.Length == 1 ? objects[0] : objects;
    }

    private static CallerDetails GetCallerDetails() {
        var currentStackFrame = new StackFrame(3, true);
        return new CallerDetails()
            .SetClassName(currentStackFrame.GetMethod().DeclaringType.FullName)
            .SetMethodName(currentStackFrame.GetMethod().Name)
            .SetLineNumber(currentStackFrame.GetFileLineNumber().ToString())
            .SetFileName(currentStackFrame.GetFileName());
    }

    private static string Format(params object[] objects) {  // TODO: make this public; need to fix stack frame offsets
        CallerDetails callerDetails = GetCallerDetails();
        StringBuilder builder = new StringBuilder(prefix);
        if (includeContext) {
            if (includeFilename)
                builder.Append(Path.GetFileName(callerDetails.FileName)).Append(" > ");
            if (includeClassName)
                builder.Append(callerDetails.ClassName).Append(" > ");
            if (includeMethodNameAndLineNumber)
                builder.Append(callerDetails.MethodName).Append(":").Append(callerDetails.LineNumber).Append(" ");
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