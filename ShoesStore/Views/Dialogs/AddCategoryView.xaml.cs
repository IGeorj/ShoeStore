using ShoesStore.ViewModels;
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
using System.Windows.Shapes;

namespace ShoesStore.Views.Dialogs
{

    public partial class AddCategoryView : Window
    {
        AddEditCategoryViewModel viewModel = new AddEditCategoryViewModel();
        public AddCategoryView()
        {
            InitializeComponent();
            DataContext = viewModel;
        }
        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(Name.Text))
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
