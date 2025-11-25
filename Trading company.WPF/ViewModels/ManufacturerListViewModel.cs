using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using Trading_company.BL.Interfaces;
using TradingCompany.BL.Interfaces;
using TradingCompany.DTO;

namespace TradingCompany.WPF.ViewModels
{
    public class ManufacturerListViewModel : INotifyPropertyChanged
    {
        private readonly IManufactureManager _manufactureManager;

        private ObservableCollection<Manufacture> _manufacturerList;
        public ObservableCollection<Manufacture> Manufacturers
        {
            get { return _manufacturerList; }
            set
            {
                _manufacturerList = value;
                OnPropertyChanged(nameof(Manufacturers));
            }
        }

        public ICollectionView ManufacturersView { get; private set; }

        private string _filterText = string.Empty;
        public string FilterText
        {
            get => _filterText;
            set
            {
                if (_filterText == value) return;
                _filterText = value;
                OnPropertyChanged(nameof(FilterText));
                ManufacturersView.Refresh();
            }
        }

        private Manufacture? _selectedManufacturer;
        public Manufacture? SelectedManufacturer
        {
            get => _selectedManufacturer;
            set
            {
                if (_selectedManufacturer == value) return;
                _selectedManufacturer = value;
                OnPropertyChanged(nameof(SelectedManufacturer));
            }
        }

        public ManufacturerListViewModel(IManufactureManager manufactureManager)
        {
            _manufactureManager = manufactureManager ?? throw new ArgumentNullException(nameof(manufactureManager));
            Refresh();
        }

        private bool FilterPredicate(object? obj)
        {
            if (string.IsNullOrWhiteSpace(FilterText))
                return true;

            if (obj is Manufacture m)
            {
                var q = FilterText.Trim();
                return (m.Name?.IndexOf(q, StringComparison.OrdinalIgnoreCase) >= 0) ||
                       (m.Country?.IndexOf(q, StringComparison.OrdinalIgnoreCase) >= 0);
            }

            return false;
        }

        public void Refresh()
        {
            Manufacturers = new ObservableCollection<Manufacture>(_manufactureManager.GetAllManufactures());
            ManufacturersView = CollectionViewSource.GetDefaultView(Manufacturers);
            ManufacturersView.Filter = FilterPredicate;
        }

        // ---------- Метод для видалення ----------
        public bool DeleteSelectedManufacturer()
        {
            if (SelectedManufacturer == null) return false;

            try
            {
                bool success = _manufactureManager.DeleteManufacture(SelectedManufacturer.ManufacturerID);
                return success;
            }
            catch
            {
                return false;
            }
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        #endregion
    }
}
