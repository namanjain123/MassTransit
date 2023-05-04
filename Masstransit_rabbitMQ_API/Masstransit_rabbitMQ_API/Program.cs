using MassTransit;
using Masstransit_rabbitMQ_API;
using Masstransit_rabbitMQ_API.Command;
using Masstransit_rabbitMQ_API.Model;
using Microsoft.Extensions.Logging;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
var rabbitMqSettings = builder.Configuration.GetSection(nameof(RabbitMQ)).Get<RabbitMQSettings>();
builder.Services.AddMassTransit(mt => mt.AddMassTransit(x => {
    x.UsingRabbitMq((cntxt, cfg) => {
        cfg.Host(rabbitMqSettings.Connection, "/", c => {
            c.Username(rabbitMqSettings.User);
            c.Password(rabbitMqSettings.Password);
        });
        cfg.ReceiveEndpoint("samplequeue", (c) => {
            c.Consumer<CommandMessageConsumer>();
        });
    });
}));
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.MapPost("/sendmessage", (long id, string message, IPublishEndpoint publishEndPoint) => {
    publishEndPoint.Publish(new CommandMessage(id, message)); ;
});


app.Run();
