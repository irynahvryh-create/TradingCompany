using System.Windows;
using TradingCompany.WPF.ViewModels;

namespace TradingCompany.WPF.Windows
{
    public partial class ProductDetails : Window
    {
        private readonly ProductDetailsViewModel _viewModel;

        public ProductDetails(ProductDetailsViewModel vm)
        {
            InitializeComponent();
            _viewModel = vm;
            DataContext = _viewModel;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.Save();
            DialogResult = true;
            Close();
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
