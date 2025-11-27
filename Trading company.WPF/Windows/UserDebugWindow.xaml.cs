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
using TradingCompany.BL.Interfaces;

namespace TradingCompany.WPF.Windows
{
    /// <summary>
    /// Interaction logic for UserDebugWindow.xaml
    /// </summary>
    public partial class UserDebugWindow : Window
    {
        public UserDebugWindow()
        {
            InitializeComponent();

            var auth = App.Services.GetRequiredService<IAuthManager>();

            // ⚠️ TEST ONLY – встановлюємо адміністратора вручну
            var adminUser = auth.GetUsers().FirstOrDefault(u => u.Login == "admin");
            if (adminUser != null)
            {
                auth.SetCurrentUser(adminUser);
            }

            DataContext = new UserDebugViewModel(auth);
        }
    }

}
