using MediaPlaylist.Core;
using MediaPlaylist.Services;
using MediaPlaylistBL;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;


namespace MediaPlaylist.ViewModels.PlaylistViewModels
{
    public class AddPlaylistViewModel : ViewModelBase
    {
        private readonly INavigationService _navigationService;
        private PlaylistManager _playlistManager;
        private NavigationBarViewModel _navigationBarViewModel;

        #region Properties
        private string _title = string.Empty;
        public string Title
        {
            get => _title;
            set
            {
                _title = value;
                OnPropertyChanged(nameof(Title));
            }
        }

        private string _description = string.Empty;
        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                OnPropertyChanged(nameof(Description));
            }
        }
        #endregion

        public ICommand AddPlaylistCommand { get; }

        public AddPlaylistViewModel(
            INavigationService navigationService, PlaylistManager playlistManager, NavigationBarViewModel navBarViewModel)
        {
            _navigationService = navigationService;
            _playlistManager = playlistManager;
            _navigationBarViewModel = navBarViewModel;

            AddPlaylistCommand = new CommandBase(_ => AddPlaylist());
        }


        public void AddPlaylist()
        {
            try
            {
                if (string.IsNullOrEmpty(Title)) throw new Exception("Title cannot be empty");

                _playlistManager.CreatePlaylist(Title, Description);
                PlaylistViewModel viewModel = new PlaylistViewModel(_playlistManager.GetLastPlaylist());
                _navigationBarViewModel.Playlists.Add(viewModel);
            }
            catch (Exception ex)
            {
                //Debug.WriteLine(ex);
                MessageBox.Show($"Error adding playlist: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
