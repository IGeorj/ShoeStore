using System.Windows;

namespace ShoesStore.Views.Dialogs
{
    public partial class EditUserView : Window
    {
        public EditUserView()
        {
            InitializeComponent();
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            if (Name.Text == null || string.IsNullOrEmpty(Login.Text) || string.IsNullOrEmpty(Email.Text))
            {
                return;
            }
            else
            {
                this.DialogResult = true;
            }
        }
    }
}