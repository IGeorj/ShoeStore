using ShoesStore.ViewModels;
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
    }
}