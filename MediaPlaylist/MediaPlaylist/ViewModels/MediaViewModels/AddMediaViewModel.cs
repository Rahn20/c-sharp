using MediaPlaylist.Core;
using MediaPlaylist.Services;
using MediaPlaylist.ViewModels.PlaylistViewModels;
using MediaPlaylistStore;
using MediaPlaylistBL;
using UtilitiesLib;
//
using System.Diagnostics;
using System.Windows.Media;
using System.Windows.Input;
using Microsoft.Win32;
using System.IO;
using System.Windows;

namespace MediaPlaylist.ViewModels.MediaViewModels
{
    public class AddMediaViewModel : ViewModelBase, INavigatable
    {
        private readonly PlaylistManager _playlistManager;
        private readonly INavigationService _navigationService;
        private PlaylistViewModel _playlist;
        private MediaPlayer _mediaPlayer = new MediaPlayer();


        #region Properties
        public MediaViewModel MediaProperties { get; }
        public string MediaHeader { get; private set; } = string.Empty;

        public bool IsSongSelected => SelectedAudioType == AudioType.Song;
        public bool IsPodcastSelected => SelectedAudioType == AudioType.Podcast;
        public bool IsAudiobookSelected => SelectedAudioType == AudioType.Audiobook;

        public bool IsTitleAvailable => 
            string.IsNullOrEmpty(MediaProperties.MediaTitle) && 
            string.IsNullOrEmpty(MediaProperties.MediaFullPath) || 
            !string.IsNullOrEmpty(MediaProperties.MediaTitle) && !string.IsNullOrEmpty(MediaProperties.MediaFullPath);
        public List<AudioType> AudioTypes { get; }

        private AudioType _selectedAudioType;
        public AudioType SelectedAudioType
        {
            get => _selectedAudioType;
            set
            {
                if (_selectedAudioType != value)
                {
                    _selectedAudioType = value;
                    OnPropertyChanged(nameof(IsSongSelected));
                    OnPropertyChanged(nameof(IsPodcastSelected));
                    OnPropertyChanged(nameof(IsAudiobookSelected));

                    ClearMediaInputs();
                    ClearProdcastInputs();
                    ClearAudioBookInputs();
                    ClearSongInputs();
                }
            }
        }

        #endregion

        public ICommand SelectMediaPathCommand { get; }
        public ICommand CreateMediaCommand { get; }
        public ICommand BackToPlaylistCommand { get; }

        public AddMediaViewModel(INavigationService navigationService, PlaylistManager manager) 
        { 
            _playlistManager = manager;
            _navigationService = navigationService;

            MediaProperties = new MediaViewModel();
            AudioTypes = Enum.GetValues(typeof(AudioType)).Cast<AudioType>().ToList();

            BackToPlaylistCommand = new CommandBase(_ => _navigationService.NavigateTo<PlaylistDetailsViewModel>(_playlist));
            SelectMediaPathCommand = new CommandBase(_ => SelectMedia());
            CreateMediaCommand = new CommandBase(_ => CreateMedia());
        }

        public void Initialize(object parameter)
        {
            if (parameter is PlaylistViewModel playlistItem)
            {
                _playlist = playlistItem;
                MediaHeader = $"Add new Media to {_playlist.Title}";
            }
        }

        private void SelectMedia()
        {
            ShellHelper? fileData = ReadFileData();
            if (fileData == null) return;

            // song properties
            MediaProperties.SongArtist = fileData.GetArtist();
            MediaProperties.SongAlbum = fileData.GetAlbum();
            MediaProperties.SongGenre = fileData.GetGenre();
                    
            // Audiobook properties
            MediaProperties.AudiobookAuthor = fileData.GetAuthor();
            MediaProperties.AudiobookGenre = fileData.GetGenre();
            OnPropertyChanged(nameof(IsTitleAvailable));
        }


        private ShellHelper? ReadFileData()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "MP3 files (*.mp3)|*.mp3|All files (*.*)|*.*";

            if (openFileDialog.ShowDialog() == true)
            {
                string filePath = openFileDialog.FileName;
                FileInfo fileInfo = new FileInfo(filePath);
                ShellHelper shell = new ShellHelper(filePath);

                MediaProperties.MediaFullPath = filePath;
                MediaProperties.MediaName = fileInfo.Name;
                MediaProperties.MediaSize = fileInfo.Length / 1000; // convert to kb
                MediaProperties.MediaDuration = shell.GetFileDuration();
                MediaProperties.MediaTitle = shell.GetTitle();
                return shell;
            }

            return null;
        }


        private bool ValidateMediaProps()
        {
            bool result = false;

            if (string.IsNullOrEmpty(MediaProperties.MediaTitle)) return false;

            if (SelectedAudioType == AudioType.Song)
            {
                result = true;
            }
            else if (SelectedAudioType == AudioType.Audiobook && !string.IsNullOrEmpty(MediaProperties.AudiobookPublisher))
            {
                result = true;
            }
            else if (SelectedAudioType == AudioType.Podcast && !string.IsNullOrEmpty(MediaProperties.PodcastHost))
            {
                result = true;
            }
            
            return result;
        }

        private void CreateMedia()
        {
            // required: podcast host, Audiobook publisher,titte 
            try
            {
                if (ValidateMediaProps() == false) throw new Exception("Media Title or Podcast Host or Audiobook Publisher is empty");
                
                Media? mediaDTO = MediaUIToDTO();
                if (mediaDTO != null)
                {
                    _playlistManager.AddMediaToPlaylist(_playlist.Id, mediaDTO);
                    //PlaylistViewModel playlistViewModel = new PlaylistViewModel(getCurrentPlaylist);
                    ClearMediaInputs();
                    ClearSongInputs();
                    ClearAudioBookInputs();
                    ClearProdcastInputs();

                    // navigate back to the playlist details
                    _navigationService.NavigateTo<PlaylistDetailsViewModel>(_playlist);
                }
            }
            catch (Exception ex) 
            {
                MessageBox.Show($"Error creating new media: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                //Debug.WriteLine(ex);
            }
        }


        //  Converts the UI representation of media properties to a (DTO).
        private Media? MediaUIToDTO()
        {
            Media mediaProps;

            switch (SelectedAudioType)
            {
                case AudioType.Song:
                    mediaProps = new Song()
                    {
                        Album = MediaProperties.SongAlbum,
                        Artist = MediaProperties.SongArtist,
                        Genre = MediaProperties.SongGenre,
                    };
                    break;
                case AudioType.Audiobook:
                    mediaProps = new Audiobook()
                    {
                        Author = MediaProperties.AudiobookAuthor,
                        Genre = MediaProperties.AudiobookGenre,
                        Publisher = MediaProperties.AudiobookPublisher,
                    };
                    break;
                case AudioType.Podcast:
                    mediaProps = new Podcast()
                    {
                        Host = MediaProperties.PodcastHost,
                        EpisodeNumber = MediaProperties.PodcastEpisodeNumber,
                        Guests = MediaProperties.PodcastGuests,
                    };
                    break;
                default:
                    return null;
            }

            InitializeMediaProperties(mediaProps);
            return mediaProps;
        }

        // Helper method to initialize common media properties
        private void InitializeMediaProperties(Media media)
        {
            media.Name = MediaProperties.MediaName;
            media.Title = MediaProperties.MediaTitle;
            media.Size = MediaProperties.MediaSize;
            media.FullPath = MediaProperties.MediaFullPath;
            media.Duration = MediaProperties.MediaDuration;
            media.AudioType = SelectedAudioType;
        }


        // Clears all common media textboxes
        private void ClearMediaInputs()
        {
            MediaProperties.MediaName = string.Empty;
            MediaProperties.MediaTitle = string.Empty;
            MediaProperties.MediaDuration = TimeSpan.Zero;
            MediaProperties.MediaFullPath = string.Empty;
            MediaProperties.MediaSize = 0;
        }

        // Clears all Song textboxes
        private void ClearSongInputs()
        {
            MediaProperties.SongAlbum = string.Empty;
            MediaProperties.SongArtist = string.Empty;
            MediaProperties.SongGenre = string.Empty;
        }

        // Clears all audiobook textboxes
        private void ClearAudioBookInputs()
        {
            MediaProperties.AudiobookPublisher = string.Empty;
            MediaProperties.AudiobookAuthor = string.Empty;
            MediaProperties.AudiobookGenre = string.Empty;

        }

        // Clears all podcast textboxes
        private void ClearProdcastInputs()
        {
            MediaProperties.PodcastEpisodeNumber = 0;
            MediaProperties.PodcastGuests = string.Empty;
            MediaProperties.PodcastHost = string.Empty;
        }
    }
}
