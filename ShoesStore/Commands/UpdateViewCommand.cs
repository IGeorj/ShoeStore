using ShoesStore.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace ShoesStore.Commands
{
    public class UpdateViewCommand : ICommand
    {
        private MainWindowModel viewModel;
        public UpdateViewCommand(MainWindowModel viewModel)
        {
            this.viewModel = viewModel;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            if(parameter.ToString() == "Home")
            {
                viewModel.SelectedViewModel = new HomeViewModel();
            }
            if (parameter.ToString() == "Store")
            {
                viewModel.SelectedViewModel = new StoreViewModel();
            }
            if (parameter.ToString() == "Users")
            {
                viewModel.SelectedViewModel = new UsersViewModel();
            }
        }
    }
}
