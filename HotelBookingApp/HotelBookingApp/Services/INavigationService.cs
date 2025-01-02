using HotelBookingApp.Core;
using System;

namespace HotelBookingApp.Services
{
    /// <summary>
    ///  Navigation services interface, provides properties and methods for navigating between ViewModels.
    /// </summary>
    public interface INavigationService
    {
        /// <summary>
        ///   Gets the current ViewModel that is being displayed.
        /// </summary>
        ViewModelBase CurrentViewModel { get; }

        /// <summary>
        ///   Navigates to a new ViewModel by creating an instance of the specified ViewModel type.
        /// </summary>
        /// <typeparam name="TViewModel"> The type of ViewModel to navigate to </typeparam>
        void NavigateTo<TViewModel>() where TViewModel : ViewModelBase;

        /// <summary>
        ///   Occurs when the current view model changes.
        /// </summary>
        event Action CurrentViewModelChanged;
    }
}
