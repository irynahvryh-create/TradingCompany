using TradingCompany.DAL.Concrete;
using TradingCompany.DTO;

class Program
{
    static void Main()
    {
        string connectionString =
        @"Data Source=localhost;Initial Catalog=TradingCompany_2;Integrated Security=True;Encrypt=False;";

        CategoryDal categoryDal = new CategoryDal(connectionString);

        Console.WriteLine("Hello :)");
        while (true)
        {
            Console.WriteLine("Available options ");
            Console.WriteLine("1. Add Category");
            Console.WriteLine("2. Show All Categories");
            Console.WriteLine("3. Find Category by ID");
            Console.WriteLine("4. Update Category");
            Console.WriteLine("5. Delete Category");
            Console.WriteLine("0. Exit");
            Console.Write("Your selection: ");
            string choice = Console.ReadLine() ?? "";

            switch (choice)
            {
                case "1":
                    Console.Write("Name: ");
                    string name = Console.ReadLine() ?? "";
                    Console.Write("Status (1=активний, 0=неактивний): ");
                    bool status = Console.ReadLine() == "1";

                    Category newCategory = new Category { Name = name, Status = status };
                    categoryDal.Create(newCategory);
                    Console.WriteLine($"Category added with ID {newCategory.CategoryID}");
                    break;

                case "2":
                    var categories = categoryDal.GetAll();
                    foreach (var cat in categories)
                        Console.WriteLine($"{cat.CategoryID}: {cat.Name} ({(cat.Status ? "Активний" : "Неактивний")})");
                    break;

                case "3":
                    Console.Write("ID: ");
                    int id = int.Parse(Console.ReadLine() ?? "0");
                    var catById = categoryDal.GetById(id);
                    if (catById != null)
                        Console.WriteLine($"{catById.CategoryID}: {catById.Name} ({(catById.Status ? "Активний" : "Неактивний")})");
                    else
                        Console.WriteLine("Category not found");
                    break;

                case "4":
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
                        Console.WriteLine("Category not found");
                    break;

                case "5":
                    Console.Write("ID to delete: ");
                    int delId = int.Parse(Console.ReadLine() ?? "0");
                    if (categoryDal.Delete(delId))
                        Console.WriteLine("Category deletedа");
                    else
                        Console.WriteLine("Category not found");
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
}