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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ShoesStore.Views
{
    /// <summary>
    /// Логика взаимодействия для RegistrationView.xaml
    /// </summary>
    public partial class RegistrationView : UserControl
    {
        RegistrationViewModel rvm = new RegistrationViewModel();
        public RegistrationView()
        {
            InitializeComponent();
            DataContext = rvm;
        }

        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            rvm.Password = PasswordBox.Password;
            rvm.ConfirmPassword = ConfirmPasswordBox.Password;
            if (string.IsNullOrEmpty(NameBox.Text))
            {
                NameBox.Focus();
                return;
            }
            else if (string.IsNullOrEmpty(UsernameBox.Text))
            {
                UsernameBox.Focus();
                return;
            }
            if(string.IsNullOrEmpty(PasswordBox.Password))
            {
                PasswordBox.Focus();
            }
            else
            {
                ConfirmPasswordBox.Focus();
            }
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            NameBox.Focus();
        }
    }
}
