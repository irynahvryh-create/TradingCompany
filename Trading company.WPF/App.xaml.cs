using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Windows;
using Trading_company.BL.Concrete;
using Trading_company.BL.Interfaces;
using TradingCompany.BL.Concrete;
using TradingCompany.BL.Interfaces;
using TradingCompany.DAL.EF.Concrete;
using TradingCompany.DAL.EF.MapperProfiles;
using TradingCompany.DAL.Interfaces;
using TradingCompany.DTO;
using TradingCompany.WPF.ViewModels;
using TradingCompany.WPF.Windows;

namespace TradingCompany.WPF
{
    public partial class App : Application
    {
        public static IServiceProvider? Services { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            Services = BuildServiceProvider();

            var authManager = Services.GetRequiredService<IAuthManager>();

            // Створюємо тестових користувачів, якщо їх немає
            if (authManager.GetUserByLogin("admin") == null)
            {
                authManager.CreateUser(
                    email: "admin@trading.com",
                    username: "admin",
                    password: "123456",
                    privilegeType: PrivilegeType.Admin
                );
            }

            var kira = authManager.GetUserByLogin("Kira");
            if (kira == null)
            {
                kira = authManager.CreateUser(
                    email: "kira@example.com",
                    username: "Kira",
                    password: "654321",
                    privilegeType: PrivilegeType.User
                );
            }
            else
            {
                // Якщо користувач існує, але немає привілеї, додаємо
                if (kira.Privileges == null || !kira.Privileges.Any())
                {
                    // Використовуємо IUserPrivilegeDal для додавання привілеї
                    var privilegeDal = App.Services.GetRequiredService<IUserPrivilegeDal>();
                    privilegeDal.AddPrivilegeToUser(kira.UserID, PrivilegeType.User);

                    // Підтягнемо оновленого користувача
                    kira = authManager.GetUserByLogin("Kira");
                }
            }


            Current.ShutdownMode = ShutdownMode.OnExplicitShutdown;

            // Показуємо вікно логіну
            var loginWindow = Services.GetRequiredService<Login>();
            bool loginResult = loginWindow.ShowDialog() ?? false;

            if (!loginResult)
            {
                // Якщо користувач не залогінився — вихід
                Current.Shutdown();
                return;
            }

            // CurrentUser вже встановлено через AuthManager після успішного Login
            var currentUser = authManager.CurrentUser;
            if (currentUser == null)
            {
                MessageBox.Show("Помилка при встановленні поточного користувача!", "Помилка");
                Current.Shutdown();
                return;
            }

            // Опційно перевіряємо права
            bool isAdmin = authManager.IsAdmin(currentUser);

            // Відкриваємо вікно вибору сутностей
            var selectionWindow = Services.GetRequiredService<EntitySelectionWindow>();
            bool selectionResult = selectionWindow.ShowDialog() ?? false;

            if (!selectionResult)
            {
                Current.Shutdown();
                return;
            }

            // Відкриваємо основне вікно залежно від вибору
            switch (selectionWindow.SelectedEntity)
            {
                case "Category":
                    Current.MainWindow = Services.GetRequiredService<CategoryListMVVM>();
                    break;
                case "Manufacturer":
                    Current.MainWindow = Services.GetRequiredService<ManufacturerListMVVM>();
                    break;
                case "Product":
                    Current.MainWindow = Services.GetRequiredService<ProductListMVVM>();
                    break;
                case "ProductLog":
                    Current.MainWindow = Services.GetRequiredService<ProductLogListMVVM>();
                    break;
                default:
                    Current.Shutdown();
                    return;
            }

            Current.ShutdownMode = ShutdownMode.OnMainWindowClose;
            Current.MainWindow.Show();
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

            // Configuration
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("config.json", optional: true)
                .Build();

            services.AddSingleton(configuration);

            // AutoMapper
            services.AddSingleton<IMapper>(sp =>
            {
                var config = new MapperConfiguration(cfg =>
                {
                    cfg.ConstructServicesUsing(sp.GetService);
                    cfg.AddMaps(typeof(Category_Map).Assembly);
                });
                return config.CreateMapper();
            });

            string connStr = configuration.GetConnectionString("TradingCompanyDB")
                ?? throw new InvalidOperationException("Connection string 'TradingCompanyDB' not found.");

            // DAL
            services.AddSingleton<IUserDal>(sp => new UserDalEF(connStr, sp.GetRequiredService<IMapper>()));
            services.AddSingleton<IUserPrivilegeDal>(sp => new UserPrivilegeDalEF(connStr, sp.GetRequiredService<IMapper>()));
            services.AddTransient<ICategoryDal>(sp => new CategoryDalEF(connStr, sp.GetRequiredService<IMapper>()));
            services.AddTransient<IProductDal>(sp => new ProductDalEF(connStr, sp.GetRequiredService<IMapper>()));
            services.AddTransient<IManufactureDal>(sp => new ManufactureDalEF(connStr, sp.GetRequiredService<IMapper>()));
            services.AddTransient<IProductLogDal>(sp => new ProductLogDalEF(connStr, sp.GetRequiredService<IMapper>()));

            // BL
            services.AddSingleton<IAuthManager, AuthManager>();
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
            services.AddTransient<ManufacturerListMVVM>();
            services.AddTransient<ManufacturerDetails>();
            services.AddTransient<ProductLogListMVVM>();
            services.AddTransient<ProductLogDetails>();

            return services.BuildServiceProvider();
        }
    }
}
