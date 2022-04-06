global using FlowersBEWebApi.Data;
global using Microsoft.EntityFrameworkCore;
using FlowersBEWebApi;

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