global using FlowersBEWebApi.Data;
global using Microsoft.EntityFrameworkCore;
using FlowersBEWebApi.Repositories;
using FlowersBEWebApi.Services;
using SimpleInjector;
using SimpleInjector.Lifestyles;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Dependency injection with SimpleInjector

var container = new Container();
container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
container.Register<DataContext>(() =>
{
    var options = new DbContextOptionsBuilder<DataContext>();
    options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
    return new DataContext(options.Options);
});
container.Register<IBasicRepository, BasicRepository>(Lifestyle.Scoped);
container.Register<IBasicService, BasicService>(Lifestyle.Scoped);

container.Verify();

var app = builder.Build();

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
