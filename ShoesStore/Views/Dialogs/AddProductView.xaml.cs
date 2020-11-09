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
    /// <summary>
    /// Логика взаимодействия для AddProductView.xaml
    /// </summary>
    public partial class AddProductView : Window
    {
        AddProductViewModel viewModel = new AddProductViewModel();
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
