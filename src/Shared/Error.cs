namespace Shared;

public class Error(string code, string message, int? errorStatusCode = 500)
{
    public static readonly Error None = new(string.Empty, string.Empty, 0);
    
    public string Code { get; } = code;
    public string Message { get; } = message;
    public int? ErrorStatusCode { get; } = errorStatusCode;
}