using System;
using MediaPlaylist.Core;
using MediaPlaylist.Services;

namespace MediaPlaylist.ViewModels
{
    public class MainViewModel : ViewModelBase, IDisposable
    {
        private readonly INavigationService _navigationService;

        public ViewModelBase CurrentViewModel => _navigationService.CurrentViewModel;
        public ViewModelBase NavViewModel { get; }


        public MainViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            _navigationService.CurrentViewModelChanged += OnCurrentViewModelChanged;

            NavViewModel = _navigationService.Navigate<NavigationBarViewModel>();
            _navigationService.NavigateTo<StartPageViewModel>();
        }

        private void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }

        public void Dispose()
        {
            _navigationService.CurrentViewModelChanged -= OnCurrentViewModelChanged;
        }
    }
}
