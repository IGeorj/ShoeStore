using ShoesStore.ViewModels;
using System.Windows;
using System.Windows.Input;

namespace ShoesStore.Views
{
    public partial class SignInView : Window
    {
        private SignInViewModel viewModel = new SignInViewModel();

        public SignInView()
        {
            InitializeComponent();
            DataContext = viewModel;
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                this.DragMove();
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            if (btnCraete.Content.ToString() == "Create Account")
            {
                btnCraete.CommandParameter = "Registration";
                btnCraete.Content = "Back";
            }
            else
            {
                btnCraete.CommandParameter = "Login";
                btnCraete.Content = "Create Account";
            }
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}