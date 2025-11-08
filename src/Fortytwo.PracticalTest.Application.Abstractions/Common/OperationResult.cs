namespace Fortytwo.PracticalTest.Application.Abstractions.Common;

public class OperationResult
{
    public OperationResultType OperationResultType { get; init; }
    public string? ErrorMessage { get; init; }
    public IEnumerable<string> ErrorDetails { get; init; } = Enumerable.Empty<string>();
    public bool IsSuccess => OperationResultType == OperationResultType.Success;

    protected OperationResult(OperationResultType operationResultType)
    {
        OperationResultType = operationResultType;
    }

    public static OperationResult Success()
        => new(OperationResultType.Success);

    public static OperationResult ValidationError(string validationError, IEnumerable<string>? errorDetails = null)
        => new(OperationResultType.ValidationError) { ErrorMessage = validationError, ErrorDetails = errorDetails ?? Enumerable.Empty<string>() };

    public static OperationResult NotFound(string? errorMessage = null)
        => new(OperationResultType.NotFound) { ErrorMessage = errorMessage };

    public static OperationResult Failure(string errorMessage)
        => new(OperationResultType.GeneralError) { ErrorMessage = errorMessage };
}

public class OperationResult<T> : OperationResult
{
    public T? Value { get; init; }

    private OperationResult(OperationResultType operationResultType) : base(operationResultType)
    {
    }

    public static OperationResult<T> Success(T value)
        => new(OperationResultType.Success) { Value = value };

    new public static OperationResult<T> ValidationError(string validationError, IEnumerable<string>? errorDetails = null)
        => new(OperationResultType.ValidationError) { ErrorMessage = validationError, ErrorDetails = errorDetails ?? Enumerable.Empty<string>() };

    new public static OperationResult<T> NotFound(string? errorMessage = null)
        => new(OperationResultType.NotFound) { ErrorMessage = errorMessage };

    new public static OperationResult<T> Failure(string? errorMessage = null)
        => new(OperationResultType.GeneralError) { ErrorMessage = errorMessage };
}