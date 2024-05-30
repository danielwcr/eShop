using Microsoft.AspNetCore.Http.HttpResults;

public static class OrdersApi
{
    public static RouteGroupBuilder MapOrdersApi(this RouteGroupBuilder app)
    {
        app.MapPost("/update-aggregate", UpdateAggregateAsync);
        app.MapGet("/list-query", ListQueryAsync);

        return app;
    }

    public static async Task<Results<Ok, BadRequest<string>, ProblemHttpResult>> UpdateAggregateAsync(
     [FromHeader(Name = "x-requestid")] Guid requestId,
     UpdateAggregateCommand command,
     [AsParameters] OrderServices services)
    {
        if (requestId == Guid.Empty)
        {
            return TypedResults.BadRequest("RequestId is missing.");
        }

        using (services.Logger.BeginScope(new List<KeyValuePair<string, object>> { new("IdentifiedCommandId", requestId) }))
        {
            var identifiedCommand = new IdentifiedCommand<UpdateAggregateCommand, bool>(command, requestId);

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

    public static async Task<Ok<IEnumerable<ListQueryDto>>> ListQueryAsync([FromQuery] string userId, [AsParameters] OrderServices services)
    {
        var orders = await services.Queries.ListQueryAsync(userId);
        return TypedResults.Ok(orders);
    }
}
