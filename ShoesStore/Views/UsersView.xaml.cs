using ShoesStore.ViewModels;
using ShoesStore.Views.Dialogs;
using System.Windows.Controls;

namespace ShoesStore.Views
{
    public partial class UsersView : UserControl
    {
        private UsersViewModel viewModel = new UsersViewModel();

        public UsersView()
        {
            InitializeComponent();
            DataContext = viewModel;
        }

        private void ChangeButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            EditUserView editUser = new EditUserView();
            editUser.DataContext = new EditUserViewModel(viewModel.SelectedUser.Id);
            if (editUser.ShowDialog() == true)
            {
                viewModel.LoadDataAsync();
            }
        }
    }
}