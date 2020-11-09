using ShoesStore.ViewModels;
using ShoesStore.Views.Dialogs;
using System.Windows;
using System.Windows.Controls;

namespace ShoesStore.Views
{
    /// <summary>
    /// Логика взаимодействия для StoreView.xaml
    /// </summary>
    public partial class StoreView : UserControl
    {
        private StoreViewModel viewModel = new StoreViewModel();

        public StoreView()
        {
            InitializeComponent();
            DataContext = viewModel;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddProductView addProductView = new AddProductView();
            if (addProductView.ShowDialog() == true)
            {
                viewModel.LoadData();
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            ListBoxItem selectedItem = (ListBoxItem)listboxProducts.ItemContainerGenerator.
                               ContainerFromItem(((Button)sender).DataContext);
            selectedItem.IsSelected = true;
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            ListBoxItem selectedItem = (ListBoxItem)listboxProducts.ItemContainerGenerator.
                               ContainerFromItem(((Button)sender).DataContext);
            selectedItem.IsSelected = true;
            EditProductView editProduct = new EditProductView();
            editProduct.DataContext = new EditProductViewModel();
            ((EditProductViewModel)editProduct.DataContext).Product = viewModel.SelectedProduct;
            editProduct.ShowDialog();
        }
    }
}