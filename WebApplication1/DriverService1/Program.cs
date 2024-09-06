using DriverService1.dbConfig;
using DriverService1.Models;
using DriverService1.Service;
using Microsoft.EntityFrameworkCore;
using RabbitMQ.Client;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<DriverDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"), 
        new MySqlServerVersion(new Version(8, 0, 23))));

builder.Services.AddControllers(); 
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();

// Register the ConnectionFactory
builder.Services.AddSingleton<IConnectionFactory>(sp =>
{
    return new ConnectionFactory
    {
        HostName = "localhost", // Set this to your RabbitMQ server's hostname or IP
        UserName = "guest", // Your RabbitMQ username
        Password = "guest" // Your RabbitMQ password
    };
});

// Register the IConnection using the ConnectionFactory
builder.Services.AddSingleton<IConnection>(sp =>
{
    try
    {
        var connectionFactory = sp.GetRequiredService<IConnectionFactory>();
        return connectionFactory.CreateConnection();
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Exception creating RabbitMQ connection: {ex.Message}");
        throw;
    }
});

builder.Services.AddSingleton<SharedStateService>();
builder.Services.AddHostedService<RabbitMqListenerService>();

// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins",
        builder =>
        {
            builder.WithOrigins("http://localhost:3001") // Next.js development server
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();
        });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseCors("AllowSpecificOrigins");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
