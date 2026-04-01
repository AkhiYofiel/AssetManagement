namespace AssetManagementApi.Services.Results;

public sealed class OperationResult
{
    private OperationResult(bool succeeded, bool isNotFound, IReadOnlyCollection<string> errors)
    {
        Succeeded = succeeded;
        IsNotFound = isNotFound;
        Errors = errors;
    }

    public bool Succeeded { get; }
    public bool IsNotFound { get; }
    public IReadOnlyCollection<string> Errors { get; }

    public static OperationResult Success() => new(true, false, Array.Empty<string>());

    public static OperationResult NotFound() => new(false, true, Array.Empty<string>());

    public static OperationResult ValidationFailed(IReadOnlyCollection<string> errors) => new(false, false, errors);
}

public sealed class OperationResult<T>
{
    private OperationResult(bool succeeded, bool isNotFound, T? data, IReadOnlyCollection<string> errors)
    {
        Succeeded = succeeded;
        IsNotFound = isNotFound;
        Data = data;
        Errors = errors;
    }

    public bool Succeeded { get; }
    public bool IsNotFound { get; }
    public T? Data { get; }
    public IReadOnlyCollection<string> Errors { get; }

    public static OperationResult<T> Success(T data) => new(true, false, data, Array.Empty<string>());

    public static OperationResult<T> NotFound() => new(false, true, default, Array.Empty<string>());

    public static OperationResult<T> ValidationFailed(IReadOnlyCollection<string> errors) => new(false, false, default, errors);
}
