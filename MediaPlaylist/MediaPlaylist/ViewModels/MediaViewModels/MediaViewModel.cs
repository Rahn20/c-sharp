using MediaPlaylist.Core;
using MediaPlaylistStore;

namespace MediaPlaylist.ViewModels.MediaViewModels
{
    public class MediaViewModel : ViewModelBase
    {
        #region Media Properties
        private int _id;
        public int Id
        {
            get => _id;
            set
            {
                if (_id != value)
                {
                    _id = value;
                    OnPropertyChanged(nameof(Id));
                }
            }
        }

        private string _mediaTitle = string.Empty;
        public string MediaTitle
        {
            get => _mediaTitle;
            set
            {
                if (_mediaTitle != value)
                {
                    _mediaTitle = value;
                    OnPropertyChanged(nameof(MediaTitle));
                }
            }
        }

        private string _mediaName = string.Empty;
        public string MediaName
        {
            get => _mediaName;
            set
            {
                if (_mediaName != value)
                {
                    _mediaName = value;
                    OnPropertyChanged(nameof(MediaName));
                }
            }
        }

        private TimeSpan _mediaDuration;
        public TimeSpan MediaDuration
        {
            get => _mediaDuration;
            set
            {
                if (_mediaDuration != value)
                {
                    _mediaDuration = value;
                    OnPropertyChanged(nameof(MediaDuration));
                }
            }
        }

        private string _mediaFullPath = string.Empty;
        public string MediaFullPath
        {
            get => _mediaFullPath;
            set
            {
                if (_mediaFullPath != value)
                {
                    _mediaFullPath = value;
                    OnPropertyChanged(nameof(MediaFullPath));
                }
            }
        }

        private float _mediaSize = 0;
        public float MediaSize
        {
            get => _mediaSize;
            set
            {
                if (_mediaSize != value)
                {
                    _mediaSize = value;
                    OnPropertyChanged(nameof(MediaSize));
                }
            }
        }

        private AudioType _audioType;
        public AudioType AudioType
        {
            get => _audioType;
            set
            {
                if (_audioType != value)
                {
                    _audioType = value;
                    OnPropertyChanged(nameof(AudioType));
                }
            }
        }

        #endregion

        #region Song Properties

        private string _songArtist = string.Empty;
        public string SongArtist
        {
            get => _songArtist;
            set
            {
                if (_songArtist != value)
                {
                    _songArtist = value;
                    OnPropertyChanged(nameof(SongArtist));
                }
            }
        }

        private string _songGenre = string.Empty;
        public string SongGenre
        {
            get => _songGenre;
            set
            {
                if (_songGenre != value)
                {
                    _songGenre = value;
                    OnPropertyChanged(nameof(SongGenre));
                }
            }
        }

        private string _songAlbum = string.Empty;
        public string SongAlbum
        {
            get => _songAlbum;
            set
            {
                if (_songAlbum != value)
                {
                    _songAlbum = value;
                    OnPropertyChanged(nameof(SongAlbum));
                }
            }
        }

        #endregion

        #region Podcast properties

        private string _podcastHost = string.Empty;
        public string PodcastHost
        {
            get => _podcastHost;
            set
            {
                if (_podcastHost != value)
                {
                    _podcastHost = value;
                    OnPropertyChanged(nameof(PodcastHost));
                }
            }
        }

        private int _podcastEpisodeNumber = 0;
        public int PodcastEpisodeNumber
        {
            get => _podcastEpisodeNumber;
            set
            {
                if (_podcastEpisodeNumber != value)
                {
                    _podcastEpisodeNumber = value;
                    OnPropertyChanged(nameof(PodcastEpisodeNumber));
                }
            }
        }

        private string _podcastGuests = string.Empty;
        public string PodcastGuests
        {
            get => _podcastGuests;
            set
            {
                if (_podcastGuests != value)
                {
                    _podcastGuests = value;
                    OnPropertyChanged(nameof(PodcastGuests));
                }
            }
        }
        #endregion

        #region AudioBook properties

        private string _audiobookAuthor = string.Empty;
        public string AudiobookAuthor
        {
            get => _audiobookAuthor;
            set
            {
                if (_audiobookAuthor != value)
                {
                    _audiobookAuthor = value;
                    OnPropertyChanged(nameof(AudiobookAuthor));
                }
            }
        }


        private string _audioGenre = string.Empty;
        public string AudiobookGenre
        {
            get => _audioGenre;
            set
            {
                if (_audioGenre != value)
                {
                    _audioGenre = value;
                    OnPropertyChanged(nameof(AudiobookGenre));
                }
            }
        }

        private string _audioPublisher = string.Empty;
        public string AudiobookPublisher
        {
            get => _audioPublisher;
            set
            {
                if (_audioPublisher != value)
                {
                    _audioPublisher = value;
                    OnPropertyChanged(nameof(AudiobookPublisher));
                }
            }
        }

        #endregion
    }
}
