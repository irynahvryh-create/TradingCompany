using TradingCompany.BL.Interfaces;
using TradingCompany.DTO;
using System.Collections.Generic;
using System.ComponentModel;

namespace TradingCompany.WPF.ViewModels
{
    public class CategoryDetailsViewModel : INotifyPropertyChanged
    {
        private ICategoryManager _manager;
        private Category _category;

        public CategoryDetailsViewModel(ICategoryManager manager, Category? category = null)
        {
            _manager = manager;
            Category = category ?? new Category();
        }

        public Category Category
        {
            get { return _category; }
            set
            {
                _category = value;
                OnPropertyChanged(nameof(Category));
                OnPropertyChanged(nameof(Name));
                OnPropertyChanged(nameof(Status));
            }
        }

        // Проксі для прив'язки XAML
        public string Name
        {
            get => _category.Name;
            set
            {
                if (_category.Name != value)
                {
                    _category.Name = value;
                    OnPropertyChanged(nameof(Name));
                }
            }
        }

        public bool Status
        {
            get => _category.Status;
            set
            {
                if (_category.Status != value)
                {
                    _category.Status = value;
                    OnPropertyChanged(nameof(Status));
                }
            }
        }

        public void Save()
        {
            if (_category.CategoryID == 0)
            {
                _category = _manager.CreateCategory(_category); // нова категорія
            }
            else
            {
                _category = _manager.UpdateCategory(_category); // існуюча категорія
            }

            OnPropertyChanged(nameof(Category));
            OnPropertyChanged(nameof(Name));
            OnPropertyChanged(nameof(Status));
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
