using System;
using System.ComponentModel;

namespace InvoiceMaker.Core
{
    /// <summary>
    ///   The base ViewModel from which the other ViewModels inherit.
    ///   Implementing "INotifyPropertyChanged" to support property change notifications.
    /// </summary>
    public class ViewModelBase : INotifyPropertyChanged
    {

        // Occurs when a property value changes.
        public event PropertyChangedEventHandler? PropertyChanged;


        /// <summary>
        ///   This method is intended to be called by derived ViewModels whenever a property value changes.
        /// </summary>
        /// <param name="propertyName"></param>
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
