using System;
using System.Windows.Input;

namespace PixelSort.Domain
{
    internal class PixelsCommandHandler : ICommand
    {
        private readonly Action _action;
        private readonly Func<bool> _canExeculte;

        public event EventHandler CanExecuteChanged;


        public PixelsCommandHandler(Action action, Func<bool> canExeculte = null)
        {
            _action = action;
            _canExeculte = canExeculte;
        }

        public bool CanExecute(object parameter)
        {
            var result = _canExeculte?.Invoke() ?? true;
            return result;
        }

        public void Execute(object parameter)
        {
            _action();
        }
    }
}
