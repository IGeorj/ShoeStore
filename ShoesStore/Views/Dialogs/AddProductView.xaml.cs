using ShoesStore.ViewModels;
using System.Windows;

namespace ShoesStore.Views.Dialogs
{
    public partial class AddProductView : Window
    {
        private AddProductViewModel viewModel = new AddProductViewModel();

        public AddProductView()
        {
            InitializeComponent();
            DataContext = viewModel;
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            if (borderImage.Source == null || string.IsNullOrEmpty(Name.Text) || cbCategories.SelectedItem == null || cbCompanies.SelectedItem == null)
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