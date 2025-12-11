using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;
using TradingCompany.BL.Interfaces;
using TradingCompany.DTO;
using TradingCompany.WPF.Commands;

namespace TradingCompany.WPF.ViewModels
{
    public class CategoryListViewModel : INotifyPropertyChanged
    {
        private readonly ICategoryManager _categoryManager;
        private readonly IAuthManager _authManager;

        public ObservableCollection<Category> Categories { get; private set; }
        public ICollectionView CategoriesView { get; private set; }
        public Category? SelectedCategory { get; set; }

        // 
        public ICommand DeleteCategoryCommand { get; }

        public CategoryListViewModel(ICategoryManager categoryManager, IAuthManager authManager)
        {
            _categoryManager = categoryManager ?? throw new ArgumentNullException(nameof(categoryManager));
            _authManager = authManager ?? throw new ArgumentNullException(nameof(authManager));

            
            DeleteCategoryCommand = new RelayCommand(
                _ => DeleteSelectedCategory(),
                _ => _authManager.CurrentUser != null && _authManager.IsAdmin(_authManager.CurrentUser)
            );

            // Слідкуємо за зміною CurrentUser для оновлення стану команд
            _authManager.CurrentUserChanged += () =>
            {
                OnPropertyChanged(nameof(CanDelete));
                CommandManager.InvalidateRequerySuggested(); // оновлює CanExecute
            };

            Refresh();
        }

        public bool CanDelete => _authManager.CurrentUser != null && _authManager.IsAdmin(_authManager.CurrentUser);

        // Фільтр для DataGrid
        private bool FilterPredicate(object? obj)
        {
            if (string.IsNullOrWhiteSpace(FilterText)) return true;

            if (obj is Category c)
                return c.Name?.IndexOf(FilterText.Trim(), StringComparison.OrdinalIgnoreCase) >= 0;

            return false;
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
                CategoriesView.Refresh();
            }
        }

        
        public void Refresh()
        {
            Categories = new ObservableCollection<Category>(_categoryManager.GetAllCategories());
            CategoriesView = CollectionViewSource.GetDefaultView(Categories);
            CategoriesView.Filter = FilterPredicate;
            OnPropertyChanged(nameof(Categories));
        }

        
        public bool DeleteSelectedCategory()
        {
            if (SelectedCategory == null) return false;

            if (_authManager.CurrentUser == null || !_authManager.IsAdmin(_authManager.CurrentUser))
            {
                
                return false;
            }

            try
            {
                _categoryManager.DeleteCategory(SelectedCategory.CategoryID);
                Refresh();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
    }
}
