using System;
using System.Windows;
using System.Windows.Input;
//
using MediaPlaylist.Core;
using MediaPlaylist.Services;
using MediaPlaylist.ViewModels.PlaylistViewModels;
using MediaPlaylistBL;
using MediaPlaylistStore;

namespace MediaPlaylist.ViewModels.MediaViewModels
{
    public class UpdateMediaViewModel : ViewModelBase, INavigatable
    {
        private readonly ApplicationManager _appManager;
        private Media _selectedMedia;
        private PlaylistViewModel _playlistViewModel;

        #region Properties
        public MediaViewModel MediaProperties { get; }
        public string MediaHeader { get; private set; } = string.Empty;

        public string _statusMessage = string.Empty;
        public string StatusMessage
        {
            get => _statusMessage;
            private set
            {
                if (_statusMessage != value)
                {
                    _statusMessage = value;
                    OnPropertyChanged(nameof(StatusMessage));
                }
            }
        }


        public bool IsSongSelected => MediaProperties.AudioType == AudioType.Song;
        public bool IsPodcastSelected => MediaProperties.AudioType == AudioType.Podcast;
        public bool IsAudiobookSelected => MediaProperties.AudioType == AudioType.Audiobook;

        #endregion

        public AsyncCommandBase UpdateMediaCommand { get; }
        public ICommand BackToPlaylistCommand { get; }

        public UpdateMediaViewModel(INavigationService navigationService, ApplicationManager manager) 
        {
            _appManager = manager;
            MediaProperties = new MediaViewModel();
            UpdateMediaCommand = new AsyncCommandBase(async _ => await UpdateMedia());
            BackToPlaylistCommand = new CommandBase(_ => navigationService.NavigateTo<PlaylistDetailsViewModel>());
        }


        public void Initialize(object parameter)
        {
            StatusMessage = string.Empty;
            Media mediaItem =  ((dynamic)parameter).SelectedMedia;
            PlaylistViewModel playlistVM = ((dynamic)parameter).CurrentPlaylistVM;

            _selectedMedia = mediaItem;
            _playlistViewModel = playlistVM;
            MediaHeader = $"Update {_selectedMedia.Title}";
            InitializeMediaProperties();
            MediaDTOToUI();
        }

        private async Task UpdateMedia()
        {
            try
            {
                if (ValidateAndChangeMedia() is true)
                {
                    await _appManager.UpdateMediaItem(_selectedMedia);
                    StatusMessage = "Media has successfully been uppdated";
                }               
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating media: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Checks the selected media audio type and updates the UI inputs with the Media DTO
        private void MediaDTOToUI()
        {
            switch (_selectedMedia.AudioType)
            {
                case AudioType.Song:
                    Song song = (Song)_selectedMedia;
                    MediaProperties.SongAlbum = song.Album;
                    MediaProperties.SongArtist = song.Artist;
                    MediaProperties.SongGenre = song.Genre;
                    break;
                case AudioType.Audiobook:
                    Audiobook audiobook = (Audiobook)_selectedMedia;
                    MediaProperties.AudiobookAuthor = audiobook.Author;
                    MediaProperties.AudiobookGenre = audiobook.Genre;
                    MediaProperties.AudiobookPublisher = audiobook.Publisher;
                    break;
                case AudioType.Podcast:
                    Podcast podcast = (Podcast)_selectedMedia;
                    MediaProperties.PodcastHost = podcast.Host;
                    MediaProperties.PodcastEpisodeNumber = podcast.EpisodeNumber;
                    MediaProperties.PodcastGuests = podcast.Guests;
                    break;
            }
        }

        // Helper method to initialize common media properties
        private void InitializeMediaProperties()
        {
            MediaProperties.AudioType = _selectedMedia.AudioType;
            MediaProperties.MediaName = _selectedMedia.Name;
            MediaProperties.MediaTitle = _selectedMedia.Title;
            MediaProperties.MediaSize = _selectedMedia.Size;
            MediaProperties.MediaFullPath = _selectedMedia.FullPath;
            MediaProperties.MediaDuration = _selectedMedia.Duration; 
        }

        // Validates media properties and updates the Media DTO with the media properties from the inputs, True if Valid, otherwise False.
        private bool ValidateAndChangeMedia()
        {
            bool result = false;

            if (string.IsNullOrEmpty(MediaProperties.MediaTitle) || string.IsNullOrEmpty(MediaProperties.MediaName))
            {
                MessageBox.Show("Name and Title cannot be empty", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }

            _selectedMedia.Title = MediaProperties.MediaTitle;
            _selectedMedia.Name = MediaProperties.MediaName;

            if (_selectedMedia is Song song)
            {
                song.Album = MediaProperties.SongAlbum;
                song.Artist = MediaProperties.SongArtist;
                song.Genre = MediaProperties.SongGenre;
                result = true;
            }
            else if (_selectedMedia is Audiobook audiobook && !string.IsNullOrEmpty(MediaProperties.AudiobookPublisher))
            {
                audiobook.Publisher = MediaProperties.AudiobookPublisher;
                audiobook.Genre = MediaProperties.AudiobookGenre;
                audiobook.Author = MediaProperties.AudiobookAuthor;
                result = true;
            }
            else if (_selectedMedia is Podcast podcast && !string.IsNullOrEmpty(MediaProperties.PodcastHost))
            {
                podcast.Host = MediaProperties.PodcastHost;
                podcast.EpisodeNumber = MediaProperties.PodcastEpisodeNumber;
                podcast.Guests = MediaProperties.PodcastGuests;
                result = true;
            }

            return result;
        }

    }
}
