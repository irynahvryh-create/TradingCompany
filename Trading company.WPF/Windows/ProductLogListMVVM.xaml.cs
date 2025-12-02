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

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            // Приховуємо поточне вікно, щоб MainWindow не закрив додаток
            this.Hide();

            // Відкриваємо вікно вибору сутності
            var selectionWindow = App.Services.GetRequiredService<EntitySelectionWindow>();
            bool? result = selectionWindow.ShowDialog();

            if (result == true)
            {
                switch (selectionWindow.SelectedEntity)
                {
                    case "Category":
                        var catWindow = App.Services.GetRequiredService<CategoryListMVVM>();
                        catWindow.ShowDialog();
                        break;
                    case "Manufacturer":
                        var manWindow = App.Services.GetRequiredService<ManufacturerListMVVM>();
                        manWindow.ShowDialog();
                        break;
                    case "Product":
                        var prodWindow = App.Services.GetRequiredService<ProductListMVVM>();
                        prodWindow.ShowDialog();
                        break;
                    case "ProductLog":
                        var logWindow = App.Services.GetRequiredService<ProductLogListMVVM>();
                        logWindow.ShowDialog();
                        break;
                }
            }

            // Після повернення з вибору сутності показуємо поточне вікно назад
            this.Show();
        }


    }
}
