using TradingCompany.DAL.Concrete;
using TradingCompany.DTO;

class Program
{
    static void Main()
    {
        string connectionString =
        @"Data Source=localhost;Initial Catalog=TradingCompany_2;Integrated Security=True;Encrypt=False;";

        CategoryDal categoryDal = new CategoryDal(connectionString);

        while (true)
        {
            Console.WriteLine("1. Додати категорію");
            Console.WriteLine("2. Показати всі категорії");
            Console.WriteLine("3. Знайти категорію за ID");
            Console.WriteLine("4. Оновити категорію");
            Console.WriteLine("5. Видалити категорію");
            Console.WriteLine("0. Вийти");
            Console.Write("Вибір: ");
            string choice = Console.ReadLine() ?? "";

            switch (choice)
            {
                case "1":
                    Console.Write("Назва: ");
                    string name = Console.ReadLine() ?? "";
                    Console.Write("Статус (1=активний, 0=неактивний): ");
                    bool status = Console.ReadLine() == "1";

                    Category newCategory = new Category { Name = name, Status = status };
                    categoryDal.Create(newCategory);
                    Console.WriteLine($"Категорія додана з ID {newCategory.CategoryID}");
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
                        Console.WriteLine("Категорія не знайдена");
                    break;

                case "4":
                    Console.Write("ID для оновлення: ");
                    int updId = int.Parse(Console.ReadLine() ?? "0");
                    var updCat = categoryDal.GetById(updId);
                    if (updCat != null)
                    {
                        Console.Write("Нова назва: ");
                        updCat.Name = Console.ReadLine() ?? updCat.Name;
                        Console.Write("Новий статус (1=активний, 0=неактивний): ");
                        updCat.Status = Console.ReadLine() == "1";
                        categoryDal.Update(updCat);
                        Console.WriteLine("Категорія оновлена");
                    }
                    else
                        Console.WriteLine("Категорія не знайдена");
                    break;

                case "5":
                    Console.Write("ID для видалення: ");
                    int delId = int.Parse(Console.ReadLine() ?? "0");
                    if (categoryDal.Delete(delId))
                        Console.WriteLine("Категорія видалена");
                    else
                        Console.WriteLine("Категорія не знайдена");
                    break;

                case "0":
                    return;

                default:
                    Console.WriteLine("Невірний вибір");
                    break;
            }

            Console.WriteLine();
        }
    }
}