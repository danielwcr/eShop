using Microsoft.AspNetCore.Http.HttpResults;

public static class OrdersApi
{
    public static RouteGroupBuilder MapOrdersApi(this RouteGroupBuilder app)
    {
        app.MapPost("/create-aggregate", CreateAggregateAsync);
        app.MapGet("/get-details-by-filter", GetDetailsByFilterAsync);
        app.MapGet("/get-aggregate", GetAggregateAsync);

        return app;
    }

    public static async Task<Results<Ok, BadRequest<string>, ProblemHttpResult>> CreateAggregateAsync(
     [FromHeader(Name = "x-requestid")] Guid requestId,
     CreateAggregateCommand command,
     [AsParameters] OrderServices services)
    {
        if (requestId == Guid.Empty)
        {
            return TypedResults.BadRequest("RequestId is missing.");
        }

        using (services.Logger.BeginScope(new List<KeyValuePair<string, object>> { new("IdentifiedCommandId", requestId) }))
        {
            var identifiedCommand = new IdentifiedCommand<CreateAggregateCommand, bool>(command, requestId);

            services.Logger.LogInformation(
                "Sending command: {CommandName} - {IdProperty}: {CommandId} ({@Command})",
                identifiedCommand.GetGenericTypeName(),
                nameof(identifiedCommand.Id),
                identifiedCommand.Id,
                identifiedCommand);

            var commandResult = await services.Mediator.Send(identifiedCommand);

            if (!commandResult)
            {
                return TypedResults.Problem(detail: "UpdateAggregateCommand failed to process.", statusCode: 500);
            }

            return TypedResults.Ok();
        }
    }

    public static async Task<Ok<IEnumerable<DetailsViewModel>>> GetDetailsByFilterAsync(
        [FromQuery] string filter,
        [AsParameters] OrderServices services)
    {
        var result = await services.Queries.GetDetailsByFilterAsync(filter);
        return TypedResults.Ok(result);
    }

    public static async Task<Ok<DetailsViewModel>> GetAggregateAsync(
       [FromQuery] int id,
       [AsParameters] OrderServices services)
    {
        var result = await services.Queries.GetAggregateAsync(id);
        return TypedResults.Ok(result);
    }
}
