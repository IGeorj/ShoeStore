using ShoesStore.ViewModels;
using ShoesStore.Views.Dialogs;
using System.Windows;
using System.Windows.Controls;

namespace ShoesStore.Views
{
    public partial class CategoriesView : UserControl
    {
        private CategoriesViewModel viewModel = new CategoriesViewModel();

        public CategoriesView()
        {
            InitializeComponent();
            DataContext = viewModel;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddCategoryView addProductView = new AddCategoryView();
            if (addProductView.ShowDialog() == true)
            {
                viewModel.LoadDataAsync();
            }
        }

        private void ChangeButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            EditCategoryView editview = new EditCategoryView();
            editview.DataContext = new AddEditCategoryViewModel(viewModel.SelectedCategory.Id);
            if (editview.ShowDialog() == true)
            {
                viewModel.LoadDataAsync();
            }
        }
    }
}