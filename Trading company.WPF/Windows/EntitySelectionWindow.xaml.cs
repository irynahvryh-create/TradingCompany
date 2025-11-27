using System.Windows;

namespace TradingCompany.WPF.Windows
{
    public partial class EntitySelectionWindow : Window
    {
        public string? SelectedEntity { get; private set; }

        public EntitySelectionWindow()
        {
            InitializeComponent();
        }

        private void CategoryButton_Click(object sender, RoutedEventArgs e)
        {
            SelectedEntity = "Category";
            DialogResult = true;
            Close();
        }

        private void ManufacturerButton_Click(object sender, RoutedEventArgs e)
        {
            SelectedEntity = "Manufacturer";
            DialogResult = true;
            Close();
        }
        private void ProductButton_Click(object sender, RoutedEventArgs e)
        {
            SelectedEntity = "Product";
            DialogResult = true;
            Close();
        }
        private void ProductLogButton_Click(object sender, RoutedEventArgs e)
        {
            SelectedEntity = "ProductLog";
            DialogResult = true;
            Close();
        }
    }
}
