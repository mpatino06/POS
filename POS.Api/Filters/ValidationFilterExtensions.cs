namespace POS.Api.Filters;

public static class ValidationFilterExtensions
{
    public static RouteHandlerBuilder AddRequestValidation<TRequest>(
        this RouteHandlerBuilder builder)
        where TRequest : class
    {
        builder.AddEndpointFilter<ValidationFilter<TRequest>>();

        return builder;
    }

}
