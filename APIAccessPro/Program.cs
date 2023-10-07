using APIAccessProDependencies.Helpers.Common;
using APIAccessProDependencies.Helpers.ConfigurationSettings;
using APIAccessProDependencies.Interfaces;
using APIAccessProDependencies.Repositories;
using MerchantTransactionCore.Helpers.Extensions;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using Serilog.Sinks.SystemConsole.Themes;
using Serilog;
using APIAccessProDependencies.Helpers.Logger;
using Microsoft.AspNetCore.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Configuration of CORS Policy.
builder.Services.AddCors(options =>
{
    options.AddPolicy("CORSPolicy", builder =>
    {
        builder.AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin();
    });
});

builder.Services.AddControllers()
    .AddNewtonsoftJson(opt =>
    {
        opt.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        opt.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Error;
    });

//Configuration for my context accessor
IHttpContextAccessor httpContextAccessor = new HttpContextAccessor();
MyHttpContextAccessor.HttpContextAccessor = httpContextAccessor;

// Configuration for config
builder.Configuration.AddJsonFile("appsettings.json");
ConfigurationSettingsHelper.Configuration = builder.Configuration;

// Use Serilog for logging
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration) // Read Serilog settings from appsettings.json
    .WriteTo.Console(theme: AnsiConsoleTheme.Literate, applyThemeToRedirectedOutput: true)
    .CreateLogger();
builder.Logging.AddSerilog();

//Configure my log writer
using (var scope = builder.Services.BuildServiceProvider().CreateScope())
{
    var loggerExtension = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
    LogWriter.Logger = loggerExtension;
}

//Configure dependency lifetimes
builder.Services.AddHttpContextAccessor();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCosmosDBServices();
builder.Services.AddScoped<IInputValidation, InputValidation>();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { 
        Title = "Capital Placement Collections", 
        Version = "v1", 
        Description = "Manage your Programs using Capital Placement Robust API" 
    });
    c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
});

var app = builder.Build();

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();
app.UseCors("CORSPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();