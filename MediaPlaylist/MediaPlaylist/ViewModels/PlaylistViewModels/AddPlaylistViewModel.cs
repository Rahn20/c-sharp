using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Threading;
using MediaPlaylist.Core;
using MediaPlaylist.Services;
using MediaPlaylistBL;

namespace MediaPlaylist.ViewModels.PlaylistViewModels
{
    public class AddPlaylistViewModel : ViewModelBase
    {
        private readonly ApplicationManager _appManager;
        private readonly NavigationBarViewModel _navigationBarViewModel;

        #region Properties
        public string _statusMessage = string.Empty;
        public string StatusMessage
        {
            get => _statusMessage;
            set
            {
                if (_statusMessage != value)
                {
                    _statusMessage = value;
                    OnPropertyChanged(nameof(StatusMessage));
                }
            }
        }

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

        public AsyncCommandBase AddPlaylistCommand { get; }

        public AddPlaylistViewModel(INavigationService navigationService, ApplicationManager manager, NavigationBarViewModel navBarViewModel)
        {
            _appManager = manager;
            _navigationBarViewModel = navBarViewModel;
            AddPlaylistCommand = new AsyncCommandBase(async _ => await AddPlaylist());
        }

        public async Task AddPlaylist()
        {
            try
            {
                if (string.IsNullOrEmpty(Title)) throw new Exception("Title cannot be empty");

                await _appManager.AddPlaylist(Title, Description);
                PlaylistViewModel viewModel = new PlaylistViewModel(await _appManager.GetLastPlaylist());
                _navigationBarViewModel.Playlists.Add(viewModel);
                ShowMessage();

                // Clear inputs
                Title = string.Empty;
                Description = string.Empty;
            }
            catch (Exception ex)
            {
                //Debug.WriteLine(ex);
                MessageBox.Show($"Error adding playlist: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Displays a status message and automatically hides the message after a 10-second delay using a timer.
        private void ShowMessage()
        {
            StatusMessage = "Playlist has successfully been added";

            // Timer to hide the message after a delay of 10 second
            var timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(10) };
            timer.Tick += (s, e) =>
            {
                StatusMessage = string.Empty;
                timer.Stop();
            };
            timer.Start();
        }
    }
}
