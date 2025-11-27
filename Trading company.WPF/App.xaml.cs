using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using System.IO;
using System.Windows;
using Trading_company.BL.Concrete;
using Trading_company.BL.Interfaces;
using TradingCompany.BL.Concrete;
using TradingCompany.BL.Concrete;
using TradingCompany.BL.Interfaces;
using TradingCompany.BL.Interfaces;
using TradingCompany.DAL.Concrete;
using TradingCompany.DAL.EF.Concrete;
using TradingCompany.DAL.EF.MapperProfiles;
using TradingCompany.DAL.Interfaces;
using TradingCompany.WPF;
using TradingCompany.WPF.ViewModels;
using TradingCompany.WPF.ViewModels;
using TradingCompany.WPF.Windows;
using TradingCompany.WPF.Windows;



namespace TradingCompany.WPF
{
    public partial class App : Application
    {
        public static IServiceProvider? Services { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            Services = BuildServiceProvider();


            //




            // У OnStartup, перед loginWindow.ShowDialog()
            var authManager = Services.GetRequiredService<IAuthManager>();
            var adminUser = authManager.GetUserByLogin("admin");

            if (adminUser == null)
            {
                // ✅ Цей виклик тепер спрацює, тому що SQL Server згенерує UserID
                authManager.CreateUser(
                    email: "admin@trading.com",
                    username: "admin",
                    password: "123456",
                    privilegeType: TradingCompany.DTO.PrivilegeType.Admin
                );
                MessageBox.Show("Тестовий адміністратор створений!", "Успіх");
            }



            //

            Current.ShutdownMode = ShutdownMode.OnExplicitShutdown;

            // Тут можна додати логін, якщо потрібна аутентифікація
            // Наприклад: var login = Services.GetRequiredService<Login>();
            // bool result = login.ShowDialog() ?? false;
            //bool result = true; // тимчасово дозволяємо одразу
            var loginWindow = Services.GetRequiredService<Login>();
            bool result = loginWindow.ShowDialog() ?? false;

            if (result)
            {
                var selectionWindow = Services.GetRequiredService<EntitySelectionWindow>();
                bool selectionResult = selectionWindow.ShowDialog() ?? false;

                if (!selectionResult)
                {
                    Current.Shutdown();
                    return;
                }

                if (selectionWindow.SelectedEntity == "Category")
                {
                    Current.MainWindow = Services.GetRequiredService<CategoryListMVVM>();
                }
                else if (selectionWindow.SelectedEntity == "Manufacturer")
                {
                    Current.MainWindow = Services.GetRequiredService<ManufacturerListMVVM>();
                }
                else if (selectionWindow.SelectedEntity == "Product")
                {
                    Current.MainWindow = Services.GetRequiredService<ProductListMVVM>();
                }
                else if (selectionWindow.SelectedEntity == "ProductLog")
                {
                    Current.MainWindow = Services.GetRequiredService<ProductLogListMVVM>();
                }


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
                builder.AddConsole()
                .SetMinimumLevel(LogLevel.Information);
            });

            // Конфігурація
            IConfiguration configuration = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("config.json", optional: true).Build();
            services.AddSingleton<IConfiguration>(configuration);


            // AutoMapper
            services.AddSingleton<IMapper>(sp =>
            {
                var loggerFactory = sp.GetRequiredService<ILoggerFactory>();
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.ConstructServicesUsing(sp.GetService);
                    cfg.AddMaps(typeof(Category_Map).Assembly); // усі профілі Mapper для DAL
                });

                return config.CreateMapper();
            });

            string connStr = configuration.GetConnectionString("TradingCompanyDB") ??
                throw new InvalidOperationException("Connection string 'TradingCompanyDB' not found in config.json.");

            // DAL реєстрації
            services.AddTransient<IUserDal>(sp =>new UserDalEF(connStr, sp.GetRequiredService<IMapper>()));
           
            services.AddTransient<IUserPrivilegeDal>(sp => new UserPrivilegeDalEF(connStr, sp.GetRequiredService<IMapper>())); // <-- ДОДАТИ

            services.AddTransient<ICategoryDal>(sp => new CategoryDalEF(connStr, sp.GetRequiredService<IMapper>()));
            services.AddTransient<IProductDal>(sp => new ProductDalEF(connStr, sp.GetRequiredService<IMapper>()));
            
            services.AddTransient<IManufactureDal>(sp => new ManufactureDalEF(connStr, sp.GetRequiredService<IMapper>()));


            services.AddTransient<IProductLogDal>(sp => new ProductLogDalEF(connStr, sp.GetRequiredService<IMapper>()));

            // BL реєстрації
            services.AddTransient<IAuthManager, AuthManager>();
            services.AddTransient<ICategoryManager, CategoryManager>();
            services.AddTransient<IProductManager, ProductManager>();
            
           services.AddTransient<IProductLogManager, ProductLogManager>();
            services.AddTransient<IManufactureManager, ManufactureManager>();


            // ViewModels

            services.AddTransient<LoginViewModel>();

            services.AddTransient<CategoryListViewModel>();
            services.AddTransient<CategoryDetailsViewModel>();

            services.AddTransient<ProductListViewModel>();
            services.AddTransient<ProductDetailsViewModel>();
            
            services.AddTransient<ProductLogListViewModel>();
            services.AddTransient<ProductLogDetailsViewModel>();
            
            services.AddTransient<ManufacturerListViewModel>();
            services.AddTransient<ManufacturerDetailsViewModel>();


            // Windows

            services.AddTransient<Login>();
            services.AddTransient<CategoryListMVVM>();
            services.AddTransient<CategoryDetails>();

            services.AddTransient<ProductListMVVM>();
            services.AddTransient<ProductDetails>();

            services.AddTransient<EntitySelectionWindow>();

            services.AddTransient<ManufacturerListMVVM>();           // <-- Додати
            services.AddTransient<ManufacturerDetails>();

            services.AddTransient<ProductLogListMVVM>();
            services.AddTransient<ProductLogDetails>();

            return services.BuildServiceProvider();
        }
    }

}