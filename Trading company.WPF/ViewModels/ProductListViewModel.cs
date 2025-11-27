using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using Trading_company.BL.Interfaces;
using TradingCompany.BL.Interfaces;
using TradingCompany.DTO;

namespace TradingCompany.WPF.ViewModels
{
    public class ProductListViewModel : INotifyPropertyChanged
    {
        private readonly IProductManager _productManager;

        private ObservableCollection<Product> _products;
        public ObservableCollection<Product> Products
        {
            get => _products;
            set { _products = value; OnPropertyChanged(nameof(Products)); }
        }

        public ICollectionView ProductsView { get; private set; }

        private Product? _selectedProduct;
        public Product? SelectedProduct
        {
            get => _selectedProduct;
            set { _selectedProduct = value; OnPropertyChanged(nameof(SelectedProduct)); }
        }

        private string _filterText = string.Empty;
        public string FilterText
        {
            get => _filterText;
            set
            {
                if (_filterText == value) return;
                _filterText = value;
                OnPropertyChanged(nameof(FilterText));
                ProductsView?.Refresh();
            }
        }

        public ProductListViewModel(IProductManager productManager)
        {
            _productManager = productManager ?? throw new ArgumentNullException(nameof(productManager));
            Refresh();
        }

        private bool FilterPredicate(object? obj)
        {
            if (string.IsNullOrWhiteSpace(FilterText)) return true;
            if (obj is Product p)
            {
                return p.Name?.IndexOf(FilterText.Trim(), StringComparison.OrdinalIgnoreCase) >= 0;
            }
            return false;
        }

        public void Refresh()
        {
            var allProducts = _productManager.GetAllProducts();

            if (Products == null)
            {
                Products = new ObservableCollection<Product>(allProducts);
                ProductsView = CollectionViewSource.GetDefaultView(Products);
                ProductsView.Filter = FilterPredicate;
            }
            else
            {
                Products.Clear();
                foreach (var p in allProducts)
                    Products.Add(p);
                ProductsView.Refresh();
            }
        }

        public bool DeleteSelectedProduct()
        {
            if (SelectedProduct == null) return false;
            try
            {
                return _productManager.DeleteProduct(SelectedProduct.ProductID);
            }
            catch { return false; }
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        #endregion
    }
}
