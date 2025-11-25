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
using TradingCompany.WPF.ViewModels;

namespace TradingCompany.WPF.Windows
{
    /// <summary>
    /// Interaction logic for ManufacturerDetails.xaml
    /// </summary>
    public partial class ManufacturerDetails : Window
    {
        private readonly ManufacturerDetailsViewModel _viewModel;

        public ManufacturerDetails(ManufacturerDetailsViewModel vm)
        {
            _viewModel = vm;
            DataContext = vm;
            InitializeComponent();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.Save();
            DialogResult = true;
            Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
