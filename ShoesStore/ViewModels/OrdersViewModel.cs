using Microsoft.Win32;
using OfficeOpenXml;
using ShoesStore.Commands;
using ShoesStore.Models;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;

namespace ShoesStore.ViewModels
{
    public class OrdersViewModel : BaseViewModel
    {
        private string _searchedText;

        public string SearchedText
        {
            get { return _searchedText; }
            set
            {
                _searchedText = value;
                OnPropertyChanged();
                LoadDataAsync();
            }
        }
        private Order _selectedOrder;

        public Order SelectedOrder
        {
            get => _selectedOrder;
            set
            {
                _selectedOrder = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Order> _orders;

        public ObservableCollection<Order> Orders
        {
            get { return _orders; }
            set
            {
                _orders = value;
                OnPropertyChanged();
            }
        }

        private RelayCommand _chechoutCommand;

        public RelayCommand CheckoutCommand
        {
            get
            {
                return _chechoutCommand ??
                  (_chechoutCommand = new RelayCommand(obj =>
                  {
                      Checkout();
                  }));
            }
        }

        public void Checkout()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (ExcelPackage excelPackage = new ExcelPackage())
            {
                excelPackage.Workbook.Properties.Author = "ShoeStore";
                excelPackage.Workbook.Properties.Title = $"Order {SelectedOrder.Id}";
                excelPackage.Workbook.Properties.Created = DateTime.Now;

                ExcelWorksheet xlWorkSheet = excelPackage.Workbook.Worksheets.Add("Sheet 1");
                xlWorkSheet.Cells[1, 1].Value = "ID";
                xlWorkSheet.Cells[1, 2].Value = "Name";
                xlWorkSheet.Cells[1, 3].Value = "Quantity";
                xlWorkSheet.Cells[1, 4].Value = "Price";
                int counter = 2;
                foreach (var item in SelectedOrder.OrderDetails)
                {
                    xlWorkSheet.Cells[counter, 1].Value = item.Id;
                    xlWorkSheet.Cells[counter, 2].Value = item.Product.Name;
                    xlWorkSheet.Cells[counter, 3].Value = item.Quantity;
                    xlWorkSheet.Cells[counter, 4].Value = item.Product.Price;
                    counter++;
                }
                xlWorkSheet.Cells[counter, 3].Value = $"Total price =";
                xlWorkSheet.Cells[counter, 3].Style.WrapText = true;
                xlWorkSheet.Cells[counter, 4].Value = SelectedOrder.TotalPrice;

                SaveFileDialog saveFileDialog1 = new SaveFileDialog();

                saveFileDialog1.Filter = "Excel files (*.xlsx)|*.xlsx|All files (*.*)|*.*";
                saveFileDialog1.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                if (saveFileDialog1.ShowDialog() == true)
                {
                    FileInfo fi = new FileInfo(saveFileDialog1.FileName);
                    excelPackage.SaveAs(fi);
                }
            }

        }

        public OrdersViewModel()
        {
            SearchedText = "";
            LoadDataAsync();
        }

        public async void LoadDataAsync()
        {
            if (SearchedText == "")
            {
                Orders = await DatabaseController.GetOrdersAsync();
            }
            else
            {
                Orders = await DatabaseController.SearchOrdersAsync(SearchedText);
            }
        }
    }
}