using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TradingCompany.DTO;
using TradingCompany.WPF.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace TradingCompany.WPF.Windows
{
    /// <summary>
    /// Interaction logic for CategoryListMVVM.xaml
    /// </summary>
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
            // Створюємо новий Category через DI та передаємо пустий об'єкт
            var editViewModel = ActivatorUtilities.CreateInstance<CategoryDetailsViewModel>(App.Services!, new Category());
            var editWin = new CategoryDetails(editViewModel);
            editWin.Owner = this;
            editWin.ShowDialog();

            // Оновлюємо список після додавання
            _viewModel.Refresh();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (_viewModel.SelectedCategory == null)
                return;

            // Передаємо обрану категорію у CategoryDetailsViewModel через DI
            var editViewModel = ActivatorUtilities.CreateInstance<CategoryDetailsViewModel>(App.Services!, _viewModel.SelectedCategory);
            var editWin = new CategoryDetails(editViewModel);
            editWin.Owner = this;
            editWin.ShowDialog();

            // Оновлюємо список після редагування
            _viewModel.Refresh();
        }
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (_viewModel.SelectedCategory == null)
                return;

            var result = MessageBox.Show($"Видалити категорію '{_viewModel.SelectedCategory.Name}'?",
                                         "Підтвердження", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                bool success = _viewModel.DeleteSelectedCategory();
                if (success)
                {
                    MessageBox.Show("Категорія видалена.", "Успіх");
                    _viewModel.Refresh();
                }
                else
                {
                    MessageBox.Show("Не вдалося видалити категорію.", "Помилка");
                }
            }
        }

    }
}