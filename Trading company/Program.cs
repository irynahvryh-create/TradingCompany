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
                    case "18":
                    FindProductById();
                    break;
                    case "19":
                    UpdateProduct();
                    break;
                    case "20":
                        AddProduct();
                    break;
                    case "21":
                        DeleteProduct();
                    break;
                    case "22":
                        FindManufacturerById();
                    break;
                    case "23":
                        UpdateManufacturer();
                    break;
                    case "24":
                        DeleteManufacturer();
                    break;
                    case "25":
                    AddManufacture();
                    break;
                    case "26":
                        FindProductLogById();   
                    break;
                    case "27":
                        UpdateProductLog();
                    break;
                    case "28":
                        DeleteProductLog();
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
        Console.WriteLine("13. ShowAllProdact() ");
        Console.WriteLine("14. ShowAllProducts_2()");
        Console.WriteLine("15. ShowAllProductLog()");
        Console.WriteLine("16. ShowAllProductLog_2()");
        Console.WriteLine("17. AddProductLog");
        Console.WriteLine("18. FindProductById");
        Console.WriteLine("19. UpdateProduct");
        Console.WriteLine("20. AddProduct");
        Console.WriteLine("21. DeleteProduct");
        Console.WriteLine("22. FindManufacturerById");
        Console.WriteLine("23. UpdateManufacturer");
        Console.WriteLine("24. DeleteManufacturer");
        Console.WriteLine("25. AddManufacture()");
        Console.WriteLine("26. AddManufacture()");
        Console.WriteLine("27. AddManufacture()");
        Console.WriteLine("28. AddManufacture()");

        Console.WriteLine("");
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


    static void ShowAllCategories()
    {
        var dal = new CategoryDal(connectionString); // просто створюємо об'єкт
        var categories = dal.GetAll();
        foreach (var cat in categories)
            Console.WriteLine($"{cat.CategoryID}: {cat.Name} ({(cat.Status ? "Активний" : "Неактивний")})");
    }

    static void ShowAllCategories_2()
    {
        var categories = categoryDalEF.GetAll();
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

    static void DeleteCategory()
    {
        Console.Write("ID to delete: ");
        int delId = int.Parse(Console.ReadLine() ?? "0");

        if (categoryDal.Delete(delId))
            Console.WriteLine("Category deleted");
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

    static void AddProduct()
    {
        Console.Write("Name: ");
        string name = Console.ReadLine() ?? "";

        Console.Write("CategoryID: ");
        int categoryId = int.Parse(Console.ReadLine() ?? "0");

        Console.Write("ManufacturerID: ");
        int manufacturerId = int.Parse(Console.ReadLine() ?? "0");

        Console.Write("PriceOut: ");
        decimal priceOut = decimal.Parse(Console.ReadLine() ?? "0");
        Console.Write("PriceIn: ");
        decimal priceIn = decimal.Parse(Console.ReadLine() ?? "0");

        Console.Write("Status (1=активний, 0=неактивний): ");
        bool status = Console.ReadLine() == "1";

        var newProduct = new TradingCompany.DTO.Product
        {
            Name = name,
            CategoryID = categoryId,
            ManufacturerID = manufacturerId,
            PriceOut = priceOut,
            PriceIn = priceIn,
            Status = status
        };

        var created = productsDalEF.Create(newProduct);
        Console.WriteLine($"Product added with ID {created.ProductID}");
    }

    static void ShowAllProdact()
    {
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

    static void FindProductById()
    {
        Console.Write("ProductID: ");
        int id = int.Parse(Console.ReadLine() ?? "0");

        var product = productsDalEF.GetById(id);
        if (product != null)
            Console.WriteLine($"{product.ProductID}) {product.Name} - {product.PriceOut}, {(product.Status ? "Активний" : "Неактивний")}");
        else
            Console.WriteLine("Product not found");
    }
    
    static void UpdateProduct()
    {
        Console.Write("ProductID to update: ");
        int id = int.Parse(Console.ReadLine() ?? "0");

        var product = productsDalEF.GetById(id);
        if (product != null)
        {
            Console.Write("New name: ");
            product.Name = Console.ReadLine() ?? product.Name;

            Console.Write("New CategoryID: ");
            product.CategoryID = int.Parse(Console.ReadLine() ?? product.CategoryID.ToString());

            Console.Write("New ManufacturerID: ");
            product.ManufacturerID = int.Parse(Console.ReadLine() ?? product.ManufacturerID.ToString());

            Console.Write("New PriceOut: ");
            product.PriceOut = decimal.Parse(Console.ReadLine() ?? product.PriceOut.ToString());

            Console.Write("New PriceIn: ");
            product.PriceIn = decimal.Parse(Console.ReadLine() ?? product.PriceIn.ToString());

            Console.Write("New Status (1=активний, 0=неактивний): ");
            product.Status = Console.ReadLine() == "1";

            productsDalEF.Update(product);
            Console.WriteLine("Product updated");
        }
        else
        {
            Console.WriteLine("Product not found");
        }
    }

    static void DeleteProduct()
    {
        Console.Write("ProductID to delete: ");
        int id = int.Parse(Console.ReadLine() ?? "0");

        if (productsDalEF.Delete(id))
            Console.WriteLine("Product deleted");
        else
            Console.WriteLine("Product not found");
    }


    // Додати нового виробника
    static void AddManufacture()
    {
        Console.Write("Name: ");
        string name = Console.ReadLine() ?? "";

        Console.Write("Country: ");
        string country = Console.ReadLine() ?? "";

        var newManufacturer = new TradingCompany.DTO.Manufacture
        {
            Name = name,
            Country = country
        };

        manufacturerDalEF.Create(newManufacturer);
        Console.WriteLine($"Виробника додано з ID {newManufacturer.ManufacturerID}");
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

    static void FindManufacturerById()
    {
        Console.Write("ID виробника: ");
        int id = int.Parse(Console.ReadLine() ?? "0");

        var manufacturer = manufacturerDalEF.GetById(id);
        if (manufacturer != null)
            Console.WriteLine($"{manufacturer.ManufacturerID}) {manufacturer.Name} - {manufacturer.Country}");
        else
            Console.WriteLine("Виробника не знайдено");
    }
    static void UpdateManufacturer()
    {
        Console.Write("ID виробника для оновлення: ");
        int updId = int.Parse(Console.ReadLine() ?? "0");

        var updManufacturer = manufacturerDalEF.GetById(updId);
        if (updManufacturer != null)
        {
            Console.Write("Нова назва: ");
            updManufacturer.Name = Console.ReadLine() ?? updManufacturer.Name;

            Console.Write("Нова країна: ");
            updManufacturer.Country = Console.ReadLine() ?? updManufacturer.Country;

            manufacturerDalEF.Update(updManufacturer);
            Console.WriteLine("Виробника оновлено ");
        }
        else
        {
            Console.WriteLine("Виробника не знайдено");
        }
    }
    static void DeleteManufacturer()
    {
        Console.Write("ID виробника для видалення: ");
        int delId = int.Parse(Console.ReadLine() ?? "0");

        if (manufacturerDalEF.Delete(delId))
            Console.WriteLine("Виробника видалено ");
        else
            Console.WriteLine("Виробника не знайдено");
    }






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
    
    static void ShowAllProductLog_2()
    {
        var productLog = productLogDalEF.GetAll();
        foreach (var cat in productLog)
             Console.WriteLine($"{cat.LogID}: ProductID={cat.ProductID}, OldPrice={cat.OldPrice}, NewPrice={cat.NewPrice}, Status={(cat.Status ? "Активний" : "Неактивний")}, Date={cat.Date}");
    }
    static void ShowAllProductLog()
    {
        var logs = productLogDalEF.GetAll(); 

        foreach (var log in logs)
        {
            Console.WriteLine($"{log.LogID}: ProductID={log.ProductID}, OldPrice={log.OldPrice}, NewPrice={log.NewPrice}, Status={(log.Status ? "Активний" : "Неактивний")}, Date={log.Date}");
        }
    }


    // Знайти лог за ID
    static void FindProductLogById()
    {
        Console.Write("LogID: ");
        int id = int.Parse(Console.ReadLine() ?? "0");

        var log = productLogDalEF.GetById(id);
        if (log != null)
        {
            Console.WriteLine($"{log.LogID}: ProductID={log.ProductID}, OldPrice={log.OldPrice}, NewPrice={log.NewPrice}, Status={(log.Status ? "Активний" : "Неактивний")}, Date={log.Date}");
        }
        else
        {
            Console.WriteLine("Log not found");
        }
    }

    // Оновити лог
    static void UpdateProductLog()
    {
        Console.Write("LogID to update: ");
        int id = int.Parse(Console.ReadLine() ?? "0");

        var log = productLogDalEF.GetById(id);
        if (log != null)
        {
            Console.Write("New OldPrice: ");
            log.OldPrice = decimal.Parse(Console.ReadLine() ?? log.OldPrice.ToString());

            Console.Write("New NewPrice: ");
            log.NewPrice = decimal.Parse(Console.ReadLine() ?? log.NewPrice.ToString());

            Console.Write("New Status (1=активний, 0=неактивний): ");
            log.Status = Console.ReadLine() == "1";

            productLogDalEF.Update(log);
            Console.WriteLine("Log updated");
        }
        else
        {
            Console.WriteLine("Log not found");
        }
    }

    // Видалити лог
    static void DeleteProductLog()
    {
        Console.Write("LogID to delete: ");
        int id = int.Parse(Console.ReadLine() ?? "0");

        if (productLogDalEF.Delete(id))
            Console.WriteLine("Log deleted");
        else
            Console.WriteLine("Log not found");
    }










}