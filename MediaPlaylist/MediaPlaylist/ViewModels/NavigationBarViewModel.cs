using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;
//
using MediaPlaylist.Core;
using MediaPlaylist.Services;
using MediaPlaylist.ViewModels.PlaylistViewModels;
using MediaPlaylistBL;

namespace MediaPlaylist.ViewModels
{
    public class NavigationBarViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private PlaylistManager _playlistManager;

        #region Properties
        public ObservableCollection<PlaylistViewModel> Playlists { get; set; }

        private PlaylistViewModel _selectedPlaylist;
        public PlaylistViewModel SelectedPlaylist
        {
            get => _selectedPlaylist;
            set
            {
                if (_selectedPlaylist != value)
                {
                    _selectedPlaylist = value;
                    OnPropertyChanged(nameof(SelectedPlaylist));

                    if (_selectedPlaylist != null)
                    {
                        // Navigate to the PlaylistDetailsViewModel when a playlist is selected
                        _navigationService.NavigateTo<PlaylistDetailsViewModel>(_selectedPlaylist);
                    
                    }
                }
            }
        }

        private bool _isPlaylistSelected;
        public bool IsPlaylistSelected
        {
            get => _isPlaylistSelected;
            set
            {
                _isPlaylistSelected = value;
                OnPropertyChanged(nameof(IsPlaylistSelected));    
            }
        }

        #endregion

        public ICommand NavigateToStartPageCommand { get; }
        public ICommand AddNewPlaylistCommand { get; }

        public NavigationBarViewModel(INavigationService navigationService, PlaylistManager manager) 
        {
            _navigationService = navigationService;
            _playlistManager = manager;
            Playlists = new ObservableCollection<PlaylistViewModel>();

            AddNewPlaylistCommand = new CommandBase(_ => NavigateToAddPlaylistPage());
            NavigateToStartPageCommand = new CommandBase(_ => NavigateToStartPage());

            //Playlists.CollectionChanged += OnPlaylistChanged;
        }

        private void NavigateToStartPage() 
        {
            // Fixa sen
            SelectedPlaylist = null;
            IsPlaylistSelected = true;
            IsPlaylistSelected = false;
            _navigationService.NavigateTo<StartPageViewModel>();
        }

        private void NavigateToAddPlaylistPage()
        {
            // Fixa sen
            SelectedPlaylist = null;
            IsPlaylistSelected = true;
            IsPlaylistSelected = false;
            _navigationService.NavigateTo<AddPlaylistViewModel>();
        }
    }
}
