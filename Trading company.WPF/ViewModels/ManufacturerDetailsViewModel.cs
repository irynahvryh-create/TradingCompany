using System;
using System.ComponentModel;
using Trading_company.BL.Interfaces;
using TradingCompany.DTO;

namespace TradingCompany.WPF.ViewModels
{
    public class ManufacturerDetailsViewModel : INotifyPropertyChanged
    {
        private IManufactureManager _manager;
        private Manufacture _manufacturer;

        public ManufacturerDetailsViewModel(IManufactureManager manager, Manufacture? manufacturer = null)
        {
            _manager = manager;
            Manufacturer = manufacturer ?? new Manufacture();
        }

        public Manufacture Manufacturer
        {
            get { return _manufacturer; }
            set
            {
                _manufacturer = value;
                OnPropertyChanged(nameof(Manufacturer));
                OnPropertyChanged(nameof(Name));
                OnPropertyChanged(nameof(Country));
            }
        }

        public string Name
        {
            get => _manufacturer.Name;
            set
            {
                if (_manufacturer.Name != value)
                {
                    _manufacturer.Name = value;
                    OnPropertyChanged(nameof(Name));
                }
            }
        }

        public string Country
        {
            get => _manufacturer.Country;
            set
            {
                if (_manufacturer.Country != value)
                {
                    _manufacturer.Country = value;
                    OnPropertyChanged(nameof(Country));
                }
            }
        }

        public void Save()
        {
            if (_manufacturer.ManufacturerID == 0)
            {
                _manufacturer = _manager.CreateManufacture(_manufacturer);
            }
            else
            {
                _manufacturer = _manager.UpdateManufacture(_manufacturer);
            }

            OnPropertyChanged(nameof(Manufacturer));
            OnPropertyChanged(nameof(Name));
            OnPropertyChanged(nameof(Country));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
