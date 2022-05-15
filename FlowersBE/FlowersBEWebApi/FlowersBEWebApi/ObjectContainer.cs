using FlowersBEWebApi.Repositories;
using FlowersBEWebApi.Repositories.Shops;
using FlowersBEWebApi.Services;
using Serilog.Core;
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

            var connectionString = builder.Configuration.GetConnectionString("Fullmaximum");
            _container.Register(() =>
            {
                var options = new DbContextOptionsBuilder<DataContext>();
                options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
                return new DataContext(options.Options, builder.Configuration);
            }, Lifestyle.Scoped);

            RegisterServices();
        }

        public static void VerifyApp(WebApplication application, Logger logger)
        {
            application.Services.UseSimpleInjector(_container);

            try
            {
                _container.Verify();
            }
            catch (Exception ex)
            {
                logger.Error($"[ObjectContainer] Verify: {ex}");
                throw;
            }
        }

        public static void RegisterServices()
        {
            //Repositories
            _container.Register<IBasicRepository, BasicRepository>(Lifestyle.Scoped);
            _container.Register<IShopRepository, ShopRepository>(Lifestyle.Scoped);

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
