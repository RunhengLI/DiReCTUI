using System;
using System.Diagnostics;
using System.Windows.Input;

namespace DiReCTUI.Controls
{
    /// <summary>
    /// A class that transmits Icommands from view models to front-end binded objects
    /// This class is copied from Daniel Yhung's code, github address https://github.com/yhung119/DiReCTUI
    /// There's no need for further modifications on this file
    /// </summary>
    public class RelayCommand : ICommand
    {
  
        readonly Action<object> _execute;
        readonly Predicate<object> _canExecute;
        
        /// <summary>
        /// Creates a new command that can always execute.
        /// </summary>
        /// <param name="execute">The execution logic.</param>
        public RelayCommand(Action<object> execute)
            : this(execute, null)
        {
        }

        public RelayCommand(Action<object> execute, object parameter)
        {
            _execute(parameter);
        }

        /// <summary>
        /// Creates a new command.
        /// </summary>
        /// <param name="execute">The execution logic.</param>
        /// <param name="canExecute">The execution status logic.</param>
        public RelayCommand(Action<object> execute, Predicate<object> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException("execute");

            _execute = execute;
            _canExecute = canExecute;
        }

        
        [DebuggerStepThrough]
        public bool CanExecute(object parameter)
        {
            return _canExecute == null ? true : _canExecute(parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Execute(object parameter)
        {
            _execute(parameter);
        }

 
    }   
}