global using FlowersBEWebApi.Data;
global using Microsoft.EntityFrameworkCore;
using FlowersBEWebApi;
using Sentry;

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

    //Dependency injection with SimpleInjector
    ObjectContainer.Init(builder);

    var app = builder.Build();

    ObjectContainer.VerifyApp(app);

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