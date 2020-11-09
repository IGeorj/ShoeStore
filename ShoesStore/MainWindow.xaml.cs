using ShoesStore.Commands;
using ShoesStore.Models;
using ShoesStore.ViewModels;
using ShoesStore.Views;
using System.Linq;
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