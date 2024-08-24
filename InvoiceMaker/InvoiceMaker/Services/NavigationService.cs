using InvoiceMaker.Core;
using System;
using System.Linq;
using System.Text;

namespace InvoiceMaker.Services
{
    /// <summary>
    ///   Defines an interface for navigation services. Provides properties and methods for navigating between ViewModels.
    /// </summary>
    public interface INavigationService
    {
        /// <summary>
        ///    Gets the current ViewModel that is being displayed.
        /// </summary>
        ViewModelBase CurrentView { get; }

        /// <summary>
        ///   Navigates to a new ViewModel by creating an instance of the specified ViewModel type.
        /// </summary>
        /// <typeparam name="TViewModel"> The type of ViewModel to navigate to </typeparam>
        void NavigateTo<TViewModel>() where TViewModel : ViewModelBase;

        /// <summary>
        ///    Creates and returns an instance of the specified ViewModel type.
        /// </summary>
        /// <typeparam name="TViewModel"> The type of ViewModel to navigate to  </typeparam>
        /// <returns> An instance of the specified viewmodel type </returns>
        TViewModel Navigate<TViewModel>() where TViewModel : ViewModelBase;
    }


    public class NavigationService : ViewModelBase, INavigationService
    {
        private readonly Func<Type, ViewModelBase> _viewModelFactory;


        private ViewModelBase _currentView;
        public ViewModelBase CurrentView
        {
            get =>  _currentView;
            private set
            {
                _currentView = value;
                OnPropertyChanged(nameof(CurrentView));
            }
        }


        public NavigationService(Func<Type, ViewModelBase> viewModelFactory)
        {
            _viewModelFactory = viewModelFactory;
        }


        /// <summary>
        ///  Navigates to a new ViewModel by creating an instance of the specified ViewModel type 
        ///  and setting it as the current view model.
        /// </summary>
        /// <typeparam name="TViewModel"> The type of ViewModel to create, which must derive from ViewModelBase </typeparam>
        /// <example>
        ///     NavigateTo<InvoiceContentViewModel>();
        /// </example>
        public void NavigateTo<TViewModel>() where TViewModel : ViewModelBase
        {
            CurrentView = _viewModelFactory.Invoke(typeof(TViewModel));
        }


        /// <summary>
        ///   Creates and returns an instance of the specified ViewModel type.
        /// </summary>
        /// <typeparam name="TViewModel"> The type of ViewModel to create, which must derive from ViewModelBase class </typeparam>
        /// <returns> An instance of the specified TViewModel type. </returns>
        public TViewModel Navigate<TViewModel>() where TViewModel : ViewModelBase
        {
            return (TViewModel)_viewModelFactory.Invoke(typeof(TViewModel));
        }
    }
}
