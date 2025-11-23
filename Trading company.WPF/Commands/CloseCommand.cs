using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TradingCompany.WPF.Interfaces;

namespace TradingCompany.WPF.Commands
{
    internal class CloseCommand : ICommand
    {
        private readonly ICloseable _vm;

        public CloseCommand(ICloseable vm)
        {
            _vm = vm;
        }

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        public bool CanExecute(object parameter) => true;

        public void Execute(object parameter)
        {
            _vm.Close?.Invoke();
        }
    }
}
