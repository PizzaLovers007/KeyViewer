using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace KeyViewer
{
    public class Command : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private Action _execute { get; set; }
        private Action<object> _executeParam { get; set; }
        public Func<bool> _canExecute { get; set; }
        public Func<object, bool> _canExecuteParam { get; set; }

        public Command(Action execute) : this(execute, null) { }

        public Command(Action execute, Func<bool> canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public Command(Action<object> execute) : this(execute, null) { }

        public Command(Action<object> execute, Func<object, bool> canExecute)
        {
            _executeParam = execute;
            _canExecuteParam = canExecute;
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute?.Invoke() ?? _canExecuteParam?.Invoke(parameter) ?? true;
        }

        public void Execute(object parameter)
        {
            if (CanExecute(parameter))
            {
                _execute?.Invoke();
                _executeParam?.Invoke(parameter);
            }
        }

        public void ChangeCanExecute()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
