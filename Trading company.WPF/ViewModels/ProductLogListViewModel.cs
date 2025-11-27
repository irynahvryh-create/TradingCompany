using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using Trading_company.BL.Interfaces;
using TradingCompany.DTO;

namespace TradingCompany.WPF.ViewModels
{
    public class ProductLogListViewModel : INotifyPropertyChanged
    {
        private readonly IProductLogManager _logManager;

        private ObservableCollection<ProductLog> _logs;
        public ObservableCollection<ProductLog> Logs
        {
            get => _logs;
            set { _logs = value; OnPropertyChanged(nameof(Logs)); }
        }

        public ICollectionView LogsView { get; private set; }

        private ProductLog? _selectedLog;
        public ProductLog? SelectedLog
        {
            get => _selectedLog;
            set { _selectedLog = value; OnPropertyChanged(nameof(SelectedLog)); }
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
                LogsView?.Refresh();
            }
        }

        public ProductLogListViewModel(IProductLogManager logManager)
        {
            _logManager = logManager ?? throw new ArgumentNullException(nameof(logManager));
            Refresh();
        }

        private bool FilterPredicate(object? obj)
        {
            if (string.IsNullOrWhiteSpace(FilterText)) return true;
            if (obj is ProductLog log)
            {
                return log.Comment?.IndexOf(FilterText.Trim(), StringComparison.OrdinalIgnoreCase) >= 0;
            }
            return false;
        }

        public void Refresh()
        {
            var allLogs = _logManager.GetAllProductLog();

            if (Logs == null)
            {
                Logs = new ObservableCollection<ProductLog>(allLogs);
                LogsView = CollectionViewSource.GetDefaultView(Logs);
                LogsView.Filter = FilterPredicate;
            }
            else
            {
                Logs.Clear();
                foreach (var log in allLogs)
                    Logs.Add(log);
                LogsView.Refresh();
            }
        }

        public bool DeleteSelectedLog()
        {
            if (SelectedLog == null) return false;
            try
            {
                return _logManager.DeleteProductLog(SelectedLog.LogID);
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
