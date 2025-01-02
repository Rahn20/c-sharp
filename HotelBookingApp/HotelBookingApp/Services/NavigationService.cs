using HotelBookingApp.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HotelBookingApp.Services
{
    public class NavigationService : INavigationService
    {
        private readonly Func<Type, ViewModelBase> _viewModelFactory;


        private ViewModelBase _currentViewModel;
        public ViewModelBase CurrentViewModel
        {
            get => _currentViewModel;
            private set 
            {
                if (_currentViewModel != value) 
                { 
                    _currentViewModel = value;
                    CurrentViewModelChanged?.Invoke();
                }
            }
        }

        public NavigationService(Func<Type, ViewModelBase> viewModelFactory)
        {
            _viewModelFactory = viewModelFactory;
        }

        public event Action CurrentViewModelChanged;

        public void NavigateTo<TViewModel>() where TViewModel : ViewModelBase
        {
            var viewModel = _viewModelFactory.Invoke(typeof(TViewModel));
            CurrentViewModel = viewModel;
        }
    }
}
