using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using TradingCompany.DTO;
using TradingCompany.WPF.ViewModels;

namespace TradingCompany.WPF.Windows
{
    public partial class ProductLogListMVVM : Window
    {
        private readonly ProductLogListViewModel _viewModel;

        public ProductLogListMVVM(ProductLogListViewModel vm)
        {
            InitializeComponent();
            _viewModel = vm;
            DataContext = _viewModel;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var vm = ActivatorUtilities.CreateInstance<ProductLogDetailsViewModel>(App.Services!, new ProductLog());
            var win = new ProductLogDetails(vm) { Owner = this };
            win.ShowDialog();
            _viewModel.Refresh();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (_viewModel.SelectedLog == null) return;
            var vm = ActivatorUtilities.CreateInstance<ProductLogDetailsViewModel>(App.Services!, _viewModel.SelectedLog);
            var win = new ProductLogDetails(vm) { Owner = this };
            win.ShowDialog();
            _viewModel.Refresh();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (_viewModel.SelectedLog == null) return;

            var result = MessageBox.Show($"Видалити лог ID '{_viewModel.SelectedLog.LogID}'?",
                                         "Підтвердження", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                if (_viewModel.DeleteSelectedLog())
                {
                    MessageBox.Show("Лог видалено.", "Успіх");
                    _viewModel.Refresh();
                }
                else
                    MessageBox.Show("Не вдалося видалити лог.", "Помилка");
            }
        }
    }
}
