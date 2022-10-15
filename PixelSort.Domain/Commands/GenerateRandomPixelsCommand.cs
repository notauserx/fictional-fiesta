using System;
using System.Windows.Input;

namespace PixelSort.Domain
{
    internal class PixelsCommandHandler : ICommand
    {
        private readonly Action _action;

        public event EventHandler CanExecuteChanged;


        public PixelsCommandHandler(Action action)
        {
            _action = action;
        }

        public bool CanExecute(object parameter)
        {
            // TODO :: if pixels are generating reture false
            return true;
        }

        public void Execute(object parameter)
        {
            _action();
        }
    }
}
