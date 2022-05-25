using Microsoft.EntityFrameworkCore;
using RideAdministration.API.Application.Queries;
using RideAdministration.API.Infrastructure.Filters;
using RideAdministration.Domain.OrderAggregate;
using RideAdministration.Domain.RideAggregate;
using RideAdministration.Infrastructure;
using RideAdministration.Infrastructure.Repositories;
using Serilog;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((ctx, lc) => lc
    .WriteTo.Console()
    .WriteTo.File("logs/log.txt", rollingInterval: RollingInterval.Day));

builder.Services.AddControllers(options => {
    options.Filters.Add(typeof(HttpGlobalExceptionFilter));
}).AddNewtonsoftJson(options => {
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(typeof(Program).Assembly);
builder.Services.AddMediatR(typeof(RideAdministration.Domain.Events.RideStopAddedEvent).Assembly);
builder.Services.AddApplicationInsightsTelemetry();

builder.Services.AddScoped<IOrderQueries>(s => new OrderQueries(builder.Configuration.GetConnectionString("Default")!));

builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IRideRepository, RideRepository>();



builder.Services.AddDbContext<RideContext>(options =>
        {
            options.UseSqlServer(builder.Configuration.GetConnectionString("Default"),
                sqlServerOptionsAction: sqlOptions =>
                {
                    sqlOptions.MigrationsAssembly(typeof(RideContext).GetTypeInfo().Assembly.GetName().Name);
                    sqlOptions.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                });
        },
            ServiceLifetime.Scoped  //Showing explicitly that the DbContext is shared across the HTTP request scope (graph of objects started in the HTTP request)
        );

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
