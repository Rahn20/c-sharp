using System;
using System.Windows.Input;

namespace InvoiceMaker.Core
{
    /// <summary>
    ///   Represents a command that can be executed and optionally has a condition that determines if it can be executed.
    /// </summary>
    public class RelayCommand : ICommand
    {
        private readonly Action<object> _execute;
        private readonly Func<object, bool> _canExecute;


        // Occurs when the ability of the command to execute changes.
        public event EventHandler? CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }


        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }


        /// <summary>
        ///    Determines if the command can be executed based on the optional canExecute logic.
        /// </summary>
        /// <param name="parameter"> The parameter to pass to the canExecute function </param>
        /// <returns> True if the command can be executed, otherwise, false </returns>
        public bool CanExecute(object? parameter)
        {
            return _canExecute == null || _canExecute(parameter);
        }


        /// <summary>
        ///   Executes the command logic.
        /// </summary>
        /// <param name="parameter"> The parameter to pass to the execute action. </param>
        public void Execute(object? parameter)
        {
            _execute(parameter);
        }


        /// <summary>
        ///   Raises the CanExecuteChanged event to indicate that the return value of CanExecute has changed.
        /// </summary>
        //protected void OnCanExecuteChanged(object? parameter)
        //{
        //    CanExecuteChanged?.Invoke(this, new EventArgs());
        //}
    }
}
