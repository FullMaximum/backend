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

    var loggerConnStrings = "DefaultEndpointsProtocol=https;AccountName=fullmaximumstorage;AccountKey=D1yj+2eTqHooxc/nT5s2/O8uRaJUS8LCMXsxB2mBCLDsESLn50qYMLmLUnosnpcaj3J3B1N+GD89yG3mdrCbcg==;EndpointSuffix=core.windows.net";
    var logger = new LoggerConfiguration()
        .WriteTo.AzureBlobStorage(connectionString: loggerConnStrings, outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level}] {Message}{NewLine}{Exception}")
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