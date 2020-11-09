using ShoesStore.Commands;
using ShoesStore.Models;
using ShoesStore.ViewModels;
using ShoesStore.Views;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ShoesStore
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainWindowModel mvd = new MainWindowModel();

        public MainWindow()
        {
            InitializeComponent();

            DataContext = mvd;
            using (ApplicationContext db = new ApplicationContext())
            {
                ////DB Initialization
                //db.Users.Add(new User { Login = "admin", Email = "admin", Password = "admin", Name = "admin", Proffesion = "Admin" });
                //db.Users.Add(new User { Login = "log2", Email = "em2", Password = "pass2", Name = "name2", Proffesion = "Seller" });
                //db.Users.Add(new User { Login = "log3", Email = "em3", Password = "pass3", Name = "name3", Proffesion = "Seller" });
                //db.Users.Add(new User { Login = "log4", Email = "em4", Password = "pass4", Name = "name4", Proffesion = "Seller" });
                //db.Users.Add(new User { Login = "qqq", Email = "qqq", Password = "qqq", Name = "qqq", Proffesion = "Admin" });
                //db.SaveChanges();
                //db.Companies.Add(new Company { Name = "Addidas" });
                //db.Companies.Add(new Company { Name = "Nike" });
                //db.Companies.Add(new Company { Name = "Patrol" });
                //db.Companies.Add(new Company { Name = "Vans" });
                //db.SaveChanges();
                //db.Categories.Add(new Category { Name = "Sandals" });
                //db.Categories.Add(new Category { Name = "Sneakers" });
                //db.Categories.Add(new Category { Name = "Gumshoes" });
                //db.Categories.Add(new Category { Name = "High Heels" });
                //db.SaveChanges();
                //db.Products.Add(new Product { Image = $"{System.Environment.CurrentDirectory}" + @"\Images\Sandali.png", Name = "G10", Category = db.Categories.FirstOrDefault(x => x.Name == "Sandals"), Company = db.Companies.FirstOrDefault(x => x.Name == "Patrol"), Price = 80, Quantity = 10, Type = "Male" });
                //db.Products.Add(new Product { Image = $"{System.Environment.CurrentDirectory}" + @"\Images\Crosi.png", Name = "Air Force", Category = db.Categories.FirstOrDefault(x => x.Name == "Sneakers"), Company = db.Companies.FirstOrDefault(x => x.Name == "Addidas"), Price = 120, Quantity = 20, Type = "Male" });
                //db.Products.Add(new Product { Image = $"{System.Environment.CurrentDirectory}" + @"\Images\Kedi.png", Name = "Yeezy", Category = db.Categories.FirstOrDefault(x => x.Name == "Gumshoes"), Company = db.Companies.FirstOrDefault(x => x.Name == "Nike"), Price = 130, Quantity = 30, Type = "Female" });
                //db.Products.Add(new Product { Image = $"{System.Environment.CurrentDirectory}" + @"\Images\Tufli.png", Name = "Lady Love", Category = db.Categories.FirstOrDefault(x => x.Name == "High Heels"), Company = db.Companies.FirstOrDefault(x => x.Name == "Vans"), Price = 50, Quantity = 40, Type = "Female" });
                //db.SaveChanges();
                //db.Orders.Add(new Order { Date = "20.20.2020", User = db.Users.First(), TotalPrice = 0 }); ;
                //db.SaveChanges();
                //db.OrderDetails.Add(new OrderDetail { Order = db.Orders.First(), Product = db.Products.FirstOrDefault(x => x.Category.Name == "Sandals"), Quantity = 1 });
                //db.OrderDetails.Add(new OrderDetail { Order = db.Orders.First(), Product = db.Products.FirstOrDefault(x => x.Category.Name == "Sneakers"), Quantity = 5 });
                //db.OrderDetails.Add(new OrderDetail { Order = db.Orders.First(), Product = db.Products.FirstOrDefault(x => x.Category.Name == "Gumshoes"), Quantity = 5 });
                //db.SaveChanges();
                //Order order = db.Orders.First();
                //var result = db.OrderDetails.Where(x => x.Order.Id == order.Id);
                //foreach (var item in result)
                //{
                //    order.TotalPrice += item.Quantity * item.Product.Price;
                //}
                //db.SaveChanges();
            }
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