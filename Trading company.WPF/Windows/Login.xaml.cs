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
using TradingCompany.WPF.Interfaces;
using TradingCompany.WPF.ViewModels;

namespace TradingCompany.WPF.Windows
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login(LoginViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;

            Loaded += Login_Loaded;
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (this.DataContext != null)
            {
                ((dynamic)this.DataContext).Password = ((PasswordBox)sender).Password;
            }
        }

        private void Login_Loaded(object sender, RoutedEventArgs e)
        {
            if (DataContext is ICloseable cvm)
            {
                cvm.Close += () =>
                {
                    DialogResult = false;
                    Close();
                };
            }
            if (DataContext is LoginViewModel lvm)
            {
                lvm.LoginSuccessful += () =>
                {
                    new UserDebugWindow().Show();
                    DialogResult = true;
                    Close();
                };
                lvm.LoginFailed += () =>
                {
                    MessageBox.Show("Invalid credentials", "Error");
                };
            }
        }
    }
}