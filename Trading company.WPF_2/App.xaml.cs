using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;
using System.Data;
using System.IO;
using System.Windows;
using TradingCompany.BL.Concrete;
using TradingCompany.BL.Interfaces;
using TradingCompany.DAL.EF.Concrete;
using TradingCompany.DAL.EF.MapperProfiles;
using TradingCompany.DAL.Interfaces;

namespace TradingCompany.WPF_2
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IServiceProvider? Services { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            // Створюємо DI-контейнер
            Services = BuildServiceProvider();

            // 1. ПЕРЕВІРКА ПІДКЛЮЧЕННЯ (Тест, щоб знайти, де збій)
            try
            {
                // Створюємо ICategoryManager, який змусить створити DAL, ініціюючи підключення до БД.
                var manager = Services.GetRequiredService<TradingCompany.BL.Interfaces.ICategoryManager>();

                // Якщо цей рядок досягнуто, підключення до БД успішне.
                MessageBox.Show("DB Connection and DI chain are successful!", "Success", MessageBoxButton.OK);
            }
            catch (Exception ex)
            {
                // Якщо тут збій (наприклад, SqlException)
                MessageBox.Show($"FATAL ERROR DURING DI/DB CONNECT: {ex.Message}", "Startup Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Current.Shutdown();
                return;
            }

            // 2. Тимчасовий запуск
            // Якщо Connection OK, запускаємо найпростіше вікно
            var testWindow = new Window { Title = "Connection Test Succeeded" };
            testWindow.Show();

            // Ми тут не використовуємо Main Window чи Login Window, щоб уникнути помилок.
        }

        private static IServiceProvider BuildServiceProvider()
        {
            var services = new ServiceCollection();

            // --- 1. Конфігурація ---
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("config.json", optional: true).Build();
            services.AddSingleton<IConfiguration>(configuration);

            // --- 2. AutoMapper ---
            services.AddAutoMapper(typeof(TradingCompany.DAL.EF.MapperProfiles.Category_Map).Assembly);
            string connStr = configuration.GetConnectionString("TradingCompanyDB") ??
                             throw new InvalidOperationException("Connection string 'TradingCompanyDB' not found in config.json.");

            // --- 3. РЕЄСТРАЦІЯ DAL ---
            services.AddTransient<TradingCompany.DAL.Interfaces.ICategoryDal>(sp =>
                new TradingCompany.DAL.EF.Concrete.CategoryDalEF(connStr, sp.GetRequiredService<IMapper>()));

            // --- 4. РЕЄСТРАЦІЯ BL ---
            services.AddTransient<TradingCompany.BL.Interfaces.ICategoryManager, TradingCompany.BL.Concrete.CategoryManager>();

            return services.BuildServiceProvider();
        }
    }

}
