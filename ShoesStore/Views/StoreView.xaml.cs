using ShoesStore.Models;
using ShoesStore.ViewModels;
using ShoesStore.Views.Dialogs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ShoesStore.Views
{
    /// <summary>
    /// Логика взаимодействия для StoreView.xaml
    /// </summary>
    public partial class StoreView : UserControl
    {
        StoreViewModel viewModel = new StoreViewModel();
        public StoreView()
        {
            InitializeComponent();
            DataContext = viewModel;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            AddProductView addProductView = new AddProductView();
            if(addProductView.ShowDialog() == true)
            {
                viewModel.LoadDataAsync();
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            ListBoxItem selectedItem = (ListBoxItem)listboxProducts.ItemContainerGenerator.
                               ContainerFromItem(((Button)sender).DataContext);
            selectedItem.IsSelected = true;
        }
    }
}
