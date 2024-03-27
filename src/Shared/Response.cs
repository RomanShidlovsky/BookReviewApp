namespace Shared;

public class Response
{
    public bool Succeeded { get; protected set; }
    public Error Error { get; protected set; }

    protected Response(bool success, Error error)
    {
        if (success ^ error == Error.None)
            throw new InvalidOperationException();

        Succeeded = success;
        Error = error;
    }

    public static Response Success() => new(true, Error.None);
    public static Response Failure(Error error) => new(false, error);

    public static Response<T> Success<T>(T value) => new(value, true, Error.None);
    public static Response<T> Failure<T>(Error error, T value = default) => new(value, false, error); 
}