global using FlowersBEWebApi.Data;
global using Microsoft.EntityFrameworkCore;
using FlowersBEWebApi;
using FlowersBEWebApi.Helpers;
using FlowersBEWebApi.Middleware;
using Sentry;
using Serilog;

using (SentrySdk.Init(o =>
{
    o.Dsn = "https://c51dbab596cc4d09bdc25c910835aac9@o1187302.ingest.sentry.io/6307231";
    o.Debug = true;
    o.TracesSampleRate = 1.0;
}))
{

    var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

    var loggerConnStrings = builder.Configuration.GetConnectionString("LoggerStrings");
    Serilog.Core.Logger logger = null;
    if (string.IsNullOrWhiteSpace(loggerConnStrings))
    {
        logger = new LoggerConfiguration()
        .WriteTo.File("C:\\temp\\flowersLogs.txt", rollingInterval: RollingInterval.Day, outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level}] {Message}{NewLine}{Exception}")
        .CreateLogger();
    }
    else
    {
        logger = new LoggerConfiguration()
        .WriteTo.AzureBlobStorage(connectionString: loggerConnStrings, outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level}] {Message}{NewLine}{Exception}")
        .WriteTo.File("C:\\temp\\flowersLogs.txt", rollingInterval: RollingInterval.Day, outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level}] {Message}{NewLine}{Exception}")
        .CreateLogger();
    }

    builder.Logging.ClearProviders();
    builder.Logging.AddSerilog(logger);

    //Dependency injection with SimpleInjector
    ObjectContainer.Init(builder);

    var app = builder.Build();

    ObjectContainer.VerifyApp(app, logger);

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseMiddleware<JwtMiddleware>();

    app.MapControllers();

    app.Run();
}