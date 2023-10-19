namespace IceCream;

internal class CallerDetails {
    public string ClassName { get; set; }
    public string MethodName { get; set; }
    public string LineNumber { get; set; }
    public string FileName { get; set; }

    public CallerDetails SetClassName(string className) {
        ClassName = className;
        return this;
    }

    public CallerDetails SetMethodName(string methodName) {
        MethodName = methodName;
        return this;
    }

    public CallerDetails SetLineNumber(string lineNumber) {
        LineNumber = lineNumber;
        return this;
    }

    public CallerDetails SetFileName(string fileName) {
        FileName = fileName;
        return this;
    }
}