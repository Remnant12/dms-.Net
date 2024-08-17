using Microsoft.EntityFrameworkCore;
using ShipmentService.dbConfig;
using ShipmentService.Services.Implementation;
using ShipmentService.Services.Interface;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ShipmentDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"), 
        new MySqlServerVersion(new Version(8, 0, 23))));

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
    });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
builder.Services.AddScoped<ITrackingNumberGenerator, TrackingNumberGenerator>();
builder.Services.AddHttpClient<ICustomerService, CustomerServiceClient>(client =>
{
    client.BaseAddress = new Uri("https://customer-service-url");
});
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseCors("AllowSpecificOrigins");
app.MapControllers();
app.Run();
