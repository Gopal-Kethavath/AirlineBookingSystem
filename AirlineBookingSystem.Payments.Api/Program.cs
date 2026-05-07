using AirlineBookingSystem.BuildingBlocks.Common;
using AirlineBookingSystem.BuildingBlocks.Contracts.EventBus.Messages;
using AirlineBookingSystem.Payments.Application.Consumers;
using AirlineBookingSystem.Payments.Application.Handlers;
using AirlineBookingSystem.Payments.Core.Repositories;
using AirlineBookingSystem.Payments.Infrastructure.Repositories;
using MassTransit;
using Microsoft.Data.SqlClient;
using Npgsql;
using System.Data;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//MediatR registration
var assemblies = new Assembly[]
{
    Assembly.GetExecutingAssembly(),
    typeof(ProcessPaymentHandler).Assembly,
    typeof(RefundPaymentHandler).Assembly
};
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assemblies));

//Application services
builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();

//MassTransit configuration
builder.Services.AddMassTransit(cfg =>
{
    //Mark this as consumer 
    cfg.AddConsumer<FlightBookedEventConsumer>();
    cfg.UsingRabbitMq((context, config) =>
    {
        //config.Host(builder.Configuration["EventBusSettings:HostAddress"]);

        config.Host(
              new Uri($"amqps://{builder.Configuration["RabbitMQ:Host"]}/{builder.Configuration["RabbitMQ:VHost"]}"),
              h =>
              {
                  h.Username(builder.Configuration["RabbitMQ:Username"]!);
                  h.Password(builder.Configuration["RabbitMQ:Password"]!);
              });
        config.ReceiveEndpoint(EventBusConstants.FlightBookQueue, e =>
        {
            e.ConfigureConsumer<FlightBookedEventConsumer>(context);
        });
    });
});

//Add Sql Server connection
//builder.Services.AddScoped<IDbConnection>(sp => new SqlConnection(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add PostgreSQL connection
builder.Services.AddScoped<IDbConnection>(sp =>
    new NpgsqlConnection(
        builder.Configuration.GetConnectionString("DefaultConnection")));

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
