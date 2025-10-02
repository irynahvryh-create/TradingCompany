using AutoMapper;
using Microsoft.Extensions.Logging;
using System;
using TradingCompany.DAL.Concrete;
using TradingCompany.DAL.EF.Data;
using TradingCompany.DAL.EF.MapperProfiles;
using TradingCompany.DAL.Interfaces;
using TradingCompany.DTO;



partial class Program
{
    static string connectionString = @"Data Source=localhost;Initial Catalog=TradingCompany_2;Integrated Security=True;Encrypt=False;";
    static CategoryDal categoryDal = new CategoryDal(connectionString);
    static ManufactureDalEF manufacturerDalEF;
    static ProductDalEF productsDalEF;
    static ProductLogDal productLogDal = new ProductLogDal(connectionString);
    static ProductLogDalEF productLogDalEF;


    static CategoryDalEF categoryDalEF;
    static IMapper _mapper;
    static ILogger<Program> _logger;

    static void Main()
    {

        // 🔹 КРОК 1. Створюємо LoggerFactory
        using var loggerFactory = LoggerFactory.Create(builder =>
        {
            builder
                .AddConsole()
                .SetMinimumLevel(LogLevel.Information);
        });

        // 🔹 Логер для Program
        _logger = loggerFactory.CreateLogger<Program>();
        _logger.LogInformation("Програма стартувала...");

        // 🔹 КРОК 2. AutoMapper автоматично підтягує всі профілі з твоєї збірки
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddMaps(typeof(Category_Map).Assembly);
        });

        // Якщо є помилки в мапінгу — вони зловляться тут
        config.AssertConfigurationIsValid();

        _mapper = config.CreateMapper();

        // 🔹 КРОК 3. Передаємо mapper у DAL
        categoryDalEF = new CategoryDalEF(connectionString, _mapper);
        manufacturerDalEF = new ManufactureDalEF(connectionString, _mapper);
        productsDalEF = new ProductDalEF(connectionString, _mapper);
        productLogDalEF = new ProductLogDalEF(connectionString, _mapper);



        Console.WriteLine("Hello :)");

        while (true)
        {
            ShowMenu();
            string choice = Console.ReadLine() ?? "";

            switch (choice)
            {
                case "1":
                    AddCategory();
                    break;
                case "2":
                    ShowAllCategories();
                    break;
                case "3":
                    FindCategoryById();
                    break;
                case "4":
                    UpdateCategory();
                    break;
                case "5":
                    DeleteCategory();
                    break;
                case "6":
                    FindCategoryById_1();
                    break;
                case "7":
                    ShowAllCategories_2();
                    break;
                case "8":
                    ShowAllManufacturer();
                    break;
                case "9":
                    ShowAllManufacturer_2();
                    break;
                case "10":
                    UpdateCategory_2();
                    break;
                case "11":
                    AddCategory_2();
                    break;
                    case "12":  
                        DeleteCategory_2();
                    break;
                    case "13":
                        ShowAllProdact();
                    break;
                    case "14":  
                        ShowAllProducts_2();
                    break;
                    case "15":
                        ShowAllProductLog();
                    break;
                    case "16":
                        ShowAllProductLog_2();
                    break;
                    case "17":
                        AddProductLog();
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Invalid selection");
                    break;
            }

            Console.WriteLine();
        }
    }

    static void ShowMenu()
    {
        Console.WriteLine("Available options:");
        Console.WriteLine("1. Add Category");
        Console.WriteLine("2. Show All Categories");
        Console.WriteLine("3. Find Category by ID");
        Console.WriteLine("4. Update Category");
        Console.WriteLine("5. Delete Category");
        Console.WriteLine("6.  Find Category by ID()");
        Console.WriteLine("7. Show All Categories(map)");
        Console.WriteLine("8. ShowAllManufacturer");
        Console.WriteLine("9. ShowAllManufacturer(map)");
        Console.WriteLine("10. UpdateCategory_2()");
        Console.WriteLine("11. AddCategory_2();");
        Console.WriteLine("12. DeleteCategory_2();");
        Console.WriteLine("13.ShowAllProdact() ");
        Console.WriteLine("14. ShowAllProducts_2()");
        Console.WriteLine("15.  ShowAllProductLog()");
        Console.WriteLine("16. ShowAllProductLog_2()");
        Console.WriteLine("17AddProductLog()");
        Console.WriteLine("0. Exit");
        Console.Write("Your selection: ");
    }

    static void AddCategory()
    {
        Console.Write("Name: ");
        string name = Console.ReadLine() ?? "";
        Console.Write("Status (1=активний, 0=неактивний): ");
        bool status = Console.ReadLine() == "1";

        Category newCategory = new Category { Name = name, Status = status };
        categoryDal.Create(newCategory);
        Console.WriteLine($"Category added with ID {newCategory.CategoryID}");
    }

    static void ShowAllCategories()
    {
        var dal = new CategoryDal(connectionString); // просто створюємо об'єкт
        var categories = dal.GetAll();
        foreach (var cat in categories)
            Console.WriteLine($"{cat.CategoryID}: {cat.Name} ({(cat.Status ? "Активний" : "Неактивний")})");
    }
    

    static void FindCategoryById()
    {
        Console.Write("ID: ");
        int id = int.Parse(Console.ReadLine() ?? "0");
        var catById = categoryDal.GetById(id);
        if (catById != null)
            Console.WriteLine($"{catById.CategoryID}: {catById.Name} ({(catById.Status ? "Активний" : "Неактивний")})");
        else
            Console.WriteLine("Category not found");
    }

    static void UpdateCategory()
    {
        Console.Write("ID to update: ");
        int updId = int.Parse(Console.ReadLine() ?? "0");
        var updCat = categoryDal.GetById(updId);

        if (updCat != null)
        {
            Console.Write("New name: ");
            updCat.Name = Console.ReadLine() ?? updCat.Name;
            Console.Write("New Status (1=активний, 0=неактивний): ");
            updCat.Status = Console.ReadLine() == "1";
            categoryDal.Update(updCat);
            Console.WriteLine("Category updated");
        }
        else
        {
            Console.WriteLine("Category not found");
        }
    }

    static void DeleteCategory()
    {
        Console.Write("ID to delete: ");
        int delId = int.Parse(Console.ReadLine() ?? "0");

        if (categoryDal.Delete(delId))
            Console.WriteLine("Category deleted");
        else
            Console.WriteLine("Category not found");
    }









    static void AddCategory_2()
    {
        Console.Write("Name: ");
        string name = Console.ReadLine() ?? "";
        Console.Write("Status (1=активний, 0=неактивний): ");
        bool status = Console.ReadLine() == "1";

        Category newCategory = new Category { Name = name, Status = status };
        categoryDalEF.Create(newCategory);
        Console.WriteLine($"Category added with ID {newCategory.CategoryID}");
    }

    static void ShowAllCategories_2()
    {
        var categories = categoryDalEF.GetAll();
        foreach (var cat in categories)
            Console.WriteLine($"{cat.CategoryID}: {cat.Name} ({(cat.Status ? "Активний" : "Неактивний")})");
    }

    static void FindCategoryById_1()
    {
        Console.Write("ID: ");
        int id = int.Parse(Console.ReadLine() ?? "0");
        var catById = categoryDalEF.GetById(id);
        if (catById != null)
            Console.WriteLine($"{catById.CategoryID}: {catById.Name} ({(catById.Status ? "Активний" : "Неактивний")})");
        else
            Console.WriteLine("Category not found");
    }
    static void DeleteCategory_2()
    {
        Console.Write("ID to delete: ");
        int delId = int.Parse(Console.ReadLine() ?? "0");

        if (categoryDalEF.Delete(delId))
            Console.WriteLine("Category deleted");
        else
            Console.WriteLine("Category not found");
    }



    static void UpdateCategory_2()
    {
        Console.Write("ID to update: ");
        int updId = int.Parse(Console.ReadLine() ?? "0");
        var updCat = categoryDalEF.GetById(updId);

        if (updCat != null)
        {
            Console.Write("New name: ");
            updCat.Name = Console.ReadLine() ?? updCat.Name;
            Console.Write("New Status (1=активний, 0=неактивний): ");
            updCat.Status = Console.ReadLine() == "1";
            categoryDal.Update(updCat);
            Console.WriteLine("Category updated");
        }
        else
        {
            Console.WriteLine("Category not found");
        }
    }



 static void ShowAllManufacturer()
    {
        var dal = new ManufactureDal(connectionString); // просто створюємо об'єкт
        var manufacturer = dal.GetAll();
        foreach (var cat in manufacturer)
            Console.WriteLine($"{cat.ManufacturerID}) {cat.Name} - {(cat.Country)}");
    }
    static void ShowAllManufacturer_2() 
    { 
    var manufacturer = manufacturerDalEF.GetAll();
        foreach (var cat in manufacturer)
            Console.WriteLine($"{cat.ManufacturerID}) {cat.Name} - {(cat.Country)}");
    }



    static void ShowAllProdact() { 
    var dal = new ProductDal(connectionString); // просто створюємо об'єкт
        var prodact = dal.GetAll();
        foreach (var cat in prodact)
            Console.WriteLine($"{cat.ProductID}) {cat.Name} - {(cat.PriceOut)} ,{cat.Status}");
    }


    static void ShowAllProducts_2()
    {
        var products = productsDalEF.GetAll(); // має повертати List<DTO.Product>

        foreach (var p in products)
        {
            // Використовуємо властивості DTO
            Console.WriteLine($"{p.ProductID}) {p.Name} - {p.PriceOut}, {(p.Status ? "Активний" : "Неактивний")}");
        }
    }





    /*
    static void ShowAllProductLog()
    {
        
       var dal = new ProductLogDal(connectionString); // просто створюємо об'єкт
        var productLog = dal.GetAll();
        foreach (var cat in productLog)
            Console.WriteLine($"{cat.LogID}) {cat.ProductID} {(cat.OldPrice)}- {(cat.NewPrice)} ,{cat.Status}");
    
        }

    */
    static void ShowAllProductLog_2()
    {
        var productLog = productLogDalEF.GetAll();
        foreach (var cat in productLog)
            Console.WriteLine($"{cat.LogID}) {cat.ProductID} ,{(cat.OldPrice)}- {(cat.NewPrice)} ,{cat.Status}");
    }



    // Показати всі логи
    static void ShowAllProductLog()
    {
        var logs = productLogDalEF.GetAll(); // productLogDalEF - твій DAL для ProductLog

        foreach (var log in logs)
        {
            Console.WriteLine($"{log.LogID}: ProductID={log.ProductID}, OldPrice={log.OldPrice}, NewPrice={log.NewPrice}, Status={(log.Status ? "Активний" : "Неактивний")}, Date={log.Date}");
        }
    }

    // Додати новий лог
    static void AddProductLog()
    {
        Console.Write("ProductID: ");
        int productId = int.Parse(Console.ReadLine() ?? "0");

        Console.Write("OldPrice: ");
        decimal oldPrice = decimal.Parse(Console.ReadLine() ?? "0");

        Console.Write("NewPrice: ");
        decimal newPrice = decimal.Parse(Console.ReadLine() ?? "0");

        Console.Write("Status (1=активний, 0=неактивний): ");
        bool status = Console.ReadLine() == "1";

        // Створюємо об'єкт DTO для DAL
        var newLog = new TradingCompany.DTO.ProductLog
        {
            ProductID = productId,
            OldPrice = oldPrice,
            NewPrice = newPrice,
            Status = status
            // Date тут не вказуємо — база поставить автоматично
        };

        var createdLog = productLogDalEF.Create(newLog); // додаємо через DAL

        Console.WriteLine($"Log додано з ID {createdLog.LogID}, дата: {createdLog.Date}");
    }



}




