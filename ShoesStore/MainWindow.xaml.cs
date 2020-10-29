using ShoesStore.Commands;
using ShoesStore.Models;
using ShoesStore.ViewModels;
using ShoesStore.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ShoesStore
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        MainWindowModel mvd = new MainWindowModel();

        public MainWindow()
        {
            InitializeComponent();

            DataContext = mvd;
            using (ApplicationContext db = new ApplicationContext())
            {
                ////DB Initialization
                //db.Users.Add(new User { Login = "log1", Email = "em1", Password = "pass1", Name = "name1", Proffesion = "Admin" });
                //db.Users.Add(new User { Login = "log2", Email = "em2", Password = "pass2", Name = "name2", Proffesion = "Seller" });
                //db.Users.Add(new User { Login = "log3", Email = "em3", Password = "pass3", Name = "name3", Proffesion = "Seller" });
                //db.Users.Add(new User { Login = "log4", Email = "em4", Password = "pass4", Name = "name4", Proffesion = "Seller" });
                //db.Users.Add(new User { Login = "qqq", Email = "qqq", Password = "qqq", Name = "qqq", Proffesion = "Admin" });
                //db.SaveChanges();
                //db.Categories.Add(new Category { Name = "Shoes" });
                //db.SaveChanges();
                //db.Products.Add(new Product { Image = $"{System.Environment.CurrentDirectory}" + @"\Images\Sandali.png", Category = db.Categories.First(), Price = 322, Quantity = 10, Type = "Male" });
                //db.Products.Add(new Product { Image = $"{System.Environment.CurrentDirectory}" + @"\Images\Kedi.png", Category = db.Categories.First(), Price = 123, Quantity = 100, Type = "Male" });
                //db.Products.Add(new Product { Image = $"{System.Environment.CurrentDirectory}" + @"\Images\Crosi.png", Category = db.Categories.First(), Price = 44, Quantity = 1, Type = "Female" });
                //db.Products.Add(new Product { Image = $"{System.Environment.CurrentDirectory}" + @"\Images\Tufli.png", Category = db.Categories.First(), Price = 1, Quantity = 10, Type = "Female" });
                //db.SaveChanges();
                //db.Orders.Add(new Order { Date = "20.20.2020", User = db.Users.First(), TotalPrice = 1000 }); ;
                //db.SaveChanges();
                //db.OrderDetails.Add(new OrderDetail { Order = db.Orders.First(), Product = db.Products.First(), Quantity = 5 });
                //db.SaveChanges();
            }
            SignInView login = new SignInView();
            login.ShowDialog();
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                this.DragMove();
        }

        private void ListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = ListViewMenu.SelectedIndex;
            MoveCursorMenu(index);
            UpdateViewCommand cmd = new UpdateViewCommand(mvd);
            switch (index)
            {
                case 0:
                    cmd.Execute("Home");
                    break;
                case 1:
                    cmd.Execute("Store");
                    break;
                case 4:
                    cmd.Execute("Orders");
                    break;
                case 5:
                    cmd.Execute("Users");
                    break;
                default:
                    break;
            }
        }

        private void MoveCursorMenu(int index)
        {
            TrainsitionigContentSlide.OnApplyTemplate();
            GridCursor.Margin = new Thickness(0, (100 + (60 * index)), 0, 0);
        }

        private void btnQuit_Click(object sender, RoutedEventArgs e)
        {
            SignInView login = new SignInView();
            login.Show();
            this.Close();
        }
    }
}
