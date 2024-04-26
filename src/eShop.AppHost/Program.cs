using eShop.AppHost;

var builder = DistributedApplication.CreateBuilder(args);

builder.AddForwardedHeaders();

var rabbitMq = builder.AddRabbitMQ("eventbus");
var postgres = builder.AddPostgres("postgres")
    .WithImage("ankane/pgvector")
    .WithImageTag("latest");

var orderDb = postgres.AddDatabase("orderingdb");

// Services

var orderingApi = builder.AddProject<Projects.Ordering_API>("ordering-api")
    .WithReference(rabbitMq)
    .WithReference(orderDb);

//builder.AddProject<Projects.OrderProcessor>("order-processor")
//    .WithReference(rabbitMq)
//    .WithReference(orderDb);

builder.Build().Run();
