using ShoesStore.ViewModels;
using ShoesStore.Views.Dialogs;
using System.Windows;
using System.Windows.Controls;

namespace ShoesStore.Views
{
    public partial class CompaniesView : UserControl
    {
        private CompaniesViewModel viewModel = new CompaniesViewModel();

        public CompaniesView()
        {
            InitializeComponent();
            DataContext = viewModel;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddCompanyView addProductView = new AddCompanyView();
            if (addProductView.ShowDialog() == true)
            {
                viewModel.LoadData();
            }
        }

        private void ChangeButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            EditCompanyView editview = new EditCompanyView();
            editview.DataContext = new AddEditCompanyViewModel(viewModel.SelectedCompany.Id);
            if (editview.ShowDialog() == true)
            {
                viewModel.LoadData();
            }
        }
    }
}