namespace Fortytwo.PracticalTest.Application.Abstractions.Common;

public interface IRequestHandler<TRequest, TResponse> where TResponse : OperationResult
{
    Task<TResponse> HandleAsync(TRequest request, CancellationToken cancellationToken);
}
