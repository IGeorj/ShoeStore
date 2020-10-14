using ShoesStore.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace ShoesStore.Commands
{
    public class ChangeProfCommand : ICommand
    {
        private UsersViewModel viewModel;
        public ChangeProfCommand(UsersViewModel viewModel)
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
            viewModel.ChangeProf();
        }
    }
}
