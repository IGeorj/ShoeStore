using ShoesStore.ViewModels;
using System;
using System.Windows;
using System.Windows.Controls;

namespace ShoesStore.Views
{
    public partial class RegistrationView : UserControl
    {
        private RegistrationViewModel viewModel = new RegistrationViewModel();

        public RegistrationView()
        {
            InitializeComponent();
            DataContext = viewModel;
            if (viewModel.RegisterAction == null)
            {
                viewModel.RegisterAction = new Action(() => Clear());
            }
        }

        public void Clear()
        {
            NameBox.Text = "";
            UsernameBox.Text = "";
            PasswordBox.Password = "";
            ConfirmPasswordBox.Password = "";
        }

        private void btnConfirm_Click(object sender, RoutedEventArgs e)
        {
            viewModel.Password = PasswordBox.Password;
            viewModel.ConfirmPassword = ConfirmPasswordBox.Password;
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
            if (string.IsNullOrEmpty(PasswordBox.Password))
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