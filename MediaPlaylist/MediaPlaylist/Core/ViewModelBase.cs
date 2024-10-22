using System;
using System.ComponentModel;

namespace MediaPlaylist.Core
{
    /// <summary>
    ///   The base ViewModel that other ViewModels inherit from. 
    ///   Implementing "INotifyPropertyChanged" to support property change notifications.
    /// </summary>
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
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
