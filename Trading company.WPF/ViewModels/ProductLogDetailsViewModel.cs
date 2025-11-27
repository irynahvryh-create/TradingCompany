using System;
using System.ComponentModel;
using Trading_company.BL.Interfaces;
using TradingCompany.DTO;

namespace TradingCompany.WPF.ViewModels
{
    public class ProductLogDetailsViewModel : INotifyPropertyChanged
    {
        private readonly IProductLogManager _logManager;

        public ProductLogDetailsViewModel(IProductLogManager logManager, ProductLog? log = null)
        {
            _logManager = logManager;
            Log = log ?? new ProductLog
            {
                Date = DateTime.Now // автоматично ставимо поточну дату
            };
        }

        private ProductLog _log;
        public ProductLog Log
        {
            get => _log;
            set
            {
                _log = value;
                OnPropertyChanged(nameof(Log));
                OnPropertyChanged(nameof(ProductID));
                OnPropertyChanged(nameof(OldPrice));
                OnPropertyChanged(nameof(NewPrice));
                OnPropertyChanged(nameof(Status));
                OnPropertyChanged(nameof(Comment));
                OnPropertyChanged(nameof(Date));
            }
        }

        public int ProductID
        {
            get => Log.ProductID;
            set { Log.ProductID = value; OnPropertyChanged(nameof(ProductID)); }
        }

        public decimal OldPrice
        {
            get => Log.OldPrice;
            set { Log.OldPrice = value; OnPropertyChanged(nameof(OldPrice)); }
        }

        public decimal NewPrice
        {
            get => Log.NewPrice;
            set { Log.NewPrice = value; OnPropertyChanged(nameof(NewPrice)); }
        }

        public bool Status
        {
            get => Log.Status;
            set { Log.Status = value; OnPropertyChanged(nameof(Status)); }
        }

        public string? Comment
        {
            get => Log.Comment;
            set { Log.Comment = value; OnPropertyChanged(nameof(Comment)); }
        }

        // Дата тільки для читання
        public DateTime Date => Log.Date;

        public void Save()
        {
            if (Log.LogID == 0)
                Log = _logManager.CreateProductLog(Log);
            else
                Log = _logManager.UpdateProductLog(Log);
        }

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        #endregion
    }
}
