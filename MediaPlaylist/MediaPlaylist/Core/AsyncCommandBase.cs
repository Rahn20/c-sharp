using System;
using System.Windows.Input;

namespace MediaPlaylist.Core
{
    /// <summary>
    ///  Represents a asynchronous base command that can be executed and optionally has a condition that determines if it can be executed.
    /// </summary>
    public class AsyncCommandBase : CommandBase
    {
        // The asynchronous function to execute when the command is invoked.
        private readonly Func<object, Task> _executeAsync;

        // True if the command is currently executing.
        private bool _isExecuting;

        /// <summary>
        ///   Constructor that accepts an asynchronous execution function.
        /// </summary>
        /// <param name="executeAsync"> The asynchronous action to execute.</param>
        /// <param name="canExecute"> Optional function that determines whether the command can execute. </param>
        public AsyncCommandBase(Func<object, Task> executeAsync, Func<object, bool>? canExecute = null)
            : base(o => { }, canExecute)
        {
            _executeAsync = executeAsync;
        }

        /// <summary>
        ///   Determines whether the command can execute. It checks if the command is currently executing.
        /// </summary>
        /// <param name="parameter"> An optional parameter .</param>
        /// <returns> True if the command can execute, false otherwise. </returns>
        public override bool CanExecute(object? parameter)
        {
            return !_isExecuting && base.CanExecute(parameter);
        }

        /// <summary>
        ///   Executes the asynchronous action provided to the command.
        /// </summary>
        /// <param name="parameter"> An optional parameter .</param>
        public override async void Execute(object? parameter)
        {
            if (!CanExecute(parameter)) return;

            _isExecuting = true;
            RaiseCanExecuteChanged(); // Notify UI

            try
            {
                await _executeAsync(parameter);
            }
            finally
            {
                _isExecuting = false;
                RaiseCanExecuteChanged();
            }
        }

        /// <summary>
        ///  Raises the CanExecuteChanged event to update the UI state when execution status changes.
        /// </summary>
        public new void RaiseCanExecuteChanged()
        {
            CommandManager.InvalidateRequerySuggested();
        }
    }
}
