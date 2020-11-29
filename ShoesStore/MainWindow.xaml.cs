using ShoesStore.Commands;
using ShoesStore.Models;
using ShoesStore.Utils;
using ShoesStore.ViewModels;
using ShoesStore.Views;
using ShoesStore.Views.Dialogs;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ShoesStore
{
    public partial class MainWindow : Window
    {
        private MainWindowModel viemodel = new MainWindowModel();
        public MainWindow()
        {
            InitializeComponent();
            DataContext = viemodel;
            if(GlobalInfo.CurrentUser.Proffesion == "Seller")
            {
                SellerMode();
            }
        }

        private void SellerMode()
        {
            CategoryMenu.Visibility = Visibility.Collapsed;
            CompanyMenu.Visibility = Visibility.Collapsed;
            UserMenu.Visibility = Visibility.Collapsed;
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
            UpdateViewCommand cmd = new UpdateViewCommand(viemodel);
            switch (index)
            {
                case 0:
                    cmd.Execute("Home");
                    break;

                case 1:
                    cmd.Execute("Store");
                    break;

                case 2:
                    cmd.Execute("Orders");
                    break;

                case 3:
                    cmd.Execute("Companies");
                    break;

                case 4:
                    cmd.Execute("Categories");
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
            GridCursor.Margin = new Thickness(0, (150 + (60 * index)), 0, 0);
        }

        private void btnQuit_Click(object sender, RoutedEventArgs e)
        {
            SignInView login = new SignInView();
            login.Show();
            this.Close();
        }

        private void btnCart_Click(object sender, RoutedEventArgs e)
        {
            CartView cartView = new CartView();
            cartView.ShowDialog();
        }
    }
}