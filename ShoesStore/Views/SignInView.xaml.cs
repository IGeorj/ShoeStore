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

namespace ShoesStore.Views
{
    /// <summary>
    /// Логика взаимодействия для LoginView.xaml
    /// </summary>
    public partial class SignInView : Window
    {
        SignInViewModel svm = new SignInViewModel();
        public SignInView()
        {
            InitializeComponent();
            DataContext = svm;
        }
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                this.DragMove();
        }

        private void btnCraete_Click(object sender, RoutedEventArgs e)
        {
            if(btnCraete.Content.ToString() == "Create Account")
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
