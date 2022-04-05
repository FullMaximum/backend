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



//builder.Services.AddDbContext<DataContext>(options =>
//{
//    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
//});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

//builder.Services.AddScoped<IBasicRepository, BasicRepository>();
//builder.Services.AddScoped<IBasicService, BasicService>();
