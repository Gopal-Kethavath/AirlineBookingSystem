using AirlineBookingSystem.Bookings.Application.Consumers;
using AirlineBookingSystem.Bookings.Application.Handlers;
using AirlineBookingSystem.Bookings.Core.Repositories;
using AirlineBookingSystem.Bookings.Infrastructure.Repositories;
using AirlineBookingSystem.BuildingBlocks.Common;
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

//Register MediatR
var assemblies = new Assembly[]
{
    Assembly.GetExecutingAssembly(),
    typeof(CreateBookingHandler).Assembly,
    typeof(GetBookingHandler).Assembly
};
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assemblies));

//Application services
builder.Services.AddScoped<IBookingRepository, BookingRepository>();

//MassTransit configuration
builder.Services.AddMassTransit(cfg =>
{
    //Mark this as consumer 
    cfg.AddConsumer<NotificationEventConsumer>();
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
        config.ReceiveEndpoint(EventBusConstants.NotificationQueue, e =>
        {
            e.ConfigureConsumer<NotificationEventConsumer>(context);
        });
    });
});

// Add SQL Server connection
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
