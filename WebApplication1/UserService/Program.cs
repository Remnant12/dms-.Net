using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using UserService.Data;

public class Program
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.ConfigureServices(services =>
                {
                    services.AddControllers();
                    services.AddEndpointsApiExplorer();

                    // Configure CORS
                    services.AddCors(options =>
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

                    services.AddSwaggerGen(c =>
                    {
                        c.SwaggerDoc("v1", new OpenApiInfo { Title = "UserService API", Version = "v1" });
                        var securityScheme = new OpenApiSecurityScheme
                        {
                            Name = "Bearer",
                            BearerFormat = "JWT",
                            Scheme = "bearer",
                            Description = "JWT Authorization header using the Bearer scheme.",
                            In = ParameterLocation.Header,
                            Type = SecuritySchemeType.Http,
                        };
                        c.AddSecurityDefinition("Bearer", securityScheme);
                        var securityRequirement = new OpenApiSecurityRequirement
                        {
                            {
                                new OpenApiSecurityScheme { Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" } },
                                new string[] {}
                            }
                        };
                        c.AddSecurityRequirement(securityRequirement);
                    });

                    services.AddAuthorization();
                    services.AddLogging(logging =>
                    {
                        logging.AddConsole();
                        logging.AddDebug();
                    });
                    services.AddDbContext<UserDbContext>(options =>
                        options.UseMySql("YourConnectionStringHere", new MySqlServerVersion(new Version(8, 0, 23))));
                })
                .Configure(app =>
                {
                    var env = app.ApplicationServices.GetRequiredService<IWebHostEnvironment>();

                    if (env.IsDevelopment())
                    {
                        app.UseDeveloperExceptionPage();
                        app.UseSwagger();
                        app.UseSwaggerUI(c =>
                        {
                            c.SwaggerEndpoint("/swagger/v1/swagger.json", "UserService API V1");
                            c.RoutePrefix = "swagger"; // Set Swagger UI at app's root
                        });
                    }
                    else
                    {
                        app.UseExceptionHandler("/Home/Error");
                        app.UseHsts();
                    }

                    app.UseHttpsRedirection();
                    app.UseRouting();

                    // Add the CORS middleware here before UseAuthorization
                    app.UseCors("AllowSpecificOrigins");

                    app.UseAuthorization();
                    app.UseEndpoints(endpoints =>
                    {
                        endpoints.MapControllers();
                    });
                });
            });
}
