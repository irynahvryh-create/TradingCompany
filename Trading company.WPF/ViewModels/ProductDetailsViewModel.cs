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

        public ProductDetailsViewModel(IProductManager productManager, Product? product = null)
        {
            _productManager = productManager;
            Product = product ?? new Product();
        }

        private Product _product;
        public Product Product
        {
            get => _product;
            set { _product = value; OnPropertyChanged(nameof(Product)); }
        }

        public string Name
        {
            get => Product.Name;
            set { Product.Name = value; OnPropertyChanged(nameof(Name)); }
        }

        public decimal PriceIn
        {
            get => Product.PriceIn;
            set { Product.PriceIn = value; OnPropertyChanged(nameof(PriceIn)); }
        }

        public decimal PriceOut
        {
            get => Product.PriceOut;
            set { Product.PriceOut = value; OnPropertyChanged(nameof(PriceOut)); }
        }

        public int CategoryID
        {
            get => Product.CategoryID;
            set { Product.CategoryID = value; OnPropertyChanged(nameof(CategoryID)); }
        }

        public int ManufacturerID
        {
            get => Product.ManufacturerID;
            set { Product.ManufacturerID = value; OnPropertyChanged(nameof(ManufacturerID)); }
        }

        public bool Status
        {
            get => Product.Status;
            set { Product.Status = value; OnPropertyChanged(nameof(Status)); }
        }

        public void Save()
        {
            if (Product.ProductID == 0)
                Product = _productManager.CreateProduct(Product);
            else
                _productManager.UpdateProduct(Product);
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        #endregion
    }
}
