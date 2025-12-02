using System;
using System.Collections.Generic;
using System.ComponentModel;
using Trading_company.BL.Interfaces;
using TradingCompany.BL.Interfaces;
using TradingCompany.DTO;

namespace TradingCompany.WPF.ViewModels
{
    public class ProductDetailsViewModel : INotifyPropertyChanged
    {
        private readonly IProductManager _productManager;
        private readonly ICategoryManager _categoryManager;

        public ProductDetailsViewModel(IProductManager productManager, ICategoryManager categoryManager, Product? product = null)
        {
            _productManager = productManager;
            _categoryManager = categoryManager;
            Product = product ?? new Product();

            // Завантажуємо список категорій
            Categories = _categoryManager.GetAllCategories();

            // Встановлюємо вибрану категорію, якщо продукт вже має CategoryID
            if (Product.CategoryID != 0)
                SelectedCategory = Categories.Find(c => c.CategoryID == Product.CategoryID);
        }

        private Product _product;
        public Product Product
        {
            get => _product;
            set { _product = value; OnPropertyChanged(nameof(Product)); }
        }

        // Назва продукту
        public string Name
        {
            get => Product.Name;
            set { Product.Name = value; OnPropertyChanged(nameof(Name)); }
        }

        // Ціна закупівлі
        public decimal PriceIn
        {
            get => Product.PriceIn;
            set { Product.PriceIn = value; OnPropertyChanged(nameof(PriceIn)); }
        }

        // Ціна продажу
        public decimal PriceOut
        {
            get => Product.PriceOut;
            set { Product.PriceOut = value; OnPropertyChanged(nameof(PriceOut)); }
        }

        // Виробник
        public int ManufacturerID
        {
            get => Product.ManufacturerID;
            set { Product.ManufacturerID = value; OnPropertyChanged(nameof(ManufacturerID)); }
        }

        // Статус продукту
        public bool Status
        {
            get => Product.Status;
            set { Product.Status = value; OnPropertyChanged(nameof(Status)); }
        }

        // Властивість для збереження/оновлення продукту
        public void Save()
        {
            if (Product.ProductID == 0)
                Product = _productManager.CreateProduct(Product);
            else
                _productManager.UpdateProduct(Product);
        }

        // ----- Нові властивості для ComboBox категорій -----
        private List<Category> _categories;
        public List<Category> Categories
        {
            get => _categories;
            set { _categories = value; OnPropertyChanged(nameof(Categories)); }
        }

        private Category? _selectedCategory;
        public Category? SelectedCategory
        {
            get => _selectedCategory;
            set
            {
                _selectedCategory = value;
                OnPropertyChanged(nameof(SelectedCategory));
                if (_selectedCategory != null)
                    Product.CategoryID = _selectedCategory.CategoryID; // прив'язка до Product
            }
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        #endregion
    }
}
