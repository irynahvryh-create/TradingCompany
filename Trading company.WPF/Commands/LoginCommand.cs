using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TradingCompany.WPF.ViewModels;

namespace TradingCompany.WPF.Commands
{
    internal class LoginCommand : ICommand
    {
        private LoginViewModel _vm;
        public LoginCommand(LoginViewModel vm)
        {
            _vm = vm;
        }

        #region ICommand
        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }

        public bool CanExecute(object parameter) => _vm.CanLogin;

        public void Execute(object parameter)
        {
            _vm.LoginError = null;
            if (_vm.Login())
            {
                _vm.LoginSuccessful?.Invoke();
            }
            else
            {
                _vm.LoginFailed?.Invoke();
            }
        }
        #endregion
    }
}
