using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using UserService.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<UserDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"), 
        new MySqlServerVersion(new Version(8, 0, 23))));
 

builder.Services.AddControllers();
var jwtKey = builder.Configuration["Jwt:Key"];

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


// var key = Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Key"]);

var key = builder.Configuration["Jwt:Key"];

if (key != null)
{
    var keyBytes = Encoding.UTF8.GetBytes(key);

    builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.RequireHttpsMetadata = false;
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = builder.Configuration["Jwt:Issuer"],
                ValidAudience = builder.Configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(keyBytes),
            };
        });
}

// var key = Encoding.ASCII.GetBytes("ODo0EHIGJqjIeX8KJFCgZRY5Jyq46eiyqYCCg8kkcKYqssg4bzNh62AHDAfdIQf");
//
// builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//     .AddJwtBearer(options =>
//     {
//          options.RequireHttpsMetadata = false;
//          options.SaveToken = true;
//         options.TokenValidationParameters = new TokenValidationParameters
//         {
//             ValidateIssuerSigningKey = true,
//             IssuerSigningKey = new SymmetricSecurityKey(key),
//             ValidateIssuer = false,
//             ValidateAudience = false,
//             // ValidIssuer = "http://localhost:5001",
//             // ValidAudience = "http://localhost:5001",
//             ValidateLifetime = true,
//             ClockSkew = TimeSpan.Zero
//         };
//     });

builder.Services.AddControllers();


var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Your API v1"));
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseCors("AllowSpecificOrigins");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();

