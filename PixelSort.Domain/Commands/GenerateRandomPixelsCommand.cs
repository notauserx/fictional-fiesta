using System;
using System.Windows.Input;

namespace PixelSort.Domain
{
    internal class GenerateRandomPixelsCommand : ICommand
    {
        private readonly Action _action;

        public event EventHandler CanExecuteChanged;


        public GenerateRandomPixelsCommand(Action action)
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
