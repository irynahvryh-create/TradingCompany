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
    /// Interaction logic for CategoryDetails.xaml
    /// </summary>
    public partial class CategoryDetails : Window
    {
        private readonly CategoryDetailsViewModel _viewModel;

        public CategoryDetails(CategoryDetailsViewModel vm)
        {
            _viewModel = vm;
            DataContext = vm;
            InitializeComponent();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            // Викликаємо логіку збереження з ViewModel
            _viewModel.Save();
            // Встановлюємо результат діалогу на успіх і закриваємо
            DialogResult = true;
            Close();
        }

        // 
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            // Встановлюємо результат діалогу на скасування/невдачу
            DialogResult = false;
            Close();
        }
    }
}

