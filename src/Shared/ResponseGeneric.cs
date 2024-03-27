namespace Shared;

public class Response<T> : Response
{
    private readonly T _value;

    public T Value => Succeeded
        ? _value
        : throw new InvalidOperationException("Attempt to get failure value.");
    
    protected internal Response(T value, bool succeeded, Error error) : base(succeeded, error)
    {
        _value = value;
    }

    public static implicit operator Response<T>(T value) => Success(value);
}