using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TradingCompany.BL.Interfaces;
using TradingCompany.DTO;
using TradingCompany.WebApp.Models;

namespace TradingCompany.WebApp.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {
        private readonly ICategoryManager _manager;
        private readonly IMapper _mapper;
        private readonly ILogger<CategoryController> _logger;

        public CategoryController(
            ICategoryManager manager,
            IMapper mapper,
            ILogger<CategoryController> logger)
        {
            _manager = manager;
            _mapper = mapper;
            _logger = logger;
        }

        // GET: Category
        public IActionResult Index()
        {
            var categories = _manager.GetAllCategories();
            return View(categories);
        }

        // GET: Category/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View(new EditCategoryModel());
        }

        // POST: Category/Create
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(EditCategoryModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(model);

                var category = _mapper.Map<Category>(model);
                _manager.CreateCategory(category);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Помилка створення категорії");
                return View(model);
            }
        }

        // GET: Category/Edit/5
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int id)
        {
            var category = _manager.GetCategoryById(id);
            if (category == null)
                return NotFound();

            var model = _mapper.Map<EditCategoryModel>(category);
            return View(model);
        }

        // POST: Category/Edit
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(EditCategoryModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View(model);

                var category = _mapper.Map<Category>(model);
                _manager.UpdateCategory(category);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Помилка редагування категорії");
                return View(model);
            }
        }

        // GET: Category/Delete/5
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            var category = _manager.GetCategoryById(id);
            if (category == null)
                return NotFound();

            return View(category);
        }

        // POST: Category/Delete
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
                _manager.DeleteCategory(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Помилка видалення категорії");
                return RedirectToAction(nameof(Index));
            }
        }
    }

}
