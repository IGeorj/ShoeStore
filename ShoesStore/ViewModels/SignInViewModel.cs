using ShoesStore.Commands;
using ShoesStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace ShoesStore.ViewModels
{
    public class SignInViewModel : BaseViewModel
    {
        public ICommand UpdateViewCommand { get; set; }

        private BaseViewModel _selectedViewModel = new LoginViewModel();
        public BaseViewModel SelectedViewModel
        {
            get { return _selectedViewModel; }
            set
            {
                _selectedViewModel = value;
                OnPropertyChanged(nameof(SelectedViewModel));
            }
        }
        public SignInViewModel()
        {
            UpdateViewCommand = new UpdateSignInViewCommand(this);
        }
    }
}
