using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
// 
using MediaPlaylist.Core;
using MediaPlaylist.Services;
using MediaPlaylistBL;
using MediaPlaylistStore;

namespace MediaPlaylist.ViewModels.PlaylistViewModels
{
    public class UpdatePlaylistViewModel : ViewModelBase, INavigatable
    {
        private readonly INavigationService _navigationService;
        private readonly NavigationBarViewModel _navigationBarViewModel;
        private readonly PlaylistManager _playlistManager;
        private PlaylistViewModel _playlist;


        #region Properties
        public ObservableCollection<Media> Medias { get; private set; }
        public string HeaderTxt { get; private set; } = string.Empty;

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

        public ICommand UpdatePlaylistCommand { get; }
        public ICommand RemoveMediaCommand { get; }
        public ICommand BackToPlaylistCommand { get; }

        public UpdatePlaylistViewModel(INavigationService navigationService, PlaylistManager manager, NavigationBarViewModel navViewModel) 
        { 
            _navigationService = navigationService;
            _playlistManager = manager;
            _navigationBarViewModel = navViewModel;

            BackToPlaylistCommand = new CommandBase(_ => _navigationService.NavigateTo<PlaylistDetailsViewModel>(_playlist));
            UpdatePlaylistCommand = new CommandBase(_ => UpdatePlaylist());
            RemoveMediaCommand = new CommandBase(RemoveMedia);
        }


        public void Initialize(object parameter)
        {
            if (parameter is PlaylistViewModel playlistItem)
            {
                _playlist = playlistItem;
                HeaderTxt = $"Update {playlistItem.Title}";
                Title = playlistItem.Title;
                Description = playlistItem.Description;
                Medias = new ObservableCollection<Media>(_playlist.Medias);
            }
        }


        private void RemoveMedia(object parameter)
        {
            try
            {
               if (parameter is Media media)
               {
                    _playlistManager.RemoveMediaFromPlaylist(_playlist.Id, media.Id);
                    Medias.Remove(media);  
               }
            }
            catch (Exception ex)
            {
                //Debug.WriteLine(ex);
                MessageBox.Show($"Error removing media from playlist: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void UpdatePlaylist()
        {
            try
            {
                if (string.IsNullOrEmpty(Title)) throw new Exception("Title cannot be empty");

                _playlistManager.UpdatePlaylist(_playlist.Id, Title, Description, _playlist.Medias);
                ReplaceItem();
                _navigationService.NavigateTo<PlaylistDetailsViewModel>(_playlist);
            }
            catch (Exception ex) 
            {
                //Debug.WriteLine(ex);
                MessageBox.Show($"Error updating playlist: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        /// <summary>
        ///   Replaces an existing item in the playlists with a new item if it is found.
        /// </summary>
        private void ReplaceItem()
        {
            // Find the index of the item to replace
            int index = _navigationBarViewModel.Playlists.IndexOf(_navigationBarViewModel.Playlists.FirstOrDefault(item => item.Id == _playlist.Id));

            if (index != -1)
            {
                _navigationBarViewModel.Playlists[index] = null;
                _navigationBarViewModel.Playlists[index] = _playlist;
            }
        }
    }
}
