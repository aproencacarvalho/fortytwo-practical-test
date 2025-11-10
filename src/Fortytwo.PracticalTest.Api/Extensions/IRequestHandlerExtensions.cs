using System;
using Fortytwo.PracticalTest.Application.Abstractions.Common;
using Microsoft.AspNetCore.Identity.Data;

namespace Fortytwo.PracticalTest.Api.Extensions;

public static class IRequestHandlerExtensions
{
    public static async Task<OperationResult<TResponse>> ExecuteRequestHandler<TRequest, TResponse>(this IRequestHandler<TRequest, OperationResult<TResponse>> requestHandler, TRequest request, CancellationToken cancellationToken, ILogger logger)
    {
        try
        {
            return await requestHandler.HandleAsync(request, cancellationToken);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while processing the request {RequestType}.", typeof(TRequest).Name);
            return OperationResult<TResponse>.Failure("An error occurred while processing the request");
        }
    }
}
