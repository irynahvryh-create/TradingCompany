using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Windows;
using TradingCompany.BL.Concrete;
using TradingCompany.BL.Interfaces;
using TradingCompany.WPF;
using TradingCompany.WPF.ViewModels;
using TradingCompany.WPF.Windows;
using TradingCompany.BL.Concrete;
using TradingCompany.BL.Interfaces;
using TradingCompany.DAL.Concrete;
using TradingCompany.DAL.EF.Concrete;
using TradingCompany.DAL.Interfaces;
using TradingCompany.WPF.ViewModels;
using Microsoft.Extensions.Logging.Console;

using TradingCompany.DAL.EF.MapperProfiles;
using TradingCompany.WPF.Windows;



namespace TradingCompany.WPF
{
    public partial class App : Application
    {
        public static IServiceProvider? Services { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            Services = BuildServiceProvider();

            Current.ShutdownMode = ShutdownMode.OnExplicitShutdown;

            // Тут можна додати логін, якщо потрібна аутентифікація
            // Наприклад: var login = Services.GetRequiredService<Login>();
            // bool result = login.ShowDialog() ?? false;
            bool result = true; // тимчасово дозволяємо одразу

            if (result)
            {
                Current.MainWindow = Services.GetRequiredService<CategoryListMVVM>();
                Current.ShutdownMode = ShutdownMode.OnMainWindowClose;
                Current.MainWindow.Show();
            }
            else
            {
                Current.Shutdown();
            }
        }

        private static IServiceProvider BuildServiceProvider()
        {
            var services = new ServiceCollection();

            // Logging
            services.AddLogging(builder =>
            {
                builder.AddConsole().SetMinimumLevel(LogLevel.Information);
            });

            // Конфігурація
            IConfiguration configuration = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("config.json", optional: true).Build();
            services.AddSingleton<IConfiguration>(configuration);


            services.AddSingleton<IConfiguration>(configuration);

            // AutoMapper
            services.AddSingleton<IMapper>(sp =>
            {
                // var loggerFactory = sp.GetRequiredService<ILoggerFactory>();
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.ConstructServicesUsing(sp.GetService);
                    cfg.AddMaps(typeof(Category_Map).Assembly); // усі профілі Mapper для DAL
                });

                return config.CreateMapper();
            });

            string connStr = configuration.GetConnectionString("TradingCompanyDB") ?? "";

            // DAL реєстрації
            services.AddTransient<ICategoryDal>(sp => new CategoryDalEF(connStr, sp.GetRequiredService<IMapper>()));
            //services.AddTransient<IProductDal>(sp => new ProductDalEF(connStr, sp.GetRequiredService<IMapper>()));
            //services.AddTransient<IManufactureDal>(sp => new ManufactureDalEF(connStr, sp.GetRequiredService<IMapper>()));
            //services.AddTransient<IProductLogDal>(sp => new ProductLogDalEF(connStr, sp.GetRequiredService<IMapper>()));

            // BL реєстрації
            services.AddTransient<ICategoryManager, CategoryManager>();
            //services.AddTransient<IProductManager, ProductManager>();
            //services.AddTransient<IManufactureManager, ManufactureManager>();
            //services.AddTransient<IProductLogManager, ProductLogManager>();

            // ViewModels
            services.AddTransient<CategoryListViewModel>();
            services.AddTransient<CategoryDetailsViewModel>();
            //services.AddTransient<ProductListViewModel>();
            //services.AddTransient<ProductDetailsViewModel>();

            // Windows
            services.AddTransient<CategoryListMVVM>();
            services.AddTransient<CategoryDetails>();
            //services.AddTransient<ProductListMVVM>();
            //services.AddTransient<ProductDetails>();

            return services.BuildServiceProvider();
        }
    }

}
