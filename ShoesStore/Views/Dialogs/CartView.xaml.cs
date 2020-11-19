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
    /// Логика взаимодействия для CartView.xaml
    /// </summary>
    public partial class CartView : Window
    {
        CartViewModel viewModel = new CartViewModel();
        public CartView()
        {
            DataContext = viewModel;
            InitializeComponent();

        }
        private void DG_LayoutUpdated(object sender, EventArgs e)
        {
            Thickness t = lblTotal.Margin;
            t.Left = (CartGrid.Columns[0].ActualWidth + CartGrid.Columns[1].ActualWidth);
            lblTotal.Margin = t;
            lblTotal.Width = CartGrid.Columns[2].ActualWidth;

            lblTotalSalesInvoiceAmount.Width = CartGrid.Columns[3].ActualWidth;
        }
    }
}
