using System;
using MediaPlaylist.Core;

namespace MediaPlaylist.Services
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
        void NavigateTo<TViewModel>(object parameter = null) where TViewModel : ViewModelBase;

        /// <summary>
        ///   Creates and returns an instance of the specified ViewModel type.
        /// </summary>
        /// <typeparam name="TViewModel"> The type of ViewModel to navigate to  </typeparam>
        /// <returns> An instance of the specified viewmodel type </returns>
        TViewModel Navigate<TViewModel>() where TViewModel : ViewModelBase;


        /// <summary>
        ///   Occurs when the current view model changes.
        /// </summary>
        event Action CurrentViewModelChanged;
    }

    /// <summary>
    ///   Defines an interface for classes that can be navigated to with an initialization parameter.
    /// </summary>
    public interface INavigatable
    {
        /// <summary>
        ///   Initializes the navigatable object with the specified parameter.
        /// </summary>
        /// <param name="parameter"> An object used to initialize the navigatable instance. 
        /// This can be any type based on the specific implementation needs. </param>
        void Initialize(object parameter);
    }


    /// <summary>
    ///   Provides navigation services for view models, implementing the INavigationService interface.
    /// </summary>
    public class NavigationService : INavigationService
    {
        private readonly Func<Type, ViewModelBase> _viewModelFactory;
        private ViewModelBase _currentViewModel;

        /// <summary>
        ///   Gets or sets the currently active view model.
        /// </summary>
        public ViewModelBase CurrentViewModel
        {
            get => _currentViewModel;
            private set
            {
                _currentViewModel = value;
                CurrentViewModelChanged?.Invoke();
            }
        }


        public event Action CurrentViewModelChanged;

        public NavigationService(Func<Type, ViewModelBase> viewModelFactory)
        {
            _viewModelFactory = viewModelFactory;
        }


        public TViewModel Navigate<TViewModel>() where TViewModel : ViewModelBase
        {
            return (TViewModel)_viewModelFactory.Invoke(typeof(TViewModel));
        }


        public void NavigateTo<TViewModel>(object parameter = null) where TViewModel : ViewModelBase
        {
            var viewModel = (ViewModelBase)_viewModelFactory.Invoke(typeof(TViewModel));

            // If the viewmodel needs parameters, check if it implements an interface or a method for it
            if (viewModel is INavigatable viewModelWithParameter)
            {
                viewModelWithParameter.Initialize(parameter);
            }

            CurrentViewModel = viewModel;
        }
    }
}
