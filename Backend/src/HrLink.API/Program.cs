using System.Reflection;
using FluentValidation;
using HrLink.API.Extensions;
using HrLink.API.Middlewares;
using HrLink.Application.Extensions;
using HrLink.Application.Interfaces;
using HrLink.Caching.Extensions;
using HrLink.Email;
using HrLink.Persistence.Extensions;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Serilog.Filters;
using Serilog.Sinks.OpenSearch;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddEnvironmentVariables();

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

builder.Services.Configure<SmtpOptions>(builder.Configuration.GetSection(nameof(SmtpOptions)));

builder.Logging.AddLogger(builder);
builder.Services.AddValidatorsFromAssembly(Assembly.GetAssembly(typeof(IEmailService)));
builder.Services.AddPersistence(builder.Configuration);
builder.Services.AddCaching(builder.Configuration);
builder.Services.AddEmail();
builder.Services.AddUseCases();
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.ApplyMigrations();

app.UseGlobalExceptionHandlerMiddleware();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "V1");
        options.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
