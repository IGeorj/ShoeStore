using System.Windows;

namespace ShoesStore.Views.Dialogs
{
    public partial class EditProductView : Window
    {
        public EditProductView()
        {
            InitializeComponent();
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