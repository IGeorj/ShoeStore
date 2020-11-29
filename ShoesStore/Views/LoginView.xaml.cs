using ShoesStore.Commands;
using ShoesStore.Models;
using ShoesStore.ViewModels;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ShoesStore.Views
{
    public partial class LoginView : UserControl
    {
        private LoginViewModel viewModel = new LoginViewModel();

        public LoginView()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                //User user1 = new User { Login = "admin", Email = "admin", Password = "admin", Name = "admin", Proffesion = "Admin" };
                //User user2 = new User { Login = "qwe", Email = "qwe", Password = "qwe", Name = "qwe", Proffesion = "Seller" };
                //db.Users.Add(user1);
                //db.Users.Add(user2);
                //Company company1 = new Company { Name = "Addidas" };
                //Company company2 = new Company { Name = "Nike" };
                //Company company3 = new Company { Name = "Patrol" };
                //Company company4 = new Company { Name = "Vans" };
                //db.Companies.Add(company1);
                //db.Companies.Add(company2);
                //db.Companies.Add(company3);
                //db.Companies.Add(company4);
                //Category category1 = new Category { Name = "Sandals" };
                //Category category2 = new Category { Name = "Sneakers" };
                //Category category3 = new Category { Name = "Gumshoes" };
                //Category category4 = new Category { Name = "High Heels" };
                //db.Categories.Add(category1);
                //db.Categories.Add(category2);
                //db.Categories.Add(category3);
                //db.Categories.Add(category4);
                //db.SaveChanges();
                //Product product1 = new Product { Image = $"{System.Environment.CurrentDirectory}" + @"\Images\Sandali.png", Name = "Lampada", Category = category1, Company = company1, Price = 100, Quantity = 10, Type = "Male" };
                //Product product2 = new Product { Image = $"{System.Environment.CurrentDirectory}" + @"\Images\Crosi.png", Name = "EZY", Category = category2, Company = company2, Price = 150, Quantity = 15, Type = "Male" };
                //Product product3 = new Product { Image = $"{System.Environment.CurrentDirectory}" + @"\Images\Kedi.png", Name = "G10", Category = category3, Company = company3, Price = 200, Quantity = 100, Type = "Female" };
                //Product product4 = new Product { Image = $"{System.Environment.CurrentDirectory}" + @"\Images\Tufli.png", Name = "LadyFirst", Category = category4, Company = company4, Price = 550, Quantity = 10, Type = "Female" };
                //db.Products.Add(product1);
                //db.Products.Add(product2);
                //db.Products.Add(product3);
                //db.Products.Add(product4);
                //db.SaveChanges();
                //Order order1 = new Order { Date = "20.20.2020", User = user1, Status = "Pending", TotalPrice = 0 };
                //db.Orders.Add(order1);
                //db.SaveChanges();
                //OrderDetail orderDetail1 = new OrderDetail { Order = order1, Product = product1, Quantity = 10, TotalPrice = 10 * product1.Price };
                //OrderDetail orderDetail2 = new OrderDetail { Order = order1, Product = product2, Quantity = 5, TotalPrice = 5 * product2.Price };
                //db.OrderDetails.Add(orderDetail1);
                //db.OrderDetails.Add(orderDetail2);
                //Order ordertotal = db.Orders.First();
                //var result = db.OrderDetails.Where(x => x.Order.Id == ordertotal.Id);
                //foreach (var item in result)
                //{
                //    ordertotal.TotalPrice += item.TotalPrice;
                //}
                //db.SaveChanges();
            }
            DataContext = viewModel;
            InitializeComponent();
            if (viewModel.LoginAction == null)
            {
                viewModel.LoginAction = new Action(() => Login());
            }
            UsernameBox.Focus();
        }

        public void Login()
        {
            MainWindow mv = new MainWindow();
            mv.Show();
            Window.GetWindow(this).Close();
        }

        private void Login_KeyDown(object sender, KeyEventArgs e)
        {
            viewModel.Password = PasswordBox.Password;
            if (e.Key == Key.Enter)
            {
                viewModel.LoginCommand.Execute(sender);
            }
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            viewModel.Password = PasswordBox.Password;
        }
    }
}