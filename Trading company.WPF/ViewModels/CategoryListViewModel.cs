using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;
using TradingCompany.BL.Interfaces;
using TradingCompany.DTO;

namespace TradingCompany.WPF.ViewModels
{
    public class CategoryListViewModel : INotifyPropertyChanged
    {
        private readonly ICategoryManager _categoryManager;

        private ObservableCollection<Category> _categoryList;
        public ObservableCollection<Category> Categories
        {
            get { return _categoryList; }
            set
            {
                _categoryList = value;
                OnPropertyChanged(nameof(Categories));
            }
        }

        public ICollectionView CategoriesView { get; private set; }

        private string _filterText = string.Empty;
        public string FilterText
        {
            get => _filterText;
            set
            {
                if (_filterText == value) return;
                _filterText = value;
                OnPropertyChanged(nameof(FilterText));
                CategoriesView.Refresh();
            }
        }

        private Category? _selectedCategory;
        public Category? SelectedCategory
        {
            get => _selectedCategory;
            set
            {
                if (_selectedCategory == value) return;
                _selectedCategory = value;
                OnPropertyChanged(nameof(SelectedCategory));
            }
        }


        //

        public ICommand DeleteCommand { get; }

        //


        public CategoryListViewModel(ICategoryManager categoryManager)
        {
            _categoryManager = categoryManager ?? throw new ArgumentNullException(nameof(categoryManager));
           
            Refresh();
        }

        private bool FilterPredicate(object? obj)
        {
            if (string.IsNullOrWhiteSpace(FilterText))
                return true;

            if (obj is Category c)
            {
                var q = FilterText.Trim();
                return (c.Name?.IndexOf(q, StringComparison.OrdinalIgnoreCase) >= 0);
            }

            return false;
        }

        public void Refresh()
        {
            Categories = new ObservableCollection<Category>(_categoryManager.GetAllCategories());
            CategoriesView = CollectionViewSource.GetDefaultView(Categories);
            CategoriesView.Filter = FilterPredicate;
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
    }
}
