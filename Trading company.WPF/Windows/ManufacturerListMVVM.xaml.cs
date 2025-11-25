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

namespace TradingCompany.WPF.Windows
{
    /// <summary>
    /// Interaction logic for ManufacturerListMVVM.xaml
    /// </summary>
    public partial class ManufacturerListMVVM : Window
    {
        private readonly ManufacturerListViewModel _viewModel;

        public ManufacturerListMVVM(ManufacturerListViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            DataContext = _viewModel;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var editVM = ActivatorUtilities.CreateInstance<ManufacturerDetailsViewModel>(App.Services!, new Manufacture());
            var editWin = new ManufacturerDetails(editVM);
            editWin.Owner = this;
            editWin.ShowDialog();
            _viewModel.Refresh();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (_viewModel.SelectedManufacturer == null) return;

            var editVM = ActivatorUtilities.CreateInstance<ManufacturerDetailsViewModel>(App.Services!, _viewModel.SelectedManufacturer);
            var editWin = new ManufacturerDetails(editVM);
            editWin.Owner = this;
            editWin.ShowDialog();
            _viewModel.Refresh();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (_viewModel.SelectedManufacturer == null)
                return;

            var result = MessageBox.Show($"Видалити виробника '{_viewModel.SelectedManufacturer.Name}'?",
                                         "Підтвердження", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                bool success = _viewModel.DeleteSelectedManufacturer();
                if (success)
                {
                    MessageBox.Show("Виробник видалений.", "Успіх");
                    _viewModel.Refresh();
                }
                else
                {
                    MessageBox.Show("Не вдалося видалити виробника.", "Помилка");
                }
            }
        }


    }
}
