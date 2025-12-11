using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TradingCompany.BL.Interfaces;
using TradingCompany.DTO;

namespace TradingCompany.WebApp.Controllers

{
    [Authorize] // тільки для залогінених користувачів
    public class CategoryController : Controller
    {
        private readonly ICategoryManager _categoryManager;

        public CategoryController(ICategoryManager categoryManager)
        {
            _categoryManager = categoryManager;
        }

        // -------------------------
        // Список категорій
        // -------------------------
        public IActionResult Index()
        {
            var categories = _categoryManager.GetAllCategories();
            return View(categories);
        }

        // -------------------------
        // Створення категорії
        // -------------------------

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Create(Category category)
        {
            if (!ModelState.IsValid)
                return View(category);

            _categoryManager.CreateCategory(category);
            return RedirectToAction("Index");
        }

        // -------------------------
        // Редагування категорії
        // -------------------------
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var category = _categoryManager.GetCategoryById(id);
            if (category == null) return NotFound();
            return View(category);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Edit(Category category)
        {
            if (!ModelState.IsValid)
                return View(category);

            _categoryManager.UpdateCategory(category);
            return RedirectToAction("Index");
        }

        // -------------------------
        // Видалення категорії
        // -------------------------
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            _categoryManager.DeleteCategory(id);
            return RedirectToAction("Index");
        }
    }
}
