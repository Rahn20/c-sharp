using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Input;
//
using MediaPlaylist.Core;
using MediaPlaylist.Services;
using MediaPlaylist.ViewModels.PlaylistViewModels;
using MediaPlaylistBL;
using MediaPlaylistStore;
using System.Windows;

namespace MediaPlaylist.ViewModels
{
    public class NavigationBarViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private readonly ApplicationManager _appManager;

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

        public NavigationBarViewModel(INavigationService navigationService, ApplicationManager manager) 
        {
            _navigationService = navigationService;
            _appManager = manager;
            LoadData();

            AddNewPlaylistCommand = new CommandBase(_ => NavigateToAddPlaylistPage());
            NavigateToStartPageCommand = new CommandBase(_ => NavigateToStartPage());
            //Playlists.CollectionChanged += OnPlaylistChanged;
        }


        // Asynchronously loads playlist data from the data source and updates the UI.
        private async void LoadData()
        {
            try
            {
                Playlists = new ObservableCollection<PlaylistViewModel>();
                IEnumerable<Playlist> getPlaylists = await _appManager.GetPlaylists();

                foreach (Playlist playlist in getPlaylists) 
                {
                    PlaylistViewModel viewModel = new PlaylistViewModel(playlist);
                    Playlists.Add(viewModel);
                }
            }
            catch (Exception ex) 
            {
                MessageBox.Show($"Error retrieving playlist: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
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
