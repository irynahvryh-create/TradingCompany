using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using TradingCompany.DTO;
using TradingCompany.WPF.ViewModels;

namespace TradingCompany.WPF.Windows
{
    public partial class CategoryListMVVM : Window
    {
        private readonly CategoryListViewModel _viewModel;

        public CategoryListMVVM(CategoryListViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            DataContext = _viewModel;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var editViewModel = ActivatorUtilities.CreateInstance<CategoryDetailsViewModel>(App.Services!, new Category());
            var editWin = new CategoryDetails(editViewModel) { Owner = this };
            editWin.ShowDialog();
            _viewModel.Refresh();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (_viewModel.SelectedCategory == null) return;
            var editViewModel = ActivatorUtilities.CreateInstance<CategoryDetailsViewModel>(App.Services!, _viewModel.SelectedCategory);
            var editWin = new CategoryDetails(editViewModel) { Owner = this };
            editWin.ShowDialog();
            _viewModel.Refresh();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (_viewModel.SelectedCategory == null) return;

            var result = MessageBox.Show(
                $"Видалити категорію '{_viewModel.SelectedCategory.Name}'?",
                "Підтвердження",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning
            );

            if (result == MessageBoxResult.Yes)
            {
                if (_viewModel.DeleteSelectedCategory())
                {
                    MessageBox.Show("Категорія видалена.", "Успіх");
                }
                else
                {
                    MessageBox.Show("Не вдалося видалити категорію.", "Помилка");
                }
            }
        }


        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            
            this.Hide();

           
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

            
            this.Show();
        }








    }
}
