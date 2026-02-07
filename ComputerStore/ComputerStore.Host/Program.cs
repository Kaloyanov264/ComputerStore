using ComputerStore.Host.HealthChecks;
using FluentValidation;
using Mapster;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;
using ComputerStore.Host.Validators;
using ComputerStore.DL;
using ComputerStore.BL;

namespace ComputerStore.Host
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(builder.Configuration)
                .Enrich.FromLogContext()
                .WriteTo.Console(theme: AnsiConsoleTheme.Code)
                .CreateLogger();
            // Add services to the container.

            builder.Services
                .AddDataLayer(builder.Configuration)
                .AddBusinessLayer();

            builder.Services.AddMapster();

            builder.Services.AddValidatorsFromAssemblyContaining<AddComputerRequestValidator>();

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Computer Store", Version = "v1" });
            });

            builder.Host.UseSerilog();

            builder.Services
                .AddHealthChecks()
                .AddCheck<MyCustomHealthCheck>("sample1")
                .AddCheck<MongoHealthCheck>("sample2");

            var app = builder.Build();

            app.MapHealthChecks("/healthz");

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("v1/swagger.json", "My API V1");
            });

            app.UseSwagger();

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
