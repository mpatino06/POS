using FluentValidation;
using POS.Application.Commons.Bases;
using POS.Utilities.Statics;

namespace POS.Api.Filters;

//Generic validation filter for request objects using FluentValidation
public sealed class ValidationFilter<TRequest> : IEndpointFilter
    where TRequest : class
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {

        //Generic filter to be used only for request and find out automatically the Dto to validate
        var request = context.Arguments
            .OfType<TRequest>()
            .FirstOrDefault();

        if (request is null)
        {
            return await next(context);
        }

        var validator = context.HttpContext.RequestServices
            .GetService<IValidator<TRequest>>();

        if (validator is null)
        {
            return await next(context);
        }

        var validationResult = await validator.ValidateAsync(
            request,
            context.HttpContext.RequestAborted);

        if (!validationResult.IsValid)
        {
            //return Results.ValidationProblem(
            //    validationResult.ToDictionary());

            // Return a custom response with validation errors
            return Results.BadRequest(
                new BaseResponse<object>
                {
                    IsSuccess = false,
                    Message = ReplyMessage.MESSAGE_VALIDATE,
                    Errors = validationResult.Errors
                });
        }

        return await next(context);

    }
}
