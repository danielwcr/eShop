using Microsoft.AspNetCore.Http.HttpResults;

public static class OrdersApi
{
    public static RouteGroupBuilder MapOrdersApi(this RouteGroupBuilder app)
    {
        app.MapPost("/create-aggregate", CreateAggregateAsync);
        app.MapPost("/update-aggregate", UpdateAggregateAsync);
        app.MapGet("/get-query", GetQueryAsync);
        app.MapGet("/list-query", ListQueryAsync);

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
            var requestCreateCommand = new IdentifiedCommand<CreateAggregateCommand, bool>(command, requestId);

            services.Logger.LogInformation(
                "Sending command: {CommandName} - {IdProperty}: {CommandId} ({@Command})",
                requestCreateCommand.GetGenericTypeName(),
                nameof(requestCreateCommand.Id),
                requestCreateCommand.Id,
                requestCreateCommand);

            var commandResult = await services.Mediator.Send(requestCreateCommand);

            if (!commandResult)
            {
                return TypedResults.Problem(detail: "CreateAggregateCommand failed to process.", statusCode: 500);
            }

            return TypedResults.Ok();
        }
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

        var requestUpdateCommand = new IdentifiedCommand<UpdateAggregateCommand, bool>(command, requestId);

        services.Logger.LogInformation(
            "Sending command: {CommandName} - {IdProperty}: {CommandId} ({@Command})",
            requestUpdateCommand.GetGenericTypeName(),
            nameof(requestUpdateCommand.Command.OrderNumber),
            requestUpdateCommand.Command.OrderNumber,
            requestUpdateCommand);

        var commandResult = await services.Mediator.Send(requestUpdateCommand);

        if (!commandResult)
        {
            return TypedResults.Problem(detail: "UpdateAgregateCommand failed to process.", statusCode: 500);
        }

        return TypedResults.Ok();
    }

    public static async Task<Results<Ok<GetQueryDto>, NotFound>> GetQueryAsync([FromQuery] int orderId, [AsParameters] OrderServices services)
    {
        try
        {
            var order = await services.Queries.GetQueryAsync(orderId);
            return TypedResults.Ok(order);
        }
        catch
        {
            return TypedResults.NotFound();
        }
    }

    public static async Task<Ok<IEnumerable<ListQueryDto>>> ListQueryAsync([FromQuery] string userId, [AsParameters] OrderServices services)
    {
        var orders = await services.Queries.ListQueryAsync(userId);
        return TypedResults.Ok(orders);
    }
}
