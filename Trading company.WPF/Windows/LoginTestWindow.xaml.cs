using System.Windows;
using TradingCompany.BL.Interfaces;
using TradingCompany.WPF.ViewModels;

namespace TradingCompany.WPF.Windows
{
    public partial class LoginTestWindow : Window
    {
        public LoginTestWindow(IAuthManager authManager)
        {
            InitializeComponent();

            DataContext = new LoginTestViewModel(authManager);
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
