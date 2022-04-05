﻿using FlowersBEWebApi.Repositories;
using FlowersBEWebApi.Services;
using SimpleInjector;
using SimpleInjector.Lifestyles;

namespace FlowersBEWebApi
{
    public static class ObjectContainer
    {
        private static Container _container;

        public static void Init(WebApplicationBuilder builder)
        {
            _container = new Container();

            _container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();
            builder.Services.AddSimpleInjector(_container, options =>
            {
                options.AddAspNetCore().AddControllerActivation();
            });

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            _container.Register(() =>
            {
                var options = new DbContextOptionsBuilder<DataContext>();
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
                return new DataContext(options.Options);
            }, Lifestyle.Scoped);

            RegisterServices();
        }

        public static void VerifyApp(WebApplication application)
        {
            application.Services.UseSimpleInjector(_container);

            try
            {
                _container.Verify();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ObjectContainer] Verify: {ex}"); //need to change to logging later
                throw;
            }
        }

        public static void RegisterServices()
        {
            //Repositories
            _container.Register<IBasicRepository, BasicRepository>(Lifestyle.Scoped);

            //Services
            _container.Register<IBasicService, BasicService>(Lifestyle.Scoped);
        }

        public static T GetInstance<T>() where T : class
        {
            var service = _container.GetInstance<T>();

            if (service == null)
                throw new NullReferenceException(string.Format("Requested service of type {0}, but null was found.",
                    typeof(T).FullName));

            return service;
        }
    }
}
