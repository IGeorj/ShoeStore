using ShoesStore.ViewModels;
using System.Windows;

namespace ShoesStore.Views.Dialogs
{
    public partial class AddCompanyView : Window
    {
        private AddEditCompanyViewModel viewModel = new AddEditCompanyViewModel();

        public AddCompanyView()
        {
            InitializeComponent();
            DataContext = viewModel;
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(Country.Text) || string.IsNullOrEmpty(Name.Text))
            {
                MessageBox.Show("Please verify data");
            }
            else
            {
                this.DialogResult = true;
            }
        }
    }
}