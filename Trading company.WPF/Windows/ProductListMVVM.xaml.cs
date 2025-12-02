using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using TradingCompany.DTO;
using TradingCompany.WPF.ViewModels;

namespace TradingCompany.WPF.Windows
{
    public partial class ProductListMVVM : Window
    {
        private readonly ProductListViewModel _viewModel;

        public ProductListMVVM(ProductListViewModel vm)
        {
            InitializeComponent();
            _viewModel = vm;
            DataContext = _viewModel;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var vm = ActivatorUtilities.CreateInstance<ProductDetailsViewModel>(App.Services!, new Product());
            var win = new ProductDetails(vm) { Owner = this };
            win.ShowDialog();
            _viewModel.Refresh();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (_viewModel.SelectedProduct == null) return;
            var vm = ActivatorUtilities.CreateInstance<ProductDetailsViewModel>(App.Services!, _viewModel.SelectedProduct);
            var win = new ProductDetails(vm) { Owner = this };
            win.ShowDialog();
            _viewModel.Refresh();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (_viewModel.SelectedProduct == null) return;

            var result = MessageBox.Show($"Видалити продукт '{_viewModel.SelectedProduct.Name}'?",
                                         "Підтвердження", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                if (_viewModel.DeleteSelectedProduct())
                {
                    MessageBox.Show("Продукт видалено.", "Успіх");
                    _viewModel.Refresh();
                }
                else
                    MessageBox.Show("Не вдалося видалити продукт.", "Помилка");
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
