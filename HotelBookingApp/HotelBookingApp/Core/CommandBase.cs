using System;
using System.Windows.Input;

namespace HotelBookingApp.Core
{
    /// <summary>
    ///  Represents a base command that can be executed and optionally has a condition that determines if it can be executed.
    /// </summary>
    public class CommandBase : ICommand
    {
        // The action to be executed when the command is invoked.
        private readonly Action<object> _execute;

        // A function that determines whether the command can be executed.
        private readonly Func<object, bool> _canExecute;


        /// <summary> </summary>
        /// <param name="execute"> The action to execute when the command is invoked </param>
        /// <param name="canExecute"> An optional function that determines if the command can execute </param>
        public CommandBase(Action<object> execute, Func<object, bool> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }


        public event EventHandler? CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        /// <summary>
        ///   Determines if the command can be executed based on the optional canExecute logic.
        /// </summary>
        /// <param name="parameter"> An optional parameter for the command execution </param>
        /// <returns> True if the command can execute, otherwise, false. </returns>
        public virtual bool CanExecute(object? parameter) => _canExecute == null || _canExecute(parameter);

        /// <summary>
        ///  Executes the command logic.
        /// </summary>
        /// <param name="parameter"> An optional parameter for the command execution </param>
        public virtual void Execute(object? parameter) => _execute(parameter);
    }
}
