global using FlowersBEWebApi.Data;
global using Microsoft.EntityFrameworkCore;
using FlowersBEWebApi;
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

    var loggerConnStrings = builder.Configuration.GetConnectionString("LoggerStrings");
    var logger = new LoggerConfiguration()
        .WriteTo.AzureBlobStorage(connectionString: loggerConnStrings, outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level}] {Message}{NewLine}{Exception}")
        .WriteTo.File(new Serilog.Formatting.Raw.RawFormatter(), "C:\\temp\\flowersLogs.txt", rollingInterval: RollingInterval.Day)
        .CreateLogger();

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

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}