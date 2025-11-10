using Fortytwo.PracticalTest.Application.Abstractions.Common;
using Microsoft.AspNetCore.Mvc;

namespace Fortytwo.PracticalTest.Api.Extensions;

public static class OperationResultExtensions
{

    public static IResult ToEndpointResult<T, TMappedResponseValue>(this OperationResult<T> operationResult, Func<T, TMappedResponseValue> mapFunction)
    {
        return operationResult.IsSuccess
            ? Results.Ok(mapFunction(operationResult.Value))
            : operationResult.OperationResultType switch
            {
                OperationResultType.NotFound => Results.NotFound(),
                OperationResultType.ValidationError => Results.BadRequest(operationResult),
                _ => Results.StatusCode(StatusCodes.Status500InternalServerError)
            };
    }
}