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
                //Initialization
                //db.Users.Add(new User { Login = "log1", Email = "em1", Password = "pass1", Name = "name1", Proffesion = "Admin" });
                //db.Users.Add(new User { Login = "log2", Email = "em2", Password = "pass2", Name = "name2", Proffesion = "Seller" });
                //db.Users.Add(new User { Login = "log3", Email = "em3", Password = "pass3", Name = "name3", Proffesion = "Seller" });
                //db.Users.Add(new User { Login = "log4", Email = "em4", Password = "pass4", Name = "name4", Proffesion = "Seller" });
                //db.Users.Add(new User { Login = "log5", Email = "em5", Password = "pass5", Name = "name5", Proffesion = "Seller" });
                //db.Products.Add(new Product { Image = $"{System.Environment.CurrentDirectory}" + @"\Images\Sandali.png", Category = "Sandali", Model = "Addidas", Price = 322, Quantity = 10, Type = "Male" });
                //db.Products.Add(new Product { Image = $"{System.Environment.CurrentDirectory}" + @"\Images\Kedi.png", Category = "Kedi", Model = "Nike", Price = 123, Quantity = 100, Type = "Male" });
                //db.Products.Add(new Product { Image = $"{System.Environment.CurrentDirectory}" + @"\Images\Crosi.png", Category = "Crosi", Model = "Patrol", Price = 44, Quantity = 1, Type = "Female" });
                //db.Products.Add(new Product { Image = $"{System.Environment.CurrentDirectory}" + @"\Images\Tufli.png", Category = "Tufli", Model = "Addidas", Price = 1, Quantity = 10, Type = "Female" });
                //db.SaveChanges();
            }
        }
        private void ButtonFechar_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
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
    }
}
