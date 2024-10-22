using System;
using MediaPlaylist.Core;
using MediaPlaylist.Services;
using MediaPlaylist.ViewModels.MediaViewModels;
using MediaPlaylistBL;
using MediaPlaylistStore;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;


namespace MediaPlaylist.ViewModels.PlaylistViewModels
{
    public class PlaylistDetailsViewModel : ViewModelBase, INavigatable
    {
        private readonly INavigationService _navigationService;
        private readonly ApplicationManager _appManager;
        private readonly NavigationBarViewModel _navigationBarViewModel;
        private readonly MediaPlayer _mediaPlayer;
        private readonly DispatcherTimer _timer;
        private Media _selectedMedia;

        #region Properties
        public List<AudioType> AudioTypes { get; }
        public ObservableCollection<Media> Medias { get; private set; }

        private int _numberOfMedia;
        public int NumberOfMedia
        {
            get => _numberOfMedia;
            private set
            {
                _numberOfMedia = value;
                OnPropertyChanged(nameof(NumberOfMedia));
            }
        }

        private PlaylistViewModel _playlist;
        public PlaylistViewModel Playlist
        {
            get => _playlist;
            set
            {
                _playlist = value;
                OnPropertyChanged(nameof(Playlist));
            }
        }

        private AudioType _selectedAudioType;
        public AudioType SelectedAudioType
        {
            get => _selectedAudioType;
            set
            {
                if (_selectedAudioType == value) return;

                _selectedAudioType = value;
                OnPropertyChanged(nameof(SelectedAudioType));
            }
        }

        private string _searchMediaBox = string.Empty;
        public string SearchMediaBox
        {
            get => _searchMediaBox;
            set
            {
                _searchMediaBox = value;
                OnPropertyChanged(nameof(SearchMediaBox));
            }
        }

        #endregion

        #region Media Card properties

        private string _currentMediaTitle = string.Empty;
        public string CurrentMediaTitle
        {
            get => _currentMediaTitle;
            set
            {
                _currentMediaTitle = value;
                OnPropertyChanged(nameof(CurrentMediaTitle));
            }
        }

        private string _currentMediaTime = string.Empty;
        public string CurrentMediaTime
        {
            get => _currentMediaTime;
            set
            {
                _currentMediaTime = value;
                OnPropertyChanged(nameof(CurrentMediaTime));
            }
        }

        // The total seconds of the media Duration
        private double _totalMediaDurationInSec;
        public double TotalMediaDurationInSec
        {
            get => _totalMediaDurationInSec;
            private set
            {
                _totalMediaDurationInSec = value;
                OnPropertyChanged(nameof(TotalMediaDurationInSec));
            }
        }

        // The total seconds of current media player Position
        private double _currentMediaPositionInSec;
        public double CurrentMediaPositionInSec
        {
            get => _currentMediaPositionInSec;
            set
            {
                _currentMediaPositionInSec = value;
                OnPropertyChanged(nameof(CurrentMediaPositionInSec));
            }
        }

        #endregion

        #region Playlist Commands
        public ICommand UpdatePlaylistCommand { get; private set; }
        public AsyncCommandBase RemovePlaylistCommand { get; private set; }
        public ICommand AddNewMediaCommand { get; private set; }
        #endregion

        #region Media Commands
        public AsyncCommandBase SearchMediaCommand { get; private set; }
        public ICommand ClearSearchMediaCommand { get; private set; }

        public ICommand StartMediaCommand { get; private set; }
        public ICommand StopMediaCommand { get; private set; }
        public ICommand PauseMediaCommand { get; private set; }
        public ICommand PlayMediaCommand { get; private set; }

        public AsyncCommandBase DeleteMediaCommand { get; private set; }
        public ICommand EditMediaCommand { get; private set; }
        #endregion

        public PlaylistDetailsViewModel(
            INavigationService navigationService, ApplicationManager manager, NavigationBarViewModel navVM)
        {
            _appManager = manager;
            _navigationService = navigationService;
            _navigationBarViewModel = navVM;
            _mediaPlayer = new MediaPlayer();
            _timer = new DispatcherTimer()
            {
                Interval = TimeSpan.FromSeconds(1)
            };

            // Define what happens on every tick
            _timer.Tick += Timer_tick;

            AudioTypes = Enum.GetValues(typeof(AudioType)).Cast<AudioType>().ToList();
            Medias = new ObservableCollection<Media>();

            AllCommands();
        }

        public void Initialize(object parameter)
        {
            if (parameter is PlaylistViewModel playlistItem)
            {
                Playlist = playlistItem;
                NumberOfMedia = Playlist.Medias.Count;
                Medias.Clear();
                Playlist.Medias.ForEach(item => Medias.Add(item));

                if (NumberOfMedia != 0 && _selectedMedia == null)
                {
                    _selectedMedia = Playlist.Medias[0];
                    _mediaPlayer.Open(new Uri(_selectedMedia.FullPath));

                    CurrentMediaTitle = _selectedMedia.Title;
                    TotalMediaDurationInSec = _selectedMedia.Duration.TotalSeconds;
                    UpdateMediaTime();
                }
            }
        }

        private void AllCommands()
        {
            RemovePlaylistCommand = new AsyncCommandBase(async _ => await RemovePlaylist());
            SearchMediaCommand = new AsyncCommandBase(async _ => await SearchMedia());
            ClearSearchMediaCommand = new CommandBase(_ =>
            {
                SearchMediaBox = string.Empty;
                Medias.Clear();
                Playlist.Medias.ForEach(item => Medias.Add(item));
            });

            // media Commands
            StartMediaCommand = new CommandBase(StartMedia);
            StopMediaCommand = new CommandBase(_ => StopMedia());
            PauseMediaCommand = new CommandBase(_ => PauseMedia());
            PlayMediaCommand = new CommandBase(_ => PlayMedia());
            DeleteMediaCommand = new AsyncCommandBase(async (parameter) => await RemoveMediaItem(parameter));
            EditMediaCommand = new CommandBase(EditMediaItem);

            AddNewMediaCommand = new CommandBase(_ => _navigationService.NavigateTo<AddMediaViewModel>(Playlist));
            UpdatePlaylistCommand = new CommandBase(_ => _navigationService.NavigateTo<UpdatePlaylistViewModel>(Playlist));
        }

        private async Task RemovePlaylist()
        {
            if (Playlist == null) return;

            try
            {
                await _appManager.RemovePlaylist(Playlist.Playlist);
                _navigationBarViewModel.Playlists.Remove(Playlist);
                _navigationService.NavigateTo<StartPageViewModel>();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error removing playlist: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private async Task SearchMedia()
        {
            try
            {
                if (!string.IsNullOrEmpty(_searchMediaBox))
                {
                    List<Media> result = await _appManager.SearchForMedia(Playlist.Id, SelectedAudioType, _searchMediaBox);
                    Medias.Clear();
                    result.ForEach(item => Medias.Add(item));
                }
                else
                {
                    MessageBox.Show("The Search box cannot be empty", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error searching media: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void StartMedia(object selectedMedia)
        {
            Media? mediaObj = selectedMedia as Media;

            if (mediaObj == null) return;
            try
            {
                _selectedMedia = mediaObj;
                _mediaPlayer.Open(new Uri(mediaObj.FullPath));

                _timer.Start();
                _mediaPlayer.Play();

                CurrentMediaTitle = mediaObj.Title;
                TotalMediaDurationInSec = mediaObj.Duration.TotalSeconds;
                UpdateMediaTime();
            }
            catch (Exception ex)
            {
                //Debug.WriteLine(ex);
                MessageBox.Show($"Error playing the media item: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void EditMediaItem(object selectedMedia)
        {
            if (selectedMedia is Media media)
            {
                // Using an Anonymous Type
                var parameter = new { SelectedMedia = media, CurrentPlaylistVM = Playlist };

                _navigationService.NavigateTo<UpdateMediaViewModel>(parameter);
            }
        }

        private async Task RemoveMediaItem(object selectedMedia)
        {
            Media? mediaObj = selectedMedia as Media;

            if (mediaObj == null) return;
            try
            {
                await _appManager.RemoveMedia(mediaObj);
                Medias.Remove(mediaObj);
                NumberOfMedia = Playlist.Medias.Count;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error removing the media item: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void UpdateMediaTime()
        {
            // Update the time display
            CurrentMediaTime = String.Format("{0} / {1}",
                _mediaPlayer.Position.ToString(@"hh\:mm\:ss"),
                _selectedMedia.Duration.ToString(@"hh\:mm\:ss"));
        }

        private void Timer_tick(object sender, EventArgs e)
        {
            // Update the current playback time in The UI
            if (_mediaPlayer.NaturalDuration.HasTimeSpan)
            {
                CurrentMediaPositionInSec = _mediaPlayer.Position.TotalSeconds;
            }

            // If the media has reached the end, stop the timer, play next media
            if (_mediaPlayer.Position >= _selectedMedia.Duration)
            {
                StopMedia();

                // Find the index of the media object by matching the 'Id' property
                int currentMediaIndex = Playlist.Medias.FindIndex(elem => elem.MediaId == _selectedMedia.MediaId);

                if (currentMediaIndex < Playlist.Medias.Count - 1)
                {
                    object nextMedia = Playlist.Medias[currentMediaIndex + 1];
                    StartMedia(nextMedia);
                }
            }
            UpdateMediaTime();
        }

        private void StopMedia()
        {
            if (_selectedMedia != null)
            {
                _mediaPlayer.Position = TimeSpan.Zero;
                CurrentMediaPositionInSec = 0;
                _mediaPlayer.Stop();
                _timer.Stop();
            }
            else
            {
                MessageBox.Show("No media is selected", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void PauseMedia() => _mediaPlayer.Pause();

        private void PlayMedia()
        {
            try
            {
                if (_selectedMedia == null) throw new Exception("These is no media in the playlist: click on add enw media to add new media");

                if (_mediaPlayer.Position == TimeSpan.Zero)
                {
                    _timer.Start();
                }

                _mediaPlayer.Play();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error playing media: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void SliderDragging() => _mediaPlayer.Position = TimeSpan.FromSeconds(CurrentMediaPositionInSec);
    }
}
